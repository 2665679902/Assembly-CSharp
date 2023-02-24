using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000577 RID: 1399
[AddComponentMenu("KMonoBehaviour/Workable/AstronautTrainingCenter")]
public class AstronautTrainingCenter : Workable
{
	// Token: 0x060021CE RID: 8654 RVA: 0x000B8145 File Offset: 0x000B6345
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.chore = this.CreateChore();
	}

	// Token: 0x060021CF RID: 8655 RVA: 0x000B815C File Offset: 0x000B635C
	private Chore CreateChore()
	{
		return new WorkChore<AstronautTrainingCenter>(Db.Get().ChoreTypes.Train, this, null, true, null, null, null, false, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x000B818F File Offset: 0x000B638F
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<Operational>().SetActive(true, false);
	}

	// Token: 0x060021D1 RID: 8657 RVA: 0x000B81A5 File Offset: 0x000B63A5
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		worker == null;
		return true;
	}

	// Token: 0x060021D2 RID: 8658 RVA: 0x000B81B0 File Offset: 0x000B63B0
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		if (this.chore != null && !this.chore.isComplete)
		{
			this.chore.Cancel("completed but not complete??");
		}
		this.chore = this.CreateChore();
	}

	// Token: 0x060021D3 RID: 8659 RVA: 0x000B81EA File Offset: 0x000B63EA
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		base.GetComponent<Operational>().SetActive(false, false);
	}

	// Token: 0x060021D4 RID: 8660 RVA: 0x000B8200 File Offset: 0x000B6400
	public override float GetPercentComplete()
	{
		base.worker == null;
		return 0f;
	}

	// Token: 0x060021D5 RID: 8661 RVA: 0x000B8214 File Offset: 0x000B6414
	public AstronautTrainingCenter()
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "IsNotMarkedForDeconstruction";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_MARKED_FOR_DECONSTRUCTION;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Deconstructable deconstructable = data as Deconstructable;
			return deconstructable == null || !deconstructable.IsMarkedForDeconstruction();
		};
		this.IsNotMarkedForDeconstruction = precondition;
		base..ctor();
	}

	// Token: 0x0400137D RID: 4989
	public float daysToMasterRole;

	// Token: 0x0400137E RID: 4990
	private Chore chore;

	// Token: 0x0400137F RID: 4991
	public Chore.Precondition IsNotMarkedForDeconstruction;
}
