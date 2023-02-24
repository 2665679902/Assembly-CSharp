using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x02000802 RID: 2050
public class LogicCircuitNetwork : UtilityNetwork
{
	// Token: 0x06003B46 RID: 15174 RVA: 0x00148B38 File Offset: 0x00146D38
	public override void AddItem(object item)
	{
		if (item is LogicWire)
		{
			LogicWire logicWire = (LogicWire)item;
			LogicWire.BitDepth maxBitDepth = logicWire.MaxBitDepth;
			List<LogicWire> list = this.wireGroups[(int)maxBitDepth];
			if (list == null)
			{
				list = new List<LogicWire>();
				this.wireGroups[(int)maxBitDepth] = list;
			}
			list.Add(logicWire);
			return;
		}
		if (item is ILogicEventReceiver)
		{
			ILogicEventReceiver logicEventReceiver = (ILogicEventReceiver)item;
			this.receivers.Add(logicEventReceiver);
			return;
		}
		if (item is ILogicEventSender)
		{
			ILogicEventSender logicEventSender = (ILogicEventSender)item;
			this.senders.Add(logicEventSender);
		}
	}

	// Token: 0x06003B47 RID: 15175 RVA: 0x00148BB8 File Offset: 0x00146DB8
	public override void RemoveItem(object item)
	{
		if (item is LogicWire)
		{
			LogicWire logicWire = (LogicWire)item;
			this.wireGroups[(int)logicWire.MaxBitDepth].Remove(logicWire);
			return;
		}
		if (item is ILogicEventReceiver)
		{
			ILogicEventReceiver logicEventReceiver = item as ILogicEventReceiver;
			this.receivers.Remove(logicEventReceiver);
			return;
		}
		if (item is ILogicEventSender)
		{
			ILogicEventSender logicEventSender = (ILogicEventSender)item;
			this.senders.Remove(logicEventSender);
		}
	}

	// Token: 0x06003B48 RID: 15176 RVA: 0x00148C22 File Offset: 0x00146E22
	public override void ConnectItem(object item)
	{
		if (item is ILogicEventReceiver)
		{
			((ILogicEventReceiver)item).OnLogicNetworkConnectionChanged(true);
			return;
		}
		if (item is ILogicEventSender)
		{
			((ILogicEventSender)item).OnLogicNetworkConnectionChanged(true);
		}
	}

	// Token: 0x06003B49 RID: 15177 RVA: 0x00148C4D File Offset: 0x00146E4D
	public override void DisconnectItem(object item)
	{
		if (item is ILogicEventReceiver)
		{
			ILogicEventReceiver logicEventReceiver = item as ILogicEventReceiver;
			logicEventReceiver.ReceiveLogicEvent(0);
			logicEventReceiver.OnLogicNetworkConnectionChanged(false);
			return;
		}
		if (item is ILogicEventSender)
		{
			(item as ILogicEventSender).OnLogicNetworkConnectionChanged(false);
		}
	}

	// Token: 0x06003B4A RID: 15178 RVA: 0x00148C80 File Offset: 0x00146E80
	public override void Reset(UtilityNetworkGridNode[] grid)
	{
		this.resetting = true;
		this.previousValue = -1;
		this.outputValue = 0;
		for (int i = 0; i < 2; i++)
		{
			List<LogicWire> list = this.wireGroups[i];
			if (list != null)
			{
				for (int j = 0; j < list.Count; j++)
				{
					LogicWire logicWire = list[j];
					if (logicWire != null)
					{
						int num = Grid.PosToCell(logicWire.transform.GetPosition());
						UtilityNetworkGridNode utilityNetworkGridNode = grid[num];
						utilityNetworkGridNode.networkIdx = -1;
						grid[num] = utilityNetworkGridNode;
					}
				}
				list.Clear();
			}
		}
		this.senders.Clear();
		this.receivers.Clear();
		this.resetting = false;
		this.RemoveOverloadedNotification();
	}

