using System;
using System.Collections.Generic;

namespace ProcGen
{
	// Token: 0x020004D0 RID: 1232
	[Serializable]
	public class WeightedBiome : IWeighted
	{
		// Token: 0x060034C5 RID: 13509 RVA: 0x00072E00 File Offset: 0x00071000
		public WeightedBiome()
		{
			this.tags = new List<string>();
		}

		// Token: 0x060034C6 RID: 13510 RVA: 0x00072E13 File Offset: 0x00071013
		public WeightedBiome(string name, float weight)
			: this()
		{
			this.name = name;
			this.weight = weight;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x00072E29 File Offset: 0x00071029
		// (set) Token: 0x060034C8 RID: 13512 RVA: 0x00072E31 File Offset: 0x00071031
		public string name { get; private set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x00072E3A File Offset: 0x0007103A
		// (set) Token: 0x060034CA RID: 13514 RVA: 0x00072E42 File Offset: 0x00071042
		public float weight { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x00072E4B File Offset: 0x0007104B
		// (set) Token: 0x060034CC RID: 13516 RVA: 0x00072E53 File Offset: 0x00071053
		public List<string> tags { get; private set; }
	}
}
