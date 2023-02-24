using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200091F RID: 2335
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPlayerAchievementByAchievementIdOptionsInternal : IDisposable
	{
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x06005124 RID: 20772 RVA: 0x00099354 File Offset: 0x00097554
		// (set) Token: 0x06005125 RID: 20773 RVA: 0x00099376 File Offset: 0x00097576
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

		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x00099388 File Offset: 0x00097588
		// (set) Token: 0x06005127 RID: 20775 RVA: 0x000993AA File Offset: 0x000975AA
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

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x06005128 RID: 20776 RVA: 0x000993BC File Offset: 0x000975BC
		// (set) Token: 0x06005129 RID: 20777 RVA: 0x000993DE File Offset: 0x000975DE
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

		// Token: 0x0600512A RID: 20778 RVA: 0x000993ED File Offset: 0x000975ED
		public void Dispose()
		{
		}

		// Token: 0x04001F8D RID: 8077
		private int m_ApiVersion;

		// Token: 0x04001F8E RID: 8078
		private IntPtr m_UserId;

		// Token: 0x04001F8F RID: 8079
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AchievementId;
	}
}
