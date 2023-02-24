using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E0 RID: 1504
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x00084C08 File Offset: 0x00082E08
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x00084C2C File Offset: 0x00082E2C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x00084C4E File Offset: 0x00082E4E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001728 RID: 5928
		private Result m_ResultCode;

		// Token: 0x04001729 RID: 5929
		private IntPtr m_ClientData;
	}
}
