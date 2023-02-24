using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000014 RID: 20
public class KAnimGroupFile : ScriptableObject
{
	// Token: 0x06000120 RID: 288 RVA: 0x00006930 File Offset: 0x00004B30
	public static void DestroyInstance()
	{
		KAnimGroupFile.groupfile = null;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x00006938 File Offset: 0x00004B38
	public static string GetFilePath(string contentDir = "")
	{
		if (string.IsNullOrEmpty(contentDir))
		{
			return "Assets/anim/base/resources/animgrouptags.asset";
		}
		return "Assets/anim" + string.Format("/{0}/", contentDir) + "animgrouptags.asset";
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00006962 File Offset: 0x00004B62
	public static KAnimGroupFile GetGroupFile()
	{
		global::Debug.Assert(KAnimGroupFile.groupfile != null, "Cannot GetGroupFile before it is loaded.");
		return KAnimGroupFile.groupfile;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00006980 File Offset: 0x00004B80
	public static KAnimGroupFile.Group GetGroup(HashedString tag)
	{
		global::Debug.Assert(KAnimGroupFile.groupfile != null, "GetGroup called before LoadAll called");
		List<KAnimGroupFile.Group> list = KAnimGroupFile.groupfile.groups;
		global::Debug.Assert(list != null, list.Count > 0);
		for (int i = 0; i < list.Count; i++)
		{
			KAnimGroupFile.Group group = list[i];
			if (group.id == tag || group.target == tag)
			{
				return group;
			}
		}
		return null;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000069FC File Offset: 0x00004BFC
	public static HashedString GetGroupForHomeDirectory(HashedString homedirectory)
	{
		List<Pair<HashedString, HashedString>> list = KAnimGroupFile.groupfile.currentGroup;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].first == homedirectory)
			{
				return list[i].second;
			}
		}
		return default(HashedString);
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00006A4F File Offset: 0x00004C4F
	public List<KAnimGroupFile.Group> GetData()
	{
		return this.groups;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00006A57 File Offset: 0x00004C57
	public void Reset()
	{
		this.groups = new List<KAnimGroupFile.Group>();
		this.currentGroup = new List<Pair<HashedString, HashedString>>();
	}

	// Token: 0x06000127 RID: 295 RVA: 0x00006A70 File Offset: 0x00004C70
	private int AddGroup(AnimCommandFile akf, KAnimGroupFile.GroupFile gf, KAnimFile file)
	{
		bool flag = akf.IsSwap(file);
		HashedString groupId = new HashedString(gf.groupID);
		int num = this.groups.FindIndex((KAnimGroupFile.Group t) => t.id == groupId);
		if (num == -1)
		{
			num = this.groups.Count;
			KAnimGroupFile.Group group = new KAnimGroupFile.Group(groupId);
			group.commandDirectory = akf.directory;
			group.maxGroupSize = akf.MaxGroupSize;
			group.renderType = akf.RendererType;
			if (this.groups.FindIndex((KAnimGroupFile.Group t) => t.commandDirectory == group.commandDirectory) == -1)
			{
				if (flag)
				{
					if (!string.IsNullOrEmpty(akf.TargetBuild))
					{
						group.target = new HashedString(akf.TargetBuild);
					}
					if (group.renderType != KAnimBatchGroup.RendererType.DontRender)
					{
						group.renderType = KAnimBatchGroup.RendererType.DontRender;
						group.swapTarget = new HashedString(akf.SwapTargetBuild);
					}
				}
				if (akf.Type == AnimCommandFile.ConfigType.AnimOnly)
				{
					group.target = new HashedString(akf.TargetBuild);
					group.renderType = KAnimBatchGroup.RendererType.AnimOnly;
					group.animTarget = new HashedString(akf.AnimTargetBuild);
					group.swapTarget = new HashedString(akf.SwapTargetBuild);
				}
			}
			this.groups.Add(group);
		}
		return num;
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00006BF0 File Offset: 0x00004DF0
	public bool AddAnimFile(KAnimGroupFile.GroupFile gf, AnimCommandFile akf, KAnimFile file)
	{
		global::Debug.Assert(gf != null);
		global::Debug.Assert(file != null, gf.groupID);
		global::Debug.Assert(akf != null, gf.groupID);
		int num = this.AddGroup(akf, gf, file);
		return this.AddFile(num, file);
	}

	// Token: 0x06000129 RID: 297 RVA: 0x00006C3C File Offset: 0x00004E3C
	private bool AddFile(int groupIndex, KAnimFile file)
	{
		if (!this.groups[groupIndex].animNames.Contains(file.name))
		{
			Pair<HashedString, HashedString> pair = new Pair<HashedString, HashedString>(file.homedirectory, this.groups[groupIndex].id);
			bool flag = false;
			for (int i = 0; i < this.currentGroup.Count; i++)
			{
				if (this.currentGroup[i].first == file.homedirectory)
				{
					this.currentGroup[i] = pair;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this.currentGroup.Add(pair);
			}
			this.groups[groupIndex].animFiles.Add(file);
			this.groups[groupIndex].animNames.Add(file.name);
			return true;
		}
		return false;
	}

	// Token: 0x0600012A RID: 298 RVA: 0x00006D28 File Offset: 0x00004F28
	public KAnimGroupFile.AddModResult AddAnimMod(KAnimGroupFile.GroupFile gf, AnimCommandFile akf, KAnimFile file)
	{
		global::Debug.Assert(gf != null);
		global::Debug.Assert(file != null, gf.groupID);
		global::Debug.Assert(akf != null, gf.groupID);
		int num = this.AddGroup(akf, gf, file);
		string name = file.GetData().name;
		int num2 = this.groups[num].animFiles.FindIndex((KAnimFile candidate) => candidate != null && candidate.GetData().name == name);
		if (num2 == -1)
		{
			this.groups[num].animFiles.Add(file);
			this.groups[num].animNames.Add(file.GetData().name);
			return KAnimGroupFile.AddModResult.Added;
		}
		this.groups[num].animFiles[num2].mod = file.mod;
		return KAnimGroupFile.AddModResult.Replaced;
	}

	// Token: 0x0600012B RID: 299 RVA: 0x00006E09 File Offset: 0x00005009
	public static void LoadGroupResourceFile()
	{
		KAnimGroupFile.groupfile = (KAnimGroupFile)Resources.Load("animgrouptags", typeof(KAnimGroupFile));
	}

	// Token: 0x0600012C RID: 300 RVA: 0x00006E29 File Offset: 0x00005029
	public static void LoadAll()
	{
		KAnimGroupFile.groupfile.Load();
	}

	// Token: 0x0600012D RID: 301 RVA: 0x00006E35 File Offset: 0x00005035
	public static void MapNamesToAnimFiles(Dictionary<HashedString, KAnimFile> animTable)
	{
		KAnimGroupFile.groupfile.DoMapNamesToAnimFiles(animTable);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x00006E44 File Offset: 0x00005044
	private void DoMapNamesToAnimFiles(Dictionary<HashedString, KAnimFile> animTable)
	{
		for (int i = 0; i < this.groups.Count; i++)
		{
			this.groups[i].animFiles = new List<KAnimFile>();
			for (int j = 0; j < this.groups[i].animNames.Count; j++)
			{
				HashedString hashedString = this.groups[i].animNames[j];
				KAnimFile kanimFile = null;
				animTable.TryGetValue(hashedString, out kanimFile);
				if (kanimFile != null)
				{
					this.groups[i].animFiles.Add(kanimFile);
				}
			}
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00006EE8 File Offset: 0x000050E8
	private void Load()
	{
		this.fileData.Clear();
		int i = 0;
		while (i < this.groups.Count)
		{
			if (!this.groups[i].id.IsValid)
			{
				global::Debug.LogErrorFormat("Group invalid groupIndex [{0}]", new object[] { i });
			}
			KBatchGroupData kbatchGroupData;
			if (this.groups[i].target.IsValid)
			{
				kbatchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.groups[i].target);
			}
			else
			{
				kbatchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.groups[i].id);
			}
			HashedString hashedString = this.groups[i].id;
			if (this.groups[i].renderType != KAnimBatchGroup.RendererType.AnimOnly)
			{
				goto IL_106;
			}
			if (this.groups[i].swapTarget.IsValid)
			{
				kbatchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.groups[i].swapTarget);
				hashedString = this.groups[i].swapTarget;
				goto IL_106;
			}
			IL_221:
			i++;
			continue;
			IL_106:
			for (int j = 0; j < this.groups[i].animFiles.Count; j++)
			{
				KAnimFile kanimFile = this.groups[i].animFiles[j];
				if (kanimFile != null && kanimFile.buildBytes != null && !this.fileData.ContainsKey(kanimFile.GetInstanceID()))
				{
					if (kanimFile.buildBytes.Length == 0)
					{
						global::Debug.LogWarning("Build File [" + kanimFile.GetData().name + "] has 0 bytes");
					}
					else
					{
						HashedString hashedString2 = new HashedString(kanimFile.name);
						HashCache.Get().Add(hashedString2.HashValue, kanimFile.name);
						KAnimFileData file = KGlobalAnimParser.Get().GetFile(kanimFile);
						file.maxVisSymbolFrames = 0;
						file.batchTag = hashedString;
						file.buildIndex = KGlobalAnimParser.ParseBuildData(kbatchGroupData, hashedString2, new FastReader(kanimFile.buildBytes), kanimFile.textureList);
						this.fileData.Add(kanimFile.GetInstanceID(), file);
					}
				}
			}
			goto IL_221;
		}
		for (int k = 0; k < this.groups.Count; k++)
		{
			if (this.groups[k].renderType == KAnimBatchGroup.RendererType.AnimOnly)
			{
				KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.groups[k].swapTarget);
				KBatchGroupData batchGroupData2 = KAnimBatchManager.Instance().GetBatchGroupData(this.groups[k].animTarget);
				for (int l = 0; l < batchGroupData.builds.Count; l++)
				{
					KAnim.Build build = batchGroupData.builds[l];
					if (build != null && build.symbols != null)
					{
						for (int m = 0; m < build.symbols.Length; m++)
						{
							KAnim.Build.Symbol symbol = build.symbols[m];
							if (symbol != null && symbol.hash.IsValid() && batchGroupData2.GetFirstIndex(symbol.hash) == -1)
							{
								KAnim.Build.Symbol symbol2 = new KAnim.Build.Symbol();
								symbol2.build = build;
								symbol2.hash = symbol.hash;
								symbol2.path = symbol.path;
								symbol2.colourChannel = symbol.colourChannel;
								symbol2.flags = symbol.flags;
								symbol2.firstFrameIdx = batchGroupData2.symbolFrameInstances.Count;
								symbol2.numFrames = symbol.numFrames;
								symbol2.symbolIndexInSourceBuild = batchGroupData2.frameElementSymbols.Count;
								for (int n = 0; n < symbol2.numFrames; n++)
								{
									KAnim.Build.SymbolFrameInstance symbolFrameInstance = batchGroupData.GetSymbolFrameInstance(n + symbol.firstFrameIdx);
									KAnim.Build.SymbolFrameInstance symbolFrameInstance2 = default(KAnim.Build.SymbolFrameInstance);
									symbolFrameInstance2.symbolFrame = symbolFrameInstance.symbolFrame;
									symbolFrameInstance2.buildImageIdx = -1;
									symbolFrameInstance2.symbolIdx = batchGroupData2.GetSymbolCount();
									batchGroupData2.symbolFrameInstances.Add(symbolFrameInstance2);
								}
								batchGroupData2.AddBuildSymbol(symbol2);
							}
						}
					}
				}
			}
		}
		for (int num = 0; num < this.groups.Count; num++)
		{
			if (!this.groups[num].id.IsValid)
			{
				global::Debug.LogErrorFormat("Group invalid groupIndex [{0}]", new object[] { num });
			}
			if (this.groups[num].renderType != KAnimBatchGroup.RendererType.DontRender)
			{
				KBatchGroupData kbatchGroupData2;
				if (this.groups[num].animTarget.IsValid)
				{
					kbatchGroupData2 = KAnimBatchManager.Instance().GetBatchGroupData(this.groups[num].animTarget);
					if (kbatchGroupData2 == null)
					{
						global::Debug.LogErrorFormat("Anim group is null for [{0}] -> [{1}]", new object[]
						{
							this.groups[num].id,
							this.groups[num].animTarget
						});
					}
				}
				else
				{
					kbatchGroupData2 = KAnimBatchManager.Instance().GetBatchGroupData(this.groups[num].id);
					if (kbatchGroupData2 == null)
					{
						global::Debug.LogErrorFormat("Anim group is null for [{0}]", new object[] { this.groups[num].id });
					}
				}
				for (int num2 = 0; num2 < this.groups[num].animFiles.Count; num2++)
				{
					KAnimFile kanimFile2 = this.groups[num].animFiles[num2];
					if (kanimFile2 != null && kanimFile2.animBytes != null)
					{
						if (kanimFile2.animBytes.Length == 0)
						{
							global::Debug.LogWarning("Anim File [" + kanimFile2.GetData().name + "] has 0 bytes");
						}
						else
						{
							if (!this.fileData.ContainsKey(kanimFile2.GetInstanceID()))
							{
								KAnimFileData file2 = KGlobalAnimParser.Get().GetFile(kanimFile2);
								file2.maxVisSymbolFrames = 0;
								file2.batchTag = this.groups[num].id;
								this.fileData.Add(kanimFile2.GetInstanceID(), file2);
							}
							HashedString hashedString3 = new HashedString(kanimFile2.name);
							FastReader fastReader = new FastReader(kanimFile2.animBytes);
							KAnimFileData kanimFileData = this.fileData[kanimFile2.GetInstanceID()];
							KGlobalAnimParser.ParseAnimData(kbatchGroupData2, hashedString3, fastReader, kanimFileData);
						}
					}
				}
			}
		}
		for (int num3 = 0; num3 < this.groups.Count; num3++)
		{
			if (!this.groups[num3].id.IsValid)
			{
				global::Debug.LogErrorFormat("Group invalid groupIndex [{0}]", new object[] { num3 });
			}
			KBatchGroupData kbatchGroupData3;
			if (this.groups[num3].target.IsValid)
			{
				kbatchGroupData3 = KAnimBatchManager.Instance().GetBatchGroupData(this.groups[num3].target);
				if (kbatchGroupData3 == null)
				{
					global::Debug.LogErrorFormat("Group is null for  [{0}] target [{1}]", new object[]
					{
						this.groups[num3].id,
						this.groups[num3].target
					});
				}
			}
			else
			{
				kbatchGroupData3 = KAnimBatchManager.Instance().GetBatchGroupData(this.groups[num3].id);
				if (kbatchGroupData3 == null)
				{
					global::Debug.LogErrorFormat("Group is null for [{0}]", new object[] { this.groups[num3].id });
				}
			}
			KGlobalAnimParser.PostParse(kbatchGroupData3);
		}
	}

	// Token: 0x04000074 RID: 116
	private const string MASTER_GROUP_FILE = "animgrouptags";

	// Token: 0x04000075 RID: 117
	public const int MAX_ANIMS_PER_GROUP = 10;

	// Token: 0x04000076 RID: 118
	private static KAnimGroupFile groupfile;

	// Token: 0x04000077 RID: 119
	private Dictionary<int, KAnimFileData> fileData = new Dictionary<int, KAnimFileData>();

	// Token: 0x04000078 RID: 120
	[SerializeField]
	private List<KAnimGroupFile.Group> groups = new List<KAnimGroupFile.Group>();

	// Token: 0x04000079 RID: 121
	[SerializeField]
	private List<Pair<HashedString, HashedString>> currentGroup = new List<Pair<HashedString, HashedString>>();

	// Token: 0x02000966 RID: 2406
	[Serializable]
	public class Group
	{
		// Token: 0x060052D8 RID: 21208 RVA: 0x0009B20B File Offset: 0x0009940B
		public Group(HashedString tag)
		{
			this.id = tag;
		}

		// Token: 0x04002081 RID: 8321
		[SerializeField]
		public HashedString id;

		// Token: 0x04002082 RID: 8322
		[SerializeField]
		public string commandDirectory = "";

		// Token: 0x04002083 RID: 8323
		[SerializeField]
		public List<HashedString> animNames = new List<HashedString>();

		// Token: 0x04002084 RID: 8324
		[SerializeField]
		public KAnimBatchGroup.RendererType renderType;

		// Token: 0x04002085 RID: 8325
		[SerializeField]
		public int maxVisibleSymbols;

		// Token: 0x04002086 RID: 8326
		[SerializeField]
		public int maxGroupSize;

		// Token: 0x04002087 RID: 8327
		[SerializeField]
		public HashedString target;

		// Token: 0x04002088 RID: 8328
		[SerializeField]
		public HashedString swapTarget;

		// Token: 0x04002089 RID: 8329
		[SerializeField]
		public HashedString animTarget;

		// Token: 0x0400208A RID: 8330
		[NonSerialized]
		public List<KAnimFile> animFiles = new List<KAnimFile>();
	}

	// Token: 0x02000967 RID: 2407
	public class GroupFile
	{
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x060052D9 RID: 21209 RVA: 0x0009B23B File Offset: 0x0009943B
		// (set) Token: 0x060052DA RID: 21210 RVA: 0x0009B243 File Offset: 0x00099443
		public string groupID { get; set; }

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060052DB RID: 21211 RVA: 0x0009B24C File Offset: 0x0009944C
		// (set) Token: 0x060052DC RID: 21212 RVA: 0x0009B254 File Offset: 0x00099454
		public string commandDirectory { get; set; }
	}

	// Token: 0x02000968 RID: 2408
	public enum AddModResult
	{
		// Token: 0x0400208E RID: 8334
		Added,
		// Token: 0x0400208F RID: 8335
		Replaced
	}
}
