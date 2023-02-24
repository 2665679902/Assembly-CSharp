using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices
{
	// Token: 0x02000533 RID: 1331
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PageQueryInternal : IDisposable
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x0008082C File Offset: 0x0007EA2C
		// (set) Token: 0x06003875 RID: 14453 RVA: 0x0008084E File Offset: 0x0007EA4E
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

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x00080860 File Offset: 0x0007EA60
		// (set) Token: 0x06003877 RID: 14455 RVA: 0x00080882 File Offset: 0x0007EA82
		public int StartIndex
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_StartIndex, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_StartIndex, value);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06003878 RID: 14456 RVA: 0x00080894 File Offset: 0x0007EA94
		// (set) Token: 0x06003879 RID: 14457 RVA: 0x000808B6 File Offset: 0x0007EAB6
		public int MaxCount
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_MaxCount, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_MaxCount, value);
			}
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x000808C5 File Offset: 0x0007EAC5
		public void Dispose()
		{
		}

		// Token: 0x0400146A RID: 5226
		private int m_ApiVersion;

		// Token: 0x0400146B RID: 5227
		private int m_StartIndex;

		// Token: 0x0400146C RID: 5228
		private int m_MaxCount;
	}
}
