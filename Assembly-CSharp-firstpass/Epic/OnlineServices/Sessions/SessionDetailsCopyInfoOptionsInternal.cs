using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000620 RID: 1568
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsCopyInfoOptionsInternal : IDisposable
	{
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x000858FC File Offset: 0x00083AFC
		// (set) Token: 0x06003DEC RID: 15852 RVA: 0x0008591E File Offset: 0x00083B1E
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

		// Token: 0x06003DED RID: 15853 RVA: 0x0008592D File Offset: 0x00083B2D
		public void Dispose()
		{
		}

		// Token: 0x04001789 RID: 6025
		private int m_ApiVersion;
	}
}
