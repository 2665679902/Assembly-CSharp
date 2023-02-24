using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000714 RID: 1812
	[StructLayout(LayoutKind.Explicit, Pack = 4)]
	internal struct EndPlayerSessionOptionsAccountIdInternal : IDisposable
	{
		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06004466 RID: 17510 RVA: 0x0008C69C File Offset: 0x0008A89C
		// (set) Token: 0x06004467 RID: 17511 RVA: 0x0008C6BE File Offset: 0x0008A8BE
		public MetricsAccountIdType AccountIdType
		{
			get
			{
				MetricsAccountIdType @default = Helper.GetDefault<MetricsAccountIdType>();
				Helper.TryMarshalGet<MetricsAccountIdType>(this.m_AccountIdType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<MetricsAccountIdType>(ref this.m_AccountIdType, value);
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x0008C6D0 File Offset: 0x0008A8D0
		// (set) Token: 0x06004469 RID: 17513 RVA: 0x0008C6F9 File Offset: 0x0008A8F9
		public EpicAccountId Epic
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId, MetricsAccountIdType>(this.m_Epic, out @default, this.m_AccountIdType, MetricsAccountIdType.Epic);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<EpicAccountId, MetricsAccountIdType>(ref this.m_Epic, value, ref this.m_AccountIdType, MetricsAccountIdType.Epic, this);
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x0008C71C File Offset: 0x0008A91C
		// (set) Token: 0x0600446B RID: 17515 RVA: 0x0008C745 File Offset: 0x0008A945
		public string External
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<MetricsAccountIdType>(this.m_External, out @default, this.m_AccountIdType, MetricsAccountIdType.External);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<MetricsAccountIdType>(ref this.m_External, value, ref this.m_AccountIdType, MetricsAccountIdType.External, this);
			}
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x0008C766 File Offset: 0x0008A966
		public void Dispose()
		{
			Helper.TryMarshalDispose<MetricsAccountIdType>(ref this.m_External, this.m_AccountIdType, MetricsAccountIdType.External);
		}

		// Token: 0x04001A48 RID: 6728
		[FieldOffset(0)]
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x04001A49 RID: 6729
		[FieldOffset(4)]
		private IntPtr m_Epic;

		// Token: 0x04001A4A RID: 6730
		[FieldOffset(4)]
		private IntPtr m_External;
	}
}
