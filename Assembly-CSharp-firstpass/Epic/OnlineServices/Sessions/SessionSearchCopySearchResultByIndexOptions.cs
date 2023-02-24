using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000641 RID: 1601
	public class SessionSearchCopySearchResultByIndexOptions
	{
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x000867DC File Offset: 0x000849DC
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06003EC6 RID: 16070 RVA: 0x000867DF File Offset: 0x000849DF
		// (set) Token: 0x06003EC7 RID: 16071 RVA: 0x000867E7 File Offset: 0x000849E7
		public uint SessionIndex { get; set; }
	}
}
