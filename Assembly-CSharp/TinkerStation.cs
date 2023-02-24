using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020009AB RID: 2475
[AddComponentMenu("KMonoBehaviour/Workable/TinkerStation")]
public class TinkerStation : Workable, IGameObjectEffectDescriptor, ISim1000ms
{
	// Token: 0x17000572 RID: 1394
	// (set) Token: 0x06004964 RID: 18788 RVA: 0x0019B26B File Offset: 0x0019946B
	public AttributeConverter AttributeConverter
	{
		set
		{
			this.attributeConverter = value;
		}
	}

	// Token: 0x17000573 RID: 1395
	// (set) Token: 0x06004965 RID: 18789 RVA: 0x0019B274 File Offset: 0x00199474
	public float AttributeExperienceMultiplier
	{
		set
		{
			this.attributeExperienceMultiplier = value;
		}
	}

	// Token: 0x17000574 RID: 1396
	// (set) Token: 0x06004966 RID: 18790 RVA: 0x0019B27D File Offset: 0x0019947D
	public string SkillExperienceSkillGroup
	{
		set
		{
			this.skillExperienceSkillGroup = value;
		}
	}

	// Token: 0x17000575 RID: 1397
	// (set) Token: 0x06004967 RID: 18791 RVA: 0x0019B286 File Offset: 0x00199486
	public float SkillExperienceMultiplier
	{
		set
		{
			this.skillExperienceMultiplier = value;
		}
	}

