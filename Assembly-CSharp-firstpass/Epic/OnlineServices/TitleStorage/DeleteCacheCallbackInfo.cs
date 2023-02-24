using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200057C RID: 1404
	public class DeleteCacheCallbackInfo
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000824E7 File Offset: 0x000806E7
		// (set) Token: 0x06003A45 RID: 14917 RVA: 0x000824EF File Offset: 0x000806EF
		public Result ResultCode { get; set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000824F8 File Offset: 0x000806F8
		// (set) Token: 0x06003A47 RID: 14919 RVA: 0x00082500 File Offset: 0x00080700
		public object ClientData { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06003A48 RID: 14920 RVA: 0x00082509 File Offset: 0x00080709
		// (set) Token: 0x06003A49 RID: 14921 RVA: 0x00082511 File Offset: 0x00080711
		public ProductUserId LocalUserId { get; set; }
	}
}
