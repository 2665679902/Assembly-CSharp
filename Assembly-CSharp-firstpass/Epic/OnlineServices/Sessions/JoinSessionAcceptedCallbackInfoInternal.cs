using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005EA RID: 1514
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinSessionAcceptedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06003CEF RID: 15599 RVA: 0x00084F2C File Offset: 0x0008312C
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x00084F4E File Offset: 0x0008314E
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x00084F58 File Offset: 0x00083158
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x00084F7C File Offset: 0x0008317C
		public ulong UiEventId
		{
			get
			{
				ulong @default = Helper.GetDefault<ulong>();
				Helper.TryMarshalGet<ulong>(this.m_UiEventId, out @default);
				return @default;
			}
		}

		// Token: 0x0400173D RID: 5949
		private IntPtr m_ClientData;

		// Token: 0x0400173E RID: 5950
		private IntPtr m_LocalUserId;

		// Token: 0x0400173F RID: 5951
		private ulong m_UiEventId;
	}
}
