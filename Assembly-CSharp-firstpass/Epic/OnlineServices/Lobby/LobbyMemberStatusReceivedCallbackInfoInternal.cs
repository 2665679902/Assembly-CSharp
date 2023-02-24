using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000772 RID: 1906
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyMemberStatusReceivedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060046A6 RID: 18086 RVA: 0x0008F1C0 File Offset: 0x0008D3C0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060046A7 RID: 18087 RVA: 0x0008F1E2 File Offset: 0x0008D3E2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060046A8 RID: 18088 RVA: 0x0008F1EC File Offset: 0x0008D3EC
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060046A9 RID: 18089 RVA: 0x0008F210 File Offset: 0x0008D410
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060046AA RID: 18090 RVA: 0x0008F234 File Offset: 0x0008D434
		public LobbyMemberStatus CurrentStatus
		{
			get
			{
				LobbyMemberStatus @default = Helper.GetDefault<LobbyMemberStatus>();
				Helper.TryMarshalGet<LobbyMemberStatus>(this.m_CurrentStatus, out @default);
				return @default;
			}
		}

		// Token: 0x04001B72 RID: 7026
		private IntPtr m_ClientData;

		// Token: 0x04001B73 RID: 7027
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;

		// Token: 0x04001B74 RID: 7028
		private IntPtr m_TargetUserId;

		// Token: 0x04001B75 RID: 7029
		private LobbyMemberStatus m_CurrentStatus;
	}
}
