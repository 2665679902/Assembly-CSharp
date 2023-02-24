using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000636 RID: 1590
	public class SessionModificationSetHostAddressOptions
	{
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06003E84 RID: 16004 RVA: 0x0008631F File Offset: 0x0008451F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06003E85 RID: 16005 RVA: 0x00086322 File Offset: 0x00084522
		// (set) Token: 0x06003E86 RID: 16006 RVA: 0x0008632A File Offset: 0x0008452A
		public string HostAddress { get; set; }
	}
}
