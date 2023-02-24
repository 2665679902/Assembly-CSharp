using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200062B RID: 1579
	public class SessionInviteAcceptedCallbackInfo
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06003E33 RID: 15923 RVA: 0x00085D6F File Offset: 0x00083F6F
		// (set) Token: 0x06003E34 RID: 15924 RVA: 0x00085D77 File Offset: 0x00083F77
		public object ClientData { get; set; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06003E35 RID: 15925 RVA: 0x00085D80 File Offset: 0x00083F80
		// (set) Token: 0x06003E36 RID: 15926 RVA: 0x00085D88 File Offset: 0x00083F88
		public string SessionId { get; set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06003E37 RID: 15927 RVA: 0x00085D91 File Offset: 0x00083F91
		// (set) Token: 0x06003E38 RID: 15928 RVA: 0x00085D99 File Offset: 0x00083F99
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06003E39 RID: 15929 RVA: 0x00085DA2 File Offset: 0x00083FA2
		// (set) Token: 0x06003E3A RID: 15930 RVA: 0x00085DAA File Offset: 0x00083FAA
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x00085DB3 File Offset: 0x00083FB3
		// (set) Token: 0x06003E3C RID: 15932 RVA: 0x00085DBB File Offset: 0x00083FBB
		public string InviteId { get; set; }
	}
}
