using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200091B RID: 2331
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionV2ByAchievementIdOptionsInternal : IDisposable
	{
		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06005110 RID: 20752 RVA: 0x0009923C File Offset: 0x0009743C
		// (set) Token: 0x06005111 RID: 20753 RVA: 0x0009925E File Offset: 0x0009745E
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

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06005112 RID: 20754 RVA: 0x00099270 File Offset: 0x00097470
		// (set) Token: 0x06005113 RID: 20755 RVA: 0x00099292 File Offset: 0x00097492
		public string AchievementId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AchievementId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AchievementId, value);
			}
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x000992A1 File Offset: 0x000974A1
		public void Dispose()
		{
		}

		// Token: 0x04001F86 RID: 8070
		private int m_ApiVersion;

		// Token: 0x04001F87 RID: 8071
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AchievementId;
	}
}
