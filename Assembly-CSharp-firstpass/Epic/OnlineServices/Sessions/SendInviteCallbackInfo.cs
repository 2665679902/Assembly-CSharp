using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000617 RID: 1559
	public class SendInviteCallbackInfo
	{
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06003DB6 RID: 15798 RVA: 0x0008553F File Offset: 0x0008373F
		// (set) Token: 0x06003DB7 RID: 15799 RVA: 0x00085547 File Offset: 0x00083747
		public Result ResultCode { get; set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06003DB8 RID: 15800 RVA: 0x00085550 File Offset: 0x00083750
		// (set) Token: 0x06003DB9 RID: 15801 RVA: 0x00085558 File Offset: 0x00083758
		public object ClientData { get; set; }
	}
}
