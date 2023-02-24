using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000650 RID: 1616
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetParameterOptionsInternal : IDisposable
	{
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06003F08 RID: 16136 RVA: 0x00086B18 File Offset: 0x00084D18
		// (set) Token: 0x06003F09 RID: 16137 RVA: 0x00086B3A File Offset: 0x00084D3A
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

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06003F0A RID: 16138 RVA: 0x00086B4C File Offset: 0x00084D4C
		// (set) Token: 0x06003F0B RID: 16139 RVA: 0x00086B6E File Offset: 0x00084D6E
		public AttributeDataInternal? Parameter
		{
			get
			{
				AttributeDataInternal? @default = Helper.GetDefault<AttributeDataInternal?>();
				Helper.TryMarshalGet<AttributeDataInternal>(this.m_Parameter, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal>(ref this.m_Parameter, value);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06003F0C RID: 16140 RVA: 0x00086B80 File Offset: 0x00084D80
		// (set) Token: 0x06003F0D RID: 16141 RVA: 0x00086BA2 File Offset: 0x00084DA2
		public ComparisonOp ComparisonOp
		{
			get
			{
				ComparisonOp @default = Helper.GetDefault<ComparisonOp>();
				Helper.TryMarshalGet<ComparisonOp>(this.m_ComparisonOp, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<ComparisonOp>(ref this.m_ComparisonOp, value);
			}
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x00086BB1 File Offset: 0x00084DB1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Parameter);
		}

		// Token: 0x040017E6 RID: 6118
		private int m_ApiVersion;

		// Token: 0x040017E7 RID: 6119
		private IntPtr m_Parameter;

		// Token: 0x040017E8 RID: 6120
		private ComparisonOp m_ComparisonOp;
	}
}
