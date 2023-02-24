using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200076A RID: 1898
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsInfoInternal : IDisposable
	{
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06004626 RID: 17958 RVA: 0x0008E3D4 File Offset: 0x0008C5D4
		// (set) Token: 0x06004627 RID: 17959 RVA: 0x0008E3F6 File Offset: 0x0008C5F6
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

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06004628 RID: 17960 RVA: 0x0008E408 File Offset: 0x0008C608
		// (set) Token: 0x06004629 RID: 17961 RVA: 0x0008E42A File Offset: 0x0008C62A
		public string LobbyId
		{
			get
			{
				string @default = Helper.GetDefault<string>();
				Helper.TryMarshalGet<string>(this.m_LobbyId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600462A RID: 17962 RVA: 0x0008E43C File Offset: 0x0008C63C
		// (set) Token: 0x0600462B RID: 17963 RVA: 0x0008E45E File Offset: 0x0008C65E
		public ProductUserId LobbyOwnerUserId
		{
			get
			{
				ProductUserId @default = Helper.GetDefault<ProductUserId>();
				Helper.TryMarshalGet<ProductUserId>(this.m_LobbyOwnerUserId, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyOwnerUserId, value);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x0008E470 File Offset: 0x0008C670
		// (set) Token: 0x0600462D RID: 17965 RVA: 0x0008E492 File Offset: 0x0008C692
		public LobbyPermissionLevel PermissionLevel
		{
			get
			{
				LobbyPermissionLevel @default = Helper.GetDefault<LobbyPermissionLevel>();
				Helper.TryMarshalGet<LobbyPermissionLevel>(this.m_PermissionLevel, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<LobbyPermissionLevel>(ref this.m_PermissionLevel, value);
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x0008E4A4 File Offset: 0x0008C6A4
		// (set) Token: 0x0600462F RID: 17967 RVA: 0x0008E4C6 File Offset: 0x0008C6C6
		public uint AvailableSlots
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_AvailableSlots, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_AvailableSlots, value);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06004630 RID: 17968 RVA: 0x0008E4D8 File Offset: 0x0008C6D8
		// (set) Token: 0x06004631 RID: 17969 RVA: 0x0008E4FA File Offset: 0x0008C6FA
		public uint MaxMembers
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MaxMembers, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MaxMembers, value);
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06004632 RID: 17970 RVA: 0x0008E50C File Offset: 0x0008C70C
		// (set) Token: 0x06004633 RID: 17971 RVA: 0x0008E52E File Offset: 0x0008C72E
		public bool AllowInvites
		{
			get
			{
				bool @default = Helper.GetDefault<bool>();
				Helper.TryMarshalGet(this.m_AllowInvites, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AllowInvites, value);
			}
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x0008E53D File Offset: 0x0008C73D
		public void Dispose()
		{
		}

		// Token: 0x04001B16 RID: 6934
		private int m_ApiVersion;

		// Token: 0x04001B17 RID: 6935
		[MarshalAs(UnmanagedType.LPStr)]
		private string m_LobbyId;

		// Token: 0x04001B18 RID: 6936
		private IntPtr m_LobbyOwnerUserId;

		// Token: 0x04001B19 RID: 6937
		private LobbyPermissionLevel m_PermissionLevel;

		// Token: 0x04001B1A RID: 6938
		private uint m_AvailableSlots;

		// Token: 0x04001B1B RID: 6939
		private uint m_MaxMembers;

		// Token: 0x04001B1C RID: 6940
		private int m_AllowInvites;
	}
}
