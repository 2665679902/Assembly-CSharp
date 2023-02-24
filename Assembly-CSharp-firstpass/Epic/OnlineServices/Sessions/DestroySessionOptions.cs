using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005DB RID: 1499
	public class DestroySessionOptions
	{
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06003CA2 RID: 15522 RVA: 0x00084AD2 File Offset: 0x00082CD2
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06003CA3 RID: 15523 RVA: 0x00084AD5 File Offset: 0x00082CD5
		// (set) Token: 0x06003CA4 RID: 15524 RVA: 0x00084ADD File Offset: 0x00082CDD
		public string SessionName { get; set; }
	}
}
