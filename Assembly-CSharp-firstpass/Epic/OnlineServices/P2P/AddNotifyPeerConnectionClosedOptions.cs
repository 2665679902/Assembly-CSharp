using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006E0 RID: 1760
	public class AddNotifyPeerConnectionClosedOptions
	{
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x0008B027 File Offset: 0x00089227
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x0008B02A File Offset: 0x0008922A
		// (set) Token: 0x0600431D RID: 17181 RVA: 0x0008B032 File Offset: 0x00089232
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x0600431E RID: 17182 RVA: 0x0008B03B File Offset: 0x0008923B
		// (set) Token: 0x0600431F RID: 17183 RVA: 0x0008B043 File Offset: 0x00089243
		public SocketId SocketId { get; set; }
	}
}
