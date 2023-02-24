using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000658 RID: 1624
	public class StartSessionOptions
	{
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06003F6D RID: 16237 RVA: 0x000876D2 File Offset: 0x000858D2
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06003F6E RID: 16238 RVA: 0x000876D5 File Offset: 0x000858D5
		// (set) Token: 0x06003F6F RID: 16239 RVA: 0x000876DD File Offset: 0x000858DD
		public string SessionName { get; set; }
	}
}
