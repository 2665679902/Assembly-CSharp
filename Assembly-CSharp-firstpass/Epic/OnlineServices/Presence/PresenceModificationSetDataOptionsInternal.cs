using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000685 RID: 1669
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetDataOptionsInternal : IDisposable
	{
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06004088 RID: 16520 RVA: 0x000888D0 File Offset: 0x00086AD0
		// (set) Token: 0x06004089 RID: 16521 RVA: 0x000888F2 File Offset: 0x00086AF2
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

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600408A RID: 16522 RVA: 0x00088904 File Offset: 0x00086B04
		// (set) Token: 0x0600408B RID: 16523 RVA: 0x0008892C File Offset: 0x00086B2C
		public DataRecordInternal[] Records
		{
			get
			{
				DataRecordInternal[] @default = Helper.GetDefault<DataRecordInternal[]>();
				Helper.TryMarshalGet<DataRecordInternal>(this.m_Records, out @default, this.m_RecordsCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<DataRecordInternal>(ref this.m_Records, value, out this.m_RecordsCount);
			}
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x00088941 File Offset: 0x00086B41
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Records);
		}

		// Token: 0x040018AB RID: 6315
		private int m_ApiVersion;

		// Token: 0x040018AC RID: 6316
		private int m_RecordsCount;

		// Token: 0x040018AD RID: 6317
		private IntPtr m_Records;
	}
}
