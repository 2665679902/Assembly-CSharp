using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008AD RID: 2221
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004E81 RID: 20097 RVA: 0x00096CB8 File Offset: 0x00094EB8
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x00096CDC File Offset: 0x00094EDC
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004E83 RID: 20099 RVA: 0x00096CFE File Offset: 0x00094EFE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004E84 RID: 20100 RVA: 0x00096D08 File Offset: 0x00094F08
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001E84 RID: 7812
		private Result m_ResultCode;

		// Token: 0x04001E85 RID: 7813
		private IntPtr m_ClientData;

		// Token: 0x04001E86 RID: 7814
		private IntPtr m_LocalUserId;
	}
}
