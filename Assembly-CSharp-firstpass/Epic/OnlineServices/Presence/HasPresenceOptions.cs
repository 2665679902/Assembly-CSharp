using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000670 RID: 1648
	public class HasPresenceOptions
	{
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x00087DFB File Offset: 0x00085FFB
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x00087DFE File Offset: 0x00085FFE
		// (set) Token: 0x06003FE7 RID: 16359 RVA: 0x00087E06 File Offset: 0x00086006
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x00087E0F File Offset: 0x0008600F
		// (set) Token: 0x06003FE9 RID: 16361 RVA: 0x00087E17 File Offset: 0x00086017
		public EpicAccountId TargetUserId { get; set; }
	}
}
