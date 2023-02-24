using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020007E9 RID: 2025
	public sealed class LeaderboardsInterface : Handle
	{
		// Token: 0x0600490C RID: 18700 RVA: 0x0009102F File Offset: 0x0008F22F
		public LeaderboardsInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x00091038 File Offset: 0x0008F238
		public void QueryLeaderboardDefinitions(QueryLeaderboardDefinitionsOptions options, object clientData, OnQueryLeaderboardDefinitionsCompleteCallback completionDelegate)
		{
			QueryLeaderboardDefinitionsOptionsInternal queryLeaderboardDefinitionsOptionsInternal = Helper.CopyProperties<QueryLeaderboardDefinitionsOptionsInternal>(options);
			OnQueryLeaderboardDefinitionsCompleteCallbackInternal onQueryLeaderboardDefinitionsCompleteCallbackInternal = new OnQueryLeaderboardDefinitionsCompleteCallbackInternal(LeaderboardsInterface.OnQueryLeaderboardDefinitionsComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryLeaderboardDefinitionsCompleteCallbackInternal, Array.Empty<Delegate>());
			LeaderboardsInterface.EOS_Leaderboards_QueryLeaderboardDefinitions(base.InnerHandle, ref queryLeaderboardDefinitionsOptionsInternal, zero, onQueryLeaderboardDefinitionsCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryLeaderboardDefinitionsOptionsInternal>(ref queryLeaderboardDefinitionsOptionsInternal);
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x00091088 File Offset: 0x0008F288
		public uint GetLeaderboardDefinitionCount(GetLeaderboardDefinitionCountOptions options)
		{
			GetLeaderboardDefinitionCountOptionsInternal getLeaderboardDefinitionCountOptionsInternal = Helper.CopyProperties<GetLeaderboardDefinitionCountOptionsInternal>(options);
			uint num = LeaderboardsInterface.EOS_Leaderboards_GetLeaderboardDefinitionCount(base.InnerHandle, ref getLeaderboardDefinitionCountOptionsInternal);
			Helper.TryMarshalDispose<GetLeaderboardDefinitionCountOptionsInternal>(ref getLeaderboardDefinitionCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x000910C0 File Offset: 0x0008F2C0
		public Result CopyLeaderboardDefinitionByIndex(CopyLeaderboardDefinitionByIndexOptions options, out Definition outLeaderboardDefinition)
		{
			CopyLeaderboardDefinitionByIndexOptionsInternal copyLeaderboardDefinitionByIndexOptionsInternal = Helper.CopyProperties<CopyLeaderboardDefinitionByIndexOptionsInternal>(options);
			outLeaderboardDefinition = Helper.GetDefault<Definition>();
			IntPtr zero = IntPtr.Zero;
			Result result = LeaderboardsInterface.EOS_Leaderboards_CopyLeaderboardDefinitionByIndex(base.InnerHandle, ref copyLeaderboardDefinitionByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyLeaderboardDefinitionByIndexOptionsInternal>(ref copyLeaderboardDefinitionByIndexOptionsInternal);
			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(zero, out outLeaderboardDefinition))
			{
				LeaderboardsInterface.EOS_Leaderboards_LeaderboardDefinition_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x00091118 File Offset: 0x0008F318
		public Result CopyLeaderboardDefinitionByLeaderboardId(CopyLeaderboardDefinitionByLeaderboardIdOptions options, out Definition outLeaderboardDefinition)
		{
			CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal copyLeaderboardDefinitionByLeaderboardIdOptionsInternal = Helper.CopyProperties<CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal>(options);
			outLeaderboardDefinition = Helper.GetDefault<Definition>();
			IntPtr zero = IntPtr.Zero;
			Result result = LeaderboardsInterface.EOS_Leaderboards_CopyLeaderboardDefinitionByLeaderboardId(base.InnerHandle, ref copyLeaderboardDefinitionByLeaderboardIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal>(ref copyLeaderboardDefinitionByLeaderboardIdOptionsInternal);
			if (Helper.TryMarshalGet<DefinitionInternal, Definition>(zero, out outLeaderboardDefinition))
			{
				LeaderboardsInterface.EOS_Leaderboards_LeaderboardDefinition_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x00091170 File Offset: 0x0008F370
		public void QueryLeaderboardRanks(QueryLeaderboardRanksOptions options, object clientData, OnQueryLeaderboardRanksCompleteCallback completionDelegate)
		{
			QueryLeaderboardRanksOptionsInternal queryLeaderboardRanksOptionsInternal = Helper.CopyProperties<QueryLeaderboardRanksOptionsInternal>(options);
			OnQueryLeaderboardRanksCompleteCallbackInternal onQueryLeaderboardRanksCompleteCallbackInternal = new OnQueryLeaderboardRanksCompleteCallbackInternal(LeaderboardsInterface.OnQueryLeaderboardRanksComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryLeaderboardRanksCompleteCallbackInternal, Array.Empty<Delegate>());
			LeaderboardsInterface.EOS_Leaderboards_QueryLeaderboardRanks(base.InnerHandle, ref queryLeaderboardRanksOptionsInternal, zero, onQueryLeaderboardRanksCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryLeaderboardRanksOptionsInternal>(ref queryLeaderboardRanksOptionsInternal);
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x000911C0 File Offset: 0x0008F3C0
		public uint GetLeaderboardRecordCount(GetLeaderboardRecordCountOptions options)
		{
			GetLeaderboardRecordCountOptionsInternal getLeaderboardRecordCountOptionsInternal = Helper.CopyProperties<GetLeaderboardRecordCountOptionsInternal>(options);
			uint num = LeaderboardsInterface.EOS_Leaderboards_GetLeaderboardRecordCount(base.InnerHandle, ref getLeaderboardRecordCountOptionsInternal);
			Helper.TryMarshalDispose<GetLeaderboardRecordCountOptionsInternal>(ref getLeaderboardRecordCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x000911F8 File Offset: 0x0008F3F8
		public Result CopyLeaderboardRecordByIndex(CopyLeaderboardRecordByIndexOptions options, out LeaderboardRecord outLeaderboardRecord)
		{
			CopyLeaderboardRecordByIndexOptionsInternal copyLeaderboardRecordByIndexOptionsInternal = Helper.CopyProperties<CopyLeaderboardRecordByIndexOptionsInternal>(options);
			outLeaderboardRecord = Helper.GetDefault<LeaderboardRecord>();
			IntPtr zero = IntPtr.Zero;
			Result result = LeaderboardsInterface.EOS_Leaderboards_CopyLeaderboardRecordByIndex(base.InnerHandle, ref copyLeaderboardRecordByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyLeaderboardRecordByIndexOptionsInternal>(ref copyLeaderboardRecordByIndexOptionsInternal);
			if (Helper.TryMarshalGet<LeaderboardRecordInternal, LeaderboardRecord>(zero, out outLeaderboardRecord))
			{
				LeaderboardsInterface.EOS_Leaderboards_LeaderboardRecord_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x00091250 File Offset: 0x0008F450
		public Result CopyLeaderboardRecordByUserId(CopyLeaderboardRecordByUserIdOptions options, out LeaderboardRecord outLeaderboardRecord)
		{
			CopyLeaderboardRecordByUserIdOptionsInternal copyLeaderboardRecordByUserIdOptionsInternal = Helper.CopyProperties<CopyLeaderboardRecordByUserIdOptionsInternal>(options);
			outLeaderboardRecord = Helper.GetDefault<LeaderboardRecord>();
			IntPtr zero = IntPtr.Zero;
			Result result = LeaderboardsInterface.EOS_Leaderboards_CopyLeaderboardRecordByUserId(base.InnerHandle, ref copyLeaderboardRecordByUserIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyLeaderboardRecordByUserIdOptionsInternal>(ref copyLeaderboardRecordByUserIdOptionsInternal);
			if (Helper.TryMarshalGet<LeaderboardRecordInternal, LeaderboardRecord>(zero, out outLeaderboardRecord))
			{
				LeaderboardsInterface.EOS_Leaderboards_LeaderboardRecord_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x000912A8 File Offset: 0x0008F4A8
		public void QueryLeaderboardUserScores(QueryLeaderboardUserScoresOptions options, object clientData, OnQueryLeaderboardUserScoresCompleteCallback completionDelegate)
		{
			QueryLeaderboardUserScoresOptionsInternal queryLeaderboardUserScoresOptionsInternal = Helper.CopyProperties<QueryLeaderboardUserScoresOptionsInternal>(options);
			OnQueryLeaderboardUserScoresCompleteCallbackInternal onQueryLeaderboardUserScoresCompleteCallbackInternal = new OnQueryLeaderboardUserScoresCompleteCallbackInternal(LeaderboardsInterface.OnQueryLeaderboardUserScoresComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryLeaderboardUserScoresCompleteCallbackInternal, Array.Empty<Delegate>());
			LeaderboardsInterface.EOS_Leaderboards_QueryLeaderboardUserScores(base.InnerHandle, ref queryLeaderboardUserScoresOptionsInternal, zero, onQueryLeaderboardUserScoresCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryLeaderboardUserScoresOptionsInternal>(ref queryLeaderboardUserScoresOptionsInternal);
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x000912F8 File Offset: 0x0008F4F8
		public uint GetLeaderboardUserScoreCount(GetLeaderboardUserScoreCountOptions options)
		{
			GetLeaderboardUserScoreCountOptionsInternal getLeaderboardUserScoreCountOptionsInternal = Helper.CopyProperties<GetLeaderboardUserScoreCountOptionsInternal>(options);
			uint num = LeaderboardsInterface.EOS_Leaderboards_GetLeaderboardUserScoreCount(base.InnerHandle, ref getLeaderboardUserScoreCountOptionsInternal);
			Helper.TryMarshalDispose<GetLeaderboardUserScoreCountOptionsInternal>(ref getLeaderboardUserScoreCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x00091330 File Offset: 0x0008F530
		public Result CopyLeaderboardUserScoreByIndex(CopyLeaderboardUserScoreByIndexOptions options, out LeaderboardUserScore outLeaderboardUserScore)
		{
			CopyLeaderboardUserScoreByIndexOptionsInternal copyLeaderboardUserScoreByIndexOptionsInternal = Helper.CopyProperties<CopyLeaderboardUserScoreByIndexOptionsInternal>(options);
			outLeaderboardUserScore = Helper.GetDefault<LeaderboardUserScore>();
			IntPtr zero = IntPtr.Zero;
			Result result = LeaderboardsInterface.EOS_Leaderboards_CopyLeaderboardUserScoreByIndex(base.InnerHandle, ref copyLeaderboardUserScoreByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyLeaderboardUserScoreByIndexOptionsInternal>(ref copyLeaderboardUserScoreByIndexOptionsInternal);
			if (Helper.TryMarshalGet<LeaderboardUserScoreInternal, LeaderboardUserScore>(zero, out outLeaderboardUserScore))
			{
				LeaderboardsInterface.EOS_Leaderboards_LeaderboardUserScore_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x00091388 File Offset: 0x0008F588
		public Result CopyLeaderboardUserScoreByUserId(CopyLeaderboardUserScoreByUserIdOptions options, out LeaderboardUserScore outLeaderboardUserScore)
		{
			CopyLeaderboardUserScoreByUserIdOptionsInternal copyLeaderboardUserScoreByUserIdOptionsInternal = Helper.CopyProperties<CopyLeaderboardUserScoreByUserIdOptionsInternal>(options);
			outLeaderboardUserScore = Helper.GetDefault<LeaderboardUserScore>();
			IntPtr zero = IntPtr.Zero;
			Result result = LeaderboardsInterface.EOS_Leaderboards_CopyLeaderboardUserScoreByUserId(base.InnerHandle, ref copyLeaderboardUserScoreByUserIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyLeaderboardUserScoreByUserIdOptionsInternal>(ref copyLeaderboardUserScoreByUserIdOptionsInternal);
			if (Helper.TryMarshalGet<LeaderboardUserScoreInternal, LeaderboardUserScore>(zero, out outLeaderboardUserScore))
			{
				LeaderboardsInterface.EOS_Leaderboards_LeaderboardUserScore_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x000913E0 File Offset: 0x0008F5E0
		[MonoPInvokeCallback]
		internal static void OnQueryLeaderboardUserScoresComplete(IntPtr address)
		{
			OnQueryLeaderboardUserScoresCompleteCallback onQueryLeaderboardUserScoresCompleteCallback = null;
			OnQueryLeaderboardUserScoresCompleteCallbackInfo onQueryLeaderboardUserScoresCompleteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryLeaderboardUserScoresCompleteCallback, OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal, OnQueryLeaderboardUserScoresCompleteCallbackInfo>(address, out onQueryLeaderboardUserScoresCompleteCallback, out onQueryLeaderboardUserScoresCompleteCallbackInfo))
			{
				onQueryLeaderboardUserScoresCompleteCallback(onQueryLeaderboardUserScoresCompleteCallbackInfo);
			}
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x00091404 File Offset: 0x0008F604
		[MonoPInvokeCallback]
		internal static void OnQueryLeaderboardRanksComplete(IntPtr address)
		{
			OnQueryLeaderboardRanksCompleteCallback onQueryLeaderboardRanksCompleteCallback = null;
			OnQueryLeaderboardRanksCompleteCallbackInfo onQueryLeaderboardRanksCompleteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryLeaderboardRanksCompleteCallback, OnQueryLeaderboardRanksCompleteCallbackInfoInternal, OnQueryLeaderboardRanksCompleteCallbackInfo>(address, out onQueryLeaderboardRanksCompleteCallback, out onQueryLeaderboardRanksCompleteCallbackInfo))
			{
				onQueryLeaderboardRanksCompleteCallback(onQueryLeaderboardRanksCompleteCallbackInfo);
			}
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x00091428 File Offset: 0x0008F628
		[MonoPInvokeCallback]
		internal static void OnQueryLeaderboardDefinitionsComplete(IntPtr address)
		{
			OnQueryLeaderboardDefinitionsCompleteCallback onQueryLeaderboardDefinitionsCompleteCallback = null;
			OnQueryLeaderboardDefinitionsCompleteCallbackInfo onQueryLeaderboardDefinitionsCompleteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryLeaderboardDefinitionsCompleteCallback, OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal, OnQueryLeaderboardDefinitionsCompleteCallbackInfo>(address, out onQueryLeaderboardDefinitionsCompleteCallback, out onQueryLeaderboardDefinitionsCompleteCallbackInfo))
			{
				onQueryLeaderboardDefinitionsCompleteCallback(onQueryLeaderboardDefinitionsCompleteCallbackInfo);
			}
		}

		// Token: 0x0600491C RID: 18716
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Leaderboards_LeaderboardDefinition_Release(IntPtr leaderboardDefinition);

		// Token: 0x0600491D RID: 18717
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Leaderboards_LeaderboardRecord_Release(IntPtr leaderboardRecord);

		// Token: 0x0600491E RID: 18718
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Leaderboards_LeaderboardUserScore_Release(IntPtr leaderboardUserScore);

		// Token: 0x0600491F RID: 18719
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Leaderboards_Definition_Release(IntPtr leaderboardDefinition);

		// Token: 0x06004920 RID: 18720
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Leaderboards_CopyLeaderboardUserScoreByUserId(IntPtr handle, ref CopyLeaderboardUserScoreByUserIdOptionsInternal options, ref IntPtr outLeaderboardUserScore);

		// Token: 0x06004921 RID: 18721
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Leaderboards_CopyLeaderboardUserScoreByIndex(IntPtr handle, ref CopyLeaderboardUserScoreByIndexOptionsInternal options, ref IntPtr outLeaderboardUserScore);

		// Token: 0x06004922 RID: 18722
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Leaderboards_GetLeaderboardUserScoreCount(IntPtr handle, ref GetLeaderboardUserScoreCountOptionsInternal options);

		// Token: 0x06004923 RID: 18723
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Leaderboards_QueryLeaderboardUserScores(IntPtr handle, ref QueryLeaderboardUserScoresOptionsInternal options, IntPtr clientData, OnQueryLeaderboardUserScoresCompleteCallbackInternal completionDelegate);

		// Token: 0x06004924 RID: 18724
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Leaderboards_CopyLeaderboardRecordByUserId(IntPtr handle, ref CopyLeaderboardRecordByUserIdOptionsInternal options, ref IntPtr outLeaderboardRecord);

		// Token: 0x06004925 RID: 18725
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Leaderboards_CopyLeaderboardRecordByIndex(IntPtr handle, ref CopyLeaderboardRecordByIndexOptionsInternal options, ref IntPtr outLeaderboardRecord);

		// Token: 0x06004926 RID: 18726
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Leaderboards_GetLeaderboardRecordCount(IntPtr handle, ref GetLeaderboardRecordCountOptionsInternal options);

		// Token: 0x06004927 RID: 18727
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Leaderboards_QueryLeaderboardRanks(IntPtr handle, ref QueryLeaderboardRanksOptionsInternal options, IntPtr clientData, OnQueryLeaderboardRanksCompleteCallbackInternal completionDelegate);

		// Token: 0x06004928 RID: 18728
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Leaderboards_CopyLeaderboardDefinitionByLeaderboardId(IntPtr handle, ref CopyLeaderboardDefinitionByLeaderboardIdOptionsInternal options, ref IntPtr outLeaderboardDefinition);

		// Token: 0x06004929 RID: 18729
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Leaderboards_CopyLeaderboardDefinitionByIndex(IntPtr handle, ref CopyLeaderboardDefinitionByIndexOptionsInternal options, ref IntPtr outLeaderboardDefinition);

		// Token: 0x0600492A RID: 18730
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Leaderboards_GetLeaderboardDefinitionCount(IntPtr handle, ref GetLeaderboardDefinitionCountOptionsInternal options);

		// Token: 0x0600492B RID: 18731
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Leaderboards_QueryLeaderboardDefinitions(IntPtr handle, ref QueryLeaderboardDefinitionsOptionsInternal options, IntPtr clientData, OnQueryLeaderboardDefinitionsCompleteCallbackInternal completionDelegate);

		// Token: 0x04001C2D RID: 7213
		public const int CopyleaderboardrecordbyuseridApiLatest = 2;

		// Token: 0x04001C2E RID: 7214
		public const int CopyleaderboardrecordbyindexApiLatest = 2;

		// Token: 0x04001C2F RID: 7215
		public const int GetleaderboardrecordcountApiLatest = 1;

		// Token: 0x04001C30 RID: 7216
		public const int LeaderboardrecordApiLatest = 2;

		// Token: 0x04001C31 RID: 7217
		public const int QueryleaderboardranksApiLatest = 1;

		// Token: 0x04001C32 RID: 7218
		public const int CopyleaderboarduserscorebyuseridApiLatest = 1;

		// Token: 0x04001C33 RID: 7219
		public const int CopyleaderboarduserscorebyindexApiLatest = 1;

		// Token: 0x04001C34 RID: 7220
		public const int GetleaderboarduserscorecountApiLatest = 1;

		// Token: 0x04001C35 RID: 7221
		public const int LeaderboarduserscoreApiLatest = 1;

		// Token: 0x04001C36 RID: 7222
		public const int QueryleaderboarduserscoresApiLatest = 1;

		// Token: 0x04001C37 RID: 7223
		public const int UserscoresquerystatinfoApiLatest = 1;

		// Token: 0x04001C38 RID: 7224
		public const int CopyleaderboarddefinitionbyleaderboardidApiLatest = 1;

		// Token: 0x04001C39 RID: 7225
		public const int CopyleaderboarddefinitionbyindexApiLatest = 1;

		// Token: 0x04001C3A RID: 7226
		public const int GetleaderboarddefinitioncountApiLatest = 1;

		// Token: 0x04001C3B RID: 7227
		public const int DefinitionApiLatest = 1;

		// Token: 0x04001C3C RID: 7228
		public const int QueryleaderboarddefinitionsApiLatest = 1;

		// Token: 0x04001C3D RID: 7229
		public const int TimeUndefined = -1;
	}
}
