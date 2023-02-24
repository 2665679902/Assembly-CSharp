using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200073A RID: 1850
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateLobbyOptionsInternal : IDisposable
	{
		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06004510 RID: 17680 RVA: 0x0008D1F4 File Offset: 0x0008B3F4
		// (set) Token: 0x06004511 RID: 17681 RVA: 0x0008D216 File Offset: 0x0008B416
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

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x0008D228 File Offset: 0x0008B428
		// (set) Token: 0x06004513 RID: 17683 RVA: 0x0008D24A File Offset: 0x0008B44A
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

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x0008D25C File Offset: 0x0008B45C
		// (set) Token: 0x06004515 RID: 17685 RVA: 0x0008D27E File Offset: 0x0008B47E
		public uint MaxLobbyMembers
		{
			get
			{
				uint @default = Helper.GetDefault<uint>();
				Helper.TryMarshalGet<uint>(this.m_MaxLobbyMembers, out @default);
				return @default;
			}
			set
			{
				Helper.TryMarshalSet<uint>(ref this.m_MaxLobbyMembers, value);
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x0008D290 File Offset: 0x0008B490
		// (set) Token: 0x06004517 RID: 17687 RVA: 0x0008D2B2 File Offset: 0x0008B4B2
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

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06004518 RID: 17688 RVA: 0x0008D2C4 File Offset: 0x0008B4C4
		// (set) Token: 0x06004519 RID: 17689 RVA: 0x0008D2E6 File Offset: 0x0008B4E6
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

		// Token: 0x0600451A RID: 17690 RVA: 0x0008D2F5 File Offset: 0x0008B4F5
		public void Dispose()
		{
		}

		// Token: 0x04001AAD RID: 6829
		private int m_ApiVersion;

		// Token: 0x04001AAE RID: 6830
		private IntPtr m_LocalUserId;

		// Token: 0x04001AAF RID: 6831
		private uint m_MaxLobbyMembers;

		// Token: 0x04001AB0 RID: 6832
		private LobbyPermissionLevel m_PermissionLevel;

		// Token: 0x04001AB1 RID: 6833
		private int m_PresenceEnabled;
	}
}
