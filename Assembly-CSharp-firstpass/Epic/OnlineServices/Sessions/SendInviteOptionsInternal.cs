using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200061A RID: 1562
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteOptionsInternal : IDisposable
	{
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000855F8 File Offset: 0x000837F8
		// (set) Token: 0x06003DC7 RID: 15815 RVA: 0x0008561A File Offset: 0x0008381A
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

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06003DC8 RID: 15816 RVA: 0x0008562C File Offset: 0x0008382C
		// (set) Token: 0x06003DC9 RID: 15817 RVA: 0x0008564E File Offset: 0x0008384E
		public string SessionName
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_SessionName, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_SessionName, value);
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06003DCA RID: 15818 RVA: 0x00085660 File Offset: 0x00083860
		// (set) Token: 0x06003DCB RID: 15819 RVA: 0x00085682 File Offset: 0x00083882
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

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06003DCC RID: 15820 RVA: 0x00085694 File Offset: 0x00083894
		// (set) Token: 0x06003DCD RID: 15821 RVA: 0x000856B6 File Offset: 0x000838B6
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_TargetUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x000856C5 File Offset: 0x000838C5
		public void Dispose()
		{
		}

		// Token: 0x0400177D RID: 6013
		private int m_ApiVersion;

		// Token: 0x0400177E RID: 6014
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;

		// Token: 0x0400177F RID: 6015
		private IntPtr m_LocalUserId;

		// Token: 0x04001780 RID: 6016
		private IntPtr m_TargetUserId;
	}
}
