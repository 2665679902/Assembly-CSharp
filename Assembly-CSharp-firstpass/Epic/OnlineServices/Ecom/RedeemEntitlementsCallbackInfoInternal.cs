using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200087E RID: 2174
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RedeemEntitlementsCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06004D51 RID: 19793 RVA: 0x000955D4 File Offset: 0x000937D4
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06004D52 RID: 19794 RVA: 0x000955F8 File Offset: 0x000937F8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06004D53 RID: 19795 RVA: 0x0009561A File Offset: 0x0009381A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06004D54 RID: 19796 RVA: 0x00095624 File Offset: 0x00093824
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001DFD RID: 7677
		private Result m_ResultCode;

		// Token: 0x04001DFE RID: 7678
		private IntPtr m_ClientData;

		// Token: 0x04001DFF RID: 7679
		private IntPtr m_LocalUserId;
	}
}
