using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006CC RID: 1740
	public class WriteFileCallbackInfo
	{
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x0008A12B File Offset: 0x0008832B
		// (set) Token: 0x06004234 RID: 16948 RVA: 0x0008A133 File Offset: 0x00088333
		public Result ResultCode { get; set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x0008A13C File Offset: 0x0008833C
		// (set) Token: 0x06004236 RID: 16950 RVA: 0x0008A144 File Offset: 0x00088344
		public object ClientData { get; set; }

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x0008A14D File Offset: 0x0008834D
		// (set) Token: 0x06004238 RID: 16952 RVA: 0x0008A155 File Offset: 0x00088355
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x0008A15E File Offset: 0x0008835E
		// (set) Token: 0x0600423A RID: 16954 RVA: 0x0008A166 File Offset: 0x00088366
		public string Filename { get; set; }
	}
}
