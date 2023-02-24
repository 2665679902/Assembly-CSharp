using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006C8 RID: 1736
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileDataCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06004214 RID: 16916 RVA: 0x00089EE0 File Offset: 0x000880E0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06004215 RID: 16917 RVA: 0x00089F02 File Offset: 0x00088102
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06004216 RID: 16918 RVA: 0x00089F0C File Offset: 0x0008810C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06004217 RID: 16919 RVA: 0x00089F30 File Offset: 0x00088130
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06004218 RID: 16920 RVA: 0x00089F54 File Offset: 0x00088154
		public uint TotalFileSizeBytes
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_TotalFileSizeBytes, out @default);
				return @default;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x00089F78 File Offset: 0x00088178
		public bool IsLastChunk
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_IsLastChunk, out @default);
				return @default;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x0600421A RID: 16922 RVA: 0x00089F9C File Offset: 0x0008819C
		public byte[] DataChunk
		{
			get
			{
				byte[] @default = Helper.GetDefault<byte[]>();
				Helper.TryMarshalGet<byte>(this.m_DataChunk, out @default, this.m_DataChunkLengthBytes);
				return @default;
			}
		}

		// Token: 0x0400193B RID: 6459
		private IntPtr m_ClientData;

		// Token: 0x0400193C RID: 6460
		private IntPtr m_LocalUserId;

		// Token: 0x0400193D RID: 6461
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;

		// Token: 0x0400193E RID: 6462
		private uint m_TotalFileSizeBytes;

		// Token: 0x0400193F RID: 6463
		private int m_IsLastChunk;

		// Token: 0x04001940 RID: 6464
		private uint m_DataChunkLengthBytes;

		// Token: 0x04001941 RID: 6465
		private IntPtr m_DataChunk;
	}
}
