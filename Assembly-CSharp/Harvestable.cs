using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x020007A6 RID: 1958
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/Harvestable")]
public class Harvestable : Workable
{
	// Token: 0x1700040B RID: 1035
	// (get) Token: 0x0600374C RID: 14156 RVA: 0x001339ED File Offset: 0x00131BED
	// (set) Token: 0x0600374D RID: 14157 RVA: 0x001339F5 File Offset: 0x00131BF5
	public Worker completed_by { get; protected set; }

	// Token: 0x1700040C RID: 1036
	// (get) Token: 0x0600374E RID: 14158 RVA: 0x001339FE File Offset: 0x00131BFE
	public bool CanBeHarvested
	{
		get
		{
			return this.canBeHarvested;
		}
	}

	// Token: 0x0600374F RID: 14159 RVA: 0x00133A06 File Offset: 0x00131C06
	protected Harvestable()
	{
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
	}

	// Token: 0x06003750 RID: 14160 RVA: 0x00133A19 File Offset: 0x00131C19
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Harvesting;
		this.multitoolContext = "harvest";
		this.multitoolHitEffectTag = "fx_harvest_splash";
	}

	// Token: 0x06003751 RID: 14161 RVA: 0x00133A58 File Offset: 0x00131C58
	protected override void OnSpawn()
	{
		this.harvestDesignatable = base.GetComponent<HarvestDesignatable>();
		base.Subscribe<Harvestable>(2127324410, Harvestable.ForceCancelHarvestDelegate);
		base.SetWorkTime(10f);
		base.Subscribe<Harvestable>(2127324410, Harvestable.OnCancelDelegate);
		this.faceTargetWhenWorking = true;
		Components.Harvestables.Add(this);
		this.attributeConverter = Db.Get().AttributeConverters.HarvestSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Farming.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
	}

	// Token: 0x06003752 RID: 14162 RVA: 0x00133AF5 File Offset: 0x00131CF5
	public void OnUprooted(object data)
	{
		if (this.canBeHarvested)
		{
			this.Harvest();
		}
	}

	// Token: 0x06003753 RID: 14163 RVA: 0x00133B08 File Offset: 0x00131D08
	public void Harvest()
	{
		this.harvestDesignatable.MarkedForHarvest = false;
		this.chore = null;
		base.Trigger(1272413801, this);
		KSelectable component = base.GetComponent<KSelectable>();
		component.RemoveStatusItem(Db.Get().MiscStatusItems.PendingHarvest, false);
		component.RemoveStatusItem(Db.Get().MiscStatusItems.Operating, false);
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06003754 RID: 14164 RVA: 0x00133B7C File Offset: 0x00131D7C
	public void OnMarkedForHarvest()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.chore == null)
		{
			this.chore = new WorkChore<Harvestable>(Db.Get().ChoreTypes.Harvest, this, null, true, null, null, null, true, null, false, true, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			component.AddStatusItem(Db.Get().MiscStatusItems.PendingHarvest, this);
		}
		component.RemoveStatusItem(Db.Get().MiscStatusItems.NotMarkedForHarvest, false);
	}

	// Token: 0x06003755 RID: 14165 RVA: 0x00133BF4 File Offset: 0x00131DF4
	public void SetCanBeHarvested(bool state)
	{
		this.canBeHarvested = state;
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.canBeHarvested)
		{
			component.AddStatusItem(Db.Get().CreatureStatusItems.ReadyForHarvest, null);
			if (this.harvestDesignatable.HarvestWhenReady)
			{
				this.harvestDesignatable.MarkForHarvest();
			}
			else if (this.harvestDesignatable.InPlanterBox)
			{
				component.AddStatusItem(Db.Get().MiscStatusItems.NotMarkedForHarvest, this);
			}
		}
		else
		{
			component.RemoveStatusItem(Db.Get().CreatureStatusItems.ReadyForHarvest, false);
			component.RemoveStatusItem(Db.Get().MiscStatusItems.NotMarkedForHarvest, false);
		}
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x06003756 RID: 14166 RVA: 0x00133CB1 File Offset: 0x00131EB1
	protected override void OnCompleteWork(Worker worker)
	{
		this.completed_by = worker;
		this.Harvest();
	}

	// Token: 0x06003757 RID: 14167 RVA: 0x00133CC0 File Offset: 0x00131EC0
	protected virtual void OnCancel(object data)
	{
		if (this.chore != null)
		{
			this.chore.Cancel("Cancel harvest");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingHarvest, false);
			this.harvestDesignatable.SetHarvestWhenReady(false);
		}
		this.harvestDesignatable.MarkedForHarvest = false;
	}

	// Token: 0x06003758 RID: 14168 RVA: 0x00133D20 File Offset: 0x00131F20
	public bool HasChore()
	{
		return this.chore != null;
	}

	// Token: 0x06003759 RID: 14169 RVA: 0x00133D2D File Offset: 0x00131F2D
	public virtual void ForceCancelHarvest(object data = null)
	{
		this.OnCancel(null);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingHarvest, false);
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x0600375A RID: 14170 RVA: 0x00133D67 File Offset: 0x00131F67
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Harvestables.Remove(this);
	}

	// Token: 0x0600375B RID: 14171 RVA: 0x00133D7A File Offset: 0x00131F7A
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.PendingHarvest, false);
	}

	// Token: 0x04002510 RID: 9488
	public HarvestDesignatable harvestDesignatable;

	// Token: 0x04002511 RID: 9489
	[Serialize]
	protected bool canBeHarvested;

	// Token: 0x04002513 RID: 9491
	protected Chore chore;

	// Token: 0x04002514 RID: 9492
	private static readonly EventSystem.IntraObjectHandler<Harvestable> ForceCancelHarvestDelegate = new EventSystem.IntraObjectHandler<Harvestable>(delegate(Harvestable component, object data)
	{
		component.ForceCancelHarvest(data);
	});

	// Token: 0x04002515 RID: 9493
	private static readonly EventSystem.IntraObjectHandler<Harvestable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Harvestable>(delegate(Harvestable component, object data)
	{
		component.OnCancel(data);
	});
}
