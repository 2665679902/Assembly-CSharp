using System;
using UnityEngine;

// Token: 0x020007BC RID: 1980
public class AttackTool : DragTool
{
	// Token: 0x06003849 RID: 14409 RVA: 0x00137CDC File Offset: 0x00135EDC
	protected override void OnDragComplete(Vector3 downPos, Vector3 upPos)
	{
		Vector2 regularizedPos = base.GetRegularizedPos(Vector2.Min(downPos, upPos), true);
		Vector2 regularizedPos2 = base.GetRegularizedPos(Vector2.Max(downPos, upPos), false);
		AttackTool.MarkForAttack(regularizedPos, regularizedPos2, true);
	}

	// Token: 0x0600384A RID: 14410 RVA: 0x00137D24 File Offset: 0x00135F24
	public static void MarkForAttack(Vector2 min, Vector2 max, bool mark)
	{
		foreach (FactionAlignment factionAlignment in Components.FactionAlignments.Items)
		{
			Vector2 vector = Grid.PosToXY(factionAlignment.transform.GetPosition());
			if (vector.x >= min.x && vector.x < max.x && vector.y >= min.y && vector.y < max.y)
			{
				if (mark)
				{
					if (FactionManager.Instance.GetDisposition(FactionManager.FactionID.Duplicant, factionAlignment.Alignment) != FactionManager.Disposition.Assist)
					{
						factionAlignment.SetPlayerTargeted(true);
						Prioritizable component = factionAlignment.GetComponent<Prioritizable>();
						if (component != null)
						{
							component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
						}
					}
				}
				else
				{
					factionAlignment.gameObject.Trigger(2127324410, null);
				}
			}
		}
	}

	// Token: 0x0600384B RID: 14411 RVA: 0x00137E20 File Offset: 0x00136020
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		ToolMenu.Instance.PriorityScreen.Show(true);
	}

	// Token: 0x0600384C RID: 14412 RVA: 0x00137E38 File Offset: 0x00136038
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		ToolMenu.Instance.PriorityScreen.Show(false);
	}
}
