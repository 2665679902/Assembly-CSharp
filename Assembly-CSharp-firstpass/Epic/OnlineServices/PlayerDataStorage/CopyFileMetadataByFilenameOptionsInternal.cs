using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200069A RID: 1690
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataByFilenameOptionsInternal : IDisposable
	{
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x00088EF4 File Offset: 0x000870F4
		// (set) Token: 0x060040F7 RID: 16631 RVA: 0x00088F16 File Offset: 0x00087116
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

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x00088F28 File Offset: 0x00087128
		// (set) Token: 0x060040F9 RID: 16633 RVA: 0x00088F4A File Offset: 0x0008714A
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

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060040FA RID: 16634 RVA: 0x00088F5C File Offset: 0x0008715C
		// (set) Token: 0x060040FB RID: 16635 RVA: 0x00088F7E File Offset: 0x0008717E
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Filename, value);
			}
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x00088F8D File Offset: 0x0008718D
		public void Dispose()
		{
		}

		// Token: 0x040018DC RID: 6364
		private int m_ApiVersion;

		// Token: 0x040018DD RID: 6365
		private IntPtr m_LocalUserId;

		// Token: 0x040018DE RID: 6366
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}
