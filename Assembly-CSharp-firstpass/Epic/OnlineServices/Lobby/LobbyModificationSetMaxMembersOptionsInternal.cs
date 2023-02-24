using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200077F RID: 1919
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetMaxMembersOptionsInternal : IDisposable
	{
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x0008F73C File Offset: 0x0008D93C
		// (set) Token: 0x060046F6 RID: 18166 RVA: 0x0008F75E File Offset: 0x0008D95E
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

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060046F7 RID: 18167 RVA: 0x0008F770 File Offset: 0x0008D970
		// (set) Token: 0x060046F8 RID: 18168 RVA: 0x0008F792 File Offset: 0x0008D992
		public uint MaxMembers
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MaxMembers, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MaxMembers, value);
			}
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x0008F7A1 File Offset: 0x0008D9A1
		public void Dispose()
		{
		}

		// Token: 0x04001B8D RID: 7053
		private int m_ApiVersion;

		// Token: 0x04001B8E RID: 7054
		private uint m_MaxMembers;
	}
}
