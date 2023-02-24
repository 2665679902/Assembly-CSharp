using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200076B RID: 1899
	public sealed class LobbyInterface : Handle
	{
		// Token: 0x06004635 RID: 17973 RVA: 0x0008E53F File Offset: 0x0008C73F
		public LobbyInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x0008E548 File Offset: 0x0008C748
		public void CreateLobby(CreateLobbyOptions options, object clientData, OnCreateLobbyCallback completionDelegate)
		{
			CreateLobbyOptionsInternal createLobbyOptionsInternal = Helper.CopyProperties<CreateLobbyOptionsInternal>(options);
			OnCreateLobbyCallbackInternal onCreateLobbyCallbackInternal = new OnCreateLobbyCallbackInternal(LobbyInterface.OnCreateLobby);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onCreateLobbyCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_CreateLobby(base.InnerHandle, ref createLobbyOptionsInternal, zero, onCreateLobbyCallbackInternal);
			Helper.TryMarshalDispose<CreateLobbyOptionsInternal>(ref createLobbyOptionsInternal);
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x0008E598 File Offset: 0x0008C798
		public void DestroyLobby(DestroyLobbyOptions options, object clientData, OnDestroyLobbyCallback completionDelegate)
		{
			DestroyLobbyOptionsInternal destroyLobbyOptionsInternal = Helper.CopyProperties<DestroyLobbyOptionsInternal>(options);
			OnDestroyLobbyCallbackInternal onDestroyLobbyCallbackInternal = new OnDestroyLobbyCallbackInternal(LobbyInterface.OnDestroyLobby);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onDestroyLobbyCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_DestroyLobby(base.InnerHandle, ref destroyLobbyOptionsInternal, zero, onDestroyLobbyCallbackInternal);
			Helper.TryMarshalDispose<DestroyLobbyOptionsInternal>(ref destroyLobbyOptionsInternal);
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x0008E5E8 File Offset: 0x0008C7E8
		public void JoinLobby(JoinLobbyOptions options, object clientData, OnJoinLobbyCallback completionDelegate)
		{
			JoinLobbyOptionsInternal joinLobbyOptionsInternal = Helper.CopyProperties<JoinLobbyOptionsInternal>(options);
			OnJoinLobbyCallbackInternal onJoinLobbyCallbackInternal = new OnJoinLobbyCallbackInternal(LobbyInterface.OnJoinLobby);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onJoinLobbyCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_JoinLobby(base.InnerHandle, ref joinLobbyOptionsInternal, zero, onJoinLobbyCallbackInternal);
			Helper.TryMarshalDispose<JoinLobbyOptionsInternal>(ref joinLobbyOptionsInternal);
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x0008E638 File Offset: 0x0008C838
		public void LeaveLobby(LeaveLobbyOptions options, object clientData, OnLeaveLobbyCallback completionDelegate)
		{
			LeaveLobbyOptionsInternal leaveLobbyOptionsInternal = Helper.CopyProperties<LeaveLobbyOptionsInternal>(options);
			OnLeaveLobbyCallbackInternal onLeaveLobbyCallbackInternal = new OnLeaveLobbyCallbackInternal(LobbyInterface.OnLeaveLobby);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onLeaveLobbyCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_LeaveLobby(base.InnerHandle, ref leaveLobbyOptionsInternal, zero, onLeaveLobbyCallbackInternal);
			Helper.TryMarshalDispose<LeaveLobbyOptionsInternal>(ref leaveLobbyOptionsInternal);
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x0008E688 File Offset: 0x0008C888
		public Result UpdateLobbyModification(UpdateLobbyModificationOptions options, out LobbyModification outLobbyModificationHandle)
		{
			UpdateLobbyModificationOptionsInternal updateLobbyModificationOptionsInternal = Helper.CopyProperties<UpdateLobbyModificationOptionsInternal>(options);
			outLobbyModificationHandle = Helper.GetDefault<LobbyModification>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyInterface.EOS_Lobby_UpdateLobbyModification(base.InnerHandle, ref updateLobbyModificationOptionsInternal, ref zero);
			Helper.TryMarshalDispose<UpdateLobbyModificationOptionsInternal>(ref updateLobbyModificationOptionsInternal);
			Helper.TryMarshalGet<LobbyModification>(zero, out outLobbyModificationHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x0008E6D8 File Offset: 0x0008C8D8
		public void UpdateLobby(UpdateLobbyOptions options, object clientData, OnUpdateLobbyCallback completionDelegate)
		{
			UpdateLobbyOptionsInternal updateLobbyOptionsInternal = Helper.CopyProperties<UpdateLobbyOptionsInternal>(options);
			OnUpdateLobbyCallbackInternal onUpdateLobbyCallbackInternal = new OnUpdateLobbyCallbackInternal(LobbyInterface.OnUpdateLobby);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onUpdateLobbyCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_UpdateLobby(base.InnerHandle, ref updateLobbyOptionsInternal, zero, onUpdateLobbyCallbackInternal);
			Helper.TryMarshalDispose<UpdateLobbyOptionsInternal>(ref updateLobbyOptionsInternal);
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x0008E728 File Offset: 0x0008C928
		public void PromoteMember(PromoteMemberOptions options, object clientData, OnPromoteMemberCallback completionDelegate)
		{
			PromoteMemberOptionsInternal promoteMemberOptionsInternal = Helper.CopyProperties<PromoteMemberOptionsInternal>(options);
			OnPromoteMemberCallbackInternal onPromoteMemberCallbackInternal = new OnPromoteMemberCallbackInternal(LobbyInterface.OnPromoteMember);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onPromoteMemberCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_PromoteMember(base.InnerHandle, ref promoteMemberOptionsInternal, zero, onPromoteMemberCallbackInternal);
			Helper.TryMarshalDispose<PromoteMemberOptionsInternal>(ref promoteMemberOptionsInternal);
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x0008E778 File Offset: 0x0008C978
		public void KickMember(KickMemberOptions options, object clientData, OnKickMemberCallback completionDelegate)
		{
			KickMemberOptionsInternal kickMemberOptionsInternal = Helper.CopyProperties<KickMemberOptionsInternal>(options);
			OnKickMemberCallbackInternal onKickMemberCallbackInternal = new OnKickMemberCallbackInternal(LobbyInterface.OnKickMember);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onKickMemberCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_KickMember(base.InnerHandle, ref kickMemberOptionsInternal, zero, onKickMemberCallbackInternal);
			Helper.TryMarshalDispose<KickMemberOptionsInternal>(ref kickMemberOptionsInternal);
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x0008E7C8 File Offset: 0x0008C9C8
		public ulong AddNotifyLobbyUpdateReceived(AddNotifyLobbyUpdateReceivedOptions options, object clientData, OnLobbyUpdateReceivedCallback notificationFn)
		{
			AddNotifyLobbyUpdateReceivedOptionsInternal addNotifyLobbyUpdateReceivedOptionsInternal = Helper.CopyProperties<AddNotifyLobbyUpdateReceivedOptionsInternal>(options);
			OnLobbyUpdateReceivedCallbackInternal onLobbyUpdateReceivedCallbackInternal = new OnLobbyUpdateReceivedCallbackInternal(LobbyInterface.OnLobbyUpdateReceived);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onLobbyUpdateReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = LobbyInterface.EOS_Lobby_AddNotifyLobbyUpdateReceived(base.InnerHandle, ref addNotifyLobbyUpdateReceivedOptionsInternal, zero, onLobbyUpdateReceivedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyLobbyUpdateReceivedOptionsInternal>(ref addNotifyLobbyUpdateReceivedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x0008E830 File Offset: 0x0008CA30
		public void RemoveNotifyLobbyUpdateReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			LobbyInterface.EOS_Lobby_RemoveNotifyLobbyUpdateReceived(base.InnerHandle, inId);
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x0008E848 File Offset: 0x0008CA48
		public ulong AddNotifyLobbyMemberUpdateReceived(AddNotifyLobbyMemberUpdateReceivedOptions options, object clientData, OnLobbyMemberUpdateReceivedCallback notificationFn)
		{
			AddNotifyLobbyMemberUpdateReceivedOptionsInternal addNotifyLobbyMemberUpdateReceivedOptionsInternal = Helper.CopyProperties<AddNotifyLobbyMemberUpdateReceivedOptionsInternal>(options);
			OnLobbyMemberUpdateReceivedCallbackInternal onLobbyMemberUpdateReceivedCallbackInternal = new OnLobbyMemberUpdateReceivedCallbackInternal(LobbyInterface.OnLobbyMemberUpdateReceived);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onLobbyMemberUpdateReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = LobbyInterface.EOS_Lobby_AddNotifyLobbyMemberUpdateReceived(base.InnerHandle, ref addNotifyLobbyMemberUpdateReceivedOptionsInternal, zero, onLobbyMemberUpdateReceivedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyLobbyMemberUpdateReceivedOptionsInternal>(ref addNotifyLobbyMemberUpdateReceivedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x0008E8B0 File Offset: 0x0008CAB0
		public void RemoveNotifyLobbyMemberUpdateReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			LobbyInterface.EOS_Lobby_RemoveNotifyLobbyMemberUpdateReceived(base.InnerHandle, inId);
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x0008E8C8 File Offset: 0x0008CAC8
		public ulong AddNotifyLobbyMemberStatusReceived(AddNotifyLobbyMemberStatusReceivedOptions options, object clientData, OnLobbyMemberStatusReceivedCallback notificationFn)
		{
			AddNotifyLobbyMemberStatusReceivedOptionsInternal addNotifyLobbyMemberStatusReceivedOptionsInternal = Helper.CopyProperties<AddNotifyLobbyMemberStatusReceivedOptionsInternal>(options);
			OnLobbyMemberStatusReceivedCallbackInternal onLobbyMemberStatusReceivedCallbackInternal = new OnLobbyMemberStatusReceivedCallbackInternal(LobbyInterface.OnLobbyMemberStatusReceived);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onLobbyMemberStatusReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = LobbyInterface.EOS_Lobby_AddNotifyLobbyMemberStatusReceived(base.InnerHandle, ref addNotifyLobbyMemberStatusReceivedOptionsInternal, zero, onLobbyMemberStatusReceivedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyLobbyMemberStatusReceivedOptionsInternal>(ref addNotifyLobbyMemberStatusReceivedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x0008E930 File Offset: 0x0008CB30
		public void RemoveNotifyLobbyMemberStatusReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			LobbyInterface.EOS_Lobby_RemoveNotifyLobbyMemberStatusReceived(base.InnerHandle, inId);
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x0008E948 File Offset: 0x0008CB48
		public void SendInvite(SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
		{
			SendInviteOptionsInternal sendInviteOptionsInternal = Helper.CopyProperties<SendInviteOptionsInternal>(options);
			OnSendInviteCallbackInternal onSendInviteCallbackInternal = new OnSendInviteCallbackInternal(LobbyInterface.OnSendInvite);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onSendInviteCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_SendInvite(base.InnerHandle, ref sendInviteOptionsInternal, zero, onSendInviteCallbackInternal);
			Helper.TryMarshalDispose<SendInviteOptionsInternal>(ref sendInviteOptionsInternal);
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x0008E998 File Offset: 0x0008CB98
		public void RejectInvite(RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
		{
			RejectInviteOptionsInternal rejectInviteOptionsInternal = Helper.CopyProperties<RejectInviteOptionsInternal>(options);
			OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = new OnRejectInviteCallbackInternal(LobbyInterface.OnRejectInvite);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onRejectInviteCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_RejectInvite(base.InnerHandle, ref rejectInviteOptionsInternal, zero, onRejectInviteCallbackInternal);
			Helper.TryMarshalDispose<RejectInviteOptionsInternal>(ref rejectInviteOptionsInternal);
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x0008E9E8 File Offset: 0x0008CBE8
		public void QueryInvites(QueryInvitesOptions options, object clientData, OnQueryInvitesCallback completionDelegate)
		{
			QueryInvitesOptionsInternal queryInvitesOptionsInternal = Helper.CopyProperties<QueryInvitesOptionsInternal>(options);
			OnQueryInvitesCallbackInternal onQueryInvitesCallbackInternal = new OnQueryInvitesCallbackInternal(LobbyInterface.OnQueryInvites);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryInvitesCallbackInternal, Array.Empty<Delegate>());
			LobbyInterface.EOS_Lobby_QueryInvites(base.InnerHandle, ref queryInvitesOptionsInternal, zero, onQueryInvitesCallbackInternal);
			Helper.TryMarshalDispose<QueryInvitesOptionsInternal>(ref queryInvitesOptionsInternal);
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x0008EA38 File Offset: 0x0008CC38
		public uint GetInviteCount(GetInviteCountOptions options)
		{
			GetInviteCountOptionsInternal getInviteCountOptionsInternal = Helper.CopyProperties<GetInviteCountOptionsInternal>(options);
			uint num = LobbyInterface.EOS_Lobby_GetInviteCount(base.InnerHandle, ref getInviteCountOptionsInternal);
			Helper.TryMarshalDispose<GetInviteCountOptionsInternal>(ref getInviteCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x0008EA70 File Offset: 0x0008CC70
		public Result GetInviteIdByIndex(GetInviteIdByIndexOptions options, StringBuilder outBuffer, ref int inOutBufferLength)
		{
			GetInviteIdByIndexOptionsInternal getInviteIdByIndexOptionsInternal = Helper.CopyProperties<GetInviteIdByIndexOptionsInternal>(options);
			Result result = LobbyInterface.EOS_Lobby_GetInviteIdByIndex(base.InnerHandle, ref getInviteIdByIndexOptionsInternal, outBuffer, ref inOutBufferLength);
			Helper.TryMarshalDispose<GetInviteIdByIndexOptionsInternal>(ref getInviteIdByIndexOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x0008EAAC File Offset: 0x0008CCAC
		public Result CreateLobbySearch(CreateLobbySearchOptions options, out LobbySearch outLobbySearchHandle)
		{
			CreateLobbySearchOptionsInternal createLobbySearchOptionsInternal = Helper.CopyProperties<CreateLobbySearchOptionsInternal>(options);
			outLobbySearchHandle = Helper.GetDefault<LobbySearch>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyInterface.EOS_Lobby_CreateLobbySearch(base.InnerHandle, ref createLobbySearchOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CreateLobbySearchOptionsInternal>(ref createLobbySearchOptionsInternal);
			Helper.TryMarshalGet<LobbySearch>(zero, out outLobbySearchHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x0008EAFC File Offset: 0x0008CCFC
		public ulong AddNotifyLobbyInviteReceived(AddNotifyLobbyInviteReceivedOptions options, object clientData, OnLobbyInviteReceivedCallback notificationFn)
		{
			AddNotifyLobbyInviteReceivedOptionsInternal addNotifyLobbyInviteReceivedOptionsInternal = Helper.CopyProperties<AddNotifyLobbyInviteReceivedOptionsInternal>(options);
			OnLobbyInviteReceivedCallbackInternal onLobbyInviteReceivedCallbackInternal = new OnLobbyInviteReceivedCallbackInternal(LobbyInterface.OnLobbyInviteReceived);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onLobbyInviteReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = LobbyInterface.EOS_Lobby_AddNotifyLobbyInviteReceived(base.InnerHandle, ref addNotifyLobbyInviteReceivedOptionsInternal, zero, onLobbyInviteReceivedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyLobbyInviteReceivedOptionsInternal>(ref addNotifyLobbyInviteReceivedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x0008EB64 File Offset: 0x0008CD64
		public void RemoveNotifyLobbyInviteReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			LobbyInterface.EOS_Lobby_RemoveNotifyLobbyInviteReceived(base.InnerHandle, inId);
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x0008EB7C File Offset: 0x0008CD7C
		public ulong AddNotifyLobbyInviteAccepted(AddNotifyLobbyInviteAcceptedOptions options, object clientData, OnLobbyInviteAcceptedCallback notificationFn)
		{
			AddNotifyLobbyInviteAcceptedOptionsInternal addNotifyLobbyInviteAcceptedOptionsInternal = Helper.CopyProperties<AddNotifyLobbyInviteAcceptedOptionsInternal>(options);
			OnLobbyInviteAcceptedCallbackInternal onLobbyInviteAcceptedCallbackInternal = new OnLobbyInviteAcceptedCallbackInternal(LobbyInterface.OnLobbyInviteAccepted);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onLobbyInviteAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = LobbyInterface.EOS_Lobby_AddNotifyLobbyInviteAccepted(base.InnerHandle, ref addNotifyLobbyInviteAcceptedOptionsInternal, zero, onLobbyInviteAcceptedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyLobbyInviteAcceptedOptionsInternal>(ref addNotifyLobbyInviteAcceptedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x0008EBE4 File Offset: 0x0008CDE4
		public void RemoveNotifyLobbyInviteAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			LobbyInterface.EOS_Lobby_RemoveNotifyLobbyInviteAccepted(base.InnerHandle, inId);
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x0008EBFC File Offset: 0x0008CDFC
		public ulong AddNotifyJoinLobbyAccepted(AddNotifyJoinLobbyAcceptedOptions options, object clientData, OnJoinLobbyAcceptedCallback notificationFn)
		{
			AddNotifyJoinLobbyAcceptedOptionsInternal addNotifyJoinLobbyAcceptedOptionsInternal = Helper.CopyProperties<AddNotifyJoinLobbyAcceptedOptionsInternal>(options);
			OnJoinLobbyAcceptedCallbackInternal onJoinLobbyAcceptedCallbackInternal = new OnJoinLobbyAcceptedCallbackInternal(LobbyInterface.OnJoinLobbyAccepted);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onJoinLobbyAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = LobbyInterface.EOS_Lobby_AddNotifyJoinLobbyAccepted(base.InnerHandle, ref addNotifyJoinLobbyAcceptedOptionsInternal, zero, onJoinLobbyAcceptedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyJoinLobbyAcceptedOptionsInternal>(ref addNotifyJoinLobbyAcceptedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x0008EC64 File Offset: 0x0008CE64
		public void RemoveNotifyJoinLobbyAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			LobbyInterface.EOS_Lobby_RemoveNotifyJoinLobbyAccepted(base.InnerHandle, inId);
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x0008EC7C File Offset: 0x0008CE7C
		public Result CopyLobbyDetailsHandleByInviteId(CopyLobbyDetailsHandleByInviteIdOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			CopyLobbyDetailsHandleByInviteIdOptionsInternal copyLobbyDetailsHandleByInviteIdOptionsInternal = Helper.CopyProperties<CopyLobbyDetailsHandleByInviteIdOptionsInternal>(options);
			outLobbyDetailsHandle = Helper.GetDefault<LobbyDetails>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyInterface.EOS_Lobby_CopyLobbyDetailsHandleByInviteId(base.InnerHandle, ref copyLobbyDetailsHandleByInviteIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyLobbyDetailsHandleByInviteIdOptionsInternal>(ref copyLobbyDetailsHandleByInviteIdOptionsInternal);
			Helper.TryMarshalGet<LobbyDetails>(zero, out outLobbyDetailsHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x0008ECCC File Offset: 0x0008CECC
		public Result CopyLobbyDetailsHandleByUiEventId(CopyLobbyDetailsHandleByUiEventIdOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			CopyLobbyDetailsHandleByUiEventIdOptionsInternal copyLobbyDetailsHandleByUiEventIdOptionsInternal = Helper.CopyProperties<CopyLobbyDetailsHandleByUiEventIdOptionsInternal>(options);
			outLobbyDetailsHandle = Helper.GetDefault<LobbyDetails>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyInterface.EOS_Lobby_CopyLobbyDetailsHandleByUiEventId(base.InnerHandle, ref copyLobbyDetailsHandleByUiEventIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyLobbyDetailsHandleByUiEventIdOptionsInternal>(ref copyLobbyDetailsHandleByUiEventIdOptionsInternal);
			Helper.TryMarshalGet<LobbyDetails>(zero, out outLobbyDetailsHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x0008ED1C File Offset: 0x0008CF1C
		public Result CopyLobbyDetailsHandle(CopyLobbyDetailsHandleOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			CopyLobbyDetailsHandleOptionsInternal copyLobbyDetailsHandleOptionsInternal = Helper.CopyProperties<CopyLobbyDetailsHandleOptionsInternal>(options);
			outLobbyDetailsHandle = Helper.GetDefault<LobbyDetails>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyInterface.EOS_Lobby_CopyLobbyDetailsHandle(base.InnerHandle, ref copyLobbyDetailsHandleOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyLobbyDetailsHandleOptionsInternal>(ref copyLobbyDetailsHandleOptionsInternal);
			Helper.TryMarshalGet<LobbyDetails>(zero, out outLobbyDetailsHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x0008ED6C File Offset: 0x0008CF6C
		[MonoPInvokeCallback]
		internal static void OnJoinLobbyAccepted(IntPtr address)
		{
			OnJoinLobbyAcceptedCallback onJoinLobbyAcceptedCallback = null;
			JoinLobbyAcceptedCallbackInfo joinLobbyAcceptedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnJoinLobbyAcceptedCallback, JoinLobbyAcceptedCallbackInfoInternal, JoinLobbyAcceptedCallbackInfo>(address, out onJoinLobbyAcceptedCallback, out joinLobbyAcceptedCallbackInfo))
			{
				onJoinLobbyAcceptedCallback(joinLobbyAcceptedCallbackInfo);
			}
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x0008ED90 File Offset: 0x0008CF90
		[MonoPInvokeCallback]
		internal static void OnLobbyInviteAccepted(IntPtr address)
		{
			OnLobbyInviteAcceptedCallback onLobbyInviteAcceptedCallback = null;
			LobbyInviteAcceptedCallbackInfo lobbyInviteAcceptedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLobbyInviteAcceptedCallback, LobbyInviteAcceptedCallbackInfoInternal, LobbyInviteAcceptedCallbackInfo>(address, out onLobbyInviteAcceptedCallback, out lobbyInviteAcceptedCallbackInfo))
			{
				onLobbyInviteAcceptedCallback(lobbyInviteAcceptedCallbackInfo);
			}
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x0008EDB4 File Offset: 0x0008CFB4
		[MonoPInvokeCallback]
		internal static void OnLobbyInviteReceived(IntPtr address)
		{
			OnLobbyInviteReceivedCallback onLobbyInviteReceivedCallback = null;
			LobbyInviteReceivedCallbackInfo lobbyInviteReceivedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLobbyInviteReceivedCallback, LobbyInviteReceivedCallbackInfoInternal, LobbyInviteReceivedCallbackInfo>(address, out onLobbyInviteReceivedCallback, out lobbyInviteReceivedCallbackInfo))
			{
				onLobbyInviteReceivedCallback(lobbyInviteReceivedCallbackInfo);
			}
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x0008EDD8 File Offset: 0x0008CFD8
		[MonoPInvokeCallback]
		internal static void OnQueryInvites(IntPtr address)
		{
			OnQueryInvitesCallback onQueryInvitesCallback = null;
			QueryInvitesCallbackInfo queryInvitesCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryInvitesCallback, QueryInvitesCallbackInfoInternal, QueryInvitesCallbackInfo>(address, out onQueryInvitesCallback, out queryInvitesCallbackInfo))
			{
				onQueryInvitesCallback(queryInvitesCallbackInfo);
			}
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x0008EDFC File Offset: 0x0008CFFC
		[MonoPInvokeCallback]
		internal static void OnRejectInvite(IntPtr address)
		{
			OnRejectInviteCallback onRejectInviteCallback = null;
			RejectInviteCallbackInfo rejectInviteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnRejectInviteCallback, RejectInviteCallbackInfoInternal, RejectInviteCallbackInfo>(address, out onRejectInviteCallback, out rejectInviteCallbackInfo))
			{
				onRejectInviteCallback(rejectInviteCallbackInfo);
			}
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x0008EE20 File Offset: 0x0008D020
		[MonoPInvokeCallback]
		internal static void OnSendInvite(IntPtr address)
		{
			OnSendInviteCallback onSendInviteCallback = null;
			SendInviteCallbackInfo sendInviteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnSendInviteCallback, SendInviteCallbackInfoInternal, SendInviteCallbackInfo>(address, out onSendInviteCallback, out sendInviteCallbackInfo))
			{
				onSendInviteCallback(sendInviteCallbackInfo);
			}
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x0008EE44 File Offset: 0x0008D044
		[MonoPInvokeCallback]
		internal static void OnLobbyMemberStatusReceived(IntPtr address)
		{
			OnLobbyMemberStatusReceivedCallback onLobbyMemberStatusReceivedCallback = null;
			LobbyMemberStatusReceivedCallbackInfo lobbyMemberStatusReceivedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLobbyMemberStatusReceivedCallback, LobbyMemberStatusReceivedCallbackInfoInternal, LobbyMemberStatusReceivedCallbackInfo>(address, out onLobbyMemberStatusReceivedCallback, out lobbyMemberStatusReceivedCallbackInfo))
			{
				onLobbyMemberStatusReceivedCallback(lobbyMemberStatusReceivedCallbackInfo);
			}
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x0008EE68 File Offset: 0x0008D068
		[MonoPInvokeCallback]
		internal static void OnLobbyMemberUpdateReceived(IntPtr address)
		{
			OnLobbyMemberUpdateReceivedCallback onLobbyMemberUpdateReceivedCallback = null;
			LobbyMemberUpdateReceivedCallbackInfo lobbyMemberUpdateReceivedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLobbyMemberUpdateReceivedCallback, LobbyMemberUpdateReceivedCallbackInfoInternal, LobbyMemberUpdateReceivedCallbackInfo>(address, out onLobbyMemberUpdateReceivedCallback, out lobbyMemberUpdateReceivedCallbackInfo))
			{
				onLobbyMemberUpdateReceivedCallback(lobbyMemberUpdateReceivedCallbackInfo);
			}
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x0008EE8C File Offset: 0x0008D08C
		[MonoPInvokeCallback]
		internal static void OnLobbyUpdateReceived(IntPtr address)
		{
			OnLobbyUpdateReceivedCallback onLobbyUpdateReceivedCallback = null;
			LobbyUpdateReceivedCallbackInfo lobbyUpdateReceivedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLobbyUpdateReceivedCallback, LobbyUpdateReceivedCallbackInfoInternal, LobbyUpdateReceivedCallbackInfo>(address, out onLobbyUpdateReceivedCallback, out lobbyUpdateReceivedCallbackInfo))
			{
				onLobbyUpdateReceivedCallback(lobbyUpdateReceivedCallbackInfo);
			}
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x0008EEB0 File Offset: 0x0008D0B0
		[MonoPInvokeCallback]
		internal static void OnKickMember(IntPtr address)
		{
			OnKickMemberCallback onKickMemberCallback = null;
			KickMemberCallbackInfo kickMemberCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnKickMemberCallback, KickMemberCallbackInfoInternal, KickMemberCallbackInfo>(address, out onKickMemberCallback, out kickMemberCallbackInfo))
			{
				onKickMemberCallback(kickMemberCallbackInfo);
			}
		}

		// Token: 0x0600465D RID: 18013 RVA: 0x0008EED4 File Offset: 0x0008D0D4
		[MonoPInvokeCallback]
		internal static void OnPromoteMember(IntPtr address)
		{
			OnPromoteMemberCallback onPromoteMemberCallback = null;
			PromoteMemberCallbackInfo promoteMemberCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnPromoteMemberCallback, PromoteMemberCallbackInfoInternal, PromoteMemberCallbackInfo>(address, out onPromoteMemberCallback, out promoteMemberCallbackInfo))
			{
				onPromoteMemberCallback(promoteMemberCallbackInfo);
			}
		}

		// Token: 0x0600465E RID: 18014 RVA: 0x0008EEF8 File Offset: 0x0008D0F8
		[MonoPInvokeCallback]
		internal static void OnUpdateLobby(IntPtr address)
		{
			OnUpdateLobbyCallback onUpdateLobbyCallback = null;
			UpdateLobbyCallbackInfo updateLobbyCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnUpdateLobbyCallback, UpdateLobbyCallbackInfoInternal, UpdateLobbyCallbackInfo>(address, out onUpdateLobbyCallback, out updateLobbyCallbackInfo))
			{
				onUpdateLobbyCallback(updateLobbyCallbackInfo);
			}
		}

		// Token: 0x0600465F RID: 18015 RVA: 0x0008EF1C File Offset: 0x0008D11C
		[MonoPInvokeCallback]
		internal static void OnLeaveLobby(IntPtr address)
		{
			OnLeaveLobbyCallback onLeaveLobbyCallback = null;
			LeaveLobbyCallbackInfo leaveLobbyCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLeaveLobbyCallback, LeaveLobbyCallbackInfoInternal, LeaveLobbyCallbackInfo>(address, out onLeaveLobbyCallback, out leaveLobbyCallbackInfo))
			{
				onLeaveLobbyCallback(leaveLobbyCallbackInfo);
			}
		}

		// Token: 0x06004660 RID: 18016 RVA: 0x0008EF40 File Offset: 0x0008D140
		[MonoPInvokeCallback]
		internal static void OnJoinLobby(IntPtr address)
		{
			OnJoinLobbyCallback onJoinLobbyCallback = null;
			JoinLobbyCallbackInfo joinLobbyCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnJoinLobbyCallback, JoinLobbyCallbackInfoInternal, JoinLobbyCallbackInfo>(address, out onJoinLobbyCallback, out joinLobbyCallbackInfo))
			{
				onJoinLobbyCallback(joinLobbyCallbackInfo);
			}
		}

		// Token: 0x06004661 RID: 18017 RVA: 0x0008EF64 File Offset: 0x0008D164
		[MonoPInvokeCallback]
		internal static void OnDestroyLobby(IntPtr address)
		{
			OnDestroyLobbyCallback onDestroyLobbyCallback = null;
			DestroyLobbyCallbackInfo destroyLobbyCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnDestroyLobbyCallback, DestroyLobbyCallbackInfoInternal, DestroyLobbyCallbackInfo>(address, out onDestroyLobbyCallback, out destroyLobbyCallbackInfo))
			{
				onDestroyLobbyCallback(destroyLobbyCallbackInfo);
			}
		}

		// Token: 0x06004662 RID: 18018 RVA: 0x0008EF88 File Offset: 0x0008D188
		[MonoPInvokeCallback]
		internal static void OnCreateLobby(IntPtr address)
		{
			OnCreateLobbyCallback onCreateLobbyCallback = null;
			CreateLobbyCallbackInfo createLobbyCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnCreateLobbyCallback, CreateLobbyCallbackInfoInternal, CreateLobbyCallbackInfo>(address, out onCreateLobbyCallback, out createLobbyCallbackInfo))
			{
				onCreateLobbyCallback(createLobbyCallbackInfo);
			}
		}

		// Token: 0x06004663 RID: 18019
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_Attribute_Release(IntPtr lobbyAttribute);

		// Token: 0x06004664 RID: 18020
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Lobby_CopyLobbyDetailsHandle(IntPtr handle, ref CopyLobbyDetailsHandleOptionsInternal options, ref IntPtr outLobbyDetailsHandle);

		// Token: 0x06004665 RID: 18021
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Lobby_CopyLobbyDetailsHandleByUiEventId(IntPtr handle, ref CopyLobbyDetailsHandleByUiEventIdOptionsInternal options, ref IntPtr outLobbyDetailsHandle);

		// Token: 0x06004666 RID: 18022
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Lobby_CopyLobbyDetailsHandleByInviteId(IntPtr handle, ref CopyLobbyDetailsHandleByInviteIdOptionsInternal options, ref IntPtr outLobbyDetailsHandle);

		// Token: 0x06004667 RID: 18023
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_RemoveNotifyJoinLobbyAccepted(IntPtr handle, ulong inId);

		// Token: 0x06004668 RID: 18024
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Lobby_AddNotifyJoinLobbyAccepted(IntPtr handle, ref AddNotifyJoinLobbyAcceptedOptionsInternal options, IntPtr clientData, OnJoinLobbyAcceptedCallbackInternal notificationFn);

		// Token: 0x06004669 RID: 18025
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_RemoveNotifyLobbyInviteAccepted(IntPtr handle, ulong inId);

		// Token: 0x0600466A RID: 18026
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Lobby_AddNotifyLobbyInviteAccepted(IntPtr handle, ref AddNotifyLobbyInviteAcceptedOptionsInternal options, IntPtr clientData, OnLobbyInviteAcceptedCallbackInternal notificationFn);

		// Token: 0x0600466B RID: 18027
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_RemoveNotifyLobbyInviteReceived(IntPtr handle, ulong inId);

		// Token: 0x0600466C RID: 18028
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Lobby_AddNotifyLobbyInviteReceived(IntPtr handle, ref AddNotifyLobbyInviteReceivedOptionsInternal options, IntPtr clientData, OnLobbyInviteReceivedCallbackInternal notificationFn);

		// Token: 0x0600466D RID: 18029
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Lobby_CreateLobbySearch(IntPtr handle, ref CreateLobbySearchOptionsInternal options, ref IntPtr outLobbySearchHandle);

		// Token: 0x0600466E RID: 18030
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Lobby_GetInviteIdByIndex(IntPtr handle, ref GetInviteIdByIndexOptionsInternal options, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x0600466F RID: 18031
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Lobby_GetInviteCount(IntPtr handle, ref GetInviteCountOptionsInternal options);

		// Token: 0x06004670 RID: 18032
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_QueryInvites(IntPtr handle, ref QueryInvitesOptionsInternal options, IntPtr clientData, OnQueryInvitesCallbackInternal completionDelegate);

		// Token: 0x06004671 RID: 18033
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_RejectInvite(IntPtr handle, ref RejectInviteOptionsInternal options, IntPtr clientData, OnRejectInviteCallbackInternal completionDelegate);

		// Token: 0x06004672 RID: 18034
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_SendInvite(IntPtr handle, ref SendInviteOptionsInternal options, IntPtr clientData, OnSendInviteCallbackInternal completionDelegate);

		// Token: 0x06004673 RID: 18035
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_RemoveNotifyLobbyMemberStatusReceived(IntPtr handle, ulong inId);

		// Token: 0x06004674 RID: 18036
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Lobby_AddNotifyLobbyMemberStatusReceived(IntPtr handle, ref AddNotifyLobbyMemberStatusReceivedOptionsInternal options, IntPtr clientData, OnLobbyMemberStatusReceivedCallbackInternal notificationFn);

		// Token: 0x06004675 RID: 18037
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_RemoveNotifyLobbyMemberUpdateReceived(IntPtr handle, ulong inId);

		// Token: 0x06004676 RID: 18038
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Lobby_AddNotifyLobbyMemberUpdateReceived(IntPtr handle, ref AddNotifyLobbyMemberUpdateReceivedOptionsInternal options, IntPtr clientData, OnLobbyMemberUpdateReceivedCallbackInternal notificationFn);

		// Token: 0x06004677 RID: 18039
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_RemoveNotifyLobbyUpdateReceived(IntPtr handle, ulong inId);

		// Token: 0x06004678 RID: 18040
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Lobby_AddNotifyLobbyUpdateReceived(IntPtr handle, ref AddNotifyLobbyUpdateReceivedOptionsInternal options, IntPtr clientData, OnLobbyUpdateReceivedCallbackInternal notificationFn);

		// Token: 0x06004679 RID: 18041
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_KickMember(IntPtr handle, ref KickMemberOptionsInternal options, IntPtr clientData, OnKickMemberCallbackInternal completionDelegate);

		// Token: 0x0600467A RID: 18042
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_PromoteMember(IntPtr handle, ref PromoteMemberOptionsInternal options, IntPtr clientData, OnPromoteMemberCallbackInternal completionDelegate);

		// Token: 0x0600467B RID: 18043
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_UpdateLobby(IntPtr handle, ref UpdateLobbyOptionsInternal options, IntPtr clientData, OnUpdateLobbyCallbackInternal completionDelegate);

		// Token: 0x0600467C RID: 18044
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Lobby_UpdateLobbyModification(IntPtr handle, ref UpdateLobbyModificationOptionsInternal options, ref IntPtr outLobbyModificationHandle);

		// Token: 0x0600467D RID: 18045
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_LeaveLobby(IntPtr handle, ref LeaveLobbyOptionsInternal options, IntPtr clientData, OnLeaveLobbyCallbackInternal completionDelegate);

		// Token: 0x0600467E RID: 18046
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_JoinLobby(IntPtr handle, ref JoinLobbyOptionsInternal options, IntPtr clientData, OnJoinLobbyCallbackInternal completionDelegate);

		// Token: 0x0600467F RID: 18047
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_DestroyLobby(IntPtr handle, ref DestroyLobbyOptionsInternal options, IntPtr clientData, OnDestroyLobbyCallbackInternal completionDelegate);

		// Token: 0x06004680 RID: 18048
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_CreateLobby(IntPtr handle, ref CreateLobbyOptionsInternal options, IntPtr clientData, OnCreateLobbyCallbackInternal completionDelegate);

		// Token: 0x04001B1D RID: 6941
		public const int LobbysearchCopysearchresultbyindexApiLatest = 1;

		// Token: 0x04001B1E RID: 6942
		public const int LobbysearchGetsearchresultcountApiLatest = 1;

		// Token: 0x04001B1F RID: 6943
		public const int LobbysearchSetmaxresultsApiLatest = 1;

		// Token: 0x04001B20 RID: 6944
		public const int LobbysearchRemoveparameterApiLatest = 1;

		// Token: 0x04001B21 RID: 6945
		public const int LobbysearchSetparameterApiLatest = 1;

		// Token: 0x04001B22 RID: 6946
		public const int LobbysearchSettargetuseridApiLatest = 1;

		// Token: 0x04001B23 RID: 6947
		public const int LobbysearchSetlobbyidApiLatest = 1;

		// Token: 0x04001B24 RID: 6948
		public const int LobbysearchFindApiLatest = 1;

		// Token: 0x04001B25 RID: 6949
		public const int LobbydetailsGetmemberbyindexApiLatest = 1;

		// Token: 0x04001B26 RID: 6950
		public const int LobbydetailsGetmembercountApiLatest = 1;

		// Token: 0x04001B27 RID: 6951
		public const int LobbydetailsCopymemberattributebykeyApiLatest = 1;

		// Token: 0x04001B28 RID: 6952
		public const int LobbydetailsCopymemberattributebyindexApiLatest = 1;

		// Token: 0x04001B29 RID: 6953
		public const int LobbydetailsGetmemberattributecountApiLatest = 1;

		// Token: 0x04001B2A RID: 6954
		public const int LobbydetailsCopyattributebykeyApiLatest = 1;

		// Token: 0x04001B2B RID: 6955
		public const int LobbydetailsCopyattributebyindexApiLatest = 1;

		// Token: 0x04001B2C RID: 6956
		public const int LobbydetailsGetattributecountApiLatest = 1;

		// Token: 0x04001B2D RID: 6957
		public const int LobbydetailsCopyinfoApiLatest = 1;

		// Token: 0x04001B2E RID: 6958
		public const int LobbydetailsGetlobbyownerApiLatest = 1;

		// Token: 0x04001B2F RID: 6959
		public const int LobbymodificationRemovememberattributeApiLatest = 1;

		// Token: 0x04001B30 RID: 6960
		public const int LobbymodificationAddmemberattributeApiLatest = 1;

		// Token: 0x04001B31 RID: 6961
		public const int LobbymodificationRemoveattributeApiLatest = 1;

		// Token: 0x04001B32 RID: 6962
		public const int LobbymodificationAddattributeApiLatest = 1;

		// Token: 0x04001B33 RID: 6963
		public const int LobbymodificationSetmaxmembersApiLatest = 1;

		// Token: 0x04001B34 RID: 6964
		public const int LobbymodificationSetpermissionlevelApiLatest = 1;

		// Token: 0x04001B35 RID: 6965
		public const int AttributeApiLatest = 1;

		// Token: 0x04001B36 RID: 6966
		public const int AttributedataApiLatest = 1;

		// Token: 0x04001B37 RID: 6967
		public const string SearchMinslotsavailable = "minslotsavailable";

		// Token: 0x04001B38 RID: 6968
		public const string SearchMincurrentmembers = "mincurrentmembers";

		// Token: 0x04001B39 RID: 6969
		public const int CopylobbydetailshandleApiLatest = 1;

		// Token: 0x04001B3A RID: 6970
		public const int GetinviteidbyindexApiLatest = 1;

		// Token: 0x04001B3B RID: 6971
		public const int GetinvitecountApiLatest = 1;

		// Token: 0x04001B3C RID: 6972
		public const int QueryinvitesApiLatest = 1;

		// Token: 0x04001B3D RID: 6973
		public const int RejectinviteApiLatest = 1;

		// Token: 0x04001B3E RID: 6974
		public const int SendinviteApiLatest = 1;

		// Token: 0x04001B3F RID: 6975
		public const int CreatelobbysearchApiLatest = 1;

		// Token: 0x04001B40 RID: 6976
		public const int CopylobbydetailshandlebyuieventidApiLatest = 1;

		// Token: 0x04001B41 RID: 6977
		public const int CopylobbydetailshandlebyinviteidApiLatest = 1;

		// Token: 0x04001B42 RID: 6978
		public const int AddnotifyjoinlobbyacceptedApiLatest = 1;

		// Token: 0x04001B43 RID: 6979
		public const int AddnotifylobbyinviteacceptedApiLatest = 1;

		// Token: 0x04001B44 RID: 6980
		public const int AddnotifylobbyinvitereceivedApiLatest = 1;

		// Token: 0x04001B45 RID: 6981
		public const int InviteidMaxLength = 64;

		// Token: 0x04001B46 RID: 6982
		public const int AddnotifylobbymemberstatusreceivedApiLatest = 1;

		// Token: 0x04001B47 RID: 6983
		public const int AddnotifylobbymemberupdatereceivedApiLatest = 1;

		// Token: 0x04001B48 RID: 6984
		public const int AddnotifylobbyupdatereceivedApiLatest = 1;

		// Token: 0x04001B49 RID: 6985
		public const int KickmemberApiLatest = 1;

		// Token: 0x04001B4A RID: 6986
		public const int PromotememberApiLatest = 1;

		// Token: 0x04001B4B RID: 6987
		public const int UpdatelobbyApiLatest = 1;

		// Token: 0x04001B4C RID: 6988
		public const int UpdatelobbymodificationApiLatest = 1;

		// Token: 0x04001B4D RID: 6989
		public const int LeavelobbyApiLatest = 1;

		// Token: 0x04001B4E RID: 6990
		public const int JoinlobbyApiLatest = 2;

		// Token: 0x04001B4F RID: 6991
		public const int DestroylobbyApiLatest = 1;

		// Token: 0x04001B50 RID: 6992
		public const int CreatelobbyApiLatest = 2;

		// Token: 0x04001B51 RID: 6993
		public const int LobbydetailsInfoApiLatest = 1;

		// Token: 0x04001B52 RID: 6994
		public const int LobbymodificationMaxAttributeLength = 64;

		// Token: 0x04001B53 RID: 6995
		public const int LobbymodificationMaxAttributes = 64;

		// Token: 0x04001B54 RID: 6996
		public const int MaxSearchResults = 200;

		// Token: 0x04001B55 RID: 6997
		public const int MaxLobbyMembers = 64;

		// Token: 0x04001B56 RID: 6998
		public const int MaxLobbies = 4;
	}
}
