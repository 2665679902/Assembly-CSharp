using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000915 RID: 2325
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAchievementsUnlockedV2OptionsInternal : IDisposable
	{
		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x060050F7 RID: 20727 RVA: 0x000990E4 File Offset: 0x000972E4
		// (set) Token: 0x060050F8 RID: 20728 RVA: 0x00099106 File Offset: 0x00097306
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

		// Token: 0x060050F9 RID: 20729 RVA: 0x00099115 File Offset: 0x00097315
		public void Dispose()
		{
		}

		// Token: 0x04001F7E RID: 8062
		private int m_ApiVersion;
	}
}
