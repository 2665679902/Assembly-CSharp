using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000659 RID: 1625
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StartSessionOptionsInternal : IDisposable
	{
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06003F71 RID: 16241 RVA: 0x000876F0 File Offset: 0x000858F0
		// (set) Token: 0x06003F72 RID: 16242 RVA: 0x00087712 File Offset: 0x00085912
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

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x00087724 File Offset: 0x00085924
		// (set) Token: 0x06003F74 RID: 16244 RVA: 0x00087746 File Offset: 0x00085946
		public string SessionName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SessionName, value);
			}
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x00087755 File Offset: 0x00085955
		public void Dispose()
		{
		}

		// Token: 0x04001835 RID: 6197
		private int m_ApiVersion;

		// Token: 0x04001836 RID: 6198
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;
	}
}
