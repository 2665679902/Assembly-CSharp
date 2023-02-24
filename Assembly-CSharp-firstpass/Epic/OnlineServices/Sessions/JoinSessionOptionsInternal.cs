using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005EE RID: 1518
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinSessionOptionsInternal : IDisposable
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06003D05 RID: 15621 RVA: 0x00085068 File Offset: 0x00083268
		// (set) Token: 0x06003D06 RID: 15622 RVA: 0x0008508A File Offset: 0x0008328A
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

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06003D07 RID: 15623 RVA: 0x0008509C File Offset: 0x0008329C
		// (set) Token: 0x06003D08 RID: 15624 RVA: 0x000850BE File Offset: 0x000832BE
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

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06003D09 RID: 15625 RVA: 0x000850D0 File Offset: 0x000832D0
		// (set) Token: 0x06003D0A RID: 15626 RVA: 0x000850F2 File Offset: 0x000832F2
		public SessionDetails SessionHandle
		{
			get
			{
				SessionDetails @default = Helper.GetDefault<SessionDetails>();
				Helper.TryMarshalGet<SessionDetails>(this.m_SessionHandle, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionHandle, value);
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x00085104 File Offset: 0x00083304
		// (set) Token: 0x06003D0C RID: 15628 RVA: 0x00085126 File Offset: 0x00083326
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

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06003D0D RID: 15629 RVA: 0x00085138 File Offset: 0x00083338
		// (set) Token: 0x06003D0E RID: 15630 RVA: 0x0008515A File Offset: 0x0008335A
		public bool PresenceEnabled
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_PresenceEnabled, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_PresenceEnabled, value);
			}
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x00085169 File Offset: 0x00083369
		public void Dispose()
		{
		}

		// Token: 0x04001748 RID: 5960
		private int m_ApiVersion;

		// Token: 0x04001749 RID: 5961
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;

		// Token: 0x0400174A RID: 5962
		private IntPtr m_SessionHandle;

		// Token: 0x0400174B RID: 5963
		private IntPtr m_LocalUserId;

		// Token: 0x0400174C RID: 5964
		private int m_PresenceEnabled;
	}
}
