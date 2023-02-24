using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000875 RID: 2165
	public class QueryOwnershipCallbackInfo
	{
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004D0C RID: 19724 RVA: 0x0009517F File Offset: 0x0009337F
		// (set) Token: 0x06004D0D RID: 19725 RVA: 0x00095187 File Offset: 0x00093387
		public Result ResultCode { get; set; }

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004D0E RID: 19726 RVA: 0x00095190 File Offset: 0x00093390
		// (set) Token: 0x06004D0F RID: 19727 RVA: 0x00095198 File Offset: 0x00093398
		public object ClientData { get; set; }

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004D10 RID: 19728 RVA: 0x000951A1 File Offset: 0x000933A1
		// (set) Token: 0x06004D11 RID: 19729 RVA: 0x000951A9 File Offset: 0x000933A9
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004D12 RID: 19730 RVA: 0x000951B2 File Offset: 0x000933B2
		// (set) Token: 0x06004D13 RID: 19731 RVA: 0x000951BA File Offset: 0x000933BA
		public ItemOwnership[] ItemOwnership { get; set; }
	}
}
