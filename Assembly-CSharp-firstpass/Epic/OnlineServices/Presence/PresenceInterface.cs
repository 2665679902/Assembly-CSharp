using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200067E RID: 1662
	public sealed class PresenceInterface : Handle
	{
		// Token: 0x0600404B RID: 16459 RVA: 0x0008830A File Offset: 0x0008650A
		public PresenceInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x00088314 File Offset: 0x00086514
		public void QueryPresence(QueryPresenceOptions options, object clientData, OnQueryPresenceCompleteCallback completionDelegate)
		{
			QueryPresenceOptionsInternal queryPresenceOptionsInternal = Helper.CopyProperties<QueryPresenceOptionsInternal>(options);
			OnQueryPresenceCompleteCallbackInternal onQueryPresenceCompleteCallbackInternal = new OnQueryPresenceCompleteCallbackInternal(PresenceInterface.OnQueryPresenceComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryPresenceCompleteCallbackInternal, Array.Empty<Delegate>());
			PresenceInterface.EOS_Presence_QueryPresence(base.InnerHandle, ref queryPresenceOptionsInternal, zero, onQueryPresenceCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryPresenceOptionsInternal>(ref queryPresenceOptionsInternal);
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x00088364 File Offset: 0x00086564
		public bool HasPresence(HasPresenceOptions options)
		{
			HasPresenceOptionsInternal hasPresenceOptionsInternal = Helper.CopyProperties<HasPresenceOptionsInternal>(options);
			int num = PresenceInterface.EOS_Presence_HasPresence(base.InnerHandle, ref hasPresenceOptionsInternal);
			Helper.TryMarshalDispose<HasPresenceOptionsInternal>(ref hasPresenceOptionsInternal);
			bool @default = Helper.GetDefault<bool>();
			Helper.TryMarshalGet(num, out @default);
			return @default;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x0008839C File Offset: 0x0008659C
		public Result CopyPresence(CopyPresenceOptions options, out Info outPresence)
		{
			CopyPresenceOptionsInternal copyPresenceOptionsInternal = Helper.CopyProperties<CopyPresenceOptionsInternal>(options);
			outPresence = Helper.GetDefault<Info>();
			IntPtr zero = IntPtr.Zero;
			Result result = PresenceInterface.EOS_Presence_CopyPresence(base.InnerHandle, ref copyPresenceOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyPresenceOptionsInternal>(ref copyPresenceOptionsInternal);
			if (Helper.TryMarshalGet<InfoInternal, Info>(zero, out outPresence))
			{
				PresenceInterface.EOS_Presence_Info_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x000883F4 File Offset: 0x000865F4
		public Result CreatePresenceModification(CreatePresenceModificationOptions options, out PresenceModification outPresenceModificationHandle)
		{
			CreatePresenceModificationOptionsInternal createPresenceModificationOptionsInternal = Helper.CopyProperties<CreatePresenceModificationOptionsInternal>(options);
			outPresenceModificationHandle = Helper.GetDefault<PresenceModification>();
			IntPtr zero = IntPtr.Zero;
			Result result = PresenceInterface.EOS_Presence_CreatePresenceModification(base.InnerHandle, ref createPresenceModificationOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CreatePresenceModificationOptionsInternal>(ref createPresenceModificationOptionsInternal);
			Helper.TryMarshalGet<PresenceModification>(zero, out outPresenceModificationHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x00088444 File Offset: 0x00086644
		public void SetPresence(SetPresenceOptions options, object clientData, SetPresenceCompleteCallback completionDelegate)
		{
			SetPresenceOptionsInternal setPresenceOptionsInternal = Helper.CopyProperties<SetPresenceOptionsInternal>(options);
			SetPresenceCompleteCallbackInternal setPresenceCompleteCallbackInternal = new SetPresenceCompleteCallbackInternal(PresenceInterface.SetPresenceComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, setPresenceCompleteCallbackInternal, Array.Empty<Delegate>());
			PresenceInterface.EOS_Presence_SetPresence(base.InnerHandle, ref setPresenceOptionsInternal, zero, setPresenceCompleteCallbackInternal);
			Helper.TryMarshalDispose<SetPresenceOptionsInternal>(ref setPresenceOptionsInternal);
		}

		// Token: 0x06004051 RID: 16465 RVA: 0x00088494 File Offset: 0x00086694
		public ulong AddNotifyOnPresenceChanged(AddNotifyOnPresenceChangedOptions options, object clientData, OnPresenceChangedCallback notificationHandler)
		{
			AddNotifyOnPresenceChangedOptionsInternal addNotifyOnPresenceChangedOptionsInternal = Helper.CopyProperties<AddNotifyOnPresenceChangedOptionsInternal>(options);
			OnPresenceChangedCallbackInternal onPresenceChangedCallbackInternal = new OnPresenceChangedCallbackInternal(PresenceInterface.OnPresenceChanged);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationHandler, onPresenceChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = PresenceInterface.EOS_Presence_AddNotifyOnPresenceChanged(base.InnerHandle, ref addNotifyOnPresenceChangedOptionsInternal, zero, onPresenceChangedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyOnPresenceChangedOptionsInternal>(ref addNotifyOnPresenceChangedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06004052 RID: 16466 RVA: 0x000884FC File Offset: 0x000866FC
		public void RemoveNotifyOnPresenceChanged(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			PresenceInterface.EOS_Presence_RemoveNotifyOnPresenceChanged(base.InnerHandle, notificationId);
		}

		// Token: 0x06004053 RID: 16467 RVA: 0x00088514 File Offset: 0x00086714
		public ulong AddNotifyJoinGameAccepted(AddNotifyJoinGameAcceptedOptions options, object clientData, OnJoinGameAcceptedCallback notificationFn)
		{
			AddNotifyJoinGameAcceptedOptionsInternal addNotifyJoinGameAcceptedOptionsInternal = Helper.CopyProperties<AddNotifyJoinGameAcceptedOptionsInternal>(options);
			OnJoinGameAcceptedCallbackInternal onJoinGameAcceptedCallbackInternal = new OnJoinGameAcceptedCallbackInternal(PresenceInterface.OnJoinGameAccepted);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onJoinGameAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = PresenceInterface.EOS_Presence_AddNotifyJoinGameAccepted(base.InnerHandle, ref addNotifyJoinGameAcceptedOptionsInternal, zero, onJoinGameAcceptedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyJoinGameAcceptedOptionsInternal>(ref addNotifyJoinGameAcceptedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06004054 RID: 16468 RVA: 0x0008857C File Offset: 0x0008677C
		public void RemoveNotifyJoinGameAccepted(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			PresenceInterface.EOS_Presence_RemoveNotifyJoinGameAccepted(base.InnerHandle, inId);
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x00088594 File Offset: 0x00086794
		public Result GetJoinInfo(GetJoinInfoOptions options, StringBuilder outBuffer, ref int inOutBufferLength)
		{
			GetJoinInfoOptionsInternal getJoinInfoOptionsInternal = Helper.CopyProperties<GetJoinInfoOptionsInternal>(options);
			Result result = PresenceInterface.EOS_Presence_GetJoinInfo(base.InnerHandle, ref getJoinInfoOptionsInternal, outBuffer, ref inOutBufferLength);
			Helper.TryMarshalDispose<GetJoinInfoOptionsInternal>(ref getJoinInfoOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x000885D0 File Offset: 0x000867D0
		[MonoPInvokeCallback]
		internal static void OnJoinGameAccepted(IntPtr address)
		{
			OnJoinGameAcceptedCallback onJoinGameAcceptedCallback = null;
			JoinGameAcceptedCallbackInfo joinGameAcceptedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnJoinGameAcceptedCallback, JoinGameAcceptedCallbackInfoInternal, JoinGameAcceptedCallbackInfo>(address, out onJoinGameAcceptedCallback, out joinGameAcceptedCallbackInfo))
			{
				onJoinGameAcceptedCallback(joinGameAcceptedCallbackInfo);
			}
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x000885F4 File Offset: 0x000867F4
		[MonoPInvokeCallback]
		internal static void OnPresenceChanged(IntPtr address)
		{
			OnPresenceChangedCallback onPresenceChangedCallback = null;
			PresenceChangedCallbackInfo presenceChangedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnPresenceChangedCallback, PresenceChangedCallbackInfoInternal, PresenceChangedCallbackInfo>(address, out onPresenceChangedCallback, out presenceChangedCallbackInfo))
			{
				onPresenceChangedCallback(presenceChangedCallbackInfo);
			}
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x00088618 File Offset: 0x00086818
		[MonoPInvokeCallback]
		internal static void SetPresenceComplete(IntPtr address)
		{
			SetPresenceCompleteCallback setPresenceCompleteCallback = null;
			SetPresenceCallbackInfo setPresenceCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<SetPresenceCompleteCallback, SetPresenceCallbackInfoInternal, SetPresenceCallbackInfo>(address, out setPresenceCompleteCallback, out setPresenceCallbackInfo))
			{
				setPresenceCompleteCallback(setPresenceCallbackInfo);
			}
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x0008863C File Offset: 0x0008683C
		[MonoPInvokeCallback]
		internal static void OnQueryPresenceComplete(IntPtr address)
		{
			OnQueryPresenceCompleteCallback onQueryPresenceCompleteCallback = null;
			QueryPresenceCallbackInfo queryPresenceCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryPresenceCompleteCallback, QueryPresenceCallbackInfoInternal, QueryPresenceCallbackInfo>(address, out onQueryPresenceCompleteCallback, out queryPresenceCallbackInfo))
			{
				onQueryPresenceCompleteCallback(queryPresenceCallbackInfo);
			}
		}

		// Token: 0x0600405A RID: 16474
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Presence_Info_Release(IntPtr presenceInfo);

		// Token: 0x0600405B RID: 16475
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Presence_GetJoinInfo(IntPtr handle, ref GetJoinInfoOptionsInternal options, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x0600405C RID: 16476
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Presence_RemoveNotifyJoinGameAccepted(IntPtr handle, ulong inId);

		// Token: 0x0600405D RID: 16477
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Presence_AddNotifyJoinGameAccepted(IntPtr handle, ref AddNotifyJoinGameAcceptedOptionsInternal options, IntPtr clientData, OnJoinGameAcceptedCallbackInternal notificationFn);

		// Token: 0x0600405E RID: 16478
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Presence_RemoveNotifyOnPresenceChanged(IntPtr handle, ulong notificationId);

		// Token: 0x0600405F RID: 16479
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Presence_AddNotifyOnPresenceChanged(IntPtr handle, ref AddNotifyOnPresenceChangedOptionsInternal options, IntPtr clientData, OnPresenceChangedCallbackInternal notificationHandler);

		// Token: 0x06004060 RID: 16480
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Presence_SetPresence(IntPtr handle, ref SetPresenceOptionsInternal options, IntPtr clientData, SetPresenceCompleteCallbackInternal completionDelegate);

		// Token: 0x06004061 RID: 16481
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Presence_CreatePresenceModification(IntPtr handle, ref CreatePresenceModificationOptionsInternal options, ref IntPtr outPresenceModificationHandle);

		// Token: 0x06004062 RID: 16482
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Presence_CopyPresence(IntPtr handle, ref CopyPresenceOptionsInternal options, ref IntPtr outPresence);

		// Token: 0x06004063 RID: 16483
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern int EOS_Presence_HasPresence(IntPtr handle, ref HasPresenceOptionsInternal options);

		// Token: 0x06004064 RID: 16484
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Presence_QueryPresence(IntPtr handle, ref QueryPresenceOptionsInternal options, IntPtr clientData, OnQueryPresenceCompleteCallbackInternal completionDelegate);

		// Token: 0x0400188A RID: 6282
		public const int DeletedataApiLatest = 1;

		// Token: 0x0400188B RID: 6283
		public const int PresencemodificationDeletedataApiLatest = 1;

		// Token: 0x0400188C RID: 6284
		public const int PresencemodificationDatarecordidApiLatest = 1;

		// Token: 0x0400188D RID: 6285
		public const int SetdataApiLatest = 1;

		// Token: 0x0400188E RID: 6286
		public const int PresencemodificationSetdataApiLatest = 1;

		// Token: 0x0400188F RID: 6287
		public const int SetrawrichtextApiLatest = 1;

		// Token: 0x04001890 RID: 6288
		public const int PresencemodificationSetrawrichtextApiLatest = 1;

		// Token: 0x04001891 RID: 6289
		public const int SetstatusApiLatest = 1;

		// Token: 0x04001892 RID: 6290
		public const int PresencemodificationSetstatusApiLatest = 1;

		// Token: 0x04001893 RID: 6291
		public const int RichTextMaxValueLength = 255;

		// Token: 0x04001894 RID: 6292
		public const int DataMaxValueLength = 255;

		// Token: 0x04001895 RID: 6293
		public const int DataMaxKeyLength = 64;

		// Token: 0x04001896 RID: 6294
		public const int DataMaxKeys = 32;

		// Token: 0x04001897 RID: 6295
		public const int PresencemodificationSetjoininfoApiLatest = 1;

		// Token: 0x04001898 RID: 6296
		public const int PresencemodificationJoininfoMaxLength = 255;

		// Token: 0x04001899 RID: 6297
		public const int GetjoininfoApiLatest = 1;

		// Token: 0x0400189A RID: 6298
		public const int AddnotifyjoingameacceptedApiLatest = 2;

		// Token: 0x0400189B RID: 6299
		public const int AddnotifyonpresencechangedApiLatest = 1;

		// Token: 0x0400189C RID: 6300
		public const int SetpresenceApiLatest = 1;

		// Token: 0x0400189D RID: 6301
		public const int CreatepresencemodificationApiLatest = 1;

		// Token: 0x0400189E RID: 6302
		public const int CopypresenceApiLatest = 2;

		// Token: 0x0400189F RID: 6303
		public const int HaspresenceApiLatest = 1;

		// Token: 0x040018A0 RID: 6304
		public const int QuerypresenceApiLatest = 1;

		// Token: 0x040018A1 RID: 6305
		public const int InfoApiLatest = 2;

		// Token: 0x040018A2 RID: 6306
		public const int DatarecordApiLatest = 1;
	}
}
