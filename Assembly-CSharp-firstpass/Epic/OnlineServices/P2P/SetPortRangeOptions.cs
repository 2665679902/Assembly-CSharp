using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000707 RID: 1799
	public class SetPortRangeOptions
	{
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600440C RID: 17420 RVA: 0x0008C04B File Offset: 0x0008A24B
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x0008C04E File Offset: 0x0008A24E
		// (set) Token: 0x0600440E RID: 17422 RVA: 0x0008C056 File Offset: 0x0008A256
		public ushort Port { get; set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x0008C05F File Offset: 0x0008A25F
		// (set) Token: 0x06004410 RID: 17424 RVA: 0x0008C067 File Offset: 0x0008A267
		public ushort MaxAdditionalPortsToTry { get; set; }
	}
}
