using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008B5 RID: 2229
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginStatusChangedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004EB6 RID: 20150 RVA: 0x00097004 File Offset: 0x00095204
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004EB7 RID: 20151 RVA: 0x00097026 File Offset: 0x00095226
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004EB8 RID: 20152 RVA: 0x00097030 File Offset: 0x00095230
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004EB9 RID: 20153 RVA: 0x00097054 File Offset: 0x00095254
		public LoginStatus PreviousStatus
		{
			get
			{
				LoginStatus @default = Helper.GetDefault<LoginStatus>();
				Helper.TryMarshalGet<LoginStatus>(this.m_PreviousStatus, out @default);
				return @default;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004EBA RID: 20154 RVA: 0x00097078 File Offset: 0x00095278
		public LoginStatus CurrentStatus
		{
			get
			{
				LoginStatus @default = Helper.GetDefault<LoginStatus>();
				Helper.TryMarshalGet<LoginStatus>(this.m_CurrentStatus, out @default);
				return @default;
			}
		}

		// Token: 0x04001E9D RID: 7837
		private IntPtr m_ClientData;

		// Token: 0x04001E9E RID: 7838
		private IntPtr m_LocalUserId;

		// Token: 0x04001E9F RID: 7839
		private LoginStatus m_PreviousStatus;

		// Token: 0x04001EA0 RID: 7840
		private LoginStatus m_CurrentStatus;
	}
}
