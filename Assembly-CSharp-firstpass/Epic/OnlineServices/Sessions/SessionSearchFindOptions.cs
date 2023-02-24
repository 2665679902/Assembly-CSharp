using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000645 RID: 1605
	public class SessionSearchFindOptions
	{
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06003ED6 RID: 16086 RVA: 0x000868DA File Offset: 0x00084ADA
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06003ED7 RID: 16087 RVA: 0x000868DD File Offset: 0x00084ADD
		// (set) Token: 0x06003ED8 RID: 16088 RVA: 0x000868E5 File Offset: 0x00084AE5
		public ProductUserId LocalUserId { get; set; }
	}
}
