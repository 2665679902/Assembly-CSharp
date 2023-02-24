using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200054B RID: 1355
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByDisplayNameCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x00081048 File Offset: 0x0007F248
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x0008106C File Offset: 0x0007F26C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x0008108E File Offset: 0x0007F28E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x00081098 File Offset: 0x0007F298
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000810BC File Offset: 0x0007F2BC
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06003916 RID: 14614 RVA: 0x000810E0 File Offset: 0x0007F2E0
		public string DisplayName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DisplayName, out @default);
				return @default;
			}
		}

		// Token: 0x04001542 RID: 5442
		private Result m_ResultCode;

		// Token: 0x04001543 RID: 5443
		private IntPtr m_ClientData;

		// Token: 0x04001544 RID: 5444
		private IntPtr m_LocalUserId;

		// Token: 0x04001545 RID: 5445
		private IntPtr m_TargetUserId;

		// Token: 0x04001546 RID: 5446
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DisplayName;
	}
}
