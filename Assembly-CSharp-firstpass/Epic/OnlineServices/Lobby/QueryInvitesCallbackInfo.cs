using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007BE RID: 1982
	public class QueryInvitesCallbackInfo
	{
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06004817 RID: 18455 RVA: 0x00090173 File Offset: 0x0008E373
		// (set) Token: 0x06004818 RID: 18456 RVA: 0x0009017B File Offset: 0x0008E37B
		public Result ResultCode { get; set; }

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06004819 RID: 18457 RVA: 0x00090184 File Offset: 0x0008E384
		// (set) Token: 0x0600481A RID: 18458 RVA: 0x0009018C File Offset: 0x0008E38C
		public object ClientData { get; set; }

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600481B RID: 18459 RVA: 0x00090195 File Offset: 0x0008E395
		// (set) Token: 0x0600481C RID: 18460 RVA: 0x0009019D File Offset: 0x0008E39D
		public ProductUserId LocalUserId { get; set; }
	}
}
