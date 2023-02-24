using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200053F RID: 1343
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserInfoOptionsInternal : IDisposable
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060038C9 RID: 14537 RVA: 0x00080D78 File Offset: 0x0007EF78
		// (set) Token: 0x060038CA RID: 14538 RVA: 0x00080D9A File Offset: 0x0007EF9A
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

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060038CB RID: 14539 RVA: 0x00080DAC File Offset: 0x0007EFAC
		// (set) Token: 0x060038CC RID: 14540 RVA: 0x00080DCE File Offset: 0x0007EFCE
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

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060038CD RID: 14541 RVA: 0x00080DE0 File Offset: 0x0007EFE0
		// (set) Token: 0x060038CE RID: 14542 RVA: 0x00080E02 File Offset: 0x0007F002
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

		// Token: 0x060038CF RID: 14543 RVA: 0x00080E11 File Offset: 0x0007F011
		public void Dispose()
		{
		}

		// Token: 0x0400152E RID: 5422
		private int m_ApiVersion;

		// Token: 0x0400152F RID: 5423
		private IntPtr m_LocalUserId;

		// Token: 0x04001530 RID: 5424
		private IntPtr m_TargetUserId;
	}
}
