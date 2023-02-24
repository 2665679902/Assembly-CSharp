using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000955 RID: 2389
[AddComponentMenu("KMonoBehaviour/scripts/OxidizerTank")]
public class OxidizerTank : KMonoBehaviour, IUserControlledCapacity
{
	// Token: 0x17000538 RID: 1336
	// (get) Token: 0x06004687 RID: 18055 RVA: 0x0018D132 File Offset: 0x0018B332
	public bool IsSuspended
	{
		get
		{
			return this.isSuspended;
		}
	}

	// Token: 0x17000539 RID: 1337
	// (get) Token: 0x06004688 RID: 18056 RVA: 0x0018D13A File Offset: 0x0018B33A
	// (set) Token: 0x06004689 RID: 18057 RVA: 0x0018D144 File Offset: 0x0018B344
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
			base.Trigger(-945020481, this);
			this.OnStorageCapacityChanged(this.targetFillMass);
			if (this.filteredStorage != null)
			{
				this.filteredStorage.FilterChanged();
			}
		}
	}

	// Token: 0x1700053A RID: 1338
	// (get) Token: 0x0600468A RID: 18058 RVA: 0x0018D1B0 File Offset: 0x0018B3B0
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x1700053B RID: 1339
	// (get) Token: 0x0600468B RID: 18059 RVA: 0x0018D1B7 File Offset: 0x0018B3B7
	public float MaxCapacity
	{
		get
		{
			return this.maxFillMass;
		}
	}

	// Token: 0x1700053C RID: 1340
	// (get) Token: 0x0600468C RID: 18060 RVA: 0x0018D1BF File Offset: 0x0018B3BF
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x1700053D RID: 1341
	// (get) Token: 0x0600468D RID: 18061 RVA: 0x0018D1CC File Offset: 0x0018B3CC
	public float TotalOxidizerPower
	{
		get
		{
			float num = 0f;
			foreach (GameObject gameObject in this.storage.items)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				float num2;
				if (DlcManager.FeatureClusterSpaceEnabled())
				{
					num2 = Clustercraft.dlc1OxidizerEfficiencies[component.ElementID.CreateTag()];
				}
				else
				{
					num2 = RocketStats.oxidizerEfficiencies[component.ElementID.CreateTag()];
				}
				num += component.Mass * num2;
			}
			return num;
		}
	}

	// Token: 0x1700053E RID: 1342
	// (get) Token: 0x0600468E RID: 18062 RVA: 0x0018D270 File Offset: 0x0018B470
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x1700053F RID: 1343
	// (get) Token: 0x0600468F RID: 18063 RVA: 0x0018D273 File Offset: 0x0018B473
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x06004690 RID: 18064 RVA: 0x0018D27C File Offset: 0x0018B47C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<OxidizerTank>(-905833192, OxidizerTank.OnCopySettingsDelegate);
		if (this.supportsMultipleOxidizers)
		{
			this.filteredStorage = new FilteredStorage(this, null, this, true, Db.Get().ChoreTypes.Fetch);
			this.filteredStorage.FilterChanged();
			KBatchedAnimTracker componentInChildren = base.gameObject.GetComponentInChildren<KBatchedAnimTracker>();
			componentInChildren.forceAlwaysAlive = true;
			componentInChildren.matchParentOffset = true;
		}
	}

	// Token: 0x06004691 RID: 18065 RVA: 0x0018D2EC File Offset: 0x0018B4EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.discoverResourcesOnSpawn != null)
		{
			foreach (SimHashes simHashes in this.discoverResourcesOnSpawn)
			{
				Element element = ElementLoader.FindElementByHash(simHashes);
				DiscoveredResources.Instance.Discover(element.tag, element.GetMaterialCategoryTag());
			}
		}
		base.GetComponent<KBatchedAnimController>().Play("grounded", KAnim.PlayMode.Loop, 1f, 0f);
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null)
		{
			global::Debug.Assert(DlcManager.IsExpansion1Active(), "EXP1 not active but trying to use EXP1 rockety system");
			component.AddModuleCondition(ProcessCondition.ProcessConditionType.RocketStorage, new ConditionSufficientOxidizer(this));
		}
		this.UserMaxCapacity = Mathf.Min(this.UserMaxCapacity, this.maxFillMass);
		base.Subscribe<OxidizerTank>(-887025858, OxidizerTank.OnRocketLandedDelegate);
		base.Subscribe<OxidizerTank>(-1697596308, OxidizerTank.OnStorageChangeDelegate);
	}

	// Token: 0x06004692 RID: 18066 RVA: 0x0018D3E8 File Offset: 0x0018B5E8
	public float GetTotalOxidizerAvailable()
	{
		float num = 0f;
		foreach (Tag tag in this.oxidizerTypes)
		{
			num += this.storage.GetAmountAvailable(tag);
		}
		return num;
	}

	// Token: 0x06004693 RID: 18067 RVA: 0x0018D428 File Offset: 0x0018B628
	public Dictionary<Tag, float> GetOxidizersAvailable()
	{
		Dictionary<Tag, float> dictionary = new Dictionary<Tag, float>();
		foreach (Tag tag in this.oxidizerTypes)
		{
			dictionary[tag] = this.storage.GetAmountAvailable(tag);
		}
		return dictionary;
	}

	// Token: 0x06004694 RID: 18068 RVA: 0x0018D46C File Offset: 0x0018B66C
	private void OnStorageChange(object data)
	{
		this.RefreshMeter();
	}

	// Token: 0x06004695 RID: 18069 RVA: 0x0018D474 File Offset: 0x0018B674
	private void OnStorageCapacityChanged(float newCapacity)
	{
		this.RefreshMeter();
	}

	// Token: 0x06004696 RID: 18070 RVA: 0x0018D47C File Offset: 0x0018B67C
	private void RefreshMeter()
	{
		if (this.filteredStorage != null)
		{
			this.filteredStorage.FilterChanged();
		}
	}

	// Token: 0x06004697 RID: 18071 RVA: 0x0018D491 File Offset: 0x0018B691
	private void OnRocketLanded(object data)
	{
		if (this.consumeOnLand)
		{
			this.storage.ConsumeAllIgnoringDisease();
		}
		if (this.filteredStorage != null)
		{
			this.filteredStorage.FilterChanged();
		}
	}

	// Token: 0x06004698 RID: 18072 RVA: 0x0018D4BC File Offset: 0x0018B6BC
	private void OnCopySettings(object data)
	{
		OxidizerTank component = ((GameObject)data).GetComponent<OxidizerTank>();
		if (component != null)
		{
			this.UserMaxCapacity = component.UserMaxCapacity;
		}
	}

	// Token: 0x06004699 RID: 18073 RVA: 0x0018D4EC File Offset: 0x0018B6EC
	[ContextMenu("Fill Tank")]
	public void DEBUG_FillTank(SimHashes element)
	{
		base.GetComponent<FlatTagFilterable>().selectedTags.Add(element.CreateTag());
		if (ElementLoader.FindElementByHash(element).IsLiquid)
		{
			this.storage.AddLiquid(element, this.targetFillMass, ElementLoader.FindElementByHash(element).defaultValues.temperature, 0, 0, false, true);
			return;
		}
		if (ElementLoader.FindElementByHash(element).IsSolid)
		{
			GameObject gameObject = ElementLoader.FindElementByHash(element).substance.SpawnResource(base.gameObject.transform.GetPosition(), this.targetFillMass, 300f, byte.MaxValue, 0, false, false, false);
			this.storage.Store(gameObject, false, false, true, false);
		}
	}

	// Token: 0x0600469A RID: 18074 RVA: 0x0018D598 File Offset: 0x0018B798
	public OxidizerTank()
	{
		Tag[] array2;
		if (!DlcManager.IsExpansion1Active())
		{
			Tag[] array = new Tag[2];
			array[0] = SimHashes.OxyRock.CreateTag();
			array2 = array;
			array[1] = SimHashes.LiquidOxygen.CreateTag();
		}
		else
		{
			Tag[] array3 = new Tag[3];
			array3[0] = SimHashes.OxyRock.CreateTag();
			array3[1] = SimHashes.LiquidOxygen.CreateTag();
			array2 = array3;
			array3[2] = SimHashes.Fertilizer.CreateTag();
		}
		this.oxidizerTypes = array2;
		base..ctor();
	}

	// Token: 0x04002EA8 RID: 11944
	public Storage storage;

	// Token: 0x04002EA9 RID: 11945
	public bool supportsMultipleOxidizers;

	// Token: 0x04002EAA RID: 11946
	private MeterController meter;

	// Token: 0x04002EAB RID: 11947
	private bool isSuspended;

	// Token: 0x04002EAC RID: 11948
	public bool consumeOnLand = true;

	// Token: 0x04002EAD RID: 11949
	[Serialize]
	public float maxFillMass;

	// Token: 0x04002EAE RID: 11950
	[Serialize]
	public float targetFillMass;

	// Token: 0x04002EAF RID: 11951
	public List<SimHashes> discoverResourcesOnSpawn;

	// Token: 0x04002EB0 RID: 11952
	[SerializeField]
	private Tag[] oxidizerTypes;

	// Token: 0x04002EB1 RID: 11953
	private FilteredStorage filteredStorage;

	// Token: 0x04002EB2 RID: 11954
	private static readonly EventSystem.IntraObjectHandler<OxidizerTank> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<OxidizerTank>(delegate(OxidizerTank component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04002EB3 RID: 11955
	private static readonly EventSystem.IntraObjectHandler<OxidizerTank> OnRocketLandedDelegate = new EventSystem.IntraObjectHandler<OxidizerTank>(delegate(OxidizerTank component, object data)
	{
		component.OnRocketLanded(data);
	});

	// Token: 0x04002EB4 RID: 11956
	private static readonly EventSystem.IntraObjectHandler<OxidizerTank> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<OxidizerTank>(delegate(OxidizerTank component, object data)
	{
		component.OnStorageChange(data);
	});
}
