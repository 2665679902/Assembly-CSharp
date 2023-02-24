using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200062E RID: 1582
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionInviteReceivedCallbackInfoInternal : ICallbackInfo
	{
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06003E4D RID: 15949 RVA: 0x00085ED4 File Offset: 0x000840D4
		public object ClientData
		{
			get
			{
				object @default = Helper.GetDefault<object>();
				Helper.TryMarshalGet(this.m_ClientData, out @default);
				return @default;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x00085EF6 File Offset: 0x000840F6
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06003E4F RID: 15951 RVA: 0x00085F00 File Offset: 0x00084100
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06003E50 RID: 15952 RVA: 0x00085F24 File Offset: 0x00084124
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x00085F48 File Offset: 0x00084148
		public string InviteId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_InviteId, out @default);
				return @default;
			}
		}

		// Token: 0x040017B3 RID: 6067
		private IntPtr m_ClientData;

		// Token: 0x040017B4 RID: 6068
		private IntPtr m_LocalUserId;

		// Token: 0x040017B5 RID: 6069
		private IntPtr m_TargetUserId;

		// Token: 0x040017B6 RID: 6070
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_InviteId;
	}
}
