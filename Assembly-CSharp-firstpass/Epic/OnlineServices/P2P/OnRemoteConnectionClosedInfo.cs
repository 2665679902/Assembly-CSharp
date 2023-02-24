using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006FC RID: 1788
	public class OnRemoteConnectionClosedInfo
	{
		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x060043A0 RID: 17312 RVA: 0x0008B6E6 File Offset: 0x000898E6
		// (set) Token: 0x060043A1 RID: 17313 RVA: 0x0008B6EE File Offset: 0x000898EE
		public object ClientData { get; set; }

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x060043A2 RID: 17314 RVA: 0x0008B6F7 File Offset: 0x000898F7
		// (set) Token: 0x060043A3 RID: 17315 RVA: 0x0008B6FF File Offset: 0x000898FF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x060043A4 RID: 17316 RVA: 0x0008B708 File Offset: 0x00089908
		// (set) Token: 0x060043A5 RID: 17317 RVA: 0x0008B710 File Offset: 0x00089910
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x0008B719 File Offset: 0x00089919
		// (set) Token: 0x060043A7 RID: 17319 RVA: 0x0008B721 File Offset: 0x00089921
		public SocketId SocketId { get; set; }

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x060043A8 RID: 17320 RVA: 0x0008B72A File Offset: 0x0008992A
		// (set) Token: 0x060043A9 RID: 17321 RVA: 0x0008B732 File Offset: 0x00089932
		public ConnectionClosedReason Reason { get; set; }
	}
}
