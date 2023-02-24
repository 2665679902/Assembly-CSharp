using System;
using UnityEngine;

// Token: 0x020005DB RID: 1499
[AddComponentMenu("KMonoBehaviour/Workable/LiquidCooledFanWorkable")]
public class LiquidCooledFanWorkable : Workable
{
	// Token: 0x06002574 RID: 9588 RVA: 0x000CA58B File Offset: 0x000C878B
	private LiquidCooledFanWorkable()
	{
		this.showProgressBar = false;
	}

	// Token: 0x06002575 RID: 9589 RVA: 0x000CA59A File Offset: 0x000C879A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = null;
	}

	// Token: 0x06002576 RID: 9590 RVA: 0x000CA5A9 File Offset: 0x000C87A9
	protected override void OnSpawn()
	{
		GameScheduler.Instance.Schedule("InsulationTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, true);
		}, null, null);
		base.OnSpawn();
	}

	// Token: 0x06002577 RID: 9591 RVA: 0x000CA5E7 File Offset: 0x000C87E7
	protected override void OnStartWork(Worker worker)
	{
		this.operational.SetActive(true, false);
	}

	// Token: 0x06002578 RID: 9592 RVA: 0x000CA5F6 File Offset: 0x000C87F6
	protected override void OnStopWork(Worker worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x06002579 RID: 9593 RVA: 0x000CA605 File Offset: 0x000C8805
	protected override void OnCompleteWork(Worker worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x040015D1 RID: 5585
	[MyCmpGet]
	private Operational operational;
}
