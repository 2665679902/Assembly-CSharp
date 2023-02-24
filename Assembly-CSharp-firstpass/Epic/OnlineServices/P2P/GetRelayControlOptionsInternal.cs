using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006F0 RID: 1776
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetRelayControlOptionsInternal : IDisposable
	{
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x0008B520 File Offset: 0x00089720
		// (set) Token: 0x0600436D RID: 17261 RVA: 0x0008B542 File Offset: 0x00089742
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

		// Token: 0x0600436E RID: 17262 RVA: 0x0008B551 File Offset: 0x00089751
		public void Dispose()
		{
		}

		// Token: 0x040019D7 RID: 6615
		private int m_ApiVersion;
	}
}
