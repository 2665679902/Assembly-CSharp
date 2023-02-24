using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x020007FF RID: 2047
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcceptInviteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600499D RID: 18845 RVA: 0x0009199C File Offset: 0x0008FB9C
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x0600499E RID: 18846 RVA: 0x000919C0 File Offset: 0x0008FBC0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600499F RID: 18847 RVA: 0x000919E2 File Offset: 0x0008FBE2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x000919EC File Offset: 0x0008FBEC
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x00091A10 File Offset: 0x0008FC10
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001C66 RID: 7270
		private Result m_ResultCode;

		// Token: 0x04001C67 RID: 7271
		private IntPtr m_ClientData;

		// Token: 0x04001C68 RID: 7272
		private IntPtr m_LocalUserId;

		// Token: 0x04001C69 RID: 7273
		private IntPtr m_TargetUserId;
	}
}
