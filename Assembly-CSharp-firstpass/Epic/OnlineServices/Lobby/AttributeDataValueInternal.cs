using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000730 RID: 1840
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct AttributeDataValueInternal : IDisposable
	{
		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x060044D1 RID: 17617 RVA: 0x0008CDAC File Offset: 0x0008AFAC
		// (set) Token: 0x060044D2 RID: 17618 RVA: 0x0008CDD5 File Offset: 0x0008AFD5
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

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060044D3 RID: 17619 RVA: 0x0008CDF8 File Offset: 0x0008AFF8
		// (set) Token: 0x060044D4 RID: 17620 RVA: 0x0008CE21 File Offset: 0x0008B021
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

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060044D5 RID: 17621 RVA: 0x0008CE44 File Offset: 0x0008B044
		// (set) Token: 0x060044D6 RID: 17622 RVA: 0x0008CE6D File Offset: 0x0008B06D
		public bool? AsBool
		{
			get
			{
				bool? @default = Helper.GetDefault<bool?>();
				Helper.TryMarshalGet<AttributeType>(this.m_AsBool, out @default, this.m_ValueType, AttributeType.Boolean);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AttributeType>(ref this.m_AsBool, value, ref this.m_ValueType, AttributeType.Boolean, this);
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060044D7 RID: 17623 RVA: 0x0008CE90 File Offset: 0x0008B090
		// (set) Token: 0x060044D8 RID: 17624 RVA: 0x0008CEB9 File Offset: 0x0008B0B9
		public string AsUtf8
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<AttributeType>(this.m_AsUtf8, out @default, this.m_ValueType, AttributeType.String);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AttributeType>(ref this.m_AsUtf8, value, ref this.m_ValueType, AttributeType.String, this);
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x0008CEDC File Offset: 0x0008B0DC
		// (set) Token: 0x060044DA RID: 17626 RVA: 0x0008CEFE File Offset: 0x0008B0FE
		public AttributeType ValueType
		{
			get
			{
				AttributeType @default = Helper.GetDefault<AttributeType>();
				Helper.TryMarshalGet<AttributeType>(this.m_ValueType, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AttributeType>(ref this.m_ValueType, value);
			}
		}

		// Token: 0x060044DB RID: 17627 RVA: 0x0008CF0D File Offset: 0x0008B10D
		public void Dispose()
		{
			Helper.TryMarshalDispose<AttributeType>(ref this.m_AsUtf8, this.m_ValueType, AttributeType.String);
		}

		// Token: 0x04001A93 RID: 6803
		[FieldOffset(0)]
		private long m_AsInt64;

		// Token: 0x04001A94 RID: 6804
		[FieldOffset(0)]
		private double m_AsDouble;

		// Token: 0x04001A95 RID: 6805
		[FieldOffset(0)]
		private int m_AsBool;

		// Token: 0x04001A96 RID: 6806
		[FieldOffset(0)]
		private IntPtr m_AsUtf8;

		// Token: 0x04001A97 RID: 6807
		[FieldOffset(8)]
		private AttributeType m_ValueType;
	}
}
