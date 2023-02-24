using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005D8 RID: 1496
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateSessionSearchOptionsInternal : IDisposable
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06003C95 RID: 15509 RVA: 0x000849F0 File Offset: 0x00082BF0
		// (set) Token: 0x06003C96 RID: 15510 RVA: 0x00084A12 File Offset: 0x00082C12
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

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06003C97 RID: 15511 RVA: 0x00084A24 File Offset: 0x00082C24
		// (set) Token: 0x06003C98 RID: 15512 RVA: 0x00084A46 File Offset: 0x00082C46
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

		// Token: 0x06003C99 RID: 15513 RVA: 0x00084A55 File Offset: 0x00082C55
		public void Dispose()
		{
		}

		// Token: 0x0400171A RID: 5914
		private int m_ApiVersion;

		// Token: 0x0400171B RID: 5915
		private uint m_MaxSearchResults;
	}
}
