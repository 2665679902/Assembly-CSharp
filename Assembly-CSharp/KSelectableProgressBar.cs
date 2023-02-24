using System;

// Token: 0x02000493 RID: 1171
public class KSelectableProgressBar : KSelectable
{
	// Token: 0x06001A4B RID: 6731 RVA: 0x0008C1DC File Offset: 0x0008A3DC
	public override string GetName()
	{
		int num = (int)(this.progressBar.PercentFull * (float)this.scaleAmount);
		return string.Format("{0} {1}/{2}", this.entityName, num, this.scaleAmount);
	}

	// Token: 0x04000EA1 RID: 3745
	[MyCmpGet]
	private ProgressBar progressBar;

	// Token: 0x04000EA2 RID: 3746
	private int scaleAmount = 100;
}
