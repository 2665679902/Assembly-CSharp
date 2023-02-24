using System;
using KSerialization;
using TUNING;
using UnityEngine;

// Token: 0x02000475 RID: 1141
[AddComponentMenu("KMonoBehaviour/Workable/Disinfectable")]
public class Disinfectable : Workable
{
	// Token: 0x06001967 RID: 6503 RVA: 0x000885AC File Offset: 0x000867AC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsetTable(OffsetGroups.InvertedStandardTableWithCorners);
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Disinfecting;
		this.attributeConverter = Db.Get().AttributeConverters.TidyingSpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Basekeeping.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		this.multitoolContext = "disinfect";
		this.multitoolHitEffectTag = "fx_disinfect_splash";
		base.Subscribe<Disinfectable>(2127324410, Disinfectable.OnCancelDelegate);
	}

	// Token: 0x06001968 RID: 6504 RVA: 0x00088663 File Offset: 0x00086863
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.isMarkedForDisinfect)
		{
			this.MarkForDisinfect(true);
		}
		base.SetWorkTime(10f);
		this.shouldTransferDiseaseWithWorker = false;
	}

	// Token: 0x06001969 RID: 6505 RVA: 0x0008868C File Offset: 0x0008688C
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.diseasePerSecond = (float)base.GetComponent<PrimaryElement>().DiseaseCount / 10f;
	}

	// Token: 0x0600196A RID: 6506 RVA: 0x000886AD File Offset: 0x000868AD
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		base.OnWorkTick(worker, dt);
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		component.AddDisease(component.DiseaseIdx, -(int)(this.diseasePerSecond * dt + 0.5f), "Disinfectable.OnWorkTick");
		return false;
	}

	// Token: 0x0600196B RID: 6507 RVA: 0x000886E0 File Offset: 0x000868E0
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		component.AddDisease(component.DiseaseIdx, -component.DiseaseCount, "Disinfectable.OnCompleteWork");
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.MarkedForDisinfection, this);
		this.isMarkedForDisinfect = false;
		this.chore = null;
		Game.Instance.userMenu.Refresh(base.gameObject);
		Prioritizable.RemoveRef(base.gameObject);
	}

	// Token: 0x0600196C RID: 6508 RVA: 0x00088762 File Offset: 0x00086962
	private void ToggleMarkForDisinfect()
	{
		if (this.isMarkedForDisinfect)
		{
			this.CancelDisinfection();
			return;
		}
		base.SetWorkTime(10f);
		this.MarkForDisinfect(false);
	}

	// Token: 0x0600196D RID: 6509 RVA: 0x00088788 File Offset: 0x00086988
	private void CancelDisinfection()
	{
		if (this.isMarkedForDisinfect)
		{
			Prioritizable.RemoveRef(base.gameObject);
			base.ShowProgressBar(false);
			this.isMarkedForDisinfect = false;
			this.chore.Cancel("disinfection cancelled");
			this.chore = null;
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.MarkedForDisinfection, this);
		}
	}

	// Token: 0x0600196E RID: 6510 RVA: 0x000887F0 File Offset: 0x000869F0
	public void MarkForDisinfect(bool force = false)
	{
		if (!this.isMarkedForDisinfect || force)
		{
			this.isMarkedForDisinfect = true;
			Prioritizable.AddRef(base.gameObject);
			this.chore = new WorkChore<Disinfectable>(Db.Get().ChoreTypes.Disinfect, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
			base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.MarkedForDisinfection, this);
		}
	}

	// Token: 0x0600196F RID: 6511 RVA: 0x00088864 File Offset: 0x00086A64
	private void OnCancel(object data)
	{
		this.CancelDisinfection();
	}

	// Token: 0x04000E33 RID: 3635
	private Chore chore;

	// Token: 0x04000E34 RID: 3636
	[Serialize]
	private bool isMarkedForDisinfect;

	// Token: 0x04000E35 RID: 3637
	private const float MAX_WORK_TIME = 10f;

	// Token: 0x04000E36 RID: 3638
	private float diseasePerSecond;

	// Token: 0x04000E37 RID: 3639
	private static readonly EventSystem.IntraObjectHandler<Disinfectable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Disinfectable>(delegate(Disinfectable component, object data)
	{
		component.OnCancel(data);
	});
}
