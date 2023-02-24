using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200093F RID: 2367
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryPlayerAchievementsCompleteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x0600520F RID: 21007 RVA: 0x0009A034 File Offset: 0x00098234
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06005210 RID: 21008 RVA: 0x0009A058 File Offset: 0x00098258
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06005211 RID: 21009 RVA: 0x0009A07A File Offset: 0x0009827A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06005212 RID: 21010 RVA: 0x0009A084 File Offset: 0x00098284
		public ProductUserId UserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001FEA RID: 8170
		private Result m_ResultCode;

		// Token: 0x04001FEB RID: 8171
		private IntPtr m_ClientData;

		// Token: 0x04001FEC RID: 8172
		private IntPtr m_UserId;
	}
}
