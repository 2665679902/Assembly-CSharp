using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006A2 RID: 1698
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DuplicateFileOptionsInternal : IDisposable
	{
		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06004128 RID: 16680 RVA: 0x000891F4 File Offset: 0x000873F4
		// (set) Token: 0x06004129 RID: 16681 RVA: 0x00089216 File Offset: 0x00087416
		public int ApiVersion
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ApiVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ApiVersion, value);
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x00089228 File Offset: 0x00087428
		// (set) Token: 0x0600412B RID: 16683 RVA: 0x0008924A File Offset: 0x0008744A
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x0008925C File Offset: 0x0008745C
		// (set) Token: 0x0600412D RID: 16685 RVA: 0x0008927E File Offset: 0x0008747E
		public string SourceFilename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SourceFilename, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SourceFilename, value);
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x00089290 File Offset: 0x00087490
		// (set) Token: 0x0600412F RID: 16687 RVA: 0x000892B2 File Offset: 0x000874B2
		public string DestinationFilename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DestinationFilename, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DestinationFilename, value);
			}
		}

		// Token: 0x06004130 RID: 16688 RVA: 0x000892C1 File Offset: 0x000874C1
		public void Dispose()
		{
		}

		// Token: 0x040018F3 RID: 6387
		private int m_ApiVersion;

		// Token: 0x040018F4 RID: 6388
		private IntPtr m_LocalUserId;

		// Token: 0x040018F5 RID: 6389
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SourceFilename;

		// Token: 0x040018F6 RID: 6390
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DestinationFilename;
	}
}
