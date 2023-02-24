using System;
using UnityEngine;

// Token: 0x020006D3 RID: 1747
[AddComponentMenu("KMonoBehaviour/scripts/EntityPreview")]
public class EntityPreview : KMonoBehaviour
{
	// Token: 0x17000357 RID: 855
	// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000FAF45 File Offset: 0x000F9145
	// (set) Token: 0x06002F7D RID: 12157 RVA: 0x000FAF4D File Offset: 0x000F914D
	public bool Valid { get; private set; }

	// Token: 0x06002F7E RID: 12158 RVA: 0x000FAF58 File Offset: 0x000F9158
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.solidPartitionerEntry = GameScenePartitioner.Instance.Add("EntityPreview", base.gameObject, this.occupyArea.GetExtents(), GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnAreaChanged));
		if (this.objectLayer != ObjectLayer.NumLayers)
		{
			this.objectPartitionerEntry = GameScenePartitioner.Instance.Add("EntityPreview", base.gameObject, this.occupyArea.GetExtents(), GameScenePartitioner.Instance.objectLayers[(int)this.objectLayer], new Action<object>(this.OnAreaChanged));
		}
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "EntityPreview.OnSpawn");
		this.OnAreaChanged(null);
	}

	// Token: 0x06002F7F RID: 12159 RVA: 0x000FB020 File Offset: 0x000F9220
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.objectPartitionerEntry);
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
		base.OnCleanUp();
	}

	// Token: 0x06002F80 RID: 12160 RVA: 0x000FB06F File Offset: 0x000F926F
	private void OnCellChange()
	{
		GameScenePartitioner.Instance.UpdatePosition(this.solidPartitionerEntry, this.occupyArea.GetExtents());
		GameScenePartitioner.Instance.UpdatePosition(this.objectPartitionerEntry, this.occupyArea.GetExtents());
		this.OnAreaChanged(null);
	}

	// Token: 0x06002F81 RID: 12161 RVA: 0x000FB0AE File Offset: 0x000F92AE
	public void SetSolid()
	{
		this.occupyArea.ApplyToCells = true;
	}

	// Token: 0x06002F82 RID: 12162 RVA: 0x000FB0BC File Offset: 0x000F92BC
	private void OnAreaChanged(object obj)
	{
		this.UpdateValidity();
	}

	// Token: 0x06002F83 RID: 12163 RVA: 0x000FB0C4 File Offset: 0x000F92C4
	public void UpdateValidity()
	{
		bool valid = this.Valid;
		this.Valid = this.occupyArea.TestArea(Grid.PosToCell(this), this, EntityPreview.ValidTestDelegate);
		if (this.Valid)
		{
			this.animController.TintColour = Color.white;
		}
		else
		{
			this.animController.TintColour = Color.red;
		}
		if (valid != this.Valid)
		{
			base.Trigger(-1820564715, this.Valid);
		}
	}

	// Token: 0x06002F84 RID: 12164 RVA: 0x000FB148 File Offset: 0x000F9348
	private static bool ValidTest(int cell, object data)
	{
		EntityPreview entityPreview = (EntityPreview)data;
		return Grid.IsValidCell(cell) && !Grid.Solid[cell] && (entityPreview.objectLayer == ObjectLayer.NumLayers || Grid.Objects[cell, (int)entityPreview.objectLayer] == entityPreview.gameObject || Grid.Objects[cell, (int)entityPreview.objectLayer] == null);
	}

	// Token: 0x04001C95 RID: 7317
	[MyCmpReq]
	private OccupyArea occupyArea;

	// Token: 0x04001C96 RID: 7318
	[MyCmpReq]
	private KBatchedAnimController animController;

	// Token: 0x04001C97 RID: 7319
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001C98 RID: 7320
	public ObjectLayer objectLayer = ObjectLayer.NumLayers;

	// Token: 0x04001C9A RID: 7322
	private HandleVector<int>.Handle solidPartitionerEntry;

	// Token: 0x04001C9B RID: 7323
	private HandleVector<int>.Handle objectPartitionerEntry;

	// Token: 0x04001C9C RID: 7324
	private static readonly Func<int, object, bool> ValidTestDelegate = (int cell, object data) => EntityPreview.ValidTest(cell, data);
}
