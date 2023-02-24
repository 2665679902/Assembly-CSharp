using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007DB RID: 2011
public class SandboxHeatTool : BrushTool
{
	// Token: 0x060039D2 RID: 14802 RVA: 0x0013FA90 File Offset: 0x0013DC90
	public static void DestroyInstance()
	{
		SandboxHeatTool.instance = null;
	}

	// Token: 0x17000425 RID: 1061
	// (get) Token: 0x060039D3 RID: 14803 RVA: 0x0013FA98 File Offset: 0x0013DC98
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x060039D4 RID: 14804 RVA: 0x0013FAA4 File Offset: 0x0013DCA4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxHeatTool.instance = this;
		this.viewMode = OverlayModes.Temperature.ID;
	}

	// Token: 0x060039D5 RID: 14805 RVA: 0x0013FABD File Offset: 0x0013DCBD
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060039D6 RID: 14806 RVA: 0x0013FACC File Offset: 0x0013DCCC
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.temperatureAdditiveSlider.row.SetActive(true);
	}

	// Token: 0x060039D7 RID: 14807 RVA: 0x0013FB23 File Offset: 0x0013DD23
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060039D8 RID: 14808 RVA: 0x0013FB3C File Offset: 0x0013DD3C
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

	// Token: 0x060039D9 RID: 14809 RVA: 0x0013FBF4 File Offset: 0x0013DDF4
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x060039DA RID: 14810 RVA: 0x0013FC00 File Offset: 0x0013DE00
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		if (this.recentlyAffectedCells.Contains(cell))
		{
			return;
		}
		this.recentlyAffectedCells.Add(cell);
		Game.CallbackInfo callbackInfo = new Game.CallbackInfo(delegate
		{
			this.recentlyAffectedCells.Remove(cell);
		}, false);
		int index = Game.Instance.callbackManager.Add(callbackInfo).index;
		float num = Grid.Temperature[cell];
		num += SandboxToolParameterMenu.instance.settings.GetFloatSetting("SandbosTools.TemperatureAdditive");
		GameUtil.TemperatureUnit temperatureUnit = GameUtil.temperatureUnit;
		if (temperatureUnit != GameUtil.TemperatureUnit.Celsius)
		{
			if (temperatureUnit == GameUtil.TemperatureUnit.Fahrenheit)
			{
				num -= 255.372f;
			}
		}
		else
		{
			num -= 273.15f;
		}
		num = Mathf.Clamp(num, 1f, 9999f);
		int cell2 = cell;
		SimHashes id = Grid.Element[cell].id;
		CellElementEvent sandBoxTool = CellEventLogger.Instance.SandBoxTool;
		float num2 = Grid.Mass[cell];
		float num3 = num;
		int num4 = index;
		SimMessages.ReplaceElement(cell2, id, sandBoxTool, num2, num3, Grid.DiseaseIdx[cell], Grid.DiseaseCount[cell], num4);
	}

	// Token: 0x04002614 RID: 9748
	public static SandboxHeatTool instance;

	// Token: 0x04002615 RID: 9749
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x04002616 RID: 9750
	protected Color recentlyAffectedCellColor = new Color(1f, 1f, 1f, 0.1f);
}
