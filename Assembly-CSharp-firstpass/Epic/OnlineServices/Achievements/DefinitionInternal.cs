using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000927 RID: 2343
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DefinitionInternal : IDisposable
	{
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x0600516A RID: 20842 RVA: 0x00099710 File Offset: 0x00097910
		// (set) Token: 0x0600516B RID: 20843 RVA: 0x00099732 File Offset: 0x00097932
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

		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x0600516C RID: 20844 RVA: 0x00099744 File Offset: 0x00097944
		// (set) Token: 0x0600516D RID: 20845 RVA: 0x00099766 File Offset: 0x00097966
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

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x0600516E RID: 20846 RVA: 0x00099778 File Offset: 0x00097978
		// (set) Token: 0x0600516F RID: 20847 RVA: 0x0009979A File Offset: 0x0009799A
		public string DisplayName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_DisplayName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x06005170 RID: 20848 RVA: 0x000997AC File Offset: 0x000979AC
		// (set) Token: 0x06005171 RID: 20849 RVA: 0x000997CE File Offset: 0x000979CE
		public string Description
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_Description, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_Description, value);
			}
		}

		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x06005172 RID: 20850 RVA: 0x000997E0 File Offset: 0x000979E0
		// (set) Token: 0x06005173 RID: 20851 RVA: 0x00099802 File Offset: 0x00097A02
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

		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x06005174 RID: 20852 RVA: 0x00099814 File Offset: 0x00097A14
		// (set) Token: 0x06005175 RID: 20853 RVA: 0x00099836 File Offset: 0x00097A36
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

		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06005176 RID: 20854 RVA: 0x00099848 File Offset: 0x00097A48
		// (set) Token: 0x06005177 RID: 20855 RVA: 0x0009986A File Offset: 0x00097A6A
		public string HiddenDescription
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_HiddenDescription, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_HiddenDescription, value);
			}
		}

		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06005178 RID: 20856 RVA: 0x0009987C File Offset: 0x00097A7C
		// (set) Token: 0x06005179 RID: 20857 RVA: 0x0009989E File Offset: 0x00097A9E
		public string CompletionDescription
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_CompletionDescription, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_CompletionDescription, value);
			}
		}

		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x0600517A RID: 20858 RVA: 0x000998B0 File Offset: 0x00097AB0
		// (set) Token: 0x0600517B RID: 20859 RVA: 0x000998D2 File Offset: 0x00097AD2
		public string UnlockedIconId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_UnlockedIconId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_UnlockedIconId, value);
			}
		}

		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x0600517C RID: 20860 RVA: 0x000998E4 File Offset: 0x00097AE4
		// (set) Token: 0x0600517D RID: 20861 RVA: 0x00099906 File Offset: 0x00097B06
		public string LockedIconId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LockedIconId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LockedIconId, value);
			}
		}

		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600517E RID: 20862 RVA: 0x00099918 File Offset: 0x00097B18
		// (set) Token: 0x0600517F RID: 20863 RVA: 0x0009993A File Offset: 0x00097B3A
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

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06005180 RID: 20864 RVA: 0x0009994C File Offset: 0x00097B4C
		// (set) Token: 0x06005181 RID: 20865 RVA: 0x00099974 File Offset: 0x00097B74
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

		// Token: 0x06005182 RID: 20866 RVA: 0x00099989 File Offset: 0x00097B89
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_StatThresholds);
		}

		// Token: 0x04001FAA RID: 8106
		private int m_ApiVersion;

		// Token: 0x04001FAB RID: 8107
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AchievementId;

		// Token: 0x04001FAC RID: 8108
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_DisplayName;

		// Token: 0x04001FAD RID: 8109
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_Description;

		// Token: 0x04001FAE RID: 8110
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LockedDisplayName;

		// Token: 0x04001FAF RID: 8111
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LockedDescription;

		// Token: 0x04001FB0 RID: 8112
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_HiddenDescription;

		// Token: 0x04001FB1 RID: 8113
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_CompletionDescription;

		// Token: 0x04001FB2 RID: 8114
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_UnlockedIconId;

		// Token: 0x04001FB3 RID: 8115
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LockedIconId;

		// Token: 0x04001FB4 RID: 8116
		private int m_IsHidden;

		// Token: 0x04001FB5 RID: 8117
		private int m_StatThresholdsCount;

		// Token: 0x04001FB6 RID: 8118
		private IntPtr m_StatThresholds;
	}
}
