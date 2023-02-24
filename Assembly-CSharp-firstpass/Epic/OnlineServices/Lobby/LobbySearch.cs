using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000783 RID: 1923
	public sealed class LobbySearch : Handle
	{
		// Token: 0x06004703 RID: 18179 RVA: 0x0008F827 File Offset: 0x0008DA27
		public LobbySearch(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x0008F830 File Offset: 0x0008DA30
		public void Find(LobbySearchFindOptions options, object clientData, LobbySearchOnFindCallback completionDelegate)
		{
			LobbySearchFindOptionsInternal lobbySearchFindOptionsInternal = Helper.CopyProperties<LobbySearchFindOptionsInternal>(options);
			LobbySearchOnFindCallbackInternal lobbySearchOnFindCallbackInternal = new LobbySearchOnFindCallbackInternal(LobbySearch.LobbySearchOnFind);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, lobbySearchOnFindCallbackInternal, Array.Empty<Delegate>());
			LobbySearch.EOS_LobbySearch_Find(base.InnerHandle, ref lobbySearchFindOptionsInternal, zero, lobbySearchOnFindCallbackInternal);
			Helper.TryMarshalDispose<LobbySearchFindOptionsInternal>(ref lobbySearchFindOptionsInternal);
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x0008F880 File Offset: 0x0008DA80
		public Result SetLobbyId(LobbySearchSetLobbyIdOptions options)
		{
			LobbySearchSetLobbyIdOptionsInternal lobbySearchSetLobbyIdOptionsInternal = Helper.CopyProperties<LobbySearchSetLobbyIdOptionsInternal>(options);
			Result result = LobbySearch.EOS_LobbySearch_SetLobbyId(base.InnerHandle, ref lobbySearchSetLobbyIdOptionsInternal);
			Helper.TryMarshalDispose<LobbySearchSetLobbyIdOptionsInternal>(ref lobbySearchSetLobbyIdOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x0008F8B8 File Offset: 0x0008DAB8
		public Result SetTargetUserId(LobbySearchSetTargetUserIdOptions options)
		{
			LobbySearchSetTargetUserIdOptionsInternal lobbySearchSetTargetUserIdOptionsInternal = Helper.CopyProperties<LobbySearchSetTargetUserIdOptionsInternal>(options);
			Result result = LobbySearch.EOS_LobbySearch_SetTargetUserId(base.InnerHandle, ref lobbySearchSetTargetUserIdOptionsInternal);
			Helper.TryMarshalDispose<LobbySearchSetTargetUserIdOptionsInternal>(ref lobbySearchSetTargetUserIdOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x0008F8F0 File Offset: 0x0008DAF0
		public Result SetParameter(LobbySearchSetParameterOptions options)
		{
			LobbySearchSetParameterOptionsInternal lobbySearchSetParameterOptionsInternal = Helper.CopyProperties<LobbySearchSetParameterOptionsInternal>(options);
			Result result = LobbySearch.EOS_LobbySearch_SetParameter(base.InnerHandle, ref lobbySearchSetParameterOptionsInternal);
			Helper.TryMarshalDispose<LobbySearchSetParameterOptionsInternal>(ref lobbySearchSetParameterOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x0008F928 File Offset: 0x0008DB28
		public Result RemoveParameter(LobbySearchRemoveParameterOptions options)
		{
			LobbySearchRemoveParameterOptionsInternal lobbySearchRemoveParameterOptionsInternal = Helper.CopyProperties<LobbySearchRemoveParameterOptionsInternal>(options);
			Result result = LobbySearch.EOS_LobbySearch_RemoveParameter(base.InnerHandle, ref lobbySearchRemoveParameterOptionsInternal);
			Helper.TryMarshalDispose<LobbySearchRemoveParameterOptionsInternal>(ref lobbySearchRemoveParameterOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x0008F960 File Offset: 0x0008DB60
		public Result SetMaxResults(LobbySearchSetMaxResultsOptions options)
		{
			LobbySearchSetMaxResultsOptionsInternal lobbySearchSetMaxResultsOptionsInternal = Helper.CopyProperties<LobbySearchSetMaxResultsOptionsInternal>(options);
			Result result = LobbySearch.EOS_LobbySearch_SetMaxResults(base.InnerHandle, ref lobbySearchSetMaxResultsOptionsInternal);
			Helper.TryMarshalDispose<LobbySearchSetMaxResultsOptionsInternal>(ref lobbySearchSetMaxResultsOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x0008F998 File Offset: 0x0008DB98
		public uint GetSearchResultCount(LobbySearchGetSearchResultCountOptions options)
		{
			LobbySearchGetSearchResultCountOptionsInternal lobbySearchGetSearchResultCountOptionsInternal = Helper.CopyProperties<LobbySearchGetSearchResultCountOptionsInternal>(options);
			uint num = LobbySearch.EOS_LobbySearch_GetSearchResultCount(base.InnerHandle, ref lobbySearchGetSearchResultCountOptionsInternal);
			Helper.TryMarshalDispose<LobbySearchGetSearchResultCountOptionsInternal>(ref lobbySearchGetSearchResultCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x0600470B RID: 18187 RVA: 0x0008F9D0 File Offset: 0x0008DBD0
		public Result CopySearchResultByIndex(LobbySearchCopySearchResultByIndexOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			LobbySearchCopySearchResultByIndexOptionsInternal lobbySearchCopySearchResultByIndexOptionsInternal = Helper.CopyProperties<LobbySearchCopySearchResultByIndexOptionsInternal>(options);
			outLobbyDetailsHandle = Helper.GetDefault<LobbyDetails>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbySearch.EOS_LobbySearch_CopySearchResultByIndex(base.InnerHandle, ref lobbySearchCopySearchResultByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<LobbySearchCopySearchResultByIndexOptionsInternal>(ref lobbySearchCopySearchResultByIndexOptionsInternal);
			Helper.TryMarshalGet<LobbyDetails>(zero, out outLobbyDetailsHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x0008FA1F File Offset: 0x0008DC1F
		public void Release()
		{
			LobbySearch.EOS_LobbySearch_Release(base.InnerHandle);
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x0008FA2C File Offset: 0x0008DC2C
		[MonoPInvokeCallback]
		internal static void LobbySearchOnFind(IntPtr address)
		{
			LobbySearchOnFindCallback lobbySearchOnFindCallback = null;
			LobbySearchFindCallbackInfo lobbySearchFindCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<LobbySearchOnFindCallback, LobbySearchFindCallbackInfoInternal, LobbySearchFindCallbackInfo>(address, out lobbySearchOnFindCallback, out lobbySearchFindCallbackInfo))
			{
				lobbySearchOnFindCallback(lobbySearchFindCallbackInfo);
			}
		}

		// Token: 0x0600470E RID: 18190
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_LobbySearch_Release(IntPtr lobbySearchHandle);

		// Token: 0x0600470F RID: 18191
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbySearch_CopySearchResultByIndex(IntPtr handle, ref LobbySearchCopySearchResultByIndexOptionsInternal options, ref IntPtr outLobbyDetailsHandle);

		// Token: 0x06004710 RID: 18192
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_LobbySearch_GetSearchResultCount(IntPtr handle, ref LobbySearchGetSearchResultCountOptionsInternal options);

		// Token: 0x06004711 RID: 18193
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbySearch_SetMaxResults(IntPtr handle, ref LobbySearchSetMaxResultsOptionsInternal options);

		// Token: 0x06004712 RID: 18194
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbySearch_RemoveParameter(IntPtr handle, ref LobbySearchRemoveParameterOptionsInternal options);

		// Token: 0x06004713 RID: 18195
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbySearch_SetParameter(IntPtr handle, ref LobbySearchSetParameterOptionsInternal options);

		// Token: 0x06004714 RID: 18196
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbySearch_SetTargetUserId(IntPtr handle, ref LobbySearchSetTargetUserIdOptionsInternal options);

		// Token: 0x06004715 RID: 18197
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbySearch_SetLobbyId(IntPtr handle, ref LobbySearchSetLobbyIdOptionsInternal options);

		// Token: 0x06004716 RID: 18198
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_LobbySearch_Find(IntPtr handle, ref LobbySearchFindOptionsInternal options, IntPtr clientData, LobbySearchOnFindCallbackInternal completionDelegate);
	}
}
