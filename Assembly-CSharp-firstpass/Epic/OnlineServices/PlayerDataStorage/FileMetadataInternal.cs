using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006A4 RID: 1700
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileMetadataInternal : IDisposable
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06004139 RID: 16697 RVA: 0x00089304 File Offset: 0x00087504
		// (set) Token: 0x0600413A RID: 16698 RVA: 0x00089326 File Offset: 0x00087526
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

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600413B RID: 16699 RVA: 0x00089338 File Offset: 0x00087538
		// (set) Token: 0x0600413C RID: 16700 RVA: 0x0008935A File Offset: 0x0008755A
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

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x0600413D RID: 16701 RVA: 0x0008936C File Offset: 0x0008756C
		// (set) Token: 0x0600413E RID: 16702 RVA: 0x0008938E File Offset: 0x0008758E
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

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600413F RID: 16703 RVA: 0x000893A0 File Offset: 0x000875A0
		// (set) Token: 0x06004140 RID: 16704 RVA: 0x000893C2 File Offset: 0x000875C2
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

		// Token: 0x06004141 RID: 16705 RVA: 0x000893D1 File Offset: 0x000875D1
		public void Dispose()
		{
		}

		// Token: 0x040018FA RID: 6394
		private int m_ApiVersion;

		// Token: 0x040018FB RID: 6395
		private uint m_FileSizeBytes;

		// Token: 0x040018FC RID: 6396
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_MD5Hash;

		// Token: 0x040018FD RID: 6397
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;
	}
}
