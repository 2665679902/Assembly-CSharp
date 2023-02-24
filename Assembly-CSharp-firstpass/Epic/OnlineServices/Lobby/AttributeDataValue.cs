using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200072F RID: 1839
	public class AttributeDataValue
	{
		// Token: 0x060044C2 RID: 17602 RVA: 0x0008CC47 File Offset: 0x0008AE47
		public static implicit operator AttributeDataValue(long value)
		{
			return new AttributeDataValue
			{
				AsInt64 = new long?(value)
			};
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x0008CC5A File Offset: 0x0008AE5A
		public static implicit operator AttributeDataValue(double value)
		{
			return new AttributeDataValue
			{
				AsDouble = new double?(value)
			};
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x0008CC6D File Offset: 0x0008AE6D
		public static implicit operator AttributeDataValue(bool value)
		{
			return new AttributeDataValue
			{
				AsBool = new bool?(value)
			};
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x0008CC80 File Offset: 0x0008AE80
		public static implicit operator AttributeDataValue(string value)
		{
			return new AttributeDataValue
			{
				AsUtf8 = value
			};
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x0008CC90 File Offset: 0x0008AE90
		// (set) Token: 0x060044C7 RID: 17607 RVA: 0x0008CCB9 File Offset: 0x0008AEB9
		public long? AsInt64
		{
			get
			{
				long? @default = Helper.GetDefault<long?>();
				Helper.TryMarshalGet<long, AttributeType>(this.m_AsInt64, out @default, this.m_ValueType, AttributeType.Int64);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<long, AttributeType>(ref this.m_AsInt64, value, ref this.m_ValueType, AttributeType.Int64, this);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x0008CCD0 File Offset: 0x0008AED0
		// (set) Token: 0x060044C9 RID: 17609 RVA: 0x0008CCF9 File Offset: 0x0008AEF9
		public double? AsDouble
		{
			get
			{
				double? @default = Helper.GetDefault<double?>();
				Helper.TryMarshalGet<double, AttributeType>(this.m_AsDouble, out @default, this.m_ValueType, AttributeType.Double);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<double, AttributeType>(ref this.m_AsDouble, value, ref this.m_ValueType, AttributeType.Double, this);
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x0008CD10 File Offset: 0x0008AF10
		// (set) Token: 0x060044CB RID: 17611 RVA: 0x0008CD39 File Offset: 0x0008AF39
		public bool? AsBool
		{
			get
			{
				bool? @default = Helper.GetDefault<bool?>();
				Helper.TryMarshalGet<bool, AttributeType>(this.m_AsBool, out @default, this.m_ValueType, AttributeType.Boolean);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<bool, AttributeType>(ref this.m_AsBool, value, ref this.m_ValueType, AttributeType.Boolean, this);
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x0008CD50 File Offset: 0x0008AF50
		// (set) Token: 0x060044CD RID: 17613 RVA: 0x0008CD79 File Offset: 0x0008AF79
		public string AsUtf8
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string, AttributeType>(this.m_AsUtf8, out @default, this.m_ValueType, AttributeType.String);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string, AttributeType>(ref this.m_AsUtf8, value, ref this.m_ValueType, AttributeType.String, this);
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x0008CD90 File Offset: 0x0008AF90
		// (set) Token: 0x060044CF RID: 17615 RVA: 0x0008CD98 File Offset: 0x0008AF98
		public AttributeType ValueType
		{
			get
			{
				return this.m_ValueType;
			}
			private set
			{
				this.m_ValueType = value;
			}
		}

		// Token: 0x04001A8E RID: 6798
		private long m_AsInt64;

		// Token: 0x04001A8F RID: 6799
		private double m_AsDouble;

		// Token: 0x04001A90 RID: 6800
		private bool m_AsBool;

		// Token: 0x04001A91 RID: 6801
		private string m_AsUtf8;

		// Token: 0x04001A92 RID: 6802
		private AttributeType m_ValueType;
	}
}
