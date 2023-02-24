using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000774 RID: 1908
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyMemberUpdateReceivedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060046B2 RID: 18098 RVA: 0x0008F294 File Offset: 0x0008D494
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060046B3 RID: 18099 RVA: 0x0008F2B6 File Offset: 0x0008D4B6
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060046B4 RID: 18100 RVA: 0x0008F2C0 File Offset: 0x0008D4C0
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060046B5 RID: 18101 RVA: 0x0008F2E4 File Offset: 0x0008D4E4
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001B79 RID: 7033
		private IntPtr m_ClientData;

		// Token: 0x04001B7A RID: 7034
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;

		// Token: 0x04001B7B RID: 7035
		private IntPtr m_TargetUserId;
	}
}
