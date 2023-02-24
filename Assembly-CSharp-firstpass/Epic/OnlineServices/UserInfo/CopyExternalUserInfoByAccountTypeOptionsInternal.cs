using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200053B RID: 1339
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyExternalUserInfoByAccountTypeOptionsInternal : IDisposable
	{
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060038A9 RID: 14505 RVA: 0x00080B6C File Offset: 0x0007ED6C
		// (set) Token: 0x060038AA RID: 14506 RVA: 0x00080B8E File Offset: 0x0007ED8E
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

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x00080BA0 File Offset: 0x0007EDA0
		// (set) Token: 0x060038AC RID: 14508 RVA: 0x00080BC2 File Offset: 0x0007EDC2
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

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060038AD RID: 14509 RVA: 0x00080BD4 File Offset: 0x0007EDD4
		// (set) Token: 0x060038AE RID: 14510 RVA: 0x00080BF6 File Offset: 0x0007EDF6
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

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060038AF RID: 14511 RVA: 0x00080C08 File Offset: 0x0007EE08
		// (set) Token: 0x060038B0 RID: 14512 RVA: 0x00080C2A File Offset: 0x0007EE2A
		public ExternalAccountType AccountType
		{
			get
			{
				ExternalAccountType @default = Helper.GetDefault<ExternalAccountType>();
				Helper.TryMarshalGet<ExternalAccountType>(this.m_AccountType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ExternalAccountType>(ref this.m_AccountType, value);
			}
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x00080C39 File Offset: 0x0007EE39
		public void Dispose()
		{
		}

		// Token: 0x04001521 RID: 5409
		private int m_ApiVersion;

		// Token: 0x04001522 RID: 5410
		private IntPtr m_LocalUserId;

		// Token: 0x04001523 RID: 5411
		private IntPtr m_TargetUserId;

		// Token: 0x04001524 RID: 5412
		private ExternalAccountType m_AccountType;
	}
}
