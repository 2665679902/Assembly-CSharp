using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020006D9 RID: 1753
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OptionsInternal : IDisposable
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060042AE RID: 17070 RVA: 0x0008A7D4 File Offset: 0x000889D4
		// (set) Token: 0x060042AF RID: 17071 RVA: 0x0008A7F6 File Offset: 0x000889F6
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

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060042B0 RID: 17072 RVA: 0x0008A808 File Offset: 0x00088A08
		// (set) Token: 0x060042B1 RID: 17073 RVA: 0x0008A82A File Offset: 0x00088A2A
		public IntPtr Reserved
		{
			get
			{
				IntPtr @default = Helper.GetDefault<IntPtr>();
				Helper.TryMarshalGet(this.m_Reserved, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<IntPtr>(ref this.m_Reserved, value);
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060042B2 RID: 17074 RVA: 0x0008A83C File Offset: 0x00088A3C
		// (set) Token: 0x060042B3 RID: 17075 RVA: 0x0008A85E File Offset: 0x00088A5E
		public string ProductId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_ProductId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_ProductId, value);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x0008A870 File Offset: 0x00088A70
		// (set) Token: 0x060042B5 RID: 17077 RVA: 0x0008A892 File Offset: 0x00088A92
		public string SandboxId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SandboxId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SandboxId, value);
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x0008A8A4 File Offset: 0x00088AA4
		// (set) Token: 0x060042B7 RID: 17079 RVA: 0x0008A8C6 File Offset: 0x00088AC6
		public ClientCredentialsInternal ClientCredentials
		{
			get
			{
				ClientCredentialsInternal @default = Helper.GetDefault<ClientCredentialsInternal>();
				Helper.TryMarshalGet<ClientCredentialsInternal>(this.m_ClientCredentials, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ClientCredentialsInternal>(ref this.m_ClientCredentials, value);
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060042B8 RID: 17080 RVA: 0x0008A8D8 File Offset: 0x00088AD8
		// (set) Token: 0x060042B9 RID: 17081 RVA: 0x0008A8FA File Offset: 0x00088AFA
		public bool IsServer
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_IsServer, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_IsServer, value);
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060042BA RID: 17082 RVA: 0x0008A90C File Offset: 0x00088B0C
		// (set) Token: 0x060042BB RID: 17083 RVA: 0x0008A92E File Offset: 0x00088B2E
		public string EncryptionKey
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_EncryptionKey, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EncryptionKey, value);
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x0008A940 File Offset: 0x00088B40
		// (set) Token: 0x060042BD RID: 17085 RVA: 0x0008A962 File Offset: 0x00088B62
		public string OverrideCountryCode
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_OverrideCountryCode, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_OverrideCountryCode, value);
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x0008A974 File Offset: 0x00088B74
		// (set) Token: 0x060042BF RID: 17087 RVA: 0x0008A996 File Offset: 0x00088B96
		public string OverrideLocaleCode
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_OverrideLocaleCode, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_OverrideLocaleCode, value);
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060042C0 RID: 17088 RVA: 0x0008A9A8 File Offset: 0x00088BA8
		// (set) Token: 0x060042C1 RID: 17089 RVA: 0x0008A9CA File Offset: 0x00088BCA
		public string DeploymentId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DeploymentId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DeploymentId, value);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060042C2 RID: 17090 RVA: 0x0008A9DC File Offset: 0x00088BDC
		// (set) Token: 0x060042C3 RID: 17091 RVA: 0x0008A9FE File Offset: 0x00088BFE
		public PlatformFlags Flags
		{
			get
			{
				PlatformFlags @default = Helper.GetDefault<PlatformFlags>();
				Helper.TryMarshalGet<PlatformFlags>(this.m_Flags, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<PlatformFlags>(ref this.m_Flags, value);
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060042C4 RID: 17092 RVA: 0x0008AA10 File Offset: 0x00088C10
		// (set) Token: 0x060042C5 RID: 17093 RVA: 0x0008AA32 File Offset: 0x00088C32
		public string CacheDirectory
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_CacheDirectory, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CacheDirectory, value);
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060042C6 RID: 17094 RVA: 0x0008AA44 File Offset: 0x00088C44
		// (set) Token: 0x060042C7 RID: 17095 RVA: 0x0008AA66 File Offset: 0x00088C66
		public uint TickBudgetInMilliseconds
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_TickBudgetInMilliseconds, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_TickBudgetInMilliseconds, value);
			}
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x0008AA75 File Offset: 0x00088C75
		public void Dispose()
		{
			Helper.TryMarshalDispose<ClientCredentialsInternal>(ref this.m_ClientCredentials);
		}

		// Token: 0x0400198F RID: 6543
		private int m_ApiVersion;

		// Token: 0x04001990 RID: 6544
		private IntPtr m_Reserved;

		// Token: 0x04001991 RID: 6545
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_ProductId;

		// Token: 0x04001992 RID: 6546
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SandboxId;

		// Token: 0x04001993 RID: 6547
		private ClientCredentialsInternal m_ClientCredentials;

		// Token: 0x04001994 RID: 6548
		private int m_IsServer;

		// Token: 0x04001995 RID: 6549
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_EncryptionKey;

		// Token: 0x04001996 RID: 6550
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OverrideCountryCode;

		// Token: 0x04001997 RID: 6551
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_OverrideLocaleCode;

		// Token: 0x04001998 RID: 6552
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DeploymentId;

		// Token: 0x04001999 RID: 6553
		private PlatformFlags m_Flags;

		// Token: 0x0400199A RID: 6554
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_CacheDirectory;

		// Token: 0x0400199B RID: 6555
		private uint m_TickBudgetInMilliseconds;
	}
}
