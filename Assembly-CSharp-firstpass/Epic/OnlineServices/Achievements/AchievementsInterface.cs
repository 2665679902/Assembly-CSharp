using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000911 RID: 2321
	public sealed class AchievementsInterface : Handle
	{
		// Token: 0x060050C4 RID: 20676 RVA: 0x00098A9B File Offset: 0x00096C9B
		public AchievementsInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x00098AA4 File Offset: 0x00096CA4
		public void QueryDefinitions(QueryDefinitionsOptions options, object clientData, OnQueryDefinitionsCompleteCallback completionDelegate)
		{
			QueryDefinitionsOptionsInternal queryDefinitionsOptionsInternal = Helper.CopyProperties<QueryDefinitionsOptionsInternal>(options);
			OnQueryDefinitionsCompleteCallbackInternal onQueryDefinitionsCompleteCallbackInternal = new OnQueryDefinitionsCompleteCallbackInternal(AchievementsInterface.OnQueryDefinitionsComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryDefinitionsCompleteCallbackInternal, Array.Empty<Delegate>());
			AchievementsInterface.EOS_Achievements_QueryDefinitions(base.InnerHandle, ref queryDefinitionsOptionsInternal, zero, onQueryDefinitionsCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryDefinitionsOptionsInternal>(ref queryDefinitionsOptionsInternal);
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x00098AF4 File Offset: 0x00096CF4
		public uint GetAchievementDefinitionCount(GetAchievementDefinitionCountOptions options)
		{
			GetAchievementDefinitionCountOptionsInternal getAchievementDefinitionCountOptionsInternal = Helper.CopyProperties<GetAchievementDefinitionCountOptionsInternal>(options);
			uint num = AchievementsInterface.EOS_Achievements_GetAchievementDefinitionCount(base.InnerHandle, ref getAchievementDefinitionCountOptionsInternal);
			Helper.TryMarshalDispose<GetAchievementDefinitionCountOptionsInternal>(ref getAchievementDefinitionCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x00098B2C File Offset: 0x00096D2C
		public Result CopyAchievementDefinitionV2ByIndex(CopyAchievementDefinitionV2ByIndexOptions options, out DefinitionV2 outDefinition)
		{
			CopyAchievementDefinitionV2ByIndexOptionsInternal copyAchievementDefinitionV2ByIndexOptionsInternal = Helper.CopyProperties<CopyAchievementDefinitionV2ByIndexOptionsInternal>(options);
			outDefinition = Helper.GetDefault<DefinitionV2>();
			IntPtr zero = IntPtr.Zero;
			Result result = AchievementsInterface.EOS_Achievements_CopyAchievementDefinitionV2ByIndex(base.InnerHandle, ref copyAchievementDefinitionV2ByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyAchievementDefinitionV2ByIndexOptionsInternal>(ref copyAchievementDefinitionV2ByIndexOptionsInternal);
			if (Helper.TryMarshalGet<DefinitionV2Internal, DefinitionV2>(zero, out outDefinition))
			{
				AchievementsInterface.EOS_Achievements_DefinitionV2_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x00098B84 File Offset: 0x00096D84
		public Result CopyAchievementDefinitionV2ByAchievementId(CopyAchievementDefinitionV2ByAchievementIdOptions options, out DefinitionV2 outDefinition)
		{
			CopyAchievementDefinitionV2ByAchievementIdOptionsInternal copyAchievementDefinitionV2ByAchievementIdOptionsInternal = Helper.CopyProperties<CopyAchievementDefinitionV2ByAchievementIdOptionsInternal>(options);
			outDefinition = Helper.GetDefault<DefinitionV2>();
			IntPtr zero = IntPtr.Zero;
			Result result = AchievementsInterface.EOS_Achievements_CopyAchievementDefinitionV2ByAchievementId(base.InnerHandle, ref copyAchievementDefinitionV2ByAchievementIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyAchievementDefinitionV2ByAchievementIdOptionsInternal>(ref copyAchievementDefinitionV2ByAchievementIdOptionsInternal);
			if (Helper.TryMarshalGet<DefinitionV2Internal, DefinitionV2>(zero, out outDefinition))
			{
				AchievementsInterface.EOS_Achievements_DefinitionV2_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x00098BDC File Offset: 0x00096DDC
		public void QueryPlayerAchievements(QueryPlayerAchievementsOptions options, object clientData, OnQueryPlayerAchievementsCompleteCallback completionDelegate)
		{
			QueryPlayerAchievementsOptionsInternal queryPlayerAchievementsOptionsInternal = Helper.CopyProperties<QueryPlayerAchievementsOptionsInternal>(options);
			OnQueryPlayerAchievementsCompleteCallbackInternal onQueryPlayerAchievementsCompleteCallbackInternal = new OnQueryPlayerAchievementsCompleteCallbackInternal(AchievementsInterface.OnQueryPlayerAchievementsComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryPlayerAchievementsCompleteCallbackInternal, Array.Empty<Delegate>());
			AchievementsInterface.EOS_Achievements_QueryPlayerAchievements(base.InnerHandle, ref queryPlayerAchievementsOptionsInternal, zero, onQueryPlayerAchievementsCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryPlayerAchievementsOptionsInternal>(ref queryPlayerAchievementsOptionsInternal);
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x00098C2C File Offset: 0x00096E2C
		public uint GetPlayerAchievementCount(GetPlayerAchievementCountOptions options)
		{
			GetPlayerAchievementCountOptionsInternal getPlayerAchievementCountOptionsInternal = Helper.CopyProperties<GetPlayerAchievementCountOptionsInternal>(options);
			uint num = AchievementsInterface.EOS_Achievements_GetPlayerAchievementCount(base.InnerHandle, ref getPlayerAchievementCountOptionsInternal);
			Helper.TryMarshalDispose<GetPlayerAchievementCountOptionsInternal>(ref getPlayerAchievementCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x00098C64 File Offset: 0x00096E64
		public Result CopyPlayerAchievementByIndex(CopyPlayerAchievementByIndexOptions options, out PlayerAchievement outAchievement)
		{
			CopyPlayerAchievementByIndexOptionsInternal copyPlayerAchievementByIndexOptionsInternal = Helper.CopyProperties<CopyPlayerAchievementByIndexOptionsInternal>(options);
			outAchievement = Helper.GetDefault<PlayerAchievement>();
			IntPtr zero = IntPtr.Zero;
			Result result = AchievementsInterface.EOS_Achievements_CopyPlayerAchievementByIndex(base.InnerHandle, ref copyPlayerAchievementByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyPlayerAchievementByIndexOptionsInternal>(ref copyPlayerAchievementByIndexOptionsInternal);
			if (Helper.TryMarshalGet<PlayerAchievementInternal, PlayerAchievement>(zero, out outAchievement))
			{
				AchievementsInterface.EOS_Achievements_PlayerAchievement_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x00098CBC File Offset: 0x00096EBC
		public Result CopyPlayerAchievementByAchievementId(CopyPlayerAchievementByAchievementIdOptions options, out PlayerAchievement outAchievement)
		{
			CopyPlayerAchievementByAchievementIdOptionsInternal copyPlayerAchievementByAchievementIdOptionsInternal = Helper.CopyProperties<CopyPlayerAchievementByAchievementIdOptionsInternal>(options);
			outAchievement = Helper.GetDefault<PlayerAchievement>();
			IntPtr zero = IntPtr.Zero;
			Result result = AchievementsInterface.EOS_Achievements_CopyPlayerAchievementByAchievementId(base.InnerHandle, ref copyPlayerAchievementByAchievementIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyPlayerAchievementByAchievementIdOptionsInternal>(ref copyPlayerAchievementByAchievementIdOptionsInternal);
			if (Helper.TryMarshalGet<PlayerAchievementInternal, PlayerAchievement>(zero, out outAchievement))
			{
				AchievementsInterface.EOS_Achievements_PlayerAchievement_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x00098D14 File Offset: 0x00096F14
		public void UnlockAchievements(UnlockAchievementsOptions options, object clientData, OnUnlockAchievementsCompleteCallback completionDelegate)
		{
			UnlockAchievementsOptionsInternal unlockAchievementsOptionsInternal = Helper.CopyProperties<UnlockAchievementsOptionsInternal>(options);
			OnUnlockAchievementsCompleteCallbackInternal onUnlockAchievementsCompleteCallbackInternal = new OnUnlockAchievementsCompleteCallbackInternal(AchievementsInterface.OnUnlockAchievementsComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onUnlockAchievementsCompleteCallbackInternal, Array.Empty<Delegate>());
			AchievementsInterface.EOS_Achievements_UnlockAchievements(base.InnerHandle, ref unlockAchievementsOptionsInternal, zero, onUnlockAchievementsCompleteCallbackInternal);
			Helper.TryMarshalDispose<UnlockAchievementsOptionsInternal>(ref unlockAchievementsOptionsInternal);
		}

		// Token: 0x060050CE RID: 20686 RVA: 0x00098D64 File Offset: 0x00096F64
		public ulong AddNotifyAchievementsUnlockedV2(AddNotifyAchievementsUnlockedV2Options options, object clientData, OnAchievementsUnlockedCallbackV2 notificationFn)
		{
			AddNotifyAchievementsUnlockedV2OptionsInternal addNotifyAchievementsUnlockedV2OptionsInternal = Helper.CopyProperties<AddNotifyAchievementsUnlockedV2OptionsInternal>(options);
			OnAchievementsUnlockedCallbackV2Internal onAchievementsUnlockedCallbackV2Internal = new OnAchievementsUnlockedCallbackV2Internal(AchievementsInterface.OnAchievementsUnlockedV2);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onAchievementsUnlockedCallbackV2Internal, Array.Empty<Delegate>());
			ulong num = AchievementsInterface.EOS_Achievements_AddNotifyAchievementsUnlockedV2(base.InnerHandle, ref addNotifyAchievementsUnlockedV2OptionsInternal, zero, onAchievementsUnlockedCallbackV2Internal);
			Helper.TryMarshalDispose<AddNotifyAchievementsUnlockedV2OptionsInternal>(ref addNotifyAchievementsUnlockedV2OptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x060050CF RID: 20687 RVA: 0x00098DCC File Offset: 0x00096FCC
		public void RemoveNotifyAchievementsUnlocked(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			AchievementsInterface.EOS_Achievements_RemoveNotifyAchievementsUnlocked(base.InnerHandle, inId);
		}

		// Token: 0x060050D0 RID: 20688 RVA: 0x00098DE4 File Offset: 0x00096FE4
		public Result CopyAchievementDefinitionByIndex(CopyAchievementDefinitionByIndexOptions options, out Definition outDefinition)
		{
			CopyAchievementDefinitionByIndexOptionsInternal copyAchievementDefinitionByIndexOptionsInternal = Helper.CopyProperties<CopyAchievementDefinitionByIndexOptionsInternal>(options);
			outDefinition = Helper.GetDefault<Definition>();
			IntPtr zero = IntPtr.Zero;
			Result result = AchievementsInterface.EOS_Achievements_CopyAchievementDefinitionByIndex(base.InnerHandle, ref copyAchievementDefinitionByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyAchievementDefinitionByIndexOptionsInternal>(ref copyAchievementDefinitionByIndexOptionsInternal);
			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(zero, out outDefinition))
			{
				AchievementsInterface.EOS_Achievements_Definition_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x00098E3C File Offset: 0x0009703C
		public Result CopyAchievementDefinitionByAchievementId(CopyAchievementDefinitionByAchievementIdOptions options, out Definition outDefinition)
		{
			CopyAchievementDefinitionByAchievementIdOptionsInternal copyAchievementDefinitionByAchievementIdOptionsInternal = Helper.CopyProperties<CopyAchievementDefinitionByAchievementIdOptionsInternal>(options);
			outDefinition = Helper.GetDefault<Definition>();
			IntPtr zero = IntPtr.Zero;
			Result result = AchievementsInterface.EOS_Achievements_CopyAchievementDefinitionByAchievementId(base.InnerHandle, ref copyAchievementDefinitionByAchievementIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyAchievementDefinitionByAchievementIdOptionsInternal>(ref copyAchievementDefinitionByAchievementIdOptionsInternal);
			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(zero, out outDefinition))
			{
				AchievementsInterface.EOS_Achievements_Definition_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x00098E94 File Offset: 0x00097094
		public uint GetUnlockedAchievementCount(GetUnlockedAchievementCountOptions options)
		{
			GetUnlockedAchievementCountOptionsInternal getUnlockedAchievementCountOptionsInternal = Helper.CopyProperties<GetUnlockedAchievementCountOptionsInternal>(options);
			uint num = AchievementsInterface.EOS_Achievements_GetUnlockedAchievementCount(base.InnerHandle, ref getUnlockedAchievementCountOptionsInternal);
			Helper.TryMarshalDispose<GetUnlockedAchievementCountOptionsInternal>(ref getUnlockedAchievementCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x00098ECC File Offset: 0x000970CC
		public Result CopyUnlockedAchievementByIndex(CopyUnlockedAchievementByIndexOptions options, out UnlockedAchievement outAchievement)
		{
			CopyUnlockedAchievementByIndexOptionsInternal copyUnlockedAchievementByIndexOptionsInternal = Helper.CopyProperties<CopyUnlockedAchievementByIndexOptionsInternal>(options);
			outAchievement = Helper.GetDefault<UnlockedAchievement>();
			IntPtr zero = IntPtr.Zero;
			Result result = AchievementsInterface.EOS_Achievements_CopyUnlockedAchievementByIndex(base.InnerHandle, ref copyUnlockedAchievementByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyUnlockedAchievementByIndexOptionsInternal>(ref copyUnlockedAchievementByIndexOptionsInternal);
			if (Helper.TryMarshalGet<UnlockedAchievementInternal, UnlockedAchievement>(zero, out outAchievement))
			{
				AchievementsInterface.EOS_Achievements_UnlockedAchievement_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x00098F24 File Offset: 0x00097124
		public Result CopyUnlockedAchievementByAchievementId(CopyUnlockedAchievementByAchievementIdOptions options, out UnlockedAchievement outAchievement)
		{
			CopyUnlockedAchievementByAchievementIdOptionsInternal copyUnlockedAchievementByAchievementIdOptionsInternal = Helper.CopyProperties<CopyUnlockedAchievementByAchievementIdOptionsInternal>(options);
			outAchievement = Helper.GetDefault<UnlockedAchievement>();
			IntPtr zero = IntPtr.Zero;
			Result result = AchievementsInterface.EOS_Achievements_CopyUnlockedAchievementByAchievementId(base.InnerHandle, ref copyUnlockedAchievementByAchievementIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyUnlockedAchievementByAchievementIdOptionsInternal>(ref copyUnlockedAchievementByAchievementIdOptionsInternal);
			if (Helper.TryMarshalGet<UnlockedAchievementInternal, UnlockedAchievement>(zero, out outAchievement))
			{
				AchievementsInterface.EOS_Achievements_UnlockedAchievement_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060050D5 RID: 20693 RVA: 0x00098F7C File Offset: 0x0009717C
		public ulong AddNotifyAchievementsUnlocked(AddNotifyAchievementsUnlockedOptions options, object clientData, OnAchievementsUnlockedCallback notificationFn)
		{
			AddNotifyAchievementsUnlockedOptionsInternal addNotifyAchievementsUnlockedOptionsInternal = Helper.CopyProperties<AddNotifyAchievementsUnlockedOptionsInternal>(options);
			OnAchievementsUnlockedCallbackInternal onAchievementsUnlockedCallbackInternal = new OnAchievementsUnlockedCallbackInternal(AchievementsInterface.OnAchievementsUnlocked);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notificationFn, onAchievementsUnlockedCallbackInternal, Array.Empty<Delegate>());
			ulong num = AchievementsInterface.EOS_Achievements_AddNotifyAchievementsUnlocked(base.InnerHandle, ref addNotifyAchievementsUnlockedOptionsInternal, zero, onAchievementsUnlockedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyAchievementsUnlockedOptionsInternal>(ref addNotifyAchievementsUnlockedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x00098FE4 File Offset: 0x000971E4
		[MonoPInvokeCallback]
		internal static void OnAchievementsUnlocked(IntPtr address)
		{
			OnAchievementsUnlockedCallback onAchievementsUnlockedCallback = null;
			OnAchievementsUnlockedCallbackInfo onAchievementsUnlockedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnAchievementsUnlockedCallback, OnAchievementsUnlockedCallbackInfoInternal, OnAchievementsUnlockedCallbackInfo>(address, out onAchievementsUnlockedCallback, out onAchievementsUnlockedCallbackInfo))
			{
				onAchievementsUnlockedCallback(onAchievementsUnlockedCallbackInfo);
			}
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x00099008 File Offset: 0x00097208
		[MonoPInvokeCallback]
		internal static void OnAchievementsUnlockedV2(IntPtr address)
		{
			OnAchievementsUnlockedCallbackV2 onAchievementsUnlockedCallbackV = null;
			OnAchievementsUnlockedCallbackV2Info onAchievementsUnlockedCallbackV2Info = null;
			if (Helper.TryGetAndRemoveCallback<OnAchievementsUnlockedCallbackV2, OnAchievementsUnlockedCallbackV2InfoInternal, OnAchievementsUnlockedCallbackV2Info>(address, out onAchievementsUnlockedCallbackV, out onAchievementsUnlockedCallbackV2Info))
			{
				onAchievementsUnlockedCallbackV(onAchievementsUnlockedCallbackV2Info);
			}
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x0009902C File Offset: 0x0009722C
		[MonoPInvokeCallback]
		internal static void OnUnlockAchievementsComplete(IntPtr address)
		{
			OnUnlockAchievementsCompleteCallback onUnlockAchievementsCompleteCallback = null;
			OnUnlockAchievementsCompleteCallbackInfo onUnlockAchievementsCompleteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnUnlockAchievementsCompleteCallback, OnUnlockAchievementsCompleteCallbackInfoInternal, OnUnlockAchievementsCompleteCallbackInfo>(address, out onUnlockAchievementsCompleteCallback, out onUnlockAchievementsCompleteCallbackInfo))
			{
				onUnlockAchievementsCompleteCallback(onUnlockAchievementsCompleteCallbackInfo);
			}
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x00099050 File Offset: 0x00097250
		[MonoPInvokeCallback]
		internal static void OnQueryPlayerAchievementsComplete(IntPtr address)
		{
			OnQueryPlayerAchievementsCompleteCallback onQueryPlayerAchievementsCompleteCallback = null;
			OnQueryPlayerAchievementsCompleteCallbackInfo onQueryPlayerAchievementsCompleteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryPlayerAchievementsCompleteCallback, OnQueryPlayerAchievementsCompleteCallbackInfoInternal, OnQueryPlayerAchievementsCompleteCallbackInfo>(address, out onQueryPlayerAchievementsCompleteCallback, out onQueryPlayerAchievementsCompleteCallbackInfo))
			{
				onQueryPlayerAchievementsCompleteCallback(onQueryPlayerAchievementsCompleteCallbackInfo);
			}
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x00099074 File Offset: 0x00097274
		[MonoPInvokeCallback]
		internal static void OnQueryDefinitionsComplete(IntPtr address)
		{
			OnQueryDefinitionsCompleteCallback onQueryDefinitionsCompleteCallback = null;
			OnQueryDefinitionsCompleteCallbackInfo onQueryDefinitionsCompleteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryDefinitionsCompleteCallback, OnQueryDefinitionsCompleteCallbackInfoInternal, OnQueryDefinitionsCompleteCallbackInfo>(address, out onQueryDefinitionsCompleteCallback, out onQueryDefinitionsCompleteCallbackInfo))
			{
				onQueryDefinitionsCompleteCallback(onQueryDefinitionsCompleteCallbackInfo);
			}
		}

		// Token: 0x060050DB RID: 20699
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Achievements_UnlockedAchievement_Release(IntPtr achievement);

		// Token: 0x060050DC RID: 20700
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Achievements_Definition_Release(IntPtr achievementDefinition);

		// Token: 0x060050DD RID: 20701
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Achievements_PlayerAchievement_Release(IntPtr achievement);

		// Token: 0x060050DE RID: 20702
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Achievements_DefinitionV2_Release(IntPtr achievementDefinition);

		// Token: 0x060050DF RID: 20703
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Achievements_AddNotifyAchievementsUnlocked(IntPtr handle, ref AddNotifyAchievementsUnlockedOptionsInternal options, IntPtr clientData, OnAchievementsUnlockedCallbackInternal notificationFn);

		// Token: 0x060050E0 RID: 20704
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Achievements_CopyUnlockedAchievementByAchievementId(IntPtr handle, ref CopyUnlockedAchievementByAchievementIdOptionsInternal options, ref IntPtr outAchievement);

		// Token: 0x060050E1 RID: 20705
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Achievements_CopyUnlockedAchievementByIndex(IntPtr handle, ref CopyUnlockedAchievementByIndexOptionsInternal options, ref IntPtr outAchievement);

		// Token: 0x060050E2 RID: 20706
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Achievements_GetUnlockedAchievementCount(IntPtr handle, ref GetUnlockedAchievementCountOptionsInternal options);

		// Token: 0x060050E3 RID: 20707
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Achievements_CopyAchievementDefinitionByAchievementId(IntPtr handle, ref CopyAchievementDefinitionByAchievementIdOptionsInternal options, ref IntPtr outDefinition);

		// Token: 0x060050E4 RID: 20708
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Achievements_CopyAchievementDefinitionByIndex(IntPtr handle, ref CopyAchievementDefinitionByIndexOptionsInternal options, ref IntPtr outDefinition);

		// Token: 0x060050E5 RID: 20709
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Achievements_RemoveNotifyAchievementsUnlocked(IntPtr handle, ulong inId);

		// Token: 0x060050E6 RID: 20710
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Achievements_AddNotifyAchievementsUnlockedV2(IntPtr handle, ref AddNotifyAchievementsUnlockedV2OptionsInternal options, IntPtr clientData, OnAchievementsUnlockedCallbackV2Internal notificationFn);

		// Token: 0x060050E7 RID: 20711
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Achievements_UnlockAchievements(IntPtr handle, ref UnlockAchievementsOptionsInternal options, IntPtr clientData, OnUnlockAchievementsCompleteCallbackInternal completionDelegate);

		// Token: 0x060050E8 RID: 20712
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Achievements_CopyPlayerAchievementByAchievementId(IntPtr handle, ref CopyPlayerAchievementByAchievementIdOptionsInternal options, ref IntPtr outAchievement);

		// Token: 0x060050E9 RID: 20713
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Achievements_CopyPlayerAchievementByIndex(IntPtr handle, ref CopyPlayerAchievementByIndexOptionsInternal options, ref IntPtr outAchievement);

		// Token: 0x060050EA RID: 20714
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Achievements_GetPlayerAchievementCount(IntPtr handle, ref GetPlayerAchievementCountOptionsInternal options);

		// Token: 0x060050EB RID: 20715
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Achievements_QueryPlayerAchievements(IntPtr handle, ref QueryPlayerAchievementsOptionsInternal options, IntPtr clientData, OnQueryPlayerAchievementsCompleteCallbackInternal completionDelegate);

		// Token: 0x060050EC RID: 20716
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Achievements_CopyAchievementDefinitionV2ByAchievementId(IntPtr handle, ref CopyAchievementDefinitionV2ByAchievementIdOptionsInternal options, ref IntPtr outDefinition);

		// Token: 0x060050ED RID: 20717
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Achievements_CopyAchievementDefinitionV2ByIndex(IntPtr handle, ref CopyAchievementDefinitionV2ByIndexOptionsInternal options, ref IntPtr outDefinition);

		// Token: 0x060050EE RID: 20718
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Achievements_GetAchievementDefinitionCount(IntPtr handle, ref GetAchievementDefinitionCountOptionsInternal options);

		// Token: 0x060050EF RID: 20719
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Achievements_QueryDefinitions(IntPtr handle, ref QueryDefinitionsOptionsInternal options, IntPtr clientData, OnQueryDefinitionsCompleteCallbackInternal completionDelegate);

		// Token: 0x04001F63 RID: 8035
		public const int AddnotifyachievementsunlockedApiLatest = 1;

		// Token: 0x04001F64 RID: 8036
		public const int CopyunlockedachievementbyachievementidApiLatest = 1;

		// Token: 0x04001F65 RID: 8037
		public const int CopyunlockedachievementbyindexApiLatest = 1;

		// Token: 0x04001F66 RID: 8038
		public const int GetunlockedachievementcountApiLatest = 1;

		// Token: 0x04001F67 RID: 8039
		public const int UnlockedachievementApiLatest = 1;

		// Token: 0x04001F68 RID: 8040
		public const int CopydefinitionbyachievementidApiLatest = 1;

		// Token: 0x04001F69 RID: 8041
		public const int CopydefinitionbyindexApiLatest = 1;

		// Token: 0x04001F6A RID: 8042
		public const int DefinitionApiLatest = 1;

		// Token: 0x04001F6B RID: 8043
		public const int Addnotifyachievementsunlockedv2ApiLatest = 2;

		// Token: 0x04001F6C RID: 8044
		public const int UnlockachievementsApiLatest = 1;

		// Token: 0x04001F6D RID: 8045
		public const int CopyplayerachievementbyachievementidApiLatest = 1;

		// Token: 0x04001F6E RID: 8046
		public const int CopyplayerachievementbyindexApiLatest = 1;

		// Token: 0x04001F6F RID: 8047
		public const int GetplayerachievementcountApiLatest = 1;

		// Token: 0x04001F70 RID: 8048
		public const int PlayerachievementApiLatest = 2;

		// Token: 0x04001F71 RID: 8049
		public const int AchievementUnlocktimeUndefined = -1;

		// Token: 0x04001F72 RID: 8050
		public const int QueryplayerachievementsApiLatest = 1;

		// Token: 0x04001F73 RID: 8051
		public const int Copydefinitionv2ByachievementidApiLatest = 2;

		// Token: 0x04001F74 RID: 8052
		public const int Copyachievementdefinitionv2ByachievementidApiLatest = 2;

		// Token: 0x04001F75 RID: 8053
		public const int Copydefinitionv2ByindexApiLatest = 2;

		// Token: 0x04001F76 RID: 8054
		public const int Copyachievementdefinitionv2ByindexApiLatest = 2;

		// Token: 0x04001F77 RID: 8055
		public const int GetachievementdefinitioncountApiLatest = 1;

		// Token: 0x04001F78 RID: 8056
		public const int Definitionv2ApiLatest = 2;

		// Token: 0x04001F79 RID: 8057
		public const int PlayerstatinfoApiLatest = 1;

		// Token: 0x04001F7A RID: 8058
		public const int StatthresholdApiLatest = 1;

		// Token: 0x04001F7B RID: 8059
		public const int StatthresholdsApiLatest = 1;

		// Token: 0x04001F7C RID: 8060
		public const int QuerydefinitionsApiLatest = 2;
	}
}