	// Token: 0x06003B4B RID: 15179 RVA: 0x00148D34 File Offset: 0x00146F34
	public void UpdateLogicValue()
	{
		if (this.resetting)
		{
			return;
		}
		this.previousValue = this.outputValue;
		this.outputValue = 0;
		foreach (ILogicEventSender logicEventSender in this.senders)
		{
			logicEventSender.LogicTick();
		}
		foreach (ILogicEventSender logicEventSender2 in this.senders)
		{
			int logicValue = logicEventSender2.GetLogicValue();
			this.outputValue |= logicValue;
		}
	}

	// Token: 0x06003B4C RID: 15180 RVA: 0x00148DF0 File Offset: 0x00146FF0
	public int GetBitsUsed()
	{
		int num;
		if (this.outputValue > 1)
		{
			num = 4;
		}
		else
		{
			num = 1;
		}
		return num;
	}

	// Token: 0x06003B4D RID: 15181 RVA: 0x00148E0F File Offset: 0x0014700F
	public bool IsBitActive(int bit)
	{
		return (this.OutputValue & (1 << bit)) > 0;
	}

	// Token: 0x06003B4E RID: 15182 RVA: 0x00148E21 File Offset: 0x00147021
	public static bool IsBitActive(int bit, int value)
	{
		return (value & (1 << bit)) > 0;
	}

	// Token: 0x06003B4F RID: 15183 RVA: 0x00148E2E File Offset: 0x0014702E
	public static int GetBitValue(int bit, int value)
	{
		return value & (1 << bit);
	}

	// Token: 0x06003B50 RID: 15184 RVA: 0x00148E38 File Offset: 0x00147038
	public void SendLogicEvents(bool force_send, int id)
	{
		if (this.resetting)
		{
			return;
		}
		if (this.outputValue != this.previousValue || force_send)
		{
			foreach (ILogicEventReceiver logicEventReceiver in this.receivers)
			{
				logicEventReceiver.ReceiveLogicEvent(this.outputValue);
			}
			if (!force_send)
			{
				this.TriggerAudio((this.previousValue >= 0) ? this.previousValue : 0, id);
			}
		}
	}

