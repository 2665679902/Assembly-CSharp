using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x020007D5 RID: 2005
public class SandboxBrushTool : BrushTool
{
	// Token: 0x06003993 RID: 14739 RVA: 0x0013E859 File Offset: 0x0013CA59
	public static void DestroyInstance()
	{
		SandboxBrushTool.instance = null;
	}

	// Token: 0x17000420 RID: 1056
	// (get) Token: 0x06003994 RID: 14740 RVA: 0x0013E861 File Offset: 0x0013CA61
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x06003995 RID: 14741 RVA: 0x0013E86D File Offset: 0x0013CA6D
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxBrushTool.instance = this;
	}

	// Token: 0x06003996 RID: 14742 RVA: 0x0013E87B File Offset: 0x0013CA7B
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x06003997 RID: 14743 RVA: 0x0013E888 File Offset: 0x0013CA88
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.massSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.temperatureSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.elementSelector.row.SetActive(true);
		SandboxToolParameterMenu.instance.diseaseSelector.row.SetActive(true);
		SandboxToolParameterMenu.instance.diseaseCountSlider.row.SetActive(true);
	}

	// Token: 0x06003998 RID: 14744 RVA: 0x0013E933 File Offset: 0x0013CB33
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x06003999 RID: 14745 RVA: 0x0013E94C File Offset: 0x0013CB4C
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.recentlyAffectedCells)
		{
			Color color = new Color(this.recentAffectedCellColor[num].r, this.recentAffectedCellColor[num].g, this.recentAffectedCellColor[num].b, MathUtil.ReRange(Mathf.Sin(Time.realtimeSinceStartup * 10f), -1f, 1f, 0.1f, 0.2f));
			colors.Add(new ToolMenu.CellColorData(num, color));
		}
		foreach (int num2 in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num2, this.radiusIndicatorColor));
		}
	}

	// Token: 0x0600399A RID: 14746 RVA: 0x0013EA64 File Offset: 0x0013CC64
	public override void SetBrushSize(int radius)
	{
		this.brushRadius = radius;
		this.brushOffsets.Clear();
		for (int i = 0; i < this.brushRadius * 2; i++)
		{
			for (int j = 0; j < this.brushRadius * 2; j++)
			{
				if (Vector2.Distance(new Vector2((float)i, (float)j), new Vector2((float)this.brushRadius, (float)this.brushRadius)) < (float)this.brushRadius - 0.8f)
				{
					this.brushOffsets.Add(new Vector2((float)(i - this.brushRadius), (float)(j - this.brushRadius)));
				}
			}
		}
	}

	// Token: 0x0600399B RID: 14747 RVA: 0x0013EAFC File Offset: 0x0013CCFC
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		this.recentlyAffectedCells.Add(cell);
		Element element = ElementLoader.elements[this.settings.GetIntSetting("SandboxTools.SelectedElement")];
		if (!this.recentAffectedCellColor.ContainsKey(cell))
		{
			this.recentAffectedCellColor.Add(cell, element.substance.uiColour);
		}
		else
		{
			this.recentAffectedCellColor[cell] = element.substance.uiColour;
		}
		Game.CallbackInfo callbackInfo = new Game.CallbackInfo(delegate
		{
			this.recentlyAffectedCells.Remove(cell);
			this.recentAffectedCellColor.Remove(cell);
		}, false);
		int index = Game.Instance.callbackManager.Add(callbackInfo).index;
		byte b = Db.Get().Diseases.GetIndex(Db.Get().Diseases.Get("FoodPoisoning").id);
		Disease disease = Db.Get().Diseases.TryGet(this.settings.GetStringSetting("SandboxTools.SelectedDisease"));
		if (disease != null)
		{
			b = Db.Get().Diseases.GetIndex(disease.id);
		}
		int cell2 = cell;
		SimHashes id = element.id;
		CellElementEvent sandBoxTool = CellEventLogger.Instance.SandBoxTool;
		float floatSetting = this.settings.GetFloatSetting("SandboxTools.Mass");
		float floatSetting2 = this.settings.GetFloatSetting("SandbosTools.Temperature");
		int num = index;
		SimMessages.ReplaceElement(cell2, id, sandBoxTool, floatSetting, floatSetting2, b, this.settings.GetIntSetting("SandboxTools.DiseaseCount"), num);
	}

	// Token: 0x0600399C RID: 14748 RVA: 0x0013EC98 File Offset: 0x0013CE98
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.SandboxCopyElement))
		{
			int num = Grid.PosToCell(PlayerController.GetCursorPos(KInputManager.GetMousePos()));
			if (Grid.IsValidCell(num))
			{
				SandboxSampleTool.Sample(num);
			}
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x04002605 RID: 9733
	public static SandboxBrushTool instance;

	// Token: 0x04002606 RID: 9734
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x04002607 RID: 9735
	private Dictionary<int, Color> recentAffectedCellColor = new Dictionary<int, Color>();
}
