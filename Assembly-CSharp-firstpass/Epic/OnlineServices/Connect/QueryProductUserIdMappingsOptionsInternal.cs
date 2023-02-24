using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020008D3 RID: 2259
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryProductUserIdMappingsOptionsInternal : IDisposable
	{
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x0009735C File Offset: 0x0009555C
		// (set) Token: 0x06004F43 RID: 20291 RVA: 0x0009737E File Offset: 0x0009557E
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

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x00097390 File Offset: 0x00095590
		// (set) Token: 0x06004F45 RID: 20293 RVA: 0x000973B2 File Offset: 0x000955B2
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

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x000973C4 File Offset: 0x000955C4
		// (set) Token: 0x06004F47 RID: 20295 RVA: 0x000973E6 File Offset: 0x000955E6
		public ExternalAccountType AccountIdType_DEPRECATED
		{
			get
			{
				ExternalAccountType @default = Helper.GetDefault<ExternalAccountType>();
				Helper.TryMarshalGet<ExternalAccountType>(this.m_AccountIdType_DEPRECATED, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ExternalAccountType>(ref this.m_AccountIdType_DEPRECATED, value);
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x000973F8 File Offset: 0x000955F8
		// (set) Token: 0x06004F49 RID: 20297 RVA: 0x00097420 File Offset: 0x00095620
		public ProductUserId[] ProductUserIds
		{
			get
			{
				ProductUserId[] @default = Helper.GetDefault<ProductUserId[]>();
				Helper.TryMarshalGet<ProductUserId>(this.m_ProductUserIds, out @default, this.m_ProductUserIdCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_ProductUserIds, value, out this.m_ProductUserIdCount);
			}
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x00097435 File Offset: 0x00095635
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ProductUserIds);
		}

		// Token: 0x04001EB8 RID: 7864
		private int m_ApiVersion;

		// Token: 0x04001EB9 RID: 7865
		private IntPtr m_LocalUserId;

		// Token: 0x04001EBA RID: 7866
		private ExternalAccountType m_AccountIdType_DEPRECATED;

		// Token: 0x04001EBB RID: 7867
		private IntPtr m_ProductUserIds;

		// Token: 0x04001EBC RID: 7868
		private uint m_ProductUserIdCount;
	}
}
