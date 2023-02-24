using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000929 RID: 2345
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DefinitionV2Internal : IDisposable
	{
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06005199 RID: 20889 RVA: 0x00099A4C File Offset: 0x00097C4C
		// (set) Token: 0x0600519A RID: 20890 RVA: 0x00099A6E File Offset: 0x00097C6E
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

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x00099A80 File Offset: 0x00097C80
		// (set) Token: 0x0600519C RID: 20892 RVA: 0x00099AA2 File Offset: 0x00097CA2
		public string AchievementId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AchievementId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_AchievementId, value);
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600519D RID: 20893 RVA: 0x00099AB4 File Offset: 0x00097CB4
		// (set) Token: 0x0600519E RID: 20894 RVA: 0x00099AD6 File Offset: 0x00097CD6
		public string UnlockedDisplayName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_UnlockedDisplayName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_UnlockedDisplayName, value);
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x0600519F RID: 20895 RVA: 0x00099AE8 File Offset: 0x00097CE8
		// (set) Token: 0x060051A0 RID: 20896 RVA: 0x00099B0A File Offset: 0x00097D0A
		public string UnlockedDescription
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_UnlockedDescription, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_UnlockedDescription, value);
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x060051A1 RID: 20897 RVA: 0x00099B1C File Offset: 0x00097D1C
		// (set) Token: 0x060051A2 RID: 20898 RVA: 0x00099B3E File Offset: 0x00097D3E
		public string LockedDisplayName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LockedDisplayName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LockedDisplayName, value);
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060051A3 RID: 20899 RVA: 0x00099B50 File Offset: 0x00097D50
		// (set) Token: 0x060051A4 RID: 20900 RVA: 0x00099B72 File Offset: 0x00097D72
		public string LockedDescription
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LockedDescription, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LockedDescription, value);
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060051A5 RID: 20901 RVA: 0x00099B84 File Offset: 0x00097D84
		// (set) Token: 0x060051A6 RID: 20902 RVA: 0x00099BA6 File Offset: 0x00097DA6
		public string FlavorText
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_FlavorText, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_FlavorText, value);
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060051A7 RID: 20903 RVA: 0x00099BB8 File Offset: 0x00097DB8
		// (set) Token: 0x060051A8 RID: 20904 RVA: 0x00099BDA File Offset: 0x00097DDA
		public string UnlockedIconURL
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_UnlockedIconURL, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_UnlockedIconURL, value);
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x00099BEC File Offset: 0x00097DEC
		// (set) Token: 0x060051AA RID: 20906 RVA: 0x00099C0E File Offset: 0x00097E0E
		public string LockedIconURL
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LockedIconURL, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LockedIconURL, value);
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060051AB RID: 20907 RVA: 0x00099C20 File Offset: 0x00097E20
		// (set) Token: 0x060051AC RID: 20908 RVA: 0x00099C42 File Offset: 0x00097E42
		public bool IsHidden
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_IsHidden, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_IsHidden, value);
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060051AD RID: 20909 RVA: 0x00099C54 File Offset: 0x00097E54
		// (set) Token: 0x060051AE RID: 20910 RVA: 0x00099C7C File Offset: 0x00097E7C
		public StatThresholdsInternal[] StatThresholds
		{
			get
			{
				StatThresholdsInternal[] @default = Helper.GetDefault<StatThresholdsInternal[]>();
				Helper.TryMarshalGet<StatThresholdsInternal>(this.m_StatThresholds, out @default, this.m_StatThresholdsCount);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<StatThresholdsInternal>(ref this.m_StatThresholds, value, out this.m_StatThresholdsCount);
			}
		}

		// Token: 0x060051AF RID: 20911 RVA: 0x00099C91 File Offset: 0x00097E91
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_StatThresholds);
		}

		// Token: 0x04001FC1 RID: 8129
		private int m_ApiVersion;

		// Token: 0x04001FC2 RID: 8130
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AchievementId;

		// Token: 0x04001FC3 RID: 8131
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_UnlockedDisplayName;

		// Token: 0x04001FC4 RID: 8132
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_UnlockedDescription;

		// Token: 0x04001FC5 RID: 8133
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LockedDisplayName;

		// Token: 0x04001FC6 RID: 8134
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LockedDescription;

		// Token: 0x04001FC7 RID: 8135
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_FlavorText;

		// Token: 0x04001FC8 RID: 8136
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_UnlockedIconURL;

		// Token: 0x04001FC9 RID: 8137
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LockedIconURL;

		// Token: 0x04001FCA RID: 8138
		private int m_IsHidden;

		// Token: 0x04001FCB RID: 8139
		private uint m_StatThresholdsCount;

		// Token: 0x04001FCC RID: 8140
		private IntPtr m_StatThresholds;
	}
}
