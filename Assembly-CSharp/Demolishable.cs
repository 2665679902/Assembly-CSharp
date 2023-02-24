using System;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000472 RID: 1138
[RequireComponent(typeof(Prioritizable))]
public class Demolishable : Workable
{
	// Token: 0x170000EB RID: 235
	// (get) Token: 0x06001938 RID: 6456 RVA: 0x00087223 File Offset: 0x00085423
	public bool HasBeenDestroyed
	{
		get
		{
			return this.destroyed;
		}
	}

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06001939 RID: 6457 RVA: 0x0008722C File Offset: 0x0008542C
	private CellOffset[] placementOffsets
	{
		get
		{
			Building component = base.GetComponent<Building>();
			if (component != null)
			{
				return component.Def.PlacementOffsets;
			}
			OccupyArea component2 = base.GetComponent<OccupyArea>();
			if (component2 != null)
			{
				return component2.OccupiedCellsOffsets;
			}
			global::Debug.Assert(false, "Ack! We put a Demolishable on something that's neither a Building nor OccupyArea!", this);
			return null;
		}
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x0008727C File Offset: 0x0008547C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.requiredSkillPerk = Db.Get().SkillPerks.CanDemolish.Id;
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Deconstructing;
		this.attributeConverter = Db.Get().AttributeConverters.ConstructionSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.minimumAttributeMultiplier = 0.75f;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Building.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.multitoolContext = "demolish";
		this.multitoolHitEffectTag = EffectConfigs.DemolishSplashId;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		CellOffset[][] array = OffsetGroups.InvertedStandardTable;
		CellOffset[] array2 = null;
		Building component = base.GetComponent<Building>();
		if (component != null && component.Def.IsTilePiece)
		{
			array = OffsetGroups.InvertedStandardTableWithCorners;
			array2 = component.Def.ConstructionOffsetFilter;
			base.SetWorkTime(component.Def.ConstructionTime * 0.5f);
		}
		else
		{
			base.SetWorkTime(30f);
		}
		CellOffset[][] array3 = OffsetGroups.BuildReachabilityTable(this.placementOffsets, array, array2);
		base.SetOffsetTable(array3);
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x000873BC File Offset: 0x000855BC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Demolishable>(493375141, Demolishable.OnRefreshUserMenuDelegate);
		base.Subscribe<Demolishable>(-111137758, Demolishable.OnRefreshUserMenuDelegate);
		base.Subscribe<Demolishable>(2127324410, Demolishable.OnCancelDelegate);
		base.Subscribe<Demolishable>(-790448070, Demolishable.OnDeconstructDelegate);
		if (this.isMarkedForDemolition)
		{
			this.QueueDemolition();
		}
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x00087421 File Offset: 0x00085621
	protected override void OnStartWork(Worker worker)
	{
		this.progressBar.barColor = ProgressBarsConfig.Instance.GetBarColor("DeconstructBar");
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingDemolition, false);
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x00087459 File Offset: 0x00085659
	protected override void OnCompleteWork(Worker worker)
	{
		this.TriggerDestroy();
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x00087461 File Offset: 0x00085661
	private void TriggerDestroy()
	{
		if (this == null || this.destroyed)
		{
			return;
		}
		this.destroyed = true;
		this.isMarkedForDemolition = false;
		base.gameObject.DeleteObject();
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x00087490 File Offset: 0x00085690
	private void QueueDemolition()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.OnCompleteWork(null);
			return;
		}
		if (this.chore == null)
		{
			Prioritizable.AddRef(base.gameObject);
			this.requiredSkillPerk = Db.Get().SkillPerks.CanDemolish.Id;
			this.chore = new WorkChore<Demolishable>(Db.Get().ChoreTypes.Demolish, this, null, true, null, null, null, true, null, false, false, Assets.GetAnim("anim_interacts_clothingfactory_kanim"), true, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.PendingDemolition, this);
			this.isMarkedForDemolition = true;
			base.Trigger(2108245096, "Demolish");
		}
		this.UpdateStatusItem(null);
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x00087550 File Offset: 0x00085750
	private void OnRefreshUserMenu(object data)
	{
		if (!this.allowDemolition)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((this.chore == null) ? new KIconButtonMenu.ButtonInfo("action_deconstruct", UI.USERMENUACTIONS.DEMOLISH.NAME, new System.Action(this.OnDemolish), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DEMOLISH.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_deconstruct", UI.USERMENUACTIONS.DEMOLISH.NAME_OFF, new System.Action(this.OnDemolish), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.DEMOLISH.TOOLTIP_OFF, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 0f);
	}

	// Token: 0x06001941 RID: 6465 RVA: 0x000875F4 File Offset: 0x000857F4
	public void CancelDemolition()
	{
		if (this.chore != null)
		{
			this.chore.Cancel("Cancelled demolition");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingDemolition, false);
			base.ShowProgressBar(false);
			this.isMarkedForDemolition = false;
			Prioritizable.RemoveRef(base.gameObject);
		}
		this.UpdateStatusItem(null);
	}

	// Token: 0x06001942 RID: 6466 RVA: 0x0008765C File Offset: 0x0008585C
	private void OnCancel(object data)
	{
		this.CancelDemolition();
	}

	// Token: 0x06001943 RID: 6467 RVA: 0x00087664 File Offset: 0x00085864
	private void OnDemolish(object data)
	{
		if (this.allowDemolition || DebugHandler.InstantBuildMode)
		{
			this.QueueDemolition();
		}
	}

	// Token: 0x06001944 RID: 6468 RVA: 0x0008767B File Offset: 0x0008587B
	private void OnDemolish()
	{
		if (this.chore == null)
		{
			this.QueueDemolition();
			return;
		}
		this.CancelDemolition();
	}

	// Token: 0x06001945 RID: 6469 RVA: 0x00087692 File Offset: 0x00085892
	protected override void UpdateStatusItem(object data = null)
	{
		this.shouldShowSkillPerkStatusItem = this.isMarkedForDemolition;
		base.UpdateStatusItem(data);
	}

	// Token: 0x04000E1C RID: 3612
	public Chore chore;

	// Token: 0x04000E1D RID: 3613
	public bool allowDemolition = true;

	// Token: 0x04000E1E RID: 3614
	[Serialize]
	private bool isMarkedForDemolition;

	// Token: 0x04000E1F RID: 3615
	private bool destroyed;

	// Token: 0x04000E20 RID: 3616
	private static readonly EventSystem.IntraObjectHandler<Demolishable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Demolishable>(delegate(Demolishable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x04000E21 RID: 3617
	private static readonly EventSystem.IntraObjectHandler<Demolishable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Demolishable>(delegate(Demolishable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x04000E22 RID: 3618
	private static readonly EventSystem.IntraObjectHandler<Demolishable> OnDeconstructDelegate = new EventSystem.IntraObjectHandler<Demolishable>(delegate(Demolishable component, object data)
	{
		component.OnDemolish(data);
	});
}
