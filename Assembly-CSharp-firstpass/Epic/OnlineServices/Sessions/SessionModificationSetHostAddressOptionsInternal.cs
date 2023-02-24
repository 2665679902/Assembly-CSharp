using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000637 RID: 1591
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetHostAddressOptionsInternal : IDisposable
	{
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x0008633C File Offset: 0x0008453C
		// (set) Token: 0x06003E89 RID: 16009 RVA: 0x0008635E File Offset: 0x0008455E
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

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x00086370 File Offset: 0x00084570
		// (set) Token: 0x06003E8B RID: 16011 RVA: 0x00086392 File Offset: 0x00084592
		public string HostAddress
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_HostAddress, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_HostAddress, value);
			}
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x000863A1 File Offset: 0x000845A1
		public void Dispose()
		{
		}

		// Token: 0x040017C3 RID: 6083
		private int m_ApiVersion;

		// Token: 0x040017C4 RID: 6084
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_HostAddress;
	}
}
