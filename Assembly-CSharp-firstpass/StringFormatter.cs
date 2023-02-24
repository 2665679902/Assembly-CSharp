using System;
using System.Collections.Generic;

// Token: 0x02000102 RID: 258
public static class StringFormatter
{
	// Token: 0x060008BD RID: 2237 RVA: 0x000231A0 File Offset: 0x000213A0
	public static string Replace(string format, string token, string replacement)
	{
		Dictionary<string, Dictionary<string, string>> dictionary = null;
		if (!StringFormatter.cachedReplacements.TryGetValue(format, out dictionary))
		{
			dictionary = new Dictionary<string, Dictionary<string, string>>();
			StringFormatter.cachedReplacements[format] = dictionary;
		}
		Dictionary<string, string> dictionary2 = null;
		if (!dictionary.TryGetValue(token, out dictionary2))
		{
			dictionary2 = new Dictionary<string, string>();
			dictionary[token] = dictionary2;
		}
		string text = null;
		if (!dictionary2.TryGetValue(replacement, out text))
		{
			text = format.Replace(token, replacement);
			dictionary2[replacement] = text;
		}
		return text;
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0002320A File Offset: 0x0002140A
	public static string Combine(string a, string b, string c)
	{
		return StringFormatter.Combine(StringFormatter.Combine(a, b), c);
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00023219 File Offset: 0x00021419
	public static string Combine(string a, string b, string c, string d)
	{
		return StringFormatter.Combine(StringFormatter.Combine(StringFormatter.Combine(a, b), c), d);
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x00023230 File Offset: 0x00021430
	public static string Combine(string a, string b)
	{
		Dictionary<string, string> dictionary = null;
		if (!StringFormatter.cachedCombines.TryGetValue(a, out dictionary))
		{
			dictionary = new Dictionary<string, string>();
			StringFormatter.cachedCombines[a] = dictionary;
		}
		string text = null;
		if (!dictionary.TryGetValue(b, out text))
		{
			text = a + b;
			dictionary[b] = text;
		}
		return text;
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x00023280 File Offset: 0x00021480
	public static string ToUpper(string a)
	{
		HashedString hashedString = a;
		string text = null;
		if (!StringFormatter.cachedToUppers.TryGetValue(hashedString, out text))
		{
			text = a.ToUpper();
			StringFormatter.cachedToUppers[hashedString] = text;
		}
		return text;
	}

	// Token: 0x04000667 RID: 1639
	private static Dictionary<string, Dictionary<string, Dictionary<string, string>>> cachedReplacements = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

	// Token: 0x04000668 RID: 1640
	private static Dictionary<string, Dictionary<string, string>> cachedCombines = new Dictionary<string, Dictionary<string, string>>();

	// Token: 0x04000669 RID: 1641
	private static Dictionary<HashedString, string> cachedToUppers = new Dictionary<HashedString, string>();
}
