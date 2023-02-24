using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006A1 RID: 1697
	public class DuplicateFileOptions
	{
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x000891B6 File Offset: 0x000873B6
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06004121 RID: 16673 RVA: 0x000891B9 File Offset: 0x000873B9
		// (set) Token: 0x06004122 RID: 16674 RVA: 0x000891C1 File Offset: 0x000873C1
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06004123 RID: 16675 RVA: 0x000891CA File Offset: 0x000873CA
		// (set) Token: 0x06004124 RID: 16676 RVA: 0x000891D2 File Offset: 0x000873D2
		public string SourceFilename { get; set; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x000891DB File Offset: 0x000873DB
		// (set) Token: 0x06004126 RID: 16678 RVA: 0x000891E3 File Offset: 0x000873E3
		public string DestinationFilename { get; set; }
	}
}
