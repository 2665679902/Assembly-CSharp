using System;

namespace rail
{
	// Token: 0x020003AE RID: 942
	public class GetAuthenticateURLResult : EventBase
	{
		// Token: 0x04000D81 RID: 3457
		public uint ticket_expire_time;

		// Token: 0x04000D82 RID: 3458
		public string authenticate_url;

		// Token: 0x04000D83 RID: 3459
		public string source_url;
	}
}
