using System;

// Token: 0x020001F6 RID: 502
public class LimitValveTuning
{
	// Token: 0x060009ED RID: 2541 RVA: 0x00039562 File Offset: 0x00037762
	public static NonLinearSlider.Range[] GetDefaultSlider()
	{
		return new NonLinearSlider.Range[]
		{
			new NonLinearSlider.Range(70f, 100f),
			new NonLinearSlider.Range(30f, 500f)
		};
	}

	// Token: 0x0400060C RID: 1548
	public const float MAX_LIMIT = 500f;

	// Token: 0x0400060D RID: 1549
	public const float DEFAULT_LIMIT = 100f;
}
