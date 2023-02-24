using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008F3 RID: 2291
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x0600500B RID: 20491 RVA: 0x00098180 File Offset: 0x00096380
		public Result ResultCode
		{
			get
			{
				Result @default = Helper.GetDefault<Result>();
				Helper.TryMarshalGet<Result>(this.m_ResultCode, out @default);
				return @default;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x0600500C RID: 20492 RVA: 0x000981A4 File Offset: 0x000963A4
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600500D RID: 20493 RVA: 0x000981C6 File Offset: 0x000963C6
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x0600500E RID: 20494 RVA: 0x000981D0 File Offset: 0x000963D0
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x0600500F RID: 20495 RVA: 0x000981F4 File Offset: 0x000963F4
		public PinGrantInfoInternal? PinGrantInfo
		{
			get
			{
				PinGrantInfoInternal? @default = Helper.GetDefault<PinGrantInfoInternal?>();
				Helper.TryMarshalGet<PinGrantInfoInternal>(this.m_PinGrantInfo, out @default);
				return @default;
			}
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06005010 RID: 20496 RVA: 0x00098218 File Offset: 0x00096418
		public ContinuanceToken ContinuanceToken
		{
			get
			{
				ContinuanceToken @default = Helper.GetDefault<ContinuanceToken>();
				Helper.TryMarshalGet<ContinuanceToken>(this.m_ContinuanceToken, out @default);
				return @default;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06005011 RID: 20497 RVA: 0x0009823C File Offset: 0x0009643C
		public AccountFeatureRestrictedInfoInternal? AccountFeatureRestrictedInfo
		{
			get
			{
				AccountFeatureRestrictedInfoInternal? @default = Helper.GetDefault<AccountFeatureRestrictedInfoInternal?>();
				Helper.TryMarshalGet<AccountFeatureRestrictedInfoInternal>(this.m_AccountFeatureRestrictedInfo, out @default);
				return @default;
			}
		}

		// Token: 0x04001F19 RID: 7961
		private Result m_ResultCode;

		// Token: 0x04001F1A RID: 7962
		private IntPtr m_ClientData;

		// Token: 0x04001F1B RID: 7963
		private IntPtr m_LocalUserId;

		// Token: 0x04001F1C RID: 7964
		private IntPtr m_PinGrantInfo;

		// Token: 0x04001F1D RID: 7965
		private IntPtr m_ContinuanceToken;

		// Token: 0x04001F1E RID: 7966
		private IntPtr m_AccountFeatureRestrictedInfo;
	}
}
