using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000807 RID: 2055
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendAtIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060049D2 RID: 18898 RVA: 0x00091E90 File Offset: 0x00090090
		// (set) Token: 0x060049D3 RID: 18899 RVA: 0x00091EB2 File Offset: 0x000900B2
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

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x00091EC4 File Offset: 0x000900C4
		// (set) Token: 0x060049D5 RID: 18901 RVA: 0x00091EE6 File Offset: 0x000900E6
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

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x00091EF8 File Offset: 0x000900F8
		// (set) Token: 0x060049D7 RID: 18903 RVA: 0x00091F1A File Offset: 0x0009011A
		public int Index
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_Index, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_Index, value);
			}
		}

		// Token: 0x060049D8 RID: 18904 RVA: 0x00091F29 File Offset: 0x00090129
		public void Dispose()
		{
		}

		// Token: 0x04001C80 RID: 7296
		private int m_ApiVersion;

		// Token: 0x04001C81 RID: 7297
		private IntPtr m_LocalUserId;

		// Token: 0x04001C82 RID: 7298
		private int m_Index;
	}
}
