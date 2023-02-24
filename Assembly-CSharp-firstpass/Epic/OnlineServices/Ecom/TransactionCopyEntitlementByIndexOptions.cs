using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000882 RID: 2178
	public class TransactionCopyEntitlementByIndexOptions
	{
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x000957F7 File Offset: 0x000939F7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06004D6D RID: 19821 RVA: 0x000957FA File Offset: 0x000939FA
		// (set) Token: 0x06004D6E RID: 19822 RVA: 0x00095802 File Offset: 0x00093A02
		public uint EntitlementIndex { get; set; }
	}
}
