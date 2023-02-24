using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000691 RID: 1681
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPresenceCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x00088CC4 File Offset: 0x00086EC4
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060040CB RID: 16587 RVA: 0x00088CE8 File Offset: 0x00086EE8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060040CC RID: 16588 RVA: 0x00088D0A File Offset: 0x00086F0A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060040CD RID: 16589 RVA: 0x00088D14 File Offset: 0x00086F14
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x040018C7 RID: 6343
		private Result m_ResultCode;

		// Token: 0x040018C8 RID: 6344
		private IntPtr m_ClientData;

		// Token: 0x040018C9 RID: 6345
		private IntPtr m_LocalUserId;
	}
}
