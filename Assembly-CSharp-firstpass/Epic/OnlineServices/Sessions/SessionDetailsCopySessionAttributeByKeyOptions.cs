using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000623 RID: 1571
	public class SessionDetailsCopySessionAttributeByKeyOptions
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06003DF7 RID: 15863 RVA: 0x000859B3 File Offset: 0x00083BB3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06003DF8 RID: 15864 RVA: 0x000859B6 File Offset: 0x00083BB6
		// (set) Token: 0x06003DF9 RID: 15865 RVA: 0x000859BE File Offset: 0x00083BBE
		public string AttrKey { get; set; }
	}
}
