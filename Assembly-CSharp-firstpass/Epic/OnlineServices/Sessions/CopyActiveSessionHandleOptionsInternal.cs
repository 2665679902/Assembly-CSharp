using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005CE RID: 1486
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyActiveSessionHandleOptionsInternal : IDisposable
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x00084604 File Offset: 0x00082804
		// (set) Token: 0x06003C55 RID: 15445 RVA: 0x00084626 File Offset: 0x00082826
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

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06003C56 RID: 15446 RVA: 0x00084638 File Offset: 0x00082838
		// (set) Token: 0x06003C57 RID: 15447 RVA: 0x0008465A File Offset: 0x0008285A
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

		// Token: 0x06003C58 RID: 15448 RVA: 0x00084669 File Offset: 0x00082869
		public void Dispose()
		{
		}

		// Token: 0x04001701 RID: 5889
		private int m_ApiVersion;

		// Token: 0x04001702 RID: 5890
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;
	}
}
