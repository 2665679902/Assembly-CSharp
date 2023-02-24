using System;
using System.Collections.Generic;

namespace ProcGen
{
	// Token: 0x020004E4 RID: 1252
	public class SpaceMapPOIPlacement
	{
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x00076544 File Offset: 0x00074744
		// (set) Token: 0x060035EC RID: 13804 RVA: 0x0007654C File Offset: 0x0007474C
		public List<string> pois { get; private set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x00076555 File Offset: 0x00074755
		// (set) Token: 0x060035EE RID: 13806 RVA: 0x0007655D File Offset: 0x0007475D
		public int numToSpawn { get; set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x00076566 File Offset: 0x00074766
		// (set) Token: 0x060035F0 RID: 13808 RVA: 0x0007656E File Offset: 0x0007476E
		public MinMaxI allowedRings { get; set; }

		// Token: 0x060035F1 RID: 13809 RVA: 0x00076577 File Offset: 0x00074777
		public SpaceMapPOIPlacement()
		{
			this.allowedRings = new MinMaxI(0, 9999);
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060035F2 RID: 13810 RVA: 0x00076590 File Offset: 0x00074790
		// (set) Token: 0x060035F3 RID: 13811 RVA: 0x00076598 File Offset: 0x00074798
		public bool avoidClumping { get; set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060035F4 RID: 13812 RVA: 0x000765A1 File Offset: 0x000747A1
		// (set) Token: 0x060035F5 RID: 13813 RVA: 0x000765A9 File Offset: 0x000747A9
		public bool canSpawnDuplicates { get; set; }
	}
}
