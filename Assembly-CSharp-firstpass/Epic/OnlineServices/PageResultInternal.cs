using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices
{
	// Token: 0x02000535 RID: 1333
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PageResultInternal : IDisposable
	{
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06003882 RID: 14466 RVA: 0x00080904 File Offset: 0x0007EB04
		// (set) Token: 0x06003883 RID: 14467 RVA: 0x00080926 File Offset: 0x0007EB26
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

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06003884 RID: 14468 RVA: 0x00080938 File Offset: 0x0007EB38
		// (set) Token: 0x06003885 RID: 14469 RVA: 0x0008095A File Offset: 0x0007EB5A
		public int Count
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_Count, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_Count, value);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x0008096C File Offset: 0x0007EB6C
		// (set) Token: 0x06003887 RID: 14471 RVA: 0x0008098E File Offset: 0x0007EB8E
		public int TotalCount
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_TotalCount, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_TotalCount, value);
			}
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x0008099D File Offset: 0x0007EB9D
		public void Dispose()
		{
		}

		// Token: 0x04001470 RID: 5232
		private int m_StartIndex;

		// Token: 0x04001471 RID: 5233
		private int m_Count;

		// Token: 0x04001472 RID: 5234
		private int m_TotalCount;
	}
}
