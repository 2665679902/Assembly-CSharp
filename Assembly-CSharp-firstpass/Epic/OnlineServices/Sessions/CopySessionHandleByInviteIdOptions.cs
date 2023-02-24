using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005CF RID: 1487
	public class CopySessionHandleByInviteIdOptions
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06003C59 RID: 15449 RVA: 0x0008466B File Offset: 0x0008286B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x0008466E File Offset: 0x0008286E
		// (set) Token: 0x06003C5B RID: 15451 RVA: 0x00084676 File Offset: 0x00082876
		public string InviteId { get; set; }
	}
}
