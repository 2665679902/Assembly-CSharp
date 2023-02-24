using System;

// Token: 0x02000B01 RID: 2817
public interface IConsumableUIItem
{
	// Token: 0x17000644 RID: 1604
	// (get) Token: 0x0600566B RID: 22123
	string ConsumableId { get; }

	// Token: 0x17000645 RID: 1605
	// (get) Token: 0x0600566C RID: 22124
	string ConsumableName { get; }

	// Token: 0x17000646 RID: 1606
	// (get) Token: 0x0600566D RID: 22125
	int MajorOrder { get; }

	// Token: 0x17000647 RID: 1607
	// (get) Token: 0x0600566E RID: 22126
	int MinorOrder { get; }

	// Token: 0x17000648 RID: 1608
	// (get) Token: 0x0600566F RID: 22127
	bool Display { get; }
}
