using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000640 RID: 1600
	public sealed class SessionSearch : Handle
	{
		// Token: 0x06003EB1 RID: 16049 RVA: 0x000865B3 File Offset: 0x000847B3
		public SessionSearch(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003EB2 RID: 16050 RVA: 0x000865BC File Offset: 0x000847BC
		public Result SetSessionId(SessionSearchSetSessionIdOptions options)
		{
			SessionSearchSetSessionIdOptionsInternal sessionSearchSetSessionIdOptionsInternal = Helper.CopyProperties<SessionSearchSetSessionIdOptionsInternal>(options);
			Result result = SessionSearch.EOS_SessionSearch_SetSessionId(base.InnerHandle, ref sessionSearchSetSessionIdOptionsInternal);
			Helper.TryMarshalDispose<SessionSearchSetSessionIdOptionsInternal>(ref sessionSearchSetSessionIdOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000865F4 File Offset: 0x000847F4
		public Result SetTargetUserId(SessionSearchSetTargetUserIdOptions options)
		{
			SessionSearchSetTargetUserIdOptionsInternal sessionSearchSetTargetUserIdOptionsInternal = Helper.CopyProperties<SessionSearchSetTargetUserIdOptionsInternal>(options);
			Result result = SessionSearch.EOS_SessionSearch_SetTargetUserId(base.InnerHandle, ref sessionSearchSetTargetUserIdOptionsInternal);
			Helper.TryMarshalDispose<SessionSearchSetTargetUserIdOptionsInternal>(ref sessionSearchSetTargetUserIdOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x0008662C File Offset: 0x0008482C
		public Result SetParameter(SessionSearchSetParameterOptions options)
		{
			SessionSearchSetParameterOptionsInternal sessionSearchSetParameterOptionsInternal = Helper.CopyProperties<SessionSearchSetParameterOptionsInternal>(options);
			Result result = SessionSearch.EOS_SessionSearch_SetParameter(base.InnerHandle, ref sessionSearchSetParameterOptionsInternal);
			Helper.TryMarshalDispose<SessionSearchSetParameterOptionsInternal>(ref sessionSearchSetParameterOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x00086664 File Offset: 0x00084864
		public Result RemoveParameter(SessionSearchRemoveParameterOptions options)
		{
			SessionSearchRemoveParameterOptionsInternal sessionSearchRemoveParameterOptionsInternal = Helper.CopyProperties<SessionSearchRemoveParameterOptionsInternal>(options);
			Result result = SessionSearch.EOS_SessionSearch_RemoveParameter(base.InnerHandle, ref sessionSearchRemoveParameterOptionsInternal);
			Helper.TryMarshalDispose<SessionSearchRemoveParameterOptionsInternal>(ref sessionSearchRemoveParameterOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x0008669C File Offset: 0x0008489C
		public Result SetMaxResults(SessionSearchSetMaxResultsOptions options)
		{
			SessionSearchSetMaxResultsOptionsInternal sessionSearchSetMaxResultsOptionsInternal = Helper.CopyProperties<SessionSearchSetMaxResultsOptionsInternal>(options);
			Result result = SessionSearch.EOS_SessionSearch_SetMaxResults(base.InnerHandle, ref sessionSearchSetMaxResultsOptionsInternal);
			Helper.TryMarshalDispose<SessionSearchSetMaxResultsOptionsInternal>(ref sessionSearchSetMaxResultsOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x000866D4 File Offset: 0x000848D4
		public void Find(SessionSearchFindOptions options, object clientData, SessionSearchOnFindCallback completionDelegate)
		{
			SessionSearchFindOptionsInternal sessionSearchFindOptionsInternal = Helper.CopyProperties<SessionSearchFindOptionsInternal>(options);
			SessionSearchOnFindCallbackInternal sessionSearchOnFindCallbackInternal = new SessionSearchOnFindCallbackInternal(SessionSearch.SessionSearchOnFind);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, sessionSearchOnFindCallbackInternal, Array.Empty<Delegate>());
			SessionSearch.EOS_SessionSearch_Find(base.InnerHandle, ref sessionSearchFindOptionsInternal, zero, sessionSearchOnFindCallbackInternal);
			Helper.TryMarshalDispose<SessionSearchFindOptionsInternal>(ref sessionSearchFindOptionsInternal);
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x00086724 File Offset: 0x00084924
		public uint GetSearchResultCount(SessionSearchGetSearchResultCountOptions options)
		{
			SessionSearchGetSearchResultCountOptionsInternal sessionSearchGetSearchResultCountOptionsInternal = Helper.CopyProperties<SessionSearchGetSearchResultCountOptionsInternal>(options);
			uint num = SessionSearch.EOS_SessionSearch_GetSearchResultCount(base.InnerHandle, ref sessionSearchGetSearchResultCountOptionsInternal);
			Helper.TryMarshalDispose<SessionSearchGetSearchResultCountOptionsInternal>(ref sessionSearchGetSearchResultCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06003EB9 RID: 16057 RVA: 0x0008675C File Offset: 0x0008495C
		public Result CopySearchResultByIndex(SessionSearchCopySearchResultByIndexOptions options, out SessionDetails outSessionHandle)
		{
			SessionSearchCopySearchResultByIndexOptionsInternal sessionSearchCopySearchResultByIndexOptionsInternal = Helper.CopyProperties<SessionSearchCopySearchResultByIndexOptionsInternal>(options);
			outSessionHandle = Helper.GetDefault<SessionDetails>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionSearch.EOS_SessionSearch_CopySearchResultByIndex(base.InnerHandle, ref sessionSearchCopySearchResultByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<SessionSearchCopySearchResultByIndexOptionsInternal>(ref sessionSearchCopySearchResultByIndexOptionsInternal);
			Helper.TryMarshalGet<SessionDetails>(zero, out outSessionHandle);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x000867AB File Offset: 0x000849AB
		public void Release()
		{
			SessionSearch.EOS_SessionSearch_Release(base.InnerHandle);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x000867B8 File Offset: 0x000849B8
		[MonoPInvokeCallback]
		internal static void SessionSearchOnFind(IntPtr address)
		{
			SessionSearchOnFindCallback sessionSearchOnFindCallback = null;
			SessionSearchFindCallbackInfo sessionSearchFindCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<SessionSearchOnFindCallback, SessionSearchFindCallbackInfoInternal, SessionSearchFindCallbackInfo>(address, out sessionSearchOnFindCallback, out sessionSearchFindCallbackInfo))
			{
				sessionSearchOnFindCallback(sessionSearchFindCallbackInfo);
			}
		}

		// Token: 0x06003EBC RID: 16060
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_SessionSearch_Release(IntPtr sessionSearchHandle);

		// Token: 0x06003EBD RID: 16061
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionSearch_CopySearchResultByIndex(IntPtr handle, ref SessionSearchCopySearchResultByIndexOptionsInternal options, ref IntPtr outSessionHandle);

		// Token: 0x06003EBE RID: 16062
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_SessionSearch_GetSearchResultCount(IntPtr handle, ref SessionSearchGetSearchResultCountOptionsInternal options);

		// Token: 0x06003EBF RID: 16063
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_SessionSearch_Find(IntPtr handle, ref SessionSearchFindOptionsInternal options, IntPtr clientData, SessionSearchOnFindCallbackInternal completionDelegate);

		// Token: 0x06003EC0 RID: 16064
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionSearch_SetMaxResults(IntPtr handle, ref SessionSearchSetMaxResultsOptionsInternal options);

		// Token: 0x06003EC1 RID: 16065
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionSearch_RemoveParameter(IntPtr handle, ref SessionSearchRemoveParameterOptionsInternal options);

		// Token: 0x06003EC2 RID: 16066
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionSearch_SetParameter(IntPtr handle, ref SessionSearchSetParameterOptionsInternal options);

		// Token: 0x06003EC3 RID: 16067
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionSearch_SetTargetUserId(IntPtr handle, ref SessionSearchSetTargetUserIdOptionsInternal options);

		// Token: 0x06003EC4 RID: 16068
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionSearch_SetSessionId(IntPtr handle, ref SessionSearchSetSessionIdOptionsInternal options);
	}
}
