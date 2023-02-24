using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200063B RID: 1595
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetJoinInProgressAllowedOptionsInternal : IDisposable
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06003E9A RID: 16026 RVA: 0x00086444 File Offset: 0x00084644
		// (set) Token: 0x06003E9B RID: 16027 RVA: 0x00086466 File Offset: 0x00084666
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

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06003E9C RID: 16028 RVA: 0x00086478 File Offset: 0x00084678
		// (set) Token: 0x06003E9D RID: 16029 RVA: 0x0008649A File Offset: 0x0008469A
		public bool AllowJoinInProgress
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_AllowJoinInProgress, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowJoinInProgress, value);
			}
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x000864A9 File Offset: 0x000846A9
		public void Dispose()
		{
		}

		// Token: 0x040017C9 RID: 6089
		private int m_ApiVersion;

		// Token: 0x040017CA RID: 6090
		private int m_AllowJoinInProgress;
	}
}
