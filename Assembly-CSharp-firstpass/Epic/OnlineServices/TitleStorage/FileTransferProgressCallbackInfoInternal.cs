using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000583 RID: 1411
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileTransferProgressCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x00082788 File Offset: 0x00080988
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x000827AA File Offset: 0x000809AA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000827B4 File Offset: 0x000809B4
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06003A77 RID: 14967 RVA: 0x000827D8 File Offset: 0x000809D8
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000827FC File Offset: 0x000809FC
		public uint BytesTransferred
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_BytesTransferred, out @default);
				return @default;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06003A79 RID: 14969 RVA: 0x00082820 File Offset: 0x00080A20
		public uint TotalFileSizeBytes
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_TotalFileSizeBytes, out @default);
				return @default;
			}
		}

		// Token: 0x04001648 RID: 5704
		private IntPtr m_ClientData;

		// Token: 0x04001649 RID: 5705
		private IntPtr m_LocalUserId;

		// Token: 0x0400164A RID: 5706
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;

		// Token: 0x0400164B RID: 5707
		private uint m_BytesTransferred;

		// Token: 0x0400164C RID: 5708
		private uint m_TotalFileSizeBytes;
	}
}
