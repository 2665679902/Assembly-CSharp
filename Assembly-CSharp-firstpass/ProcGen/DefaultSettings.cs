using System;
using System.Collections.Generic;

namespace ProcGen
{
	// Token: 0x020004E1 RID: 1249
	[Serializable]
	public class DefaultSettings
	{
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x000763C8 File Offset: 0x000745C8
		// (set) Token: 0x060035D4 RID: 13780 RVA: 0x000763D0 File Offset: 0x000745D0
		public BaseLocation baseData { get; private set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060035D5 RID: 13781 RVA: 0x000763D9 File Offset: 0x000745D9
		// (set) Token: 0x060035D6 RID: 13782 RVA: 0x000763E1 File Offset: 0x000745E1
		public Dictionary<string, object> data { get; private set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000763EA File Offset: 0x000745EA
		// (set) Token: 0x060035D8 RID: 13784 RVA: 0x000763F2 File Offset: 0x000745F2
		public List<string> defaultMoveTags { get; private set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000763FB File Offset: 0x000745FB
		// (set) Token: 0x060035DA RID: 13786 RVA: 0x00076403 File Offset: 0x00074603
		public List<string> overworldAddTags { get; private set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060035DB RID: 13787 RVA: 0x0007640C File Offset: 0x0007460C
		// (set) Token: 0x060035DC RID: 13788 RVA: 0x00076414 File Offset: 0x00074614
		public List<StartingWorldElementSetting> startingWorldElements { get; private set; }
	}
}
