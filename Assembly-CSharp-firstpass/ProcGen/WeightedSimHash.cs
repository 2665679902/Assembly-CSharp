using System;

namespace ProcGen
{
	// Token: 0x020004C8 RID: 1224
	[Serializable]
	public class WeightedSimHash : IWeighted
	{
		// Token: 0x0600348A RID: 13450 RVA: 0x00072B0E File Offset: 0x00070D0E
		public WeightedSimHash()
		{
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x00072B16 File Offset: 0x00070D16
		public WeightedSimHash(string elementHash, float weight, SampleDescriber.Override overrides = null)
		{
			this.element = elementHash;
			this.weight = weight;
			this.overrides = overrides;
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600348C RID: 13452 RVA: 0x00072B33 File Offset: 0x00070D33
		// (set) Token: 0x0600348D RID: 13453 RVA: 0x00072B3B File Offset: 0x00070D3B
		public string element { get; private set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x00072B44 File Offset: 0x00070D44
		// (set) Token: 0x0600348F RID: 13455 RVA: 0x00072B4C File Offset: 0x00070D4C
		public float weight { get; set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x00072B55 File Offset: 0x00070D55
		// (set) Token: 0x06003491 RID: 13457 RVA: 0x00072B5D File Offset: 0x00070D5D
		public SampleDescriber.Override overrides { get; private set; }
	}
}
