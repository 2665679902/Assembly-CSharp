using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005DC RID: 1500
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroySessionOptionsInternal : IDisposable
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06003CA6 RID: 15526 RVA: 0x00084AF0 File Offset: 0x00082CF0
		// (set) Token: 0x06003CA7 RID: 15527 RVA: 0x00084B12 File Offset: 0x00082D12
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

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06003CA8 RID: 15528 RVA: 0x00084B24 File Offset: 0x00082D24
		// (set) Token: 0x06003CA9 RID: 15529 RVA: 0x00084B46 File Offset: 0x00082D46
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

		// Token: 0x06003CAA RID: 15530 RVA: 0x00084B55 File Offset: 0x00082D55
		public void Dispose()
		{
		}

		// Token: 0x04001721 RID: 5921
		private int m_ApiVersion;

		// Token: 0x04001722 RID: 5922
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;
	}
}
