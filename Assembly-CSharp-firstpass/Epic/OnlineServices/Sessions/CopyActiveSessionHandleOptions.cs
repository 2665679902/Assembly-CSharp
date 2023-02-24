using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005CD RID: 1485
	public class CopyActiveSessionHandleOptions
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x000845E6 File Offset: 0x000827E6
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000845E9 File Offset: 0x000827E9
		// (set) Token: 0x06003C52 RID: 15442 RVA: 0x000845F1 File Offset: 0x000827F1
		public string SessionName { get; set; }
	}
}
