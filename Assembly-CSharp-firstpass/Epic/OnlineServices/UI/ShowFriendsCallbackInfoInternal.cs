using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000574 RID: 1396
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ShowFriendsCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x00081F60 File Offset: 0x00080160
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06003A04 RID: 14852 RVA: 0x00081F84 File Offset: 0x00080184
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x00081FA6 File Offset: 0x000801A6
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x00081FB0 File Offset: 0x000801B0
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001619 RID: 5657
		private Result m_ResultCode;

		// Token: 0x0400161A RID: 5658
		private IntPtr m_ClientData;

		// Token: 0x0400161B RID: 5659
		private IntPtr m_LocalUserId;
	}
}
