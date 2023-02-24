using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000713 RID: 1811
	public class EndPlayerSessionOptionsAccountId
	{
		// Token: 0x0600445D RID: 17501 RVA: 0x0008C5E7 File Offset: 0x0008A7E7
		public static implicit operator EndPlayerSessionOptionsAccountId(EpicAccountId value)
		{
			return new EndPlayerSessionOptionsAccountId
			{
				Epic = value
			};
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x0008C5F5 File Offset: 0x0008A7F5
		public static implicit operator EndPlayerSessionOptionsAccountId(string value)
		{
			return new EndPlayerSessionOptionsAccountId
			{
				External = value
			};
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x0008C603 File Offset: 0x0008A803
		// (set) Token: 0x06004460 RID: 17504 RVA: 0x0008C60B File Offset: 0x0008A80B
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

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x0008C614 File Offset: 0x0008A814
		// (set) Token: 0x06004462 RID: 17506 RVA: 0x0008C63D File Offset: 0x0008A83D
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

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x0008C654 File Offset: 0x0008A854
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x0008C67D File Offset: 0x0008A87D
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

		// Token: 0x04001A45 RID: 6725
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x04001A46 RID: 6726
		private EpicAccountId m_Epic;

		// Token: 0x04001A47 RID: 6727
		private string m_External;
	}
}
