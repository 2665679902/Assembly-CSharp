using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006E6 RID: 1766
	public class CloseConnectionsOptions
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06004346 RID: 17222 RVA: 0x0008B2EB File Offset: 0x000894EB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x0008B2EE File Offset: 0x000894EE
		// (set) Token: 0x06004348 RID: 17224 RVA: 0x0008B2F6 File Offset: 0x000894F6
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x0008B2FF File Offset: 0x000894FF
		// (set) Token: 0x0600434A RID: 17226 RVA: 0x0008B307 File Offset: 0x00089507
		public SocketId SocketId { get; set; }
	}
}
