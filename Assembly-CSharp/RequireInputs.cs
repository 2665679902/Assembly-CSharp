using System;
using UnityEngine;

// Token: 0x020008D5 RID: 2261
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/RequireInputs")]
public class RequireInputs : KMonoBehaviour, ISim200ms
{
	// Token: 0x1700048B RID: 1163
	// (get) Token: 0x06004106 RID: 16646 RVA: 0x0016C493 File Offset: 0x0016A693
	public bool RequiresPower
	{
		get
		{
			return this.requirePower;
		}
	}

	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x06004107 RID: 16647 RVA: 0x0016C49B File Offset: 0x0016A69B
	public bool RequiresInputConduit
	{
		get
		{
			return this.requireConduit;
		}
	}

	// Token: 0x06004108 RID: 16648 RVA: 0x0016C4A3 File Offset: 0x0016A6A3
	public void SetRequirements(bool power, bool conduit)
	{
		this.requirePower = power;
		this.requireConduit = conduit;
	}

	// Token: 0x1700048D RID: 1165
	// (get) Token: 0x06004109 RID: 16649 RVA: 0x0016C4B3 File Offset: 0x0016A6B3
	public bool RequirementsMet
	{
		get
		{
			return this.requirementsMet;
		}
	}

	// Token: 0x0600410A RID: 16650 RVA: 0x0016C4BB File Offset: 0x0016A6BB
	protected override void OnPrefabInit()
	{
		this.Bind();
	}

	// Token: 0x0600410B RID: 16651 RVA: 0x0016C4C3 File Offset: 0x0016A6C3
	protected override void OnSpawn()
	{
		this.CheckRequirements(true);
		this.Bind();
	}

	// Token: 0x0600410C RID: 16652 RVA: 0x0016C4D4 File Offset: 0x0016A6D4
	[ContextMenu("Bind")]
	private void Bind()
	{
		if (this.requirePower)
		{
			this.energy = base.GetComponent<IEnergyConsumer>();
			this.button = base.GetComponent<BuildingEnabledButton>();
		}
		if (this.requireConduit && !this.conduitConsumer)
		{
			this.conduitConsumer = base.GetComponent<ConduitConsumer>();
		}
	}

	// Token: 0x0600410D RID: 16653 RVA: 0x0016C522 File Offset: 0x0016A722
	public void Sim200ms(float dt)
	{
		this.CheckRequirements(false);
	}

