using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000738 RID: 1848
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateLobbyCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06004502 RID: 17666 RVA: 0x0008D130 File Offset: 0x0008B330
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06004503 RID: 17667 RVA: 0x0008D154 File Offset: 0x0008B354
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06004504 RID: 17668 RVA: 0x0008D176 File Offset: 0x0008B376
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06004505 RID: 17669 RVA: 0x0008D180 File Offset: 0x0008B380
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x04001AA6 RID: 6822
		private Result m_ResultCode;

		// Token: 0x04001AA7 RID: 6823
		private IntPtr m_ClientData;

		// Token: 0x04001AA8 RID: 6824
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
