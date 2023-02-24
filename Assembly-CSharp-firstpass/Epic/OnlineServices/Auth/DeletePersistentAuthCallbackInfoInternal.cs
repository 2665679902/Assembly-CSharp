using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008EA RID: 2282
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeletePersistentAuthCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06004FD3 RID: 20435 RVA: 0x00097E4C File Offset: 0x0009604C
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06004FD4 RID: 20436 RVA: 0x00097E70 File Offset: 0x00096070
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004FD5 RID: 20437 RVA: 0x00097E92 File Offset: 0x00096092
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x04001EFC RID: 7932
		private Result m_ResultCode;

		// Token: 0x04001EFD RID: 7933
		private IntPtr m_ClientData;
	}
}
