using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000895 RID: 2197
	public class CreateDeviceIdCallbackInfo
	{
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004DF7 RID: 19959 RVA: 0x00096487 File Offset: 0x00094687
		// (set) Token: 0x06004DF8 RID: 19960 RVA: 0x0009648F File Offset: 0x0009468F
		public Result ResultCode { get; set; }

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004DF9 RID: 19961 RVA: 0x00096498 File Offset: 0x00094698
		// (set) Token: 0x06004DFA RID: 19962 RVA: 0x000964A0 File Offset: 0x000946A0
		public object ClientData { get; set; }
	}
}
