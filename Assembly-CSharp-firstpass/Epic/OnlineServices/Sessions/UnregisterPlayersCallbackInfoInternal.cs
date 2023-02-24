using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200065B RID: 1627
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPlayersCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x00087784 File Offset: 0x00085984
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000877A8 File Offset: 0x000859A8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000877CA File Offset: 0x000859CA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001839 RID: 6201
		private Result m_ResultCode;

		// Token: 0x0400183A RID: 6202
		private IntPtr m_ClientData;
	}
}
