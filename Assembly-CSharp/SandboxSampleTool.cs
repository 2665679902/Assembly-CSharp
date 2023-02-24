using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020007DC RID: 2012
public class SandboxSampleTool : InterfaceTool
{
	// Token: 0x060039DC RID: 14812 RVA: 0x0013FD6D File Offset: 0x0013DF6D
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		colors.Add(new ToolMenu.CellColorData(this.currentCell, this.radiusIndicatorColor));
	}

	// Token: 0x060039DD RID: 14813 RVA: 0x0013FD8F File Offset: 0x0013DF8F
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
		this.currentCell = Grid.PosToCell(cursorPos);
	}

	// Token: 0x060039DE RID: 14814 RVA: 0x0013FDA4 File Offset: 0x0013DFA4
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		int num = Grid.PosToCell(cursor_pos);
		if (!Grid.IsValidCell(num))
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.DEBUG_TOOLS.INVALID_LOCATION, null, cursor_pos, 1.5f, false, true);
			return;
		}
		SandboxSampleTool.Sample(num);
	}

	// Token: 0x060039DF RID: 14815 RVA: 0x0013FDF0 File Offset: 0x0013DFF0
	public static void Sample(int cell)
	{
		UISounds.PlaySound(UISounds.Sound.ClickObject);
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandboxTools.SelectedElement", (int)Grid.Element[cell].idx);
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandboxTools.Mass", Mathf.Round(Grid.Mass[cell] * 100f) / 100f);
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandbosTools.Temperature", Mathf.Round(Grid.Temperature[cell] * 10f) / 10f);
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandboxTools.DiseaseCount", Grid.DiseaseCount[cell]);
		SandboxToolParameterMenu.instance.RefreshDisplay();
	}

	// Token: 0x060039E0 RID: 14816 RVA: 0x0013FEAC File Offset: 0x0013E0AC
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

	// Token: 0x060039E1 RID: 14817 RVA: 0x0013FF42 File Offset: 0x0013E142
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x04002617 RID: 9751
	protected Color radiusIndicatorColor = new Color(0.5f, 0.7f, 0.5f, 0.2f);

	// Token: 0x04002618 RID: 9752
	private int currentCell;
}
