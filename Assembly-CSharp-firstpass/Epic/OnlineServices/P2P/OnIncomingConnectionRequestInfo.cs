using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006F4 RID: 1780
	public class OnIncomingConnectionRequestInfo
	{
		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x0008B553 File Offset: 0x00089753
		// (set) Token: 0x06004378 RID: 17272 RVA: 0x0008B55B File Offset: 0x0008975B
		public object ClientData { get; set; }

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x0008B564 File Offset: 0x00089764
		// (set) Token: 0x0600437A RID: 17274 RVA: 0x0008B56C File Offset: 0x0008976C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x0008B575 File Offset: 0x00089775
		// (set) Token: 0x0600437C RID: 17276 RVA: 0x0008B57D File Offset: 0x0008977D
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x0008B586 File Offset: 0x00089786
		// (set) Token: 0x0600437E RID: 17278 RVA: 0x0008B58E File Offset: 0x0008978E
		public SocketId SocketId { get; set; }
	}
}
