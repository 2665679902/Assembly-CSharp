using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005AB RID: 1451
	public class IngestStatCompleteCallbackInfo
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x0008366B File Offset: 0x0008186B
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x00083673 File Offset: 0x00081873
		public Result ResultCode { get; set; }

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x0008367C File Offset: 0x0008187C
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x00083684 File Offset: 0x00081884
		public object ClientData { get; set; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x0008368D File Offset: 0x0008188D
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x00083695 File Offset: 0x00081895
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x0008369E File Offset: 0x0008189E
		// (set) Token: 0x06003B76 RID: 15222 RVA: 0x000836A6 File Offset: 0x000818A6
		public ProductUserId TargetUserId { get; set; }
	}
}
