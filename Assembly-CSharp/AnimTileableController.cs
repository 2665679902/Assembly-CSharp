using System;
using UnityEngine;

// Token: 0x02000548 RID: 1352
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/AnimTileableController")]
public class AnimTileableController : KMonoBehaviour
{
	// Token: 0x06002039 RID: 8249 RVA: 0x000B00A1 File Offset: 0x000AE2A1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.tags == null || this.tags.Length == 0)
		{
			this.tags = new Tag[] { base.GetComponent<KPrefabID>().PrefabTag };
		}
	}

	// Token: 0x0600203A RID: 8250 RVA: 0x000B00D8 File Offset: 0x000AE2D8
	protected override void OnSpawn()
	{
		OccupyArea component = base.GetComponent<OccupyArea>();
		if (component != null)
		{
			this.extents = component.GetExtents();
		}
		else
		{
			Building component2 = base.GetComponent<Building>();
			this.extents = component2.GetExtents();
		}
		Extents extents = new Extents(this.extents.x - 1, this.extents.y - 1, this.extents.width + 2, this.extents.height + 2);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("AnimTileable.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.objectLayers[(int)this.objectLayer], new Action<object>(this.OnNeighbourCellsUpdated));
		KBatchedAnimController component3 = base.GetComponent<KBatchedAnimController>();
		this.left = new KAnimSynchronizedController(component3, (Grid.SceneLayer)component3.GetLayer(), this.leftName);
		this.right = new KAnimSynchronizedController(component3, (Grid.SceneLayer)component3.GetLayer(), this.rightName);
		this.top = new KAnimSynchronizedController(component3, (Grid.SceneLayer)component3.GetLayer(), this.topName);
		this.bottom = new KAnimSynchronizedController(component3, (Grid.SceneLayer)component3.GetLayer(), this.bottomName);
		this.UpdateEndCaps();
	}

	// Token: 0x0600203B RID: 8251 RVA: 0x000B01F7 File Offset: 0x000AE3F7
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x0600203C RID: 8252 RVA: 0x000B0210 File Offset: 0x000AE410
	private void UpdateEndCaps()
	{
		int num = Grid.PosToCell(this);
		bool flag = true;
		bool flag2 = true;
		bool flag3 = true;
		bool flag4 = true;
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset rotatedCellOffset = new CellOffset(this.extents.x - num2 - 1, 0);
		CellOffset rotatedCellOffset2 = new CellOffset(this.extents.x - num2 + this.extents.width, 0);
		CellOffset rotatedCellOffset3 = new CellOffset(0, this.extents.y - num3 + this.extents.height);
		CellOffset rotatedCellOffset4 = new CellOffset(0, this.extents.y - num3 - 1);
		Rotatable component = base.GetComponent<Rotatable>();
		if (component)
		{
			rotatedCellOffset = component.GetRotatedCellOffset(rotatedCellOffset);
			rotatedCellOffset2 = component.GetRotatedCellOffset(rotatedCellOffset2);
			rotatedCellOffset3 = component.GetRotatedCellOffset(rotatedCellOffset3);
			rotatedCellOffset4 = component.GetRotatedCellOffset(rotatedCellOffset4);
		}
		int num4 = Grid.OffsetCell(num, rotatedCellOffset);
		int num5 = Grid.OffsetCell(num, rotatedCellOffset2);
		int num6 = Grid.OffsetCell(num, rotatedCellOffset3);
		int num7 = Grid.OffsetCell(num, rotatedCellOffset4);
		if (Grid.IsValidCell(num4))
		{
			flag = !this.HasTileableNeighbour(num4);
		}
		if (Grid.IsValidCell(num5))
		{
			flag2 = !this.HasTileableNeighbour(num5);
		}
		if (Grid.IsValidCell(num6))
		{
			flag3 = !this.HasTileableNeighbour(num6);
		}
		if (Grid.IsValidCell(num7))
		{
			flag4 = !this.HasTileableNeighbour(num7);
		}
		this.left.Enable(flag);
		this.right.Enable(flag2);
		this.top.Enable(flag3);
		this.bottom.Enable(flag4);
	}

	// Token: 0x0600203D RID: 8253 RVA: 0x000B0394 File Offset: 0x000AE594
	private bool HasTileableNeighbour(int neighbour_cell)
	{
		bool flag = false;
		GameObject gameObject = Grid.Objects[neighbour_cell, (int)this.objectLayer];
		if (gameObject != null)
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component != null && component.HasAnyTags(this.tags))
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x0600203E RID: 8254 RVA: 0x000B03DF File Offset: 0x000AE5DF
	private void OnNeighbourCellsUpdated(object data)
	{
		if (this == null || base.gameObject == null)
		{
			return;
		}
		if (this.partitionerEntry.IsValid())
		{
			this.UpdateEndCaps();
		}
	}

	// Token: 0x04001281 RID: 4737
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001282 RID: 4738
	public ObjectLayer objectLayer = ObjectLayer.Building;

	// Token: 0x04001283 RID: 4739
	public Tag[] tags;

	// Token: 0x04001284 RID: 4740
	private Extents extents;

	// Token: 0x04001285 RID: 4741
	public string leftName = "#cap_left";

	// Token: 0x04001286 RID: 4742
	public string rightName = "#cap_right";

	// Token: 0x04001287 RID: 4743
	public string topName = "#cap_top";

	// Token: 0x04001288 RID: 4744
	public string bottomName = "#cap_bottom";

	// Token: 0x04001289 RID: 4745
	private KAnimSynchronizedController left;

	// Token: 0x0400128A RID: 4746
	private KAnimSynchronizedController right;

	// Token: 0x0400128B RID: 4747
	private KAnimSynchronizedController top;

	// Token: 0x0400128C RID: 4748
	private KAnimSynchronizedController bottom;
}
