using System;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020009B5 RID: 2485
[AddComponentMenu("KMonoBehaviour/Workable/Uprootable")]
public class Uprootable : Workable
{
	// Token: 0x17000578 RID: 1400
	// (get) Token: 0x060049D0 RID: 18896 RVA: 0x0019DDB9 File Offset: 0x0019BFB9
	public bool IsMarkedForUproot
	{
		get
		{
			return this.isMarkedForUproot;
		}
	}

	// Token: 0x17000579 RID: 1401
	// (get) Token: 0x060049D1 RID: 18897 RVA: 0x0019DDC1 File Offset: 0x0019BFC1
	public Storage GetPlanterStorage
	{
		get
		{
			return this.planterStorage;
		}
	}

	// Token: 0x060049D2 RID: 18898 RVA: 0x0019DDCC File Offset: 0x0019BFCC
	protected Uprootable()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.buttonLabel = UI.USERMENUACTIONS.UPROOT.NAME;
		this.buttonTooltip = UI.USERMENUACTIONS.UPROOT.TOOLTIP;
		this.cancelButtonLabel = UI.USERMENUACTIONS.CANCELUPROOT.NAME;
		this.cancelButtonTooltip = UI.USERMENUACTIONS.CANCELUPROOT.TOOLTIP;
		this.pendingStatusItem = Db.Get().MiscStatusItems.PendingUproot;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Uprooting;
	}

	// Token: 0x060049D3 RID: 18899 RVA: 0x0019DE6C File Offset: 0x0019C06C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.pendingStatusItem = Db.Get().MiscStatusItems.PendingUproot;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Uprooting;
		this.attributeConverter = Db.Get().AttributeConverters.HarvestSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Farming.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.multitoolContext = "harvest";
		this.multitoolHitEffectTag = "fx_harvest_splash";
		base.Subscribe<Uprootable>(1309017699, Uprootable.OnPlanterStorageDelegate);
	}

	// Token: 0x060049D4 RID: 18900 RVA: 0x0019DF20 File Offset: 0x0019C120
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<Uprootable>(2127324410, Uprootable.ForceCancelUprootDelegate);
		base.SetWorkTime(12.5f);
		base.Subscribe<Uprootable>(2127324410, Uprootable.OnCancelDelegate);
		base.Subscribe<Uprootable>(493375141, Uprootable.OnRefreshUserMenuDelegate);
		this.faceTargetWhenWorking = true;
		Components.Uprootables.Add(this);
		this.area = base.GetComponent<OccupyArea>();
		Prioritizable.AddRef(base.gameObject);
		base.gameObject.AddTag(GameTags.Plant);
		Extents extents = new Extents(Grid.PosToCell(base.gameObject), base.gameObject.GetComponent<OccupyArea>().OccupiedCellsOffsets);
		this.partitionerEntry = GameScenePartitioner.Instance.Add(base.gameObject.name, base.gameObject.GetComponent<KPrefabID>(), extents, GameScenePartitioner.Instance.plants, null);
		if (this.isMarkedForUproot)
		{
			this.MarkForUproot(true);
		}
	}

	// Token: 0x060049D5 RID: 18901 RVA: 0x0019E010 File Offset: 0x0019C210
	private void OnPlanterStorage(object data)
	{
		this.planterStorage = (Storage)data;
		Prioritizable component = base.GetComponent<Prioritizable>();
		if (component != null)
		{
			component.showIcon = this.planterStorage == null;
		}
	}

	// Token: 0x060049D6 RID: 18902 RVA: 0x0019E04B File Offset: 0x0019C24B
	public bool IsInPlanterBox()
	{
		return this.planterStorage != null;
	}

	// Token: 0x060049D7 RID: 18903 RVA: 0x0019E05C File Offset: 0x0019C25C
	public void Uproot()
	{
		this.isMarkedForUproot = false;
		this.chore = null;
		this.uprootComplete = true;
		base.Trigger(-216549700, this);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingUproot, false);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.Operating, false);
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x060049D8 RID: 18904 RVA: 0x0019E0D7 File Offset: 0x0019C2D7
	public void SetCanBeUprooted(bool state)
	{
		this.canBeUprooted = state;
		if (this.canBeUprooted)
		{
			this.SetUprootedComplete(false);
		}
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x060049D9 RID: 18905 RVA: 0x0019E104 File Offset: 0x0019C304
	public void SetUprootedComplete(bool state)
	{
		this.uprootComplete = state;
	}

	// Token: 0x060049DA RID: 18906 RVA: 0x0019E110 File Offset: 0x0019C310
	public void MarkForUproot(bool instantOnDebug = true)
	{
		if (!this.canBeUprooted)
		{
			return;
		}
		if (DebugHandler.InstantBuildMode && instantOnDebug)
		{
			this.Uproot();
		}
		else if (this.chore == null)
		{
			this.chore = new WorkChore<Uprootable>(Db.Get().ChoreTypes.Uproot, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			base.GetComponent<KSelectable>().AddStatusItem(this.pendingStatusItem, this);
		}
		this.isMarkedForUproot = true;
	}

	// Token: 0x060049DB RID: 18907 RVA: 0x0019E185 File Offset: 0x0019C385
	protected override void OnCompleteWork(Worker worker)
	{
		this.Uproot();
	}

	// Token: 0x060049DC RID: 18908 RVA: 0x0019E190 File Offset: 0x0019C390
	private void OnCancel(object data)
	{
		if (this.chore != null)
		{
			this.chore.Cancel("Cancel uproot");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingUproot, false);
		}
		this.isMarkedForUproot = false;
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x060049DD RID: 18909 RVA: 0x0019E1F4 File Offset: 0x0019C3F4
	public bool HasChore()
	{
		return this.chore != null;
	}

	// Token: 0x060049DE RID: 18910 RVA: 0x0019E201 File Offset: 0x0019C401
	private void OnClickUproot()
	{
		this.MarkForUproot(true);
	}

	// Token: 0x060049DF RID: 18911 RVA: 0x0019E20A File Offset: 0x0019C40A
	protected void OnClickCancelUproot()
	{
		this.OnCancel(null);
	}

	// Token: 0x060049E0 RID: 18912 RVA: 0x0019E213 File Offset: 0x0019C413
	public virtual void ForceCancelUproot(object data = null)
	{
		this.OnCancel(null);
	}

	// Token: 0x060049E1 RID: 18913 RVA: 0x0019E21C File Offset: 0x0019C41C
	private void OnRefreshUserMenu(object data)
	{
		if (!this.showUserMenuButtons)
		{
			return;
		}
		if (this.uprootComplete)
		{
			if (this.deselectOnUproot)
			{
				KSelectable component = base.GetComponent<KSelectable>();
				if (component != null && SelectTool.Instance.selected == component)
				{
					SelectTool.Instance.Select(null, false);
				}
			}
			return;
		}
		if (!this.canBeUprooted)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((this.chore != null) ? new KIconButtonMenu.ButtonInfo("action_uproot", this.cancelButtonLabel, new System.Action(this.OnClickCancelUproot), global::Action.NumActions, null, null, null, this.cancelButtonTooltip, true) : new KIconButtonMenu.ButtonInfo("action_uproot", this.buttonLabel, new System.Action(this.OnClickUproot), global::Action.NumActions, null, null, null, this.buttonTooltip, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x060049E2 RID: 18914 RVA: 0x0019E2F6 File Offset: 0x0019C4F6
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		Components.Uprootables.Remove(this);
	}

	// Token: 0x060049E3 RID: 18915 RVA: 0x0019E319 File Offset: 0x0019C519
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingUproot, false);
	}

	// Token: 0x0400307B RID: 12411
	[Serialize]
	protected bool isMarkedForUproot;

	// Token: 0x0400307C RID: 12412
	protected bool uprootComplete;

	// Token: 0x0400307D RID: 12413
	[MyCmpReq]
	private Prioritizable prioritizable;

	// Token: 0x0400307E RID: 12414
	[Serialize]
	protected bool canBeUprooted = true;

	// Token: 0x0400307F RID: 12415
	public bool deselectOnUproot = true;

	// Token: 0x04003080 RID: 12416
	protected Chore chore;

	// Token: 0x04003081 RID: 12417
	private string buttonLabel;

	// Token: 0x04003082 RID: 12418
	private string buttonTooltip;

	// Token: 0x04003083 RID: 12419
	private string cancelButtonLabel;

	// Token: 0x04003084 RID: 12420
	private string cancelButtonTooltip;

	// Token: 0x04003085 RID: 12421
	private StatusItem pendingStatusItem;

	// Token: 0x04003086 RID: 12422
	public OccupyArea area;

	// Token: 0x04003087 RID: 12423
	private Storage planterStorage;

	// Token: 0x04003088 RID: 12424
	public bool showUserMenuButtons = true;

	// Token: 0x04003089 RID: 12425
	public HandleVector<int>.Handle partitionerEntry;

	// Token: 0x0400308A RID: 12426
	private static readonly EventSystem.IntraObjectHandler<Uprootable> OnPlanterStorageDelegate = new EventSystem.IntraObjectHandler<Uprootable>(delegate(Uprootable component, object data)
	{
		component.OnPlanterStorage(data);
	});

	// Token: 0x0400308B RID: 12427
	private static readonly EventSystem.IntraObjectHandler<Uprootable> ForceCancelUprootDelegate = new EventSystem.IntraObjectHandler<Uprootable>(delegate(Uprootable component, object data)
	{
		component.ForceCancelUproot(data);
	});

	// Token: 0x0400308C RID: 12428
	private static readonly EventSystem.IntraObjectHandler<Uprootable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Uprootable>(delegate(Uprootable component, object data)
	{
		component.OnCancel(data);
	});

	// Token: 0x0400308D RID: 12429
	private static readonly EventSystem.IntraObjectHandler<Uprootable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<Uprootable>(delegate(Uprootable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
