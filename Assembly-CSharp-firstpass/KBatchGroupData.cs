using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class KBatchGroupData
{
	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000143 RID: 323 RVA: 0x0000780C File Offset: 0x00005A0C
	// (set) Token: 0x06000144 RID: 324 RVA: 0x00007814 File Offset: 0x00005A14
	public HashedString groupID { get; private set; }

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000145 RID: 325 RVA: 0x0000781D File Offset: 0x00005A1D
	// (set) Token: 0x06000146 RID: 326 RVA: 0x00007825 File Offset: 0x00005A25
	public bool isSwap { get; private set; }

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000147 RID: 327 RVA: 0x0000782E File Offset: 0x00005A2E
	// (set) Token: 0x06000148 RID: 328 RVA: 0x00007836 File Offset: 0x00005A36
	public int maxVisibleSymbols { get; private set; }

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000149 RID: 329 RVA: 0x0000783F File Offset: 0x00005A3F
	public int maxSymbolsPerBuild
	{
		get
		{
			return this.frameElementSymbols.Count;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600014A RID: 330 RVA: 0x0000784C File Offset: 0x00005A4C
	public int maxSymbolFrameInstancesPerbuild
	{
		get
		{
			return this.symbolFrameInstances.Count;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600014B RID: 331 RVA: 0x00007859 File Offset: 0x00005A59
	public int animDataStartOffset
	{
		get
		{
			return this.symbolFrameInstances.Count * 16;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600014C RID: 332 RVA: 0x00007869 File Offset: 0x00005A69
	// (set) Token: 0x0600014D RID: 333 RVA: 0x00007871 File Offset: 0x00005A71
	public List<KAnim.Anim> anims { get; private set; }

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600014E RID: 334 RVA: 0x0000787A File Offset: 0x00005A7A
	// (set) Token: 0x0600014F RID: 335 RVA: 0x00007882 File Offset: 0x00005A82
	public Dictionary<KAnimHashedString, int> animIndex { get; private set; }

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x06000150 RID: 336 RVA: 0x0000788B File Offset: 0x00005A8B
	// (set) Token: 0x06000151 RID: 337 RVA: 0x00007893 File Offset: 0x00005A93
	public Dictionary<KAnimHashedString, int> animCount { get; private set; }

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06000152 RID: 338 RVA: 0x0000789C File Offset: 0x00005A9C
	// (set) Token: 0x06000153 RID: 339 RVA: 0x000078A4 File Offset: 0x00005AA4
	public List<KAnim.Anim.Frame> animFrames { get; private set; }

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06000154 RID: 340 RVA: 0x000078AD File Offset: 0x00005AAD
	// (set) Token: 0x06000155 RID: 341 RVA: 0x000078B5 File Offset: 0x00005AB5
	public List<KAnim.Anim.FrameElement> frameElements { get; private set; }

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06000156 RID: 342 RVA: 0x000078BE File Offset: 0x00005ABE
	// (set) Token: 0x06000157 RID: 343 RVA: 0x000078C6 File Offset: 0x00005AC6
	public List<KAnim.Build> builds { get; private set; }

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000158 RID: 344 RVA: 0x000078CF File Offset: 0x00005ACF
	// (set) Token: 0x06000159 RID: 345 RVA: 0x000078D7 File Offset: 0x00005AD7
	public List<KAnim.Build.Symbol> frameElementSymbols { get; private set; }

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x0600015A RID: 346 RVA: 0x000078E0 File Offset: 0x00005AE0
	// (set) Token: 0x0600015B RID: 347 RVA: 0x000078E8 File Offset: 0x00005AE8
	public Dictionary<KAnimHashedString, int> frameElementSymbolIndices { get; private set; }

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x0600015C RID: 348 RVA: 0x000078F1 File Offset: 0x00005AF1
	// (set) Token: 0x0600015D RID: 349 RVA: 0x000078F9 File Offset: 0x00005AF9
	public List<KAnim.Build.SymbolFrameInstance> symbolFrameInstances { get; private set; }

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x0600015E RID: 350 RVA: 0x00007902 File Offset: 0x00005B02
	// (set) Token: 0x0600015F RID: 351 RVA: 0x0000790A File Offset: 0x00005B0A
	public Dictionary<KAnimHashedString, int> textureStartIndex { get; private set; }

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x06000160 RID: 352 RVA: 0x00007913 File Offset: 0x00005B13
	// (set) Token: 0x06000161 RID: 353 RVA: 0x0000791B File Offset: 0x00005B1B
	public Dictionary<KAnimHashedString, int> firstSymbolIndex { get; private set; }

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x06000162 RID: 354 RVA: 0x00007924 File Offset: 0x00005B24
	// (set) Token: 0x06000163 RID: 355 RVA: 0x0000792C File Offset: 0x00005B2C
	public List<Texture2D> textures { get; private set; }

	// Token: 0x06000164 RID: 356 RVA: 0x00007935 File Offset: 0x00005B35
	public KBatchGroupData(HashedString id)
	{
		this.groupID = id;
		this.maxVisibleSymbols = 1;
		this.Init();
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00007954 File Offset: 0x00005B54
	private void Init()
	{
		this.anims = new List<KAnim.Anim>();
		this.animIndex = new Dictionary<KAnimHashedString, int>();
		this.animCount = new Dictionary<KAnimHashedString, int>();
		this.animFrames = new List<KAnim.Anim.Frame>();
		this.frameElements = new List<KAnim.Anim.FrameElement>();
		this.builds = new List<KAnim.Build>();
		this.frameElementSymbols = new List<KAnim.Build.Symbol>();
		this.frameElementSymbolIndices = new Dictionary<KAnimHashedString, int>();
		this.symbolFrameInstances = new List<KAnim.Build.SymbolFrameInstance>();
		this.textures = new List<Texture2D>();
		this.textureStartIndex = new Dictionary<KAnimHashedString, int>();
		this.firstSymbolIndex = new Dictionary<KAnimHashedString, int>();
	}

	// Token: 0x06000166 RID: 358 RVA: 0x000079E8 File Offset: 0x00005BE8
	public void FreeResources()
	{
		if (this.anims != null)
		{
			this.anims.Clear();
			this.anims = null;
		}
		if (this.animIndex != null)
		{
			this.animIndex.Clear();
			this.animIndex = null;
		}
		if (this.animCount != null)
		{
			this.animCount.Clear();
			this.animCount = null;
		}
		if (this.animFrames != null)
		{
			this.animFrames.Clear();
			this.animFrames = null;
		}
		if (this.frameElements != null)
		{
			this.frameElements.Clear();
			this.frameElements = null;
		}
		if (this.builds != null)
		{
			this.builds.Clear();
			this.builds = null;
		}
		if (this.frameElementSymbols != null)
		{
			this.frameElementSymbols.Clear();
			this.frameElementSymbols = null;
		}
		if (this.symbolFrameInstances != null)
		{
			this.symbolFrameInstances.Clear();
			this.symbolFrameInstances = null;
		}
		if (this.textures != null)
		{
			this.textures.Clear();
			this.textures = null;
		}
		if (this.textureStartIndex != null)
		{
			this.textureStartIndex.Clear();
			this.textureStartIndex = null;
		}
		if (this.firstSymbolIndex != null)
		{
			this.firstSymbolIndex.Clear();
			this.firstSymbolIndex = null;
		}
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00007B14 File Offset: 0x00005D14
	public KAnim.Build AddNewBuildFile(KAnimHashedString fileHash)
	{
		this.textureStartIndex.Add(fileHash, this.textures.Count);
		this.firstSymbolIndex.Add(fileHash, this.GetSymbolCount());
		KAnim.Build build = new KAnim.Build();
		build.textureStartIdx = this.textures.Count;
		build.fileHash = fileHash;
		build.index = this.builds.Count;
		this.builds.Add(build);
		return build;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00007B86 File Offset: 0x00005D86
	public void AddTextures(List<Texture2D> buildtextures)
	{
		this.textures.AddRange(buildtextures);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00007B94 File Offset: 0x00005D94
	public void AddAnim(KAnim.Anim anim)
	{
		global::Debug.Assert(anim.index == this.anims.Count);
		this.anims.Add(anim);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00007BBC File Offset: 0x00005DBC
	public KAnim.Anim GetAnim(int anim)
	{
		if (anim < 0 || anim >= this.anims.Count)
		{
			global::Debug.LogError(string.Format("Anim [{0}] out of range [{1}] in batch [{2}]", anim, this.anims.Count, this.groupID));
		}
		return this.anims[anim];
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00007C17 File Offset: 0x00005E17
	public KAnim.Build GetBuild(int index)
	{
		return this.builds[index];
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00007C25 File Offset: 0x00005E25
	public void UpdateMaxVisibleSymbols(int newCount)
	{
		this.maxVisibleSymbols = Mathf.Min(120, Mathf.Max(this.maxVisibleSymbols, newCount));
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00007C40 File Offset: 0x00005E40
	public KAnim.Build.Symbol GetSymbol(KAnimHashedString symbol_name)
	{
		int num = 0;
		if (!this.frameElementSymbolIndices.TryGetValue(symbol_name, out num))
		{
			return null;
		}
		return this.frameElementSymbols[num];
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00007C6D File Offset: 0x00005E6D
	public KAnim.Build.Symbol GetSymbol(int index)
	{
		if (index >= 0 && index < this.frameElementSymbols.Count)
		{
			return this.frameElementSymbols[index];
		}
		return null;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00007C8F File Offset: 0x00005E8F
	public void AddBuildSymbol(KAnim.Build.Symbol symbol)
	{
		if (!this.frameElementSymbolIndices.ContainsKey(symbol.hash))
		{
			this.frameElementSymbolIndices.Add(symbol.hash, this.frameElementSymbols.Count);
		}
		this.frameElementSymbols.Add(symbol);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00007CCC File Offset: 0x00005ECC
	public int GetSymbolCount()
	{
		return this.frameElementSymbols.Count;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00007CDC File Offset: 0x00005EDC
	public KAnim.Build.SymbolFrameInstance GetSymbolFrameInstance(int index)
	{
		if (index >= 0 && index < this.symbolFrameInstances.Count)
		{
			return this.symbolFrameInstances[index];
		}
		return new KAnim.Build.SymbolFrameInstance
		{
			symbolIdx = -1
		};
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00007D19 File Offset: 0x00005F19
	public Texture2D GetTexure(int index)
	{
		if (index < 0 || this.textures == null || index >= this.textures.Count)
		{
			return null;
		}
		return this.textures[index];
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00007D43 File Offset: 0x00005F43
	public KAnim.Build.Symbol GetBuildSymbol(int idx)
	{
		if (this.frameElementSymbols == null || idx < 0 || idx >= this.frameElementSymbols.Count)
		{
			return null;
		}
		return this.frameElementSymbols[idx];
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00007D6D File Offset: 0x00005F6D
	public KAnim.Anim.Frame GetFrame(int index)
	{
		if (index < 0 || index >= this.animFrames.Count)
		{
			return KAnim.Anim.Frame.InvalidFrame;
		}
		return this.animFrames[index];
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00007D93 File Offset: 0x00005F93
	public KAnim.Anim.FrameElement GetFrameElement(int index)
	{
		return this.frameElements[index];
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00007DA1 File Offset: 0x00005FA1
	public List<KAnim.Anim.Frame> GetAnimFrames()
	{
		return this.animFrames;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00007DA9 File Offset: 0x00005FA9
	public List<KAnim.Anim.FrameElement> GetAnimFrameElements()
	{
		return this.frameElements;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00007DB1 File Offset: 0x00005FB1
	public int GetBuildSymbolFrameCount()
	{
		return this.symbolFrameInstances.Count;
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00007DC0 File Offset: 0x00005FC0
	public void WriteAnimData(int start_index, NativeArray<float> data)
	{
		List<KAnim.Anim.Frame> animFrames = this.GetAnimFrames();
		List<KAnim.Anim.FrameElement> animFrameElements = this.GetAnimFrameElements();
		int num = 1 + ((animFrames.Count == 0) ? this.symbolFrameInstances.Count : animFrames.Count);
		if (animFrames.Count == 0 && this.symbolFrameInstances.Count == 0 && animFrameElements.Count == 0)
		{
			global::Debug.LogError(string.Concat(new string[]
			{
				"Eh, no data ",
				animFrames.Count.ToString(),
				" ",
				this.symbolFrameInstances.Count.ToString(),
				" ",
				animFrameElements.Count.ToString()
			}));
		}
		data[start_index++] = (float)num;
		data[start_index++] = (float)animFrames.Count;
		data[start_index++] = (float)animFrameElements.Count;
		data[start_index++] = (float)this.symbolFrameInstances.Count;
		if (animFrames.Count == 0)
		{
			for (int i = 0; i < this.symbolFrameInstances.Count; i++)
			{
				this.WriteAnimFrame(data, start_index, i, i, 1, i);
				start_index += 4;
			}
			for (int j = 0; j < this.symbolFrameInstances.Count; j++)
			{
				this.WriteAnimFrameElement(data, start_index, j, j, Matrix2x3.identity, Color.white);
				start_index += 12;
			}
			return;
		}
		for (int k = 0; k < animFrames.Count; k++)
		{
			this.Write(data, start_index, k, animFrames[k]);
			start_index += 4;
		}
		for (int l = 0; l < animFrameElements.Count; l++)
		{
			KAnim.Anim.FrameElement frameElement = animFrameElements[l];
			if (frameElement.symbol == KGlobalAnimParser.MISSING_SYMBOL)
			{
				this.WriteAnimFrameElement(data, start_index, -1, l, Matrix2x3.identity, Color.white);
			}
			else
			{
				KAnim.Build.Symbol buildSymbol = this.GetBuildSymbol(frameElement.symbolIdx);
				if (buildSymbol == null)
				{
					string[] array = new string[5];
					array[0] = "Missing symbol for Anim Frame Element: [";
					array[1] = HashCache.Get().Get(frameElement.symbol);
					array[2] = ": ";
					int num2 = 3;
					KAnimHashedString symbol = frameElement.symbol;
					array[num2] = symbol.ToString();
					array[4] = "]";
					global::Debug.LogError(string.Concat(array));
				}
				int frameIdx = buildSymbol.GetFrameIdx(frameElement.frame);
				this.Write(data, start_index, frameIdx, l, frameElement);
			}
			start_index += 12;
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00008040 File Offset: 0x00006240
	public int GetFirstIndex(KAnimHashedString symbol)
	{
		return this.frameElementSymbols.FindIndex((KAnim.Build.Symbol fes) => fes.hash == symbol);
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00008074 File Offset: 0x00006274
	public int GetSymbolIndex(KAnimHashedString symbol)
	{
		int num = 0;
		if (!this.frameElementSymbolIndices.TryGetValue(symbol, out num))
		{
			return -1;
		}
		return num;
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00008098 File Offset: 0x00006298
	public int WriteBuildData(List<KAnim.Build.SymbolFrameInstance> symbol_frame_instances, NativeArray<float> data)
	{
		int i;
		for (i = 0; i < symbol_frame_instances.Count; i++)
		{
			this.Write(data, i * 16, i, this.symbolFrameInstances[i].buildImageIdx, symbol_frame_instances[i]);
		}
		return i * 16;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x000080E0 File Offset: 0x000062E0
	private void Write(NativeArray<float> data, int startIndex, int thisFrameIndex, int atlasIndex, KAnim.Build.SymbolFrameInstance symbol_frame_instance)
	{
		data[startIndex++] = (float)atlasIndex;
		data[startIndex++] = (float)thisFrameIndex;
		data[startIndex++] = (float)symbol_frame_instance.symbolIdx;
		KAnim.Build.SymbolFrame symbolFrame = symbol_frame_instance.symbolFrame;
		KAnim.Build.Symbol buildSymbol = this.GetBuildSymbol(symbol_frame_instance.symbolIdx);
		if (buildSymbol == null || symbolFrame == null)
		{
			data[startIndex++] = 0f;
			data[startIndex++] = 0f;
			data[startIndex++] = 0f;
			data[startIndex++] = 0f;
		}
		else
		{
			data[startIndex++] = (float)buildSymbol.numFrames;
			data[startIndex++] = (float)buildSymbol.flags;
			if (this.firstSymbolIndex.ContainsKey(buildSymbol.build.fileHash))
			{
				data[startIndex++] = (float)this.firstSymbolIndex[buildSymbol.build.fileHash];
			}
			else
			{
				data[startIndex++] = 0f;
			}
			data[startIndex++] = (float)buildSymbol.symbolIndexInSourceBuild;
		}
		data[startIndex++] = 3.452817E+09f;
		if (symbolFrame == null)
		{
			return;
		}
		data[startIndex++] = symbolFrame.bboxMin.x;
		data[startIndex++] = symbolFrame.bboxMin.y;
		data[startIndex++] = symbolFrame.bboxMax.x;
		data[startIndex++] = symbolFrame.bboxMax.y;
		data[startIndex++] = symbolFrame.uvMin.x;
		data[startIndex++] = symbolFrame.uvMin.y;
		data[startIndex++] = symbolFrame.uvMax.x;
		data[startIndex++] = symbolFrame.uvMax.y;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000082F0 File Offset: 0x000064F0
	private void WriteAnimFrame(NativeArray<float> data, int startIndex, int firstElementIdx, int idx, int numElements, int thisFrameIndex)
	{
		data[startIndex++] = (float)firstElementIdx;
		data[startIndex++] = (float)numElements;
		data[startIndex++] = (float)thisFrameIndex;
		data[startIndex++] = (float)idx;
	}

	// Token: 0x0600017F RID: 383 RVA: 0x0000833C File Offset: 0x0000653C
	private void Write(NativeArray<float> data, int startIndex, int thisFrameIndex, KAnim.Anim.Frame frame)
	{
		this.WriteAnimFrame(data, startIndex, frame.firstElementIdx, frame.idx, frame.numElements, thisFrameIndex);
	}

	// Token: 0x06000180 RID: 384 RVA: 0x0000835C File Offset: 0x0000655C
	private void WriteAnimFrameElement(NativeArray<float> data, int startIndex, int symbolFrameIdx, int thisFrameIndex, Matrix2x3 transform, Color colour)
	{
		if (symbolFrameIdx != -1010)
		{
			data[startIndex++] = (float)symbolFrameIdx;
			data[startIndex++] = (float)thisFrameIndex;
			data[startIndex++] = colour.r;
			data[startIndex++] = colour.g;
			data[startIndex++] = colour.b;
			data[startIndex++] = colour.a;
			data[startIndex++] = transform.m00;
			data[startIndex++] = transform.m01;
			data[startIndex++] = transform.m02;
			data[startIndex++] = transform.m10;
			data[startIndex++] = transform.m11;
			data[startIndex++] = transform.m12;
			return;
		}
		data[startIndex++] = (float)symbolFrameIdx;
		data[startIndex++] = (float)thisFrameIndex;
		data[startIndex++] = colour.r;
		data[startIndex++] = colour.g;
		data[startIndex++] = colour.b;
		data[startIndex++] = colour.a;
		data[startIndex++] = 0f;
		data[startIndex++] = 0f;
		data[startIndex++] = 0f;
		data[startIndex++] = 0f;
		data[startIndex++] = 0f;
		data[startIndex++] = 0f;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00008537 File Offset: 0x00006737
	private void Write(NativeArray<float> data, int startIndex, int symbolFrameIdx, int thisFrameIndex, KAnim.Anim.FrameElement element)
	{
		this.WriteAnimFrameElement(data, startIndex, symbolFrameIdx, thisFrameIndex, element.transform, element.multColour);
	}

	// Token: 0x0400007B RID: 123
	public const int SIZE_OF_SYMBOL_FRAME_ELEMENT = 16;

	// Token: 0x0400007C RID: 124
	public const int SIZE_OF_ANIM_FRAME = 4;

	// Token: 0x0400007D RID: 125
	public const int SIZE_OF_ANIM_FRAME_ELEMENT = 12;

	// Token: 0x0400007E RID: 126
	private const int MAX_VISIBLE_SYMBOLS = 120;

	// Token: 0x0400007F RID: 127
	public const int MAX_GROUP_SIZE = 30;

	// Token: 0x0400008F RID: 143
	private const int NULL_DATA_FRAME_ID = -1010;
}
