using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020006D5 RID: 1749
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ClientCredentialsInternal : IDisposable
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06004270 RID: 17008 RVA: 0x0008A484 File Offset: 0x00088684
		// (set) Token: 0x06004271 RID: 17009 RVA: 0x0008A4A6 File Offset: 0x000886A6
		public string ClientId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ClientId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ClientId, value);
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06004272 RID: 17010 RVA: 0x0008A4B8 File Offset: 0x000886B8
		// (set) Token: 0x06004273 RID: 17011 RVA: 0x0008A4DA File Offset: 0x000886DA
		public string ClientSecret
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ClientSecret, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ClientSecret, value);
			}
		}

		// Token: 0x06004274 RID: 17012 RVA: 0x0008A4E9 File Offset: 0x000886E9
		public void Dispose()
		{
		}

		// Token: 0x04001973 RID: 6515
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ClientId;

		// Token: 0x04001974 RID: 6516
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ClientSecret;
	}
}
