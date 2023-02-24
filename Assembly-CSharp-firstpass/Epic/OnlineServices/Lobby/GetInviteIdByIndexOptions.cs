using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000743 RID: 1859
	public class GetInviteIdByIndexOptions
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06004545 RID: 17733 RVA: 0x0008D577 File Offset: 0x0008B777
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06004546 RID: 17734 RVA: 0x0008D57A File Offset: 0x0008B77A
		// (set) Token: 0x06004547 RID: 17735 RVA: 0x0008D582 File Offset: 0x0008B782
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06004548 RID: 17736 RVA: 0x0008D58B File Offset: 0x0008B78B
		// (set) Token: 0x06004549 RID: 17737 RVA: 0x0008D593 File Offset: 0x0008B793
		public uint Index { get; set; }
	}
}
