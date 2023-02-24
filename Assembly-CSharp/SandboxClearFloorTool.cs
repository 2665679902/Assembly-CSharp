using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020007D6 RID: 2006
public class SandboxClearFloorTool : BrushTool
{
	// Token: 0x0600399E RID: 14750 RVA: 0x0013ECFD File Offset: 0x0013CEFD
	public static void DestroyInstance()
	{
		SandboxClearFloorTool.instance = null;
	}

	// Token: 0x17000421 RID: 1057
	// (get) Token: 0x0600399F RID: 14751 RVA: 0x0013ED05 File Offset: 0x0013CF05
	private SandboxSettings settings
	{
		get
		{
			return SandboxToolParameterMenu.instance.settings;
		}
	}

	// Token: 0x060039A0 RID: 14752 RVA: 0x0013ED11 File Offset: 0x0013CF11
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		SandboxClearFloorTool.instance = this;
	}

	// Token: 0x060039A1 RID: 14753 RVA: 0x0013ED1F File Offset: 0x0013CF1F
	public void Activate()
	{
		PlayerController.Instance.ActivateTool(this);
	}

	// Token: 0x060039A2 RID: 14754 RVA: 0x0013ED2C File Offset: 0x0013CF2C
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.brushRadiusSlider.row.SetActive(true);
		SandboxToolParameterMenu.instance.brushRadiusSlider.SetValue((float)this.settings.GetIntSetting("SandboxTools.BrushSize"), true);
	}

	// Token: 0x060039A3 RID: 14755 RVA: 0x0013ED8F File Offset: 0x0013CF8F
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060039A4 RID: 14756 RVA: 0x0013EDA8 File Offset: 0x0013CFA8
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		foreach (int num in this.cellsInRadius)
		{
			colors.Add(new ToolMenu.CellColorData(num, this.radiusIndicatorColor));
		}
	}

	// Token: 0x060039A5 RID: 14757 RVA: 0x0013EE10 File Offset: 0x0013D010
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
	}

	// Token: 0x060039A6 RID: 14758 RVA: 0x0013EE1C File Offset: 0x0013D01C
	protected override void OnPaintCell(int cell, int distFromOrigin)
	{
		base.OnPaintCell(cell, distFromOrigin);
		bool flag = false;
		using (List<Pickupable>.Enumerator enumerator = Components.Pickupables.Items.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Pickupable pickup = enumerator.Current;
				if (!(pickup.storage != null) && Grid.PosToCell(pickup) == cell && Components.LiveMinionIdentities.Items.Find((MinionIdentity match) => match.gameObject == pickup.gameObject) == null)
				{
					if (!flag)
					{
						UISounds.PlaySound(UISounds.Sound.Negative);
						PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.SANDBOXTOOLS.CLEARFLOOR.DELETED, pickup.transform, 1.5f, false);
						flag = true;
					}
					Util.KDestroyGameObject(pickup.gameObject);
				}
			}
		}
	}

	// Token: 0x04002608 RID: 9736
	public static SandboxClearFloorTool instance;
}
