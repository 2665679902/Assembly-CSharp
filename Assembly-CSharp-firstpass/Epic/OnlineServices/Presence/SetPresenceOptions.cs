using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000694 RID: 1684
	public class SetPresenceOptions
	{
		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x00088D36 File Offset: 0x00086F36
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x00088D39 File Offset: 0x00086F39
		// (set) Token: 0x060040D8 RID: 16600 RVA: 0x00088D41 File Offset: 0x00086F41
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060040D9 RID: 16601 RVA: 0x00088D4A File Offset: 0x00086F4A
		// (set) Token: 0x060040DA RID: 16602 RVA: 0x00088D52 File Offset: 0x00086F52
		public PresenceModification PresenceModificationHandle { get; set; }
	}
}
