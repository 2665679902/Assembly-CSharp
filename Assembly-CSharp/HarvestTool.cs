using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007CE RID: 1998
public class HarvestTool : DragTool
{
	// Token: 0x06003932 RID: 14642 RVA: 0x0013CDFC File Offset: 0x0013AFFC
	public static void DestroyInstance()
	{
		HarvestTool.Instance = null;
	}

	// Token: 0x06003933 RID: 14643 RVA: 0x0013CE04 File Offset: 0x0013B004
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		HarvestTool.Instance = this;
		this.options.Add("HARVEST_WHEN_READY", ToolParameterMenu.ToggleState.On);
		this.options.Add("DO_NOT_HARVEST", ToolParameterMenu.ToggleState.Off);
		this.viewMode = OverlayModes.Harvest.ID;
	}

	// Token: 0x06003934 RID: 14644 RVA: 0x0013CE40 File Offset: 0x0013B040
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		if (Grid.IsValidCell(cell))
		{
			foreach (HarvestDesignatable harvestDesignatable in Components.HarvestDesignatables.Items)
			{
				OccupyArea area = harvestDesignatable.area;
				if (Grid.PosToCell(harvestDesignatable) == cell || (area != null && area.CheckIsOccupying(cell)))
				{
					if (this.options["HARVEST_WHEN_READY"] == ToolParameterMenu.ToggleState.On)
					{
						harvestDesignatable.SetHarvestWhenReady(true);
					}
					else if (this.options["DO_NOT_HARVEST"] == ToolParameterMenu.ToggleState.On)
					{
						harvestDesignatable.SetHarvestWhenReady(false);
					}
					Prioritizable component = harvestDesignatable.GetComponent<Prioritizable>();
					if (component != null)
					{
						component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
					}
				}
			}
		}
	}

	// Token: 0x06003935 RID: 14645 RVA: 0x0013CF1C File Offset: 0x0013B11C
	public void Update()
	{
		MeshRenderer componentInChildren = this.visualizer.GetComponentInChildren<MeshRenderer>();
		if (componentInChildren != null)
		{
			if (this.options["HARVEST_WHEN_READY"] == ToolParameterMenu.ToggleState.On)
			{
				componentInChildren.material.mainTexture = this.visualizerTextures[0];
				return;
			}
			if (this.options["DO_NOT_HARVEST"] == ToolParameterMenu.ToggleState.On)
			{
				componentInChildren.material.mainTexture = this.visualizerTextures[1];
			}
		}
	}

	// Token: 0x06003936 RID: 14646 RVA: 0x0013CF89 File Offset: 0x0013B189
	public override void OnLeftClickUp(Vector3 cursor_pos)
	{
		base.OnLeftClickUp(cursor_pos);
	}

	// Token: 0x06003937 RID: 14647 RVA: 0x0013CF92 File Offset: 0x0013B192
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
		ToolMenu.Instance.toolParameterMenu.PopulateMenu(this.options);
	}

	// Token: 0x06003938 RID: 14648 RVA: 0x0013CFBF File Offset: 0x0013B1BF
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
		ToolMenu.Instance.toolParameterMenu.ClearMenu();
	}

	// Token: 0x040025D4 RID: 9684
	public GameObject Placer;

	// Token: 0x040025D5 RID: 9685
	public static HarvestTool Instance;

	// Token: 0x040025D6 RID: 9686
	public Texture2D[] visualizerTextures;

	// Token: 0x040025D7 RID: 9687
	private Dictionary<string, ToolParameterMenu.ToggleState> options = new Dictionary<string, ToolParameterMenu.ToggleState>();
}
