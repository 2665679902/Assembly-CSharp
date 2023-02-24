using System;
using System.Diagnostics;

namespace KSerialization
{
	// Token: 0x020004FB RID: 1275
	internal static class DebugLog
	{
		// Token: 0x060036F5 RID: 14069 RVA: 0x0007929A File Offset: 0x0007749A
		[Conditional("DEBUG_LOG")]
		public static void Output(DebugLog.Level msg_level, string msg)
		{
			if (msg_level > DebugLog.Level.Error)
			{
				return;
			}
			switch (msg_level)
			{
			case DebugLog.Level.Error:
				global::Debug.LogError(msg);
				return;
			case DebugLog.Level.Warning:
				global::Debug.LogWarning(msg);
				return;
			case DebugLog.Level.Info:
				global::Debug.Log(msg);
				return;
			default:
				return;
			}
		}

		// Token: 0x040013AC RID: 5036
		private const DebugLog.Level OutputLevel = DebugLog.Level.Error;

		// Token: 0x02000B1A RID: 2842
		public enum Level
		{
			// Token: 0x04002629 RID: 9769
			Error,
			// Token: 0x0400262A RID: 9770
			Warning,
			// Token: 0x0400262B RID: 9771
			Info
		}
	}
}
