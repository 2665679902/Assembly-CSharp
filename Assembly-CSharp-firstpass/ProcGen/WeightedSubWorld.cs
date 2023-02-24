using System;

namespace ProcGen
{
	// Token: 0x020004DA RID: 1242
	public class WeightedSubWorld : IWeighted
	{
		// Token: 0x06003558 RID: 13656 RVA: 0x000752C9 File Offset: 0x000734C9
		public WeightedSubWorld(float weight, SubWorld subWorld, float overridePower = -1f, int minCount = 0, int maxCount = 2147483647, int priority = 0)
		{
			this.weight = weight;
			this.subWorld = subWorld;
			this.overridePower = overridePower;
			this.minCount = minCount;
			this.maxCount = maxCount;
			this.priority = priority;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000752FE File Offset: 0x000734FE
		// (set) Token: 0x0600355A RID: 13658 RVA: 0x00075306 File Offset: 0x00073506
		public SubWorld subWorld { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x0007530F File Offset: 0x0007350F
		// (set) Token: 0x0600355C RID: 13660 RVA: 0x00075317 File Offset: 0x00073517
		public float weight { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x0600355D RID: 13661 RVA: 0x00075320 File Offset: 0x00073520
		// (set) Token: 0x0600355E RID: 13662 RVA: 0x00075328 File Offset: 0x00073528
		public float overridePower { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600355F RID: 13663 RVA: 0x00075331 File Offset: 0x00073531
		// (set) Token: 0x06003560 RID: 13664 RVA: 0x00075339 File Offset: 0x00073539
		public int minCount { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x00075342 File Offset: 0x00073542
		// (set) Token: 0x06003562 RID: 13666 RVA: 0x0007534A File Offset: 0x0007354A
		public int maxCount { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06003563 RID: 13667 RVA: 0x00075353 File Offset: 0x00073553
		// (set) Token: 0x06003564 RID: 13668 RVA: 0x0007535B File Offset: 0x0007355B
		public int priority { get; set; }

		// Token: 0x06003565 RID: 13669 RVA: 0x00075364 File Offset: 0x00073564
		public override int GetHashCode()
		{
			return this.subWorld.GetHashCode();
		}
	}
}
