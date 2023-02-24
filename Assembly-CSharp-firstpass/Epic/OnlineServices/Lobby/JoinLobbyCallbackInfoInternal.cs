using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000748 RID: 1864
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x0008D72C File Offset: 0x0008B92C
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x0008D750 File Offset: 0x0008B950
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x0008D772 File Offset: 0x0008B972
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x0008D77C File Offset: 0x0008B97C
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x04001AD1 RID: 6865
		private Result m_ResultCode;

		// Token: 0x04001AD2 RID: 6866
		private IntPtr m_ClientData;

		// Token: 0x04001AD3 RID: 6867
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
