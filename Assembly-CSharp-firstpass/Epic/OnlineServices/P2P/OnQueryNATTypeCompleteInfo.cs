using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006F8 RID: 1784
	public class OnQueryNATTypeCompleteInfo
	{
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600438D RID: 17293 RVA: 0x0008B636 File Offset: 0x00089836
		// (set) Token: 0x0600438E RID: 17294 RVA: 0x0008B63E File Offset: 0x0008983E
		public Result ResultCode { get; set; }

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600438F RID: 17295 RVA: 0x0008B647 File Offset: 0x00089847
		// (set) Token: 0x06004390 RID: 17296 RVA: 0x0008B64F File Offset: 0x0008984F
		public object ClientData { get; set; }

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06004391 RID: 17297 RVA: 0x0008B658 File Offset: 0x00089858
		// (set) Token: 0x06004392 RID: 17298 RVA: 0x0008B660 File Offset: 0x00089860
		public NATType NATType { get; set; }
	}
}
