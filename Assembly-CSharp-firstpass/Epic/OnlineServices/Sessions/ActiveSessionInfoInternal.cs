using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020005C2 RID: 1474
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionInfoInternal : IDisposable
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x00084068 File Offset: 0x00082268
		// (set) Token: 0x06003C10 RID: 15376 RVA: 0x0008408A File Offset: 0x0008228A
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

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x0008409C File Offset: 0x0008229C
		// (set) Token: 0x06003C12 RID: 15378 RVA: 0x000840BE File Offset: 0x000822BE
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

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06003C13 RID: 15379 RVA: 0x000840D0 File Offset: 0x000822D0
		// (set) Token: 0x06003C14 RID: 15380 RVA: 0x000840F2 File Offset: 0x000822F2
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

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x00084104 File Offset: 0x00082304
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x00084126 File Offset: 0x00082326
		public OnlineSessionState State
		{
			get
			{
				OnlineSessionState @default = Helper.GetDefault<OnlineSessionState>();
				Helper.TryMarshalGet<OnlineSessionState>(this.m_State, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<OnlineSessionState>(ref this.m_State, value);
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x00084138 File Offset: 0x00082338
		// (set) Token: 0x06003C18 RID: 15384 RVA: 0x0008415A File Offset: 0x0008235A
		public SessionDetailsInfoInternal? SessionDetails
		{
			get
			{
				SessionDetailsInfoInternal? @default = Helper.GetDefault<SessionDetailsInfoInternal?>();
				Helper.TryMarshalGet<SessionDetailsInfoInternal>(this.m_SessionDetails, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<SessionDetailsInfoInternal>(ref this.m_SessionDetails, value);
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x00084169 File Offset: 0x00082369
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionDetails);
		}

		// Token: 0x040016E9 RID: 5865
		private int m_ApiVersion;

		// Token: 0x040016EA RID: 5866
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_SessionName;

		// Token: 0x040016EB RID: 5867
		private IntPtr m_LocalUserId;

		// Token: 0x040016EC RID: 5868
		private OnlineSessionState m_State;

		// Token: 0x040016ED RID: 5869
		private IntPtr m_SessionDetails;
	}
}
