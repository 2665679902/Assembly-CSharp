using System;
using System.Diagnostics;
using System.IO;

// Token: 0x0200009D RID: 157
public class FileLog
{
	// Token: 0x06000614 RID: 1556 RVA: 0x0001BF98 File Offset: 0x0001A198
	[Conditional("ENABLE_LOG")]
	public static void Initialize(string filename)
	{
		FileLog.instance = new FileLog(filename);
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0001BFA5 File Offset: 0x0001A1A5
	[Conditional("ENABLE_LOG")]
	public static void Shutdown()
	{
		if (FileLog.instance.writer != null)
		{
			FileLog.instance.writer.Close();
		}
		FileLog.instance = null;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0001BFC8 File Offset: 0x0001A1C8
	private FileLog(string filename)
	{
		this.writer = new StreamWriter(filename);
		this.writer.AutoFlush = true;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0001BFE8 File Offset: 0x0001A1E8
	[Conditional("ENABLE_LOG")]
	public static void Log(params object[] objs)
	{
		FileLog.instance.LogObjs(objs);
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0001BFF8 File Offset: 0x0001A1F8
	private void LogObjs(object[] objs)
	{
		string text = FileLog.BuildString(objs);
		this.writer.WriteLine(text);
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0001C018 File Offset: 0x0001A218
	private static string BuildString(object[] objs)
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

	// Token: 0x04000596 RID: 1430
	private static FileLog instance;

	// Token: 0x04000597 RID: 1431
	private StreamWriter writer;
}
