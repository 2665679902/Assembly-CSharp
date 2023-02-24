using System;

// Token: 0x02000492 RID: 1170
public class KSelectableHealthBar : KSelectable
{
	// Token: 0x06001A49 RID: 6729 RVA: 0x0008C188 File Offset: 0x0008A388
	public override string GetName()
	{
		int num = (int)(this.progressBar.PercentFull * (float)this.scaleAmount);
		return string.Format("{0} {1}/{2}", this.entityName, num, this.scaleAmount);
	}

	// Token: 0x04000E9F RID: 3743
	[MyCmpGet]
	private ProgressBar progressBar;

	// Token: 0x04000EA0 RID: 3744
	private int scaleAmount = 100;
}
