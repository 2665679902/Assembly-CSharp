using System;

// Token: 0x020009A3 RID: 2467
public interface IReadonlyTags
{
	// Token: 0x06004939 RID: 18745
	bool HasTag(string tag);

	// Token: 0x0600493A RID: 18746
	bool HasTag(int hashtag);

	// Token: 0x0600493B RID: 18747
	bool HasTags(int[] tags);
}
