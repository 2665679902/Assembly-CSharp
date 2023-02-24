using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200068C RID: 1676
	public class QueryPresenceCallbackInfo
	{
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060040A8 RID: 16552 RVA: 0x00088ADB File Offset: 0x00086CDB
		// (set) Token: 0x060040A9 RID: 16553 RVA: 0x00088AE3 File Offset: 0x00086CE3
		public Result ResultCode { get; set; }

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060040AA RID: 16554 RVA: 0x00088AEC File Offset: 0x00086CEC
		// (set) Token: 0x060040AB RID: 16555 RVA: 0x00088AF4 File Offset: 0x00086CF4
		public object ClientData { get; set; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060040AC RID: 16556 RVA: 0x00088AFD File Offset: 0x00086CFD
		// (set) Token: 0x060040AD RID: 16557 RVA: 0x00088B05 File Offset: 0x00086D05
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060040AE RID: 16558 RVA: 0x00088B0E File Offset: 0x00086D0E
		// (set) Token: 0x060040AF RID: 16559 RVA: 0x00088B16 File Offset: 0x00086D16
		public EpicAccountId TargetUserId { get; set; }
	}
}
