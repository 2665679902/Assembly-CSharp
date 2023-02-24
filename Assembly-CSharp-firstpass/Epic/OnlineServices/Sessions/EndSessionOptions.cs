using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E1 RID: 1505
	public class EndSessionOptions
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x00084C56 File Offset: 0x00082E56
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06003CBD RID: 15549 RVA: 0x00084C59 File Offset: 0x00082E59
		// (set) Token: 0x06003CBE RID: 15550 RVA: 0x00084C61 File Offset: 0x00082E61
		public string SessionName { get; set; }
	}
}
