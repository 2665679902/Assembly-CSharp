using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000675 RID: 1653
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinGameAcceptedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x000881A0 File Offset: 0x000863A0
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x000881C2 File Offset: 0x000863C2
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x000881CC File Offset: 0x000863CC
		public string JoinInfo
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_JoinInfo, out @default);
				return @default;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x000881F0 File Offset: 0x000863F0
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x00088214 File Offset: 0x00086414
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
				Helper.TryMarshalGet<EpicAccountId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x00088238 File Offset: 0x00086438
		public ulong UiEventId
		{
			get
			{
				ulong @default = Helper.GetDefault<ulong>();
				Helper.TryMarshalGet<ulong>(this.m_UiEventId, out @default);
				return @default;
			}
		}

		// Token: 0x0400187F RID: 6271
		private IntPtr m_ClientData;

		// Token: 0x04001880 RID: 6272
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_JoinInfo;

		// Token: 0x04001881 RID: 6273
		private IntPtr m_LocalUserId;

		// Token: 0x04001882 RID: 6274
		private IntPtr m_TargetUserId;

		// Token: 0x04001883 RID: 6275
		private ulong m_UiEventId;
	}
}
