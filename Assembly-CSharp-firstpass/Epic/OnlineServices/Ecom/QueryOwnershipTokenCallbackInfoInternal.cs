using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200087A RID: 2170
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOwnershipTokenCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004D34 RID: 19764 RVA: 0x000953DC File Offset: 0x000935DC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004D35 RID: 19765 RVA: 0x00095400 File Offset: 0x00093600
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06004D36 RID: 19766 RVA: 0x00095422 File Offset: 0x00093622
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06004D37 RID: 19767 RVA: 0x0009542C File Offset: 0x0009362C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06004D38 RID: 19768 RVA: 0x00095450 File Offset: 0x00093650
		public string OwnershipToken
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_OwnershipToken, out @default);
				return @default;
			}
		}

		// Token: 0x04001DEE RID: 7662
		private Result m_ResultCode;

		// Token: 0x04001DEF RID: 7663
		private IntPtr m_ClientData;

		// Token: 0x04001DF0 RID: 7664
		private IntPtr m_LocalUserId;

		// Token: 0x04001DF1 RID: 7665
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OwnershipToken;
	}
}
