using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000836 RID: 2102
	public class CopyItemByIdOptions
	{
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06004B48 RID: 19272 RVA: 0x0009340B File Offset: 0x0009160B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06004B49 RID: 19273 RVA: 0x0009340E File Offset: 0x0009160E
		// (set) Token: 0x06004B4A RID: 19274 RVA: 0x00093416 File Offset: 0x00091616
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06004B4B RID: 19275 RVA: 0x0009341F File Offset: 0x0009161F
		// (set) Token: 0x06004B4C RID: 19276 RVA: 0x00093427 File Offset: 0x00091627
		public string ItemId { get; set; }
	}
}
