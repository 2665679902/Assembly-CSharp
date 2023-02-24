using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000655 RID: 1621
	public sealed class SessionsInterface : Handle
	{
		// Token: 0x06003F21 RID: 16161 RVA: 0x00086CC7 File Offset: 0x00084EC7
		public SessionsInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x00086CD0 File Offset: 0x00084ED0
		public Result CreateSessionModification(CreateSessionModificationOptions options, out SessionModification outSessionModificationHandle)
		{
			CreateSessionModificationOptionsInternal createSessionModificationOptionsInternal = Helper.CopyProperties<CreateSessionModificationOptionsInternal>(options);
			outSessionModificationHandle = Helper.GetDefault<SessionModification>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionsInterface.EOS_Sessions_CreateSessionModification(base.InnerHandle, ref createSessionModificationOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CreateSessionModificationOptionsInternal>(ref createSessionModificationOptionsInternal);
			Helper.TryMarshalGet<SessionModification>(zero, out outSessionModificationHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x00086D20 File Offset: 0x00084F20
		public Result UpdateSessionModification(UpdateSessionModificationOptions options, out SessionModification outSessionModificationHandle)
		{
			UpdateSessionModificationOptionsInternal updateSessionModificationOptionsInternal = Helper.CopyProperties<UpdateSessionModificationOptionsInternal>(options);
			outSessionModificationHandle = Helper.GetDefault<SessionModification>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionsInterface.EOS_Sessions_UpdateSessionModification(base.InnerHandle, ref updateSessionModificationOptionsInternal, ref zero);
			Helper.TryMarshalDispose<UpdateSessionModificationOptionsInternal>(ref updateSessionModificationOptionsInternal);
			Helper.TryMarshalGet<SessionModification>(zero, out outSessionModificationHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x00086D70 File Offset: 0x00084F70
		public void UpdateSession(UpdateSessionOptions options, object clientData, OnUpdateSessionCallback completionDelegate)
		{
			UpdateSessionOptionsInternal updateSessionOptionsInternal = Helper.CopyProperties<UpdateSessionOptionsInternal>(options);
			OnUpdateSessionCallbackInternal onUpdateSessionCallbackInternal = new OnUpdateSessionCallbackInternal(SessionsInterface.OnUpdateSession);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onUpdateSessionCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_UpdateSession(base.InnerHandle, ref updateSessionOptionsInternal, zero, onUpdateSessionCallbackInternal);
			Helper.TryMarshalDispose<UpdateSessionOptionsInternal>(ref updateSessionOptionsInternal);
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x00086DC0 File Offset: 0x00084FC0
		public void DestroySession(DestroySessionOptions options, object clientData, OnDestroySessionCallback completionDelegate)
		{
			DestroySessionOptionsInternal destroySessionOptionsInternal = Helper.CopyProperties<DestroySessionOptionsInternal>(options);
			OnDestroySessionCallbackInternal onDestroySessionCallbackInternal = new OnDestroySessionCallbackInternal(SessionsInterface.OnDestroySession);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onDestroySessionCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_DestroySession(base.InnerHandle, ref destroySessionOptionsInternal, zero, onDestroySessionCallbackInternal);
			Helper.TryMarshalDispose<DestroySessionOptionsInternal>(ref destroySessionOptionsInternal);
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x00086E10 File Offset: 0x00085010
		public void JoinSession(JoinSessionOptions options, object clientData, OnJoinSessionCallback completionDelegate)
		{
			JoinSessionOptionsInternal joinSessionOptionsInternal = Helper.CopyProperties<JoinSessionOptionsInternal>(options);
			OnJoinSessionCallbackInternal onJoinSessionCallbackInternal = new OnJoinSessionCallbackInternal(SessionsInterface.OnJoinSession);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onJoinSessionCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_JoinSession(base.InnerHandle, ref joinSessionOptionsInternal, zero, onJoinSessionCallbackInternal);
			Helper.TryMarshalDispose<JoinSessionOptionsInternal>(ref joinSessionOptionsInternal);
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x00086E60 File Offset: 0x00085060
		public void StartSession(StartSessionOptions options, object clientData, OnStartSessionCallback completionDelegate)
		{
			StartSessionOptionsInternal startSessionOptionsInternal = Helper.CopyProperties<StartSessionOptionsInternal>(options);
			OnStartSessionCallbackInternal onStartSessionCallbackInternal = new OnStartSessionCallbackInternal(SessionsInterface.OnStartSession);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onStartSessionCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_StartSession(base.InnerHandle, ref startSessionOptionsInternal, zero, onStartSessionCallbackInternal);
			Helper.TryMarshalDispose<StartSessionOptionsInternal>(ref startSessionOptionsInternal);
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x00086EB0 File Offset: 0x000850B0
		public void EndSession(EndSessionOptions options, object clientData, OnEndSessionCallback completionDelegate)
		{
			EndSessionOptionsInternal endSessionOptionsInternal = Helper.CopyProperties<EndSessionOptionsInternal>(options);
			OnEndSessionCallbackInternal onEndSessionCallbackInternal = new OnEndSessionCallbackInternal(SessionsInterface.OnEndSession);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onEndSessionCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_EndSession(base.InnerHandle, ref endSessionOptionsInternal, zero, onEndSessionCallbackInternal);
			Helper.TryMarshalDispose<EndSessionOptionsInternal>(ref endSessionOptionsInternal);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x00086F00 File Offset: 0x00085100
		public void RegisterPlayers(RegisterPlayersOptions options, object clientData, OnRegisterPlayersCallback completionDelegate)
		{
			RegisterPlayersOptionsInternal registerPlayersOptionsInternal = Helper.CopyProperties<RegisterPlayersOptionsInternal>(options);
			OnRegisterPlayersCallbackInternal onRegisterPlayersCallbackInternal = new OnRegisterPlayersCallbackInternal(SessionsInterface.OnRegisterPlayers);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onRegisterPlayersCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_RegisterPlayers(base.InnerHandle, ref registerPlayersOptionsInternal, zero, onRegisterPlayersCallbackInternal);
			Helper.TryMarshalDispose<RegisterPlayersOptionsInternal>(ref registerPlayersOptionsInternal);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x00086F50 File Offset: 0x00085150
		public void UnregisterPlayers(UnregisterPlayersOptions options, object clientData, OnUnregisterPlayersCallback completionDelegate)
		{
			UnregisterPlayersOptionsInternal unregisterPlayersOptionsInternal = Helper.CopyProperties<UnregisterPlayersOptionsInternal>(options);
			OnUnregisterPlayersCallbackInternal onUnregisterPlayersCallbackInternal = new OnUnregisterPlayersCallbackInternal(SessionsInterface.OnUnregisterPlayers);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onUnregisterPlayersCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_UnregisterPlayers(base.InnerHandle, ref unregisterPlayersOptionsInternal, zero, onUnregisterPlayersCallbackInternal);
			Helper.TryMarshalDispose<UnregisterPlayersOptionsInternal>(ref unregisterPlayersOptionsInternal);
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x00086FA0 File Offset: 0x000851A0
		public void SendInvite(SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
		{
			SendInviteOptionsInternal sendInviteOptionsInternal = Helper.CopyProperties<SendInviteOptionsInternal>(options);
			OnSendInviteCallbackInternal onSendInviteCallbackInternal = new OnSendInviteCallbackInternal(SessionsInterface.OnSendInvite);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onSendInviteCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_SendInvite(base.InnerHandle, ref sendInviteOptionsInternal, zero, onSendInviteCallbackInternal);
			Helper.TryMarshalDispose<SendInviteOptionsInternal>(ref sendInviteOptionsInternal);
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x00086FF0 File Offset: 0x000851F0
		public void RejectInvite(RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
		{
			RejectInviteOptionsInternal rejectInviteOptionsInternal = Helper.CopyProperties<RejectInviteOptionsInternal>(options);
			OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = new OnRejectInviteCallbackInternal(SessionsInterface.OnRejectInvite);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onRejectInviteCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_RejectInvite(base.InnerHandle, ref rejectInviteOptionsInternal, zero, onRejectInviteCallbackInternal);
			Helper.TryMarshalDispose<RejectInviteOptionsInternal>(ref rejectInviteOptionsInternal);
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x00087040 File Offset: 0x00085240
		public void QueryInvites(QueryInvitesOptions options, object clientData, OnQueryInvitesCallback completionDelegate)
		{
			QueryInvitesOptionsInternal queryInvitesOptionsInternal = Helper.CopyProperties<QueryInvitesOptionsInternal>(options);
			OnQueryInvitesCallbackInternal onQueryInvitesCallbackInternal = new OnQueryInvitesCallbackInternal(SessionsInterface.OnQueryInvites);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryInvitesCallbackInternal, Array.Empty<Delegate>());
			SessionsInterface.EOS_Sessions_QueryInvites(base.InnerHandle, ref queryInvitesOptionsInternal, zero, onQueryInvitesCallbackInternal);
			Helper.TryMarshalDispose<QueryInvitesOptionsInternal>(ref queryInvitesOptionsInternal);
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x00087090 File Offset: 0x00085290
		public uint GetInviteCount(GetInviteCountOptions options)
		{
			GetInviteCountOptionsInternal getInviteCountOptionsInternal = Helper.CopyProperties<GetInviteCountOptionsInternal>(options);
			uint num = SessionsInterface.EOS_Sessions_GetInviteCount(base.InnerHandle, ref getInviteCountOptionsInternal);
			Helper.TryMarshalDispose<GetInviteCountOptionsInternal>(ref getInviteCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06003F2F RID: 16175 RVA: 0x000870C8 File Offset: 0x000852C8
		public Result GetInviteIdByIndex(GetInviteIdByIndexOptions options, StringBuilder outBuffer, ref int inOutBufferLength)
		{
			GetInviteIdByIndexOptionsInternal getInviteIdByIndexOptionsInternal = Helper.CopyProperties<GetInviteIdByIndexOptionsInternal>(options);
			Result result = SessionsInterface.EOS_Sessions_GetInviteIdByIndex(base.InnerHandle, ref getInviteIdByIndexOptionsInternal, outBuffer, ref inOutBufferLength);
			Helper.TryMarshalDispose<GetInviteIdByIndexOptionsInternal>(ref getInviteIdByIndexOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x00087104 File Offset: 0x00085304
		public Result CreateSessionSearch(CreateSessionSearchOptions options, out SessionSearch outSessionSearchHandle)
		{
			CreateSessionSearchOptionsInternal createSessionSearchOptionsInternal = Helper.CopyProperties<CreateSessionSearchOptionsInternal>(options);
			outSessionSearchHandle = Helper.GetDefault<SessionSearch>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionsInterface.EOS_Sessions_CreateSessionSearch(base.InnerHandle, ref createSessionSearchOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CreateSessionSearchOptionsInternal>(ref createSessionSearchOptionsInternal);
			Helper.TryMarshalGet<SessionSearch>(zero, out outSessionSearchHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F31 RID: 16177 RVA: 0x00087154 File Offset: 0x00085354
		public Result CopyActiveSessionHandle(CopyActiveSessionHandleOptions options, out ActiveSession outSessionHandle)
		{
			CopyActiveSessionHandleOptionsInternal copyActiveSessionHandleOptionsInternal = Helper.CopyProperties<CopyActiveSessionHandleOptionsInternal>(options);
			outSessionHandle = Helper.GetDefault<ActiveSession>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionsInterface.EOS_Sessions_CopyActiveSessionHandle(base.InnerHandle, ref copyActiveSessionHandleOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyActiveSessionHandleOptionsInternal>(ref copyActiveSessionHandleOptionsInternal);
			Helper.TryMarshalGet<ActiveSession>(zero, out outSessionHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F32 RID: 16178 RVA: 0x000871A4 File Offset: 0x000853A4
		public ulong AddNotifySessionInviteReceived(AddNotifySessionInviteReceivedOptions options, object clientData, OnSessionInviteReceivedCallback notificationFn)
		{
			AddNotifySessionInviteReceivedOptionsInternal addNotifySessionInviteReceivedOptionsInternal = Helper.CopyProperties<AddNotifySessionInviteReceivedOptionsInternal>(options);
			OnSessionInviteReceivedCallbackInternal onSessionInviteReceivedCallbackInternal = new OnSessionInviteReceivedCallbackInternal(SessionsInterface.OnSessionInviteReceived);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onSessionInviteReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = SessionsInterface.EOS_Sessions_AddNotifySessionInviteReceived(base.InnerHandle, ref addNotifySessionInviteReceivedOptionsInternal, zero, onSessionInviteReceivedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifySessionInviteReceivedOptionsInternal>(ref addNotifySessionInviteReceivedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06003F33 RID: 16179 RVA: 0x0008720C File Offset: 0x0008540C
		public void RemoveNotifySessionInviteReceived(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			SessionsInterface.EOS_Sessions_RemoveNotifySessionInviteReceived(base.InnerHandle, inId);
		}

		// Token: 0x06003F34 RID: 16180 RVA: 0x00087224 File Offset: 0x00085424
		public ulong AddNotifySessionInviteAccepted(AddNotifySessionInviteAcceptedOptions options, object clientData, OnSessionInviteAcceptedCallback notificationFn)
		{
			AddNotifySessionInviteAcceptedOptionsInternal addNotifySessionInviteAcceptedOptionsInternal = Helper.CopyProperties<AddNotifySessionInviteAcceptedOptionsInternal>(options);
			OnSessionInviteAcceptedCallbackInternal onSessionInviteAcceptedCallbackInternal = new OnSessionInviteAcceptedCallbackInternal(SessionsInterface.OnSessionInviteAccepted);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onSessionInviteAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = SessionsInterface.EOS_Sessions_AddNotifySessionInviteAccepted(base.InnerHandle, ref addNotifySessionInviteAcceptedOptionsInternal, zero, onSessionInviteAcceptedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifySessionInviteAcceptedOptionsInternal>(ref addNotifySessionInviteAcceptedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06003F35 RID: 16181 RVA: 0x0008728C File Offset: 0x0008548C
		public void RemoveNotifySessionInviteAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			SessionsInterface.EOS_Sessions_RemoveNotifySessionInviteAccepted(base.InnerHandle, inId);
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x000872A4 File Offset: 0x000854A4
		public ulong AddNotifyJoinSessionAccepted(AddNotifyJoinSessionAcceptedOptions options, object clientData, OnJoinSessionAcceptedCallback notificationFn)
		{
			AddNotifyJoinSessionAcceptedOptionsInternal addNotifyJoinSessionAcceptedOptionsInternal = Helper.CopyProperties<AddNotifyJoinSessionAcceptedOptionsInternal>(options);
			OnJoinSessionAcceptedCallbackInternal onJoinSessionAcceptedCallbackInternal = new OnJoinSessionAcceptedCallbackInternal(SessionsInterface.OnJoinSessionAccepted);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onJoinSessionAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = SessionsInterface.EOS_Sessions_AddNotifyJoinSessionAccepted(base.InnerHandle, ref addNotifyJoinSessionAcceptedOptionsInternal, zero, onJoinSessionAcceptedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyJoinSessionAcceptedOptionsInternal>(ref addNotifyJoinSessionAcceptedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06003F37 RID: 16183 RVA: 0x0008730C File Offset: 0x0008550C
		public void RemoveNotifyJoinSessionAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			SessionsInterface.EOS_Sessions_RemoveNotifyJoinSessionAccepted(base.InnerHandle, inId);
		}

		// Token: 0x06003F38 RID: 16184 RVA: 0x00087324 File Offset: 0x00085524
		public Result CopySessionHandleByInviteId(CopySessionHandleByInviteIdOptions options, out SessionDetails outSessionHandle)
		{
			CopySessionHandleByInviteIdOptionsInternal copySessionHandleByInviteIdOptionsInternal = Helper.CopyProperties<CopySessionHandleByInviteIdOptionsInternal>(options);
			outSessionHandle = Helper.GetDefault<SessionDetails>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionsInterface.EOS_Sessions_CopySessionHandleByInviteId(base.InnerHandle, ref copySessionHandleByInviteIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopySessionHandleByInviteIdOptionsInternal>(ref copySessionHandleByInviteIdOptionsInternal);
			Helper.TryMarshalGet<SessionDetails>(zero, out outSessionHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F39 RID: 16185 RVA: 0x00087374 File Offset: 0x00085574
		public Result CopySessionHandleByUiEventId(CopySessionHandleByUiEventIdOptions options, out SessionDetails outSessionHandle)
		{
			CopySessionHandleByUiEventIdOptionsInternal copySessionHandleByUiEventIdOptionsInternal = Helper.CopyProperties<CopySessionHandleByUiEventIdOptionsInternal>(options);
			outSessionHandle = Helper.GetDefault<SessionDetails>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionsInterface.EOS_Sessions_CopySessionHandleByUiEventId(base.InnerHandle, ref copySessionHandleByUiEventIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopySessionHandleByUiEventIdOptionsInternal>(ref copySessionHandleByUiEventIdOptionsInternal);
			Helper.TryMarshalGet<SessionDetails>(zero, out outSessionHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x000873C4 File Offset: 0x000855C4
		public Result CopySessionHandleForPresence(CopySessionHandleForPresenceOptions options, out SessionDetails outSessionHandle)
		{
			CopySessionHandleForPresenceOptionsInternal copySessionHandleForPresenceOptionsInternal = Helper.CopyProperties<CopySessionHandleForPresenceOptionsInternal>(options);
			outSessionHandle = Helper.GetDefault<SessionDetails>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionsInterface.EOS_Sessions_CopySessionHandleForPresence(base.InnerHandle, ref copySessionHandleForPresenceOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopySessionHandleForPresenceOptionsInternal>(ref copySessionHandleForPresenceOptionsInternal);
			Helper.TryMarshalGet<SessionDetails>(zero, out outSessionHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x00087414 File Offset: 0x00085614
		public Result IsUserInSession(IsUserInSessionOptions options)
		{
			IsUserInSessionOptionsInternal isUserInSessionOptionsInternal = Helper.CopyProperties<IsUserInSessionOptionsInternal>(options);
			Result result = SessionsInterface.EOS_Sessions_IsUserInSession(base.InnerHandle, ref isUserInSessionOptionsInternal);
			Helper.TryMarshalDispose<IsUserInSessionOptionsInternal>(ref isUserInSessionOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F3C RID: 16188 RVA: 0x0008744C File Offset: 0x0008564C
		public Result DumpSessionState(DumpSessionStateOptions options)
		{
			DumpSessionStateOptionsInternal dumpSessionStateOptionsInternal = Helper.CopyProperties<DumpSessionStateOptionsInternal>(options);
			Result result = SessionsInterface.EOS_Sessions_DumpSessionState(base.InnerHandle, ref dumpSessionStateOptionsInternal);
			Helper.TryMarshalDispose<DumpSessionStateOptionsInternal>(ref dumpSessionStateOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x00087484 File Offset: 0x00085684
		[MonoPInvokeCallback]
		internal static void OnJoinSessionAccepted(IntPtr address)
		{
			OnJoinSessionAcceptedCallback onJoinSessionAcceptedCallback = null;
			JoinSessionAcceptedCallbackInfo joinSessionAcceptedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnJoinSessionAcceptedCallback, JoinSessionAcceptedCallbackInfoInternal, JoinSessionAcceptedCallbackInfo>(address, out onJoinSessionAcceptedCallback, out joinSessionAcceptedCallbackInfo))
			{
				onJoinSessionAcceptedCallback(joinSessionAcceptedCallbackInfo);
			}
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x000874A8 File Offset: 0x000856A8
		[MonoPInvokeCallback]
		internal static void OnSessionInviteAccepted(IntPtr address)
		{
			OnSessionInviteAcceptedCallback onSessionInviteAcceptedCallback = null;
			SessionInviteAcceptedCallbackInfo sessionInviteAcceptedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnSessionInviteAcceptedCallback, SessionInviteAcceptedCallbackInfoInternal, SessionInviteAcceptedCallbackInfo>(address, out onSessionInviteAcceptedCallback, out sessionInviteAcceptedCallbackInfo))
			{
				onSessionInviteAcceptedCallback(sessionInviteAcceptedCallbackInfo);
			}
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x000874CC File Offset: 0x000856CC
		[MonoPInvokeCallback]
		internal static void OnSessionInviteReceived(IntPtr address)
		{
			OnSessionInviteReceivedCallback onSessionInviteReceivedCallback = null;
			SessionInviteReceivedCallbackInfo sessionInviteReceivedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnSessionInviteReceivedCallback, SessionInviteReceivedCallbackInfoInternal, SessionInviteReceivedCallbackInfo>(address, out onSessionInviteReceivedCallback, out sessionInviteReceivedCallbackInfo))
			{
				onSessionInviteReceivedCallback(sessionInviteReceivedCallbackInfo);
			}
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x000874F0 File Offset: 0x000856F0
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

		// Token: 0x06003F41 RID: 16193 RVA: 0x00087514 File Offset: 0x00085714
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

		// Token: 0x06003F42 RID: 16194 RVA: 0x00087538 File Offset: 0x00085738
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

		// Token: 0x06003F43 RID: 16195 RVA: 0x0008755C File Offset: 0x0008575C
		[MonoPInvokeCallback]
		internal static void OnUnregisterPlayers(IntPtr address)
		{
			OnUnregisterPlayersCallback onUnregisterPlayersCallback = null;
			UnregisterPlayersCallbackInfo unregisterPlayersCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnUnregisterPlayersCallback, UnregisterPlayersCallbackInfoInternal, UnregisterPlayersCallbackInfo>(address, out onUnregisterPlayersCallback, out unregisterPlayersCallbackInfo))
			{
				onUnregisterPlayersCallback(unregisterPlayersCallbackInfo);
			}
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x00087580 File Offset: 0x00085780
		[MonoPInvokeCallback]
		internal static void OnRegisterPlayers(IntPtr address)
		{
			OnRegisterPlayersCallback onRegisterPlayersCallback = null;
			RegisterPlayersCallbackInfo registerPlayersCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnRegisterPlayersCallback, RegisterPlayersCallbackInfoInternal, RegisterPlayersCallbackInfo>(address, out onRegisterPlayersCallback, out registerPlayersCallbackInfo))
			{
				onRegisterPlayersCallback(registerPlayersCallbackInfo);
			}
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x000875A4 File Offset: 0x000857A4
		[MonoPInvokeCallback]
		internal static void OnEndSession(IntPtr address)
		{
			OnEndSessionCallback onEndSessionCallback = null;
			EndSessionCallbackInfo endSessionCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnEndSessionCallback, EndSessionCallbackInfoInternal, EndSessionCallbackInfo>(address, out onEndSessionCallback, out endSessionCallbackInfo))
			{
				onEndSessionCallback(endSessionCallbackInfo);
			}
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x000875C8 File Offset: 0x000857C8
		[MonoPInvokeCallback]
		internal static void OnStartSession(IntPtr address)
		{
			OnStartSessionCallback onStartSessionCallback = null;
			StartSessionCallbackInfo startSessionCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnStartSessionCallback, StartSessionCallbackInfoInternal, StartSessionCallbackInfo>(address, out onStartSessionCallback, out startSessionCallbackInfo))
			{
				onStartSessionCallback(startSessionCallbackInfo);
			}
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000875EC File Offset: 0x000857EC
		[MonoPInvokeCallback]
		internal static void OnJoinSession(IntPtr address)
		{
			OnJoinSessionCallback onJoinSessionCallback = null;
			JoinSessionCallbackInfo joinSessionCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnJoinSessionCallback, JoinSessionCallbackInfoInternal, JoinSessionCallbackInfo>(address, out onJoinSessionCallback, out joinSessionCallbackInfo))
			{
				onJoinSessionCallback(joinSessionCallbackInfo);
			}
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x00087610 File Offset: 0x00085810
		[MonoPInvokeCallback]
		internal static void OnDestroySession(IntPtr address)
		{
			OnDestroySessionCallback onDestroySessionCallback = null;
			DestroySessionCallbackInfo destroySessionCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnDestroySessionCallback, DestroySessionCallbackInfoInternal, DestroySessionCallbackInfo>(address, out onDestroySessionCallback, out destroySessionCallbackInfo))
			{
				onDestroySessionCallback(destroySessionCallbackInfo);
			}
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x00087634 File Offset: 0x00085834
		[MonoPInvokeCallback]
		internal static void OnUpdateSession(IntPtr address)
		{
			OnUpdateSessionCallback onUpdateSessionCallback = null;
			UpdateSessionCallbackInfo updateSessionCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnUpdateSessionCallback, UpdateSessionCallbackInfoInternal, UpdateSessionCallbackInfo>(address, out onUpdateSessionCallback, out updateSessionCallbackInfo))
			{
				onUpdateSessionCallback(updateSessionCallbackInfo);
			}
		}

		// Token: 0x06003F4A RID: 16202
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_DumpSessionState(IntPtr handle, ref DumpSessionStateOptionsInternal options);

		// Token: 0x06003F4B RID: 16203
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_IsUserInSession(IntPtr handle, ref IsUserInSessionOptionsInternal options);

		// Token: 0x06003F4C RID: 16204
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_CopySessionHandleForPresence(IntPtr handle, ref CopySessionHandleForPresenceOptionsInternal options, ref IntPtr outSessionHandle);

		// Token: 0x06003F4D RID: 16205
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_CopySessionHandleByUiEventId(IntPtr handle, ref CopySessionHandleByUiEventIdOptionsInternal options, ref IntPtr outSessionHandle);

		// Token: 0x06003F4E RID: 16206
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_CopySessionHandleByInviteId(IntPtr handle, ref CopySessionHandleByInviteIdOptionsInternal options, ref IntPtr outSessionHandle);

		// Token: 0x06003F4F RID: 16207
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_RemoveNotifyJoinSessionAccepted(IntPtr handle, ulong inId);

		// Token: 0x06003F50 RID: 16208
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Sessions_AddNotifyJoinSessionAccepted(IntPtr handle, ref AddNotifyJoinSessionAcceptedOptionsInternal options, IntPtr clientData, OnJoinSessionAcceptedCallbackInternal notificationFn);

		// Token: 0x06003F51 RID: 16209
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_RemoveNotifySessionInviteAccepted(IntPtr handle, ulong inId);

		// Token: 0x06003F52 RID: 16210
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Sessions_AddNotifySessionInviteAccepted(IntPtr handle, ref AddNotifySessionInviteAcceptedOptionsInternal options, IntPtr clientData, OnSessionInviteAcceptedCallbackInternal notificationFn);

		// Token: 0x06003F53 RID: 16211
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_RemoveNotifySessionInviteReceived(IntPtr handle, ulong inId);

		// Token: 0x06003F54 RID: 16212
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Sessions_AddNotifySessionInviteReceived(IntPtr handle, ref AddNotifySessionInviteReceivedOptionsInternal options, IntPtr clientData, OnSessionInviteReceivedCallbackInternal notificationFn);

		// Token: 0x06003F55 RID: 16213
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_CopyActiveSessionHandle(IntPtr handle, ref CopyActiveSessionHandleOptionsInternal options, ref IntPtr outSessionHandle);

		// Token: 0x06003F56 RID: 16214
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_CreateSessionSearch(IntPtr handle, ref CreateSessionSearchOptionsInternal options, ref IntPtr outSessionSearchHandle);

		// Token: 0x06003F57 RID: 16215
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_GetInviteIdByIndex(IntPtr handle, ref GetInviteIdByIndexOptionsInternal options, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x06003F58 RID: 16216
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Sessions_GetInviteCount(IntPtr handle, ref GetInviteCountOptionsInternal options);

		// Token: 0x06003F59 RID: 16217
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_QueryInvites(IntPtr handle, ref QueryInvitesOptionsInternal options, IntPtr clientData, OnQueryInvitesCallbackInternal completionDelegate);

		// Token: 0x06003F5A RID: 16218
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_RejectInvite(IntPtr handle, ref RejectInviteOptionsInternal options, IntPtr clientData, OnRejectInviteCallbackInternal completionDelegate);

		// Token: 0x06003F5B RID: 16219
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_SendInvite(IntPtr handle, ref SendInviteOptionsInternal options, IntPtr clientData, OnSendInviteCallbackInternal completionDelegate);

		// Token: 0x06003F5C RID: 16220
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_UnregisterPlayers(IntPtr handle, ref UnregisterPlayersOptionsInternal options, IntPtr clientData, OnUnregisterPlayersCallbackInternal completionDelegate);

		// Token: 0x06003F5D RID: 16221
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_RegisterPlayers(IntPtr handle, ref RegisterPlayersOptionsInternal options, IntPtr clientData, OnRegisterPlayersCallbackInternal completionDelegate);

		// Token: 0x06003F5E RID: 16222
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_EndSession(IntPtr handle, ref EndSessionOptionsInternal options, IntPtr clientData, OnEndSessionCallbackInternal completionDelegate);

		// Token: 0x06003F5F RID: 16223
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_StartSession(IntPtr handle, ref StartSessionOptionsInternal options, IntPtr clientData, OnStartSessionCallbackInternal completionDelegate);

		// Token: 0x06003F60 RID: 16224
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_JoinSession(IntPtr handle, ref JoinSessionOptionsInternal options, IntPtr clientData, OnJoinSessionCallbackInternal completionDelegate);

		// Token: 0x06003F61 RID: 16225
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_DestroySession(IntPtr handle, ref DestroySessionOptionsInternal options, IntPtr clientData, OnDestroySessionCallbackInternal completionDelegate);

		// Token: 0x06003F62 RID: 16226
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Sessions_UpdateSession(IntPtr handle, ref UpdateSessionOptionsInternal options, IntPtr clientData, OnUpdateSessionCallbackInternal completionDelegate);

		// Token: 0x06003F63 RID: 16227
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_UpdateSessionModification(IntPtr handle, ref UpdateSessionModificationOptionsInternal options, ref IntPtr outSessionModificationHandle);

		// Token: 0x06003F64 RID: 16228
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Sessions_CreateSessionModification(IntPtr handle, ref CreateSessionModificationOptionsInternal options, ref IntPtr outSessionModificationHandle);

		// Token: 0x040017EF RID: 6127
		public const int DumpsessionstateApiLatest = 1;

		// Token: 0x040017F0 RID: 6128
		public const int IsuserinsessionApiLatest = 1;

		// Token: 0x040017F1 RID: 6129
		public const int CopysessionhandleforpresenceApiLatest = 1;

		// Token: 0x040017F2 RID: 6130
		public const int CopysessionhandlebyuieventidApiLatest = 1;

		// Token: 0x040017F3 RID: 6131
		public const int CopysessionhandlebyinviteidApiLatest = 1;

		// Token: 0x040017F4 RID: 6132
		public const int AddnotifyjoinsessionacceptedApiLatest = 1;

		// Token: 0x040017F5 RID: 6133
		public const int AddnotifysessioninviteacceptedApiLatest = 1;

		// Token: 0x040017F6 RID: 6134
		public const int AddnotifysessioninvitereceivedApiLatest = 1;

		// Token: 0x040017F7 RID: 6135
		public const int CopyactivesessionhandleApiLatest = 1;

		// Token: 0x040017F8 RID: 6136
		public const int ActivesessionInfoApiLatest = 1;

		// Token: 0x040017F9 RID: 6137
		public const int SessiondetailsCopysessionattributebykeyApiLatest = 1;

		// Token: 0x040017FA RID: 6138
		public const int SessiondetailsCopysessionattributebyindexApiLatest = 1;

		// Token: 0x040017FB RID: 6139
		public const int SessiondetailsGetsessionattributecountApiLatest = 1;

		// Token: 0x040017FC RID: 6140
		public const int SessiondetailsCopyinfoApiLatest = 1;

		// Token: 0x040017FD RID: 6141
		public const int SessiondetailsInfoApiLatest = 1;

		// Token: 0x040017FE RID: 6142
		public const int SessiondetailsSettingsApiLatest = 2;

		// Token: 0x040017FF RID: 6143
		public const int SessionsearchRemoveparameterApiLatest = 1;

		// Token: 0x04001800 RID: 6144
		public const int SessionsearchSetparameterApiLatest = 1;

		// Token: 0x04001801 RID: 6145
		public const int SessionsearchSettargetuseridApiLatest = 1;

		// Token: 0x04001802 RID: 6146
		public const int SessionsearchSetsessionidApiLatest = 1;

		// Token: 0x04001803 RID: 6147
		public const int SessionsearchCopysearchresultbyindexApiLatest = 1;

		// Token: 0x04001804 RID: 6148
		public const int SessionsearchGetsearchresultcountApiLatest = 1;

		// Token: 0x04001805 RID: 6149
		public const int SessionsearchFindApiLatest = 2;

		// Token: 0x04001806 RID: 6150
		public const int SessionsearchSetmaxsearchresultsApiLatest = 1;

		// Token: 0x04001807 RID: 6151
		public const int MaxSearchResults = 200;

		// Token: 0x04001808 RID: 6152
		public const int SessionmodificationRemoveattributeApiLatest = 1;

		// Token: 0x04001809 RID: 6153
		public const int SessionmodificationAddattributeApiLatest = 1;

		// Token: 0x0400180A RID: 6154
		public const int SessionattributeApiLatest = 1;

		// Token: 0x0400180B RID: 6155
		public const int SessiondetailsAttributeApiLatest = 1;

		// Token: 0x0400180C RID: 6156
		public const int ActivesessionGetregisteredplayerbyindexApiLatest = 1;

		// Token: 0x0400180D RID: 6157
		public const int ActivesessionGetregisteredplayercountApiLatest = 1;

		// Token: 0x0400180E RID: 6158
		public const int ActivesessionCopyinfoApiLatest = 1;

		// Token: 0x0400180F RID: 6159
		public const int SessionattributedataApiLatest = 1;

		// Token: 0x04001810 RID: 6160
		public const int AttributedataApiLatest = 1;

		// Token: 0x04001811 RID: 6161
		public const string SearchMinslotsavailable = "minslotsavailable";

		// Token: 0x04001812 RID: 6162
		public const string SearchNonemptyServersOnly = "nonemptyonly";

		// Token: 0x04001813 RID: 6163
		public const string SearchEmptyServersOnly = "emptyonly";

		// Token: 0x04001814 RID: 6164
		public const string SearchBucketId = "bucket";

		// Token: 0x04001815 RID: 6165
		public const int SessionmodificationSetinvitesallowedApiLatest = 1;

		// Token: 0x04001816 RID: 6166
		public const int SessionmodificationSetmaxplayersApiLatest = 1;

		// Token: 0x04001817 RID: 6167
		public const int Maxregisteredplayers = 1000;

		// Token: 0x04001818 RID: 6168
		public const int SessionmodificationSetjoininprogressallowedApiLatest = 1;

		// Token: 0x04001819 RID: 6169
		public const int SessionmodificationSetpermissionlevelApiLatest = 1;

		// Token: 0x0400181A RID: 6170
		public const int SessionmodificationSethostaddressApiLatest = 1;

		// Token: 0x0400181B RID: 6171
		public const int SessionmodificationSetbucketidApiLatest = 1;

		// Token: 0x0400181C RID: 6172
		public const int UnregisterplayersApiLatest = 1;

		// Token: 0x0400181D RID: 6173
		public const int RegisterplayersApiLatest = 1;

		// Token: 0x0400181E RID: 6174
		public const int EndsessionApiLatest = 1;

		// Token: 0x0400181F RID: 6175
		public const int StartsessionApiLatest = 1;

		// Token: 0x04001820 RID: 6176
		public const int JoinsessionApiLatest = 2;

		// Token: 0x04001821 RID: 6177
		public const int DestroysessionApiLatest = 1;

		// Token: 0x04001822 RID: 6178
		public const int UpdatesessionApiLatest = 1;

		// Token: 0x04001823 RID: 6179
		public const int CreatesessionsearchApiLatest = 1;

		// Token: 0x04001824 RID: 6180
		public const int GetinviteidbyindexApiLatest = 1;

		// Token: 0x04001825 RID: 6181
		public const int GetinvitecountApiLatest = 1;

		// Token: 0x04001826 RID: 6182
		public const int QueryinvitesApiLatest = 1;

		// Token: 0x04001827 RID: 6183
		public const int RejectinviteApiLatest = 1;

		// Token: 0x04001828 RID: 6184
		public const int SendinviteApiLatest = 1;

		// Token: 0x04001829 RID: 6185
		public const int InviteidMaxLength = 64;

		// Token: 0x0400182A RID: 6186
		public const int UpdatesessionmodificationApiLatest = 1;

		// Token: 0x0400182B RID: 6187
		public const int CreatesessionmodificationApiLatest = 3;

		// Token: 0x0400182C RID: 6188
		public const int SessionmodificationMaxSessionidoverrideLength = 64;

		// Token: 0x0400182D RID: 6189
		public const int SessionmodificationMinSessionidoverrideLength = 16;

		// Token: 0x0400182E RID: 6190
		public const int SessionmodificationMaxSessionAttributeLength = 64;

		// Token: 0x0400182F RID: 6191
		public const int SessionmodificationMaxSessionAttributes = 64;
	}
}
