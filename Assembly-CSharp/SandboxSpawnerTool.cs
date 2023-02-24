using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007DD RID: 2013
public class SandboxSpawnerTool : InterfaceTool
{
	// Token: 0x060039E3 RID: 14819 RVA: 0x0013FF82 File Offset: 0x0013E182
	public override void GetOverlayColorData(out HashSet<ToolMenu.CellColorData> colors)
	{
		colors = new HashSet<ToolMenu.CellColorData>();
		colors.Add(new ToolMenu.CellColorData(this.currentCell, this.radiusIndicatorColor));
	}

	// Token: 0x060039E4 RID: 14820 RVA: 0x0013FFA4 File Offset: 0x0013E1A4
	public override void OnMouseMove(Vector3 cursorPos)
	{
		base.OnMouseMove(cursorPos);
		this.currentCell = Grid.PosToCell(cursorPos);
	}

	// Token: 0x060039E5 RID: 14821 RVA: 0x0013FFB9 File Offset: 0x0013E1B9
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		this.Place(Grid.PosToCell(cursor_pos));
	}

	// Token: 0x060039E6 RID: 14822 RVA: 0x0013FFC8 File Offset: 0x0013E1C8
	private void Place(int cell)
	{
		if (!Grid.IsValidBuildingCell(cell))
		{
			return;
		}
		string stringSetting = SandboxToolParameterMenu.instance.settings.GetStringSetting("SandboxTools.SelectedEntity");
		GameObject prefab = Assets.GetPrefab(stringSetting);
		if (stringSetting == MinionConfig.ID)
		{
			this.SpawnMinion();
		}
		else if (prefab.GetComponent<Building>() != null)
		{
			BuildingDef def = prefab.GetComponent<Building>().Def;
			def.Build(cell, Orientation.Neutral, null, def.DefaultElements(), 298.15f, true, -1f);
		}
		else
		{
			GameUtil.KInstantiate(prefab, Grid.CellToPosCBC(this.currentCell, Grid.SceneLayer.Creatures), Grid.SceneLayer.Creatures, null, 0).SetActive(true);
		}
		UISounds.PlaySound(UISounds.Sound.ClickObject);
	}

	// Token: 0x060039E7 RID: 14823 RVA: 0x0014006C File Offset: 0x0013E26C
	protected override void OnActivateTool()
	{
		base.OnActivateTool();
		SandboxToolParameterMenu.instance.gameObject.SetActive(true);
		SandboxToolParameterMenu.instance.DisableParameters();
		SandboxToolParameterMenu.instance.entitySelector.row.SetActive(true);
	}

	// Token: 0x060039E8 RID: 14824 RVA: 0x001400A3 File Offset: 0x0013E2A3
	protected override void OnDeactivateTool(InterfaceTool new_tool)
	{
		base.OnDeactivateTool(new_tool);
		SandboxToolParameterMenu.instance.gameObject.SetActive(false);
	}

	// Token: 0x060039E9 RID: 14825 RVA: 0x001400BC File Offset: 0x0013E2BC
	private void SpawnMinion()
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(MinionConfig.ID), null, null);
		gameObject.name = Assets.GetPrefab(MinionConfig.ID).name;
		Immigration.Instance.ApplyDefaultPersonalPriorities(gameObject);
		Vector3 vector = Grid.CellToPosCBC(this.currentCell, Grid.SceneLayer.Move);
		gameObject.transform.SetLocalPosition(vector);
		gameObject.SetActive(true);
		new MinionStartingStats(false, null, null, false).Apply(gameObject);
	}

	// Token: 0x060039EA RID: 14826 RVA: 0x00140138 File Offset: 0x0013E338
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.SandboxCopyElement))
		{
			int num = Grid.PosToCell(PlayerController.GetCursorPos(KInputManager.GetMousePos()));
			List<ObjectLayer> list = new List<ObjectLayer>();
			list.Add(ObjectLayer.Pickupables);
			list.Add(ObjectLayer.Plants);
			list.Add(ObjectLayer.Minion);
			list.Add(ObjectLayer.Building);
			if (Grid.IsValidCell(num))
			{
				foreach (ObjectLayer objectLayer in list)
				{
					GameObject gameObject = Grid.Objects[num, (int)objectLayer];
					if (gameObject)
					{
						SandboxToolParameterMenu.instance.settings.SetStringSetting("SandboxTools.SelectedEntity", gameObject.PrefabID().ToString());
						break;
					}
				}
			}
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x04002619 RID: 9753
	protected Color radiusIndicatorColor = new Color(0.5f, 0.7f, 0.5f, 0.2f);

	// Token: 0x0400261A RID: 9754
	private int currentCell;
}
