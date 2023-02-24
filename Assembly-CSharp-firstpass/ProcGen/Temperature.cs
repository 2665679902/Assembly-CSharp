using System;

namespace ProcGen
{
	// Token: 0x020004CE RID: 1230
	public class Temperature
	{
		// Token: 0x060034BA RID: 13498 RVA: 0x00072D80 File Offset: 0x00070F80
		public Temperature()
		{
			this.min = 0f;
			this.max = 0f;
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x00072D9E File Offset: 0x00070F9E
		// (set) Token: 0x060034BC RID: 13500 RVA: 0x00072DA6 File Offset: 0x00070FA6
		public float min { get; private set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x00072DAF File Offset: 0x00070FAF
		// (set) Token: 0x060034BE RID: 13502 RVA: 0x00072DB7 File Offset: 0x00070FB7
		public float max { get; private set; }

		// Token: 0x02000AFA RID: 2810
		public enum Range
		{
			// Token: 0x0400259A RID: 9626
			ExtremelyCold,
			// Token: 0x0400259B RID: 9627
			VeryCold,
			// Token: 0x0400259C RID: 9628
			Cold,
			// Token: 0x0400259D RID: 9629
			Chilly,
			// Token: 0x0400259E RID: 9630
			Cool,
			// Token: 0x0400259F RID: 9631
			Mild,
			// Token: 0x040025A0 RID: 9632
			Room,
			// Token: 0x040025A1 RID: 9633
			HumanWarm,
			// Token: 0x040025A2 RID: 9634
			HumanHot,
			// Token: 0x040025A3 RID: 9635
			Hot,
			// Token: 0x040025A4 RID: 9636
			VeryHot,
			// Token: 0x040025A5 RID: 9637
			ExtremelyHot
		}
	}
}
