using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000840 RID: 2112
	public class CopyOfferImageInfoByIndexOptions
	{
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004B91 RID: 19345 RVA: 0x00093883 File Offset: 0x00091A83
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06004B92 RID: 19346 RVA: 0x00093886 File Offset: 0x00091A86
		// (set) Token: 0x06004B93 RID: 19347 RVA: 0x0009388E File Offset: 0x00091A8E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06004B94 RID: 19348 RVA: 0x00093897 File Offset: 0x00091A97
		// (set) Token: 0x06004B95 RID: 19349 RVA: 0x0009389F File Offset: 0x00091A9F
		public string OfferId { get; set; }

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004B96 RID: 19350 RVA: 0x000938A8 File Offset: 0x00091AA8
		// (set) Token: 0x06004B97 RID: 19351 RVA: 0x000938B0 File Offset: 0x00091AB0
		public uint ImageInfoIndex { get; set; }
	}
}
