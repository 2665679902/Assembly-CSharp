using System;
using STRINGS;
using UnityEngine;

// Token: 0x020007C1 RID: 1985
public class CaptureTool : DragTool
{
	// Token: 0x060038A3 RID: 14499 RVA: 0x0013A33C File Offset: 0x0013853C
	protected override void OnDragComplete(Vector3 downPos, Vector3 upPos)
	{
		Vector2 regularizedPos = base.GetRegularizedPos(Vector2.Min(downPos, upPos), true);
		Vector2 regularizedPos2 = base.GetRegularizedPos(Vector2.Max(downPos, upPos), false);
		CaptureTool.MarkForCapture(regularizedPos, regularizedPos2, true);
	}

	// Token: 0x060038A4 RID: 14500 RVA: 0x0013A384 File Offset: 0x00138584
	public static void MarkForCapture(Vector2 min, Vector2 max, bool mark)
	{
		foreach (Capturable capturable in Components.Capturables.Items)
		{
			Vector2 vector = Grid.PosToXY(capturable.transform.GetPosition());
			if (vector.x >= min.x && vector.x < max.x && vector.y >= min.y && vector.y < max.y)
			{
				if (capturable.allowCapture)
				{
					PrioritySetting lastSelectedPriority = ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority();
					capturable.MarkForCapture(mark, lastSelectedPriority, true);
				}
				else if (mark)
				{
					PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, UI.TOOLS.CAPTURE.NOT_CAPTURABLE, null, capturable.transform.GetPosition(), 1.5f, false, false);
				}
			}
		}
	}

	// Token: 0x060038A5 RID: 14501 RVA: 0x0013A484 File Offset: 0x00138684
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x060038A6 RID: 14502 RVA: 0x0013A49C File Offset: 0x0013869C
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}
}
