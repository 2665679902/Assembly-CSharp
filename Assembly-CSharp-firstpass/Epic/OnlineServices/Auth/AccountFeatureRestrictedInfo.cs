using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008DE RID: 2270
	public class AccountFeatureRestrictedInfo
	{
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x000977B7 File Offset: 0x000959B7
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004F85 RID: 20357 RVA: 0x000977BA File Offset: 0x000959BA
		// (set) Token: 0x06004F86 RID: 20358 RVA: 0x000977C2 File Offset: 0x000959C2
		public string VerificationURI { get; set; }
	}
}
