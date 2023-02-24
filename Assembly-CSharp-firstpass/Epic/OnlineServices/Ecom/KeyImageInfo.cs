using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200085E RID: 2142
	public class KeyImageInfo
	{
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06004C93 RID: 19603 RVA: 0x00094CDF File Offset: 0x00092EDF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06004C94 RID: 19604 RVA: 0x00094CE2 File Offset: 0x00092EE2
		// (set) Token: 0x06004C95 RID: 19605 RVA: 0x00094CEA File Offset: 0x00092EEA
		public string Type { get; set; }

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06004C96 RID: 19606 RVA: 0x00094CF3 File Offset: 0x00092EF3
		// (set) Token: 0x06004C97 RID: 19607 RVA: 0x00094CFB File Offset: 0x00092EFB
		public string Url { get; set; }

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06004C98 RID: 19608 RVA: 0x00094D04 File Offset: 0x00092F04
		// (set) Token: 0x06004C99 RID: 19609 RVA: 0x00094D0C File Offset: 0x00092F0C
		public uint Width { get; set; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06004C9A RID: 19610 RVA: 0x00094D15 File Offset: 0x00092F15
		// (set) Token: 0x06004C9B RID: 19611 RVA: 0x00094D1D File Offset: 0x00092F1D
		public uint Height { get; set; }
	}
}
