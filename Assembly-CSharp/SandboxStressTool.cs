using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020007DF RID: 2015
public class SandboxStressTool : BrushTool
{
	// Token: 0x060039F9 RID: 14841 RVA: 0x00140895 File Offset: 0x0013EA95
	public static void DestroyInstance()
	{
		SandboxStressTool.instance = null;
	}

	// Token: 0x17000427 RID: 1063
	// (get) Token: 0x060039FA RID: 14842 RVA: 0x0014089D File Offset: 0x0013EA9D
	public override string[] DlcIDs
	{
		get
		{
			return DlcManager.AVAILABLE_ALL_VERSIONS;
		}
	}

	// Token: 0x17000428 RID: 1064
	// (get) Token: 0x060039FB RID: 14843 RVA: 0x001408A4 File Offset: 0x0013EAA4
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x060039FC RID: 14844 RVA: 0x001408B0 File Offset: 0x0013EAB0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxStressTool.instance = this;
	}

	// Token: 0x060039FD RID: 14845 RVA: 0x001408BE File Offset: 0x0013EABE
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060039FE RID: 14846 RVA: 0x001408CC File Offset: 0x0013EACC
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.stressAdditiveSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.stressAdditiveSlider.SetValue(5f, true);
		SandboxToolParameterMenu.instance.moraleSlider.SetValue(0f, true);
		if (DebugHandler.InstantBuildMode)
		{
			SandboxToolParameterMenu.instance.moraleSlider.row.SetActive(true);
		}
	}

	// Token: 0x060039FF RID: 14847 RVA: 0x00140969 File Offset: 0x0013EB69
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x06003A00 RID: 14848 RVA: 0x00140984 File Offset: 0x0013EB84
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

	// Token: 0x06003A01 RID: 14849 RVA: 0x00140A3C File Offset: 0x0013EC3C
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x06003A02 RID: 14850 RVA: 0x00140A48 File Offset: 0x0013EC48
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
		{
			GameObject gameObject = Components.LiveMinionIdentities[i].gameObject;
			if (Grid.PosToCell(gameObject) == cell)
			{
				float num = -1f * SandboxToolParameterMenu.instance.settings.GetFloatSetting("SandbosTools.StressAdditive");
				Db.Get().Amounts.Stress.Lookup(Components.LiveMinionIdentities[i].gameObject).ApplyDelta(num);
				PopFXManager.Instance.SpawnFX((num >= 0f) ? Assets.GetSprite("crew_state_angry") : Assets.GetSprite("crew_state_happy"), GameUtil.GetFormattedPercent(num, GameUtil.TimeSlice.None), gameObject.transform, 1.5f, false);
				int intSetting = SandboxToolParameterMenu.instance.settings.GetIntSetting("SandbosTools.MoraleAdjustment");
				AttributeInstance attributeInstance = gameObject.GetAttributes().Get(Db.Get().Attributes.QualityOfLife);
				MinionIdentity component = gameObject.GetComponent<MinionIdentity>();
				if (this.moraleAdjustments.ContainsKey(component))
				{
					attributeInstance.Remove(this.moraleAdjustments[component]);
					this.moraleAdjustments.Remove(component);
				}
				if (intSetting != 0)
				{
					AttributeModifier attributeModifier = new AttributeModifier(attributeInstance.Id, (float)intSetting, () => DUPLICANTS.MODIFIERS.SANDBOXMORALEADJUSTMENT.NAME, false, false);
					attributeModifier.SetValue((float)intSetting);
					attributeInstance.Add(attributeModifier);
					this.moraleAdjustments.Add(component, attributeModifier);
				}
			}
		}
	}

	// Token: 0x0400261E RID: 9758
	public static SandboxStressTool instance;

	// Token: 0x0400261F RID: 9759
	protected HashSet<int> recentlyAffectedCells = new HashSet<int>();

	// Token: 0x04002620 RID: 9760
	protected Color recentlyAffectedCellColor = new Color(1f, 1f, 1f, 0.1f);

	// Token: 0x04002621 RID: 9761
	private Dictionary<MinionIdentity, AttributeModifier> moraleAdjustments = new Dictionary<MinionIdentity, AttributeModifier>();
}
