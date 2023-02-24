using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005E2 RID: 1506
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionOptionsInternal : IDisposable
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x00084C74 File Offset: 0x00082E74
		// (set) Token: 0x06003CC1 RID: 15553 RVA: 0x00084C96 File Offset: 0x00082E96
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

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x00084CA8 File Offset: 0x00082EA8
		// (set) Token: 0x06003CC3 RID: 15555 RVA: 0x00084CCA File Offset: 0x00082ECA
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

		// Token: 0x06003CC4 RID: 15556 RVA: 0x00084CD9 File Offset: 0x00082ED9
		public void Dispose()
		{
		}

		// Token: 0x0400172B RID: 5931
		private int m_ApiVersion;

		// Token: 0x0400172C RID: 5932
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;
	}
}
