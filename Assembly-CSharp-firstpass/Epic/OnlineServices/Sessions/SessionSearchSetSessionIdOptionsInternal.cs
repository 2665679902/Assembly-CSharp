using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000652 RID: 1618
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetSessionIdOptionsInternal : IDisposable
	{
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06003F13 RID: 16147 RVA: 0x00086BDC File Offset: 0x00084DDC
		// (set) Token: 0x06003F14 RID: 16148 RVA: 0x00086BFE File Offset: 0x00084DFE
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

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x00086C10 File Offset: 0x00084E10
		// (set) Token: 0x06003F16 RID: 16150 RVA: 0x00086C32 File Offset: 0x00084E32
		public string SessionId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SessionId, value);
			}
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x00086C41 File Offset: 0x00084E41
		public void Dispose()
		{
		}

		// Token: 0x040017EA RID: 6122
		private int m_ApiVersion;

		// Token: 0x040017EB RID: 6123
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionId;
	}
}
