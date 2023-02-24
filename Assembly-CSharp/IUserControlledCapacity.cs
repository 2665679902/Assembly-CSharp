using System;

// Token: 0x02000B9A RID: 2970
public interface IUserControlledCapacity
{
	// Token: 0x17000686 RID: 1670
	// (get) Token: 0x06005D60 RID: 23904
	// (set) Token: 0x06005D61 RID: 23905
	float UserMaxCapacity { get; set; }

	// Token: 0x17000687 RID: 1671
	// (get) Token: 0x06005D62 RID: 23906
	float AmountStored { get; }

	// Token: 0x17000688 RID: 1672
	// (get) Token: 0x06005D63 RID: 23907
	float MinCapacity { get; }

	// Token: 0x17000689 RID: 1673
	// (get) Token: 0x06005D64 RID: 23908
	float MaxCapacity { get; }

	// Token: 0x1700068A RID: 1674
	// (get) Token: 0x06005D65 RID: 23909
	bool WholeValues { get; }

	// Token: 0x1700068B RID: 1675
	// (get) Token: 0x06005D66 RID: 23910
	LocString CapacityUnits { get; }
}
