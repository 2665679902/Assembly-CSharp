using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200081B RID: 2075
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFriendsOptionsInternal : IDisposable
	{
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x0009225C File Offset: 0x0009045C
		// (set) Token: 0x06004A38 RID: 19000 RVA: 0x0009227E File Offset: 0x0009047E
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

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06004A39 RID: 19001 RVA: 0x00092290 File Offset: 0x00090490
		// (set) Token: 0x06004A3A RID: 19002 RVA: 0x000922B2 File Offset: 0x000904B2
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

		// Token: 0x06004A3B RID: 19003 RVA: 0x000922C1 File Offset: 0x000904C1
		public void Dispose()
		{
		}

		// Token: 0x04001C9C RID: 7324
		private int m_ApiVersion;

		// Token: 0x04001C9D RID: 7325
		private IntPtr m_LocalUserId;
	}
}
