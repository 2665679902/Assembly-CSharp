using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D2 RID: 1234
[AddComponentMenu("KMonoBehaviour/scripts/StationaryChoreRangeVisualizer")]
public class StationaryChoreRangeVisualizer : KMonoBehaviour
{
	// Token: 0x06001C9E RID: 7326 RVA: 0x00098948 File Offset: 0x00096B48
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<StationaryChoreRangeVisualizer>(-1503271301, StationaryChoreRangeVisualizer.OnSelectDelegate);
		if (this.movable)
		{
			Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "StationaryChoreRangeVisualizer.OnSpawn");
			base.Subscribe<StationaryChoreRangeVisualizer>(-1643076535, StationaryChoreRangeVisualizer.OnRotatedDelegate);
		}
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x000989A8 File Offset: 0x00096BA8
	protected override void OnCleanUp()
	{
		Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
		base.Unsubscribe<StationaryChoreRangeVisualizer>(-1503271301, StationaryChoreRangeVisualizer.OnSelectDelegate, false);
		base.Unsubscribe<StationaryChoreRangeVisualizer>(-1643076535, StationaryChoreRangeVisualizer.OnRotatedDelegate, false);
		this.ClearVisualizers();
		base.OnCleanUp();
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x00098A00 File Offset: 0x00096C00
	private void OnSelect(object data)
	{
		if ((bool)data)
		{
			SoundEvent.PlayOneShot(GlobalAssets.GetSound("RadialGrid_form", false), base.transform.position, 1f);
			this.UpdateVisualizers();
			return;
		}
		SoundEvent.PlayOneShot(GlobalAssets.GetSound("RadialGrid_disappear", false), base.transform.position, 1f);
		this.ClearVisualizers();
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x00098A64 File Offset: 0x00096C64
	private void OnRotated(object data)
	{
		this.UpdateVisualizers();
	}

	// Token: 0x06001CA2 RID: 7330 RVA: 0x00098A6C File Offset: 0x00096C6C
	private void OnCellChange()
	{
		this.UpdateVisualizers();
	}

	// Token: 0x06001CA3 RID: 7331 RVA: 0x00098A74 File Offset: 0x00096C74
	private void UpdateVisualizers()
	{
		this.newCells.Clear();
		CellOffset rotatedCellOffset = this.vision_offset;
		if (this.rotatable)
		{
			rotatedCellOffset = this.rotatable.GetRotatedCellOffset(this.vision_offset);
		}
		int num = Grid.PosToCell(base.transform.gameObject);
		int num2;
		int num3;
		Grid.CellToXY(Grid.OffsetCell(num, rotatedCellOffset), out num2, out num3);
		for (int i = 0; i < this.height; i++)
		{
			for (int j = 0; j < this.width; j++)
			{
				CellOffset rotatedCellOffset2 = new CellOffset(this.x + j, this.y + i);
				if (this.rotatable)
				{
					rotatedCellOffset2 = this.rotatable.GetRotatedCellOffset(rotatedCellOffset2);
				}
				int num4 = Grid.OffsetCell(num, rotatedCellOffset2);
				if (Grid.IsValidCell(num4))
				{
					int num5;
					int num6;
					Grid.CellToXY(num4, out num5, out num6);
					if (Grid.TestLineOfSight(num2, num3, num5, num6, this.blocking_cb, this.blocking_tile_visible))
					{
						this.newCells.Add(num4);
					}
				}
			}
		}
		for (int k = this.visualizers.Count - 1; k >= 0; k--)
		{
			if (this.newCells.Contains(this.visualizers[k].cell))
			{
				this.newCells.Remove(this.visualizers[k].cell);
			}
			else
			{
				this.DestroyEffect(this.visualizers[k].controller);
				this.visualizers.RemoveAt(k);
			}
		}
		for (int l = 0; l < this.newCells.Count; l++)
		{
			KBatchedAnimController kbatchedAnimController = this.CreateEffect(this.newCells[l]);
			this.visualizers.Add(new StationaryChoreRangeVisualizer.VisData
			{
				cell = this.newCells[l],
				controller = kbatchedAnimController
			});
		}
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x00098C60 File Offset: 0x00096E60
	private void ClearVisualizers()
	{
		for (int i = 0; i < this.visualizers.Count; i++)
		{
			this.DestroyEffect(this.visualizers[i].controller);
		}
		this.visualizers.Clear();
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x00098CA8 File Offset: 0x00096EA8
	private KBatchedAnimController CreateEffect(int cell)
	{
		KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect(StationaryChoreRangeVisualizer.AnimName, Grid.CellToPosCCC(cell, this.sceneLayer), null, false, this.sceneLayer, true);
		kbatchedAnimController.destroyOnAnimComplete = false;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.Always;
		kbatchedAnimController.gameObject.SetActive(true);
		kbatchedAnimController.Play(StationaryChoreRangeVisualizer.PreAnims, KAnim.PlayMode.Loop);
		return kbatchedAnimController;
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x00098CFA File Offset: 0x00096EFA
	private void DestroyEffect(KBatchedAnimController controller)
	{
		controller.destroyOnAnimComplete = true;
		controller.Play(StationaryChoreRangeVisualizer.PostAnim, KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x0400101E RID: 4126
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x0400101F RID: 4127
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x04001020 RID: 4128
	public int x;

	// Token: 0x04001021 RID: 4129
	public int y;

	// Token: 0x04001022 RID: 4130
	public int width;

	// Token: 0x04001023 RID: 4131
	public int height;

	// Token: 0x04001024 RID: 4132
	public bool movable;

	// Token: 0x04001025 RID: 4133
	public Grid.SceneLayer sceneLayer = Grid.SceneLayer.FXFront;

	// Token: 0x04001026 RID: 4134
	public CellOffset vision_offset;

	// Token: 0x04001027 RID: 4135
	public Func<int, bool> blocking_cb = new Func<int, bool>(Grid.PhysicalBlockingCB);

	// Token: 0x04001028 RID: 4136
	public bool blocking_tile_visible = true;

	// Token: 0x04001029 RID: 4137
	private static readonly string AnimName = "transferarmgrid_kanim";

	// Token: 0x0400102A RID: 4138
	private static readonly HashedString[] PreAnims = new HashedString[] { "grid_pre", "grid_loop" };

	// Token: 0x0400102B RID: 4139
	private static readonly HashedString PostAnim = "grid_pst";

	// Token: 0x0400102C RID: 4140
	private List<StationaryChoreRangeVisualizer.VisData> visualizers = new List<StationaryChoreRangeVisualizer.VisData>();

	// Token: 0x0400102D RID: 4141
	private List<int> newCells = new List<int>();

	// Token: 0x0400102E RID: 4142
	private static readonly EventSystem.IntraObjectHandler<StationaryChoreRangeVisualizer> OnSelectDelegate = new EventSystem.IntraObjectHandler<StationaryChoreRangeVisualizer>(delegate(StationaryChoreRangeVisualizer component, object data)
	{
		component.OnSelect(data);
	});

	// Token: 0x0400102F RID: 4143
	private static readonly EventSystem.IntraObjectHandler<StationaryChoreRangeVisualizer> OnRotatedDelegate = new EventSystem.IntraObjectHandler<StationaryChoreRangeVisualizer>(delegate(StationaryChoreRangeVisualizer component, object data)
	{
		component.OnRotated(data);
	});

	// Token: 0x02001114 RID: 4372
	private struct VisData
	{
		// Token: 0x040059BD RID: 22973
		public int cell;

		// Token: 0x040059BE RID: 22974
		public KBatchedAnimController controller;
	}
}
