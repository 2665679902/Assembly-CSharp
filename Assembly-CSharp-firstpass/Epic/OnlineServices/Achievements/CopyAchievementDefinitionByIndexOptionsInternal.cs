using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000919 RID: 2329
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x000991B8 File Offset: 0x000973B8
		// (set) Token: 0x06005108 RID: 20744 RVA: 0x000991DA File Offset: 0x000973DA
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

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06005109 RID: 20745 RVA: 0x000991EC File Offset: 0x000973EC
		// (set) Token: 0x0600510A RID: 20746 RVA: 0x0009920E File Offset: 0x0009740E
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

		// Token: 0x0600510B RID: 20747 RVA: 0x0009921D File Offset: 0x0009741D
		public void Dispose()
		{
		}

		// Token: 0x04001F83 RID: 8067
		private int m_ApiVersion;

		// Token: 0x04001F84 RID: 8068
		private uint m_AchievementIndex;
	}
}
