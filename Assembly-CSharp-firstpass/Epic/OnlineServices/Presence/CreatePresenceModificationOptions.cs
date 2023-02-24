using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200066A RID: 1642
	public class CreatePresenceModificationOptions
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x00087BE7 File Offset: 0x00085DE7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06003FC3 RID: 16323 RVA: 0x00087BEA File Offset: 0x00085DEA
		// (set) Token: 0x06003FC4 RID: 16324 RVA: 0x00087BF2 File Offset: 0x00085DF2
		public EpicAccountId LocalUserId { get; set; }
	}
}
