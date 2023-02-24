using System;

// Token: 0x02000103 RID: 259
[Serializable]
public struct StringKey
{
	// Token: 0x060008C3 RID: 2243 RVA: 0x000232D9 File Offset: 0x000214D9
	public StringKey(string str)
	{
		this.String = str;
		this.Hash = str.GetHashCode();
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x000232F0 File Offset: 0x000214F0
	public override string ToString()
	{
		return string.Concat(new string[]
		{
			"S: [",
			this.String,
			"] H: [",
			this.Hash.ToString(),
			"] Value: [",
			Strings.Get(this),
			"]"
		});
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00023352 File Offset: 0x00021552
	public bool IsValid()
	{
		return this.Hash != 0;
	}

	// Token: 0x0400066A RID: 1642
	public string String;

	// Token: 0x0400066B RID: 1643
	public int Hash;
}
