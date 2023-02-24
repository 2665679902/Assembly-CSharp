using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200069C RID: 1692
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteFileCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x00088FCC File Offset: 0x000871CC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x00088FF0 File Offset: 0x000871F0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x00089012 File Offset: 0x00087212
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06004107 RID: 16647 RVA: 0x0008901C File Offset: 0x0008721C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x040018E2 RID: 6370
		private Result m_ResultCode;

		// Token: 0x040018E3 RID: 6371
		private IntPtr m_ClientData;

		// Token: 0x040018E4 RID: 6372
		private IntPtr m_LocalUserId;
	}
}
