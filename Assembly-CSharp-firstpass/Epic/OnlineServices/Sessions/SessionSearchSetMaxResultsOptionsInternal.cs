using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200064E RID: 1614
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetMaxResultsOptionsInternal : IDisposable
	{
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06003EFD RID: 16125 RVA: 0x00086A84 File Offset: 0x00084C84
		// (set) Token: 0x06003EFE RID: 16126 RVA: 0x00086AA6 File Offset: 0x00084CA6
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

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06003EFF RID: 16127 RVA: 0x00086AB8 File Offset: 0x00084CB8
		// (set) Token: 0x06003F00 RID: 16128 RVA: 0x00086ADA File Offset: 0x00084CDA
		public uint MaxSearchResults
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MaxSearchResults, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MaxSearchResults, value);
			}
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x00086AE9 File Offset: 0x00084CE9
		public void Dispose()
		{
		}

		// Token: 0x040017E2 RID: 6114
		private int m_ApiVersion;

		// Token: 0x040017E3 RID: 6115
		private uint m_MaxSearchResults;
	}
}
