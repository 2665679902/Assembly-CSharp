using System;

namespace ProcGen
{
	// Token: 0x020004C6 RID: 1222
	[Serializable]
	public class WeightedName : IWeighted
	{
		// Token: 0x06003472 RID: 13426 RVA: 0x00072A07 File Offset: 0x00070C07
		public WeightedName()
		{
			this.weight = 1f;
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x00072A1A File Offset: 0x00070C1A
		public WeightedName(string name, float weight)
		{
			this.name = name;
			this.weight = weight;
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06003474 RID: 13428 RVA: 0x00072A30 File Offset: 0x00070C30
		// (set) Token: 0x06003475 RID: 13429 RVA: 0x00072A38 File Offset: 0x00070C38
		public string name { get; private set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x00072A41 File Offset: 0x00070C41
		// (set) Token: 0x06003477 RID: 13431 RVA: 0x00072A49 File Offset: 0x00070C49
		public string overrideName { get; private set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x00072A52 File Offset: 0x00070C52
		// (set) Token: 0x06003479 RID: 13433 RVA: 0x00072A5A File Offset: 0x00070C5A
		public float weight { get; set; }
	}
}
