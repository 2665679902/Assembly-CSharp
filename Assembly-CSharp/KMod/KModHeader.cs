using System;

namespace KMod
{
	// Token: 0x02000D08 RID: 3336
	public class KModHeader
	{
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x0600673E RID: 26430 RVA: 0x0027D97C File Offset: 0x0027BB7C
		// (set) Token: 0x0600673F RID: 26431 RVA: 0x0027D984 File Offset: 0x0027BB84
		public string staticID { get; set; }

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06006740 RID: 26432 RVA: 0x0027D98D File Offset: 0x0027BB8D
		// (set) Token: 0x06006741 RID: 26433 RVA: 0x0027D995 File Offset: 0x0027BB95
		public string title { get; set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06006742 RID: 26434 RVA: 0x0027D99E File Offset: 0x0027BB9E
		// (set) Token: 0x06006743 RID: 26435 RVA: 0x0027D9A6 File Offset: 0x0027BBA6
		public string description { get; set; }
	}
}
