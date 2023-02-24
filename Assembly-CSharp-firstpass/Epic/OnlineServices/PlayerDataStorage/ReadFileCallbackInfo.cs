using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C5 RID: 1733
	public class ReadFileCallbackInfo
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x00089D8F File Offset: 0x00087F8F
		// (set) Token: 0x060041FA RID: 16890 RVA: 0x00089D97 File Offset: 0x00087F97
		public Result ResultCode { get; set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060041FB RID: 16891 RVA: 0x00089DA0 File Offset: 0x00087FA0
		// (set) Token: 0x060041FC RID: 16892 RVA: 0x00089DA8 File Offset: 0x00087FA8
		public object ClientData { get; set; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060041FD RID: 16893 RVA: 0x00089DB1 File Offset: 0x00087FB1
		// (set) Token: 0x060041FE RID: 16894 RVA: 0x00089DB9 File Offset: 0x00087FB9
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060041FF RID: 16895 RVA: 0x00089DC2 File Offset: 0x00087FC2
		// (set) Token: 0x06004200 RID: 16896 RVA: 0x00089DCA File Offset: 0x00087FCA
		public string Filename { get; set; }
	}
}
