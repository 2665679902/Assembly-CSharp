using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000890 RID: 2192
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserExternalAccountByAccountTypeOptionsInternal : IDisposable
	{
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004DDA RID: 19930 RVA: 0x000962A0 File Offset: 0x000944A0
		// (set) Token: 0x06004DDB RID: 19931 RVA: 0x000962C2 File Offset: 0x000944C2
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

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004DDC RID: 19932 RVA: 0x000962D4 File Offset: 0x000944D4
		// (set) Token: 0x06004DDD RID: 19933 RVA: 0x000962F6 File Offset: 0x000944F6
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

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06004DDE RID: 19934 RVA: 0x00096308 File Offset: 0x00094508
		// (set) Token: 0x06004DDF RID: 19935 RVA: 0x0009632A File Offset: 0x0009452A
		public ExternalAccountType AccountIdType
		{
			get
			{
				ExternalAccountType @default = Helper.GetDefault<ExternalAccountType>();
				Helper.TryMarshalGet<ExternalAccountType>(this.m_AccountIdType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ExternalAccountType>(ref this.m_AccountIdType, value);
			}
		}

		// Token: 0x06004DE0 RID: 19936 RVA: 0x00096339 File Offset: 0x00094539
		public void Dispose()
		{
		}

		// Token: 0x04001E33 RID: 7731
		private int m_ApiVersion;

		// Token: 0x04001E34 RID: 7732
		private IntPtr m_TargetUserId;

		// Token: 0x04001E35 RID: 7733
		private ExternalAccountType m_AccountIdType;
	}
}
