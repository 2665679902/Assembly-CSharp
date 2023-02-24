using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000842 RID: 2114
	public class CopyOfferItemByIndexOptions
	{
		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06004BA2 RID: 19362 RVA: 0x00093993 File Offset: 0x00091B93
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x00093996 File Offset: 0x00091B96
		// (set) Token: 0x06004BA4 RID: 19364 RVA: 0x0009399E File Offset: 0x00091B9E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06004BA5 RID: 19365 RVA: 0x000939A7 File Offset: 0x00091BA7
		// (set) Token: 0x06004BA6 RID: 19366 RVA: 0x000939AF File Offset: 0x00091BAF
		public string OfferId { get; set; }

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06004BA7 RID: 19367 RVA: 0x000939B8 File Offset: 0x00091BB8
		// (set) Token: 0x06004BA8 RID: 19368 RVA: 0x000939C0 File Offset: 0x00091BC0
		public uint ItemIndex { get; set; }
	}
}
