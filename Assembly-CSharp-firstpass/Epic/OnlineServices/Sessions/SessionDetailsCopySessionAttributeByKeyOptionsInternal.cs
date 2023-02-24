using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000624 RID: 1572
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsCopySessionAttributeByKeyOptionsInternal : IDisposable
	{
		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06003DFB RID: 15867 RVA: 0x000859D0 File Offset: 0x00083BD0
		// (set) Token: 0x06003DFC RID: 15868 RVA: 0x000859F2 File Offset: 0x00083BF2
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

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06003DFD RID: 15869 RVA: 0x00085A04 File Offset: 0x00083C04
		// (set) Token: 0x06003DFE RID: 15870 RVA: 0x00085A26 File Offset: 0x00083C26
		public string AttrKey
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AttrKey, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AttrKey, value);
			}
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x00085A35 File Offset: 0x00083C35
		public void Dispose()
		{
		}

		// Token: 0x0400178E RID: 6030
		private int m_ApiVersion;

		// Token: 0x0400178F RID: 6031
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AttrKey;
	}
}
