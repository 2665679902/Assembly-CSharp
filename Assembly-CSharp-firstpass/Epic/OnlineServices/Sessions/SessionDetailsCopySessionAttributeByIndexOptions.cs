using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000621 RID: 1569
	public class SessionDetailsCopySessionAttributeByIndexOptions
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06003DEE RID: 15854 RVA: 0x0008592F File Offset: 0x00083B2F
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06003DEF RID: 15855 RVA: 0x00085932 File Offset: 0x00083B32
		// (set) Token: 0x06003DF0 RID: 15856 RVA: 0x0008593A File Offset: 0x00083B3A
		public uint AttrIndex { get; set; }
	}
}
