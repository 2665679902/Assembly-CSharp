using System;
using STRINGS;
using UnityEngine;

// Token: 0x020007A4 RID: 1956
public class HEPFuelTank : KMonoBehaviour, IFuelTank, IUserControlledCapacity
{
	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x06003724 RID: 14116 RVA: 0x001331B8 File Offset: 0x001313B8
	public IStorage Storage
	{
		get
		{
			return this.hepStorage;
		}
	}

	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x06003725 RID: 14117 RVA: 0x001331C0 File Offset: 0x001313C0
	public bool ConsumeFuelOnLand
	{
		get
		{
			return this.consumeFuelOnLand;
		}
	}

	// Token: 0x06003726 RID: 14118 RVA: 0x001331C8 File Offset: 0x001313C8
	public void DEBUG_FillTank()
	{
		this.hepStorage.Store(this.hepStorage.RemainingCapacity());
	}

	// Token: 0x17000402 RID: 1026
	// (get) Token: 0x06003727 RID: 14119 RVA: 0x001331E1 File Offset: 0x001313E1
	// (set) Token: 0x06003728 RID: 14120 RVA: 0x001331EE File Offset: 0x001313EE
	public float UserMaxCapacity
	{
		get
		{
			return this.hepStorage.capacity;
		}
		set
		{
			this.hepStorage.capacity = value;
			base.Trigger(-795826715, this);
		}
	}

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x06003729 RID: 14121 RVA: 0x00133208 File Offset: 0x00131408
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x0600372A RID: 14122 RVA: 0x0013320F File Offset: 0x0013140F
	public float MaxCapacity
	{
		get
		{
			return this.physicalFuelCapacity;
		}
	}

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x0600372B RID: 14123 RVA: 0x00133217 File Offset: 0x00131417
	public float AmountStored
	{
		get
		{
			return this.hepStorage.Particles;
		}
	}

	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x0600372C RID: 14124 RVA: 0x00133224 File Offset: 0x00131424
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x0600372D RID: 14125 RVA: 0x00133227 File Offset: 0x00131427
	public LocString CapacityUnits
	{
		get
		{
			return UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;
		}
	}

	// Token: 0x0600372E RID: 14126 RVA: 0x00133230 File Offset: 0x00131430
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketStorage, new ConditionProperlyFueled(this));
		this.m_meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		this.m_meter.gameObject.GetComponent<KBatchedAnimTracker>().matchParentOffset = true;
		this.OnStorageChange(null);
		base.Subscribe<HEPFuelTank>(-795826715, HEPFuelTank.OnStorageChangedDelegate);
		base.Subscribe<HEPFuelTank>(-1837862626, HEPFuelTank.OnStorageChangedDelegate);
	}

	// Token: 0x0600372F RID: 14127 RVA: 0x001332D9 File Offset: 0x001314D9
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<HEPFuelTank>(-905833192, HEPFuelTank.OnCopySettingsDelegate);
	}

	// Token: 0x06003730 RID: 14128 RVA: 0x001332F2 File Offset: 0x001314F2
	private void OnStorageChange(object data)
	{
		this.m_meter.SetPositionPercent(this.hepStorage.Particles / Mathf.Max(1f, this.hepStorage.capacity));
	}

	// Token: 0x06003731 RID: 14129 RVA: 0x00133320 File Offset: 0x00131520
	private void OnCopySettings(object data)
	{
		HEPFuelTank component = ((GameObject)data).GetComponent<HEPFuelTank>();
		if (component != null)
		{
			this.UserMaxCapacity = component.UserMaxCapacity;
		}
	}

	// Token: 0x040024FE RID: 9470
	[MyCmpReq]
	public HighEnergyParticleStorage hepStorage;

	// Token: 0x040024FF RID: 9471
	public float physicalFuelCapacity;

	// Token: 0x04002500 RID: 9472
	private MeterController m_meter;

	// Token: 0x04002501 RID: 9473
	public bool consumeFuelOnLand;

	// Token: 0x04002502 RID: 9474
	private static readonly EventSystem.IntraObjectHandler<HEPFuelTank> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<HEPFuelTank>(delegate(HEPFuelTank component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04002503 RID: 9475
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04002504 RID: 9476
	private static readonly EventSystem.IntraObjectHandler<HEPFuelTank> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<HEPFuelTank>(delegate(HEPFuelTank component, object data)
	{
		component.OnCopySettings(data);
	});
}
