using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000686 RID: 1670
	public class PresenceModificationSetJoinInfoOptions
	{
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600408D RID: 16525 RVA: 0x0008894F File Offset: 0x00086B4F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600408E RID: 16526 RVA: 0x00088952 File Offset: 0x00086B52
		// (set) Token: 0x0600408F RID: 16527 RVA: 0x0008895A File Offset: 0x00086B5A
		public string JoinInfo { get; set; }
	}
}
