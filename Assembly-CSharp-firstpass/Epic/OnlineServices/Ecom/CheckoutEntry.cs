using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200082C RID: 2092
	public class CheckoutEntry
	{
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06004B03 RID: 19203 RVA: 0x00092FBE File Offset: 0x000911BE
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06004B04 RID: 19204 RVA: 0x00092FC1 File Offset: 0x000911C1
		// (set) Token: 0x06004B05 RID: 19205 RVA: 0x00092FC9 File Offset: 0x000911C9
		public string OfferId { get; set; }
	}
}
