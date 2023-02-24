using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200092D RID: 2349
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPlayerAchievementCountOptionsInternal : IDisposable
	{
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x060051B9 RID: 20921 RVA: 0x00099CFC File Offset: 0x00097EFC
		// (set) Token: 0x060051BA RID: 20922 RVA: 0x00099D1E File Offset: 0x00097F1E
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

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x060051BB RID: 20923 RVA: 0x00099D30 File Offset: 0x00097F30
		// (set) Token: 0x060051BC RID: 20924 RVA: 0x00099D52 File Offset: 0x00097F52
		public ProductUserId UserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x060051BD RID: 20925 RVA: 0x00099D61 File Offset: 0x00097F61
		public void Dispose()
		{
		}

		// Token: 0x04001FCF RID: 8143
		private int m_ApiVersion;

		// Token: 0x04001FD0 RID: 8144
		private IntPtr m_UserId;
	}
}
