using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace KSerialization
{
	// Token: 0x020004FA RID: 1274
	public static class Stats
	{
		// Token: 0x060036EF RID: 14063 RVA: 0x00079107 File Offset: 0x00077307
		[Conditional("ENABLE_KSERIALIZER_STATS")]
		public static void Clear()
		{
			Stats.serializationStats.Clear();
			Stats.deserializationStats.Clear();
		}

		// Token: 0x060036F0 RID: 14064 RVA: 0x00079120 File Offset: 0x00077320
		[Conditional("ENABLE_KSERIALIZER_STATS")]
		public static void Write(Type type, long num_bytes)
		{
			Stats.StatInfo statInfo;
			Stats.serializationStats.TryGetValue(type, out statInfo);
			statInfo.numOccurrences++;
			statInfo.numBytes += num_bytes;
			Stats.serializationStats[type] = statInfo;
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x00079160 File Offset: 0x00077360
		[Conditional("ENABLE_KSERIALIZER_STATS")]
		public static void Read(Type type, long num_bytes)
		{
			Stats.StatInfo statInfo;
			Stats.deserializationStats.TryGetValue(type, out statInfo);
			statInfo.numOccurrences++;
			statInfo.numBytes += num_bytes;
			Stats.deserializationStats[type] = statInfo;
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x0007919F File Offset: 0x0007739F
		public static void Print()
		{
			int count = Stats.serializationStats.Count;
			int count2 = Stats.deserializationStats.Count;
		}

		// Token: 0x060036F3 RID: 14067 RVA: 0x000791BC File Offset: 0x000773BC
		[Conditional("ENABLE_KSERIALIZER_STATS")]
		private static void Print(string header, Dictionary<Type, Stats.StatInfo> stats)
		{
			string text = header + "\n";
			foreach (KeyValuePair<Type, Stats.StatInfo> keyValuePair in stats)
			{
				string[] array = new string[7];
				array[0] = text;
				array[1] = keyValuePair.Key.ToString();
				array[2] = ",";
				int num = 3;
				Stats.StatInfo statInfo = keyValuePair.Value;
				array[num] = statInfo.numOccurrences.ToString();
				array[4] = ",";
				int num2 = 5;
				statInfo = keyValuePair.Value;
				array[num2] = statInfo.numBytes.ToString();
				array[6] = "\n";
				text = string.Concat(array);
			}
			DebugUtil.LogArgs(new object[] { text });
		}

		// Token: 0x040013AA RID: 5034
		private static Dictionary<Type, Stats.StatInfo> serializationStats = new Dictionary<Type, Stats.StatInfo>();

		// Token: 0x040013AB RID: 5035
		private static Dictionary<Type, Stats.StatInfo> deserializationStats = new Dictionary<Type, Stats.StatInfo>();

		// Token: 0x02000B19 RID: 2841
		private struct StatInfo
		{
			// Token: 0x04002626 RID: 9766
			public int numOccurrences;

			// Token: 0x04002627 RID: 9767
			public long numBytes;
		}
	}
}