	// Token: 0x0600410E RID: 16654 RVA: 0x0016C52C File Offset: 0x0016A72C
	private void CheckRequirements(bool forceEvent)
	{
		bool flag = true;
		bool flag2 = false;
		if (this.requirePower)
		{
			bool isConnected = this.energy.IsConnected;
			bool isPowered = this.energy.IsPowered;
			flag = flag && isPowered && isConnected;
			bool flag3 = this.VisualizeRequirement(RequireInputs.Requirements.NeedPower) && isConnected && !isPowered && (this.button == null || this.button.IsEnabled);
			bool flag4 = this.VisualizeRequirement(RequireInputs.Requirements.NoWire) && !isConnected;
			this.needPowerStatusGuid = this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NeedPower, this.needPowerStatusGuid, flag3, this);
			this.noWireStatusGuid = this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.NoWireConnected, this.noWireStatusGuid, flag4, this);
			flag2 = flag != this.RequirementsMet && base.GetComponent<Light2D>() != null;
		}
		if (this.requireConduit)
		{
			bool flag5 = !this.conduitConsumer.enabled || this.conduitConsumer.IsConnected;
			bool flag6 = !this.conduitConsumer.enabled || this.conduitConsumer.IsSatisfied;
			if (this.VisualizeRequirement(RequireInputs.Requirements.ConduitConnected) && this.previouslyConnected != flag5)
			{
				this.previouslyConnected = flag5;
				StatusItem statusItem = null;
				ConduitType conduitType = this.conduitConsumer.TypeOfConduit;
				if (conduitType != ConduitType.Gas)
				{
					if (conduitType == ConduitType.Liquid)
					{
						statusItem = Db.Get().BuildingStatusItems.NeedLiquidIn;
					}
				}
				else
				{
					statusItem = Db.Get().BuildingStatusItems.NeedGasIn;
				}
				if (statusItem != null)
				{
					this.selectable.ToggleStatusItem(statusItem, !flag5, new global::Tuple<ConduitType, Tag>(this.conduitConsumer.TypeOfConduit, this.conduitConsumer.capacityTag));
				}
				this.operational.SetFlag(RequireInputs.inputConnectedFlag, flag5);
			}
			flag = flag && flag5;
			if (this.VisualizeRequirement(RequireInputs.Requirements.ConduitEmpty) && this.previouslySatisfied != flag6)
			{
				this.previouslySatisfied = flag6;
				StatusItem statusItem2 = null;
				ConduitType conduitType = this.conduitConsumer.TypeOfConduit;
				if (conduitType != ConduitType.Gas)
				{
					if (conduitType == ConduitType.Liquid)
					{
						statusItem2 = Db.Get().BuildingStatusItems.LiquidPipeEmpty;
					}
				}
				else
				{
					statusItem2 = Db.Get().BuildingStatusItems.GasPipeEmpty;
				}
				if (this.requireConduitHasMass)
				{
					if (statusItem2 != null)
					{
						this.selectable.ToggleStatusItem(statusItem2, !flag6, this);
					}
					this.operational.SetFlag(RequireInputs.pipesHaveMass, flag6);
				}
			}
		}
		this.requirementsMet = flag;
		if (flag2)
		{
			Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
			if (roomOfGameObject != null)
			{
				Game.Instance.roomProber.UpdateRoom(roomOfGameObject.cavity);
			}
		}
	}

	// Token: 0x0600410F RID: 16655 RVA: 0x0016C7C8 File Offset: 0x0016A9C8
	public bool VisualizeRequirement(RequireInputs.Requirements r)
	{
		return (this.visualizeRequirements & r) == r;
	}

	// Token: 0x04002B62 RID: 11106
	[SerializeField]
	private bool requirePower = true;

	// Token: 0x04002B63 RID: 11107
	[SerializeField]
	private bool requireConduit;

	// Token: 0x04002B64 RID: 11108
	public bool requireConduitHasMass = true;

	// Token: 0x04002B65 RID: 11109
	public RequireInputs.Requirements visualizeRequirements = RequireInputs.Requirements.All;

	// Token: 0x04002B66 RID: 11110
	private static readonly Operational.Flag inputConnectedFlag = new Operational.Flag("inputConnected", Operational.Flag.Type.Requirement);

	// Token: 0x04002B67 RID: 11111
	private static readonly Operational.Flag pipesHaveMass = new Operational.Flag("pipesHaveMass", Operational.Flag.Type.Requirement);

	// Token: 0x04002B68 RID: 11112
	private Guid noWireStatusGuid;

	// Token: 0x04002B69 RID: 11113
	private Guid needPowerStatusGuid;

	// Token: 0x04002B6A RID: 11114
	private bool requirementsMet;

	// Token: 0x04002B6B RID: 11115
	private BuildingEnabledButton button;

	// Token: 0x04002B6C RID: 11116
	private IEnergyConsumer energy;

	// Token: 0x04002B6D RID: 11117
	public ConduitConsumer conduitConsumer;

	// Token: 0x04002B6E RID: 11118
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04002B6F RID: 11119
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002B70 RID: 11120
	private bool previouslyConnected = true;

	// Token: 0x04002B71 RID: 11121
	private bool previouslySatisfied = true;

	// Token: 0x02001697 RID: 5783
	[Flags]
	public enum Requirements
	{
		// Token: 0x04006A46 RID: 27206
		None = 0,
		// Token: 0x04006A47 RID: 27207
		NoWire = 1,
		// Token: 0x04006A48 RID: 27208
		NeedPower = 2,
		// Token: 0x04006A49 RID: 27209
		ConduitConnected = 4,
		// Token: 0x04006A4A RID: 27210
		ConduitEmpty = 8,
		// Token: 0x04006A4B RID: 27211
		AllPower = 3,
		// Token: 0x04006A4C RID: 27212
		AllConduit = 12,
		// Token: 0x04006A4D RID: 27213
		All = 15
	}
}
