using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200074C RID: 1868
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickMemberCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06004580 RID: 17792 RVA: 0x0008D8E8 File Offset: 0x0008BAE8
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x0008D90C File Offset: 0x0008BB0C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06004582 RID: 17794 RVA: 0x0008D92E File Offset: 0x0008BB2E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06004583 RID: 17795 RVA: 0x0008D938 File Offset: 0x0008BB38
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x04001ADE RID: 6878
		private Result m_ResultCode;

		// Token: 0x04001ADF RID: 6879
		private IntPtr m_ClientData;

		// Token: 0x04001AE0 RID: 6880
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;
	}
}
