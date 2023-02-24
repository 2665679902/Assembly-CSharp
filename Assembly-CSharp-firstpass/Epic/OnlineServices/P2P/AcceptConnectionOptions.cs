using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006DE RID: 1758
	public class AcceptConnectionOptions
	{
		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600430A RID: 17162 RVA: 0x0008AF0B File Offset: 0x0008910B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600430B RID: 17163 RVA: 0x0008AF0E File Offset: 0x0008910E
		// (set) Token: 0x0600430C RID: 17164 RVA: 0x0008AF16 File Offset: 0x00089116
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x0008AF1F File Offset: 0x0008911F
		// (set) Token: 0x0600430E RID: 17166 RVA: 0x0008AF27 File Offset: 0x00089127
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x0008AF30 File Offset: 0x00089130
		// (set) Token: 0x06004310 RID: 17168 RVA: 0x0008AF38 File Offset: 0x00089138
		public SocketId SocketId { get; set; }
	}
}
