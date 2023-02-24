using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200088A RID: 2186
	public class AuthExpirationCallbackInfo
	{
		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x06004D84 RID: 19844 RVA: 0x0009593B File Offset: 0x00093B3B
		// (set) Token: 0x06004D85 RID: 19845 RVA: 0x00095943 File Offset: 0x00093B43
		public object ClientData { get; set; }

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x06004D86 RID: 19846 RVA: 0x0009594C File Offset: 0x00093B4C
		// (set) Token: 0x06004D87 RID: 19847 RVA: 0x00095954 File Offset: 0x00093B54
		public ProductUserId LocalUserId { get; set; }
	}
}
