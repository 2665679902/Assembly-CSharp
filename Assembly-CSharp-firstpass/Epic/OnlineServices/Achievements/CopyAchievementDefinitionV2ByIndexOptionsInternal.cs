using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200091D RID: 2333
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionV2ByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06005119 RID: 20761 RVA: 0x000992C0 File Offset: 0x000974C0
		// (set) Token: 0x0600511A RID: 20762 RVA: 0x000992E2 File Offset: 0x000974E2
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

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x0600511B RID: 20763 RVA: 0x000992F4 File Offset: 0x000974F4
		// (set) Token: 0x0600511C RID: 20764 RVA: 0x00099316 File Offset: 0x00097516
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

		// Token: 0x0600511D RID: 20765 RVA: 0x00099325 File Offset: 0x00097525
		public void Dispose()
		{
		}

		// Token: 0x04001F89 RID: 8073
		private int m_ApiVersion;

		// Token: 0x04001F8A RID: 8074
		private uint m_AchievementIndex;
	}
}
