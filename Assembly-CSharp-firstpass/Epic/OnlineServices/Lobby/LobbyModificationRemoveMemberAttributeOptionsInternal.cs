using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200077D RID: 1917
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationRemoveMemberAttributeOptionsInternal : IDisposable
	{
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060046EC RID: 18156 RVA: 0x0008F6B8 File Offset: 0x0008D8B8
		// (set) Token: 0x060046ED RID: 18157 RVA: 0x0008F6DA File Offset: 0x0008D8DA
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

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060046EE RID: 18158 RVA: 0x0008F6EC File Offset: 0x0008D8EC
		// (set) Token: 0x060046EF RID: 18159 RVA: 0x0008F70E File Offset: 0x0008D90E
		public string Key
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Key, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Key, value);
			}
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x0008F71D File Offset: 0x0008D91D
		public void Dispose()
		{
		}

		// Token: 0x04001B8A RID: 7050
		private int m_ApiVersion;

		// Token: 0x04001B8B RID: 7051
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Key;
	}
}
