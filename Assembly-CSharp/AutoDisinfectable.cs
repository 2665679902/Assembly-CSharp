using System;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200044E RID: 1102
[AddComponentMenu("KMonoBehaviour/Workable/AutoDisinfectable")]
public class AutoDisinfectable : Workable
{
	// Token: 0x060017CD RID: 6093 RVA: 0x0007CBB0 File Offset: 0x0007ADB0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTableWithCorners);
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Disinfecting;
		this.resetProgressOnStop = true;
		this.multitoolContext = "disinfect";
		this.multitoolHitEffectTag = "fx_disinfect_splash";
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x0007CC18 File Offset: 0x0007AE18
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<AutoDisinfectable>(493375141, AutoDisinfectable.OnRefreshUserMenuDelegate);
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		base.SetWorkTime(10f);
		this.shouldTransferDiseaseWithWorker = false;
	}

	// Token: 0x060017CF RID: 6095 RVA: 0x0007CC93 File Offset: 0x0007AE93
	public void CancelChore()
	{
		if (this.chore != null)
		{
			this.chore.Cancel("AutoDisinfectable.CancelChore");
			this.chore = null;
		}
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x0007CCB4 File Offset: 0x0007AEB4
	public void RefreshChore()
	{
		if (KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		if (!this.enableAutoDisinfect || !SaveGame.Instance.enableAutoDisinfect)
		{
			if (this.chore != null)
			{
				this.chore.Cancel("Autodisinfect Disabled");
				this.chore = null;
				return;
			}
		}
		else if (this.chore == null || !(this.chore.driver != null))
		{
			int diseaseCount = this.primaryElement.DiseaseCount;
			if (this.chore == null && diseaseCount > SaveGame.Instance.minGermCountForDisinfect)
			{
				this.chore = new WorkChore<AutoDisinfectable>(Db.Get().ChoreTypes.Disinfect, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
				return;
			}
			if (diseaseCount < SaveGame.Instance.minGermCountForDisinfect && this.chore != null)
			{
				this.chore.Cancel("AutoDisinfectable.Update");
				this.chore = null;
			}
		}
	}

	// Token: 0x060017D1 RID: 6097 RVA: 0x0007CD95 File Offset: 0x0007AF95
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.diseasePerSecond = (float)base.GetComponent<PrimaryElement>().DiseaseCount / 10f;
	}

	// Token: 0x060017D2 RID: 6098 RVA: 0x0007CDB6 File Offset: 0x0007AFB6
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		base.OnWorkTick(worker, dt);
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		component.AddDisease(component.DiseaseIdx, -(int)(this.diseasePerSecond * dt + 0.5f), "Disinfectable.OnWorkTick");
		return false;
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x0007CDE8 File Offset: 0x0007AFE8
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		component.AddDisease(component.DiseaseIdx, -component.DiseaseCount, "Disinfectable.OnCompleteWork");
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.MarkedForDisinfection, this);
		this.chore = null;
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x0007CE58 File Offset: 0x0007B058
	private void EnableAutoDisinfect()
	{
		this.enableAutoDisinfect = true;
		this.RefreshChore();
	}

	// Token: 0x060017D5 RID: 6101 RVA: 0x0007CE67 File Offset: 0x0007B067
	private void DisableAutoDisinfect()
	{
		this.enableAutoDisinfect = false;
		this.RefreshChore();
	}

	// Token: 0x060017D6 RID: 6102 RVA: 0x0007CE78 File Offset: 0x0007B078
	private void OnRefreshUserMenu(object data)
	{
		KIconButtonMenu.ButtonInfo buttonInfo;
		if (!this.enableAutoDisinfect)
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_disinfect", STRINGS.BUILDINGS.AUTODISINFECTABLE.ENABLE_AUTODISINFECT.NAME, new System.Action(this.EnableAutoDisinfect), global::Action.NumActions, null, null, null, STRINGS.BUILDINGS.AUTODISINFECTABLE.ENABLE_AUTODISINFECT.TOOLTIP, true);
		}
		else
		{
			buttonInfo = new KIconButtonMenu.ButtonInfo("action_disinfect", STRINGS.BUILDINGS.AUTODISINFECTABLE.DISABLE_AUTODISINFECT.NAME, new System.Action(this.DisableAutoDisinfect), global::Action.NumActions, null, null, null, STRINGS.BUILDINGS.AUTODISINFECTABLE.DISABLE_AUTODISINFECT.TOOLTIP, true);
		}
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 10f);
	}

	// Token: 0x04000D31 RID: 3377
	private Chore chore;

	// Token: 0x04000D32 RID: 3378
	private const float MAX_WORK_TIME = 10f;

	// Token: 0x04000D33 RID: 3379
	private float diseasePerSecond;

	// Token: 0x04000D34 RID: 3380
	[MyCmpGet]
	private PrimaryElement primaryElement;

	// Token: 0x04000D35 RID: 3381
	[Serialize]
	private bool enableAutoDisinfect = true;

	// Token: 0x04000D36 RID: 3382
	private static readonly EventSystem.IntraObjectHandler<AutoDisinfectable> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<AutoDisinfectable>(delegate(AutoDisinfectable component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
