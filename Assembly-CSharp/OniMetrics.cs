using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009DC RID: 2524
public class OniMetrics : MonoBehaviour
{
	// Token: 0x06004B65 RID: 19301 RVA: 0x001A714C File Offset: 0x001A534C
	private static void EnsureMetrics()
	{
		if (OniMetrics.Metrics != null)
		{
			return;
		}
		OniMetrics.Metrics = new List<Dictionary<string, object>>(2);
		for (int i = 0; i < 2; i++)
		{
			OniMetrics.Metrics.Add(null);
		}
	}

	// Token: 0x06004B66 RID: 19302 RVA: 0x001A7183 File Offset: 0x001A5383
	public static void LogEvent(OniMetrics.Event eventType, string key, object data)
	{
		OniMetrics.EnsureMetrics();
		if (OniMetrics.Metrics[(int)eventType] == null)
		{
			OniMetrics.Metrics[(int)eventType] = new Dictionary<string, object>();
		}
		OniMetrics.Metrics[(int)eventType][key] = data;
	}

	// Token: 0x06004B67 RID: 19303 RVA: 0x001A71BC File Offset: 0x001A53BC
	public static void SendEvent(OniMetrics.Event eventType, string debugName)
	{
		if (OniMetrics.Metrics[(int)eventType] == null || OniMetrics.Metrics[(int)eventType].Count == 0)
		{
			return;
		}
		ThreadedHttps<KleiMetrics>.Instance.SendEvent(OniMetrics.Metrics[(int)eventType], debugName);
		OniMetrics.Metrics[(int)eventType].Clear();
	}

	// Token: 0x04003164 RID: 12644
	private static List<Dictionary<string, object>> Metrics;

	// Token: 0x020017DE RID: 6110
	public enum Event : short
	{
		// Token: 0x04006E43 RID: 28227
		NewSave,
		// Token: 0x04006E44 RID: 28228
		EndOfCycle,
		// Token: 0x04006E45 RID: 28229
		NumEvents
	}
}
