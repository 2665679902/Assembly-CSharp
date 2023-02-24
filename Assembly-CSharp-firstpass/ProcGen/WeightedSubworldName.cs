using System;

namespace ProcGen
{
	// Token: 0x020004C7 RID: 1223
	[Serializable]
	public class WeightedSubworldName : IWeighted
	{
		// Token: 0x0600347A RID: 13434 RVA: 0x00072A63 File Offset: 0x00070C63
		public WeightedSubworldName()
		{
			this.weight = 1f;
			this.maxCount = int.MaxValue;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x00072A81 File Offset: 0x00070C81
		public WeightedSubworldName(string name, float weight)
		{
			this.name = name;
			this.weight = weight;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x00072A97 File Offset: 0x00070C97
		// (set) Token: 0x0600347D RID: 13437 RVA: 0x00072A9F File Offset: 0x00070C9F
		public string name { get; private set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x00072AA8 File Offset: 0x00070CA8
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x00072AB0 File Offset: 0x00070CB0
		public string overrideName { get; private set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x00072AB9 File Offset: 0x00070CB9
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x00072AC1 File Offset: 0x00070CC1
		public float overridePower { get; private set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x00072ACA File Offset: 0x00070CCA
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x00072AD2 File Offset: 0x00070CD2
		public float weight { get; set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x00072ADB File Offset: 0x00070CDB
		// (set) Token: 0x06003485 RID: 13445 RVA: 0x00072AE3 File Offset: 0x00070CE3
		public int minCount { get; set; }

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06003486 RID: 13446 RVA: 0x00072AEC File Offset: 0x00070CEC
		// (set) Token: 0x06003487 RID: 13447 RVA: 0x00072AF4 File Offset: 0x00070CF4
		public int maxCount { get; set; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06003488 RID: 13448 RVA: 0x00072AFD File Offset: 0x00070CFD
		// (set) Token: 0x06003489 RID: 13449 RVA: 0x00072B05 File Offset: 0x00070D05
		public int priority { get; set; }
	}
}
