using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200068B RID: 1675
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetStatusOptionsInternal : IDisposable
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x00088A74 File Offset: 0x00086C74
		// (set) Token: 0x060040A4 RID: 16548 RVA: 0x00088A96 File Offset: 0x00086C96
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

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060040A5 RID: 16549 RVA: 0x00088AA8 File Offset: 0x00086CA8
		// (set) Token: 0x060040A6 RID: 16550 RVA: 0x00088ACA File Offset: 0x00086CCA
		public Status Status
		{
			get
			{
				Status @default = Helper.GetDefault<Status>();
				Helper.TryMarshalGet<Status>(this.m_Status, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<Status>(ref this.m_Status, value);
			}
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x00088AD9 File Offset: 0x00086CD9
		public void Dispose()
		{
		}

		// Token: 0x040018B5 RID: 6325
		private int m_ApiVersion;

		// Token: 0x040018B6 RID: 6326
		private Status m_Status;
	}
}
