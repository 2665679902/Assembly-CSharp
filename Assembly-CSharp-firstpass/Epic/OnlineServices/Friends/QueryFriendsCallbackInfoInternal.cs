using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000819 RID: 2073
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFriendsCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06004A2F RID: 18991 RVA: 0x000921CC File Offset: 0x000903CC
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06004A30 RID: 18992 RVA: 0x000921F0 File Offset: 0x000903F0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06004A31 RID: 18993 RVA: 0x00092212 File Offset: 0x00090412
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x0009221C File Offset: 0x0009041C
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x04001C98 RID: 7320
		private Result m_ResultCode;

		// Token: 0x04001C99 RID: 7321
		private IntPtr m_ClientData;

		// Token: 0x04001C9A RID: 7322
		private IntPtr m_LocalUserId;
	}
}
