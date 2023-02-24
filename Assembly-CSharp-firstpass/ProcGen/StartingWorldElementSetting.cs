using System;

namespace ProcGen
{
	// Token: 0x020004E2 RID: 1250
	[Serializable]
	public class StartingWorldElementSetting
	{
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060035DE RID: 13790 RVA: 0x00076425 File Offset: 0x00074625
		// (set) Token: 0x060035DF RID: 13791 RVA: 0x0007642D File Offset: 0x0007462D
		public string element { get; private set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x00076436 File Offset: 0x00074636
		// (set) Token: 0x060035E1 RID: 13793 RVA: 0x0007643E File Offset: 0x0007463E
		public float amount { get; private set; }
	}
}
