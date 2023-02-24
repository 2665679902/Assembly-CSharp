using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200060C RID: 1548
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x000851A8 File Offset: 0x000833A8
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06003D80 RID: 15744 RVA: 0x000851CC File Offset: 0x000833CC
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06003D81 RID: 15745 RVA: 0x000851EE File Offset: 0x000833EE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06003D82 RID: 15746 RVA: 0x000851F8 File Offset: 0x000833F8
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x0400175D RID: 5981
		private Result m_ResultCode;

		// Token: 0x0400175E RID: 5982
		private IntPtr m_ClientData;

		// Token: 0x0400175F RID: 5983
		private IntPtr m_LocalUserId;
	}
}
