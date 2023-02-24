using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000553 RID: 1363
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06003952 RID: 14674 RVA: 0x00081474 File Offset: 0x0007F674
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06003953 RID: 14675 RVA: 0x00081498 File Offset: 0x0007F698
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06003954 RID: 14676 RVA: 0x000814BA File Offset: 0x0007F6BA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000814C4 File Offset: 0x0007F6C4
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06003956 RID: 14678 RVA: 0x000814E8 File Offset: 0x0007F6E8
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001563 RID: 5475
		private Result m_ResultCode;

		// Token: 0x04001564 RID: 5476
		private IntPtr m_ClientData;

		// Token: 0x04001565 RID: 5477
		private IntPtr m_LocalUserId;

		// Token: 0x04001566 RID: 5478
		private IntPtr m_TargetUserId;
	}
}
