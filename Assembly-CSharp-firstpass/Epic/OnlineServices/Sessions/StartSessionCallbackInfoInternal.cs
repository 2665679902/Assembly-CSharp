using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000657 RID: 1623
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StartSessionCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06003F6A RID: 16234 RVA: 0x00087684 File Offset: 0x00085884
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06003F6B RID: 16235 RVA: 0x000876A8 File Offset: 0x000858A8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06003F6C RID: 16236 RVA: 0x000876CA File Offset: 0x000858CA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001832 RID: 6194
		private Result m_ResultCode;

		// Token: 0x04001833 RID: 6195
		private IntPtr m_ClientData;
	}
}
