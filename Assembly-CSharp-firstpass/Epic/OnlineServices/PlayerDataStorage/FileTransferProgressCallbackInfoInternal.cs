using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006A6 RID: 1702
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileTransferProgressCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600414D RID: 16717 RVA: 0x00089430 File Offset: 0x00087630
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600414E RID: 16718 RVA: 0x00089452 File Offset: 0x00087652
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600414F RID: 16719 RVA: 0x0008945C File Offset: 0x0008765C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06004150 RID: 16720 RVA: 0x00089480 File Offset: 0x00087680
		public string Filename
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Filename, out @default);
				return @default;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x000894A4 File Offset: 0x000876A4
		public uint BytesTransferred
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_BytesTransferred, out @default);
				return @default;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06004152 RID: 16722 RVA: 0x000894C8 File Offset: 0x000876C8
		public uint TotalFileSizeBytes
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_TotalFileSizeBytes, out @default);
				return @default;
			}
		}

		// Token: 0x04001903 RID: 6403
		private IntPtr m_ClientData;

		// Token: 0x04001904 RID: 6404
		private IntPtr m_LocalUserId;

		// Token: 0x04001905 RID: 6405
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Filename;

		// Token: 0x04001906 RID: 6406
		private uint m_BytesTransferred;

		// Token: 0x04001907 RID: 6407
		private uint m_TotalFileSizeBytes;
	}
}
