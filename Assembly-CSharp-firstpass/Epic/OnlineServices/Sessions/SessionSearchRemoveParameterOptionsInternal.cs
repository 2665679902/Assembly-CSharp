using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200064C RID: 1612
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchRemoveParameterOptionsInternal : IDisposable
	{
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06003EF2 RID: 16114 RVA: 0x000869CC File Offset: 0x00084BCC
		// (set) Token: 0x06003EF3 RID: 16115 RVA: 0x000869EE File Offset: 0x00084BEE
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

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06003EF4 RID: 16116 RVA: 0x00086A00 File Offset: 0x00084C00
		// (set) Token: 0x06003EF5 RID: 16117 RVA: 0x00086A22 File Offset: 0x00084C22
		public string Key
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Key, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Key, value);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x00086A34 File Offset: 0x00084C34
		// (set) Token: 0x06003EF7 RID: 16119 RVA: 0x00086A56 File Offset: 0x00084C56
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

		// Token: 0x06003EF8 RID: 16120 RVA: 0x00086A65 File Offset: 0x00084C65
		public void Dispose()
		{
		}

		// Token: 0x040017DE RID: 6110
		private int m_ApiVersion;

		// Token: 0x040017DF RID: 6111
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Key;

		// Token: 0x040017E0 RID: 6112
		private ComparisonOp m_ComparisonOp;
	}
}
