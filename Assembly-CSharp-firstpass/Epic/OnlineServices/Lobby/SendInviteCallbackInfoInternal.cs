using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C7 RID: 1991
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600484A RID: 18506 RVA: 0x0009045C File Offset: 0x0008E65C
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600484B RID: 18507 RVA: 0x00090480 File Offset: 0x0008E680
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x0600484C RID: 18508 RVA: 0x000904A2 File Offset: 0x0008E6A2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x0600484D RID: 18509 RVA: 0x000904AC File Offset: 0x0008E6AC
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x04001BDC RID: 7132
		private Result m_ResultCode;

		// Token: 0x04001BDD RID: 7133
		private IntPtr m_ClientData;

		// Token: 0x04001BDE RID: 7134
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
