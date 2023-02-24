using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000899 RID: 2201
	public class CreateUserCallbackInfo
	{
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x00096587 File Offset: 0x00094787
		// (set) Token: 0x06004E09 RID: 19977 RVA: 0x0009658F File Offset: 0x0009478F
		public Result ResultCode { get; set; }

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004E0A RID: 19978 RVA: 0x00096598 File Offset: 0x00094798
		// (set) Token: 0x06004E0B RID: 19979 RVA: 0x000965A0 File Offset: 0x000947A0
		public object ClientData { get; set; }

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004E0C RID: 19980 RVA: 0x000965A9 File Offset: 0x000947A9
		// (set) Token: 0x06004E0D RID: 19981 RVA: 0x000965B1 File Offset: 0x000947B1
		public ProductUserId LocalUserId { get; set; }
	}
}
