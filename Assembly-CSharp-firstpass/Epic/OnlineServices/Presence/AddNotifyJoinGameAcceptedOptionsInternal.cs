using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000665 RID: 1637
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyJoinGameAcceptedOptionsInternal : IDisposable
	{
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06003FAD RID: 16301 RVA: 0x00087AAC File Offset: 0x00085CAC
		// (set) Token: 0x06003FAE RID: 16302 RVA: 0x00087ACE File Offset: 0x00085CCE
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

		// Token: 0x06003FAF RID: 16303 RVA: 0x00087ADD File Offset: 0x00085CDD
		public void Dispose()
		{
		}

		// Token: 0x0400184F RID: 6223
		private int m_ApiVersion;
	}
}
