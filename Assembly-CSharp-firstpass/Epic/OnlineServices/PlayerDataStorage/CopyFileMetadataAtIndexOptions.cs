using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000697 RID: 1687
	public class CopyFileMetadataAtIndexOptions
	{
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x00088DFF File Offset: 0x00086FFF
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060040E4 RID: 16612 RVA: 0x00088E02 File Offset: 0x00087002
		// (set) Token: 0x060040E5 RID: 16613 RVA: 0x00088E0A File Offset: 0x0008700A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x00088E13 File Offset: 0x00087013
		// (set) Token: 0x060040E7 RID: 16615 RVA: 0x00088E1B File Offset: 0x0008701B
		public uint Index { get; set; }
	}
}
