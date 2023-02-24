using System;

namespace YamlDotNet.Samples.Helpers
{
	// Token: 0x020001F8 RID: 504
	internal class SampleAttribute : Attribute
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0003DB94 File Offset: 0x0003BD94
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x0003DB9C File Offset: 0x0003BD9C
		public string DisplayName { get; private set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0003DBA5 File Offset: 0x0003BDA5
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x0003DBAD File Offset: 0x0003BDAD
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
				this.DisplayName = "Sample: " + value;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0003DBC7 File Offset: 0x0003BDC7
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x0003DBCF File Offset: 0x0003BDCF
		public string Description { get; set; }

		// Token: 0x0400087D RID: 2173
		private string title;
	}
}
