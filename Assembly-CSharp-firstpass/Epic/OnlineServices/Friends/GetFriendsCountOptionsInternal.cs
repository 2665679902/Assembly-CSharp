using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000809 RID: 2057
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendsCountOptionsInternal : IDisposable
	{
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060049DD RID: 18909 RVA: 0x00091F48 File Offset: 0x00090148
		// (set) Token: 0x060049DE RID: 18910 RVA: 0x00091F6A File Offset: 0x0009016A
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

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060049DF RID: 18911 RVA: 0x00091F7C File Offset: 0x0009017C
		// (set) Token: 0x060049E0 RID: 18912 RVA: 0x00091F9E File Offset: 0x0009019E
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060049E1 RID: 18913 RVA: 0x00091FAD File Offset: 0x000901AD
		public void Dispose()
		{
		}

		// Token: 0x04001C84 RID: 7300
		private int m_ApiVersion;

		// Token: 0x04001C85 RID: 7301
		private IntPtr m_LocalUserId;
	}
}
