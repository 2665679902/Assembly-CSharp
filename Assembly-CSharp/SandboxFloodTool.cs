using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x020007DA RID: 2010
public class SandboxFloodTool : FloodTool
{
	// Token: 0x060039C5 RID: 14789 RVA: 0x0013F671 File Offset: 0x0013D871
	public static void DestroyInstance()
	{
		SandboxFloodTool.instance = null;
	}

	// Token: 0x060039C6 RID: 14790 RVA: 0x0013F679 File Offset: 0x0013D879
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxFloodTool.instance = this;
		this.floodCriteria = (int cell) => Grid.IsValidCell(cell) && Grid.Element[cell] == Grid.Element[this.mouseCell] && Grid.WorldIdx[cell] == Grid.WorldIdx[this.mouseCell];
		this.paintArea = delegate(HashSet<int> cells)
		{
			foreach (int num in cells)
			{
				this.PaintCell(num);
			}
		};
	}

	// Token: 0x060039C7 RID: 14791 RVA: 0x0013F6AC File Offset: 0x0013D8AC
	private void PaintCell(int cell)
	{
		this.recentlyAffectedCells.Add(cell);
		Game.CallbackInfo callbackInfo = new Game.CallbackInfo(delegate
		{
			this.recentlyAffectedCells.Remove(cell);
		}, false);
		Element element = ElementLoader.elements[this.settings.GetIntSetting("SandboxTools.SelectedElement")];
		byte b = Db.Get().Diseases.GetIndex(Db.Get().Diseases.Get("FoodPoisoning").id);
		Disease disease = Db.Get().Diseases.TryGet(this.settings.GetStringSetting("SandboxTools.SelectedDisease"));
		if (disease != null)
		{
			b = Db.Get().Diseases.GetIndex(disease.id);
		}
		int index = Game.Instance.callbackManager.Add(callbackInfo).index;
		int cell2 = cell;
		SimHashes id = element.id;
		CellElementEvent sandBoxTool = CellEventLogger.Instance.SandBoxTool;
		float floatSetting = this.settings.GetFloatSetting("SandboxTools.Mass");
		float floatSetting2 = this.settings.GetFloatSetting("SandbosTools.Temperature");
		int num = index;
		SimMessages.ReplaceElement(cell2, id, sandBoxTool, floatSetting, floatSetting2, b, this.settings.GetIntSetting("SandboxTools.DiseaseCount"), num);
	}

	// Token: 0x17000424 RID: 1060
	// (get) Token: 0x060039C8 RID: 14792 RVA: 0x0013F7E0 File Offset: 0x0013D9E0
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x060039C9 RID: 14793 RVA: 0x0013F7EC File Offset: 0x0013D9EC
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060039CA RID: 14794 RVA: 0x0013F7FC File Offset: 0x0013D9FC
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.massSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.temperatureSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.elementSelector.row.SetActive(true);
		SandboxToolParameterMenu.instance.diseaseSelector.row.SetActive(true);
		SandboxToolParameterMenu.instance.diseaseCountSlider.row.SetActive(true);
	}

	// Token: 0x060039CB RID: 14795 RVA: 0x0013F892 File Offset: 0x0013DA92
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060039CC RID: 14796 RVA: 0x0013F8AC File Offset: 0x0013DAAC
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.recentlyAffectedCells)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.recentlyAffectedCellColor));
		}
		foreach (int num2 in this.cellsToAffect)
		{
			colors.Add(new ToolMenu.CellColorData(num2, this.areaColour));
		}
	}

	// Token: 0x060039CD RID: 14797 RVA: 0x0013F968 File Offset: 0x0013DB68
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
		this.cellsToAffect = base.Flood(Grid.PosToCell(cursorPos));
	}

	// Token: 0x060039CE RID: 14798 RVA: 0x0013F984 File Offset: 0x0013DB84
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

	// Token: 0x04002610 RID: 9744
	public static SandboxFloodTool instance;

	// Token: 0x04002611 RID: 9745
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x04002612 RID: 9746
	protected HashSet<int> cellsToAffect = new HashSet<int>();

	// Token: 0x04002613 RID: 9747
	protected Color recentlyAffectedCellColor = new Color(1f, 1f, 1f, 0.1f);
}
