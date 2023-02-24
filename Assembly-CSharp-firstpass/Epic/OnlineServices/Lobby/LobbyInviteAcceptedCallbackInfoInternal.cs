using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200076D RID: 1901
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyInviteAcceptedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x0008EFF8 File Offset: 0x0008D1F8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600468B RID: 18059 RVA: 0x0008F01A File Offset: 0x0008D21A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x0600468C RID: 18060 RVA: 0x0008F024 File Offset: 0x0008D224
		public string InviteId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_InviteId, out @default);
				return @default;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x0008F048 File Offset: 0x0008D248
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x0008F06C File Offset: 0x0008D26C
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001B5B RID: 7003
		private IntPtr m_ClientData;

		// Token: 0x04001B5C RID: 7004
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_InviteId;

		// Token: 0x04001B5D RID: 7005
		private IntPtr m_LocalUserId;

		// Token: 0x04001B5E RID: 7006
		private IntPtr m_TargetUserId;
	}
}
