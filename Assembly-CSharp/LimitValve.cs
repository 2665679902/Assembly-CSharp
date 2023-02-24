using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005DA RID: 1498
[SerializationConfig(MemberSerialization.OptIn)]
public class LimitValve : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06002562 RID: 9570 RVA: 0x000CA020 File Offset: 0x000C8220
	public float RemainingCapacity
	{
		get
		{
			return Mathf.Max(0f, this.m_limit - this.m_amount);
		}
	}

	// Token: 0x06002563 RID: 9571 RVA: 0x000CA039 File Offset: 0x000C8239
	public NonLinearSlider.Range[] GetRanges()
	{
		if (this.sliderRanges != null && this.sliderRanges.Length != 0)
		{
			return this.sliderRanges;
		}
		return NonLinearSlider.GetDefaultRange(this.maxLimitKg);
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06002564 RID: 9572 RVA: 0x000CA05E File Offset: 0x000C825E
	// (set) Token: 0x06002565 RID: 9573 RVA: 0x000CA066 File Offset: 0x000C8266
	public float Limit
	{
		get
		{
			return this.m_limit;
		}
		set
		{
			this.m_limit = value;
			this.Refresh();
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06002566 RID: 9574 RVA: 0x000CA075 File Offset: 0x000C8275
	// (set) Token: 0x06002567 RID: 9575 RVA: 0x000CA07D File Offset: 0x000C827D
	public float Amount
	{
		get
		{
			return this.m_amount;
		}
		set
		{
			this.m_amount = value;
			base.Trigger(-1722241721, this.Amount);
			this.Refresh();
		}
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x000CA0A2 File Offset: 0x000C82A2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LimitValve>(-905833192, LimitValve.OnCopySettingsDelegate);
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x000CA0BC File Offset: 0x000C82BC
	protected override void OnSpawn()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (System.Action)Delegate.Combine(logicCircuitManager.onLogicTick, new System.Action(this.LogicTick));
		base.Subscribe<LimitValve>(-801688580, LimitValve.OnLogicValueChangedDelegate);
		if (this.conduitType == ConduitType.Gas || this.conduitType == ConduitType.Liquid)
		{
			ConduitBridge conduitBridge = this.conduitBridge;
			conduitBridge.desiredMassTransfer = (ConduitBridgeBase.DesiredMassTransfer)Delegate.Combine(conduitBridge.desiredMassTransfer, new ConduitBridgeBase.DesiredMassTransfer(this.DesiredMassTransfer));
			ConduitBridge conduitBridge2 = this.conduitBridge;
			conduitBridge2.OnMassTransfer = (ConduitBridgeBase.ConduitBridgeEvent)Delegate.Combine(conduitBridge2.OnMassTransfer, new ConduitBridgeBase.ConduitBridgeEvent(this.OnMassTransfer));
		}
		else if (this.conduitType == ConduitType.Solid)
		{
			SolidConduitBridge solidConduitBridge = this.solidConduitBridge;
			solidConduitBridge.desiredMassTransfer = (ConduitBridgeBase.DesiredMassTransfer)Delegate.Combine(solidConduitBridge.desiredMassTransfer, new ConduitBridgeBase.DesiredMassTransfer(this.DesiredMassTransfer));
			SolidConduitBridge solidConduitBridge2 = this.solidConduitBridge;
			solidConduitBridge2.OnMassTransfer = (ConduitBridgeBase.ConduitBridgeEvent)Delegate.Combine(solidConduitBridge2.OnMassTransfer, new ConduitBridgeBase.ConduitBridgeEvent(this.OnMassTransfer));
		}
		if (this.limitMeter == null)
		{
			this.limitMeter = new MeterController(this.controller, "meter_target_counter", "meter_counter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target_counter" });
		}
		this.Refresh();
		base.OnSpawn();
	}

	// Token: 0x0600256A RID: 9578 RVA: 0x000CA1FE File Offset: 0x000C83FE
	protected override void OnCleanUp()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (System.Action)Delegate.Remove(logicCircuitManager.onLogicTick, new System.Action(this.LogicTick));
		base.OnCleanUp();
	}

	// Token: 0x0600256B RID: 9579 RVA: 0x000CA231 File Offset: 0x000C8431
	private void LogicTick()
	{
		if (this.m_resetRequested)
		{
			this.ResetAmount();
		}
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x000CA241 File Offset: 0x000C8441
	public void ResetAmount()
	{
		this.m_resetRequested = false;
		this.Amount = 0f;
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x000CA258 File Offset: 0x000C8458
	private float DesiredMassTransfer(float dt, SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, Pickupable pickupable)
	{
		if (!this.operational.IsOperational)
		{
			return 0f;
		}
		if (this.conduitType == ConduitType.Solid && pickupable != null && GameTags.DisplayAsUnits.Contains(pickupable.PrefabID()))
		{
			float num = pickupable.PrimaryElement.Units;
			if (this.RemainingCapacity < num)
			{
				num = (float)Mathf.FloorToInt(this.RemainingCapacity);
			}
			return num * pickupable.PrimaryElement.MassPerUnit;
		}
		return Mathf.Min(mass, this.RemainingCapacity);
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x000CA2DC File Offset: 0x000C84DC
	private void OnMassTransfer(SimHashes element, float transferredMass, float temperature, byte disease_idx, int disease_count, Pickupable pickupable)
	{
		if (!LogicCircuitNetwork.IsBitActive(0, this.ports.GetInputValue(LimitValve.RESET_PORT_ID)))
		{
			if (this.conduitType == ConduitType.Gas || this.conduitType == ConduitType.Liquid)
			{
				this.Amount += transferredMass;
			}
			else if (this.conduitType == ConduitType.Solid && pickupable != null)
			{
				this.Amount += transferredMass / pickupable.PrimaryElement.MassPerUnit;
			}
		}
		this.operational.SetActive(this.operational.IsOperational && transferredMass > 0f, false);
		this.Refresh();
	}

	// Token: 0x0600256F RID: 9583 RVA: 0x000CA37C File Offset: 0x000C857C
	private void Refresh()
	{
		if (this.operational == null)
		{
			return;
		}
		this.ports.SendSignal(LimitValve.OUTPUT_PORT_ID, (this.RemainingCapacity <= 0f) ? 1 : 0);
		this.operational.SetFlag(LimitValve.limitNotReached, this.RemainingCapacity > 0f);
		if (this.RemainingCapacity > 0f)
		{
			this.limitMeter.meterController.Play("meter_counter", KAnim.PlayMode.Paused, 1f, 0f);
			this.limitMeter.SetPositionPercent(this.Amount / this.Limit);
			this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.LimitValveLimitNotReached, this);
			return;
		}
		this.limitMeter.meterController.Play("meter_on", KAnim.PlayMode.Paused, 1f, 0f);
		this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.LimitValveLimitReached, this);
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x000CA49C File Offset: 0x000C869C
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == LimitValve.RESET_PORT_ID && LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue))
		{
			this.ResetAmount();
		}
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x000CA4D8 File Offset: 0x000C86D8
	private void OnCopySettings(object data)
	{
		LimitValve component = ((GameObject)data).GetComponent<LimitValve>();
		if (component != null)
		{
			this.Limit = component.Limit;
		}
	}

	// Token: 0x040015BD RID: 5565
	public static readonly HashedString RESET_PORT_ID = new HashedString("LimitValveReset");

	// Token: 0x040015BE RID: 5566
	public static readonly HashedString OUTPUT_PORT_ID = new HashedString("LimitValveOutput");

	// Token: 0x040015BF RID: 5567
	public static readonly Operational.Flag limitNotReached = new Operational.Flag("limitNotReached", Operational.Flag.Type.Requirement);

	// Token: 0x040015C0 RID: 5568
	public ConduitType conduitType;

	// Token: 0x040015C1 RID: 5569
	public float maxLimitKg = 100f;

	// Token: 0x040015C2 RID: 5570
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040015C3 RID: 5571
	[MyCmpReq]
	private LogicPorts ports;

	// Token: 0x040015C4 RID: 5572
	[MyCmpGet]
	private KBatchedAnimController controller;

	// Token: 0x040015C5 RID: 5573
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040015C6 RID: 5574
	[MyCmpGet]
	private ConduitBridge conduitBridge;

	// Token: 0x040015C7 RID: 5575
	[MyCmpGet]
	private SolidConduitBridge solidConduitBridge;

	// Token: 0x040015C8 RID: 5576
	[Serialize]
	[SerializeField]
	private float m_limit;

	// Token: 0x040015C9 RID: 5577
	[Serialize]
	private float m_amount;

	// Token: 0x040015CA RID: 5578
	[Serialize]
	private bool m_resetRequested;

	// Token: 0x040015CB RID: 5579
	private MeterController limitMeter;

	// Token: 0x040015CC RID: 5580
	public bool displayUnitsInsteadOfMass;

	// Token: 0x040015CD RID: 5581
	public NonLinearSlider.Range[] sliderRanges;

	// Token: 0x040015CE RID: 5582
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040015CF RID: 5583
	private static readonly EventSystem.IntraObjectHandler<LimitValve> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LimitValve>(delegate(LimitValve component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x040015D0 RID: 5584
	private static readonly EventSystem.IntraObjectHandler<LimitValve> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LimitValve>(delegate(LimitValve component, object data)
	{
		component.OnCopySettings(data);
	});
}
