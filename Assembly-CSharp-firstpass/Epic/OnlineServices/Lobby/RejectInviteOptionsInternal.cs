using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020007C5 RID: 1989
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteOptionsInternal : IDisposable
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600483C RID: 18492 RVA: 0x00090384 File Offset: 0x0008E584
		// (set) Token: 0x0600483D RID: 18493 RVA: 0x000903A6 File Offset: 0x0008E5A6
		public int ApiVersion
		{
			get
			{
				int @default = Helper.GetDefault<int>();
				Helper.TryMarshalGet<int>(this.m_ApiVersion, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<int>(ref this.m_ApiVersion, value);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x0600483E RID: 18494 RVA: 0x000903B8 File Offset: 0x0008E5B8
		// (set) Token: 0x0600483F RID: 18495 RVA: 0x000903DA File Offset: 0x0008E5DA
		public string InviteId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_InviteId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_InviteId, value);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06004840 RID: 18496 RVA: 0x000903EC File Offset: 0x0008E5EC
		// (set) Token: 0x06004841 RID: 18497 RVA: 0x0009040E File Offset: 0x0008E60E
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LocalUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x0009041D File Offset: 0x0008E61D
		public void Dispose()
		{
		}

		// Token: 0x04001BD6 RID: 7126
		private int m_ApiVersion;

		// Token: 0x04001BD7 RID: 7127
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_InviteId;

		// Token: 0x04001BD8 RID: 7128
		private IntPtr m_LocalUserId;
	}
}
