using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000913 RID: 2323
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAchievementsUnlockedOptionsInternal : IDisposable
	{
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x060050F2 RID: 20722 RVA: 0x000990A4 File Offset: 0x000972A4
		// (set) Token: 0x060050F3 RID: 20723 RVA: 0x000990C6 File Offset: 0x000972C6
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

		// Token: 0x060050F4 RID: 20724 RVA: 0x000990D5 File Offset: 0x000972D5
		public void Dispose()
		{
		}

		// Token: 0x04001F7D RID: 8061
		private int m_ApiVersion;
	}
}
