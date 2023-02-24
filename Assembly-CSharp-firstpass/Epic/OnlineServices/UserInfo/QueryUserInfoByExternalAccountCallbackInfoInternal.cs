using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200054F RID: 1359
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByExternalAccountCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06003931 RID: 14641 RVA: 0x0008123C File Offset: 0x0007F43C
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06003932 RID: 14642 RVA: 0x00081260 File Offset: 0x0007F460
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06003933 RID: 14643 RVA: 0x00081282 File Offset: 0x0007F482
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06003934 RID: 14644 RVA: 0x0008128C File Offset: 0x0007F48C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x000812B0 File Offset: 0x0007F4B0
		public string ExternalAccountId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ExternalAccountId, out @default);
				return @default;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06003936 RID: 14646 RVA: 0x000812D4 File Offset: 0x0007F4D4
		public ExternalAccountType AccountType
		{
			get
			{
				ExternalAccountType @default = Helper.GetDefault<ExternalAccountType>();
				Helper.TryMarshalGet<ExternalAccountType>(this.m_AccountType, out @default);
				return @default;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06003937 RID: 14647 RVA: 0x000812F8 File Offset: 0x0007F4F8
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001552 RID: 5458
		private Result m_ResultCode;

		// Token: 0x04001553 RID: 5459
		private IntPtr m_ClientData;

		// Token: 0x04001554 RID: 5460
		private IntPtr m_LocalUserId;

		// Token: 0x04001555 RID: 5461
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ExternalAccountId;

		// Token: 0x04001556 RID: 5462
		private ExternalAccountType m_AccountType;

		// Token: 0x04001557 RID: 5463
		private IntPtr m_TargetUserId;
	}
}
