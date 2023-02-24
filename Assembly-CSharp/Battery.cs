using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000579 RID: 1401
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name}")]
[AddComponentMenu("KMonoBehaviour/scripts/Battery")]
public class Battery : KMonoBehaviour, IEnergyConsumer, ICircuitConnected, IGameObjectEffectDescriptor, IEnergyProducer
{
	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x060021EC RID: 8684 RVA: 0x000B8AA9 File Offset: 0x000B6CA9
	// (set) Token: 0x060021ED RID: 8685 RVA: 0x000B8AB1 File Offset: 0x000B6CB1
	public float WattsUsed { get; private set; }

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x060021EE RID: 8686 RVA: 0x000B8ABA File Offset: 0x000B6CBA
	public float WattsNeededWhenActive
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x060021EF RID: 8687 RVA: 0x000B8AC1 File Offset: 0x000B6CC1
	public float PercentFull
	{
		get
		{
			return this.joulesAvailable / this.capacity;
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000B8AD0 File Offset: 0x000B6CD0
	public float PreviousPercentFull
	{
		get
		{
			return this.PreviousJoulesAvailable / this.capacity;
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x060021F1 RID: 8689 RVA: 0x000B8ADF File Offset: 0x000B6CDF
	public float JoulesAvailable
	{
		get
		{
			return this.joulesAvailable;
		}
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x060021F2 RID: 8690 RVA: 0x000B8AE7 File Offset: 0x000B6CE7
	public float Capacity
	{
		get
		{
			return this.capacity;
		}
	}

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x060021F3 RID: 8691 RVA: 0x000B8AEF File Offset: 0x000B6CEF
	// (set) Token: 0x060021F4 RID: 8692 RVA: 0x000B8AF7 File Offset: 0x000B6CF7
	public float ChargeCapacity { get; private set; }

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x060021F5 RID: 8693 RVA: 0x000B8B00 File Offset: 0x000B6D00
	public int PowerSortOrder
	{
		get
		{
			return this.powerSortOrder;
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000B8B08 File Offset: 0x000B6D08
	public string Name
	{
		get
		{
			return base.GetComponent<KSelectable>().GetName();
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x060021F7 RID: 8695 RVA: 0x000B8B15 File Offset: 0x000B6D15
	// (set) Token: 0x060021F8 RID: 8696 RVA: 0x000B8B1D File Offset: 0x000B6D1D
	public int PowerCell { get; private set; }

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x060021F9 RID: 8697 RVA: 0x000B8B26 File Offset: 0x000B6D26
	public ushort CircuitID
	{
		get
		{
			return Game.Instance.circuitManager.GetCircuitID(this);
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x060021FA RID: 8698 RVA: 0x000B8B38 File Offset: 0x000B6D38
	public bool IsConnected
	{
		get
		{
			return this.connectionStatus > CircuitManager.ConnectionStatus.NotConnected;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x060021FB RID: 8699 RVA: 0x000B8B43 File Offset: 0x000B6D43
	public bool IsPowered
	{
		get
		{
			return this.connectionStatus == CircuitManager.ConnectionStatus.Powered;
		}
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x060021FC RID: 8700 RVA: 0x000B8B4E File Offset: 0x000B6D4E
	// (set) Token: 0x060021FD RID: 8701 RVA: 0x000B8B56 File Offset: 0x000B6D56
	public bool IsVirtual { get; protected set; }

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x060021FE RID: 8702 RVA: 0x000B8B5F File Offset: 0x000B6D5F
	// (set) Token: 0x060021FF RID: 8703 RVA: 0x000B8B67 File Offset: 0x000B6D67
	public object VirtualCircuitKey { get; protected set; }

	// Token: 0x06002200 RID: 8704 RVA: 0x000B8B70 File Offset: 0x000B6D70
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Batteries.Add(this);
		Building component = base.GetComponent<Building>();
		this.PowerCell = component.GetPowerInputCell();
		base.Subscribe<Battery>(-1582839653, Battery.OnTagsChangedDelegate);
		this.OnTagsChanged(null);
		this.meter = (base.GetComponent<PowerTransformer>() ? null : new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" }));
		Game.Instance.circuitManager.Connect(this);
		Game.Instance.energySim.AddBattery(this);
	}

	// Token: 0x06002201 RID: 8705 RVA: 0x000B8C30 File Offset: 0x000B6E30
	private void OnTagsChanged(object data)
	{
		if (this.HasAllTags(this.connectedTags))
		{
			Game.Instance.circuitManager.Connect(this);
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.JoulesAvailable, this);
			return;
		}
		Game.Instance.circuitManager.Disconnect(this, false);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.JoulesAvailable, false);
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x000B8CB4 File Offset: 0x000B6EB4
	protected override void OnCleanUp()
	{
		Game.Instance.energySim.RemoveBattery(this);
		Game.Instance.circuitManager.Disconnect(this, true);
		Components.Batteries.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x000B8CE8 File Offset: 0x000B6EE8
	public virtual void EnergySim200ms(float dt)
	{
		this.dt = dt;
		this.joulesConsumed = 0f;
		this.WattsUsed = 0f;
		this.ChargeCapacity = this.chargeWattage * dt;
		if (this.meter != null)
		{
			float percentFull = this.PercentFull;
			this.meter.SetPositionPercent(percentFull);
		}
		this.UpdateSounds();
		this.PreviousJoulesAvailable = this.JoulesAvailable;
		this.ConsumeEnergy(this.joulesLostPerSecond * dt, true);
	}

	// Token: 0x06002204 RID: 8708 RVA: 0x000B8D5C File Offset: 0x000B6F5C
	private void UpdateSounds()
	{
		float previousPercentFull = this.PreviousPercentFull;
		float percentFull = this.PercentFull;
		if (percentFull == 0f && previousPercentFull != 0f)
		{
			base.GetComponent<LoopingSounds>().PlayEvent(GameSoundEvents.BatteryDischarged);
		}
		if (percentFull > 0.999f && previousPercentFull <= 0.999f)
		{
			base.GetComponent<LoopingSounds>().PlayEvent(GameSoundEvents.BatteryFull);
		}
		if (percentFull < 0.25f && previousPercentFull >= 0.25f)
		{
			base.GetComponent<LoopingSounds>().PlayEvent(GameSoundEvents.BatteryWarning);
		}
	}

	// Token: 0x06002205 RID: 8709 RVA: 0x000B8DD8 File Offset: 0x000B6FD8
	public void SetConnectionStatus(CircuitManager.ConnectionStatus status)
	{
		this.connectionStatus = status;
		if (status == CircuitManager.ConnectionStatus.NotConnected)
		{
			this.operational.SetActive(false, false);
			return;
		}
		this.operational.SetActive(this.operational.IsOperational && this.JoulesAvailable > 0f, false);
	}

	// Token: 0x06002206 RID: 8710 RVA: 0x000B8E28 File Offset: 0x000B7028
	public void AddEnergy(float joules)
	{
		this.joulesAvailable = Mathf.Min(this.capacity, this.JoulesAvailable + joules);
		this.joulesConsumed += joules;
		this.ChargeCapacity -= joules;
		this.WattsUsed = this.joulesConsumed / this.dt;
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x000B8E80 File Offset: 0x000B7080
	public void ConsumeEnergy(float joules, bool report = false)
	{
		if (report)
		{
			float num = Mathf.Min(this.JoulesAvailable, joules);
			ReportManager.Instance.ReportValue(ReportManager.ReportType.EnergyWasted, -num, StringFormatter.Replace(BUILDINGS.PREFABS.BATTERY.CHARGE_LOSS, "{Battery}", this.GetProperName()), null);
		}
		this.joulesAvailable = Mathf.Max(0f, this.JoulesAvailable - joules);
	}

	// Token: 0x06002208 RID: 8712 RVA: 0x000B8EDE File Offset: 0x000B70DE
	public void ConsumeEnergy(float joules)
	{
		this.ConsumeEnergy(joules, false);
	}

	// Token: 0x06002209 RID: 8713 RVA: 0x000B8EE8 File Offset: 0x000B70E8
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.powerTransformer == null)
		{
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.REQUIRESPOWERGENERATOR, UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESPOWERGENERATOR, Descriptor.DescriptorType.Requirement, false));
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.BATTERYCAPACITY, GameUtil.GetFormattedJoules(this.capacity, "", GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.BATTERYCAPACITY, GameUtil.GetFormattedJoules(this.capacity, "", GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect, false));
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.BATTERYLEAK, GameUtil.GetFormattedJoules(this.joulesLostPerSecond, "F1", GameUtil.TimeSlice.PerCycle)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.BATTERYLEAK, GameUtil.GetFormattedJoules(this.joulesLostPerSecond, "F1", GameUtil.TimeSlice.PerCycle)), Descriptor.DescriptorType.Effect, false));
		}
		else
		{
			list.Add(new Descriptor(UI.BUILDINGEFFECTS.TRANSFORMER_INPUT_WIRE, UI.BUILDINGEFFECTS.TOOLTIPS.TRANSFORMER_INPUT_WIRE, Descriptor.DescriptorType.Requirement, false));
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.TRANSFORMER_OUTPUT_WIRE, GameUtil.GetFormattedWattage(this.capacity, GameUtil.WattageFormatterUnit.Automatic, true)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.TRANSFORMER_OUTPUT_WIRE, GameUtil.GetFormattedWattage(this.capacity, GameUtil.WattageFormatterUnit.Automatic, true)), Descriptor.DescriptorType.Requirement, false));
		}
		return list;
	}

	// Token: 0x0600220A RID: 8714 RVA: 0x000B9030 File Offset: 0x000B7230
	[ContextMenu("Refill Power")]
	public void DEBUG_RefillPower()
	{
		this.joulesAvailable = this.capacity;
	}

	// Token: 0x04001399 RID: 5017
	[SerializeField]
	public float capacity;

	// Token: 0x0400139A RID: 5018
	[SerializeField]
	public float chargeWattage = float.PositiveInfinity;

	// Token: 0x0400139B RID: 5019
	[Serialize]
	private float joulesAvailable;

	// Token: 0x0400139C RID: 5020
	[MyCmpGet]
	protected Operational operational;

	// Token: 0x0400139D RID: 5021
	[MyCmpGet]
	public PowerTransformer powerTransformer;

	// Token: 0x0400139E RID: 5022
	protected MeterController meter;

	// Token: 0x040013A0 RID: 5024
	public float joulesLostPerSecond;

	// Token: 0x040013A2 RID: 5026
	[SerializeField]
	public int powerSortOrder;

	// Token: 0x040013A6 RID: 5030
	private float PreviousJoulesAvailable;

	// Token: 0x040013A7 RID: 5031
	private CircuitManager.ConnectionStatus connectionStatus;

	// Token: 0x040013A8 RID: 5032
	public static readonly Tag[] DEFAULT_CONNECTED_TAGS = new Tag[] { GameTags.Operational };

	// Token: 0x040013A9 RID: 5033
	[SerializeField]
	public Tag[] connectedTags = Battery.DEFAULT_CONNECTED_TAGS;

	// Token: 0x040013AA RID: 5034
	private static readonly EventSystem.IntraObjectHandler<Battery> OnTagsChangedDelegate = new EventSystem.IntraObjectHandler<Battery>(delegate(Battery component, object data)
	{
		component.OnTagsChanged(data);
	});

	// Token: 0x040013AB RID: 5035
	private float dt;

	// Token: 0x040013AC RID: 5036
	private float joulesConsumed;
}
