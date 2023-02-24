using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000746 RID: 1862
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinLobbyAcceptedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x0008D67C File Offset: 0x0008B87C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x0600455A RID: 17754 RVA: 0x0008D69E File Offset: 0x0008B89E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x0008D6A8 File Offset: 0x0008B8A8
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x0600455C RID: 17756 RVA: 0x0008D6CC File Offset: 0x0008B8CC
		public ulong UiEventId
		{
			get
			{
				ulong @default = Helper.GetDefault<ulong>();
				Helper.TryMarshalGet<ulong>(this.m_UiEventId, out @default);
				return @default;
			}
		}

		// Token: 0x04001ACB RID: 6859
		private IntPtr m_ClientData;

		// Token: 0x04001ACC RID: 6860
		private IntPtr m_LocalUserId;

		// Token: 0x04001ACD RID: 6861
		private ulong m_UiEventId;
	}
}
