using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200090E RID: 2318
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyUserAuthCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x060050B8 RID: 20664 RVA: 0x000989BC File Offset: 0x00096BBC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x060050B9 RID: 20665 RVA: 0x000989E0 File Offset: 0x00096BE0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x060050BA RID: 20666 RVA: 0x00098A02 File Offset: 0x00096C02
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001F5E RID: 8030
		private Result m_ResultCode;

		// Token: 0x04001F5F RID: 8031
		private IntPtr m_ClientData;
	}
}
