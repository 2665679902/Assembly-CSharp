using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200073E RID: 1854
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroyLobbyCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600452B RID: 17707 RVA: 0x0008D3B8 File Offset: 0x0008B5B8
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600452C RID: 17708 RVA: 0x0008D3DC File Offset: 0x0008B5DC
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600452D RID: 17709 RVA: 0x0008D3FE File Offset: 0x0008B5FE
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600452E RID: 17710 RVA: 0x0008D408 File Offset: 0x0008B608
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x04001AB8 RID: 6840
		private Result m_ResultCode;

		// Token: 0x04001AB9 RID: 6841
		private IntPtr m_ClientData;

		// Token: 0x04001ABA RID: 6842
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
