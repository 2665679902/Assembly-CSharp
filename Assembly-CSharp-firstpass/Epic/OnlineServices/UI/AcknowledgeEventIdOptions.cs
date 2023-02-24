using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000559 RID: 1369
	public class AcknowledgeEventIdOptions
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x00081A68 File Offset: 0x0007FC68
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x00081A6B File Offset: 0x0007FC6B
		// (set) Token: 0x06003995 RID: 14741 RVA: 0x00081A73 File Offset: 0x0007FC73
		public ulong UiEventId { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06003996 RID: 14742 RVA: 0x00081A7C File Offset: 0x0007FC7C
		// (set) Token: 0x06003997 RID: 14743 RVA: 0x00081A84 File Offset: 0x0007FC84
		public Result Result { get; set; }
	}
}
