using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

// Token: 0x02000002 RID: 2
public static class Debug
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private static string TimeStamp()
	{
		return DateTime.UtcNow.ToString("[HH:mm:ss.fff] [") + Thread.CurrentThread.ManagedThreadId.ToString() + "] ";
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000208B File Offset: 0x0000028B
	private static void WriteTimeStamped(params object[] objs)
	{
		Console.WriteLine(global::Debug.TimeStamp() + DebugUtil.BuildString(objs));
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000003 RID: 3 RVA: 0x000020A2 File Offset: 0x000002A2
	public static bool isDebugBuild
	{
		get
		{
			return UnityEngine.Debug.isDebugBuild;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000004 RID: 4 RVA: 0x000020A9 File Offset: 0x000002A9
	// (set) Token: 0x06000005 RID: 5 RVA: 0x000020B0 File Offset: 0x000002B0
	public static bool developerConsoleVisible
	{
		get
		{
			return UnityEngine.Debug.developerConsoleVisible;
		}
		set
		{
			UnityEngine.Debug.developerConsoleVisible = value;
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020B8 File Offset: 0x000002B8
	[Conditional("DEBUG")]
	public static void Break()
	{
		if (Debugger.IsAttached)
		{
			Debugger.Break();
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020C6 File Offset: 0x000002C6
	public static void LogException(Exception exception)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		UnityEngine.Debug.LogException(exception);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020D6 File Offset: 0x000002D6
	public static void Log(object obj)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[] { "[INFO]", obj });
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000020F7 File Offset: 0x000002F7
	public static void Log(object obj, UnityEngine.Object context)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[]
		{
			"[INFO]",
			(context != null) ? context.name : "null",
			obj
		});
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002131 File Offset: 0x00000331
	public static void LogFormat(string format, params object[] args)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[]
		{
			"[INFO]",
			string.Format(format, args)
		});
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002158 File Offset: 0x00000358
	public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[]
		{
			"[INFO]",
			(context != null) ? context.name : "null",
			string.Format(format, args)
		});
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002198 File Offset: 0x00000398
	public static void LogWarning(object obj)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[] { "[WARNING]", obj });
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000021B9 File Offset: 0x000003B9
	public static void LogWarning(object obj, UnityEngine.Object context)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[]
		{
			"[WARNING]",
			(context != null) ? context.name : "null",
			obj
		});
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021F3 File Offset: 0x000003F3
	public static void LogWarningFormat(string format, params object[] args)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[]
		{
			"[WARNING]",
			string.Format(format, args)
		});
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000221A File Offset: 0x0000041A
	public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[]
		{
			"[WARNING]",
			(context != null) ? context.name : "null",
			string.Format(format, args)
		});
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000225A File Offset: 0x0000045A
	public static void LogError(object obj)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[] { "[ERROR]", obj });
		UnityEngine.Debug.LogError(obj);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002284 File Offset: 0x00000484
	public static void LogError(object obj, UnityEngine.Object context)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[]
		{
			"[ERROR]",
			(context != null) ? context.name : "null",
			obj
		});
		UnityEngine.Debug.LogError(obj, context);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000022D0 File Offset: 0x000004D0
	public static void LogErrorFormat(string format, params object[] args)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[]
		{
			"[ERROR]",
			string.Format(format, args)
		});
		UnityEngine.Debug.LogErrorFormat(format, args);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002300 File Offset: 0x00000500
	public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
	{
		if (global::Debug.s_loggingDisabled)
		{
			return;
		}
		global::Debug.WriteTimeStamped(new object[]
		{
			"[ERROR]",
			(context != null) ? context.name : "null",
			string.Format(format, args)
		});
		UnityEngine.Debug.LogErrorFormat(context, format, args);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002353 File Offset: 0x00000553
	public static void Assert(bool condition)
	{
		if (!condition)
		{
			global::Debug.LogError("Assert failed");
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002362 File Offset: 0x00000562
	public static void Assert(bool condition, object message)
	{
		if (!condition)
		{
			global::Debug.LogError("Assert failed: " + ((message != null) ? message.ToString() : null));
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002382 File Offset: 0x00000582
	public static void Assert(bool condition, object message, UnityEngine.Object context)
	{
		if (!condition)
		{
			global::Debug.LogError("Assert failed: " + ((message != null) ? message.ToString() : null), context);
		}
	}

	// Token: 0x06000017 RID: 23 RVA: 0x000023A3 File Offset: 0x000005A3
	public static void DisableLogging()
	{
		global::Debug.s_loggingDisabled = true;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000023AB File Offset: 0x000005AB
	[Conditional("UNITY_EDITOR")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float duration = 0f, bool depthTest = true)
	{
		UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000023B8 File Offset: 0x000005B8
	[Conditional("UNITY_EDITOR")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color = default(Color), float duration = 0f, bool depthTest = true)
	{
		UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);
	}

	// Token: 0x04000001 RID: 1
	private static bool s_loggingDisabled;
}
