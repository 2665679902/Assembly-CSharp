using System;
using System.Collections.Generic;
using System.IO;
using KSerialization.Converters;

// Token: 0x02000013 RID: 19
public class AnimCommandFile
{
	// Token: 0x17000037 RID: 55
	// (get) Token: 0x0600010C RID: 268 RVA: 0x000066FC File Offset: 0x000048FC
	// (set) Token: 0x0600010D RID: 269 RVA: 0x00006704 File Offset: 0x00004904
	[StringEnumConverter]
	public AnimCommandFile.ConfigType Type { get; private set; }

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600010E RID: 270 RVA: 0x0000670D File Offset: 0x0000490D
	// (set) Token: 0x0600010F RID: 271 RVA: 0x00006715 File Offset: 0x00004915
	[StringEnumConverter]
	public AnimCommandFile.GroupBy TagGroup { get; private set; }

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000110 RID: 272 RVA: 0x0000671E File Offset: 0x0000491E
	// (set) Token: 0x06000111 RID: 273 RVA: 0x00006726 File Offset: 0x00004926
	[StringEnumConverter]
	public KAnimBatchGroup.RendererType RendererType { get; private set; }

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000112 RID: 274 RVA: 0x0000672F File Offset: 0x0000492F
	// (set) Token: 0x06000113 RID: 275 RVA: 0x00006737 File Offset: 0x00004937
	public string TargetBuild { get; private set; }

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000114 RID: 276 RVA: 0x00006740 File Offset: 0x00004940
	// (set) Token: 0x06000115 RID: 277 RVA: 0x00006748 File Offset: 0x00004948
	public string AnimTargetBuild { get; private set; }

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000116 RID: 278 RVA: 0x00006751 File Offset: 0x00004951
	// (set) Token: 0x06000117 RID: 279 RVA: 0x00006759 File Offset: 0x00004959
	public string SwapTargetBuild { get; private set; }

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000118 RID: 280 RVA: 0x00006762 File Offset: 0x00004962
	// (set) Token: 0x06000119 RID: 281 RVA: 0x0000676A File Offset: 0x0000496A
	public Dictionary<string, List<string>> DefaultBuilds { get; private set; }

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600011A RID: 282 RVA: 0x00006773 File Offset: 0x00004973
	// (set) Token: 0x0600011B RID: 283 RVA: 0x0000677B File Offset: 0x0000497B
	public int MaxGroupSize { get; private set; }

	// Token: 0x0600011C RID: 284 RVA: 0x00006784 File Offset: 0x00004984
	public AnimCommandFile()
	{
		this.MaxGroupSize = 30;
		this.DefaultBuilds = new Dictionary<string, List<string>>();
		this.TagGroup = AnimCommandFile.GroupBy.DontGroup;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x000067BC File Offset: 0x000049BC
	public bool IsSwap(KAnimFile file)
	{
		if (this.TagGroup != AnimCommandFile.GroupBy.NamedGroup)
		{
			return false;
		}
		string fileName = Path.GetFileName(file.homedirectory);
		foreach (KeyValuePair<string, List<string>> keyValuePair in this.DefaultBuilds)
		{
			if (keyValuePair.Value.Contains(fileName))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00006838 File Offset: 0x00004A38
	public void AddGroupFile(KAnimGroupFile.GroupFile gf)
	{
		if (!this.groupFiles.Contains(gf))
		{
			this.groupFiles.Add(gf);
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00006854 File Offset: 0x00004A54
	public string GetGroupName(KAnimFile kaf)
	{
		switch (this.TagGroup)
		{
		case AnimCommandFile.GroupBy.__IGNORE__:
			return null;
		case AnimCommandFile.GroupBy.DontGroup:
			return kaf.name;
		case AnimCommandFile.GroupBy.Folder:
			return Path.GetFileName(this.directory) + (this.groupFiles.Count / 10).ToString();
		case AnimCommandFile.GroupBy.NamedGroup:
		{
			string fileName = Path.GetFileName(kaf.homedirectory);
			foreach (KeyValuePair<string, List<string>> keyValuePair in this.DefaultBuilds)
			{
				if (keyValuePair.Value.Contains(fileName))
				{
					return keyValuePair.Key;
				}
			}
			return this.TargetBuild;
		}
		case AnimCommandFile.GroupBy.NamedGroupNoSplit:
			return this.TargetBuild;
		default:
			return null;
		}
	}

	// Token: 0x04000072 RID: 114
	[NonSerialized]
	public string directory = "";

	// Token: 0x04000073 RID: 115
	[NonSerialized]
	private List<KAnimGroupFile.GroupFile> groupFiles = new List<KAnimGroupFile.GroupFile>();

	// Token: 0x02000964 RID: 2404
	public enum ConfigType
	{
		// Token: 0x04002079 RID: 8313
		Default,
		// Token: 0x0400207A RID: 8314
		AnimOnly
	}

	// Token: 0x02000965 RID: 2405
	public enum GroupBy
	{
		// Token: 0x0400207C RID: 8316
		__IGNORE__,
		// Token: 0x0400207D RID: 8317
		DontGroup,
		// Token: 0x0400207E RID: 8318
		Folder,
		// Token: 0x0400207F RID: 8319
		NamedGroup,
		// Token: 0x04002080 RID: 8320
		NamedGroupNoSplit
	}
}
