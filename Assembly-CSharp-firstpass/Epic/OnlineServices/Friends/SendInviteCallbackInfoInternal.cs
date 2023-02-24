using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000821 RID: 2081
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x000924BC File Offset: 0x000906BC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x000924E0 File Offset: 0x000906E0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x00092502 File Offset: 0x00090702
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06004A63 RID: 19043 RVA: 0x0009250C File Offset: 0x0009070C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x00092530 File Offset: 0x00090730
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001CAF RID: 7343
		private Result m_ResultCode;

		// Token: 0x04001CB0 RID: 7344
		private IntPtr m_ClientData;

		// Token: 0x04001CB1 RID: 7345
		private IntPtr m_LocalUserId;

		// Token: 0x04001CB2 RID: 7346
		private IntPtr m_TargetUserId;
	}
}
