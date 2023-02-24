using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200059D RID: 1437
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileDataCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x00082D54 File Offset: 0x00080F54
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x00082D76 File Offset: 0x00080F76
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x00082D80 File Offset: 0x00080F80
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x00082DA4 File Offset: 0x00080FA4
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x00082DC8 File Offset: 0x00080FC8
		public uint TotalFileSizeBytes
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_TotalFileSizeBytes, out @default);
				return @default;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x00082DEC File Offset: 0x00080FEC
		public bool IsLastChunk
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_IsLastChunk, out @default);
				return @default;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x00082E10 File Offset: 0x00081010
		public byte[] DataChunk
		{
			get
			{
				byte[] @default = Helper.GetDefault<byte[]>();
				Helper.TryMarshalGet<byte>(this.m_DataChunk, out @default, this.m_DataChunkLengthBytes);
				return @default;
			}
		}

		// Token: 0x04001677 RID: 5751
		private IntPtr m_ClientData;

		// Token: 0x04001678 RID: 5752
		private IntPtr m_LocalUserId;

		// Token: 0x04001679 RID: 5753
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;

		// Token: 0x0400167A RID: 5754
		private uint m_TotalFileSizeBytes;

		// Token: 0x0400167B RID: 5755
		private int m_IsLastChunk;

		// Token: 0x0400167C RID: 5756
		private uint m_DataChunkLengthBytes;

		// Token: 0x0400167D RID: 5757
		private IntPtr m_DataChunk;
	}
}
