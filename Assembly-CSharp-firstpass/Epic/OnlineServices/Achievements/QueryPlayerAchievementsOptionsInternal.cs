using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200094B RID: 2379
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPlayerAchievementsOptionsInternal : IDisposable
	{
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06005274 RID: 21108 RVA: 0x0009A660 File Offset: 0x00098860
		// (set) Token: 0x06005275 RID: 21109 RVA: 0x0009A682 File Offset: 0x00098882
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

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06005276 RID: 21110 RVA: 0x0009A694 File Offset: 0x00098894
		// (set) Token: 0x06005277 RID: 21111 RVA: 0x0009A6B6 File Offset: 0x000988B6
		public ProductUserId UserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x0009A6C5 File Offset: 0x000988C5
		public void Dispose()
		{
		}

		// Token: 0x04002017 RID: 8215
		private int m_ApiVersion;

		// Token: 0x04002018 RID: 8216
		private IntPtr m_UserId;
	}
}
