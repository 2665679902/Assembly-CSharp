using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000705 RID: 1797
	public class SendPacketOptions
	{
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x0008BE03 File Offset: 0x0008A003
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x0008BE06 File Offset: 0x0008A006
		// (set) Token: 0x060043ED RID: 17389 RVA: 0x0008BE0E File Offset: 0x0008A00E
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x0008BE17 File Offset: 0x0008A017
		// (set) Token: 0x060043EF RID: 17391 RVA: 0x0008BE1F File Offset: 0x0008A01F
		public ProductUserId RemoteUserId { get; set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x060043F0 RID: 17392 RVA: 0x0008BE28 File Offset: 0x0008A028
		// (set) Token: 0x060043F1 RID: 17393 RVA: 0x0008BE30 File Offset: 0x0008A030
		public SocketId SocketId { get; set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x0008BE39 File Offset: 0x0008A039
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x0008BE41 File Offset: 0x0008A041
		public byte Channel { get; set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x0008BE4A File Offset: 0x0008A04A
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x0008BE52 File Offset: 0x0008A052
		public byte[] Data { get; set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x060043F6 RID: 17398 RVA: 0x0008BE5B File Offset: 0x0008A05B
		// (set) Token: 0x060043F7 RID: 17399 RVA: 0x0008BE63 File Offset: 0x0008A063
		public bool AllowDelayedDelivery { get; set; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060043F8 RID: 17400 RVA: 0x0008BE6C File Offset: 0x0008A06C
		// (set) Token: 0x060043F9 RID: 17401 RVA: 0x0008BE74 File Offset: 0x0008A074
		public PacketReliability Reliability { get; set; }
	}
}
