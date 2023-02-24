using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000643 RID: 1603
	public class SessionSearchFindCallbackInfo
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x0008685F File Offset: 0x00084A5F
		// (set) Token: 0x06003ECF RID: 16079 RVA: 0x00086867 File Offset: 0x00084A67
		public Result ResultCode { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x00086870 File Offset: 0x00084A70
		// (set) Token: 0x06003ED1 RID: 16081 RVA: 0x00086878 File Offset: 0x00084A78
		public object ClientData { get; set; }
	}
}
