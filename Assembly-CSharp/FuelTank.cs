using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000945 RID: 2373
public class FuelTank : KMonoBehaviour, IUserControlledCapacity, IFuelTank
{
	// Token: 0x1700051A RID: 1306
	// (get) Token: 0x06004613 RID: 17939 RVA: 0x0018AEB5 File Offset: 0x001890B5
	public IStorage Storage
	{
		get
		{
			return this.storage;
		}
	}

	// Token: 0x1700051B RID: 1307
	// (get) Token: 0x06004614 RID: 17940 RVA: 0x0018AEBD File Offset: 0x001890BD
	public bool ConsumeFuelOnLand
	{
		get
		{
			return this.consumeFuelOnLand;
		}
	}

	// Token: 0x1700051C RID: 1308
	// (get) Token: 0x06004615 RID: 17941 RVA: 0x0018AEC5 File Offset: 0x001890C5
	// (set) Token: 0x06004616 RID: 17942 RVA: 0x0018AED0 File Offset: 0x001890D0
	public float UserMaxCapacity
	{
		get
		{
			return this.targetFillMass;
		}
		set
		{
			this.targetFillMass = value;
			this.storage.capacityKg = this.targetFillMass;
			ConduitConsumer component = base.GetComponent<ConduitConsumer>();
			if (component != null)
			{
				component.capacityKG = this.targetFillMass;
			}
			ManualDeliveryKG component2 = base.GetComponent<ManualDeliveryKG>();
			if (component2 != null)
			{
				component2.capacity = (component2.refillMass = this.targetFillMass);
			}
			base.Trigger(-945020481, this);
		}
	}

	// Token: 0x1700051D RID: 1309
	// (get) Token: 0x06004617 RID: 17943 RVA: 0x0018AF42 File Offset: 0x00189142
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x1700051E RID: 1310
	// (get) Token: 0x06004618 RID: 17944 RVA: 0x0018AF49 File Offset: 0x00189149
	public float MaxCapacity
	{
		get
		{
			return this.physicalFuelCapacity;
		}
	}

	// Token: 0x1700051F RID: 1311
	// (get) Token: 0x06004619 RID: 17945 RVA: 0x0018AF51 File Offset: 0x00189151
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x17000520 RID: 1312
	// (get) Token: 0x0600461A RID: 17946 RVA: 0x0018AF5E File Offset: 0x0018915E
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000521 RID: 1313
	// (get) Token: 0x0600461B RID: 17947 RVA: 0x0018AF61 File Offset: 0x00189161
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x17000522 RID: 1314
	// (get) Token: 0x0600461C RID: 17948 RVA: 0x0018AF69 File Offset: 0x00189169
	// (set) Token: 0x0600461D RID: 17949 RVA: 0x0018AF74 File Offset: 0x00189174
	public Tag FuelType
	{
		get
		{
			return this.fuelType;
		}
		set
		{
			this.fuelType = value;
			if (this.storage.storageFilters == null)
			{
				this.storage.storageFilters = new List<Tag>();
			}
			this.storage.storageFilters.Add(this.fuelType);
			ManualDeliveryKG component = base.GetComponent<ManualDeliveryKG>();
			if (component != null)
			{
				component.RequestedItemTag = this.fuelType;
			}
		}
	}

