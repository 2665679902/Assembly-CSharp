using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000787 RID: 1927
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchFindCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x0008FB00 File Offset: 0x0008DD00
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x0008FB24 File Offset: 0x0008DD24
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06004727 RID: 18215 RVA: 0x0008FB46 File Offset: 0x0008DD46
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001B9B RID: 7067
		private Result m_ResultCode;

		// Token: 0x04001B9C RID: 7068
		private IntPtr m_ClientData;
	}
}
