using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000976 RID: 2422
public class ConditionFlightPathIsClear : ProcessCondition
{
	// Token: 0x060047EF RID: 18415 RVA: 0x00194A2A File Offset: 0x00192C2A
	public ConditionFlightPathIsClear(GameObject module, int bufferWidth)
	{
		this.module = module;
		this.bufferWidth = bufferWidth;
	}

	// Token: 0x060047F0 RID: 18416 RVA: 0x00194A47 File Offset: 0x00192C47
	public override ProcessCondition.Status EvaluateCondition()
	{
		this.Update();
		if (!this.hasClearSky)
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x060047F1 RID: 18417 RVA: 0x00194A5A File Offset: 0x00192C5A
	public override StatusItem GetStatusItem(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Failure)
		{
			return Db.Get().BuildingStatusItems.PathNotClear;
		}
		return null;
	}

	// Token: 0x060047F2 RID: 18418 RVA: 0x00194A70 File Offset: 0x00192C70
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.FLIGHT_PATH_CLEAR.STATUS.READY : UI.STARMAP.LAUNCHCHECKLIST.FLIGHT_PATH_CLEAR.STATUS.FAILURE;
		}
		if (status != ProcessCondition.Status.Ready)
		{
			return Db.Get().BuildingStatusItems.PathNotClear.notificationText;
		}
		global::Debug.LogError("ConditionFlightPathIsClear: You'll need to add new strings/status items if you want to show the ready state");
		return "";
	}

	// Token: 0x060047F3 RID: 18419 RVA: 0x00194AC4 File Offset: 0x00192CC4
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.FLIGHT_PATH_CLEAR.TOOLTIP.READY : UI.STARMAP.LAUNCHCHECKLIST.FLIGHT_PATH_CLEAR.TOOLTIP.FAILURE;
		}
		if (status != ProcessCondition.Status.Ready)
		{
			return Db.Get().BuildingStatusItems.PathNotClear.notificationTooltipText;
		}
		global::Debug.LogError("ConditionFlightPathIsClear: You'll need to add new strings/status items if you want to show the ready state");
		return "";
	}

	// Token: 0x060047F4 RID: 18420 RVA: 0x00194B16 File Offset: 0x00192D16
	public override bool ShowInUI()
	{
		return DlcManager.FeatureClusterSpaceEnabled();
	}

	// Token: 0x060047F5 RID: 18421 RVA: 0x00194B20 File Offset: 0x00192D20
	public void Update()
	{
		Extents extents = this.module.GetComponent<Building>().GetExtents();
		int num = extents.x - this.bufferWidth;
		int num2 = extents.x + extents.width - 1 + this.bufferWidth;
		int y = extents.y;
		int num3 = Grid.XYToCell(num, y);
		int num4 = Grid.XYToCell(num2, y);
		this.hasClearSky = true;
		this.obstructedTile = -1;
		for (int i = num3; i <= num4; i++)
		{
			if (!ConditionFlightPathIsClear.CanReachSpace(i, out this.obstructedTile))
			{
				this.hasClearSky = false;
				return;
			}
		}
	}

	// Token: 0x060047F6 RID: 18422 RVA: 0x00194BB0 File Offset: 0x00192DB0
	public static int PadTopEdgeDistanceToCeilingEdge(GameObject launchpad)
	{
		Vector2 maximumBounds = launchpad.GetMyWorld().maximumBounds;
		int num = (int)launchpad.GetMyWorld().maximumBounds.y;
		int y = Grid.CellToXY(launchpad.GetComponent<LaunchPad>().RocketBottomPosition).y;
		return num - Grid.TopBorderHeight - y + 1;
	}

	// Token: 0x060047F7 RID: 18423 RVA: 0x00194BFC File Offset: 0x00192DFC
	public static bool CheckFlightPathClear(CraftModuleInterface craft, GameObject launchpad, out int obstruction)
	{
		Vector2I vector2I = Grid.CellToXY(launchpad.GetComponent<LaunchPad>().RocketBottomPosition);
		int num = ConditionFlightPathIsClear.PadTopEdgeDistanceToCeilingEdge(launchpad);
		foreach (Ref<RocketModuleCluster> @ref in craft.ClusterModules)
		{
			Building component = @ref.Get().GetComponent<Building>();
			int widthInCells = component.Def.WidthInCells;
			int moduleRelativeVerticalPosition = craft.GetModuleRelativeVerticalPosition(@ref.Get().gameObject);
			if (moduleRelativeVerticalPosition + component.Def.HeightInCells > num)
			{
				int num2 = Grid.XYToCell(vector2I.x, moduleRelativeVerticalPosition + vector2I.y);
				obstruction = num2;
				return false;
			}
			for (int i = moduleRelativeVerticalPosition; i < num; i++)
			{
				for (int j = 0; j < widthInCells; j++)
				{
					int num3 = Grid.XYToCell(j + (vector2I.x - widthInCells / 2), i + vector2I.y);
					bool flag = Grid.Solid[num3];
					if (!Grid.IsValidCell(num3) || Grid.WorldIdx[num3] != Grid.WorldIdx[launchpad.GetComponent<LaunchPad>().RocketBottomPosition] || flag)
					{
						obstruction = num3;
						return false;
					}
				}
			}
		}
		obstruction = -1;
		return true;
	}

	// Token: 0x060047F8 RID: 18424 RVA: 0x00194D5C File Offset: 0x00192F5C
	private static bool CanReachSpace(int startCell, out int obstruction)
	{
		WorldContainer worldContainer = ((startCell >= 0) ? ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[startCell]) : null);
		int num = ((worldContainer == null) ? Grid.HeightInCells : ((int)worldContainer.maximumBounds.y));
		obstruction = -1;
		int num2 = startCell;
		while (Grid.CellRow(num2) < num)
		{
			if (!Grid.IsValidCell(num2) || Grid.Solid[num2])
			{
				obstruction = num2;
				return false;
			}
			num2 = Grid.CellAbove(num2);
		}
		return true;
	}

	// Token: 0x060047F9 RID: 18425 RVA: 0x00194DD4 File Offset: 0x00192FD4
	public string GetObstruction()
	{
		if (this.obstructedTile == -1)
		{
			return null;
		}
		if (Grid.Objects[this.obstructedTile, 1] != null)
		{
			return Grid.Objects[this.obstructedTile, 1].GetComponent<Building>().Def.Name;
		}
		return string.Format(BUILDING.STATUSITEMS.PATH_NOT_CLEAR.TILE_FORMAT, Grid.Element[this.obstructedTile].tag.ProperName());
	}

	// Token: 0x04002F77 RID: 12151
	private GameObject module;

	// Token: 0x04002F78 RID: 12152
	private int bufferWidth;

	// Token: 0x04002F79 RID: 12153
	private bool hasClearSky;

	// Token: 0x04002F7A RID: 12154
	private int obstructedTile = -1;

	// Token: 0x04002F7B RID: 12155
	public const int MAXIMUM_ROCKET_HEIGHT = 35;
}
