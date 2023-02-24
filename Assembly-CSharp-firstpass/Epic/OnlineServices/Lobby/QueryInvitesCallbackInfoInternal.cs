using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007BF RID: 1983
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600481E RID: 18462 RVA: 0x000901B0 File Offset: 0x0008E3B0
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600481F RID: 18463 RVA: 0x000901D4 File Offset: 0x0008E3D4
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06004820 RID: 18464 RVA: 0x000901F6 File Offset: 0x0008E3F6
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06004821 RID: 18465 RVA: 0x00090200 File Offset: 0x0008E400
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001BC8 RID: 7112
		private Result m_ResultCode;

		// Token: 0x04001BC9 RID: 7113
		private IntPtr m_ClientData;

		// Token: 0x04001BCA RID: 7114
		private IntPtr m_LocalUserId;
	}
}
