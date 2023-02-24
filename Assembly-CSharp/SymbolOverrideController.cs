using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200042C RID: 1068
[AddComponentMenu("KMonoBehaviour/scripts/SymbolOverrideController")]
public class SymbolOverrideController : KMonoBehaviour
{
	// Token: 0x170000BF RID: 191
	// (get) Token: 0x060016FD RID: 5885 RVA: 0x00077330 File Offset: 0x00075530
	public SymbolOverrideController.SymbolEntry[] GetSymbolOverrides
	{
		get
		{
			return this.symbolOverrides.ToArray();
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x060016FE RID: 5886 RVA: 0x0007733D File Offset: 0x0007553D
	// (set) Token: 0x060016FF RID: 5887 RVA: 0x00077345 File Offset: 0x00075545
	public int version { get; private set; }

	// Token: 0x06001700 RID: 5888 RVA: 0x00077350 File Offset: 0x00075550
	protected override void OnPrefabInit()
	{
		this.animController = base.GetComponent<KBatchedAnimController>();
		DebugUtil.Assert(base.GetComponent<KBatchedAnimController>() != null, "SymbolOverrideController requires KBatchedAnimController");
		DebugUtil.Assert(base.GetComponent<KBatchedAnimController>().usingNewSymbolOverrideSystem, "SymbolOverrideController requires usingNewSymbolOverrideSystem to be set to true. Try adding the component by calling: SymbolOverrideControllerUtil.AddToPrefab");
		for (int i = 0; i < this.symbolOverrides.Count; i++)
		{
			SymbolOverrideController.SymbolEntry symbolEntry = this.symbolOverrides[i];
			symbolEntry.sourceSymbol = KAnimBatchManager.Instance().GetBatchGroupData(symbolEntry.sourceSymbolBatchTag).GetSymbol(symbolEntry.sourceSymbolId);
			this.symbolOverrides[i] = symbolEntry;
		}
		this.atlases = new KAnimBatch.AtlasList(0, KAnimBatchManager.MaxAtlasesByMaterialType[(int)this.animController.materialType]);
		this.faceGraph = base.GetComponent<FaceGraph>();
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x00077414 File Offset: 0x00075614
	public int AddSymbolOverride(HashedString target_symbol, KAnim.Build.Symbol source_symbol, int priority = 0)
	{
		if (source_symbol == null)
		{
			throw new Exception("NULL source symbol when overriding: " + target_symbol.ToString());
		}
		SymbolOverrideController.SymbolEntry symbolEntry = new SymbolOverrideController.SymbolEntry
		{
			targetSymbol = target_symbol,
			sourceSymbol = source_symbol,
			sourceSymbolId = new HashedString(source_symbol.hash.HashValue),
			sourceSymbolBatchTag = source_symbol.build.batchTag,
			priority = priority
		};
		int num = this.GetSymbolOverrideIdx(target_symbol, priority);
		if (num >= 0)
		{
			this.symbolOverrides[num] = symbolEntry;
		}
		else
		{
			num = this.symbolOverrides.Count;
			this.symbolOverrides.Add(symbolEntry);
		}
		this.MarkDirty();
		return num;
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x000774C8 File Offset: 0x000756C8
	public bool RemoveSymbolOverride(HashedString target_symbol, int priority = 0)
	{
		for (int i = 0; i < this.symbolOverrides.Count; i++)
		{
			SymbolOverrideController.SymbolEntry symbolEntry = this.symbolOverrides[i];
			if (symbolEntry.targetSymbol == target_symbol && symbolEntry.priority == priority)
			{
				this.symbolOverrides.RemoveAt(i);
				this.MarkDirty();
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001703 RID: 5891 RVA: 0x00077524 File Offset: 0x00075724
	public void RemoveAllSymbolOverrides(int priority = 0)
	{
		this.symbolOverrides.RemoveAll((SymbolOverrideController.SymbolEntry x) => x.priority >= priority);
		this.MarkDirty();
	}

	// Token: 0x06001704 RID: 5892 RVA: 0x0007755C File Offset: 0x0007575C
	public int GetSymbolOverrideIdx(HashedString target_symbol, int priority = 0)
	{
		for (int i = 0; i < this.symbolOverrides.Count; i++)
		{
			SymbolOverrideController.SymbolEntry symbolEntry = this.symbolOverrides[i];
			if (symbolEntry.targetSymbol == target_symbol && symbolEntry.priority == priority)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x000775A6 File Offset: 0x000757A6
	public int GetAtlasIdx(Texture2D atlas)
	{
		return this.atlases.GetAtlasIdx(atlas);
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x000775B4 File Offset: 0x000757B4
	public void ApplyOverrides()
	{
		if (this.requiresSorting)
		{
			this.symbolOverrides.Sort((SymbolOverrideController.SymbolEntry x, SymbolOverrideController.SymbolEntry y) => x.priority - y.priority);
			this.requiresSorting = false;
		}
		KAnimBatch batch = this.animController.GetBatch();
		DebugUtil.Assert(batch != null);
		KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(this.animController.batchGroupID);
		int count = batch.atlases.Count;
		this.atlases.Clear(count);
		DictionaryPool<HashedString, Pair<int, int>, SymbolOverrideController>.PooledDictionary pooledDictionary = DictionaryPool<HashedString, Pair<int, int>, SymbolOverrideController>.Allocate();
		ListPool<SymbolOverrideController.SymbolEntry, SymbolOverrideController>.PooledList pooledList = ListPool<SymbolOverrideController.SymbolEntry, SymbolOverrideController>.Allocate();
		for (int i = 0; i < this.symbolOverrides.Count; i++)
		{
			SymbolOverrideController.SymbolEntry symbolEntry = this.symbolOverrides[i];
			Pair<int, int> pair;
			if (pooledDictionary.TryGetValue(symbolEntry.targetSymbol, out pair))
			{
				int first = pair.first;
				if (symbolEntry.priority > first)
				{
					int second = pair.second;
					pooledDictionary[symbolEntry.targetSymbol] = new Pair<int, int>(symbolEntry.priority, second);
					pooledList[second] = symbolEntry;
				}
			}
			else
			{
				pooledDictionary[symbolEntry.targetSymbol] = new Pair<int, int>(symbolEntry.priority, pooledList.Count);
				pooledList.Add(symbolEntry);
			}
		}
		DictionaryPool<KAnim.Build, SymbolOverrideController.BatchGroupInfo, SymbolOverrideController>.PooledDictionary pooledDictionary2 = DictionaryPool<KAnim.Build, SymbolOverrideController.BatchGroupInfo, SymbolOverrideController>.Allocate();
		for (int j = 0; j < pooledList.Count; j++)
		{
			SymbolOverrideController.SymbolEntry symbolEntry2 = pooledList[j];
			SymbolOverrideController.BatchGroupInfo batchGroupInfo;
			if (!pooledDictionary2.TryGetValue(symbolEntry2.sourceSymbol.build, out batchGroupInfo))
			{
				batchGroupInfo = new SymbolOverrideController.BatchGroupInfo
				{
					build = symbolEntry2.sourceSymbol.build,
					data = KAnimBatchManager.Instance().GetBatchGroupData(symbolEntry2.sourceSymbol.build.batchTag)
				};
				Texture2D texture = symbolEntry2.sourceSymbol.build.GetTexture(0);
				int num = batch.atlases.GetAtlasIdx(texture);
				if (num < 0)
				{
					num = this.atlases.Add(texture);
				}
				batchGroupInfo.atlasIdx = num;
				pooledDictionary2[batchGroupInfo.build] = batchGroupInfo;
			}
			KAnim.Build.Symbol symbol = batchGroupData.GetSymbol(symbolEntry2.targetSymbol);
			if (symbol != null)
			{
				this.animController.SetSymbolOverrides(symbol.firstFrameIdx, symbol.numFrames, batchGroupInfo.atlasIdx, batchGroupInfo.data, symbolEntry2.sourceSymbol.firstFrameIdx, symbolEntry2.sourceSymbol.numFrames);
			}
		}
		pooledDictionary2.Recycle();
		pooledList.Recycle();
		pooledDictionary.Recycle();
		if (this.faceGraph != null)
		{
			this.faceGraph.ApplyShape();
		}
	}

	// Token: 0x06001707 RID: 5895 RVA: 0x00077850 File Offset: 0x00075A50
	public void ApplyAtlases()
	{
		KAnimBatch batch = this.animController.GetBatch();
		this.atlases.Apply(batch.matProperties);
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x0007787A File Offset: 0x00075A7A
	public KAnimBatch.AtlasList GetAtlasList()
	{
		return this.atlases;
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x00077884 File Offset: 0x00075A84
	public void MarkDirty()
	{
		if (this.animController != null)
		{
			this.animController.SetDirty();
		}
		int num = this.version + 1;
		this.version = num;
		this.requiresSorting = true;
	}

	// Token: 0x04000CBC RID: 3260
	public bool applySymbolOverridesEveryFrame;

	// Token: 0x04000CBD RID: 3261
	[SerializeField]
	private List<SymbolOverrideController.SymbolEntry> symbolOverrides = new List<SymbolOverrideController.SymbolEntry>();

	// Token: 0x04000CBE RID: 3262
	private KAnimBatch.AtlasList atlases;

	// Token: 0x04000CBF RID: 3263
	private KBatchedAnimController animController;

	// Token: 0x04000CC0 RID: 3264
	private FaceGraph faceGraph;

	// Token: 0x04000CC2 RID: 3266
	private bool requiresSorting;

	// Token: 0x0200104A RID: 4170
	[Serializable]
	public struct SymbolEntry
	{
		// Token: 0x040056E8 RID: 22248
		public HashedString targetSymbol;

		// Token: 0x040056E9 RID: 22249
		[NonSerialized]
		public KAnim.Build.Symbol sourceSymbol;

		// Token: 0x040056EA RID: 22250
		public HashedString sourceSymbolId;

		// Token: 0x040056EB RID: 22251
		public HashedString sourceSymbolBatchTag;

		// Token: 0x040056EC RID: 22252
		public int priority;
	}

	// Token: 0x0200104B RID: 4171
	private struct SymbolToOverride
	{
		// Token: 0x040056ED RID: 22253
		public KAnim.Build.Symbol sourceSymbol;

		// Token: 0x040056EE RID: 22254
		public HashedString targetSymbol;

		// Token: 0x040056EF RID: 22255
		public KBatchGroupData data;

		// Token: 0x040056F0 RID: 22256
		public int atlasIdx;
	}

	// Token: 0x0200104C RID: 4172
	private class BatchGroupInfo
	{
		// Token: 0x040056F1 RID: 22257
		public KAnim.Build build;

		// Token: 0x040056F2 RID: 22258
		public int atlasIdx;

		// Token: 0x040056F3 RID: 22259
		public KBatchGroupData data;
	}
}
