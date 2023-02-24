using System;

// Token: 0x02000C39 RID: 3129
public static class WorldGenLogger
{
	// Token: 0x060062FC RID: 25340 RVA: 0x00249846 File Offset: 0x00247A46
	public static void LogException(string message, string stack)
	{
		Debug.LogError(message + "\n" + stack);
	}
}
