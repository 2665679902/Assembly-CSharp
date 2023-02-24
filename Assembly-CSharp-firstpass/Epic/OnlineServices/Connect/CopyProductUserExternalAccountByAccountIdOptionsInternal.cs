using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200088E RID: 2190
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserExternalAccountByAccountIdOptionsInternal : IDisposable
	{
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x000961D8 File Offset: 0x000943D8
		// (set) Token: 0x06004DCE RID: 19918 RVA: 0x000961FA File Offset: 0x000943FA
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

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004DCF RID: 19919 RVA: 0x0009620C File Offset: 0x0009440C
		// (set) Token: 0x06004DD0 RID: 19920 RVA: 0x0009622E File Offset: 0x0009442E
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06004DD1 RID: 19921 RVA: 0x00096240 File Offset: 0x00094440
		// (set) Token: 0x06004DD2 RID: 19922 RVA: 0x00096262 File Offset: 0x00094462
		public string AccountId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AccountId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AccountId, value);
			}
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x00096271 File Offset: 0x00094471
		public void Dispose()
		{
		}

		// Token: 0x04001E2E RID: 7726
		private int m_ApiVersion;

		// Token: 0x04001E2F RID: 7727
		private IntPtr m_TargetUserId;

		// Token: 0x04001E30 RID: 7728
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AccountId;
	}
}
