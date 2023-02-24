using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200076F RID: 1903
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyInviteReceivedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06004698 RID: 18072 RVA: 0x0008F0DC File Offset: 0x0008D2DC
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x0008F0FE File Offset: 0x0008D2FE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600469A RID: 18074 RVA: 0x0008F108 File Offset: 0x0008D308
		public string InviteId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_InviteId, out @default);
				return @default;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x0008F12C File Offset: 0x0008D32C
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x0600469C RID: 18076 RVA: 0x0008F150 File Offset: 0x0008D350
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001B63 RID: 7011
		private IntPtr m_ClientData;

		// Token: 0x04001B64 RID: 7012
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_InviteId;

		// Token: 0x04001B65 RID: 7013
		private IntPtr m_LocalUserId;

		// Token: 0x04001B66 RID: 7014
		private IntPtr m_TargetUserId;
	}
}
