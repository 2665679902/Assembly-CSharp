using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000943 RID: 2371
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnUnlockAchievementsCompleteCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06005224 RID: 21028 RVA: 0x0009A0F4 File Offset: 0x000982F4
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06005225 RID: 21029 RVA: 0x0009A118 File Offset: 0x00098318
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06005226 RID: 21030 RVA: 0x0009A13A File Offset: 0x0009833A
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06005227 RID: 21031 RVA: 0x0009A144 File Offset: 0x00098344
		public ProductUserId UserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06005228 RID: 21032 RVA: 0x0009A168 File Offset: 0x00098368
		public uint AchievementsCount
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_AchievementsCount, out @default);
				return @default;
			}
		}

		// Token: 0x04001FF1 RID: 8177
		private Result m_ResultCode;

		// Token: 0x04001FF2 RID: 8178
		private IntPtr m_ClientData;

		// Token: 0x04001FF3 RID: 8179
		private IntPtr m_UserId;

		// Token: 0x04001FF4 RID: 8180
		private uint m_AchievementsCount;
	}
}
