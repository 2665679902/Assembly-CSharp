using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005CB RID: 1483
	public class AttributeDataValue
	{
		// Token: 0x06003C36 RID: 15414 RVA: 0x0008430B File Offset: 0x0008250B
		public static implicit operator AttributeDataValue(long value)
		{
			return new AttributeDataValue
			{
				AsInt64 = new long?(value)
			};
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x0008431E File Offset: 0x0008251E
		public static implicit operator AttributeDataValue(double value)
		{
			return new AttributeDataValue
			{
				AsDouble = new double?(value)
			};
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x00084331 File Offset: 0x00082531
		public static implicit operator AttributeDataValue(bool value)
		{
			return new AttributeDataValue
			{
				AsBool = new bool?(value)
			};
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x00084344 File Offset: 0x00082544
		public static implicit operator AttributeDataValue(string value)
		{
			return new AttributeDataValue
			{
				AsUtf8 = value
			};
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06003C3A RID: 15418 RVA: 0x00084354 File Offset: 0x00082554
		// (set) Token: 0x06003C3B RID: 15419 RVA: 0x0008437D File Offset: 0x0008257D
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

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06003C3C RID: 15420 RVA: 0x00084394 File Offset: 0x00082594
		// (set) Token: 0x06003C3D RID: 15421 RVA: 0x000843BD File Offset: 0x000825BD
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

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06003C3E RID: 15422 RVA: 0x000843D4 File Offset: 0x000825D4
		// (set) Token: 0x06003C3F RID: 15423 RVA: 0x000843FD File Offset: 0x000825FD
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

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06003C40 RID: 15424 RVA: 0x00084414 File Offset: 0x00082614
		// (set) Token: 0x06003C41 RID: 15425 RVA: 0x0008443D File Offset: 0x0008263D
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

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06003C42 RID: 15426 RVA: 0x00084454 File Offset: 0x00082654
		// (set) Token: 0x06003C43 RID: 15427 RVA: 0x0008445C File Offset: 0x0008265C
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

		// Token: 0x040016F6 RID: 5878
		private long m_AsInt64;

		// Token: 0x040016F7 RID: 5879
		private double m_AsDouble;

		// Token: 0x040016F8 RID: 5880
		private bool m_AsBool;

		// Token: 0x040016F9 RID: 5881
		private string m_AsUtf8;

		// Token: 0x040016FA RID: 5882
		private AttributeType m_ValueType;
	}
}
