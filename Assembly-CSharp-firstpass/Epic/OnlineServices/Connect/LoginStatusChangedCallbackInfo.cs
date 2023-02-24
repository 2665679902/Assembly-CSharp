using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008B4 RID: 2228
	public class LoginStatusChangedCallbackInfo
	{
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004EAD RID: 20141 RVA: 0x00096FB7 File Offset: 0x000951B7
		// (set) Token: 0x06004EAE RID: 20142 RVA: 0x00096FBF File Offset: 0x000951BF
		public object ClientData { get; set; }

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004EAF RID: 20143 RVA: 0x00096FC8 File Offset: 0x000951C8
		// (set) Token: 0x06004EB0 RID: 20144 RVA: 0x00096FD0 File Offset: 0x000951D0
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004EB1 RID: 20145 RVA: 0x00096FD9 File Offset: 0x000951D9
		// (set) Token: 0x06004EB2 RID: 20146 RVA: 0x00096FE1 File Offset: 0x000951E1
		public LoginStatus PreviousStatus { get; set; }

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004EB3 RID: 20147 RVA: 0x00096FEA File Offset: 0x000951EA
		// (set) Token: 0x06004EB4 RID: 20148 RVA: 0x00096FF2 File Offset: 0x000951F2
		public LoginStatus CurrentStatus { get; set; }
	}
}
