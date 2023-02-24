using System;

// Token: 0x02000494 RID: 1172
public class KnockKnock : Activatable
{
	// Token: 0x06001A4D RID: 6733 RVA: 0x0008C22F File Offset: 0x0008A42F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = false;
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x0008C23E File Offset: 0x0008A43E
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (!this.doorAnswered)
		{
			this.workTimeRemaining += dt;
		}
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x0008C25E File Offset: 0x0008A45E
	public void AnswerDoor()
	{
		this.doorAnswered = true;
		this.workTimeRemaining = 1f;
	}

	// Token: 0x04000EA3 RID: 3747
	private bool doorAnswered;
}
