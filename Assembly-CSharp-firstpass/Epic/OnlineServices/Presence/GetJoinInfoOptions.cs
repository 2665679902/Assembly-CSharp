using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200066E RID: 1646
	public class GetJoinInfoOptions
	{
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06003FD8 RID: 16344 RVA: 0x00087D33 File Offset: 0x00085F33
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06003FD9 RID: 16345 RVA: 0x00087D36 File Offset: 0x00085F36
		// (set) Token: 0x06003FDA RID: 16346 RVA: 0x00087D3E File Offset: 0x00085F3E
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06003FDB RID: 16347 RVA: 0x00087D47 File Offset: 0x00085F47
		// (set) Token: 0x06003FDC RID: 16348 RVA: 0x00087D4F File Offset: 0x00085F4F
		public EpicAccountId TargetUserId { get; set; }
	}
}
