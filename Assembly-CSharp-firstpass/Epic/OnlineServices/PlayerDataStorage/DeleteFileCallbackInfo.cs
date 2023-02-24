using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200069B RID: 1691
	public class DeleteFileCallbackInfo
	{
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x00088F8F File Offset: 0x0008718F
		// (set) Token: 0x060040FE RID: 16638 RVA: 0x00088F97 File Offset: 0x00087197
		public Result ResultCode { get; set; }

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x00088FA0 File Offset: 0x000871A0
		// (set) Token: 0x06004100 RID: 16640 RVA: 0x00088FA8 File Offset: 0x000871A8
		public object ClientData { get; set; }

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06004101 RID: 16641 RVA: 0x00088FB1 File Offset: 0x000871B1
		// (set) Token: 0x06004102 RID: 16642 RVA: 0x00088FB9 File Offset: 0x000871B9
		public ProductUserId LocalUserId { get; set; }
	}
}
