using System;
using FMOD.Studio;
using KSerialization;
using UnityEngine;

// Token: 0x020005EF RID: 1519
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicHammer : Switch
{
	// Token: 0x060026CB RID: 9931 RVA: 0x000D1204 File Offset: 0x000CF404
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.animController = base.GetComponent<KBatchedAnimController>();
		this.switchedOn = false;
		this.UpdateVisualState(false, false);
		this.rotatable = base.GetComponent<Rotatable>();
		CellOffset rotatedCellOffset = this.rotatable.GetRotatedCellOffset(this.target_offset);
		int num = Grid.PosToCell(base.transform.GetPosition());
		this.resonator_cell = Grid.OffsetCell(num, rotatedCellOffset);
		base.Subscribe<LogicHammer>(-801688580, LogicHammer.OnLogicValueChangedDelegate);
		base.Subscribe<LogicHammer>(-592767678, LogicHammer.OnOperationalChangedDelegate);
		base.OnToggle += this.OnSwitchToggled;
	}

	// Token: 0x060026CC RID: 9932 RVA: 0x000D12A4 File Offset: 0x000CF4A4
	private void OnSwitchToggled(bool toggled_on)
	{
		bool flag = false;
		if (this.operational.IsOperational && toggled_on)
		{
			flag = this.TriggerAudio();
			this.operational.SetActive(true, false);
		}
		else
		{
			this.operational.SetActive(false, false);
		}
		this.UpdateVisualState(flag, false);
	}

	// Token: 0x060026CD RID: 9933 RVA: 0x000D12ED File Offset: 0x000CF4ED
	private void OnOperationalChanged(object data)
	{
		if (this.operational.IsOperational)
		{
			this.SetState(LogicCircuitNetwork.IsBitActive(0, this.logic_value));
			return;
		}
		this.UpdateVisualState(false, false);
	}

	// Token: 0x060026CE RID: 9934 RVA: 0x000D1318 File Offset: 0x000CF518
	private bool TriggerAudio()
	{
		if (this.wasOn || !this.switchedOn)
		{
			return false;
		}
		string text = null;
		if (!Grid.IsValidCell(this.resonator_cell))
		{
			return false;
		}
		float num = float.NaN;
		GameObject gameObject = Grid.Objects[this.resonator_cell, 1];
		if (gameObject == null)
		{
			gameObject = Grid.Objects[this.resonator_cell, 30];
			if (gameObject == null)
			{
				gameObject = Grid.Objects[this.resonator_cell, 26];
				if (gameObject != null)
				{
					Wire component = gameObject.GetComponent<Wire>();
					if (component != null)
					{
						ElectricalUtilityNetwork electricalUtilityNetwork = (ElectricalUtilityNetwork)Game.Instance.electricalConduitSystem.GetNetworkForCell(component.GetNetworkCell());
						if (electricalUtilityNetwork != null)
						{
							num = (float)electricalUtilityNetwork.allWires.Count;
						}
					}
				}
				else
				{
					gameObject = Grid.Objects[this.resonator_cell, 31];
					if (gameObject != null)
					{
						if (gameObject.GetComponent<LogicWire>() != null)
						{
							LogicCircuitNetwork networkForCell = Game.Instance.logicCircuitManager.GetNetworkForCell(this.resonator_cell);
							if (networkForCell != null)
							{
								num = (float)networkForCell.WireCount;
							}
						}
					}
					else
					{
						gameObject = Grid.Objects[this.resonator_cell, 12];
						if (gameObject != null)
						{
							Conduit component2 = gameObject.GetComponent<Conduit>();
							FlowUtilityNetwork flowUtilityNetwork = (FlowUtilityNetwork)Conduit.GetNetworkManager(ConduitType.Gas).GetNetworkForCell(component2.GetNetworkCell());
							if (flowUtilityNetwork != null)
							{
								num = (float)flowUtilityNetwork.conduitCount;
							}
						}
						else
						{
							gameObject = Grid.Objects[this.resonator_cell, 16];
							if (gameObject != null)
							{
								Conduit component3 = gameObject.GetComponent<Conduit>();
								FlowUtilityNetwork flowUtilityNetwork2 = (FlowUtilityNetwork)Conduit.GetNetworkManager(ConduitType.Liquid).GetNetworkForCell(component3.GetNetworkCell());
								if (flowUtilityNetwork2 != null)
								{
									num = (float)flowUtilityNetwork2.conduitCount;
								}
							}
							else
							{
								gameObject = Grid.Objects[this.resonator_cell, 20];
								gameObject != null;
							}
						}
					}
				}
			}
		}
		if (gameObject != null)
		{
			Building component4 = gameObject.GetComponent<BuildingComplete>();
			if (component4 != null)
			{
				text = component4.Def.PrefabID;
			}
		}
		if (text != null)
		{
			string text2 = StringFormatter.Combine(LogicHammer.SOUND_EVENT_PREFIX, text);
			text2 = GlobalAssets.GetSound(text2, true);
			if (text2 == null)
			{
				text2 = GlobalAssets.GetSound(LogicHammer.DEFAULT_NO_SOUND_EVENT, false);
			}
			Vector3 position = base.transform.position;
			position.z = 0f;
			EventInstance eventInstance = KFMOD.BeginOneShot(text2, position, 1f);
			if (!float.IsNaN(num))
			{
				eventInstance.setParameterByName(LogicHammer.PARAMETER_NAME, num, false);
			}
			KFMOD.EndOneShot(eventInstance);
			return true;
		}
		return false;
	}

	// Token: 0x060026CF RID: 9935 RVA: 0x000D15A0 File Offset: 0x000CF7A0
	private void UpdateVisualState(bool connected, bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			if (this.switchedOn)
			{
				if (connected)
				{
					this.animController.Play(LogicHammer.ON_HIT_ANIMS, KAnim.PlayMode.Once);
					return;
				}
				this.animController.Play(LogicHammer.ON_MISS_ANIMS, KAnim.PlayMode.Once);
				return;
			}
			else
			{
				this.animController.Play(LogicHammer.OFF_ANIMS, KAnim.PlayMode.Once);
			}
		}
	}

	// Token: 0x060026D0 RID: 9936 RVA: 0x000D1610 File Offset: 0x000CF810
	private void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == LogicHammer.PORT_ID)
		{
			this.SetState(LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue));
			this.logic_value = logicValueChanged.newValue;
		}
	}

	// Token: 0x040016E2 RID: 5858
	protected KBatchedAnimController animController;

	// Token: 0x040016E3 RID: 5859
	private static readonly EventSystem.IntraObjectHandler<LogicHammer> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicHammer>(delegate(LogicHammer component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x040016E4 RID: 5860
	private static readonly EventSystem.IntraObjectHandler<LogicHammer> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<LogicHammer>(delegate(LogicHammer component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x040016E5 RID: 5861
	public static readonly HashedString PORT_ID = new HashedString("LogicHammerInput");

	// Token: 0x040016E6 RID: 5862
	private static string PARAMETER_NAME = "hammerObjectCount";

	// Token: 0x040016E7 RID: 5863
	private static string SOUND_EVENT_PREFIX = "Hammer_strike_";

	// Token: 0x040016E8 RID: 5864
	private static string DEFAULT_NO_SOUND_EVENT = "Hammer_strike_default";

	// Token: 0x040016E9 RID: 5865
	[MyCmpGet]
	private Operational operational;

	// Token: 0x040016EA RID: 5866
	private int resonator_cell;

	// Token: 0x040016EB RID: 5867
	private CellOffset target_offset = new CellOffset(-1, 0);

	// Token: 0x040016EC RID: 5868
	private Rotatable rotatable;

	// Token: 0x040016ED RID: 5869
	private int logic_value;

	// Token: 0x040016EE RID: 5870
	private bool wasOn;

	// Token: 0x040016EF RID: 5871
	protected static readonly HashedString[] ON_HIT_ANIMS = new HashedString[] { "on_hit" };

	// Token: 0x040016F0 RID: 5872
	protected static readonly HashedString[] ON_MISS_ANIMS = new HashedString[] { "on_miss" };

	// Token: 0x040016F1 RID: 5873
	protected static readonly HashedString[] OFF_ANIMS = new HashedString[] { "off_pre", "off" };
}
