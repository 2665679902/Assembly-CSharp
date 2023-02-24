using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007C0 RID: 1984
public class CancelTool : FilteredDragTool
{
	// Token: 0x0600389B RID: 14491 RVA: 0x0013A24F File Offset: 0x0013844F
	public static void DestroyInstance()
	{
		CancelTool.Instance = null;
	}

	// Token: 0x0600389C RID: 14492 RVA: 0x0013A257 File Offset: 0x00138457
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		CancelTool.Instance = this;
	}

	// Token: 0x0600389D RID: 14493 RVA: 0x0013A265 File Offset: 0x00138465
	protected override void GetDefaultFilters(Dictionary<string, ToolParameterMenu.ToggleState> filters)
	{
		base.GetDefaultFilters(filters);
		filters.Add(ToolParameterMenu.FILTERLAYERS.CLEANANDCLEAR, ToolParameterMenu.ToggleState.Off);
		filters.Add(ToolParameterMenu.FILTERLAYERS.DIGPLACER, ToolParameterMenu.ToggleState.Off);
	}

	// Token: 0x0600389E RID: 14494 RVA: 0x0013A286 File Offset: 0x00138486
	protected override string GetConfirmSound()
	{
		return "Tile_Confirm_NegativeTool";
	}

	// Token: 0x0600389F RID: 14495 RVA: 0x0013A28D File Offset: 0x0013848D
	protected override string GetDragSound()
	{
		return "Tile_Drag_NegativeTool";
	}

	// Token: 0x060038A0 RID: 14496 RVA: 0x0013A294 File Offset: 0x00138494
	protected override void OnDragTool(int cell, int distFromOrigin)
	{
		for (int i = 0; i < 44; i++)
		{
			GameObject gameObject = Grid.Objects[cell, i];
			if (gameObject != null)
			{
				string filterLayerFromGameObject = this.GetFilterLayerFromGameObject(gameObject);
				if (base.IsActiveLayer(filterLayerFromGameObject))
				{
					gameObject.Trigger(2127324410, null);
				}
			}
		}
	}

	// Token: 0x060038A1 RID: 14497 RVA: 0x0013A2E4 File Offset: 0x001384E4
	protected override void OnDragComplete(Vector3 downPos, Vector3 upPos)
	{
		Vector2 regularizedPos = base.GetRegularizedPos(Vector2.Min(downPos, upPos), true);
		Vector2 regularizedPos2 = base.GetRegularizedPos(Vector2.Max(downPos, upPos), false);
		AttackTool.MarkForAttack(regularizedPos, regularizedPos2, false);
		CaptureTool.MarkForCapture(regularizedPos, regularizedPos2, false);
	}

	// Token: 0x040025A5 RID: 9637
	public static CancelTool Instance;
}
