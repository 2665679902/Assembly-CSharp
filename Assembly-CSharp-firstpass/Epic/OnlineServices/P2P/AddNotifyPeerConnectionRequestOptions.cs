using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006E2 RID: 1762
	public class AddNotifyPeerConnectionRequestOptions
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06004328 RID: 17192 RVA: 0x0008B0FB File Offset: 0x000892FB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x0008B0FE File Offset: 0x000892FE
		// (set) Token: 0x0600432A RID: 17194 RVA: 0x0008B106 File Offset: 0x00089306
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x0600432B RID: 17195 RVA: 0x0008B10F File Offset: 0x0008930F
		// (set) Token: 0x0600432C RID: 17196 RVA: 0x0008B117 File Offset: 0x00089317
		public SocketId SocketId { get; set; }
	}
}