	// Token: 0x06003B51 RID: 15185 RVA: 0x00148EC8 File Offset: 0x001470C8
	private void TriggerAudio(int old_value, int id)
	{
		SpeedControlScreen instance = SpeedControlScreen.Instance;
		if (old_value != this.outputValue && instance != null && !instance.IsPaused)
		{
			int num = 0;
			GridArea visibleArea = GridVisibleArea.GetVisibleArea();
			List<LogicWire> list = new List<LogicWire>();
			for (int i = 0; i < 2; i++)
			{
				List<LogicWire> list2 = this.wireGroups[i];
				if (list2 != null)
				{
					for (int j = 0; j < list2.Count; j++)
					{
						num++;
						if (visibleArea.Min <= list2[j].transform.GetPosition() && list2[j].transform.GetPosition() <= visibleArea.Max)
						{
							list.Add(list2[j]);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				int num2 = Mathf.CeilToInt((float)(list.Count / 2));
				if (list[num2] != null)
				{
					Vector3 position = list[num2].transform.GetPosition();
					position.z = 0f;
					string text = "Logic_Circuit_Toggle";
					LogicCircuitNetwork.LogicSoundPair logicSoundPair = new LogicCircuitNetwork.LogicSoundPair();
					if (!LogicCircuitNetwork.logicSoundRegister.ContainsKey(id))
					{
						LogicCircuitNetwork.logicSoundRegister.Add(id, logicSoundPair);
					}
					else
					{
						logicSoundPair.playedIndex = LogicCircuitNetwork.logicSoundRegister[id].playedIndex;
						logicSoundPair.lastPlayed = LogicCircuitNetwork.logicSoundRegister[id].lastPlayed;
					}
					if (logicSoundPair.playedIndex < 2)
					{
						LogicCircuitNetwork.logicSoundRegister[id].playedIndex = logicSoundPair.playedIndex + 1;
					}
					else
					{
						LogicCircuitNetwork.logicSoundRegister[id].playedIndex = 0;
						LogicCircuitNetwork.logicSoundRegister[id].lastPlayed = Time.time;
					}
					float num3 = (Time.time - logicSoundPair.lastPlayed) / 3f;
					EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound(text, false), position, 1f);
					eventInstance.setParameterByName("logic_volumeModifer", num3, false);
					eventInstance.setParameterByName("wireCount", (float)(num % 24), false);
					eventInstance.setParameterByName("enabled", (float)this.outputValue, false);
					KFMOD.EndOneShot(eventInstance);
				}
			}
		}
	}

	// Token: 0x06003B52 RID: 15186 RVA: 0x00149100 File Offset: 0x00147300
	public void UpdateOverloadTime(float dt, int bits_used)
	{
		bool flag = false;
		List<LogicWire> list = null;
		List<LogicUtilityNetworkLink> list2 = null;
		for (int i = 0; i < 2; i++)
		{
			List<LogicWire> list3 = this.wireGroups[i];
			List<LogicUtilityNetworkLink> list4 = this.relevantBridges[i];
			float num = (float)LogicWire.GetBitDepthAsInt((LogicWire.BitDepth)i);
			if ((float)bits_used > num && ((list4 != null && list4.Count > 0) || (list3 != null && list3.Count > 0)))
			{
				flag = true;
				list = list3;
				list2 = list4;
				break;
			}
		}
		if (list != null)
		{
			list.RemoveAll((LogicWire x) => x == null);
		}
		if (list2 != null)
		{
			list2.RemoveAll((LogicUtilityNetworkLink x) => x == null);
		}
		if (flag)
		{
			this.timeOverloaded += dt;
			if (this.timeOverloaded > 6f)
			{
				this.timeOverloaded = 0f;
				if (this.targetOverloadedWire == null)
				{
					if (list2 != null && list2.Count > 0)
					{
						int num2 = UnityEngine.Random.Range(0, list2.Count);
						this.targetOverloadedWire = list2[num2].gameObject;
					}
					else if (list != null && list.Count > 0)
					{
						int num3 = UnityEngine.Random.Range(0, list.Count);
						this.targetOverloadedWire = list[num3].gameObject;
					}
				}
				if (this.targetOverloadedWire != null)
				{
					this.targetOverloadedWire.Trigger(-794517298, new BuildingHP.DamageSourceInfo
					{
						damage = 1,
						source = BUILDINGS.DAMAGESOURCES.LOGIC_CIRCUIT_OVERLOADED,
						popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.LOGIC_CIRCUIT_OVERLOADED,
						takeDamageEffect = SpawnFXHashes.BuildingLogicOverload,
						fullDamageEffectName = "logic_ribbon_damage_kanim",
						statusItemID = Db.Get().BuildingStatusItems.LogicOverloaded.Id
					});
				}
				if (this.overloadedNotification == null)
				{
					this.timeOverloadNotificationDisplayed = 0f;
					this.overloadedNotification = new Notification(MISC.NOTIFICATIONS.LOGIC_CIRCUIT_OVERLOADED.NAME, NotificationType.BadMinor, null, null, true, 0f, null, null, this.targetOverloadedWire.transform, true, false, false);
					Game.Instance.FindOrAdd<Notifier>().Add(this.overloadedNotification, "");
					return;
				}
			}
		}
		else
		{
			this.timeOverloaded = Mathf.Max(0f, this.timeOverloaded - dt * 0.95f);
			this.timeOverloadNotificationDisplayed += dt;
			if (this.timeOverloadNotificationDisplayed > 5f)
			{
				this.RemoveOverloadedNotification();
			}
		}
	}

	// Token: 0x06003B53 RID: 15187 RVA: 0x0014937B File Offset: 0x0014757B
	private void RemoveOverloadedNotification()
	{
		if (this.overloadedNotification != null)
		{
			Game.Instance.FindOrAdd<Notifier>().Remove(this.overloadedNotification);
			this.overloadedNotification = null;
		}
	}

	// Token: 0x06003B54 RID: 15188 RVA: 0x001493A4 File Offset: 0x001475A4
	public void UpdateRelevantBridges(List<LogicUtilityNetworkLink>[] bridgeGroups)
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		for (int i = 0; i < bridgeGroups.Length; i++)
		{
			if (this.relevantBridges[i] != null)
			{
				this.relevantBridges[i].Clear();
			}
			for (int j = 0; j < bridgeGroups[i].Count; j++)
			{
				if (logicCircuitManager.GetNetworkForCell(bridgeGroups[i][j].cell_one) == this || logicCircuitManager.GetNetworkForCell(bridgeGroups[i][j].cell_two) == this)
				{
					if (this.relevantBridges[i] == null)
					{
						this.relevantBridges[i] = new List<LogicUtilityNetworkLink>();
					}
					this.relevantBridges[i].Add(bridgeGroups[i][j]);
				}
			}
		}
	}

	// Token: 0x17000432 RID: 1074
	// (get) Token: 0x06003B55 RID: 15189 RVA: 0x00149455 File Offset: 0x00147655
	public int OutputValue
	{
		get
		{
			return this.outputValue;
		}
	}

	// Token: 0x17000433 RID: 1075
	// (get) Token: 0x06003B56 RID: 15190 RVA: 0x00149460 File Offset: 0x00147660
	public int WireCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < 2; i++)
			{
				if (this.wireGroups[i] != null)
				{
					num += this.wireGroups[i].Count;
				}
			}
			return num;
		}
	}

	// Token: 0x17000434 RID: 1076
	// (get) Token: 0x06003B57 RID: 15191 RVA: 0x00149496 File Offset: 0x00147696
	public ReadOnlyCollection<ILogicEventSender> Senders
	{
		get
		{
			return this.senders.AsReadOnly();
		}
	}

	// Token: 0x17000435 RID: 1077
	// (get) Token: 0x06003B58 RID: 15192 RVA: 0x001494A3 File Offset: 0x001476A3
	public ReadOnlyCollection<ILogicEventReceiver> Receivers
	{
		get
		{
			return this.receivers.AsReadOnly();
		}
	}

	// Token: 0x040026BC RID: 9916
	private List<LogicWire>[] wireGroups = new List<LogicWire>[2];

	// Token: 0x040026BD RID: 9917
	private List<LogicUtilityNetworkLink>[] relevantBridges = new List<LogicUtilityNetworkLink>[2];

	// Token: 0x040026BE RID: 9918
	private List<ILogicEventReceiver> receivers = new List<ILogicEventReceiver>();

	// Token: 0x040026BF RID: 9919
	private List<ILogicEventSender> senders = new List<ILogicEventSender>();

	// Token: 0x040026C0 RID: 9920
	private int previousValue = -1;

	// Token: 0x040026C1 RID: 9921
	private int outputValue;

	// Token: 0x040026C2 RID: 9922
	private bool resetting;

	// Token: 0x040026C3 RID: 9923
	public static float logicSoundLastPlayedTime = 0f;

	// Token: 0x040026C4 RID: 9924
	private const float MIN_OVERLOAD_TIME_FOR_DAMAGE = 6f;

	// Token: 0x040026C5 RID: 9925
	private const float MIN_OVERLOAD_NOTIFICATION_DISPLAY_TIME = 5f;

	// Token: 0x040026C6 RID: 9926
	private GameObject targetOverloadedWire;

	// Token: 0x040026C7 RID: 9927
	private float timeOverloaded;

	// Token: 0x040026C8 RID: 9928
	private float timeOverloadNotificationDisplayed;

	// Token: 0x040026C9 RID: 9929
	private Notification overloadedNotification;

	// Token: 0x040026CA RID: 9930
	public static Dictionary<int, LogicCircuitNetwork.LogicSoundPair> logicSoundRegister = new Dictionary<int, LogicCircuitNetwork.LogicSoundPair>();

	// Token: 0x02001555 RID: 5461
	public class LogicSoundPair
	{
		// Token: 0x04006671 RID: 26225
		public int playedIndex;

		// Token: 0x04006672 RID: 26226
		public float lastPlayed;
	}
}
