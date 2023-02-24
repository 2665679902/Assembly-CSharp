using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000627 RID: 1575
	public class SessionDetailsInfo
	{
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06003E05 RID: 15877 RVA: 0x00085A77 File Offset: 0x00083C77
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x00085A7A File Offset: 0x00083C7A
		// (set) Token: 0x06003E07 RID: 15879 RVA: 0x00085A82 File Offset: 0x00083C82
		public string SessionId { get; set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06003E08 RID: 15880 RVA: 0x00085A8B File Offset: 0x00083C8B
		// (set) Token: 0x06003E09 RID: 15881 RVA: 0x00085A93 File Offset: 0x00083C93
		public string HostAddress { get; set; }

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06003E0A RID: 15882 RVA: 0x00085A9C File Offset: 0x00083C9C
		// (set) Token: 0x06003E0B RID: 15883 RVA: 0x00085AA4 File Offset: 0x00083CA4
		public uint NumOpenPublicConnections { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06003E0C RID: 15884 RVA: 0x00085AAD File Offset: 0x00083CAD
		// (set) Token: 0x06003E0D RID: 15885 RVA: 0x00085AB5 File Offset: 0x00083CB5
		public SessionDetailsSettings Settings { get; set; }
	}
}