	// Token: 0x06004968 RID: 18792 RVA: 0x0019B290 File Offset: 0x00199490
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		if (this.useFilteredStorage)
		{
			ChoreType byHash = Db.Get().ChoreTypes.GetByHash(this.fetchChoreType);
			this.filteredStorage = new FilteredStorage(this, null, null, false, byHash);
		}
		base.SetWorkTime(15f);
		base.Subscribe<TinkerStation>(-592767678, TinkerStation.OnOperationalChangedDelegate);
	}

	// Token: 0x06004969 RID: 18793 RVA: 0x0019B332 File Offset: 0x00199532
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.useFilteredStorage && this.filteredStorage != null)
		{
			this.filteredStorage.FilterChanged();
		}
	}

	// Token: 0x0600496A RID: 18794 RVA: 0x0019B355 File Offset: 0x00199555
	protected override void OnCleanUp()
	{
		if (this.filteredStorage != null)
		{
			this.filteredStorage.CleanUp();
		}
		base.OnCleanUp();
	}

	// Token: 0x0600496B RID: 18795 RVA: 0x0019B370 File Offset: 0x00199570
	private bool CorrectRolePrecondition(MinionIdentity worker)
	{
		MinionResume component = worker.GetComponent<MinionResume>();
		return component != null && component.HasPerk(this.requiredSkillPerk);
	}

	// Token: 0x0600496C RID: 18796 RVA: 0x0019B3A0 File Offset: 0x001995A0
	private void OnOperationalChanged(object data)
	{
		RoomTracker component = base.GetComponent<RoomTracker>();
		if (component != null && component.room != null)
		{
			component.room.RetriggerBuildings();
		}
	}

	// Token: 0x0600496D RID: 18797 RVA: 0x0019B3D0 File Offset: 0x001995D0
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		if (!this.operational.IsOperational)
		{
			return;
		}
		this.operational.SetActive(true, false);
	}

	// Token: 0x0600496E RID: 18798 RVA: 0x0019B3F4 File Offset: 0x001995F4
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		base.ShowProgressBar(false);
		this.operational.SetActive(false, false);
	}

	// Token: 0x0600496F RID: 18799 RVA: 0x0019B414 File Offset: 0x00199614
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		float num;
		SimUtil.DiseaseInfo diseaseInfo;
		float num2;
		this.storage.ConsumeAndGetDisease(this.inputMaterial, this.massPerTinker, out num, out diseaseInfo, out num2);
		GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab(this.outputPrefab), base.transform.GetPosition(), Grid.SceneLayer.Ore, null, 0);
		gameObject.GetComponent<PrimaryElement>().Temperature = this.outputTemperature;
		gameObject.SetActive(true);
		this.chore = null;
	}

	// Token: 0x06004970 RID: 18800 RVA: 0x0019B482 File Offset: 0x00199682
	public void Sim1000ms(float dt)
	{
		this.UpdateChore();
	}

	// Token: 0x06004971 RID: 18801 RVA: 0x0019B48C File Offset: 0x0019968C
	private void UpdateChore()
	{
		if (this.operational.IsOperational && (this.ToolsRequested() || this.alwaysTinker) && this.HasMaterial())
		{
			if (this.chore == null)
			{
				this.chore = new WorkChore<TinkerStation>(Db.Get().ChoreTypes.GetByHash(this.choreType), this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
				this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, this.requiredSkillPerk);
				base.SetWorkTime(this.workTime);
				return;
			}
		}
		else if (this.chore != null)
		{
			this.chore.Cancel("Can't tinker");
			this.chore = null;
		}
	}

	// Token: 0x06004972 RID: 18802 RVA: 0x0019B53F File Offset: 0x0019973F
	private bool HasMaterial()
	{
		return this.storage.MassStored() > 0f;
	}

	// Token: 0x06004973 RID: 18803 RVA: 0x0019B554 File Offset: 0x00199754
	private bool ToolsRequested()
	{
		return MaterialNeeds.GetAmount(this.outputPrefab, base.gameObject.GetMyWorldId(), false) > 0f && this.GetMyWorld().worldInventory.GetAmount(this.outputPrefab, true) <= 0f;
	}

	// Token: 0x06004974 RID: 18804 RVA: 0x0019B5A4 File Offset: 0x001997A4
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		string text = this.inputMaterial.ProperName();
		List<Descriptor> descriptors = base.GetDescriptors(go);
		descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(this.massPerTinker, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(this.massPerTinker, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		descriptors.AddRange(GameUtil.GetAllDescriptors(Assets.GetPrefab(this.outputPrefab), false));
		List<Tinkerable> list = new List<Tinkerable>();
		foreach (GameObject gameObject in Assets.GetPrefabsWithComponent<Tinkerable>())
		{
			Tinkerable component = gameObject.GetComponent<Tinkerable>();
			if (component.tinkerMaterialTag == this.outputPrefab)
			{
				list.Add(component);
			}
		}
		if (list.Count > 0)
		{
			Effect effect = Db.Get().effects.Get(list[0].addedEffect);
			descriptors.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ADDED_EFFECT, effect.Name), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ADDED_EFFECT, effect.Name, Effect.CreateTooltip(effect, true, "\n    • ", true)), Descriptor.DescriptorType.Effect, false));
			descriptors.Add(new Descriptor(UI.BUILDINGEFFECTS.IMPROVED_BUILDINGS, UI.BUILDINGEFFECTS.TOOLTIPS.IMPROVED_BUILDINGS, Descriptor.DescriptorType.Effect, false));
			foreach (Tinkerable tinkerable in list)
			{
				Descriptor descriptor = new Descriptor(string.Format(UI.BUILDINGEFFECTS.IMPROVED_BUILDINGS_ITEM, tinkerable.GetProperName()), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.IMPROVED_BUILDINGS_ITEM, tinkerable.GetProperName()), Descriptor.DescriptorType.Effect, false);
				descriptor.IncreaseIndent();
				descriptors.Add(descriptor);
			}
		}
		return descriptors;
	}

	// Token: 0x06004975 RID: 18805 RVA: 0x0019B7A4 File Offset: 0x001999A4
	public static TinkerStation AddTinkerStation(GameObject go, string required_room_type)
	{
		TinkerStation tinkerStation = go.AddOrGet<TinkerStation>();
		go.AddOrGet<RoomTracker>().requiredRoomType = required_room_type;
		return tinkerStation;
	}

	// Token: 0x0400303B RID: 12347
	public HashedString choreType;

	// Token: 0x0400303C RID: 12348
	public HashedString fetchChoreType;

	// Token: 0x0400303D RID: 12349
	private Chore chore;

	// Token: 0x0400303E RID: 12350
	[MyCmpAdd]
	private Operational operational;

	// Token: 0x0400303F RID: 12351
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04003040 RID: 12352
	public bool useFilteredStorage;

	// Token: 0x04003041 RID: 12353
	protected FilteredStorage filteredStorage;

	// Token: 0x04003042 RID: 12354
	public bool alwaysTinker;

	// Token: 0x04003043 RID: 12355
	public float massPerTinker;

	// Token: 0x04003044 RID: 12356
	public Tag inputMaterial;

	// Token: 0x04003045 RID: 12357
	public Tag outputPrefab;

	// Token: 0x04003046 RID: 12358
	public float outputTemperature;

	// Token: 0x04003047 RID: 12359
	private static readonly EventSystem.IntraObjectHandler<TinkerStation> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<TinkerStation>(delegate(TinkerStation component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
