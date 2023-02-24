using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200089A RID: 2202
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06004E0F RID: 19983 RVA: 0x000965C4 File Offset: 0x000947C4
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004E10 RID: 19984 RVA: 0x000965E8 File Offset: 0x000947E8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004E11 RID: 19985 RVA: 0x0009660A File Offset: 0x0009480A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x00096614 File Offset: 0x00094814
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001E48 RID: 7752
		private Result m_ResultCode;

		// Token: 0x04001E49 RID: 7753
		private IntPtr m_ClientData;

		// Token: 0x04001E4A RID: 7754
		private IntPtr m_LocalUserId;
	}
}
