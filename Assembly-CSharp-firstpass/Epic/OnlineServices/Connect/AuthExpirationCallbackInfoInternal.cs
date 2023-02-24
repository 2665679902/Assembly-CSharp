using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200088B RID: 2187
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AuthExpirationCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x06004D89 RID: 19849 RVA: 0x00095968 File Offset: 0x00093B68
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x06004D8A RID: 19850 RVA: 0x0009598A File Offset: 0x00093B8A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x06004D8B RID: 19851 RVA: 0x00095994 File Offset: 0x00093B94
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001E0E RID: 7694
		private IntPtr m_ClientData;

		// Token: 0x04001E0F RID: 7695
		private IntPtr m_LocalUserId;
	}
}
