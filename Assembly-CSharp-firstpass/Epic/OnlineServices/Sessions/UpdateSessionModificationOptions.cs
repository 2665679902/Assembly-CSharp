using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000660 RID: 1632
	public class UpdateSessionModificationOptions
	{
		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06003F99 RID: 16281 RVA: 0x00087996 File Offset: 0x00085B96
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06003F9A RID: 16282 RVA: 0x00087999 File Offset: 0x00085B99
		// (set) Token: 0x06003F9B RID: 16283 RVA: 0x000879A1 File Offset: 0x00085BA1
		public string SessionName { get; set; }
	}
}
