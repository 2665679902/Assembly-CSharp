using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005EC RID: 1516
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinSessionCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x00084FC8 File Offset: 0x000831C8
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06003CF9 RID: 15609 RVA: 0x00084FEC File Offset: 0x000831EC
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x0008500E File Offset: 0x0008320E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001742 RID: 5954
		private Result m_ResultCode;

		// Token: 0x04001743 RID: 5955
		private IntPtr m_ClientData;
	}
}
