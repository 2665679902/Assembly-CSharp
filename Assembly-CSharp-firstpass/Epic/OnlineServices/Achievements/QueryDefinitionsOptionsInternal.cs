using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000949 RID: 2377
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryDefinitionsOptionsInternal : IDisposable
	{
		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06005267 RID: 21095 RVA: 0x0009A55C File Offset: 0x0009875C
		// (set) Token: 0x06005268 RID: 21096 RVA: 0x0009A57E File Offset: 0x0009877E
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

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06005269 RID: 21097 RVA: 0x0009A590 File Offset: 0x00098790
		// (set) Token: 0x0600526A RID: 21098 RVA: 0x0009A5B2 File Offset: 0x000987B2
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x0600526B RID: 21099 RVA: 0x0009A5C4 File Offset: 0x000987C4
		// (set) Token: 0x0600526C RID: 21100 RVA: 0x0009A5E6 File Offset: 0x000987E6
		public EpicAccountId EpicUserId_DEPRECATED
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_EpicUserId_DEPRECATED, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_EpicUserId_DEPRECATED, value);
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x0600526D RID: 21101 RVA: 0x0009A5F8 File Offset: 0x000987F8
		// (set) Token: 0x0600526E RID: 21102 RVA: 0x0009A620 File Offset: 0x00098820
		public string[] HiddenAchievementIds_DEPRECATED
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_HiddenAchievementIds_DEPRECATED, out @default, this.m_HiddenAchievementsCount_DEPRECATED);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_HiddenAchievementIds_DEPRECATED, value, out this.m_HiddenAchievementsCount_DEPRECATED);
			}
		}

		// Token: 0x0600526F RID: 21103 RVA: 0x0009A635 File Offset: 0x00098835
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_HiddenAchievementIds_DEPRECATED);
		}

		// Token: 0x04002011 RID: 8209
		private int m_ApiVersion;

		// Token: 0x04002012 RID: 8210
		private IntPtr m_LocalUserId;

		// Token: 0x04002013 RID: 8211
		private IntPtr m_EpicUserId_DEPRECATED;

		// Token: 0x04002014 RID: 8212
		private IntPtr m_HiddenAchievementIds_DEPRECATED;

		// Token: 0x04002015 RID: 8213
		private uint m_HiddenAchievementsCount_DEPRECATED;
	}
}
