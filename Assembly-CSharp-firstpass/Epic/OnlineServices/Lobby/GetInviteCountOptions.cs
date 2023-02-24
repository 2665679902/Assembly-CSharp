using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000741 RID: 1857
	public class GetInviteCountOptions
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x0008D4F3 File Offset: 0x0008B6F3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x0600453D RID: 17725 RVA: 0x0008D4F6 File Offset: 0x0008B6F6
		// (set) Token: 0x0600453E RID: 17726 RVA: 0x0008D4FE File Offset: 0x0008B6FE
		public ProductUserId LocalUserId { get; set; }
	}
}
