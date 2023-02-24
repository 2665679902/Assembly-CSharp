using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000634 RID: 1588
	public class SessionModificationSetBucketIdOptions
	{
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06003E7B RID: 15995 RVA: 0x0008629B File Offset: 0x0008449B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06003E7C RID: 15996 RVA: 0x0008629E File Offset: 0x0008449E
		// (set) Token: 0x06003E7D RID: 15997 RVA: 0x000862A6 File Offset: 0x000844A6
		public string BucketId { get; set; }
	}
}
