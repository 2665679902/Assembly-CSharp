using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200086E RID: 2158
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryEntitlementsCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x00094E70 File Offset: 0x00093070
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x00094E94 File Offset: 0x00093094
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004CE1 RID: 19681 RVA: 0x00094EB6 File Offset: 0x000930B6
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x00094EC0 File Offset: 0x000930C0
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001DC3 RID: 7619
		private Result m_ResultCode;

		// Token: 0x04001DC4 RID: 7620
		private IntPtr m_ClientData;

		// Token: 0x04001DC5 RID: 7621
		private IntPtr m_LocalUserId;
	}
}
