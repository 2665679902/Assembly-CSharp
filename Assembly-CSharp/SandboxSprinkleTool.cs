using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x020007DE RID: 2014
public class SandboxSprinkleTool : BrushTool
{
	// Token: 0x060039EC RID: 14828 RVA: 0x00140243 File Offset: 0x0013E443
	public static void DestroyInstance()
	{
		SandboxSprinkleTool.instance = null;
	}

	// Token: 0x17000426 RID: 1062
	// (get) Token: 0x060039ED RID: 14829 RVA: 0x0014024B File Offset: 0x0013E44B
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x060039EE RID: 14830 RVA: 0x00140257 File Offset: 0x0013E457
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxSprinkleTool.instance = this;
	}

	// Token: 0x060039EF RID: 14831 RVA: 0x00140265 File Offset: 0x0013E465
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060039F0 RID: 14832 RVA: 0x00140274 File Offset: 0x0013E474
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.noiseScaleSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.noiseDensitySlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.massSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.temperatureSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.elementSelector.row.SetActive(true);
		SandboxToolParameterMenu.instance.diseaseSelector.row.SetActive(true);
		SandboxToolParameterMenu.instance.diseaseCountSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.brushRadiusSlider.SetValue((float)this.settings.GetIntSetting("SandboxTools.BrushSize"), true);
	}

	// Token: 0x060039F1 RID: 14833 RVA: 0x0014036A File Offset: 0x0013E56A
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060039F2 RID: 14834 RVA: 0x00140384 File Offset: 0x0013E584
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.recentlyAffectedCells)
		{
			Color color = new Color(this.recentAffectedCellColor[num].r, this.recentAffectedCellColor[num].g, this.recentAffectedCellColor[num].b, MathUtil.ReRange(Mathf.Sin(Time.realtimeSinceStartup * 5f), -1f, 1f, 0.1f, 0.2f));
			colors.Add(new ToolMenu.CellColorData(num, color));
		}
		foreach (int num2 in this.cellsInRadius)
		{
			if (this.recentlyAffectedCells.Contains(num2))
			{
				Color radiusIndicatorColor = this.radiusIndicatorColor;
				Color color2 = this.recentAffectedCellColor[num2];
				color2.a = 0.2f;
				Color color3 = new Color((radiusIndicatorColor.r + color2.r) / 2f, (radiusIndicatorColor.g + color2.g) / 2f, (radiusIndicatorColor.b + color2.b) / 2f, radiusIndicatorColor.a + (1f - radiusIndicatorColor.a) * color2.a);
				colors.Add(new ToolMenu.CellColorData(num2, color3));
			}
			else
			{
				colors.Add(new ToolMenu.CellColorData(num2, this.radiusIndicatorColor));
			}
		}
	}

	// Token: 0x060039F3 RID: 14835 RVA: 0x0014054C File Offset: 0x0013E74C
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
					Vector2 vector = Grid.CellToXY(Grid.OffsetCell(this.currentCell, i, j));
					float num = PerlinSimplexNoise.noise(vector.x / this.settings.GetFloatSetting("SandboxTools.NoiseDensity"), vector.y / this.settings.GetFloatSetting("SandboxTools.NoiseDensity"), Time.realtimeSinceStartup);
					if (this.settings.GetFloatSetting("SandboxTools.NoiseScale") <= num)
					{
						this.brushOffsets.Add(new Vector2((float)(i - this.brushRadius), (float)(j - this.brushRadius)));
					}
				}
			}
		}
	}

	// Token: 0x060039F4 RID: 14836 RVA: 0x00140656 File Offset: 0x0013E856
	private void Update()
	{
		this.OnMouseMove(Grid.CellToPos(this.currentCell));
	}

	// Token: 0x060039F5 RID: 14837 RVA: 0x00140669 File Offset: 0x0013E869
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
		this.SetBrushSize(this.settings.GetIntSetting("SandboxTools.BrushSize"));
	}

	// Token: 0x060039F6 RID: 14838 RVA: 0x00140688 File Offset: 0x0013E888
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
		this.SetBrushSize(this.brushRadius);
	}

	// Token: 0x060039F7 RID: 14839 RVA: 0x00140830 File Offset: 0x0013EA30
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

	// Token: 0x0400261B RID: 9755
	public static SandboxSprinkleTool instance;

	// Token: 0x0400261C RID: 9756
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x0400261D RID: 9757
	private Dictionary<int, Color> recentAffectedCellColor = new Dictionary<int, Color>();
}
