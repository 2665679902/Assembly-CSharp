using System;
using System.Collections.Generic;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020009AC RID: 2476
[AddComponentMenu("KMonoBehaviour/Workable/Tinkerable")]
public class Tinkerable : Workable
{
	// Token: 0x06004978 RID: 18808 RVA: 0x0019B7DC File Offset: 0x001999DC
	public static Tinkerable MakePowerTinkerable(GameObject prefab)
	{
		RoomTracker roomTracker = prefab.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.PowerPlant.Id;
		roomTracker.requirement = RoomTracker.Requirement.TrackingOnly;
		Tinkerable tinkerable = prefab.AddOrGet<Tinkerable>();
		tinkerable.tinkerMaterialTag = PowerControlStationConfig.TINKER_TOOLS;
		tinkerable.tinkerMaterialAmount = 1f;
		tinkerable.requiredSkillPerk = PowerControlStationConfig.ROLE_PERK;
		tinkerable.SetWorkTime(180f);
		tinkerable.workerStatusItem = Db.Get().DuplicantStatusItems.Tinkering;
		tinkerable.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		tinkerable.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		tinkerable.choreTypeTinker = Db.Get().ChoreTypes.PowerTinker.IdHash;
		tinkerable.choreTypeFetch = Db.Get().ChoreTypes.PowerFetch.IdHash;
		tinkerable.addedEffect = "PowerTinker";
		tinkerable.effectAttributeId = Db.Get().Attributes.Machinery.Id;
		tinkerable.effectMultiplier = 0.025f;
		tinkerable.multitoolContext = "powertinker";
		tinkerable.multitoolHitEffectTag = "fx_powertinker_splash";
		tinkerable.shouldShowSkillPerkStatusItem = false;
		prefab.AddOrGet<Storage>();
		prefab.AddOrGet<Effects>();
		prefab.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
			inst.GetComponent<Tinkerable>().SetOffsetTable(OffsetGroups.InvertedStandardTable);
		};
		return tinkerable;
	}

	// Token: 0x06004979 RID: 18809 RVA: 0x0019B93C File Offset: 0x00199B3C
	public static Tinkerable MakeFarmTinkerable(GameObject prefab)
	{
		RoomTracker roomTracker = prefab.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Farm.Id;
		roomTracker.requirement = RoomTracker.Requirement.TrackingOnly;
		Tinkerable tinkerable = prefab.AddOrGet<Tinkerable>();
		tinkerable.tinkerMaterialTag = FarmStationConfig.TINKER_TOOLS;
		tinkerable.tinkerMaterialAmount = 1f;
		tinkerable.requiredSkillPerk = Db.Get().SkillPerks.CanFarmTinker.Id;
		tinkerable.workerStatusItem = Db.Get().DuplicantStatusItems.Tinkering;
		tinkerable.addedEffect = "FarmTinker";
		tinkerable.effectAttributeId = Db.Get().Attributes.Botanist.Id;
		tinkerable.effectMultiplier = 0.1f;
		tinkerable.SetWorkTime(15f);
		tinkerable.attributeConverter = Db.Get().AttributeConverters.PlantTendSpeed;
		tinkerable.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		tinkerable.choreTypeTinker = Db.Get().ChoreTypes.CropTend.IdHash;
		tinkerable.choreTypeFetch = Db.Get().ChoreTypes.FarmFetch.IdHash;
		tinkerable.multitoolContext = "tend";
		tinkerable.multitoolHitEffectTag = "fx_tend_splash";
		tinkerable.shouldShowSkillPerkStatusItem = false;
		prefab.AddOrGet<Storage>();
		prefab.AddOrGet<Effects>();
		prefab.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
			inst.GetComponent<Tinkerable>().SetOffsetTable(OffsetGroups.InvertedStandardTable);
		};
		return tinkerable;
	}

	// Token: 0x0600497A RID: 18810 RVA: 0x0019BAA8 File Offset: 0x00199CA8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_machine_kanim") };
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Tinkering;
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		base.Subscribe<Tinkerable>(-1157678353, Tinkerable.OnEffectRemovedDelegate);
		base.Subscribe<Tinkerable>(-1697596308, Tinkerable.OnStorageChangeDelegate);
		base.Subscribe<Tinkerable>(144050788, Tinkerable.OnUpdateRoomDelegate);
		base.Subscribe<Tinkerable>(-592767678, Tinkerable.OnOperationalChangedDelegate);
	}

	// Token: 0x0600497B RID: 18811 RVA: 0x0019BB55 File Offset: 0x00199D55
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Prioritizable.AddRef(base.gameObject);
	}

	// Token: 0x0600497C RID: 18812 RVA: 0x0019BB68 File Offset: 0x00199D68
	protected override void OnCleanUp()
	{
		this.UpdateMaterialReservation(false);
		if (this.updateHandle.IsValid)
		{
			this.updateHandle.ClearScheduler();
		}
		Prioritizable.RemoveRef(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x0600497D RID: 18813 RVA: 0x0019BB9A File Offset: 0x00199D9A
	private void OnOperationalChanged(object data)
	{
		this.QueueUpdateChore();
	}

	// Token: 0x0600497E RID: 18814 RVA: 0x0019BBA2 File Offset: 0x00199DA2
	private void OnEffectRemoved(object data)
	{
		this.QueueUpdateChore();
	}

	// Token: 0x0600497F RID: 18815 RVA: 0x0019BBAA File Offset: 0x00199DAA
	private void OnUpdateRoom(object data)
	{
		this.QueueUpdateChore();
	}

	// Token: 0x06004980 RID: 18816 RVA: 0x0019BBB2 File Offset: 0x00199DB2
	private void OnStorageChange(object data)
	{
		if (((GameObject)data).IsPrefabID(this.tinkerMaterialTag))
		{
			this.QueueUpdateChore();
		}
	}

	// Token: 0x06004981 RID: 18817 RVA: 0x0019BBD0 File Offset: 0x00199DD0
	private void QueueUpdateChore()
	{
		if (this.updateHandle.IsValid)
		{
			this.updateHandle.ClearScheduler();
		}
		this.updateHandle = GameScheduler.Instance.Schedule("UpdateTinkerChore", 1.2f, new Action<object>(this.UpdateChoreCallback), null, null);
	}

	// Token: 0x06004982 RID: 18818 RVA: 0x0019BC1D File Offset: 0x00199E1D
	private void UpdateChoreCallback(object obj)
	{
		this.UpdateChore();
	}

	// Token: 0x06004983 RID: 18819 RVA: 0x0019BC28 File Offset: 0x00199E28
	private void UpdateChore()
	{
		Operational component = base.GetComponent<Operational>();
		bool flag = component == null || component.IsFunctional;
		bool flag2 = this.HasEffect();
		bool flag3 = this.RoomHasActiveTinkerstation();
		bool flag4 = !flag2 && flag3 && flag;
		bool flag5 = flag2 || !flag3;
		if (this.chore == null && flag4)
		{
			this.UpdateMaterialReservation(true);
			if (this.HasMaterial())
			{
				this.chore = new WorkChore<Tinkerable>(Db.Get().ChoreTypes.GetByHash(this.choreTypeTinker), this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
				if (component != null)
				{
					this.chore.AddPrecondition(ChorePreconditions.instance.IsFunctional, component);
				}
			}
			else
			{
				this.chore = new FetchChore(Db.Get().ChoreTypes.GetByHash(this.choreTypeFetch), this.storage, this.tinkerMaterialAmount, new HashSet<Tag> { this.tinkerMaterialTag }, FetchChore.MatchCriteria.MatchID, Tag.Invalid, null, null, true, new Action<Chore>(this.OnFetchComplete), null, null, Operational.State.Functional, 0);
			}
			this.chore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, this.requiredSkillPerk);
			if (!string.IsNullOrEmpty(base.GetComponent<RoomTracker>().requiredRoomType))
			{
				this.chore.AddPrecondition(ChorePreconditions.instance.IsInMyRoom, Grid.PosToCell(base.transform.GetPosition()));
				return;
			}
		}
		else if (this.chore != null && flag5)
		{
			this.UpdateMaterialReservation(false);
			this.chore.Cancel("No longer needed");
			this.chore = null;
		}
	}

	// Token: 0x06004984 RID: 18820 RVA: 0x0019BDC0 File Offset: 0x00199FC0
	private bool RoomHasActiveTinkerstation()
	{
		if (!this.roomTracker.IsInCorrectRoom())
		{
			return false;
		}
		if (this.roomTracker.room == null)
		{
			return false;
		}
		foreach (KPrefabID kprefabID in this.roomTracker.room.buildings)
		{
			if (!(kprefabID == null))
			{
				TinkerStation component = kprefabID.GetComponent<TinkerStation>();
				if (component != null && component.outputPrefab == this.tinkerMaterialTag && kprefabID.GetComponent<Operational>().IsOperational)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06004985 RID: 18821 RVA: 0x0019BE74 File Offset: 0x0019A074
	private void UpdateMaterialReservation(bool shouldReserve)
	{
		if (shouldReserve && !this.hasReservedMaterial)
		{
			MaterialNeeds.UpdateNeed(this.tinkerMaterialTag, this.tinkerMaterialAmount, base.gameObject.GetMyWorldId());
			this.hasReservedMaterial = shouldReserve;
			return;
		}
		if (!shouldReserve && this.hasReservedMaterial)
		{
			MaterialNeeds.UpdateNeed(this.tinkerMaterialTag, -this.tinkerMaterialAmount, base.gameObject.GetMyWorldId());
			this.hasReservedMaterial = shouldReserve;
		}
	}

	// Token: 0x06004986 RID: 18822 RVA: 0x0019BEDF File Offset: 0x0019A0DF
	private void OnFetchComplete(Chore data)
	{
		this.UpdateMaterialReservation(false);
		this.chore = null;
		this.UpdateChore();
	}

	// Token: 0x06004987 RID: 18823 RVA: 0x0019BEF8 File Offset: 0x0019A0F8
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		this.storage.ConsumeIgnoringDisease(this.tinkerMaterialTag, this.tinkerMaterialAmount);
		float totalValue = worker.GetAttributes().Get(Db.Get().Attributes.Get(this.effectAttributeId)).GetTotalValue();
		this.effects.Add(this.addedEffect, true).timeRemaining *= 1f + totalValue * this.effectMultiplier;
		this.UpdateMaterialReservation(false);
		this.chore = null;
		this.UpdateChore();
	}

	// Token: 0x06004988 RID: 18824 RVA: 0x0019BF89 File Offset: 0x0019A189
	private bool HasMaterial()
	{
		return this.storage.GetAmountAvailable(this.tinkerMaterialTag) >= this.tinkerMaterialAmount;
	}

	// Token: 0x06004989 RID: 18825 RVA: 0x0019BFA7 File Offset: 0x0019A1A7
	private bool HasEffect()
	{
		return this.effects.HasEffect(this.addedEffect);
	}

	// Token: 0x04003048 RID: 12360
	private Chore chore;

	// Token: 0x04003049 RID: 12361
	[MyCmpGet]
	private Storage storage;

	// Token: 0x0400304A RID: 12362
	[MyCmpGet]
	private Effects effects;

	// Token: 0x0400304B RID: 12363
	[MyCmpGet]
	private RoomTracker roomTracker;

	// Token: 0x0400304C RID: 12364
	public Tag tinkerMaterialTag;

	// Token: 0x0400304D RID: 12365
	public float tinkerMaterialAmount;

	// Token: 0x0400304E RID: 12366
	public string addedEffect;

	// Token: 0x0400304F RID: 12367
	public string effectAttributeId;

	// Token: 0x04003050 RID: 12368
	public float effectMultiplier;

	// Token: 0x04003051 RID: 12369
	public HashedString choreTypeTinker = Db.Get().ChoreTypes.PowerTinker.IdHash;

	// Token: 0x04003052 RID: 12370
	public HashedString choreTypeFetch = Db.Get().ChoreTypes.PowerFetch.IdHash;

	// Token: 0x04003053 RID: 12371
	private static readonly EventSystem.IntraObjectHandler<Tinkerable> OnEffectRemovedDelegate = new EventSystem.IntraObjectHandler<Tinkerable>(delegate(Tinkerable component, object data)
	{
		component.OnEffectRemoved(data);
	});

	// Token: 0x04003054 RID: 12372
	private static readonly EventSystem.IntraObjectHandler<Tinkerable> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<Tinkerable>(delegate(Tinkerable component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04003055 RID: 12373
	private static readonly EventSystem.IntraObjectHandler<Tinkerable> OnUpdateRoomDelegate = new EventSystem.IntraObjectHandler<Tinkerable>(delegate(Tinkerable component, object data)
	{
		component.OnUpdateRoom(data);
	});

	// Token: 0x04003056 RID: 12374
	private static readonly EventSystem.IntraObjectHandler<Tinkerable> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<Tinkerable>(delegate(Tinkerable component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04003057 RID: 12375
	private SchedulerHandle updateHandle;

	// Token: 0x04003058 RID: 12376
	private bool hasReservedMaterial;
}
