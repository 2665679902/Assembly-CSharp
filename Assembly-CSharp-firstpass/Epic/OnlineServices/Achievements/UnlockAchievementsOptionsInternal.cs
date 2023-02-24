using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200094F RID: 2383
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlockAchievementsOptionsInternal : IDisposable
	{
		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x0600528C RID: 21132 RVA: 0x0009A7BC File Offset: 0x000989BC
		// (set) Token: 0x0600528D RID: 21133 RVA: 0x0009A7DE File Offset: 0x000989DE
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

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x0009A7F0 File Offset: 0x000989F0
		// (set) Token: 0x0600528F RID: 21135 RVA: 0x0009A812 File Offset: 0x00098A12
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

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06005290 RID: 21136 RVA: 0x0009A824 File Offset: 0x00098A24
		// (set) Token: 0x06005291 RID: 21137 RVA: 0x0009A84C File Offset: 0x00098A4C
		public string[] AchievementIds
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_AchievementIds, out @default, this.m_AchievementsCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AchievementIds, value, out this.m_AchievementsCount);
			}
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0009A861 File Offset: 0x00098A61
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AchievementIds);
		}

		// Token: 0x04002020 RID: 8224
		private int m_ApiVersion;

		// Token: 0x04002021 RID: 8225
		private IntPtr m_UserId;

		// Token: 0x04002022 RID: 8226
		private IntPtr m_AchievementIds;

		// Token: 0x04002023 RID: 8227
		private uint m_AchievementsCount;
	}
}
