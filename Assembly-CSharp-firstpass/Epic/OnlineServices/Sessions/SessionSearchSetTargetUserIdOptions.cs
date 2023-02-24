using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000653 RID: 1619
	public class SessionSearchSetTargetUserIdOptions
	{
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x00086C43 File Offset: 0x00084E43
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x00086C46 File Offset: 0x00084E46
		// (set) Token: 0x06003F1A RID: 16154 RVA: 0x00086C4E File Offset: 0x00084E4E
		public ProductUserId TargetUserId { get; set; }
	}
}
