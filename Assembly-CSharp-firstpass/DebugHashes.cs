using System;
using System.Collections.Generic;

// Token: 0x02000091 RID: 145
public static class DebugHashes
{
	// Token: 0x06000580 RID: 1408 RVA: 0x0001A758 File Offset: 0x00018958
	public static void Add(string name)
	{
		int num = Hash.SDBMLower(name);
		DebugHashes.hashMap[num] = name;
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0001A778 File Offset: 0x00018978
	public static string GetName(int hash)
	{
		if (DebugHashes.hashMap.ContainsKey(hash))
		{
			return DebugHashes.hashMap[hash];
		}
		return "Unknown HASH [0x" + hash.ToString("X") + "]";
	}

	// Token: 0x04000572 RID: 1394
	private static Dictionary<int, string> hashMap = new Dictionary<int, string>();
}
