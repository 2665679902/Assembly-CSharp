using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000593 RID: 1427
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x00082904 File Offset: 0x00080B04
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x00082928 File Offset: 0x00080B28
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x0008294A File Offset: 0x00080B4A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x00082954 File Offset: 0x00080B54
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001653 RID: 5715
		private Result m_ResultCode;

		// Token: 0x04001654 RID: 5716
		private IntPtr m_ClientData;

		// Token: 0x04001655 RID: 5717
		private IntPtr m_LocalUserId;
	}
}
