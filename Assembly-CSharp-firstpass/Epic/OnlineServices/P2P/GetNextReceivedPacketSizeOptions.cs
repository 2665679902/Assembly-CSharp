using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006EB RID: 1771
	public class GetNextReceivedPacketSizeOptions
	{
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x0008B3FF File Offset: 0x000895FF
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x0008B402 File Offset: 0x00089602
		// (set) Token: 0x0600435A RID: 17242 RVA: 0x0008B40A File Offset: 0x0008960A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x0008B413 File Offset: 0x00089613
		// (set) Token: 0x0600435C RID: 17244 RVA: 0x0008B41B File Offset: 0x0008961B
		public byte? RequestedChannel { get; set; }
	}
}
