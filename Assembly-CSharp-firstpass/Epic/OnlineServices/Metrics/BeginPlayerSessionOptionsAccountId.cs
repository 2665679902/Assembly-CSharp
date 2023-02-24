using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x0200070F RID: 1807
	public class BeginPlayerSessionOptionsAccountId
	{
		// Token: 0x06004444 RID: 17476 RVA: 0x0008C3C3 File Offset: 0x0008A5C3
		public static implicit operator BeginPlayerSessionOptionsAccountId(EpicAccountId value)
		{
			return new BeginPlayerSessionOptionsAccountId
			{
				Epic = value
			};
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x0008C3D1 File Offset: 0x0008A5D1
		public static implicit operator BeginPlayerSessionOptionsAccountId(string value)
		{
			return new BeginPlayerSessionOptionsAccountId
			{
				External = value
			};
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x0008C3DF File Offset: 0x0008A5DF
		// (set) Token: 0x06004447 RID: 17479 RVA: 0x0008C3E7 File Offset: 0x0008A5E7
		public MetricsAccountIdType AccountIdType
		{
			get
			{
				return this.m_AccountIdType;
			}
			private set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x0008C3F0 File Offset: 0x0008A5F0
		// (set) Token: 0x06004449 RID: 17481 RVA: 0x0008C419 File Offset: 0x0008A619
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

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x0008C430 File Offset: 0x0008A630
		// (set) Token: 0x0600444B RID: 17483 RVA: 0x0008C459 File Offset: 0x0008A659
		public string External
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string, MetricsAccountIdType>(this.m_External, out @default, this.m_AccountIdType, MetricsAccountIdType.External);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string, MetricsAccountIdType>(ref this.m_External, value, ref this.m_AccountIdType, MetricsAccountIdType.External, this);
			}
		}

		// Token: 0x04001A3C RID: 6716
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x04001A3D RID: 6717
		private EpicAccountId m_Epic;

		// Token: 0x04001A3E RID: 6718
		private string m_External;
	}
}
