using System;
using System.Collections.Generic;

// Token: 0x02000773 RID: 1907
public interface IFetchList
{
	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x06003452 RID: 13394
	Storage Destination { get; }

	// Token: 0x06003453 RID: 13395
	float GetMinimumAmount(Tag tag);

	// Token: 0x06003454 RID: 13396
	Dictionary<Tag, float> GetRemaining();

	// Token: 0x06003455 RID: 13397
	Dictionary<Tag, float> GetRemainingMinimum();
}
