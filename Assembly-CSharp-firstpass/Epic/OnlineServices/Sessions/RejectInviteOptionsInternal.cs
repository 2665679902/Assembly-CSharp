using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000616 RID: 1558
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteOptionsInternal : IDisposable
	{
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x000854A4 File Offset: 0x000836A4
		// (set) Token: 0x06003DB0 RID: 15792 RVA: 0x000854C6 File Offset: 0x000836C6
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

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06003DB1 RID: 15793 RVA: 0x000854D8 File Offset: 0x000836D8
		// (set) Token: 0x06003DB2 RID: 15794 RVA: 0x000854FA File Offset: 0x000836FA
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

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06003DB3 RID: 15795 RVA: 0x0008550C File Offset: 0x0008370C
		// (set) Token: 0x06003DB4 RID: 15796 RVA: 0x0008552E File Offset: 0x0008372E
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

		// Token: 0x06003DB5 RID: 15797 RVA: 0x0008553D File Offset: 0x0008373D
		public void Dispose()
		{
		}

		// Token: 0x04001773 RID: 6003
		private int m_ApiVersion;

		// Token: 0x04001774 RID: 6004
		private IntPtr m_LocalUserId;

		// Token: 0x04001775 RID: 6005
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_InviteId;
	}
}
