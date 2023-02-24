using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000562 RID: 1378
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HideFriendsCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x00081C74 File Offset: 0x0007FE74
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060039BB RID: 14779 RVA: 0x00081C98 File Offset: 0x0007FE98
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x00081CBA File Offset: 0x0007FEBA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x00081CC4 File Offset: 0x0007FEC4
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x0400158F RID: 5519
		private Result m_ResultCode;

		// Token: 0x04001590 RID: 5520
		private IntPtr m_ClientData;

		// Token: 0x04001591 RID: 5521
		private IntPtr m_LocalUserId;
	}
}
