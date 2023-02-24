using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005DA RID: 1498
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroySessionCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06003C9F RID: 15519 RVA: 0x00084A84 File Offset: 0x00082C84
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06003CA0 RID: 15520 RVA: 0x00084AA8 File Offset: 0x00082CA8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06003CA1 RID: 15521 RVA: 0x00084ACA File Offset: 0x00082CCA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x0400171E RID: 5918
		private Result m_ResultCode;

		// Token: 0x0400171F RID: 5919
		private IntPtr m_ClientData;
	}
}
