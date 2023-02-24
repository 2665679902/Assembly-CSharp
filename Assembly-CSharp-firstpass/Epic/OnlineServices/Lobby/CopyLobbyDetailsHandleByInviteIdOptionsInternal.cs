using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000732 RID: 1842
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLobbyDetailsHandleByInviteIdOptionsInternal : IDisposable
	{
		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x0008CF40 File Offset: 0x0008B140
		// (set) Token: 0x060044E1 RID: 17633 RVA: 0x0008CF62 File Offset: 0x0008B162
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

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x0008CF74 File Offset: 0x0008B174
		// (set) Token: 0x060044E3 RID: 17635 RVA: 0x0008CF96 File Offset: 0x0008B196
		public string InviteId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_InviteId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_InviteId, value);
			}
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x0008CFA5 File Offset: 0x0008B1A5
		public void Dispose()
		{
		}

		// Token: 0x04001A99 RID: 6809
		private int m_ApiVersion;

		// Token: 0x04001A9A RID: 6810
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_InviteId;
	}
}
