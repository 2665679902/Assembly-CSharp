using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000804 RID: 2052
	public sealed class FriendsInterface : Handle
	{
		// Token: 0x060049B4 RID: 18868 RVA: 0x00091B3B File Offset: 0x0008FD3B
		public FriendsInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x00091B44 File Offset: 0x0008FD44
		public void QueryFriends(QueryFriendsOptions options, object clientData, OnQueryFriendsCallback completionDelegate)
		{
			QueryFriendsOptionsInternal queryFriendsOptionsInternal = Helper.CopyProperties<QueryFriendsOptionsInternal>(options);
			OnQueryFriendsCallbackInternal onQueryFriendsCallbackInternal = new OnQueryFriendsCallbackInternal(FriendsInterface.OnQueryFriends);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryFriendsCallbackInternal, Array.Empty<Delegate>());
			FriendsInterface.EOS_Friends_QueryFriends(base.InnerHandle, ref queryFriendsOptionsInternal, zero, onQueryFriendsCallbackInternal);
			Helper.TryMarshalDispose<QueryFriendsOptionsInternal>(ref queryFriendsOptionsInternal);
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x00091B94 File Offset: 0x0008FD94
		public void SendInvite(SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
		{
			SendInviteOptionsInternal sendInviteOptionsInternal = Helper.CopyProperties<SendInviteOptionsInternal>(options);
			OnSendInviteCallbackInternal onSendInviteCallbackInternal = new OnSendInviteCallbackInternal(FriendsInterface.OnSendInvite);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onSendInviteCallbackInternal, Array.Empty<Delegate>());
			FriendsInterface.EOS_Friends_SendInvite(base.InnerHandle, ref sendInviteOptionsInternal, zero, onSendInviteCallbackInternal);
			Helper.TryMarshalDispose<SendInviteOptionsInternal>(ref sendInviteOptionsInternal);
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x00091BE4 File Offset: 0x0008FDE4
		public void AcceptInvite(AcceptInviteOptions options, object clientData, OnAcceptInviteCallback completionDelegate)
		{
			AcceptInviteOptionsInternal acceptInviteOptionsInternal = Helper.CopyProperties<AcceptInviteOptionsInternal>(options);
			OnAcceptInviteCallbackInternal onAcceptInviteCallbackInternal = new OnAcceptInviteCallbackInternal(FriendsInterface.OnAcceptInvite);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onAcceptInviteCallbackInternal, Array.Empty<Delegate>());
			FriendsInterface.EOS_Friends_AcceptInvite(base.InnerHandle, ref acceptInviteOptionsInternal, zero, onAcceptInviteCallbackInternal);
			Helper.TryMarshalDispose<AcceptInviteOptionsInternal>(ref acceptInviteOptionsInternal);
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x00091C34 File Offset: 0x0008FE34
		public void RejectInvite(RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
		{
			RejectInviteOptionsInternal rejectInviteOptionsInternal = Helper.CopyProperties<RejectInviteOptionsInternal>(options);
			OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = new OnRejectInviteCallbackInternal(FriendsInterface.OnRejectInvite);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onRejectInviteCallbackInternal, Array.Empty<Delegate>());
			FriendsInterface.EOS_Friends_RejectInvite(base.InnerHandle, ref rejectInviteOptionsInternal, zero, onRejectInviteCallbackInternal);
			Helper.TryMarshalDispose<RejectInviteOptionsInternal>(ref rejectInviteOptionsInternal);
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x00091C84 File Offset: 0x0008FE84
		public int GetFriendsCount(GetFriendsCountOptions options)
		{
			GetFriendsCountOptionsInternal getFriendsCountOptionsInternal = Helper.CopyProperties<GetFriendsCountOptionsInternal>(options);
			int num = FriendsInterface.EOS_Friends_GetFriendsCount(base.InnerHandle, ref getFriendsCountOptionsInternal);
			Helper.TryMarshalDispose<GetFriendsCountOptionsInternal>(ref getFriendsCountOptionsInternal);
			int @default = Helper.GetDefault<int>();
			Helper.TryMarshalGet<int>(num, out @default);
			return @default;
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x00091CBC File Offset: 0x0008FEBC
		public EpicAccountId GetFriendAtIndex(GetFriendAtIndexOptions options)
		{
			GetFriendAtIndexOptionsInternal getFriendAtIndexOptionsInternal = Helper.CopyProperties<GetFriendAtIndexOptionsInternal>(options);
			IntPtr intPtr = FriendsInterface.EOS_Friends_GetFriendAtIndex(base.InnerHandle, ref getFriendAtIndexOptionsInternal);
			Helper.TryMarshalDispose<GetFriendAtIndexOptionsInternal>(ref getFriendAtIndexOptionsInternal);
			EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
			Helper.TryMarshalGet<EpicAccountId>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x00091CF4 File Offset: 0x0008FEF4
		public FriendsStatus GetStatus(GetStatusOptions options)
		{
			GetStatusOptionsInternal getStatusOptionsInternal = Helper.CopyProperties<GetStatusOptionsInternal>(options);
			FriendsStatus friendsStatus = FriendsInterface.EOS_Friends_GetStatus(base.InnerHandle, ref getStatusOptionsInternal);
			Helper.TryMarshalDispose<GetStatusOptionsInternal>(ref getStatusOptionsInternal);
			FriendsStatus @default = Helper.GetDefault<FriendsStatus>();
			Helper.TryMarshalGet<FriendsStatus>(friendsStatus, out @default);
			return @default;
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x00091D2C File Offset: 0x0008FF2C
		public ulong AddNotifyFriendsUpdate(AddNotifyFriendsUpdateOptions options, object clientData, OnFriendsUpdateCallback friendsUpdateHandler)
		{
			AddNotifyFriendsUpdateOptionsInternal addNotifyFriendsUpdateOptionsInternal = Helper.CopyProperties<AddNotifyFriendsUpdateOptionsInternal>(options);
			OnFriendsUpdateCallbackInternal onFriendsUpdateCallbackInternal = new OnFriendsUpdateCallbackInternal(FriendsInterface.OnFriendsUpdate);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, friendsUpdateHandler, onFriendsUpdateCallbackInternal, Array.Empty<Delegate>());
			ulong num = FriendsInterface.EOS_Friends_AddNotifyFriendsUpdate(base.InnerHandle, ref addNotifyFriendsUpdateOptionsInternal, zero, onFriendsUpdateCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyFriendsUpdateOptionsInternal>(ref addNotifyFriendsUpdateOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x00091D94 File Offset: 0x0008FF94
		public void RemoveNotifyFriendsUpdate(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			FriendsInterface.EOS_Friends_RemoveNotifyFriendsUpdate(base.InnerHandle, notificationId);
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x00091DAC File Offset: 0x0008FFAC
		[MonoPInvokeCallback]
		internal static void OnFriendsUpdate(IntPtr address)
		{
			OnFriendsUpdateCallback onFriendsUpdateCallback = null;
			OnFriendsUpdateInfo onFriendsUpdateInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnFriendsUpdateCallback, OnFriendsUpdateInfoInternal, OnFriendsUpdateInfo>(address, out onFriendsUpdateCallback, out onFriendsUpdateInfo))
			{
				onFriendsUpdateCallback(onFriendsUpdateInfo);
			}
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x00091DD0 File Offset: 0x0008FFD0
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

		// Token: 0x060049C0 RID: 18880 RVA: 0x00091DF4 File Offset: 0x0008FFF4
		[MonoPInvokeCallback]
		internal static void OnAcceptInvite(IntPtr address)
		{
			OnAcceptInviteCallback onAcceptInviteCallback = null;
			AcceptInviteCallbackInfo acceptInviteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnAcceptInviteCallback, AcceptInviteCallbackInfoInternal, AcceptInviteCallbackInfo>(address, out onAcceptInviteCallback, out acceptInviteCallbackInfo))
			{
				onAcceptInviteCallback(acceptInviteCallbackInfo);
			}
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x00091E18 File Offset: 0x00090018
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

		// Token: 0x060049C2 RID: 18882 RVA: 0x00091E3C File Offset: 0x0009003C
		[MonoPInvokeCallback]
		internal static void OnQueryFriends(IntPtr address)
		{
			OnQueryFriendsCallback onQueryFriendsCallback = null;
			QueryFriendsCallbackInfo queryFriendsCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryFriendsCallback, QueryFriendsCallbackInfoInternal, QueryFriendsCallbackInfo>(address, out onQueryFriendsCallback, out queryFriendsCallbackInfo))
			{
				onQueryFriendsCallback(queryFriendsCallbackInfo);
			}
		}

		// Token: 0x060049C3 RID: 18883
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Friends_RemoveNotifyFriendsUpdate(IntPtr handle, ulong notificationId);

		// Token: 0x060049C4 RID: 18884
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Friends_AddNotifyFriendsUpdate(IntPtr handle, ref AddNotifyFriendsUpdateOptionsInternal options, IntPtr clientData, OnFriendsUpdateCallbackInternal friendsUpdateHandler);

		// Token: 0x060049C5 RID: 18885
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern FriendsStatus EOS_Friends_GetStatus(IntPtr handle, ref GetStatusOptionsInternal options);

		// Token: 0x060049C6 RID: 18886
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Friends_GetFriendAtIndex(IntPtr handle, ref GetFriendAtIndexOptionsInternal options);

		// Token: 0x060049C7 RID: 18887
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern int EOS_Friends_GetFriendsCount(IntPtr handle, ref GetFriendsCountOptionsInternal options);

		// Token: 0x060049C8 RID: 18888
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Friends_RejectInvite(IntPtr handle, ref RejectInviteOptionsInternal options, IntPtr clientData, OnRejectInviteCallbackInternal completionDelegate);

		// Token: 0x060049C9 RID: 18889
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Friends_AcceptInvite(IntPtr handle, ref AcceptInviteOptionsInternal options, IntPtr clientData, OnAcceptInviteCallbackInternal completionDelegate);

		// Token: 0x060049CA RID: 18890
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Friends_SendInvite(IntPtr handle, ref SendInviteOptionsInternal options, IntPtr clientData, OnSendInviteCallbackInternal completionDelegate);

		// Token: 0x060049CB RID: 18891
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Friends_QueryFriends(IntPtr handle, ref QueryFriendsOptionsInternal options, IntPtr clientData, OnQueryFriendsCallbackInternal completionDelegate);

		// Token: 0x04001C70 RID: 7280
		public const int AddnotifyfriendsupdateApiLatest = 1;

		// Token: 0x04001C71 RID: 7281
		public const int GetstatusApiLatest = 1;

		// Token: 0x04001C72 RID: 7282
		public const int GetfriendatindexApiLatest = 1;

		// Token: 0x04001C73 RID: 7283
		public const int GetfriendscountApiLatest = 1;

		// Token: 0x04001C74 RID: 7284
		public const int DeletefriendApiLatest = 1;

		// Token: 0x04001C75 RID: 7285
		public const int RejectinviteApiLatest = 1;

		// Token: 0x04001C76 RID: 7286
		public const int AcceptinviteApiLatest = 1;

		// Token: 0x04001C77 RID: 7287
		public const int SendinviteApiLatest = 1;

		// Token: 0x04001C78 RID: 7288
		public const int QueryfriendsApiLatest = 1;
	}
}
