using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000661 RID: 1633
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UpdateSessionModificationOptionsInternal : IDisposable
	{
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06003F9D RID: 16285 RVA: 0x000879B4 File Offset: 0x00085BB4
		// (set) Token: 0x06003F9E RID: 16286 RVA: 0x000879D6 File Offset: 0x00085BD6
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

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06003F9F RID: 16287 RVA: 0x000879E8 File Offset: 0x00085BE8
		// (set) Token: 0x06003FA0 RID: 16288 RVA: 0x00087A0A File Offset: 0x00085C0A
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

		// Token: 0x06003FA1 RID: 16289 RVA: 0x00087A19 File Offset: 0x00085C19
		public void Dispose()
		{
		}

		// Token: 0x0400184A RID: 6218
		private int m_ApiVersion;

		// Token: 0x0400184B RID: 6219
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;
	}
}
