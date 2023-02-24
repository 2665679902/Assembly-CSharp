using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000560 RID: 1376
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetToggleFriendsKeyOptionsInternal : IDisposable
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060039B0 RID: 14768 RVA: 0x00081C04 File Offset: 0x0007FE04
		// (set) Token: 0x060039B1 RID: 14769 RVA: 0x00081C26 File Offset: 0x0007FE26
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

		// Token: 0x060039B2 RID: 14770 RVA: 0x00081C35 File Offset: 0x0007FE35
		public void Dispose()
		{
		}

		// Token: 0x0400158B RID: 5515
		private int m_ApiVersion;
	}
}
