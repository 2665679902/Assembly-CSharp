using System;

// Token: 0x0200088B RID: 2187
public struct PlantElementAbsorber
{
	// Token: 0x06003EB9 RID: 16057 RVA: 0x0015EA11 File Offset: 0x0015CC11
	public void Clear()
	{
		this.storage = null;
		this.consumedElements = null;
	}

	// Token: 0x0400290B RID: 10507
	public Storage storage;

	// Token: 0x0400290C RID: 10508
	public PlantElementAbsorber.LocalInfo localInfo;

	// Token: 0x0400290D RID: 10509
	public HandleVector<int>.Handle[] accumulators;

	// Token: 0x0400290E RID: 10510
	public PlantElementAbsorber.ConsumeInfo[] consumedElements;

	// Token: 0x02001645 RID: 5701
	public struct ConsumeInfo
	{
		// Token: 0x06008731 RID: 34609 RVA: 0x002F1217 File Offset: 0x002EF417
		public ConsumeInfo(Tag tag, float mass_consumption_rate)
		{
			this.tag = tag;
			this.massConsumptionRate = mass_consumption_rate;
		}

		// Token: 0x0400694E RID: 26958
		public Tag tag;

		// Token: 0x0400694F RID: 26959
		public float massConsumptionRate;
	}

	// Token: 0x02001646 RID: 5702
	public struct LocalInfo
	{
		// Token: 0x04006950 RID: 26960
		public Tag tag;

		// Token: 0x04006951 RID: 26961
		public float massConsumptionRate;
	}
}
