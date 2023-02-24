using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020004B2 RID: 1202
[AddComponentMenu("KMonoBehaviour/scripts/Operational")]
public class Operational : KMonoBehaviour
{
	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0009179B File Offset: 0x0008F99B
	// (set) Token: 0x06001B61 RID: 7009 RVA: 0x000917A3 File Offset: 0x0008F9A3
	public bool IsFunctional { get; private set; }

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06001B62 RID: 7010 RVA: 0x000917AC File Offset: 0x0008F9AC
	// (set) Token: 0x06001B63 RID: 7011 RVA: 0x000917B4 File Offset: 0x0008F9B4
	public bool IsOperational { get; private set; }

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06001B64 RID: 7012 RVA: 0x000917BD File Offset: 0x0008F9BD
	// (set) Token: 0x06001B65 RID: 7013 RVA: 0x000917C5 File Offset: 0x0008F9C5
	public bool IsActive { get; private set; }

	// Token: 0x06001B66 RID: 7014 RVA: 0x000917CE File Offset: 0x0008F9CE
	[OnSerializing]
	private void OnSerializing()
	{
		this.AddTimeData(this.IsActive);
		this.activeStartTime = GameClock.Instance.GetTime();
		this.inactiveStartTime = GameClock.Instance.GetTime();
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x000917FC File Offset: 0x0008F9FC
	protected override void OnPrefabInit()
	{
		this.UpdateFunctional();
		this.UpdateOperational();
		base.Subscribe<Operational>(-1661515756, Operational.OnNewBuildingDelegate);
		GameClock.Instance.Subscribe(631075836, new Action<object>(this.OnNewDay));
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x00091838 File Offset: 0x0008FA38
	public void OnNewBuilding(object data)
	{
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.creationTime > 0f)
		{
			this.inactiveStartTime = component.creationTime;
			this.activeStartTime = component.creationTime;
		}
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x00091871 File Offset: 0x0008FA71
	public bool IsOperationalType(Operational.Flag.Type type)
	{
		if (type == Operational.Flag.Type.Functional)
		{
			return this.IsFunctional;
		}
		return this.IsOperational;
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x00091884 File Offset: 0x0008FA84
	public void SetFlag(Operational.Flag flag, bool value)
	{
		bool flag2 = false;
		if (this.Flags.TryGetValue(flag, out flag2))
		{
			if (flag2 != value)
			{
				this.Flags[flag] = value;
				base.Trigger(187661686, flag);
			}
		}
		else
		{
			this.Flags[flag] = value;
			base.Trigger(187661686, flag);
		}
		if (flag.FlagType == Operational.Flag.Type.Functional && value != this.IsFunctional)
		{
			this.UpdateFunctional();
		}
		if (value != this.IsOperational)
		{
			this.UpdateOperational();
		}
	}

	// Token: 0x06001B6B RID: 7019 RVA: 0x00091904 File Offset: 0x0008FB04
	public bool GetFlag(Operational.Flag flag)
	{
		bool flag2 = false;
		this.Flags.TryGetValue(flag, out flag2);
		return flag2;
	}

	// Token: 0x06001B6C RID: 7020 RVA: 0x00091924 File Offset: 0x0008FB24
	private void UpdateFunctional()
	{
		bool flag = true;
		foreach (KeyValuePair<Operational.Flag, bool> keyValuePair in this.Flags)
		{
			if (keyValuePair.Key.FlagType == Operational.Flag.Type.Functional && !keyValuePair.Value)
			{
				flag = false;
				break;
			}
		}
		this.IsFunctional = flag;
		base.Trigger(-1852328367, this.IsFunctional);
	}

	// Token: 0x06001B6D RID: 7021 RVA: 0x000919AC File Offset: 0x0008FBAC
	private void UpdateOperational()
	{
		Dictionary<Operational.Flag, bool>.Enumerator enumerator = this.Flags.GetEnumerator();
		bool flag = true;
		while (enumerator.MoveNext())
		{
			KeyValuePair<Operational.Flag, bool> keyValuePair = enumerator.Current;
			if (!keyValuePair.Value)
			{
				flag = false;
				break;
			}
		}
		if (flag != this.IsOperational)
		{
			this.IsOperational = flag;
			if (!this.IsOperational)
			{
				this.SetActive(false, false);
			}
			if (this.IsOperational)
			{
				base.GetComponent<KPrefabID>().AddTag(GameTags.Operational, false);
			}
			else
			{
				base.GetComponent<KPrefabID>().RemoveTag(GameTags.Operational);
			}
			base.Trigger(-592767678, this.IsOperational);
			Game.Instance.Trigger(-809948329, base.gameObject);
		}
	}

	// Token: 0x06001B6E RID: 7022 RVA: 0x00091A5D File Offset: 0x0008FC5D
	public void SetActive(bool value, bool force_ignore = false)
	{
		if (this.IsActive != value)
		{
			this.AddTimeData(value);
			base.Trigger(824508782, this);
			Game.Instance.Trigger(-809948329, base.gameObject);
		}
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x00091A90 File Offset: 0x0008FC90
	private void AddTimeData(bool value)
	{
		float num = (this.IsActive ? this.activeStartTime : this.inactiveStartTime);
		float time = GameClock.Instance.GetTime();
		float num2 = time - num;
		if (this.IsActive)
		{
			this.activeTime += num2;
		}
		else
		{
			this.inactiveTime += num2;
		}
		this.IsActive = value;
		if (this.IsActive)
		{
			this.activeStartTime = time;
			return;
		}
		this.inactiveStartTime = time;
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x00091B08 File Offset: 0x0008FD08
	public void OnNewDay(object data)
	{
		this.AddTimeData(this.IsActive);
		this.uptimeData.Add(this.activeTime / 600f);
		while (this.uptimeData.Count > this.MAX_DATA_POINTS)
		{
			this.uptimeData.RemoveAt(0);
		}
		this.activeTime = 0f;
		this.inactiveTime = 0f;
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x00091B70 File Offset: 0x0008FD70
	public float GetCurrentCycleUptime()
	{
		if (this.IsActive)
		{
			float num = (this.IsActive ? this.activeStartTime : this.inactiveStartTime);
			float num2 = GameClock.Instance.GetTime() - num;
			return (this.activeTime + num2) / GameClock.Instance.GetTimeSinceStartOfCycle();
		}
		return this.activeTime / GameClock.Instance.GetTimeSinceStartOfCycle();
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x00091BCE File Offset: 0x0008FDCE
	public float GetLastCycleUptime()
	{
		if (this.uptimeData.Count > 0)
		{
			return this.uptimeData[this.uptimeData.Count - 1];
		}
		return 0f;
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x00091BFC File Offset: 0x0008FDFC
	public float GetUptimeOverCycles(int num_cycles)
	{
		if (this.uptimeData.Count > 0)
		{
			int num = Mathf.Min(this.uptimeData.Count, num_cycles);
			float num2 = 0f;
			for (int i = num - 1; i >= 0; i--)
			{
				num2 += this.uptimeData[i];
			}
			return num2 / (float)num;
		}
		return 0f;
	}

	// Token: 0x06001B74 RID: 7028 RVA: 0x00091C56 File Offset: 0x0008FE56
	public bool MeetsRequirements(Operational.State stateRequirement)
	{
		switch (stateRequirement)
		{
		case Operational.State.Operational:
			return this.IsOperational;
		case Operational.State.Functional:
			return this.IsFunctional;
		case Operational.State.Active:
			return this.IsActive;
		}
		return true;
	}

	// Token: 0x06001B75 RID: 7029 RVA: 0x00091C86 File Offset: 0x0008FE86
	public static GameHashes GetEventForState(Operational.State state)
	{
		if (state == Operational.State.Operational)
		{
			return GameHashes.OperationalChanged;
		}
		if (state == Operational.State.Functional)
		{
			return GameHashes.FunctionalChanged;
		}
		return GameHashes.ActiveChanged;
	}

	// Token: 0x04000F4E RID: 3918
	[Serialize]
	public float inactiveStartTime;

	// Token: 0x04000F4F RID: 3919
	[Serialize]
	public float activeStartTime;

	// Token: 0x04000F50 RID: 3920
	[Serialize]
	private List<float> uptimeData = new List<float>();

	// Token: 0x04000F51 RID: 3921
	[Serialize]
	private float activeTime;

	// Token: 0x04000F52 RID: 3922
	[Serialize]
	private float inactiveTime;

	// Token: 0x04000F53 RID: 3923
	private int MAX_DATA_POINTS = 5;

	// Token: 0x04000F54 RID: 3924
	public Dictionary<Operational.Flag, bool> Flags = new Dictionary<Operational.Flag, bool>();

	// Token: 0x04000F55 RID: 3925
	private static readonly EventSystem.IntraObjectHandler<Operational> OnNewBuildingDelegate = new EventSystem.IntraObjectHandler<Operational>(delegate(Operational component, object data)
	{
		component.OnNewBuilding(data);
	});

	// Token: 0x020010EC RID: 4332
	public enum State
	{
		// Token: 0x0400591B RID: 22811
		Operational,
		// Token: 0x0400591C RID: 22812
		Functional,
		// Token: 0x0400591D RID: 22813
		Active,
		// Token: 0x0400591E RID: 22814
		None
	}

	// Token: 0x020010ED RID: 4333
	public class Flag
	{
		// Token: 0x060074DF RID: 29919 RVA: 0x002B4591 File Offset: 0x002B2791
		public Flag(string name, Operational.Flag.Type type)
		{
			this.Name = name;
			this.FlagType = type;
		}

		// Token: 0x060074E0 RID: 29920 RVA: 0x002B45A7 File Offset: 0x002B27A7
		public static Operational.Flag.Type GetFlagType(Operational.State operationalState)
		{
			switch (operationalState)
			{
			case Operational.State.Operational:
			case Operational.State.Active:
				return Operational.Flag.Type.Requirement;
			case Operational.State.Functional:
				return Operational.Flag.Type.Functional;
			}
			throw new InvalidOperationException("Can not convert NONE state to an Operational Flag Type");
		}

		// Token: 0x0400591F RID: 22815
		public string Name;

		// Token: 0x04005920 RID: 22816
		public Operational.Flag.Type FlagType;

		// Token: 0x02001F74 RID: 8052
		public enum Type
		{
			// Token: 0x04008BD2 RID: 35794
			Requirement,
			// Token: 0x04008BD3 RID: 35795
			Functional
		}
	}
}
