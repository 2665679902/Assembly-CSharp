using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200084A RID: 2122
	public class Entitlement
	{
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06004C0D RID: 19469 RVA: 0x000944C4 File Offset: 0x000926C4
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06004C0E RID: 19470 RVA: 0x000944C7 File Offset: 0x000926C7
		// (set) Token: 0x06004C0F RID: 19471 RVA: 0x000944CF File Offset: 0x000926CF
		public string EntitlementName { get; set; }

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06004C10 RID: 19472 RVA: 0x000944D8 File Offset: 0x000926D8
		// (set) Token: 0x06004C11 RID: 19473 RVA: 0x000944E0 File Offset: 0x000926E0
		public string EntitlementId { get; set; }

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06004C12 RID: 19474 RVA: 0x000944E9 File Offset: 0x000926E9
		// (set) Token: 0x06004C13 RID: 19475 RVA: 0x000944F1 File Offset: 0x000926F1
		public string CatalogItemId { get; set; }

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06004C14 RID: 19476 RVA: 0x000944FA File Offset: 0x000926FA
		// (set) Token: 0x06004C15 RID: 19477 RVA: 0x00094502 File Offset: 0x00092702
		public int ServerIndex { get; set; }

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06004C16 RID: 19478 RVA: 0x0009450B File Offset: 0x0009270B
		// (set) Token: 0x06004C17 RID: 19479 RVA: 0x00094513 File Offset: 0x00092713
		public bool Redeemed { get; set; }

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x0009451C File Offset: 0x0009271C
		// (set) Token: 0x06004C19 RID: 19481 RVA: 0x00094524 File Offset: 0x00092724
		public long EndTimestamp { get; set; }
	}
}
