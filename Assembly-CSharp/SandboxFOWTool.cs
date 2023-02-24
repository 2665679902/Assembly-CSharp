using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007D9 RID: 2009
public class SandboxFOWTool : BrushTool
{
	// Token: 0x060039BB RID: 14779 RVA: 0x0013F4E6 File Offset: 0x0013D6E6
	public static void DestroyInstance()
	{
		SandboxFOWTool.instance = null;
	}

	// Token: 0x17000423 RID: 1059
	// (get) Token: 0x060039BC RID: 14780 RVA: 0x0013F4EE File Offset: 0x0013D6EE
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x060039BD RID: 14781 RVA: 0x0013F4FA File Offset: 0x0013D6FA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxFOWTool.instance = this;
	}

	// Token: 0x060039BE RID: 14782 RVA: 0x0013F508 File Offset: 0x0013D708
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060039BF RID: 14783 RVA: 0x0013F515 File Offset: 0x0013D715
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
	}

	// Token: 0x060039C0 RID: 14784 RVA: 0x0013F54C File Offset: 0x0013D74C
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060039C1 RID: 14785 RVA: 0x0013F568 File Offset: 0x0013D768
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

	// Token: 0x060039C2 RID: 14786 RVA: 0x0013F620 File Offset: 0x0013D820
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x060039C3 RID: 14787 RVA: 0x0013F629 File Offset: 0x0013D829
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		Grid.Reveal(cell, byte.MaxValue, false);
	}

	// Token: 0x0400260D RID: 9741
	public static SandboxFOWTool instance;

	// Token: 0x0400260E RID: 9742
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x0400260F RID: 9743
	protected Color recentlyAffectedCellColor = new Color(1f, 1f, 1f, 0.1f);
}
