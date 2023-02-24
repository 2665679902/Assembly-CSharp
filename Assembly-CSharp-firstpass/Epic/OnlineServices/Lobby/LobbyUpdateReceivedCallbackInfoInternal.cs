using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000799 RID: 1945
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyUpdateReceivedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x0008FF68 File Offset: 0x0008E168
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06004779 RID: 18297 RVA: 0x0008FF8A File Offset: 0x0008E18A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x0008FF94 File Offset: 0x0008E194
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x04001BB6 RID: 7094
		private IntPtr m_ClientData;

		// Token: 0x04001BB7 RID: 7095
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
