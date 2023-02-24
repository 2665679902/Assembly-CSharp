using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000622 RID: 1570
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsCopySessionAttributeByIndexOptionsInternal : IDisposable
	{
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06003DF2 RID: 15858 RVA: 0x0008594C File Offset: 0x00083B4C
		// (set) Token: 0x06003DF3 RID: 15859 RVA: 0x0008596E File Offset: 0x00083B6E
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

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06003DF4 RID: 15860 RVA: 0x00085980 File Offset: 0x00083B80
		// (set) Token: 0x06003DF5 RID: 15861 RVA: 0x000859A2 File Offset: 0x00083BA2
		public uint AttrIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_AttrIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_AttrIndex, value);
			}
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000859B1 File Offset: 0x00083BB1
		public void Dispose()
		{
		}

		// Token: 0x0400178B RID: 6027
		private int m_ApiVersion;

		// Token: 0x0400178C RID: 6028
		private uint m_AttrIndex;
	}
}
