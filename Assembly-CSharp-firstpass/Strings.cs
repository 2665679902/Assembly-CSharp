using System;
using System.Collections.Generic;

// Token: 0x02000106 RID: 262
public static class Strings
{
	// Token: 0x060008CE RID: 2254 RVA: 0x000235C8 File Offset: 0x000217C8
	private static StringEntry GetInvalidString(params StringKey[] keys)
	{
		string text = "MISSING";
		foreach (StringKey stringKey in keys)
		{
			if (text != "")
			{
				text += ".";
			}
			text += stringKey.String;
		}
		Strings.invalidKeys.Add(text);
		return new StringEntry(text);
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0002362C File Offset: 0x0002182C
	public static StringEntry Get(StringKey key0)
	{
		StringEntry stringEntry = Strings.RootTable.Get(key0);
		if (stringEntry == null)
		{
			stringEntry = Strings.GetInvalidString(new StringKey[] { key0 });
		}
		return stringEntry;
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x00023660 File Offset: 0x00021860
	public static StringEntry Get(string key)
	{
		StringKey stringKey = new StringKey(key);
		StringEntry stringEntry = Strings.RootTable.Get(stringKey);
		if (stringEntry == null)
		{
			stringEntry = Strings.GetInvalidString(new StringKey[] { stringKey });
		}
		return stringEntry;
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x00023699 File Offset: 0x00021899
	public static bool TryGet(StringKey key, out StringEntry result)
	{
		result = Strings.RootTable.Get(key);
		return result != null;
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x000236AD File Offset: 0x000218AD
	public static bool TryGet(string key, out StringEntry result)
	{
		return Strings.TryGet(new StringKey(key), out result);
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x000236BB File Offset: 0x000218BB
	public static void Add(params string[] value)
	{
		Strings.RootTable.Add(0, value);
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x000236C9 File Offset: 0x000218C9
	public static void PrintTable()
	{
		Strings.RootTable.Print("");
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x000236DA File Offset: 0x000218DA
	public static void VisitEntries(StringTable.EntryVisitor visit)
	{
		Strings.RootTable.VisitEntries(visit);
	}

	// Token: 0x04000670 RID: 1648
	private static StringTable RootTable = new StringTable();

	// Token: 0x04000671 RID: 1649
	private static HashSet<string> invalidKeys = new HashSet<string>();
}
