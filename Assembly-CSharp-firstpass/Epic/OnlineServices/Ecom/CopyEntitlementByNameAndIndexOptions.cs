using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000834 RID: 2100
	public class CopyEntitlementByNameAndIndexOptions
	{
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x000932FB File Offset: 0x000914FB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06004B38 RID: 19256 RVA: 0x000932FE File Offset: 0x000914FE
		// (set) Token: 0x06004B39 RID: 19257 RVA: 0x00093306 File Offset: 0x00091506
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06004B3A RID: 19258 RVA: 0x0009330F File Offset: 0x0009150F
		// (set) Token: 0x06004B3B RID: 19259 RVA: 0x00093317 File Offset: 0x00091517
		public string EntitlementName { get; set; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06004B3C RID: 19260 RVA: 0x00093320 File Offset: 0x00091520
		// (set) Token: 0x06004B3D RID: 19261 RVA: 0x00093328 File Offset: 0x00091528
		public uint Index { get; set; }
	}
}
