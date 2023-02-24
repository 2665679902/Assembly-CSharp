using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008B1 RID: 2225
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004E9B RID: 20123 RVA: 0x00096E40 File Offset: 0x00095040
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004E9C RID: 20124 RVA: 0x00096E64 File Offset: 0x00095064
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004E9D RID: 20125 RVA: 0x00096E86 File Offset: 0x00095086
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004E9E RID: 20126 RVA: 0x00096E90 File Offset: 0x00095090
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004E9F RID: 20127 RVA: 0x00096EB4 File Offset: 0x000950B4
		public ContinuanceToken ContinuanceToken
		{
			get
			{
				ContinuanceToken @default = Helper.GetDefault<ContinuanceToken>();
				Helper.TryMarshalGet<ContinuanceToken>(this.m_ContinuanceToken, out @default);
				return @default;
			}
		}

		// Token: 0x04001E90 RID: 7824
		private Result m_ResultCode;

		// Token: 0x04001E91 RID: 7825
		private IntPtr m_ClientData;

		// Token: 0x04001E92 RID: 7826
		private IntPtr m_LocalUserId;

		// Token: 0x04001E93 RID: 7827
		private IntPtr m_ContinuanceToken;
	}
}
