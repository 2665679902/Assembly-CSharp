using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;

// Token: 0x02000092 RID: 146
public static class DebugUtil
{
	// Token: 0x06000583 RID: 1411 RVA: 0x0001A7BA File Offset: 0x000189BA
	public static void Separator()
	{
		global::Debug.Log(DebugUtil.LINE);
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x0001A7C6 File Offset: 0x000189C6
	public static void Assert(bool test)
	{
		global::Debug.Assert(test);
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0001A7CE File Offset: 0x000189CE
	public static void Assert(bool test, string message)
	{
		global::Debug.Assert(test, message);
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0001A7D7 File Offset: 0x000189D7
	public static void Assert(bool test, string message0, string message1)
	{
		if (!test)
		{
			DebugUtil.s_errorMessageBuilder.Length = 0;
			global::Debug.Assert(test, DebugUtil.s_errorMessageBuilder.Append(message0).Append(" ").Append(message1)
				.ToString());
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x0001A810 File Offset: 0x00018A10
	public static void Assert(bool test, string message0, string message1, string message2)
	{
		if (!test)
		{
			DebugUtil.s_errorMessageBuilder.Length = 0;
			global::Debug.Assert(test, DebugUtil.s_errorMessageBuilder.Append(message0).Append(" ").Append(message1)
				.Append(" ")
				.Append(message2)
				.ToString());
		}
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x0001A861 File Offset: 0x00018A61
	public static void AssertArgs(bool test, params object[] objs)
	{
		if (!test)
		{
			global::Debug.LogError(DebugUtil.BuildString(objs));
		}
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x0001A874 File Offset: 0x00018A74
	public static string BuildString(object[] objs)
	{
		string text = "";
		if (objs.Length != 0)
		{
			text = ((objs[0] != null) ? objs[0].ToString() : "null");
			for (int i = 1; i < objs.Length; i++)
			{
				object obj = objs[i];
				text = text + " " + ((obj != null) ? obj.ToString() : "null");
			}
		}
		return text;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0001A8CF File Offset: 0x00018ACF
	public static void DevAssert(bool test, string msg, UnityEngine.Object context = null)
	{
		if (!test)
		{
			global::Debug.LogWarning(msg, context);
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0001A8DB File Offset: 0x00018ADB
	public static void DevAssertArgs(bool test, params object[] objs)
	{
		if (!test)
		{
			global::Debug.LogWarning(DebugUtil.BuildString(objs));
		}
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0001A8EB File Offset: 0x00018AEB
	[Conditional("UNITY_EDITOR")]
	public static void AssertEditorOnlyArgs(bool test, params object[] objs)
	{
		global::Debug.Assert(test, DebugUtil.BuildString(objs));
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x0001A8FC File Offset: 0x00018AFC
	public static void DevAssertArgsWithStack(bool test, params object[] objs)
	{
		if (!test)
		{
			StackTrace stackTrace = new StackTrace(1, true);
			global::Debug.LogWarning(string.Format("{0}\n{1}", DebugUtil.BuildString(objs), stackTrace));
		}
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x0001A92A File Offset: 0x00018B2A
	public static void DevLogError(UnityEngine.Object context, string msg)
	{
		global::Debug.LogWarningFormat(context, msg, Array.Empty<object>());
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x0001A938 File Offset: 0x00018B38
	public static void DevLogError(string msg)
	{
		global::Debug.LogWarningFormat(msg, Array.Empty<object>());
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x0001A945 File Offset: 0x00018B45
	public static void DevLogErrorFormat(UnityEngine.Object context, string format, params object[] args)
	{
		global::Debug.LogWarningFormat(context, format, args);
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x0001A94F File Offset: 0x00018B4F
	public static void DevLogErrorFormat(string format, params object[] args)
	{
		global::Debug.LogWarningFormat(format, args);
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0001A958 File Offset: 0x00018B58
	public static void LogArgs(params object[] objs)
	{
		global::Debug.Log(DebugUtil.BuildString(objs));
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0001A965 File Offset: 0x00018B65
	public static void LogArgs(UnityEngine.Object context, params object[] objs)
	{
		global::Debug.Log(DebugUtil.BuildString(objs), context);
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x0001A973 File Offset: 0x00018B73
	public static void LogWarningArgs(params object[] objs)
	{
		global::Debug.LogWarning(DebugUtil.BuildString(objs));
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x0001A980 File Offset: 0x00018B80
	public static void LogWarningArgs(UnityEngine.Object context, params object[] objs)
	{
		global::Debug.LogWarning(DebugUtil.BuildString(objs), context);
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0001A98E File Offset: 0x00018B8E
	public static void LogErrorArgs(params object[] objs)
	{
		global::Debug.LogError(DebugUtil.BuildString(objs));
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0001A99B File Offset: 0x00018B9B
	public static void LogErrorArgs(UnityEngine.Object context, params object[] objs)
	{
		global::Debug.LogError(DebugUtil.BuildString(objs), context);
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001A9A9 File Offset: 0x00018BA9
	public static void LogException(UnityEngine.Object context, string errorMessage, Exception e)
	{
		DebugUtil.s_lastExceptionLogged = e;
		DebugUtil.LogErrorArgs(context, new object[]
		{
			errorMessage,
			"\n" + e.ToString()
		});
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0001A9D4 File Offset: 0x00018BD4
	public static Exception RetrieveLastExceptionLogged()
	{
		Exception ex = DebugUtil.s_lastExceptionLogged;
		DebugUtil.s_lastExceptionLogged = null;
		return ex;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x0001A9E1 File Offset: 0x00018BE1
	private static void RecursiveBuildFullName(GameObject obj)
	{
		if (obj == null)
		{
			return;
		}
		DebugUtil.RecursiveBuildFullName(obj.transform.parent.gameObject);
		DebugUtil.fullNameBuilder.Append("/").Append(obj.name);
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0001AA1D File Offset: 0x00018C1D
	private static StringBuilder BuildFullName(GameObject obj)
	{
		DebugUtil.fullNameBuilder.Length = 0;
		DebugUtil.RecursiveBuildFullName(obj);
		return DebugUtil.fullNameBuilder.Append(" (").Append(obj.GetInstanceID()).Append(")");
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0001AA54 File Offset: 0x00018C54
	public static string FullName(GameObject obj)
	{
		return DebugUtil.BuildFullName(obj).ToString();
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0001AA64 File Offset: 0x00018C64
	public static string FullName(Component cmp)
	{
		return DebugUtil.BuildFullName(cmp.gameObject).Append(" (").Append(cmp.GetType())
			.Append(" ")
			.Append(cmp.GetInstanceID().ToString())
			.Append(")")
			.ToString();
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0001AABD File Offset: 0x00018CBD
	[Conditional("UNITY_EDITOR")]
	public static void LogIfSelected(GameObject obj, params object[] objs)
	{
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0001AABF File Offset: 0x00018CBF
	[Conditional("ENABLE_DETAILED_PROFILING")]
	public static void ProfileBegin(string str)
	{
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0001AAC1 File Offset: 0x00018CC1
	[Conditional("ENABLE_DETAILED_PROFILING")]
	public static void ProfileBegin(string str, UnityEngine.Object target)
	{
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0001AAC3 File Offset: 0x00018CC3
	[Conditional("ENABLE_DETAILED_PROFILING")]
	public static void ProfileEnd()
	{
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0001AAC5 File Offset: 0x00018CC5
	public static KProfiler.Region ProfileRegion(string regionName, UnityEngine.Object profilerObj = null)
	{
		return new KProfiler.Region(regionName, profilerObj);
	}

	// Token: 0x04000573 RID: 1395
	private static StringBuilder s_errorMessageBuilder = new StringBuilder();

	// Token: 0x04000574 RID: 1396
	private static Exception s_lastExceptionLogged;

	// Token: 0x04000575 RID: 1397
	public static string LINE = "-----------------------------------------------------------";

	// Token: 0x04000576 RID: 1398
	private static StringBuilder fullNameBuilder = new StringBuilder();
}
