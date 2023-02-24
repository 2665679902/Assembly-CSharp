using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000758 RID: 1880
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyAttributeByKeyOptionsInternal : IDisposable
	{
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060045D3 RID: 17875 RVA: 0x0008DF64 File Offset: 0x0008C164
		// (set) Token: 0x060045D4 RID: 17876 RVA: 0x0008DF86 File Offset: 0x0008C186
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

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060045D5 RID: 17877 RVA: 0x0008DF98 File Offset: 0x0008C198
		// (set) Token: 0x060045D6 RID: 17878 RVA: 0x0008DFBA File Offset: 0x0008C1BA
		public string AttrKey
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AttrKey, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AttrKey, value);
			}
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x0008DFC9 File Offset: 0x0008C1C9
		public void Dispose()
		{
		}

		// Token: 0x04001AFA RID: 6906
		private int m_ApiVersion;

		// Token: 0x04001AFB RID: 6907
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AttrKey;
	}
}
