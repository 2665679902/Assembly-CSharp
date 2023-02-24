using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200057E RID: 1406
	public class DeleteCacheOptions
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x00082596 File Offset: 0x00080796
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06003A50 RID: 14928 RVA: 0x00082599 File Offset: 0x00080799
		// (set) Token: 0x06003A51 RID: 14929 RVA: 0x000825A1 File Offset: 0x000807A1
		public ProductUserId LocalUserId { get; set; }
	}
}
