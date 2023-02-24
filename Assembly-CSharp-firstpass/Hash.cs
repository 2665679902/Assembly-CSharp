using System;
using System.Collections.Generic;

// Token: 0x020000A4 RID: 164
public static class Hash
{
	// Token: 0x06000637 RID: 1591 RVA: 0x0001C70C File Offset: 0x0001A90C
	public static int SDBMLower(string s)
	{
		if (s == null)
		{
			return 0;
		}
		uint num = 0U;
		for (int i = 0; i < s.Length; i++)
		{
			num = (uint)char.ToLowerInvariant(s[i]) + (num << 6) + (num << 16) - num;
		}
		return (int)num;
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0001C74C File Offset: 0x0001A94C
	public static int[] SDBMLower(IList<string> strings)
	{
		int[] array = new int[strings.Count];
		for (int i = 0; i < strings.Count; i++)
		{
			array[i] = Hash.SDBMLower(strings[i]);
		}
		return array;
	}
}
