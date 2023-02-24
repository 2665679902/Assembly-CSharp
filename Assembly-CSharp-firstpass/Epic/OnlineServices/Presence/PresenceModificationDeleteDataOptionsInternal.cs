using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000683 RID: 1667
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationDeleteDataOptionsInternal : IDisposable
	{
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x00088834 File Offset: 0x00086A34
		// (set) Token: 0x06004080 RID: 16512 RVA: 0x00088856 File Offset: 0x00086A56
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

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x00088868 File Offset: 0x00086A68
		// (set) Token: 0x06004082 RID: 16514 RVA: 0x00088890 File Offset: 0x00086A90
		public PresenceModificationDataRecordIdInternal[] Records
		{
			get
			{
				PresenceModificationDataRecordIdInternal[] @default = Helper.GetDefault<PresenceModificationDataRecordIdInternal[]>();
				Helper.TryMarshalGet<PresenceModificationDataRecordIdInternal>(this.m_Records, out @default, this.m_RecordsCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<PresenceModificationDataRecordIdInternal>(ref this.m_Records, value, out this.m_RecordsCount);
			}
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000888A5 File Offset: 0x00086AA5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Records);
		}

		// Token: 0x040018A7 RID: 6311
		private int m_ApiVersion;

		// Token: 0x040018A8 RID: 6312
		private int m_RecordsCount;

		// Token: 0x040018A9 RID: 6313
		private IntPtr m_Records;
	}
}
