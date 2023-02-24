using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D3 RID: 1491
	public class CopySessionHandleForPresenceOptions
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x00084773 File Offset: 0x00082973
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06003C6C RID: 15468 RVA: 0x00084776 File Offset: 0x00082976
		// (set) Token: 0x06003C6D RID: 15469 RVA: 0x0008477E File Offset: 0x0008297E
		public ProductUserId LocalUserId { get; set; }
	}
}
