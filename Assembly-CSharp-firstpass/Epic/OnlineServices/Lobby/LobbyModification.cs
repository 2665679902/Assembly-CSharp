using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000775 RID: 1909
	public sealed class LobbyModification : Handle
	{
		// Token: 0x060046B6 RID: 18102 RVA: 0x0008F306 File Offset: 0x0008D506
		public LobbyModification(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x060046B7 RID: 18103 RVA: 0x0008F310 File Offset: 0x0008D510
		public Result SetPermissionLevel(LobbyModificationSetPermissionLevelOptions options)
		{
			LobbyModificationSetPermissionLevelOptionsInternal lobbyModificationSetPermissionLevelOptionsInternal = Helper.CopyProperties<LobbyModificationSetPermissionLevelOptionsInternal>(options);
			Result result = LobbyModification.EOS_LobbyModification_SetPermissionLevel(base.InnerHandle, ref lobbyModificationSetPermissionLevelOptionsInternal);
			Helper.TryMarshalDispose<LobbyModificationSetPermissionLevelOptionsInternal>(ref lobbyModificationSetPermissionLevelOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060046B8 RID: 18104 RVA: 0x0008F348 File Offset: 0x0008D548
		public Result SetMaxMembers(LobbyModificationSetMaxMembersOptions options)
		{
			LobbyModificationSetMaxMembersOptionsInternal lobbyModificationSetMaxMembersOptionsInternal = Helper.CopyProperties<LobbyModificationSetMaxMembersOptionsInternal>(options);
			Result result = LobbyModification.EOS_LobbyModification_SetMaxMembers(base.InnerHandle, ref lobbyModificationSetMaxMembersOptionsInternal);
			Helper.TryMarshalDispose<LobbyModificationSetMaxMembersOptionsInternal>(ref lobbyModificationSetMaxMembersOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060046B9 RID: 18105 RVA: 0x0008F380 File Offset: 0x0008D580
		public Result AddAttribute(LobbyModificationAddAttributeOptions options)
		{
			LobbyModificationAddAttributeOptionsInternal lobbyModificationAddAttributeOptionsInternal = Helper.CopyProperties<LobbyModificationAddAttributeOptionsInternal>(options);
			Result result = LobbyModification.EOS_LobbyModification_AddAttribute(base.InnerHandle, ref lobbyModificationAddAttributeOptionsInternal);
			Helper.TryMarshalDispose<LobbyModificationAddAttributeOptionsInternal>(ref lobbyModificationAddAttributeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x0008F3B8 File Offset: 0x0008D5B8
		public Result RemoveAttribute(LobbyModificationRemoveAttributeOptions options)
		{
			LobbyModificationRemoveAttributeOptionsInternal lobbyModificationRemoveAttributeOptionsInternal = Helper.CopyProperties<LobbyModificationRemoveAttributeOptionsInternal>(options);
			Result result = LobbyModification.EOS_LobbyModification_RemoveAttribute(base.InnerHandle, ref lobbyModificationRemoveAttributeOptionsInternal);
			Helper.TryMarshalDispose<LobbyModificationRemoveAttributeOptionsInternal>(ref lobbyModificationRemoveAttributeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x0008F3F0 File Offset: 0x0008D5F0
		public Result AddMemberAttribute(LobbyModificationAddMemberAttributeOptions options)
		{
			LobbyModificationAddMemberAttributeOptionsInternal lobbyModificationAddMemberAttributeOptionsInternal = Helper.CopyProperties<LobbyModificationAddMemberAttributeOptionsInternal>(options);
			Result result = LobbyModification.EOS_LobbyModification_AddMemberAttribute(base.InnerHandle, ref lobbyModificationAddMemberAttributeOptionsInternal);
			Helper.TryMarshalDispose<LobbyModificationAddMemberAttributeOptionsInternal>(ref lobbyModificationAddMemberAttributeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x0008F428 File Offset: 0x0008D628
		public Result RemoveMemberAttribute(LobbyModificationRemoveMemberAttributeOptions options)
		{
			LobbyModificationRemoveMemberAttributeOptionsInternal lobbyModificationRemoveMemberAttributeOptionsInternal = Helper.CopyProperties<LobbyModificationRemoveMemberAttributeOptionsInternal>(options);
			Result result = LobbyModification.EOS_LobbyModification_RemoveMemberAttribute(base.InnerHandle, ref lobbyModificationRemoveMemberAttributeOptionsInternal);
			Helper.TryMarshalDispose<LobbyModificationRemoveMemberAttributeOptionsInternal>(ref lobbyModificationRemoveMemberAttributeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x0008F460 File Offset: 0x0008D660
		public void Release()
		{
			LobbyModification.EOS_LobbyModification_Release(base.InnerHandle);
		}

		// Token: 0x060046BE RID: 18110
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_LobbyModification_Release(IntPtr lobbyModificationHandle);

		// Token: 0x060046BF RID: 18111
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyModification_RemoveMemberAttribute(IntPtr handle, ref LobbyModificationRemoveMemberAttributeOptionsInternal options);

		// Token: 0x060046C0 RID: 18112
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyModification_AddMemberAttribute(IntPtr handle, ref LobbyModificationAddMemberAttributeOptionsInternal options);

		// Token: 0x060046C1 RID: 18113
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyModification_RemoveAttribute(IntPtr handle, ref LobbyModificationRemoveAttributeOptionsInternal options);

		// Token: 0x060046C2 RID: 18114
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyModification_AddAttribute(IntPtr handle, ref LobbyModificationAddAttributeOptionsInternal options);

		// Token: 0x060046C3 RID: 18115
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyModification_SetMaxMembers(IntPtr handle, ref LobbyModificationSetMaxMembersOptionsInternal options);

		// Token: 0x060046C4 RID: 18116
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyModification_SetPermissionLevel(IntPtr handle, ref LobbyModificationSetPermissionLevelOptionsInternal options);
	}
}
