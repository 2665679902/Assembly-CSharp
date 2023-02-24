using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class KAnimBatchManager
{
	// Token: 0x1700002B RID: 43
	// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005B5A File Offset: 0x00003D5A
	// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005B62 File Offset: 0x00003D62
	public int dirtyBatchLastFrame { get; private set; }

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005B6B File Offset: 0x00003D6B
	public static KAnimBatchManager instance
	{
		get
		{
			return Singleton<KAnimBatchManager>.Instance;
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00005B72 File Offset: 0x00003D72
	public static void CreateInstance()
	{
		Singleton<KAnimBatchManager>.CreateInstance();
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00005B79 File Offset: 0x00003D79
	public static KAnimBatchManager Instance()
	{
		return KAnimBatchManager.instance;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00005B80 File Offset: 0x00003D80
	public static void DestroyInstance()
	{
		if (KAnimBatchManager.instance != null)
		{
			KAnimBatchManager.instance.ready = false;
			foreach (KeyValuePair<BatchGroupKey, KAnimBatchGroup> keyValuePair in KAnimBatchManager.instance.batchGroups)
			{
				keyValuePair.Value.FreeResources();
			}
			KAnimBatchManager.instance.batchGroups.Clear();
			foreach (KeyValuePair<HashedString, KBatchGroupData> keyValuePair2 in KAnimBatchManager.instance.batchGroupData)
			{
				if (keyValuePair2.Value != null)
				{
					keyValuePair2.Value.FreeResources();
				}
			}
			KAnimBatchManager.instance.batchGroupData.Clear();
			foreach (KeyValuePair<BatchKey, BatchSet> keyValuePair3 in KAnimBatchManager.instance.batchSets)
			{
				if (keyValuePair3.Value != null)
				{
					keyValuePair3.Value.Clear();
				}
			}
			KAnimBatchManager.instance.batchSets.Clear();
			KAnimBatchManager.instance.culledBatchSetInfos.Clear();
			KAnimBatchManager.instance.uiBatchSets.Clear();
			KAnimBatchManager.instance.activeBatchSets.Clear();
			KAnimBatchManager.instance.dirtyBatchLastFrame = 0;
			KAnimBatchGroup.FinalizeTextureCache();
		}
		Singleton<KAnimBatchManager>.DestroyInstance();
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x060000EA RID: 234 RVA: 0x00005D0C File Offset: 0x00003F0C
	public bool isReady
	{
		get
		{
			return this.ready;
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00005D14 File Offset: 0x00003F14
	public KBatchGroupData GetBatchGroupData(HashedString groupID)
	{
		if (!groupID.IsValid || groupID == KAnimBatchManager.NO_BATCH || groupID == KAnimBatchManager.IGNORE)
		{
			return null;
		}
		KBatchGroupData kbatchGroupData = null;
		if (!this.batchGroupData.TryGetValue(groupID, out kbatchGroupData))
		{
			kbatchGroupData = new KBatchGroupData(groupID);
			this.batchGroupData[groupID] = kbatchGroupData;
		}
		return kbatchGroupData;
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00005D70 File Offset: 0x00003F70
	public KAnimBatchGroup GetBatchGroup(BatchGroupKey group_key)
	{
		KAnimBatchGroup kanimBatchGroup = null;
		if (!this.batchGroups.TryGetValue(group_key, out kanimBatchGroup))
		{
			kanimBatchGroup = new KAnimBatchGroup(group_key.groupID);
			this.batchGroups.Add(group_key, kanimBatchGroup);
		}
		return kanimBatchGroup;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00005DAA File Offset: 0x00003FAA
	public static Vector2I CellXYToChunkXY(Vector2I cell_xy)
	{
		return new Vector2I(cell_xy.x / 32, cell_xy.y / 32);
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00005DC3 File Offset: 0x00003FC3
	public static Vector2I ControllerToChunkXY(KAnimConverter.IAnimConverter controller)
	{
		return KAnimBatchManager.CellXYToChunkXY(controller.GetCellXY());
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00005DD0 File Offset: 0x00003FD0
	public void Register(KAnimConverter.IAnimConverter controller)
	{
		if (!this.isReady)
		{
			global::Debug.LogError(string.Format("Batcher isnt finished setting up, controller [{0}] is registering too early.", controller.GetName()));
		}
		BatchKey batchKey = BatchKey.Create(controller);
		Vector2I vector2I = KAnimBatchManager.ControllerToChunkXY(controller);
		BatchSet batchSet;
		if (!this.batchSets.TryGetValue(batchKey, out batchSet))
		{
			batchSet = new BatchSet(this.GetBatchGroup(new BatchGroupKey(batchKey.groupID)), batchKey, vector2I);
			this.batchSets[batchKey] = batchSet;
			if (batchSet.key.materialType == KAnimBatchGroup.MaterialType.UI)
			{
				this.uiBatchSets.Add(new KAnimBatchManager.BatchSetInfo
				{
					batchSet = batchSet,
					isActive = false,
					spatialIdx = vector2I
				});
			}
			else
			{
				this.culledBatchSetInfos.Add(new KAnimBatchManager.BatchSetInfo
				{
					batchSet = batchSet,
					isActive = false,
					spatialIdx = vector2I
				});
			}
		}
		batchSet.Add(controller);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00005EB8 File Offset: 0x000040B8
	public void UpdateActiveArea(Vector2I vis_chunk_min, Vector2I vis_chunk_max)
	{
		this.activeBatchSets.Clear();
		for (int i = 0; i < this.uiBatchSets.Count; i++)
		{
			KAnimBatchManager.BatchSetInfo batchSetInfo = this.uiBatchSets[i];
			this.activeBatchSets.Add(batchSetInfo.batchSet);
			if (!batchSetInfo.isActive)
			{
				batchSetInfo.isActive = true;
				batchSetInfo.batchSet.SetActive(true);
				this.uiBatchSets[i] = batchSetInfo;
			}
		}
		for (int j = 0; j < this.culledBatchSetInfos.Count; j++)
		{
			KAnimBatchManager.BatchSetInfo batchSetInfo2 = this.culledBatchSetInfos[j];
			if (batchSetInfo2.spatialIdx.x >= vis_chunk_min.x && batchSetInfo2.spatialIdx.x <= vis_chunk_max.x && batchSetInfo2.spatialIdx.y >= vis_chunk_min.y && batchSetInfo2.spatialIdx.y <= vis_chunk_max.y)
			{
				this.activeBatchSets.Add(batchSetInfo2.batchSet);
				if (!batchSetInfo2.isActive)
				{
					batchSetInfo2.isActive = true;
					this.culledBatchSetInfos[j] = batchSetInfo2;
					batchSetInfo2.batchSet.SetActive(true);
				}
			}
			else if (batchSetInfo2.isActive)
			{
				batchSetInfo2.isActive = false;
				this.culledBatchSetInfos[j] = batchSetInfo2;
				batchSetInfo2.batchSet.SetActive(false);
			}
		}
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00006008 File Offset: 0x00004208
	public int UpdateDirty(int frame)
	{
		if (!this.ready)
		{
			return 0;
		}
		this.dirtyBatchLastFrame = 0;
		foreach (BatchSet batchSet in this.activeBatchSets)
		{
			this.dirtyBatchLastFrame += batchSet.UpdateDirty(frame);
		}
		return this.dirtyBatchLastFrame;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x00006080 File Offset: 0x00004280
	public void Render()
	{
		if (!this.ready)
		{
			return;
		}
		foreach (BatchSet batchSet in this.activeBatchSets)
		{
			DebugUtil.Assert(batchSet != null);
			DebugUtil.Assert(batchSet.group != null);
			Mesh mesh = batchSet.group.mesh;
			for (int i = 0; i < batchSet.batchCount; i++)
			{
				KAnimBatch batch = batchSet.GetBatch(i);
				float num = 0.01f / (float)(1 + batch.id % 256);
				if (batch.size != 0 && batch.active && batch.materialType != KAnimBatchGroup.MaterialType.UI)
				{
					Vector3 zero = Vector3.zero;
					zero.z = batch.position.z + num;
					int layer = batch.layer;
					Graphics.DrawMesh(mesh, zero, Quaternion.identity, batchSet.group.GetMaterial(batch.materialType), layer, null, 0, batch.matProperties);
				}
			}
		}
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x000061A4 File Offset: 0x000043A4
	public void CompleteInit()
	{
		this.ready = true;
	}

	// Token: 0x04000043 RID: 67
	private const int DEFAULT_BATCH_SIZE = 30;

	// Token: 0x04000044 RID: 68
	public const int CHUNK_SIZE = 32;

	// Token: 0x04000045 RID: 69
	public static HashedString NO_BATCH = new HashedString("NO_BATCH");

	// Token: 0x04000046 RID: 70
	public static HashedString IGNORE = new HashedString("IGNORE");

	// Token: 0x04000047 RID: 71
	public static HashedString BATCH_HUMAN = "human";

	// Token: 0x04000048 RID: 72
	public static Vector2 GROUP_SIZE = new Vector2(32f, 32f);

	// Token: 0x04000049 RID: 73
	private bool ready;

	// Token: 0x0400004B RID: 75
	private Dictionary<HashedString, KBatchGroupData> batchGroupData = new Dictionary<HashedString, KBatchGroupData>();

	// Token: 0x0400004C RID: 76
	private Dictionary<BatchGroupKey, KAnimBatchGroup> batchGroups = new Dictionary<BatchGroupKey, KAnimBatchGroup>();

	// Token: 0x0400004D RID: 77
	private Dictionary<BatchKey, BatchSet> batchSets = new Dictionary<BatchKey, BatchSet>();

	// Token: 0x0400004E RID: 78
	private List<KAnimBatchManager.BatchSetInfo> culledBatchSetInfos = new List<KAnimBatchManager.BatchSetInfo>();

	// Token: 0x0400004F RID: 79
	private List<KAnimBatchManager.BatchSetInfo> uiBatchSets = new List<KAnimBatchManager.BatchSetInfo>();

	// Token: 0x04000050 RID: 80
	private List<BatchSet> activeBatchSets = new List<BatchSet>();

	// Token: 0x04000051 RID: 81
	public static int[] AtlasNames = new int[]
	{
		Shader.PropertyToID("atlas0"),
		Shader.PropertyToID("atlas1"),
		Shader.PropertyToID("atlas2"),
		Shader.PropertyToID("atlas3"),
		Shader.PropertyToID("atlas4"),
		Shader.PropertyToID("atlas5"),
		Shader.PropertyToID("atlas6"),
		Shader.PropertyToID("atlas7"),
		Shader.PropertyToID("atlas8"),
		Shader.PropertyToID("atlas9"),
		Shader.PropertyToID("atlas10"),
		Shader.PropertyToID("atlas11"),
		Shader.PropertyToID("atlas12"),
		Shader.PropertyToID("atlas13"),
		Shader.PropertyToID("atlas14"),
		Shader.PropertyToID("atlas15")
	};

	// Token: 0x04000052 RID: 82
	public static int[] MaxAtlasesByMaterialType = new int[] { 12, 12, 12, 16, 12, 13 };

	// Token: 0x02000960 RID: 2400
	private struct BatchSetInfo
	{
		// Token: 0x04002071 RID: 8305
		public BatchSet batchSet;

		// Token: 0x04002072 RID: 8306
		public Vector2I spatialIdx;

		// Token: 0x04002073 RID: 8307
		public bool isActive;
	}
}
