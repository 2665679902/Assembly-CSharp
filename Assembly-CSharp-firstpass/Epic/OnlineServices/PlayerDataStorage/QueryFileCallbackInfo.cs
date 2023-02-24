using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006BD RID: 1725
	public class QueryFileCallbackInfo
	{
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x00089AB0 File Offset: 0x00087CB0
		// (set) Token: 0x060041CB RID: 16843 RVA: 0x00089AB8 File Offset: 0x00087CB8
		public Result ResultCode { get; set; }

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x060041CC RID: 16844 RVA: 0x00089AC1 File Offset: 0x00087CC1
		// (set) Token: 0x060041CD RID: 16845 RVA: 0x00089AC9 File Offset: 0x00087CC9
		public object ClientData { get; set; }

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x00089AD2 File Offset: 0x00087CD2
		// (set) Token: 0x060041CF RID: 16847 RVA: 0x00089ADA File Offset: 0x00087CDA
		public ProductUserId LocalUserId { get; set; }
	}
}
