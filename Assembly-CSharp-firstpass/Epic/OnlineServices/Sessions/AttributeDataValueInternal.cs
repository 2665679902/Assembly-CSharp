using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005CC RID: 1484
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	internal struct AttributeDataValueInternal : IDisposable
	{
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06003C45 RID: 15429 RVA: 0x00084470 File Offset: 0x00082670
		// (set) Token: 0x06003C46 RID: 15430 RVA: 0x00084499 File Offset: 0x00082699
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

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06003C47 RID: 15431 RVA: 0x000844BC File Offset: 0x000826BC
		// (set) Token: 0x06003C48 RID: 15432 RVA: 0x000844E5 File Offset: 0x000826E5
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

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06003C49 RID: 15433 RVA: 0x00084508 File Offset: 0x00082708
		// (set) Token: 0x06003C4A RID: 15434 RVA: 0x00084531 File Offset: 0x00082731
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

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06003C4B RID: 15435 RVA: 0x00084554 File Offset: 0x00082754
		// (set) Token: 0x06003C4C RID: 15436 RVA: 0x0008457D File Offset: 0x0008277D
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

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06003C4D RID: 15437 RVA: 0x000845A0 File Offset: 0x000827A0
		// (set) Token: 0x06003C4E RID: 15438 RVA: 0x000845C2 File Offset: 0x000827C2
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

		// Token: 0x06003C4F RID: 15439 RVA: 0x000845D1 File Offset: 0x000827D1
		public void Dispose()
		{
			Helper.TryMarshalDispose<AttributeType>(ref this.m_AsUtf8, this.m_ValueType, AttributeType.String);
		}

		// Token: 0x040016FB RID: 5883
		[FieldOffset(0)]
		private long m_AsInt64;

		// Token: 0x040016FC RID: 5884
		[FieldOffset(0)]
		private double m_AsDouble;

		// Token: 0x040016FD RID: 5885
		[FieldOffset(0)]
		private int m_AsBool;

		// Token: 0x040016FE RID: 5886
		[FieldOffset(0)]
		private IntPtr m_AsUtf8;

		// Token: 0x040016FF RID: 5887
		[FieldOffset(8)]
		private AttributeType m_ValueType;
	}
}
