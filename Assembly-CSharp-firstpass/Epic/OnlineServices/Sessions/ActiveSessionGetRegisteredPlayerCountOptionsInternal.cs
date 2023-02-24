using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005C0 RID: 1472
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionGetRegisteredPlayerCountOptionsInternal : IDisposable
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06003C02 RID: 15362 RVA: 0x00083FE4 File Offset: 0x000821E4
		// (set) Token: 0x06003C03 RID: 15363 RVA: 0x00084006 File Offset: 0x00082206
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

		// Token: 0x06003C04 RID: 15364 RVA: 0x00084015 File Offset: 0x00082215
		public void Dispose()
		{
		}

		// Token: 0x040016E4 RID: 5860
		private int m_ApiVersion;
	}
}
