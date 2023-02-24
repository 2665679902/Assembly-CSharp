using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008FA RID: 2298
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogoutCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x00098454 File Offset: 0x00096654
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x06005035 RID: 20533 RVA: 0x00098478 File Offset: 0x00096678
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06005036 RID: 20534 RVA: 0x0009849A File Offset: 0x0009669A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06005037 RID: 20535 RVA: 0x000984A4 File Offset: 0x000966A4
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001F38 RID: 7992
		private Result m_ResultCode;

		// Token: 0x04001F39 RID: 7993
		private IntPtr m_ClientData;

		// Token: 0x04001F3A RID: 7994
		private IntPtr m_LocalUserId;
	}
}
