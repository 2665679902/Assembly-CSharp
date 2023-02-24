using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000766 RID: 1894
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetMemberByIndexOptionsInternal : IDisposable
	{
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600460E RID: 17934 RVA: 0x0008E2BC File Offset: 0x0008C4BC
		// (set) Token: 0x0600460F RID: 17935 RVA: 0x0008E2DE File Offset: 0x0008C4DE
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

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06004610 RID: 17936 RVA: 0x0008E2F0 File Offset: 0x0008C4F0
		// (set) Token: 0x06004611 RID: 17937 RVA: 0x0008E312 File Offset: 0x0008C512
		public uint MemberIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MemberIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MemberIndex, value);
			}
		}

		// Token: 0x06004612 RID: 17938 RVA: 0x0008E321 File Offset: 0x0008C521
		public void Dispose()
		{
		}

		// Token: 0x04001B0D RID: 6925
		private int m_ApiVersion;

		// Token: 0x04001B0E RID: 6926
		private uint m_MemberIndex;
	}
}
