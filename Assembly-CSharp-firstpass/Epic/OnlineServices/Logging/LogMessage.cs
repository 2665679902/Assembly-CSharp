using System;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x0200071A RID: 1818
	public class LogMessage
	{
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x0008C7F4 File Offset: 0x0008A9F4
		// (set) Token: 0x06004473 RID: 17523 RVA: 0x0008C7FC File Offset: 0x0008A9FC
		public string Category { get; set; }

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06004474 RID: 17524 RVA: 0x0008C805 File Offset: 0x0008AA05
		// (set) Token: 0x06004475 RID: 17525 RVA: 0x0008C80D File Offset: 0x0008AA0D
		public string Message { get; set; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06004476 RID: 17526 RVA: 0x0008C816 File Offset: 0x0008AA16
		// (set) Token: 0x06004477 RID: 17527 RVA: 0x0008C81E File Offset: 0x0008AA1E
		public LogLevel Level { get; set; }
	}
}
