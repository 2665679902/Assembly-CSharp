using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008CD RID: 2253
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryExternalAccountMappingsCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x000970D8 File Offset: 0x000952D8
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004F1B RID: 20251 RVA: 0x000970FC File Offset: 0x000952FC
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004F1C RID: 20252 RVA: 0x0009711E File Offset: 0x0009531E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06004F1D RID: 20253 RVA: 0x00097128 File Offset: 0x00095328
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001EA4 RID: 7844
		private Result m_ResultCode;

		// Token: 0x04001EA5 RID: 7845
		private IntPtr m_ClientData;

		// Token: 0x04001EA6 RID: 7846
		private IntPtr m_LocalUserId;
	}
}
