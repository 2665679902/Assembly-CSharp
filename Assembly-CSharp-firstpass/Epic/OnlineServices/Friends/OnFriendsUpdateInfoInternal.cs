using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000811 RID: 2065
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnFriendsUpdateInfoInternal : ICallbackInfo
	{
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06004A0A RID: 18954 RVA: 0x000920D4 File Offset: 0x000902D4
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06004A0B RID: 18955 RVA: 0x000920F6 File Offset: 0x000902F6
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06004A0C RID: 18956 RVA: 0x00092100 File Offset: 0x00090300
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06004A0D RID: 18957 RVA: 0x00092124 File Offset: 0x00090324
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06004A0E RID: 18958 RVA: 0x00092148 File Offset: 0x00090348
		public FriendsStatus PreviousStatus
		{
			get
			{
				FriendsStatus @default = Helper.GetDefault<FriendsStatus>();
				Helper.TryMarshalGet<FriendsStatus>(this.m_PreviousStatus, out @default);
				return @default;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06004A0F RID: 18959 RVA: 0x0009216C File Offset: 0x0009036C
		public FriendsStatus CurrentStatus
		{
			get
			{
				FriendsStatus @default = Helper.GetDefault<FriendsStatus>();
				Helper.TryMarshalGet<FriendsStatus>(this.m_CurrentStatus, out @default);
				return @default;
			}
		}

		// Token: 0x04001C90 RID: 7312
		private IntPtr m_ClientData;

		// Token: 0x04001C91 RID: 7313
		private IntPtr m_LocalUserId;

		// Token: 0x04001C92 RID: 7314
		private IntPtr m_TargetUserId;

		// Token: 0x04001C93 RID: 7315
		private FriendsStatus m_PreviousStatus;

		// Token: 0x04001C94 RID: 7316
		private FriendsStatus m_CurrentStatus;
	}
}
