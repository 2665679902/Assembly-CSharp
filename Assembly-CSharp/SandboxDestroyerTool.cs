using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D8 RID: 2008
public class SandboxDestroyerTool : BrushTool
{
	// Token: 0x060039B1 RID: 14769 RVA: 0x0013F0D8 File Offset: 0x0013D2D8
	public static void DestroyInstance()
	{
		SandboxDestroyerTool.instance = null;
	}

	// Token: 0x17000422 RID: 1058
	// (get) Token: 0x060039B2 RID: 14770 RVA: 0x0013F0E0 File Offset: 0x0013D2E0
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x060039B3 RID: 14771 RVA: 0x0013F0EC File Offset: 0x0013D2EC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxDestroyerTool.instance = this;
		this.affectFoundation = true;
	}

	// Token: 0x060039B4 RID: 14772 RVA: 0x0013F101 File Offset: 0x0013D301
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060039B5 RID: 14773 RVA: 0x0013F10E File Offset: 0x0013D30E
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
	}

	// Token: 0x060039B6 RID: 14774 RVA: 0x0013F145 File Offset: 0x0013D345
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060039B7 RID: 14775 RVA: 0x0013F160 File Offset: 0x0013D360
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.recentlyAffectedCells)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.recentlyAffectedCellColor));
		}
		foreach (int num2 in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num2, this.radiusIndicatorColor));
		}
	}

	// Token: 0x060039B8 RID: 14776 RVA: 0x0013F218 File Offset: 0x0013D418
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x060039B9 RID: 14777 RVA: 0x0013F224 File Offset: 0x0013D424
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		this.recentlyAffectedCells.Add(cell);
		Game.CallbackInfo callbackInfo = new Game.CallbackInfo(delegate
		{
			this.recentlyAffectedCells.Remove(cell);
		}, false);
		int index = Game.Instance.callbackManager.Add(callbackInfo).index;
		SimMessages.ReplaceElement(cell, SimHashes.Vacuum, CellEventLogger.Instance.SandBoxTool, 0f, 0f, byte.MaxValue, 0, index);
		HashSetPool<GameObject, SandboxDestroyerTool>.PooledHashSet pooledHashSet = HashSetPool<GameObject, SandboxDestroyerTool>.Allocate();
		foreach (Pickupable pickupable in Components.Pickupables.Items)
		{
			if (Grid.PosToCell(pickupable) == cell)
			{
				pooledHashSet.Add(pickupable.gameObject);
			}
		}
		foreach (BuildingComplete buildingComplete in Components.BuildingCompletes.Items)
		{
			if (Grid.PosToCell(buildingComplete) == cell)
			{
				pooledHashSet.Add(buildingComplete.gameObject);
			}
		}
		if (Grid.Objects[cell, 1] != null)
		{
			pooledHashSet.Add(Grid.Objects[cell, 1]);
		}
		foreach (Crop crop in Components.Crops.Items)
		{
			if (Grid.PosToCell(crop) == cell)
			{
				pooledHashSet.Add(crop.gameObject);
			}
		}
		foreach (Health health in Components.Health.Items)
		{
			if (Grid.PosToCell(health) == cell)
			{
				pooledHashSet.Add(health.gameObject);
			}
		}
		foreach (GameObject gameObject in pooledHashSet)
		{
			Util.KDestroyGameObject(gameObject);
		}
		pooledHashSet.Recycle();
	}

	// Token: 0x0400260A RID: 9738
	public static SandboxDestroyerTool instance;

	// Token: 0x0400260B RID: 9739
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x0400260C RID: 9740
	protected Color recentlyAffectedCellColor = new Color(1f, 1f, 1f, 0.1f);
}
