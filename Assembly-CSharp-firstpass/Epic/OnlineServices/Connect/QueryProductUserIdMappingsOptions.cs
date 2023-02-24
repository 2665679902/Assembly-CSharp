using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D2 RID: 2258
	public class QueryProductUserIdMappingsOptions
	{
		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06004F3A RID: 20282 RVA: 0x0009731E File Offset: 0x0009551E
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004F3B RID: 20283 RVA: 0x00097321 File Offset: 0x00095521
		// (set) Token: 0x06004F3C RID: 20284 RVA: 0x00097329 File Offset: 0x00095529
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004F3D RID: 20285 RVA: 0x00097332 File Offset: 0x00095532
		// (set) Token: 0x06004F3E RID: 20286 RVA: 0x0009733A File Offset: 0x0009553A
		public ExternalAccountType AccountIdType_DEPRECATED { get; set; }

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004F3F RID: 20287 RVA: 0x00097343 File Offset: 0x00095543
		// (set) Token: 0x06004F40 RID: 20288 RVA: 0x0009734B File Offset: 0x0009554B
		public ProductUserId[] ProductUserIds { get; set; }
	}
}
