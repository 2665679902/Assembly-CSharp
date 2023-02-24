using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020006D6 RID: 1750
	public class InitializeOptions
	{
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x0008A4EB File Offset: 0x000886EB
		public int ApiVersion
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06004276 RID: 17014 RVA: 0x0008A4EE File Offset: 0x000886EE
		// (set) Token: 0x06004277 RID: 17015 RVA: 0x0008A4F6 File Offset: 0x000886F6
		public AllocateMemoryFunc AllocateMemoryFunction { get; set; }

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06004278 RID: 17016 RVA: 0x0008A4FF File Offset: 0x000886FF
		// (set) Token: 0x06004279 RID: 17017 RVA: 0x0008A507 File Offset: 0x00088707
		public ReallocateMemoryFunc ReallocateMemoryFunction { get; set; }

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x0008A510 File Offset: 0x00088710
		// (set) Token: 0x0600427B RID: 17019 RVA: 0x0008A518 File Offset: 0x00088718
		public ReleaseMemoryFunc ReleaseMemoryFunction { get; set; }

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x0008A521 File Offset: 0x00088721
		// (set) Token: 0x0600427D RID: 17021 RVA: 0x0008A529 File Offset: 0x00088729
		public string ProductName { get; set; }

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x0008A532 File Offset: 0x00088732
		// (set) Token: 0x0600427F RID: 17023 RVA: 0x0008A53A File Offset: 0x0008873A
		public string ProductVersion { get; set; }

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06004280 RID: 17024 RVA: 0x0008A543 File Offset: 0x00088743
		// (set) Token: 0x06004281 RID: 17025 RVA: 0x0008A54B File Offset: 0x0008874B
		public IntPtr SystemInitializeOptions { get; set; }
	}
}
