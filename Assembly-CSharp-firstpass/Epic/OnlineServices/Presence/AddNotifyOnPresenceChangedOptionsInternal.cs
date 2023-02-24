using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000667 RID: 1639
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyOnPresenceChangedOptionsInternal : IDisposable
	{
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06003FB2 RID: 16306 RVA: 0x00087AEC File Offset: 0x00085CEC
		// (set) Token: 0x06003FB3 RID: 16307 RVA: 0x00087B0E File Offset: 0x00085D0E
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

		// Token: 0x06003FB4 RID: 16308 RVA: 0x00087B1D File Offset: 0x00085D1D
		public void Dispose()
		{
		}

		// Token: 0x04001850 RID: 6224
		private int m_ApiVersion;
	}
}
