using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000917 RID: 2327
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionByAchievementIdOptionsInternal : IDisposable
	{
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x00099134 File Offset: 0x00097334
		// (set) Token: 0x060050FF RID: 20735 RVA: 0x00099156 File Offset: 0x00097356
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

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06005100 RID: 20736 RVA: 0x00099168 File Offset: 0x00097368
		// (set) Token: 0x06005101 RID: 20737 RVA: 0x0009918A File Offset: 0x0009738A
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

		// Token: 0x06005102 RID: 20738 RVA: 0x00099199 File Offset: 0x00097399
		public void Dispose()
		{
		}

		// Token: 0x04001F80 RID: 8064
		private int m_ApiVersion;

		// Token: 0x04001F81 RID: 8065
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AchievementId;
	}
}
