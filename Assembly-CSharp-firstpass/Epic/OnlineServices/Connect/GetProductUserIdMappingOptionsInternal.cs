using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008AB RID: 2219
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProductUserIdMappingOptionsInternal : IDisposable
	{
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004E71 RID: 20081 RVA: 0x00096BAC File Offset: 0x00094DAC
		// (set) Token: 0x06004E72 RID: 20082 RVA: 0x00096BCE File Offset: 0x00094DCE
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

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004E73 RID: 20083 RVA: 0x00096BE0 File Offset: 0x00094DE0
		// (set) Token: 0x06004E74 RID: 20084 RVA: 0x00096C02 File Offset: 0x00094E02
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004E75 RID: 20085 RVA: 0x00096C14 File Offset: 0x00094E14
		// (set) Token: 0x06004E76 RID: 20086 RVA: 0x00096C36 File Offset: 0x00094E36
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

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004E77 RID: 20087 RVA: 0x00096C48 File Offset: 0x00094E48
		// (set) Token: 0x06004E78 RID: 20088 RVA: 0x00096C6A File Offset: 0x00094E6A
		public ProductUserId TargetProductUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetProductUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetProductUserId, value);
			}
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x00096C79 File Offset: 0x00094E79
		public void Dispose()
		{
		}

		// Token: 0x04001E7D RID: 7805
		private int m_ApiVersion;

		// Token: 0x04001E7E RID: 7806
		private IntPtr m_LocalUserId;

		// Token: 0x04001E7F RID: 7807
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04001E80 RID: 7808
		private IntPtr m_TargetProductUserId;
	}
}
