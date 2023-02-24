using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005C8 RID: 1480
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifySessionInviteReceivedOptionsInternal : IDisposable
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06003C26 RID: 15398 RVA: 0x00084204 File Offset: 0x00082404
		// (set) Token: 0x06003C27 RID: 15399 RVA: 0x00084226 File Offset: 0x00082426
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

		// Token: 0x06003C28 RID: 15400 RVA: 0x00084235 File Offset: 0x00082435
		public void Dispose()
		{
		}

		// Token: 0x040016F0 RID: 5872
		private int m_ApiVersion;
	}
}
