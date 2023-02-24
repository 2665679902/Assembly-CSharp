using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000937 RID: 2359
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnAchievementsUnlockedCallbackV2InfoInternal : ICallbackInfo
	{
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060051EB RID: 20971 RVA: 0x00099EE8 File Offset: 0x000980E8
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060051EC RID: 20972 RVA: 0x00099F0A File Offset: 0x0009810A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060051ED RID: 20973 RVA: 0x00099F14 File Offset: 0x00098114
		public ProductUserId UserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060051EE RID: 20974 RVA: 0x00099F38 File Offset: 0x00098138
		public string AchievementId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_AchievementId, out @default);
				return @default;
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x060051EF RID: 20975 RVA: 0x00099F5C File Offset: 0x0009815C
		public DateTimeOffset? UnlockTime
		{
			get
			{
				DateTimeOffset? @default = Helper.GetDefault<DateTimeOffset?>();
				Helper.TryMarshalGet(this.m_UnlockTime, out @default);
				return @default;
			}
		}

		// Token: 0x04001FDF RID: 8159
		private IntPtr m_ClientData;

		// Token: 0x04001FE0 RID: 8160
		private IntPtr m_UserId;

		// Token: 0x04001FE1 RID: 8161
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_AchievementId;

		// Token: 0x04001FE2 RID: 8162
		private long m_UnlockTime;
	}
}
