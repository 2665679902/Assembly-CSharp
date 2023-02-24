using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D7 RID: 1495
	public class CreateSessionSearchOptions
	{
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06003C91 RID: 15505 RVA: 0x000849D3 File Offset: 0x00082BD3
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06003C92 RID: 15506 RVA: 0x000849D6 File Offset: 0x00082BD6
		// (set) Token: 0x06003C93 RID: 15507 RVA: 0x000849DE File Offset: 0x00082BDE
		public uint MaxSearchResults { get; set; }
	}
}
