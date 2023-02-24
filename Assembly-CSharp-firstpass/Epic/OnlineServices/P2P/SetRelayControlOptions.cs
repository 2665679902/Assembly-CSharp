using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000709 RID: 1801
	public class SetRelayControlOptions
	{
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x0008C113 File Offset: 0x0008A313
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x0600441A RID: 17434 RVA: 0x0008C116 File Offset: 0x0008A316
		// (set) Token: 0x0600441B RID: 17435 RVA: 0x0008C11E File Offset: 0x0008A31E
		public RelayControl RelayControl { get; set; }
	}
}
