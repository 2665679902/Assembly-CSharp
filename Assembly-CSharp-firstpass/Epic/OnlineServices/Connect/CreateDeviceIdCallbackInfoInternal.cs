using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000896 RID: 2198
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateDeviceIdCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x000964B4 File Offset: 0x000946B4
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06004DFD RID: 19965 RVA: 0x000964D8 File Offset: 0x000946D8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004DFE RID: 19966 RVA: 0x000964FA File Offset: 0x000946FA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001E40 RID: 7744
		private Result m_ResultCode;

		// Token: 0x04001E41 RID: 7745
		private IntPtr m_ClientData;
	}
}
