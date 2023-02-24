using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006D1 RID: 1745
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WriteFileOptionsInternal : IDisposable, IInitializable
	{
		// Token: 0x0600425B RID: 16987 RVA: 0x0008A352 File Offset: 0x00088552
		public void Initialize()
		{
			this.m_WriteFileDataCallback = new OnWriteFileDataCallbackInternal(PlayerDataStorageInterface.OnWriteFileData);
			this.m_FileTransferProgressCallback = new OnFileTransferProgressCallbackInternal(PlayerDataStorageInterface.OnFileTransferProgress);
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600425C RID: 16988 RVA: 0x0008A378 File Offset: 0x00088578
		// (set) Token: 0x0600425D RID: 16989 RVA: 0x0008A39A File Offset: 0x0008859A
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

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600425E RID: 16990 RVA: 0x0008A3AC File Offset: 0x000885AC
		// (set) Token: 0x0600425F RID: 16991 RVA: 0x0008A3CE File Offset: 0x000885CE
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

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06004260 RID: 16992 RVA: 0x0008A3E0 File Offset: 0x000885E0
		// (set) Token: 0x06004261 RID: 16993 RVA: 0x0008A402 File Offset: 0x00088602
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

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06004262 RID: 16994 RVA: 0x0008A414 File Offset: 0x00088614
		// (set) Token: 0x06004263 RID: 16995 RVA: 0x0008A436 File Offset: 0x00088636
		public uint ChunkLengthBytes
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_ChunkLengthBytes, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_ChunkLengthBytes, value);
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06004264 RID: 16996 RVA: 0x0008A445 File Offset: 0x00088645
		public OnWriteFileDataCallbackInternal WriteFileDataCallback
		{
			get
			{
				return this.m_WriteFileDataCallback;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06004265 RID: 16997 RVA: 0x0008A44D File Offset: 0x0008864D
		public OnFileTransferProgressCallbackInternal FileTransferProgressCallback
		{
			get
			{
				return this.m_FileTransferProgressCallback;
			}
		}

		// Token: 0x06004266 RID: 16998 RVA: 0x0008A455 File Offset: 0x00088655
		public void Dispose()
		{
		}

		// Token: 0x04001966 RID: 6502
		private int m_ApiVersion;

		// Token: 0x04001967 RID: 6503
		private IntPtr m_LocalUserId;

		// Token: 0x04001968 RID: 6504
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;

		// Token: 0x04001969 RID: 6505
		private uint m_ChunkLengthBytes;

		// Token: 0x0400196A RID: 6506
		private OnWriteFileDataCallbackInternal m_WriteFileDataCallback;

		// Token: 0x0400196B RID: 6507
		private OnFileTransferProgressCallbackInternal m_FileTransferProgressCallback;
	}
}
