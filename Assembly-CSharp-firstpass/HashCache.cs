using System;
using System.Collections.Generic;

// Token: 0x020000A5 RID: 165
public class HashCache
{
	// Token: 0x06000639 RID: 1593 RVA: 0x0001C786 File Offset: 0x0001A986
	public static HashCache Get()
	{
		if (HashCache.instance == null)
		{
			HashCache.instance = new HashCache();
		}
		return HashCache.instance;
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0001C7A0 File Offset: 0x0001A9A0
	public string Get(int hash)
	{
		string text = "";
		this.hashes.TryGetValue(hash, out text);
		return text;
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0001C7C3 File Offset: 0x0001A9C3
	public string Get(HashedString hash)
	{
		return this.Get(hash.HashValue);
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0001C7D2 File Offset: 0x0001A9D2
	public string Get(KAnimHashedString hash)
	{
		return this.Get(hash.HashValue);
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
	public HashedString Add(string text)
	{
		HashedString hashedString = new HashedString(text);
		this.Add(hashedString.HashValue, text);
		return hashedString;
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0001C808 File Offset: 0x0001AA08
	public void Add(int hash, string text)
	{
		string text2 = null;
		if (!this.hashes.TryGetValue(hash, out text2))
		{
			this.hashes[hash] = text.ToLower();
		}
	}

	// Token: 0x040005A5 RID: 1445
	private Dictionary<int, string> hashes = new Dictionary<int, string>();

	// Token: 0x040005A6 RID: 1446
	private static HashCache instance;
}
