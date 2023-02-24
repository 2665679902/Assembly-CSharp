using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000681 RID: 1665
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationDataRecordIdInternal : IDisposable
	{
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x000887B0 File Offset: 0x000869B0
		// (set) Token: 0x06004077 RID: 16503 RVA: 0x000887D2 File Offset: 0x000869D2
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

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06004078 RID: 16504 RVA: 0x000887E4 File Offset: 0x000869E4
		// (set) Token: 0x06004079 RID: 16505 RVA: 0x00088806 File Offset: 0x00086A06
		public string Key
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Key, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Key, value);
			}
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x00088815 File Offset: 0x00086A15
		public void Dispose()
		{
		}

		// Token: 0x040018A4 RID: 6308
		private int m_ApiVersion;

		// Token: 0x040018A5 RID: 6309
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Key;
	}
}
