using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200062A RID: 1578
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsSettingsInternal : IDisposable
	{
		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06003E26 RID: 15910 RVA: 0x00085C38 File Offset: 0x00083E38
		// (set) Token: 0x06003E27 RID: 15911 RVA: 0x00085C5A File Offset: 0x00083E5A
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

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06003E28 RID: 15912 RVA: 0x00085C6C File Offset: 0x00083E6C
		// (set) Token: 0x06003E29 RID: 15913 RVA: 0x00085C8E File Offset: 0x00083E8E
		public string BucketId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_BucketId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_BucketId, value);
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06003E2A RID: 15914 RVA: 0x00085CA0 File Offset: 0x00083EA0
		// (set) Token: 0x06003E2B RID: 15915 RVA: 0x00085CC2 File Offset: 0x00083EC2
		public uint NumPublicConnections
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_NumPublicConnections, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_NumPublicConnections, value);
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06003E2C RID: 15916 RVA: 0x00085CD4 File Offset: 0x00083ED4
		// (set) Token: 0x06003E2D RID: 15917 RVA: 0x00085CF6 File Offset: 0x00083EF6
		public bool AllowJoinInProgress
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_AllowJoinInProgress, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowJoinInProgress, value);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06003E2E RID: 15918 RVA: 0x00085D08 File Offset: 0x00083F08
		// (set) Token: 0x06003E2F RID: 15919 RVA: 0x00085D2A File Offset: 0x00083F2A
		public OnlineSessionPermissionLevel PermissionLevel
		{
			get
			{
				OnlineSessionPermissionLevel @default = Helper.GetDefault<OnlineSessionPermissionLevel>();
				Helper.TryMarshalGet<OnlineSessionPermissionLevel>(this.m_PermissionLevel, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<OnlineSessionPermissionLevel>(ref this.m_PermissionLevel, value);
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x00085D3C File Offset: 0x00083F3C
		// (set) Token: 0x06003E31 RID: 15921 RVA: 0x00085D5E File Offset: 0x00083F5E
		public bool InvitesAllowed
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_InvitesAllowed, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_InvitesAllowed, value);
			}
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x00085D6D File Offset: 0x00083F6D
		public void Dispose()
		{
		}

		// Token: 0x0400179F RID: 6047
		private int m_ApiVersion;

		// Token: 0x040017A0 RID: 6048
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_BucketId;

		// Token: 0x040017A1 RID: 6049
		private uint m_NumPublicConnections;

		// Token: 0x040017A2 RID: 6050
		private int m_AllowJoinInProgress;

		// Token: 0x040017A3 RID: 6051
		private OnlineSessionPermissionLevel m_PermissionLevel;

		// Token: 0x040017A4 RID: 6052
		private int m_InvitesAllowed;
	}
}
