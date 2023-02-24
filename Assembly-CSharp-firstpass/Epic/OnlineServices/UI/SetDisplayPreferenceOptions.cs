using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200056F RID: 1391
	public class SetDisplayPreferenceOptions
	{
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060039EA RID: 14826 RVA: 0x00081E1A File Offset: 0x0008001A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x00081E1D File Offset: 0x0008001D
		// (set) Token: 0x060039EC RID: 14828 RVA: 0x00081E25 File Offset: 0x00080025
		public NotificationLocation NotificationLocation { get; set; }
	}
}
