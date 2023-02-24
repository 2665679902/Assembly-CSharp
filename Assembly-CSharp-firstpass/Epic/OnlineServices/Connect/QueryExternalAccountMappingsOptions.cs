using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008CE RID: 2254
	public class QueryExternalAccountMappingsOptions
	{
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x0009714A File Offset: 0x0009534A
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004F1F RID: 20255 RVA: 0x0009714D File Offset: 0x0009534D
		// (set) Token: 0x06004F20 RID: 20256 RVA: 0x00097155 File Offset: 0x00095355
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x0009715E File Offset: 0x0009535E
		// (set) Token: 0x06004F22 RID: 20258 RVA: 0x00097166 File Offset: 0x00095366
		public ExternalAccountType AccountIdType { get; set; }

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004F23 RID: 20259 RVA: 0x0009716F File Offset: 0x0009536F
		// (set) Token: 0x06004F24 RID: 20260 RVA: 0x00097177 File Offset: 0x00095377
		public string[] ExternalAccountIds { get; set; }
	}
}
