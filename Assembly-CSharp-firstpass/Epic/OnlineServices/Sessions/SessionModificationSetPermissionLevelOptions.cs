using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200063E RID: 1598
	public class SessionModificationSetPermissionLevelOptions
	{
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x0008652F File Offset: 0x0008472F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x00086532 File Offset: 0x00084732
		// (set) Token: 0x06003EAA RID: 16042 RVA: 0x0008653A File Offset: 0x0008473A
		public OnlineSessionPermissionLevel PermissionLevel { get; set; }
	}
}
