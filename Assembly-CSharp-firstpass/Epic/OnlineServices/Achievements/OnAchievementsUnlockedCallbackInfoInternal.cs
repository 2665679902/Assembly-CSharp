using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000933 RID: 2355
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnAchievementsUnlockedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060051D6 RID: 20950 RVA: 0x00099E24 File Offset: 0x00098024
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060051D7 RID: 20951 RVA: 0x00099E46 File Offset: 0x00098046
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060051D8 RID: 20952 RVA: 0x00099E50 File Offset: 0x00098050
		public ProductUserId UserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x060051D9 RID: 20953 RVA: 0x00099E74 File Offset: 0x00098074
		public string[] AchievementIds
		{
			get
			{
				string[] @default = Helper.GetDefault<string[]>();
				Helper.TryMarshalGet<string>(this.m_AchievementIds, out @default, this.m_AchievementsCount);
				return @default;
			}
		}

		// Token: 0x04001FD7 RID: 8151
		private IntPtr m_ClientData;

		// Token: 0x04001FD8 RID: 8152
		private IntPtr m_UserId;

		// Token: 0x04001FD9 RID: 8153
		private uint m_AchievementsCount;

		// Token: 0x04001FDA RID: 8154
		private IntPtr m_AchievementIds;
	}
}
