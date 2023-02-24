using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000642 RID: 1602
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchCopySearchResultByIndexOptionsInternal : IDisposable
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06003EC9 RID: 16073 RVA: 0x000867F8 File Offset: 0x000849F8
		// (set) Token: 0x06003ECA RID: 16074 RVA: 0x0008681A File Offset: 0x00084A1A
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

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06003ECB RID: 16075 RVA: 0x0008682C File Offset: 0x00084A2C
		// (set) Token: 0x06003ECC RID: 16076 RVA: 0x0008684E File Offset: 0x00084A4E
		public uint SessionIndex
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_SessionIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_SessionIndex, value);
			}
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x0008685D File Offset: 0x00084A5D
		public void Dispose()
		{
		}

		// Token: 0x040017D2 RID: 6098
		private int m_ApiVersion;

		// Token: 0x040017D3 RID: 6099
		private uint m_SessionIndex;
	}
}
