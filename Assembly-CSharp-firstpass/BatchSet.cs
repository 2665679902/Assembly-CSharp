using System;
using System.Collections.Generic;

// Token: 0x0200000A RID: 10
public class BatchSet
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000077 RID: 119 RVA: 0x0000430B File Offset: 0x0000250B
	// (set) Token: 0x06000078 RID: 120 RVA: 0x00004313 File Offset: 0x00002513
	public KAnimBatchGroup group { get; private set; }

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000079 RID: 121 RVA: 0x0000431C File Offset: 0x0000251C
	// (set) Token: 0x0600007A RID: 122 RVA: 0x00004324 File Offset: 0x00002524
	private protected List<KAnimBatch> batches { protected get; private set; }

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600007B RID: 123 RVA: 0x0000432D File Offset: 0x0000252D
	// (set) Token: 0x0600007C RID: 124 RVA: 0x00004335 File Offset: 0x00002535
	public Vector2I idx { get; private set; }

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x0600007D RID: 125 RVA: 0x0000433E File Offset: 0x0000253E
	// (set) Token: 0x0600007E RID: 126 RVA: 0x00004346 File Offset: 0x00002546
	public BatchKey key { get; private set; }

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600007F RID: 127 RVA: 0x0000434F File Offset: 0x0000254F
	// (set) Token: 0x06000080 RID: 128 RVA: 0x00004357 File Offset: 0x00002557
	public bool dirty { get; private set; }

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000081 RID: 129 RVA: 0x00004360 File Offset: 0x00002560
	// (set) Token: 0x06000082 RID: 130 RVA: 0x00004368 File Offset: 0x00002568
	public bool active { get; private set; }

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000083 RID: 131 RVA: 0x00004371 File Offset: 0x00002571
	public int batchCount
	{
		get
		{
			return this.batches.Count;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000084 RID: 132 RVA: 0x0000437E File Offset: 0x0000257E
	// (set) Token: 0x06000085 RID: 133 RVA: 0x00004386 File Offset: 0x00002586
	public int dirtyBatchLastFrame { get; private set; }

	// Token: 0x06000086 RID: 134 RVA: 0x0000438F File Offset: 0x0000258F
	public BatchSet(KAnimBatchGroup batchGroup, BatchKey batchKey, Vector2I spacialIdx)
	{
		this.idx = spacialIdx;
		this.key = batchKey;
		this.dirty = true;
		this.group = batchGroup;
		this.batches = new List<KAnimBatch>();
	}

	// Token: 0x06000087 RID: 135 RVA: 0x000043C0 File Offset: 0x000025C0
	public void Clear()
	{
		this.group = null;
		for (int i = 0; i < this.batches.Count; i++)
		{
			if (this.batches[i] != null)
			{
				this.batches[i].Clear();
			}
		}
		this.batches.Clear();
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00004414 File Offset: 0x00002614
	public KAnimBatch GetBatch(int idx)
	{
		return this.batches[idx];
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00004424 File Offset: 0x00002624
	public void Add(KAnimConverter.IAnimConverter controller)
	{
		int layer = controller.GetLayer();
		if (layer != this.key.layer)
		{
			Debug.LogError("Registering with wrong batch set (layer) " + controller.GetName());
		}
		if (!(controller.GetBatchGroupID(false) == this.key.groupID))
		{
			Debug.LogError("Registering with wrong batch set (groupID) " + controller.GetName());
		}
		KAnimBatchGroup.MaterialType materialType = controller.GetMaterialType();
		for (int i = 0; i < this.batches.Count; i++)
		{
			if (this.batches[i].size < this.group.maxGroupSize && this.batches[i].materialType == materialType)
			{
				if (this.batches[i].Register(controller))
				{
					this.SetDirty();
				}
				return;
			}
		}
		KAnimBatch kanimBatch = new KAnimBatch(this.group, layer, controller.GetZ(), materialType);
		kanimBatch.Init();
		this.AddBatch(kanimBatch);
		kanimBatch.Register(controller);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000452C File Offset: 0x0000272C
	public void RemoveBatch(KAnimBatch batch)
	{
		Debug.Assert(batch.batchset == this);
		if (this.batches.Contains(batch))
		{
			this.group.batchCount--;
			this.batches.Remove(batch);
			batch.SetBatchSet(null);
		}
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0000457C File Offset: 0x0000277C
	public void AddBatch(KAnimBatch batch)
	{
		if (batch.batchset != this)
		{
			if (batch.batchset != null)
			{
				batch.batchset.RemoveBatch(batch);
			}
			batch.SetBatchSet(this);
			if (!this.batches.Contains(batch))
			{
				this.group.batchCount++;
				this.batches.Add(batch);
				this.batches.Sort((KAnimBatch b0, KAnimBatch b1) => b0.position.z.CompareTo(b1.position.z));
			}
		}
		Debug.Assert(batch.position.x == (float)(this.idx.x * 32));
		Debug.Assert(batch.position.y == (float)(this.idx.y * 32));
		this.SetDirty();
	}

	// Token: 0x0600008C RID: 140 RVA: 0x0000464C File Offset: 0x0000284C
	public void SetDirty()
	{
		this.dirty = true;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00004658 File Offset: 0x00002858
	public void SetActive(bool isActive)
	{
		if (isActive != this.active)
		{
			if (!isActive)
			{
				for (int i = 0; i < this.batches.Count; i++)
				{
					if (this.batches[i] != null)
					{
						this.batches[i].Deactivate();
					}
				}
			}
			else
			{
				for (int j = 0; j < this.batches.Count; j++)
				{
					if (this.batches[j] != null)
					{
						this.batches[j].Activate();
					}
				}
				this.SetDirty();
			}
		}
		this.active = isActive;
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600008E RID: 142 RVA: 0x000046EA File Offset: 0x000028EA
	// (set) Token: 0x0600008F RID: 143 RVA: 0x000046F2 File Offset: 0x000028F2
	public int lastDirtyFrame { get; private set; }

	// Token: 0x06000090 RID: 144 RVA: 0x000046FC File Offset: 0x000028FC
	public int UpdateDirty(int frame)
	{
		this.dirtyBatchLastFrame = 0;
		if (this.dirty)
		{
			for (int i = 0; i < this.batches.Count; i++)
			{
				this.dirtyBatchLastFrame += this.batches[i].UpdateDirty(frame);
			}
			this.lastDirtyFrame = frame;
			this.dirty = false;
		}
		return this.dirtyBatchLastFrame;
	}
}
