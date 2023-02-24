using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005BD RID: 1469
	public class ActiveSessionGetRegisteredPlayerByIndexOptions
	{
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x00083F53 File Offset: 0x00082153
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x00083F56 File Offset: 0x00082156
		// (set) Token: 0x06003BF9 RID: 15353 RVA: 0x00083F5E File Offset: 0x0008215E
		public uint PlayerIndex { get; set; }
	}
}
