using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000702 RID: 1794
	public class ReceivePacketOptions
	{
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060043DA RID: 17370 RVA: 0x0008BCE7 File Offset: 0x00089EE7
		public int ApiVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x060043DB RID: 17371 RVA: 0x0008BCEA File Offset: 0x00089EEA
		// (set) Token: 0x060043DC RID: 17372 RVA: 0x0008BCF2 File Offset: 0x00089EF2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x0008BCFB File Offset: 0x00089EFB
		// (set) Token: 0x060043DE RID: 17374 RVA: 0x0008BD03 File Offset: 0x00089F03
		public uint MaxDataSizeBytes { get; set; }

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x0008BD0C File Offset: 0x00089F0C
		// (set) Token: 0x060043E0 RID: 17376 RVA: 0x0008BD14 File Offset: 0x00089F14
		public byte? RequestedChannel { get; set; }
	}
}
