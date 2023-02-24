using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000682 RID: 1666
	public class PresenceModificationDeleteDataOptions
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600407B RID: 16507 RVA: 0x00088817 File Offset: 0x00086A17
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x0008881A File Offset: 0x00086A1A
		// (set) Token: 0x0600407D RID: 16509 RVA: 0x00088822 File Offset: 0x00086A22
		public PresenceModificationDataRecordId[] Records { get; set; }
	}
}
