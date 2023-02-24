using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200059F RID: 1439
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileOptionsInternal : IDisposable, IInitializable
	{
		// Token: 0x06003B14 RID: 15124 RVA: 0x00082E98 File Offset: 0x00081098
		public void Initialize()
		{
			this.m_ReadFileDataCallback = new OnReadFileDataCallbackInternal(TitleStorageInterface.OnReadFileData);
			this.m_FileTransferProgressCallback = new OnFileTransferProgressCallbackInternal(TitleStorageInterface.OnFileTransferProgress);
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x00082EC0 File Offset: 0x000810C0
		// (set) Token: 0x06003B16 RID: 15126 RVA: 0x00082EE2 File Offset: 0x000810E2
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

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x00082EF4 File Offset: 0x000810F4
		// (set) Token: 0x06003B18 RID: 15128 RVA: 0x00082F16 File Offset: 0x00081116
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

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x00082F28 File Offset: 0x00081128
		// (set) Token: 0x06003B1A RID: 15130 RVA: 0x00082F4A File Offset: 0x0008114A
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

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x00082F5C File Offset: 0x0008115C
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x00082F7E File Offset: 0x0008117E
		public uint ReadChunkLengthBytes
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_ReadChunkLengthBytes, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_ReadChunkLengthBytes, value);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x00082F8D File Offset: 0x0008118D
		public OnReadFileDataCallbackInternal ReadFileDataCallback
		{
			get
			{
				return this.m_ReadFileDataCallback;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x00082F95 File Offset: 0x00081195
		public OnFileTransferProgressCallbackInternal FileTransferProgressCallback
		{
			get
			{
				return this.m_FileTransferProgressCallback;
			}
		}

		// Token: 0x06003B1F RID: 15135 RVA: 0x00082F9D File Offset: 0x0008119D
		public void Dispose()
		{
		}

		// Token: 0x04001683 RID: 5763
		private int m_ApiVersion;

		// Token: 0x04001684 RID: 5764
		private IntPtr m_LocalUserId;

		// Token: 0x04001685 RID: 5765
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;

		// Token: 0x04001686 RID: 5766
		private uint m_ReadChunkLengthBytes;

		// Token: 0x04001687 RID: 5767
		private OnReadFileDataCallbackInternal m_ReadFileDataCallback;

		// Token: 0x04001688 RID: 5768
		private OnFileTransferProgressCallbackInternal m_FileTransferProgressCallback;
	}
}
