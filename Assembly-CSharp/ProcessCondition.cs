using System;

// Token: 0x020008A3 RID: 2211
public abstract class ProcessCondition
{
	// Token: 0x06003F8A RID: 16266
	public abstract ProcessCondition.Status EvaluateCondition();

	// Token: 0x06003F8B RID: 16267
	public abstract bool ShowInUI();

	// Token: 0x06003F8C RID: 16268
	public abstract string GetStatusMessage(ProcessCondition.Status status);

	// Token: 0x06003F8D RID: 16269 RVA: 0x00162BCA File Offset: 0x00160DCA
	public string GetStatusMessage()
	{
		return this.GetStatusMessage(this.EvaluateCondition());
	}

	// Token: 0x06003F8E RID: 16270
	public abstract string GetStatusTooltip(ProcessCondition.Status status);

	// Token: 0x06003F8F RID: 16271 RVA: 0x00162BD8 File Offset: 0x00160DD8
	public string GetStatusTooltip()
	{
		return this.GetStatusTooltip(this.EvaluateCondition());
	}

	// Token: 0x06003F90 RID: 16272 RVA: 0x00162BE6 File Offset: 0x00160DE6
	public virtual StatusItem GetStatusItem(ProcessCondition.Status status)
	{
		return null;
	}

	// Token: 0x06003F91 RID: 16273 RVA: 0x00162BE9 File Offset: 0x00160DE9
	public virtual ProcessCondition GetParentCondition()
	{
		return this.parentCondition;
	}

	// Token: 0x040029B4 RID: 10676
	protected ProcessCondition parentCondition;

	// Token: 0x02001676 RID: 5750
	public enum ProcessConditionType
	{
		// Token: 0x040069C5 RID: 27077
		RocketFlight,
		// Token: 0x040069C6 RID: 27078
		RocketPrep,
		// Token: 0x040069C7 RID: 27079
		RocketStorage,
		// Token: 0x040069C8 RID: 27080
		RocketBoard,
		// Token: 0x040069C9 RID: 27081
		All
	}

	// Token: 0x02001677 RID: 5751
	public enum Status
	{
		// Token: 0x040069CB RID: 27083
		Failure,
		// Token: 0x040069CC RID: 27084
		Warning,
		// Token: 0x040069CD RID: 27085
		Ready
	}
}
