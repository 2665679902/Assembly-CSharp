using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000651 RID: 1617
	public class SessionSearchSetSessionIdOptions
	{
		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x00086BBF File Offset: 0x00084DBF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06003F10 RID: 16144 RVA: 0x00086BC2 File Offset: 0x00084DC2
		// (set) Token: 0x06003F11 RID: 16145 RVA: 0x00086BCA File Offset: 0x00084DCA
		public string SessionId { get; set; }
	}
}
