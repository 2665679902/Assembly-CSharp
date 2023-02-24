using System;

namespace Klei
{
	// Token: 0x02000D51 RID: 3409
	public struct SolidInfo
	{
		// Token: 0x06006850 RID: 26704 RVA: 0x0028A525 File Offset: 0x00288725
		public SolidInfo(int cellIdx, bool isSolid)
		{
			this.cellIdx = cellIdx;
			this.isSolid = isSolid;
		}

		// Token: 0x04004E76 RID: 20086
		public int cellIdx;

		// Token: 0x04004E77 RID: 20087
		public bool isSolid;
	}
}
