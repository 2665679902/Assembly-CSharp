using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008A0 RID: 2208
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteDeviceIdCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06004E2E RID: 20014 RVA: 0x000967B0 File Offset: 0x000949B0
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06004E2F RID: 20015 RVA: 0x000967D4 File Offset: 0x000949D4
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x000967F6 File Offset: 0x000949F6
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001E55 RID: 7765
		private Result m_ResultCode;

		// Token: 0x04001E56 RID: 7766
		private IntPtr m_ClientData;
	}
}
