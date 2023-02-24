using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000883 RID: 2179
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransactionCopyEntitlementByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x06004D70 RID: 19824 RVA: 0x00095814 File Offset: 0x00093A14
		// (set) Token: 0x06004D71 RID: 19825 RVA: 0x00095836 File Offset: 0x00093A36
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

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x06004D72 RID: 19826 RVA: 0x00095848 File Offset: 0x00093A48
		// (set) Token: 0x06004D73 RID: 19827 RVA: 0x0009586A File Offset: 0x00093A6A
		public uint EntitlementIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_EntitlementIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_EntitlementIndex, value);
			}
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x00095879 File Offset: 0x00093A79
		public void Dispose()
		{
		}

		// Token: 0x04001E07 RID: 7687
		private int m_ApiVersion;

		// Token: 0x04001E08 RID: 7688
		private uint m_EntitlementIndex;
	}
}
