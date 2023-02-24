using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005C1 RID: 1473
	public class ActiveSessionInfo
	{
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06003C05 RID: 15365 RVA: 0x00084017 File Offset: 0x00082217
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06003C06 RID: 15366 RVA: 0x0008401A File Offset: 0x0008221A
		// (set) Token: 0x06003C07 RID: 15367 RVA: 0x00084022 File Offset: 0x00082222
		public string SessionName { get; set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06003C08 RID: 15368 RVA: 0x0008402B File Offset: 0x0008222B
		// (set) Token: 0x06003C09 RID: 15369 RVA: 0x00084033 File Offset: 0x00082233
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06003C0A RID: 15370 RVA: 0x0008403C File Offset: 0x0008223C
		// (set) Token: 0x06003C0B RID: 15371 RVA: 0x00084044 File Offset: 0x00082244
		public OnlineSessionState State { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06003C0C RID: 15372 RVA: 0x0008404D File Offset: 0x0008224D
		// (set) Token: 0x06003C0D RID: 15373 RVA: 0x00084055 File Offset: 0x00082255
		public SessionDetailsInfo SessionDetails { get; set; }
	}
}
