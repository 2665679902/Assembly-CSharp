using System;

namespace ProcGen
{
	// Token: 0x020004C2 RID: 1218
	[Serializable]
	public class MobReference
	{
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x00071E58 File Offset: 0x00070058
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x00071E60 File Offset: 0x00070060
		public string type { get; private set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x00071E69 File Offset: 0x00070069
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x00071E71 File Offset: 0x00070071
		public MinMax count { get; private set; }
	}
}
