using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// Token: 0x02000275 RID: 629
[Serializable]
public struct ModInfo
{
	// Token: 0x0400074A RID: 1866
	[JsonConverter(typeof(StringEnumConverter))]
	public ModInfo.Source source;

	// Token: 0x0400074B RID: 1867
	[JsonConverter(typeof(StringEnumConverter))]
	public ModInfo.ModType type;

	// Token: 0x0400074C RID: 1868
	public string assetID;

	// Token: 0x0400074D RID: 1869
	public string assetPath;

	// Token: 0x0400074E RID: 1870
	public bool enabled;

	// Token: 0x0400074F RID: 1871
	public bool markedForDelete;

	// Token: 0x04000750 RID: 1872
	public bool markedForUpdate;

	// Token: 0x04000751 RID: 1873
	public string description;

	// Token: 0x04000752 RID: 1874
	public ulong lastModifiedTime;

	// Token: 0x02000EF0 RID: 3824
	public enum Source
	{
		// Token: 0x040052C1 RID: 21185
		Local,
		// Token: 0x040052C2 RID: 21186
		Steam,
		// Token: 0x040052C3 RID: 21187
		Rail
	}

	// Token: 0x02000EF1 RID: 3825
	public enum ModType
	{
		// Token: 0x040052C5 RID: 21189
		WorldGen,
		// Token: 0x040052C6 RID: 21190
		Scenario,
		// Token: 0x040052C7 RID: 21191
		Mod
	}
}
