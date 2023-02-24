using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200083E RID: 2110
	public class CopyOfferByIndexOptions
	{
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06004B84 RID: 19332 RVA: 0x000937BB File Offset: 0x000919BB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06004B85 RID: 19333 RVA: 0x000937BE File Offset: 0x000919BE
		// (set) Token: 0x06004B86 RID: 19334 RVA: 0x000937C6 File Offset: 0x000919C6
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06004B87 RID: 19335 RVA: 0x000937CF File Offset: 0x000919CF
		// (set) Token: 0x06004B88 RID: 19336 RVA: 0x000937D7 File Offset: 0x000919D7
		public uint OfferIndex { get; set; }
	}
}
