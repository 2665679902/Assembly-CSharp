using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000925 RID: 2341
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUnlockedAchievementByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x0600514B RID: 20811 RVA: 0x000995AC File Offset: 0x000977AC
		// (set) Token: 0x0600514C RID: 20812 RVA: 0x000995CE File Offset: 0x000977CE
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

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x0600514D RID: 20813 RVA: 0x000995E0 File Offset: 0x000977E0
		// (set) Token: 0x0600514E RID: 20814 RVA: 0x00099602 File Offset: 0x00097802
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

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x0600514F RID: 20815 RVA: 0x00099614 File Offset: 0x00097814
		// (set) Token: 0x06005150 RID: 20816 RVA: 0x00099636 File Offset: 0x00097836
		public uint AchievementIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_AchievementIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_AchievementIndex, value);
			}
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x00099645 File Offset: 0x00097845
		public void Dispose()
		{
		}

		// Token: 0x04001F9C RID: 8092
		private int m_ApiVersion;

		// Token: 0x04001F9D RID: 8093
		private IntPtr m_UserId;

		// Token: 0x04001F9E RID: 8094
		private uint m_AchievementIndex;
	}
}