	// Token: 0x0600461E RID: 17950 RVA: 0x0018AFD7 File Offset: 0x001891D7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<FuelTank>(-905833192, FuelTank.OnCopySettingsDelegate);
	}

	// Token: 0x0600461F RID: 17951 RVA: 0x0018AFF0 File Offset: 0x001891F0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.targetFillMass == -1f)
		{
			this.targetFillMass = this.physicalFuelCapacity;
		}
		base.GetComponent<KBatchedAnimController>().Play("grounded", KAnim.PlayMode.Loop, 1f, 0f);
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			base.GetComponent<RocketModule>().AddModuleCondition(ProcessCondition.ProcessConditionType.RocketStorage, new ConditionProperlyFueled(this));
		}
		base.Subscribe<FuelTank>(-887025858, FuelTank.OnRocketLandedDelegate);
		this.UserMaxCapacity = this.UserMaxCapacity;
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		this.meter.gameObject.GetComponent<KBatchedAnimTracker>().matchParentOffset = true;
		this.OnStorageChange(null);
		base.Subscribe<FuelTank>(-1697596308, FuelTank.OnStorageChangedDelegate);
	}

	// Token: 0x06004620 RID: 17952 RVA: 0x0018B0E5 File Offset: 0x001892E5
	private void OnStorageChange(object data)
	{
		this.meter.SetPositionPercent(this.storage.MassStored() / this.storage.capacityKg);
	}

	// Token: 0x06004621 RID: 17953 RVA: 0x0018B109 File Offset: 0x00189309
	private void OnRocketLanded(object data)
	{
		if (this.ConsumeFuelOnLand)
		{
			this.storage.ConsumeAllIgnoringDisease();
		}
	}

	// Token: 0x06004622 RID: 17954 RVA: 0x0018B120 File Offset: 0x00189320
	private void OnCopySettings(object data)
	{
		FuelTank component = ((GameObject)data).GetComponent<FuelTank>();
		if (component != null)
		{
			this.UserMaxCapacity = component.UserMaxCapacity;
		}
	}

	// Token: 0x06004623 RID: 17955 RVA: 0x0018B150 File Offset: 0x00189350
	public void DEBUG_FillTank()
	{
		if (!DlcManager.FeatureClusterSpaceEnabled())
		{
			RocketEngine rocketEngine = null;
			foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(base.GetComponent<AttachableBuilding>()))
			{
				rocketEngine = gameObject.GetComponent<RocketEngine>();
				if (rocketEngine != null && rocketEngine.mainEngine)
				{
					break;
				}
			}
			if (rocketEngine != null)
			{
				Element element = ElementLoader.GetElement(rocketEngine.fuelTag);
				if (element.IsLiquid)
				{
					this.storage.AddLiquid(element.id, this.targetFillMass - this.storage.MassStored(), element.defaultValues.temperature, 0, 0, false, true);
					return;
				}
				if (element.IsGas)
				{
					this.storage.AddGasChunk(element.id, this.targetFillMass - this.storage.MassStored(), element.defaultValues.temperature, 0, 0, false, true);
					return;
				}
				if (element.IsSolid)
				{
					this.storage.AddOre(element.id, this.targetFillMass - this.storage.MassStored(), element.defaultValues.temperature, 0, 0, false, true);
					return;
				}
			}
			else
			{
				global::Debug.LogWarning("Fuel tank couldn't find rocket engine");
			}
			return;
		}
		RocketEngineCluster rocketEngineCluster = null;
		foreach (GameObject gameObject2 in AttachableBuilding.GetAttachedNetwork(base.GetComponent<AttachableBuilding>()))
		{
			rocketEngineCluster = gameObject2.GetComponent<RocketEngineCluster>();
			if (rocketEngineCluster != null && rocketEngineCluster.mainEngine)
			{
				break;
			}
		}
		if (rocketEngineCluster != null)
		{
			Element element2 = ElementLoader.GetElement(rocketEngineCluster.fuelTag);
			if (element2.IsLiquid)
			{
				this.storage.AddLiquid(element2.id, this.targetFillMass - this.storage.MassStored(), element2.defaultValues.temperature, 0, 0, false, true);
			}
			else if (element2.IsGas)
			{
				this.storage.AddGasChunk(element2.id, this.targetFillMass - this.storage.MassStored(), element2.defaultValues.temperature, 0, 0, false, true);
			}
			else if (element2.IsSolid)
			{
				this.storage.AddOre(element2.id, this.targetFillMass - this.storage.MassStored(), element2.defaultValues.temperature, 0, 0, false, true);
			}
			rocketEngineCluster.GetComponent<RocketModuleCluster>().CraftInterface.GetComponent<Clustercraft>().UpdateStatusItem();
			return;
		}
		global::Debug.LogWarning("Fuel tank couldn't find rocket engine");
	}

	// Token: 0x04002E6C RID: 11884
	public Storage storage;

	// Token: 0x04002E6D RID: 11885
	private MeterController meter;

	// Token: 0x04002E6E RID: 11886
	[Serialize]
	public float targetFillMass = -1f;

	// Token: 0x04002E6F RID: 11887
	[SerializeField]
	public float physicalFuelCapacity;

	// Token: 0x04002E70 RID: 11888
	public bool consumeFuelOnLand;

	// Token: 0x04002E71 RID: 11889
	[SerializeField]
	private Tag fuelType;

	// Token: 0x04002E72 RID: 11890
	private static readonly EventSystem.IntraObjectHandler<FuelTank> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<FuelTank>(delegate(FuelTank component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04002E73 RID: 11891
	private static readonly EventSystem.IntraObjectHandler<FuelTank> OnRocketLandedDelegate = new EventSystem.IntraObjectHandler<FuelTank>(delegate(FuelTank component, object data)
	{
		component.OnRocketLanded(data);
	});

	// Token: 0x04002E74 RID: 11892
	private static readonly EventSystem.IntraObjectHandler<FuelTank> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<FuelTank>(delegate(FuelTank component, object data)
	{
		component.OnStorageChange(data);
	});
}
