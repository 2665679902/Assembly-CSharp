using System;
using UnityEngine;

// Token: 0x02000343 RID: 835
public class VerticalModuleTiler : KMonoBehaviour
{
	// Token: 0x060010C7 RID: 4295 RVA: 0x0005AC0C File Offset: 0x00058E0C
	protected override void OnSpawn()
	{
		OccupyArea component = base.GetComponent<OccupyArea>();
		if (component != null)
		{
			this.extents = component.GetExtents();
		}
		KBatchedAnimController component2 = base.GetComponent<KBatchedAnimController>();
		if (this.manageTopCap)
		{
			this.topCapWide = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), VerticalModuleTiler.topCapStr);
		}
		if (this.manageBottomCap)
		{
			this.bottomCapWide = new KAnimSynchronizedController(component2, (Grid.SceneLayer)component2.GetLayer(), VerticalModuleTiler.bottomCapStr);
		}
		this.PostReorderMove();
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0005AC80 File Offset: 0x00058E80
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x0005AC98 File Offset: 0x00058E98
	public void PostReorderMove()
	{
		this.dirty = true;
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0005ACA1 File Offset: 0x00058EA1
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

	// Token: 0x060010CB RID: 4299 RVA: 0x0005ACD0 File Offset: 0x00058ED0
	private void UpdateEndCaps()
	{
		int num;
		int num2;
		Grid.CellToXY(Grid.PosToCell(this), out num, out num2);
		int cellTop = this.GetCellTop();
		int cellBottom = this.GetCellBottom();
		if (Grid.IsValidCell(cellTop))
		{
			if (this.HasWideNeighbor(cellTop))
			{
				this.topCapSetting = VerticalModuleTiler.AnimCapType.FiveWide;
			}
			else
			{
				this.topCapSetting = VerticalModuleTiler.AnimCapType.ThreeWide;
			}
		}
		if (Grid.IsValidCell(cellBottom))
		{
			if (this.HasWideNeighbor(cellBottom))
			{
				this.bottomCapSetting = VerticalModuleTiler.AnimCapType.FiveWide;
			}
			else
			{
				this.bottomCapSetting = VerticalModuleTiler.AnimCapType.ThreeWide;
			}
		}
		if (this.manageTopCap)
		{
			this.topCapWide.Enable(this.topCapSetting == VerticalModuleTiler.AnimCapType.FiveWide);
		}
		if (this.manageBottomCap)
		{
			this.bottomCapWide.Enable(this.bottomCapSetting == VerticalModuleTiler.AnimCapType.FiveWide);
		}
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0005AD74 File Offset: 0x00058F74
	private int GetCellTop()
	{
		int num = Grid.PosToCell(this);
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset cellOffset = new CellOffset(0, this.extents.y - num3 + this.extents.height);
		return Grid.OffsetCell(num, cellOffset);
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0005ADB8 File Offset: 0x00058FB8
	private int GetCellBottom()
	{
		int num = Grid.PosToCell(this);
		int num2;
		int num3;
		Grid.CellToXY(num, out num2, out num3);
		CellOffset cellOffset = new CellOffset(0, this.extents.y - num3 - 1);
		return Grid.OffsetCell(num, cellOffset);
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0005ADF4 File Offset: 0x00058FF4
	private bool HasWideNeighbor(int neighbour_cell)
	{
		bool flag = false;
		GameObject gameObject = Grid.Objects[neighbour_cell, (int)this.objectLayer];
		if (gameObject != null)
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component != null && component.GetComponent<ReorderableBuilding>() != null && component.GetComponent<Building>().Def.WidthInCells >= 5)
			{
				flag = true;
			}
		}
		return flag;
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0005AE54 File Offset: 0x00059054
	private void LateUpdate()
	{
		if (this.animController.Offset != this.m_previousAnimControllerOffset)
		{
			this.m_previousAnimControllerOffset = this.animController.Offset;
			this.bottomCapWide.Dirty();
			this.topCapWide.Dirty();
		}
		if (this.dirty)
		{
			if (this.partitionerEntry != HandleVector<int>.InvalidHandle)
			{
				GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
			}
			OccupyArea component = base.GetComponent<OccupyArea>();
			if (component != null)
			{
				this.extents = component.GetExtents();
			}
			Extents extents = new Extents(this.extents.x, this.extents.y - 1, this.extents.width, this.extents.height + 2);
			this.partitionerEntry = GameScenePartitioner.Instance.Add("VerticalModuleTiler.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.objectLayers[(int)this.objectLayer], new Action<object>(this.OnNeighbourCellsUpdated));
			this.UpdateEndCaps();
			this.dirty = false;
		}
	}

	// Token: 0x0400090E RID: 2318
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x0400090F RID: 2319
	public ObjectLayer objectLayer = ObjectLayer.Building;

	// Token: 0x04000910 RID: 2320
	private Extents extents;

	// Token: 0x04000911 RID: 2321
	private VerticalModuleTiler.AnimCapType topCapSetting;

	// Token: 0x04000912 RID: 2322
	private VerticalModuleTiler.AnimCapType bottomCapSetting;

	// Token: 0x04000913 RID: 2323
	private bool manageTopCap = true;

	// Token: 0x04000914 RID: 2324
	private bool manageBottomCap = true;

	// Token: 0x04000915 RID: 2325
	private KAnimSynchronizedController topCapWide;

	// Token: 0x04000916 RID: 2326
	private KAnimSynchronizedController bottomCapWide;

	// Token: 0x04000917 RID: 2327
	private static readonly string topCapStr = "#cap_top_5";

	// Token: 0x04000918 RID: 2328
	private static readonly string bottomCapStr = "#cap_bottom_5";

	// Token: 0x04000919 RID: 2329
	private bool dirty;

	// Token: 0x0400091A RID: 2330
	[MyCmpGet]
	private KAnimControllerBase animController;

	// Token: 0x0400091B RID: 2331
	private Vector3 m_previousAnimControllerOffset;

	// Token: 0x02000F19 RID: 3865
	private enum AnimCapType
	{
		// Token: 0x04005326 RID: 21286
		ThreeWide,
		// Token: 0x04005327 RID: 21287
		FiveWide
	}
}
