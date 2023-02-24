using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000781 RID: 1921
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetPermissionLevelOptionsInternal : IDisposable
	{
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060046FE RID: 18174 RVA: 0x0008F7C0 File Offset: 0x0008D9C0
		// (set) Token: 0x060046FF RID: 18175 RVA: 0x0008F7E2 File Offset: 0x0008D9E2
		public int ApiVersion
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ApiVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ApiVersion, value);
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06004700 RID: 18176 RVA: 0x0008F7F4 File Offset: 0x0008D9F4
		// (set) Token: 0x06004701 RID: 18177 RVA: 0x0008F816 File Offset: 0x0008DA16
		public LobbyPermissionLevel PermissionLevel
		{
			get
			{
				LobbyPermissionLevel @default = Helper.GetDefault<LobbyPermissionLevel>();
				Helper.TryMarshalGet<LobbyPermissionLevel>(this.m_PermissionLevel, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<LobbyPermissionLevel>(ref this.m_PermissionLevel, value);
			}
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x0008F825 File Offset: 0x0008DA25
		public void Dispose()
		{
		}

		// Token: 0x04001B90 RID: 7056
		private int m_ApiVersion;

		// Token: 0x04001B91 RID: 7057
		private LobbyPermissionLevel m_PermissionLevel;
	}
}
