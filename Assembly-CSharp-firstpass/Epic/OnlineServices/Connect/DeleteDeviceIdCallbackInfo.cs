using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200089F RID: 2207
	public class DeleteDeviceIdCallbackInfo
	{
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06004E29 RID: 20009 RVA: 0x00096783 File Offset: 0x00094983
		// (set) Token: 0x06004E2A RID: 20010 RVA: 0x0009678B File Offset: 0x0009498B
		public Result ResultCode { get; set; }

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06004E2B RID: 20011 RVA: 0x00096794 File Offset: 0x00094994
		// (set) Token: 0x06004E2C RID: 20012 RVA: 0x0009679C File Offset: 0x0009499C
		public object ClientData { get; set; }
	}
}
