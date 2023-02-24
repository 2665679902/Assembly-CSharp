using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000923 RID: 2339
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUnlockedAchievementByAchievementIdOptionsInternal : IDisposable
	{
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x0600513E RID: 20798 RVA: 0x000994E4 File Offset: 0x000976E4
		// (set) Token: 0x0600513F RID: 20799 RVA: 0x00099506 File Offset: 0x00097706
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

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06005140 RID: 20800 RVA: 0x00099518 File Offset: 0x00097718
		// (set) Token: 0x06005141 RID: 20801 RVA: 0x0009953A File Offset: 0x0009773A
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

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x06005142 RID: 20802 RVA: 0x0009954C File Offset: 0x0009774C
		// (set) Token: 0x06005143 RID: 20803 RVA: 0x0009956E File Offset: 0x0009776E
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

		// Token: 0x06005144 RID: 20804 RVA: 0x0009957D File Offset: 0x0009777D
		public void Dispose()
		{
		}

		// Token: 0x04001F97 RID: 8087
		private int m_ApiVersion;

		// Token: 0x04001F98 RID: 8088
		private IntPtr m_UserId;

		// Token: 0x04001F99 RID: 8089
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AchievementId;
	}
}
