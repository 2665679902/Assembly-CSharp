using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200092B RID: 2347
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAchievementDefinitionCountOptionsInternal : IDisposable
	{
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x060051B2 RID: 20914 RVA: 0x00099CAC File Offset: 0x00097EAC
		// (set) Token: 0x060051B3 RID: 20915 RVA: 0x00099CCE File Offset: 0x00097ECE
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

		// Token: 0x060051B4 RID: 20916 RVA: 0x00099CDD File Offset: 0x00097EDD
		public void Dispose()
		{
		}

		// Token: 0x04001FCD RID: 8141
		private int m_ApiVersion;
	}
}
