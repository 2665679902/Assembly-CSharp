using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D7 RID: 2007
public class SandboxCritterTool : BrushTool
{
	// Token: 0x060039A8 RID: 14760 RVA: 0x0013EF20 File Offset: 0x0013D120
	public static void DestroyInstance()
	{
		SandboxCritterTool.instance = null;
	}

	// Token: 0x060039A9 RID: 14761 RVA: 0x0013EF28 File Offset: 0x0013D128
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxCritterTool.instance = this;
	}

	// Token: 0x060039AA RID: 14762 RVA: 0x0013EF36 File Offset: 0x0013D136
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060039AB RID: 14763 RVA: 0x0013EF43 File Offset: 0x0013D143
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.SetValue(6f, true);
	}

	// Token: 0x060039AC RID: 14764 RVA: 0x0013EF7A File Offset: 0x0013D17A
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060039AD RID: 14765 RVA: 0x0013EF94 File Offset: 0x0013D194
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.radiusIndicatorColor));
		}
	}

	// Token: 0x060039AE RID: 14766 RVA: 0x0013EFFC File Offset: 0x0013D1FC
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x060039AF RID: 14767 RVA: 0x0013F008 File Offset: 0x0013D208
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		HashSetPool<GameObject, SandboxCritterTool>.PooledHashSet pooledHashSet = HashSetPool<GameObject, SandboxCritterTool>.Allocate();
		foreach (Health health in Components.Health.Items)
		{
			if (Grid.PosToCell(health) == cell && health.GetComponent<KPrefabID>().HasTag(GameTags.Creature))
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

	// Token: 0x04002609 RID: 9737
	public static SandboxCritterTool instance;
}
