using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200064D RID: 1613
	public class SessionSearchSetMaxResultsOptions
	{
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06003EF9 RID: 16121 RVA: 0x00086A67 File Offset: 0x00084C67
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x00086A6A File Offset: 0x00084C6A
		// (set) Token: 0x06003EFB RID: 16123 RVA: 0x00086A72 File Offset: 0x00084C72
		public uint MaxSearchResults { get; set; }
	}
}
