using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200087F RID: 2175
	public class RedeemEntitlementsOptions
	{
		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06004D55 RID: 19797 RVA: 0x00095646 File Offset: 0x00093846
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x00095649 File Offset: 0x00093849
		// (set) Token: 0x06004D57 RID: 19799 RVA: 0x00095651 File Offset: 0x00093851
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06004D58 RID: 19800 RVA: 0x0009565A File Offset: 0x0009385A
		// (set) Token: 0x06004D59 RID: 19801 RVA: 0x00095662 File Offset: 0x00093862
		public string[] EntitlementIds { get; set; }
	}
}
