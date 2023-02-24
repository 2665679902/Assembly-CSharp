using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000539 RID: 1337
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyExternalUserInfoByAccountIdOptionsInternal : IDisposable
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06003898 RID: 14488 RVA: 0x00080A5C File Offset: 0x0007EC5C
		// (set) Token: 0x06003899 RID: 14489 RVA: 0x00080A7E File Offset: 0x0007EC7E
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

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600389A RID: 14490 RVA: 0x00080A90 File Offset: 0x0007EC90
		// (set) Token: 0x0600389B RID: 14491 RVA: 0x00080AB2 File Offset: 0x0007ECB2
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600389C RID: 14492 RVA: 0x00080AC4 File Offset: 0x0007ECC4
		// (set) Token: 0x0600389D RID: 14493 RVA: 0x00080AE6 File Offset: 0x0007ECE6
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600389E RID: 14494 RVA: 0x00080AF8 File Offset: 0x0007ECF8
		// (set) Token: 0x0600389F RID: 14495 RVA: 0x00080B1A File Offset: 0x0007ED1A
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

		// Token: 0x060038A0 RID: 14496 RVA: 0x00080B29 File Offset: 0x0007ED29
		public void Dispose()
		{
		}

		// Token: 0x0400151A RID: 5402
		private int m_ApiVersion;

		// Token: 0x0400151B RID: 5403
		private IntPtr m_LocalUserId;

		// Token: 0x0400151C RID: 5404
		private IntPtr m_TargetUserId;

		// Token: 0x0400151D RID: 5405
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AccountId;
	}
}
