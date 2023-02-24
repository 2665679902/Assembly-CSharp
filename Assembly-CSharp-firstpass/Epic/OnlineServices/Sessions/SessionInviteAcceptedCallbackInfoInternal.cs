using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200062C RID: 1580
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionInviteAcceptedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x00085DCC File Offset: 0x00083FCC
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x00085DEE File Offset: 0x00083FEE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06003E40 RID: 15936 RVA: 0x00085DF8 File Offset: 0x00083FF8
		public string SessionId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionId, out @default);
				return @default;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06003E41 RID: 15937 RVA: 0x00085E1C File Offset: 0x0008401C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x00085E40 File Offset: 0x00084040
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06003E43 RID: 15939 RVA: 0x00085E64 File Offset: 0x00084064
		public string InviteId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_InviteId, out @default);
				return @default;
			}
		}

		// Token: 0x040017AA RID: 6058
		private IntPtr m_ClientData;

		// Token: 0x040017AB RID: 6059
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionId;

		// Token: 0x040017AC RID: 6060
		private IntPtr m_LocalUserId;

		// Token: 0x040017AD RID: 6061
		private IntPtr m_TargetUserId;

		// Token: 0x040017AE RID: 6062
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_InviteId;
	}
}
