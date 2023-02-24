using System;

namespace ProcGen
{
	// Token: 0x020004CF RID: 1231
	public class WeightedMob : IWeighted
	{
		// Token: 0x060034BF RID: 13503 RVA: 0x00072DC0 File Offset: 0x00070FC0
		public WeightedMob()
		{
		}

		// Token: 0x060034C0 RID: 13504 RVA: 0x00072DC8 File Offset: 0x00070FC8
		public WeightedMob(string tag, float weight)
		{
			this.tag = tag;
			this.weight = weight;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x00072DDE File Offset: 0x00070FDE
		// (set) Token: 0x060034C2 RID: 13506 RVA: 0x00072DE6 File Offset: 0x00070FE6
		public float weight { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x00072DEF File Offset: 0x00070FEF
		// (set) Token: 0x060034C4 RID: 13508 RVA: 0x00072DF7 File Offset: 0x00070FF7
		public string tag { get; private set; }
	}
}
