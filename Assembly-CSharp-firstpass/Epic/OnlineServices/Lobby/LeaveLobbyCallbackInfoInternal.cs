using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000750 RID: 1872
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveLobbyCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x0008DAA4 File Offset: 0x0008BCA4
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x0008DAC8 File Offset: 0x0008BCC8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x0008DAEA File Offset: 0x0008BCEA
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x0008DAF4 File Offset: 0x0008BCF4
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x04001AEB RID: 6891
		private Result m_ResultCode;

		// Token: 0x04001AEC RID: 6892
		private IntPtr m_ClientData;

		// Token: 0x04001AED RID: 6893
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
