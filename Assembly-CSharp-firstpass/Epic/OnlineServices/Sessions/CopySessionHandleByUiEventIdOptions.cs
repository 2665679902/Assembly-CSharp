using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D1 RID: 1489
	public class CopySessionHandleByUiEventIdOptions
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06003C62 RID: 15458 RVA: 0x000846EF File Offset: 0x000828EF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06003C63 RID: 15459 RVA: 0x000846F2 File Offset: 0x000828F2
		// (set) Token: 0x06003C64 RID: 15460 RVA: 0x000846FA File Offset: 0x000828FA
		public ulong UiEventId { get; set; }
	}
}
