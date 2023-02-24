using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000828 RID: 2088
	public class CatalogRelease
	{
		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x00092D9B File Offset: 0x00090F9B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06004AE5 RID: 19173 RVA: 0x00092D9E File Offset: 0x00090F9E
		// (set) Token: 0x06004AE6 RID: 19174 RVA: 0x00092DA6 File Offset: 0x00090FA6
		public string[] CompatibleAppIds { get; set; }

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06004AE7 RID: 19175 RVA: 0x00092DAF File Offset: 0x00090FAF
		// (set) Token: 0x06004AE8 RID: 19176 RVA: 0x00092DB7 File Offset: 0x00090FB7
		public string[] CompatiblePlatforms { get; set; }

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06004AE9 RID: 19177 RVA: 0x00092DC0 File Offset: 0x00090FC0
		// (set) Token: 0x06004AEA RID: 19178 RVA: 0x00092DC8 File Offset: 0x00090FC8
		public string ReleaseNote { get; set; }
	}
}
