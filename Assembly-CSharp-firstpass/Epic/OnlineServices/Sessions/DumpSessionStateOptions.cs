using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005DD RID: 1501
	public class DumpSessionStateOptions
	{
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x00084B57 File Offset: 0x00082D57
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x00084B5A File Offset: 0x00082D5A
		// (set) Token: 0x06003CAD RID: 15533 RVA: 0x00084B62 File Offset: 0x00082D62
		public string SessionName { get; set; }
	}
}
