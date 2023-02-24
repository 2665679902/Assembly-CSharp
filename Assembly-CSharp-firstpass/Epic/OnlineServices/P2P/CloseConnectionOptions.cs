using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006E4 RID: 1764
	public class CloseConnectionOptions
	{
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x0008B1CF File Offset: 0x000893CF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x0008B1D2 File Offset: 0x000893D2
		// (set) Token: 0x06004337 RID: 17207 RVA: 0x0008B1DA File Offset: 0x000893DA
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06004338 RID: 17208 RVA: 0x0008B1E3 File Offset: 0x000893E3
		// (set) Token: 0x06004339 RID: 17209 RVA: 0x0008B1EB File Offset: 0x000893EB
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x0600433A RID: 17210 RVA: 0x0008B1F4 File Offset: 0x000893F4
		// (set) Token: 0x0600433B RID: 17211 RVA: 0x0008B1FC File Offset: 0x000893FC
		public SocketId SocketId { get; set; }
	}
}
