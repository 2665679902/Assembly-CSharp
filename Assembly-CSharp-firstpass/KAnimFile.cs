using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class KAnimFile : ScriptableObject
{
	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060000F7 RID: 247 RVA: 0x00006357 File Offset: 0x00004557
	// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000635F File Offset: 0x0000455F
	public bool IsBuildLoaded { get; private set; }

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060000F9 RID: 249 RVA: 0x00006368 File Offset: 0x00004568
	// (set) Token: 0x060000FA RID: 250 RVA: 0x00006370 File Offset: 0x00004570
	public bool IsAnimLoaded { get; private set; }

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060000FB RID: 251 RVA: 0x00006379 File Offset: 0x00004579
	public byte[] animBytes
	{
		get
		{
			if (this.mod != null)
			{
				return this.mod.anim;
			}
			if (!(this.animFile != null))
			{
				return null;
			}
			return this.animFile.bytes;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060000FC RID: 252 RVA: 0x000063AA File Offset: 0x000045AA
	public byte[] buildBytes
	{
		get
		{
			if (this.mod != null)
			{
				return this.mod.build;
			}
			if (!(this.buildFile != null))
			{
				return null;
			}
			return this.buildFile.bytes;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060000FD RID: 253 RVA: 0x000063DB File Offset: 0x000045DB
	public List<Texture2D> textureList
	{
		get
		{
			if (this.mod != null)
			{
				return this.mod.textures;
			}
			return this.textures;
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x000063F7 File Offset: 0x000045F7
	public void FinalizeLoading()
	{
		this.IsBuildLoaded = this.buildBytes != null;
		this.IsAnimLoaded = this.animBytes != null;
		this.animFile = null;
		this.buildFile = null;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00006425 File Offset: 0x00004625
	public void Initialize(TextAsset anim, TextAsset build, IList<Texture2D> textures)
	{
		this.animFile = anim;
		this.buildFile = build;
		this.textures.Clear();
		this.textures.AddRange(textures);
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000100 RID: 256 RVA: 0x0000644C File Offset: 0x0000464C
	public HashedString batchTag
	{
		get
		{
			if (this._batchTag.IsValid)
			{
				return this._batchTag;
			}
			if (this.homedirectory == null || this.homedirectory == "")
			{
				return KAnimBatchManager.NO_BATCH;
			}
			this._batchTag = KAnimGroupFile.GetGroupForHomeDirectory(new HashedString(this.homedirectory));
			return this._batchTag;
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x000064AC File Offset: 0x000046AC
	public KAnimFileData GetData()
	{
		if (this.data == null)
		{
			KGlobalAnimParser kglobalAnimParser = KGlobalAnimParser.Get();
			if (kglobalAnimParser != null)
			{
				this.data = kglobalAnimParser.Load(this);
			}
		}
		return this.data;
	}

	// Token: 0x04000053 RID: 83
	public const string ANIM_ROOT_PATH = "Assets/anim";

	// Token: 0x04000054 RID: 84
	[SerializeField]
	private TextAsset animFile;

	// Token: 0x04000055 RID: 85
	[SerializeField]
	private TextAsset buildFile;

	// Token: 0x04000056 RID: 86
	[SerializeField]
	private List<Texture2D> textures = new List<Texture2D>();

	// Token: 0x04000057 RID: 87
	public KAnimFile.Mod mod;

	// Token: 0x0400005A RID: 90
	private KAnimFileData data;

	// Token: 0x0400005B RID: 91
	private HashedString _batchTag;

	// Token: 0x0400005C RID: 92
	public string homedirectory = "";

	// Token: 0x02000962 RID: 2402
	public class Mod
	{
		// Token: 0x060052D4 RID: 21204 RVA: 0x0009B1D2 File Offset: 0x000993D2
		public bool IsValid()
		{
			return this.anim != null;
		}

		// Token: 0x04002074 RID: 8308
		public byte[] anim;

		// Token: 0x04002075 RID: 8309
		public byte[] build;

		// Token: 0x04002076 RID: 8310
		public List<Texture2D> textures = new List<Texture2D>();
	}
}
