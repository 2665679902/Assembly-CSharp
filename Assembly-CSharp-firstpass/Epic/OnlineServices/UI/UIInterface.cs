using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000577 RID: 1399
	public sealed class UIInterface : Handle
	{
		// Token: 0x06003A10 RID: 14864 RVA: 0x00082057 File Offset: 0x00080257
		public UIInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x00082060 File Offset: 0x00080260
		public void ShowFriends(ShowFriendsOptions options, object clientData, OnShowFriendsCallback completionDelegate)
		{
			ShowFriendsOptionsInternal showFriendsOptionsInternal = Helper.CopyProperties<ShowFriendsOptionsInternal>(options);
			OnShowFriendsCallbackInternal onShowFriendsCallbackInternal = new OnShowFriendsCallbackInternal(UIInterface.OnShowFriends);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onShowFriendsCallbackInternal, Array.Empty<Delegate>());
			UIInterface.EOS_UI_ShowFriends(base.InnerHandle, ref showFriendsOptionsInternal, zero, onShowFriendsCallbackInternal);
			Helper.TryMarshalDispose<ShowFriendsOptionsInternal>(ref showFriendsOptionsInternal);
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000820B0 File Offset: 0x000802B0
		public void HideFriends(HideFriendsOptions options, object clientData, OnHideFriendsCallback completionDelegate)
		{
			HideFriendsOptionsInternal hideFriendsOptionsInternal = Helper.CopyProperties<HideFriendsOptionsInternal>(options);
			OnHideFriendsCallbackInternal onHideFriendsCallbackInternal = new OnHideFriendsCallbackInternal(UIInterface.OnHideFriends);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onHideFriendsCallbackInternal, Array.Empty<Delegate>());
			UIInterface.EOS_UI_HideFriends(base.InnerHandle, ref hideFriendsOptionsInternal, zero, onHideFriendsCallbackInternal);
			Helper.TryMarshalDispose<HideFriendsOptionsInternal>(ref hideFriendsOptionsInternal);
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x00082100 File Offset: 0x00080300
		public bool GetFriendsVisible(GetFriendsVisibleOptions options)
		{
			GetFriendsVisibleOptionsInternal getFriendsVisibleOptionsInternal = Helper.CopyProperties<GetFriendsVisibleOptionsInternal>(options);
			int num = UIInterface.EOS_UI_GetFriendsVisible(base.InnerHandle, ref getFriendsVisibleOptionsInternal);
			Helper.TryMarshalDispose<GetFriendsVisibleOptionsInternal>(ref getFriendsVisibleOptionsInternal);
			bool @default = Helper.GetDefault<bool>();
			Helper.TryMarshalGet(num, out @default);
			return @default;
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x00082138 File Offset: 0x00080338
		public ulong AddNotifyDisplaySettingsUpdated(AddNotifyDisplaySettingsUpdatedOptions options, object clientData, OnDisplaySettingsUpdatedCallback notificationFn)
		{
			AddNotifyDisplaySettingsUpdatedOptionsInternal addNotifyDisplaySettingsUpdatedOptionsInternal = Helper.CopyProperties<AddNotifyDisplaySettingsUpdatedOptionsInternal>(options);
			OnDisplaySettingsUpdatedCallbackInternal onDisplaySettingsUpdatedCallbackInternal = new OnDisplaySettingsUpdatedCallbackInternal(UIInterface.OnDisplaySettingsUpdated);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onDisplaySettingsUpdatedCallbackInternal, Array.Empty<Delegate>());
			ulong num = UIInterface.EOS_UI_AddNotifyDisplaySettingsUpdated(base.InnerHandle, ref addNotifyDisplaySettingsUpdatedOptionsInternal, zero, onDisplaySettingsUpdatedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyDisplaySettingsUpdatedOptionsInternal>(ref addNotifyDisplaySettingsUpdatedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000821A0 File Offset: 0x000803A0
		public void RemoveNotifyDisplaySettingsUpdated(ulong id)
		{
			Helper.TryRemoveCallbackByNotificationId(id);
			UIInterface.EOS_UI_RemoveNotifyDisplaySettingsUpdated(base.InnerHandle, id);
		}

		// Token: 0x06003A16 RID: 14870 RVA: 0x000821B8 File Offset: 0x000803B8
		public Result SetToggleFriendsKey(SetToggleFriendsKeyOptions options)
		{
			SetToggleFriendsKeyOptionsInternal setToggleFriendsKeyOptionsInternal = Helper.CopyProperties<SetToggleFriendsKeyOptionsInternal>(options);
			Result result = UIInterface.EOS_UI_SetToggleFriendsKey(base.InnerHandle, ref setToggleFriendsKeyOptionsInternal);
			Helper.TryMarshalDispose<SetToggleFriendsKeyOptionsInternal>(ref setToggleFriendsKeyOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003A17 RID: 14871 RVA: 0x000821F0 File Offset: 0x000803F0
		public KeyCombination GetToggleFriendsKey(GetToggleFriendsKeyOptions options)
		{
			GetToggleFriendsKeyOptionsInternal getToggleFriendsKeyOptionsInternal = Helper.CopyProperties<GetToggleFriendsKeyOptionsInternal>(options);
			KeyCombination keyCombination = UIInterface.EOS_UI_GetToggleFriendsKey(base.InnerHandle, ref getToggleFriendsKeyOptionsInternal);
			Helper.TryMarshalDispose<GetToggleFriendsKeyOptionsInternal>(ref getToggleFriendsKeyOptionsInternal);
			KeyCombination @default = Helper.GetDefault<KeyCombination>();
			Helper.TryMarshalGet<KeyCombination>(keyCombination, out @default);
			return @default;
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x00082228 File Offset: 0x00080428
		public bool IsValidKeyCombination(KeyCombination keyCombination)
		{
			int num = UIInterface.EOS_UI_IsValidKeyCombination(base.InnerHandle, keyCombination);
			bool @default = Helper.GetDefault<bool>();
			Helper.TryMarshalGet(num, out @default);
			return @default;
		}

		// Token: 0x06003A19 RID: 14873 RVA: 0x00082250 File Offset: 0x00080450
		public Result SetDisplayPreference(SetDisplayPreferenceOptions options)
		{
			SetDisplayPreferenceOptionsInternal setDisplayPreferenceOptionsInternal = Helper.CopyProperties<SetDisplayPreferenceOptionsInternal>(options);
			Result result = UIInterface.EOS_UI_SetDisplayPreference(base.InnerHandle, ref setDisplayPreferenceOptionsInternal);
			Helper.TryMarshalDispose<SetDisplayPreferenceOptionsInternal>(ref setDisplayPreferenceOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003A1A RID: 14874 RVA: 0x00082288 File Offset: 0x00080488
		public NotificationLocation GetNotificationLocationPreference()
		{
			NotificationLocation notificationLocation = UIInterface.EOS_UI_GetNotificationLocationPreference(base.InnerHandle);
			NotificationLocation @default = Helper.GetDefault<NotificationLocation>();
			Helper.TryMarshalGet<NotificationLocation>(notificationLocation, out @default);
			return @default;
		}

		// Token: 0x06003A1B RID: 14875 RVA: 0x000822B0 File Offset: 0x000804B0
		public Result AcknowledgeEventId(AcknowledgeEventIdOptions options)
		{
			AcknowledgeEventIdOptionsInternal acknowledgeEventIdOptionsInternal = Helper.CopyProperties<AcknowledgeEventIdOptionsInternal>(options);
			Result result = UIInterface.EOS_UI_AcknowledgeEventId(base.InnerHandle, ref acknowledgeEventIdOptionsInternal);
			Helper.TryMarshalDispose<AcknowledgeEventIdOptionsInternal>(ref acknowledgeEventIdOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000822E8 File Offset: 0x000804E8
		[MonoPInvokeCallback]
		internal static void OnDisplaySettingsUpdated(IntPtr address)
		{
			OnDisplaySettingsUpdatedCallback onDisplaySettingsUpdatedCallback = null;
			OnDisplaySettingsUpdatedCallbackInfo onDisplaySettingsUpdatedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnDisplaySettingsUpdatedCallback, OnDisplaySettingsUpdatedCallbackInfoInternal, OnDisplaySettingsUpdatedCallbackInfo>(address, out onDisplaySettingsUpdatedCallback, out onDisplaySettingsUpdatedCallbackInfo))
			{
				onDisplaySettingsUpdatedCallback(onDisplaySettingsUpdatedCallbackInfo);
			}
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x0008230C File Offset: 0x0008050C
		[MonoPInvokeCallback]
		internal static void OnHideFriends(IntPtr address)
		{
			OnHideFriendsCallback onHideFriendsCallback = null;
			HideFriendsCallbackInfo hideFriendsCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnHideFriendsCallback, HideFriendsCallbackInfoInternal, HideFriendsCallbackInfo>(address, out onHideFriendsCallback, out hideFriendsCallbackInfo))
			{
				onHideFriendsCallback(hideFriendsCallbackInfo);
			}
		}

		// Token: 0x06003A1E RID: 14878 RVA: 0x00082330 File Offset: 0x00080530
		[MonoPInvokeCallback]
		internal static void OnShowFriends(IntPtr address)
		{
			OnShowFriendsCallback onShowFriendsCallback = null;
			ShowFriendsCallbackInfo showFriendsCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnShowFriendsCallback, ShowFriendsCallbackInfoInternal, ShowFriendsCallbackInfo>(address, out onShowFriendsCallback, out showFriendsCallbackInfo))
			{
				onShowFriendsCallback(showFriendsCallbackInfo);
			}
		}

		// Token: 0x06003A1F RID: 14879
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_UI_AcknowledgeEventId(IntPtr handle, ref AcknowledgeEventIdOptionsInternal options);

		// Token: 0x06003A20 RID: 14880
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern NotificationLocation EOS_UI_GetNotificationLocationPreference(IntPtr handle);

		// Token: 0x06003A21 RID: 14881
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_UI_SetDisplayPreference(IntPtr handle, ref SetDisplayPreferenceOptionsInternal options);

		// Token: 0x06003A22 RID: 14882
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern int EOS_UI_IsValidKeyCombination(IntPtr handle, KeyCombination keyCombination);

		// Token: 0x06003A23 RID: 14883
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern KeyCombination EOS_UI_GetToggleFriendsKey(IntPtr handle, ref GetToggleFriendsKeyOptionsInternal options);

		// Token: 0x06003A24 RID: 14884
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_UI_SetToggleFriendsKey(IntPtr handle, ref SetToggleFriendsKeyOptionsInternal options);

		// Token: 0x06003A25 RID: 14885
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_UI_RemoveNotifyDisplaySettingsUpdated(IntPtr handle, ulong id);

		// Token: 0x06003A26 RID: 14886
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_UI_AddNotifyDisplaySettingsUpdated(IntPtr handle, ref AddNotifyDisplaySettingsUpdatedOptionsInternal options, IntPtr clientData, OnDisplaySettingsUpdatedCallbackInternal notificationFn);

		// Token: 0x06003A27 RID: 14887
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern int EOS_UI_GetFriendsVisible(IntPtr handle, ref GetFriendsVisibleOptionsInternal options);

		// Token: 0x06003A28 RID: 14888
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_UI_HideFriends(IntPtr handle, ref HideFriendsOptionsInternal options, IntPtr clientData, OnHideFriendsCallbackInternal completionDelegate);

		// Token: 0x06003A29 RID: 14889
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_UI_ShowFriends(IntPtr handle, ref ShowFriendsOptionsInternal options, IntPtr clientData, OnShowFriendsCallbackInternal completionDelegate);

		// Token: 0x0400161F RID: 5663
		public const int AcknowledgecorrelationidApiLatest = 1;

		// Token: 0x04001620 RID: 5664
		public const int AcknowledgeeventidApiLatest = 1;

		// Token: 0x04001621 RID: 5665
		public const int SetdisplaypreferenceApiLatest = 1;

		// Token: 0x04001622 RID: 5666
		public const int GettogglefriendskeyApiLatest = 1;

		// Token: 0x04001623 RID: 5667
		public const int SettogglefriendskeyApiLatest = 1;

		// Token: 0x04001624 RID: 5668
		public const int AddnotifydisplaysettingsupdatedApiLatest = 1;

		// Token: 0x04001625 RID: 5669
		public const int GetfriendsvisibleApiLatest = 1;

		// Token: 0x04001626 RID: 5670
		public const int HidefriendsApiLatest = 1;

		// Token: 0x04001627 RID: 5671
		public const int ShowfriendsApiLatest = 1;

		// Token: 0x04001628 RID: 5672
		public const int EventidInvalid = 0;
	}
}
