using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200090A RID: 2314
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PinGrantInfoInternal : IDisposable
	{
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x0600507B RID: 20603 RVA: 0x0009859C File Offset: 0x0009679C
		// (set) Token: 0x0600507C RID: 20604 RVA: 0x000985BE File Offset: 0x000967BE
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

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600507D RID: 20605 RVA: 0x000985D0 File Offset: 0x000967D0
		// (set) Token: 0x0600507E RID: 20606 RVA: 0x000985F2 File Offset: 0x000967F2
		public string UserCode
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_UserCode, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_UserCode, value);
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600507F RID: 20607 RVA: 0x00098604 File Offset: 0x00096804
		// (set) Token: 0x06005080 RID: 20608 RVA: 0x00098626 File Offset: 0x00096826
		public string VerificationURI
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_VerificationURI, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_VerificationURI, value);
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06005081 RID: 20609 RVA: 0x00098638 File Offset: 0x00096838
		// (set) Token: 0x06005082 RID: 20610 RVA: 0x0009865A File Offset: 0x0009685A
		public int ExpiresIn
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ExpiresIn, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ExpiresIn, value);
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06005083 RID: 20611 RVA: 0x0009866C File Offset: 0x0009686C
		// (set) Token: 0x06005084 RID: 20612 RVA: 0x0009868E File Offset: 0x0009688E
		public string VerificationURIComplete
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_VerificationURIComplete, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_VerificationURIComplete, value);
			}
		}

		// Token: 0x06005085 RID: 20613 RVA: 0x0009869D File Offset: 0x0009689D
		public void Dispose()
		{
		}

		// Token: 0x04001F42 RID: 8002
		private int m_ApiVersion;

		// Token: 0x04001F43 RID: 8003
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_UserCode;

		// Token: 0x04001F44 RID: 8004
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_VerificationURI;

		// Token: 0x04001F45 RID: 8005
		private int m_ExpiresIn;

		// Token: 0x04001F46 RID: 8006
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_VerificationURIComplete;
	}
}
