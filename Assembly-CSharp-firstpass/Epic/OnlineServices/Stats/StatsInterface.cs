using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020005B9 RID: 1465
	public sealed class StatsInterface : Handle
	{
		// Token: 0x06003BDA RID: 15322 RVA: 0x00083C5B File Offset: 0x00081E5B
		public StatsInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003BDB RID: 15323 RVA: 0x00083C64 File Offset: 0x00081E64
		public void IngestStat(IngestStatOptions options, object clientData, OnIngestStatCompleteCallback completionDelegate)
		{
			IngestStatOptionsInternal ingestStatOptionsInternal = Helper.CopyProperties<IngestStatOptionsInternal>(options);
			OnIngestStatCompleteCallbackInternal onIngestStatCompleteCallbackInternal = new OnIngestStatCompleteCallbackInternal(StatsInterface.OnIngestStatComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onIngestStatCompleteCallbackInternal, Array.Empty<Delegate>());
			StatsInterface.EOS_Stats_IngestStat(base.InnerHandle, ref ingestStatOptionsInternal, zero, onIngestStatCompleteCallbackInternal);
			Helper.TryMarshalDispose<IngestStatOptionsInternal>(ref ingestStatOptionsInternal);
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x00083CB4 File Offset: 0x00081EB4
		public void QueryStats(QueryStatsOptions options, object clientData, OnQueryStatsCompleteCallback completionDelegate)
		{
			QueryStatsOptionsInternal queryStatsOptionsInternal = Helper.CopyProperties<QueryStatsOptionsInternal>(options);
			OnQueryStatsCompleteCallbackInternal onQueryStatsCompleteCallbackInternal = new OnQueryStatsCompleteCallbackInternal(StatsInterface.OnQueryStatsComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryStatsCompleteCallbackInternal, Array.Empty<Delegate>());
			StatsInterface.EOS_Stats_QueryStats(base.InnerHandle, ref queryStatsOptionsInternal, zero, onQueryStatsCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryStatsOptionsInternal>(ref queryStatsOptionsInternal);
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x00083D04 File Offset: 0x00081F04
		public uint GetStatsCount(GetStatCountOptions options)
		{
			GetStatCountOptionsInternal getStatCountOptionsInternal = Helper.CopyProperties<GetStatCountOptionsInternal>(options);
			uint num = StatsInterface.EOS_Stats_GetStatsCount(base.InnerHandle, ref getStatCountOptionsInternal);
			Helper.TryMarshalDispose<GetStatCountOptionsInternal>(ref getStatCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x00083D3C File Offset: 0x00081F3C
		public Result CopyStatByIndex(CopyStatByIndexOptions options, out Stat outStat)
		{
			CopyStatByIndexOptionsInternal copyStatByIndexOptionsInternal = Helper.CopyProperties<CopyStatByIndexOptionsInternal>(options);
			outStat = Helper.GetDefault<Stat>();
			IntPtr zero = IntPtr.Zero;
			Result result = StatsInterface.EOS_Stats_CopyStatByIndex(base.InnerHandle, ref copyStatByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyStatByIndexOptionsInternal>(ref copyStatByIndexOptionsInternal);
			if (Helper.TryMarshalGet<StatInternal, Stat>(zero, out outStat))
			{
				StatsInterface.EOS_Stats_Stat_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x00083D94 File Offset: 0x00081F94
		public Result CopyStatByName(CopyStatByNameOptions options, out Stat outStat)
		{
			CopyStatByNameOptionsInternal copyStatByNameOptionsInternal = Helper.CopyProperties<CopyStatByNameOptionsInternal>(options);
			outStat = Helper.GetDefault<Stat>();
			IntPtr zero = IntPtr.Zero;
			Result result = StatsInterface.EOS_Stats_CopyStatByName(base.InnerHandle, ref copyStatByNameOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyStatByNameOptionsInternal>(ref copyStatByNameOptionsInternal);
			if (Helper.TryMarshalGet<StatInternal, Stat>(zero, out outStat))
			{
				StatsInterface.EOS_Stats_Stat_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x00083DEC File Offset: 0x00081FEC
		[MonoPInvokeCallback]
		internal static void OnQueryStatsComplete(IntPtr address)
		{
			OnQueryStatsCompleteCallback onQueryStatsCompleteCallback = null;
			OnQueryStatsCompleteCallbackInfo onQueryStatsCompleteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryStatsCompleteCallback, OnQueryStatsCompleteCallbackInfoInternal, OnQueryStatsCompleteCallbackInfo>(address, out onQueryStatsCompleteCallback, out onQueryStatsCompleteCallbackInfo))
			{
				onQueryStatsCompleteCallback(onQueryStatsCompleteCallbackInfo);
			}
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x00083E10 File Offset: 0x00082010
		[MonoPInvokeCallback]
		internal static void OnIngestStatComplete(IntPtr address)
		{
			OnIngestStatCompleteCallback onIngestStatCompleteCallback = null;
			IngestStatCompleteCallbackInfo ingestStatCompleteCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnIngestStatCompleteCallback, IngestStatCompleteCallbackInfoInternal, IngestStatCompleteCallbackInfo>(address, out onIngestStatCompleteCallback, out ingestStatCompleteCallbackInfo))
			{
				onIngestStatCompleteCallback(ingestStatCompleteCallbackInfo);
			}
		}

		// Token: 0x06003BE2 RID: 15330
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Stats_Stat_Release(IntPtr stat);

		// Token: 0x06003BE3 RID: 15331
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Stats_CopyStatByName(IntPtr handle, ref CopyStatByNameOptionsInternal options, ref IntPtr outStat);

		// Token: 0x06003BE4 RID: 15332
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Stats_CopyStatByIndex(IntPtr handle, ref CopyStatByIndexOptionsInternal options, ref IntPtr outStat);

		// Token: 0x06003BE5 RID: 15333
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Stats_GetStatsCount(IntPtr handle, ref GetStatCountOptionsInternal options);

		// Token: 0x06003BE6 RID: 15334
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Stats_QueryStats(IntPtr handle, ref QueryStatsOptionsInternal options, IntPtr clientData, OnQueryStatsCompleteCallbackInternal completionDelegate);

		// Token: 0x06003BE7 RID: 15335
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Stats_IngestStat(IntPtr handle, ref IngestStatOptionsInternal options, IntPtr clientData, OnIngestStatCompleteCallbackInternal completionDelegate);

		// Token: 0x040016D5 RID: 5845
		public const int CopystatbynameApiLatest = 1;

		// Token: 0x040016D6 RID: 5846
		public const int CopystatbyindexApiLatest = 1;

		// Token: 0x040016D7 RID: 5847
		public const int GetstatcountApiLatest = 1;

		// Token: 0x040016D8 RID: 5848
		public const int GetstatscountApiLatest = 1;

		// Token: 0x040016D9 RID: 5849
		public const int StatApiLatest = 1;

		// Token: 0x040016DA RID: 5850
		public const int TimeUndefined = -1;

		// Token: 0x040016DB RID: 5851
		public const int QuerystatsApiLatest = 2;

		// Token: 0x040016DC RID: 5852
		public const int MaxQueryStats = 1000;

		// Token: 0x040016DD RID: 5853
		public const int IngeststatApiLatest = 2;

		// Token: 0x040016DE RID: 5854
		public const int MaxIngestStats = 3000;

		// Token: 0x040016DF RID: 5855
		public const int IngestdataApiLatest = 1;
	}
}
