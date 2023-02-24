using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006CA RID: 1738
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileOptionsInternal : IDisposable, IInitializable
	{
		// Token: 0x06004227 RID: 16935 RVA: 0x0008A024 File Offset: 0x00088224
		public void Initialize()
		{
			this.m_ReadFileDataCallback = new OnReadFileDataCallbackInternal(PlayerDataStorageInterface.OnReadFileData);
			this.m_FileTransferProgressCallback = new OnFileTransferProgressCallbackInternal(PlayerDataStorageInterface.OnFileTransferProgress);
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x0008A04C File Offset: 0x0008824C
		// (set) Token: 0x06004229 RID: 16937 RVA: 0x0008A06E File Offset: 0x0008826E
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

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x0008A080 File Offset: 0x00088280
		// (set) Token: 0x0600422B RID: 16939 RVA: 0x0008A0A2 File Offset: 0x000882A2
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

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x0008A0B4 File Offset: 0x000882B4
		// (set) Token: 0x0600422D RID: 16941 RVA: 0x0008A0D6 File Offset: 0x000882D6
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

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600422E RID: 16942 RVA: 0x0008A0E8 File Offset: 0x000882E8
		// (set) Token: 0x0600422F RID: 16943 RVA: 0x0008A10A File Offset: 0x0008830A
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

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x0008A119 File Offset: 0x00088319
		public OnReadFileDataCallbackInternal ReadFileDataCallback
		{
			get
			{
				return this.m_ReadFileDataCallback;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06004231 RID: 16945 RVA: 0x0008A121 File Offset: 0x00088321
		public OnFileTransferProgressCallbackInternal FileTransferProgressCallback
		{
			get
			{
				return this.m_FileTransferProgressCallback;
			}
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x0008A129 File Offset: 0x00088329
		public void Dispose()
		{
		}

		// Token: 0x04001947 RID: 6471
		private int m_ApiVersion;

		// Token: 0x04001948 RID: 6472
		private IntPtr m_LocalUserId;

		// Token: 0x04001949 RID: 6473
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;

		// Token: 0x0400194A RID: 6474
		private uint m_ReadChunkLengthBytes;

		// Token: 0x0400194B RID: 6475
		private OnReadFileDataCallbackInternal m_ReadFileDataCallback;

		// Token: 0x0400194C RID: 6476
		private OnFileTransferProgressCallbackInternal m_FileTransferProgressCallback;
	}
}
