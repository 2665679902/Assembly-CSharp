using System;
using TUNING;
using UnityEngine;

// Token: 0x020005D2 RID: 1490
[AddComponentMenu("KMonoBehaviour/Workable/IceCooledFanWorkable")]
public class IceCooledFanWorkable : Workable
{
	// Token: 0x0600252F RID: 9519 RVA: 0x000C8EAF File Offset: 0x000C70AF
	private IceCooledFanWorkable()
	{
		this.showProgressBar = false;
	}

	// Token: 0x06002530 RID: 9520 RVA: 0x000C8EC0 File Offset: 0x000C70C0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
		this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
		this.workerStatusItem = null;
	}

	// Token: 0x06002531 RID: 9521 RVA: 0x000C8F1F File Offset: 0x000C711F
	protected override void OnSpawn()
	{
		GameScheduler.Instance.Schedule("InsulationTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, true);
		}, null, null);
		base.OnSpawn();
	}

	// Token: 0x06002532 RID: 9522 RVA: 0x000C8F5D File Offset: 0x000C715D
	protected override void OnStartWork(Worker worker)
	{
		this.operational.SetActive(true, false);
	}

	// Token: 0x06002533 RID: 9523 RVA: 0x000C8F6C File Offset: 0x000C716C
	protected override void OnStopWork(Worker worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x06002534 RID: 9524 RVA: 0x000C8F7B File Offset: 0x000C717B
	protected override void OnCompleteWork(Worker worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x0400158A RID: 5514
	[MyCmpGet]
	private Operational operational;
}
