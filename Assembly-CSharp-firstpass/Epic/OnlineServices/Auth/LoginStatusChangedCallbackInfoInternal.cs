using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008F8 RID: 2296
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginStatusChangedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06005028 RID: 20520 RVA: 0x00098380 File Offset: 0x00096580
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x000983A2 File Offset: 0x000965A2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x000983AC File Offset: 0x000965AC
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x000983D0 File Offset: 0x000965D0
		public LoginStatus PrevStatus
		{
			get
			{
				LoginStatus @default = Helper.GetDefault<LoginStatus>();
				Helper.TryMarshalGet<LoginStatus>(this.m_PrevStatus, out @default);
				return @default;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x000983F4 File Offset: 0x000965F4
		public LoginStatus CurrentStatus
		{
			get
			{
				LoginStatus @default = Helper.GetDefault<LoginStatus>();
				Helper.TryMarshalGet<LoginStatus>(this.m_CurrentStatus, out @default);
				return @default;
			}
		}

		// Token: 0x04001F31 RID: 7985
		private IntPtr m_ClientData;

		// Token: 0x04001F32 RID: 7986
		private IntPtr m_LocalUserId;

		// Token: 0x04001F33 RID: 7987
		private LoginStatus m_PrevStatus;

		// Token: 0x04001F34 RID: 7988
		private LoginStatus m_CurrentStatus;
	}
}
