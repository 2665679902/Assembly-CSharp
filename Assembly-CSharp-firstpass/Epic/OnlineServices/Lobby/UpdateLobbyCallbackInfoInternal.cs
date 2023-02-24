using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007CB RID: 1995
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateLobbyCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x00090618 File Offset: 0x0008E818
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06004867 RID: 18535 RVA: 0x0009063C File Offset: 0x0008E83C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06004868 RID: 18536 RVA: 0x0009065E File Offset: 0x0008E85E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06004869 RID: 18537 RVA: 0x00090668 File Offset: 0x0008E868
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x04001BE9 RID: 7145
		private Result m_ResultCode;

		// Token: 0x04001BEA RID: 7146
		private IntPtr m_ClientData;

		// Token: 0x04001BEB RID: 7147
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
