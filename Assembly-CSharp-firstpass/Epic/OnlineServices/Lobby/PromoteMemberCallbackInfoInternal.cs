using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007BB RID: 1979
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PromoteMemberCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06004802 RID: 18434 RVA: 0x0008FFF4 File Offset: 0x0008E1F4
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06004803 RID: 18435 RVA: 0x00090018 File Offset: 0x0008E218
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06004804 RID: 18436 RVA: 0x0009003A File Offset: 0x0008E23A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06004805 RID: 18437 RVA: 0x00090044 File Offset: 0x0008E244
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x04001BBB RID: 7099
		private Result m_ResultCode;

		// Token: 0x04001BBC RID: 7100
		private IntPtr m_ClientData;

		// Token: 0x04001BBD RID: 7101
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
