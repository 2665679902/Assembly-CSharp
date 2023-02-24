using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000532 RID: 1330
	public class PageQuery
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600386E RID: 14446 RVA: 0x000807FE File Offset: 0x0007E9FE
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x00080801 File Offset: 0x0007EA01
		// (set) Token: 0x06003870 RID: 14448 RVA: 0x00080809 File Offset: 0x0007EA09
		public int StartIndex { get; set; }

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x00080812 File Offset: 0x0007EA12
		// (set) Token: 0x06003872 RID: 14450 RVA: 0x0008081A File Offset: 0x0007EA1A
		public int MaxCount { get; set; }
	}
}
