using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200081D RID: 2077
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06004A45 RID: 19013 RVA: 0x00092310 File Offset: 0x00090510
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06004A46 RID: 19014 RVA: 0x00092334 File Offset: 0x00090534
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x00092356 File Offset: 0x00090556
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x00092360 File Offset: 0x00090560
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x00092384 File Offset: 0x00090584
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001CA2 RID: 7330
		private Result m_ResultCode;

		// Token: 0x04001CA3 RID: 7331
		private IntPtr m_ClientData;

		// Token: 0x04001CA4 RID: 7332
		private IntPtr m_LocalUserId;

		// Token: 0x04001CA5 RID: 7333
		private IntPtr m_TargetUserId;
	}
}
