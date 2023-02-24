using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000581 RID: 1409
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileMetadataInternal : IDisposable
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x0008265C File Offset: 0x0008085C
		// (set) Token: 0x06003A61 RID: 14945 RVA: 0x0008267E File Offset: 0x0008087E
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

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x00082690 File Offset: 0x00080890
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x000826B2 File Offset: 0x000808B2
		public uint FileSizeBytes
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_FileSizeBytes, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_FileSizeBytes, value);
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000826C4 File Offset: 0x000808C4
		// (set) Token: 0x06003A65 RID: 14949 RVA: 0x000826E6 File Offset: 0x000808E6
		public string MD5Hash
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_MD5Hash, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_MD5Hash, value);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x000826F8 File Offset: 0x000808F8
		// (set) Token: 0x06003A67 RID: 14951 RVA: 0x0008271A File Offset: 0x0008091A
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

		// Token: 0x06003A68 RID: 14952 RVA: 0x00082729 File Offset: 0x00080929
		public void Dispose()
		{
		}

		// Token: 0x0400163F RID: 5695
		private int m_ApiVersion;

		// Token: 0x04001640 RID: 5696
		private uint m_FileSizeBytes;

		// Token: 0x04001641 RID: 5697
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_MD5Hash;

		// Token: 0x04001642 RID: 5698
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}
