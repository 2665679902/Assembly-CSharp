using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x020006BE RID: 1726
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x060041D1 RID: 16849 RVA: 0x00089AEC File Offset: 0x00087CEC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x00089B10 File Offset: 0x00087D10
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x00089B32 File Offset: 0x00087D32
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x00089B3C File Offset: 0x00087D3C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x0400191A RID: 6426
		private Result m_ResultCode;

		// Token: 0x0400191B RID: 6427
		private IntPtr m_ClientData;

		// Token: 0x0400191C RID: 6428
		private IntPtr m_LocalUserId;
	}
}
