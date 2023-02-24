using System;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
[AddComponentMenu("KMonoBehaviour/Workable/Studyable")]
public class Studyable : Workable, ISidescreenButtonControl
{
	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0009CFB5 File Offset: 0x0009B1B5
	public bool Studied
	{
		get
		{
			return this.studied;
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06001D61 RID: 7521 RVA: 0x0009CFBD File Offset: 0x0009B1BD
	public bool Studying
	{
		get
		{
			return this.chore != null && this.chore.InProgress();
		}
	}

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0009CFD4 File Offset: 0x0009B1D4
	public string SidescreenTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.TITLE";
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0009CFDB File Offset: 0x0009B1DB
	public string SidescreenStatusMessage
	{
		get
		{
			if (this.studied)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.STUDIED_STATUS;
			}
			if (this.markedForStudy)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.PENDING_STATUS;
			}
			return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.SEND_STATUS;
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0009D00D File Offset: 0x0009B20D
	public string SidescreenButtonText
	{
		get
		{
			if (this.studied)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.STUDIED_BUTTON;
			}
			if (this.markedForStudy)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.PENDING_BUTTON;
			}
			return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.SEND_BUTTON;
		}
	}

	// Token: 0x1700014B RID: 331
	// (get) Token: 0x06001D65 RID: 7525 RVA: 0x0009D03F File Offset: 0x0009B23F
	public string SidescreenButtonTooltip
	{
		get
		{
			if (this.studied)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.STUDIED_STATUS;
			}
			if (this.markedForStudy)
			{
				return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.PENDING_STATUS;
			}
			return UI.UISIDESCREENS.STUDYABLE_SIDE_SCREEN.SEND_STATUS;
		}
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x0009D071 File Offset: 0x0009B271
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x0009D078 File Offset: 0x0009B278
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x0009D07B File Offset: 0x0009B27B
	public bool SidescreenButtonInteractable()
	{
		return !this.studied;
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x0009D088 File Offset: 0x0009B288
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_machine_kanim") };
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Studying;
		this.resetProgressOnStop = false;
		this.requiredSkillPerk = Db.Get().SkillPerks.CanStudyWorldObjects.Id;
		this.attributeConverter = Db.Get().AttributeConverters.ResearchSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Research.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		base.SetWorkTime(3600f);
	}

	// Token: 0x06001D6A RID: 7530 RVA: 0x0009D150 File Offset: 0x0009B350
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.studiedIndicator = new MeterController(base.GetComponent<KBatchedAnimController>(), this.meterTrackerSymbol, this.meterAnim, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { this.meterTrackerSymbol });
		this.studiedIndicator.meterController.gameObject.AddComponent<LoopingSounds>();
		this.Refresh();
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x0009D1AE File Offset: 0x0009B3AE
	public void CancelChore()
	{
		if (this.chore != null)
		{
			this.chore.Cancel("Studyable.CancelChore");
			this.chore = null;
			base.Trigger(1488501379, null);
		}
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x0009D1DC File Offset: 0x0009B3DC
	public void Refresh()
	{
		if (KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.studied)
		{
			this.statusItemGuid = component.ReplaceStatusItem(this.statusItemGuid, Db.Get().MiscStatusItems.Studied, null);
			this.studiedIndicator.gameObject.SetActive(true);
			this.studiedIndicator.meterController.Play(this.meterAnim, KAnim.PlayMode.Loop, 1f, 0f);
			this.requiredSkillPerk = null;
			this.UpdateStatusItem(null);
			return;
		}
		if (this.markedForStudy)
		{
			if (this.chore == null)
			{
				this.chore = new WorkChore<Studyable>(Db.Get().ChoreTypes.Research, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			}
			this.statusItemGuid = component.ReplaceStatusItem(this.statusItemGuid, Db.Get().MiscStatusItems.AwaitingStudy, null);
		}
		else
		{
			this.CancelChore();
			this.statusItemGuid = component.RemoveStatusItem(this.statusItemGuid, false);
		}
		this.studiedIndicator.gameObject.SetActive(false);
	}

	// Token: 0x06001D6D RID: 7533 RVA: 0x0009D2F4 File Offset: 0x0009B4F4
	private void ToggleStudyChore()
	{
		if (DebugHandler.InstantBuildMode)
		{
			this.studied = true;
			if (this.chore != null)
			{
				this.chore.Cancel("debug");
				this.chore = null;
			}
			base.Trigger(-1436775550, null);
		}
		else
		{
			this.markedForStudy = !this.markedForStudy;
		}
		this.Refresh();
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x0009D351 File Offset: 0x0009B551
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		this.studied = true;
		this.chore = null;
		this.Refresh();
		base.Trigger(-1436775550, null);
		if (DlcManager.IsExpansion1Active())
		{
			this.DropDatabanks();
		}
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x0009D388 File Offset: 0x0009B588
	private void DropDatabanks()
	{
		int num = UnityEngine.Random.Range(7, 13);
		for (int i = 0; i <= num; i++)
		{
			GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("OrbitalResearchDatabank"), base.transform.position + new Vector3(0f, 1f, 0f), Grid.SceneLayer.Ore, null, 0);
			gameObject.GetComponent<PrimaryElement>().Temperature = 298.15f;
			gameObject.SetActive(true);
		}
	}

	// Token: 0x06001D70 RID: 7536 RVA: 0x0009D3FC File Offset: 0x0009B5FC
	public void OnSidescreenButtonPressed()
	{
		this.ToggleStudyChore();
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x0009D404 File Offset: 0x0009B604
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x04001098 RID: 4248
	public string meterTrackerSymbol;

	// Token: 0x04001099 RID: 4249
	public string meterAnim;

	// Token: 0x0400109A RID: 4250
	private Chore chore;

	// Token: 0x0400109B RID: 4251
	private const float STUDY_WORK_TIME = 3600f;

	// Token: 0x0400109C RID: 4252
	[Serialize]
	private bool studied;

	// Token: 0x0400109D RID: 4253
	[Serialize]
	private bool markedForStudy;

	// Token: 0x0400109E RID: 4254
	private Guid statusItemGuid;

	// Token: 0x0400109F RID: 4255
	private Guid additionalStatusItemGuid;

	// Token: 0x040010A0 RID: 4256
	public MeterController studiedIndicator;
}
