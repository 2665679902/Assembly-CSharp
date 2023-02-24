using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005DE RID: 1502
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DumpSessionStateOptionsInternal : IDisposable
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06003CAF RID: 15535 RVA: 0x00084B74 File Offset: 0x00082D74
		// (set) Token: 0x06003CB0 RID: 15536 RVA: 0x00084B96 File Offset: 0x00082D96
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

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06003CB1 RID: 15537 RVA: 0x00084BA8 File Offset: 0x00082DA8
		// (set) Token: 0x06003CB2 RID: 15538 RVA: 0x00084BCA File Offset: 0x00082DCA
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

		// Token: 0x06003CB3 RID: 15539 RVA: 0x00084BD9 File Offset: 0x00082DD9
		public void Dispose()
		{
		}

		// Token: 0x04001724 RID: 5924
		private int m_ApiVersion;

		// Token: 0x04001725 RID: 5925
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;
	}
}
