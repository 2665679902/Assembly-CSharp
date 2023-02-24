using System;
using System.Collections.Generic;

namespace ProcGen
{
	// Token: 0x020004CD RID: 1229
	[Serializable]
	public class Feature
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x00072D2F File Offset: 0x00070F2F
		// (set) Token: 0x060034B4 RID: 13492 RVA: 0x00072D37 File Offset: 0x00070F37
		public string type { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x00072D40 File Offset: 0x00070F40
		// (set) Token: 0x060034B6 RID: 13494 RVA: 0x00072D48 File Offset: 0x00070F48
		public List<string> tags { get; private set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x00072D51 File Offset: 0x00070F51
		// (set) Token: 0x060034B8 RID: 13496 RVA: 0x00072D59 File Offset: 0x00070F59
		public List<string> excludesTags { get; private set; }

		// Token: 0x060034B9 RID: 13497 RVA: 0x00072D62 File Offset: 0x00070F62
		public Feature()
		{
			this.tags = new List<string>();
			this.excludesTags = new List<string>();
		}
	}
}
