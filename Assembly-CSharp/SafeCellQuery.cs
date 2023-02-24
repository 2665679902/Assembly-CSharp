using System;

// Token: 0x020003DA RID: 986
public class SafeCellQuery : PathFinderQuery
{
	// Token: 0x06001461 RID: 5217 RVA: 0x0006B9E5 File Offset: 0x00069BE5
	public SafeCellQuery Reset(MinionBrain brain, bool avoid_light)
	{
		this.brain = brain;
		this.targetCell = PathFinder.InvalidCell;
		this.targetCost = int.MaxValue;
		this.targetCellFlags = (SafeCellQuery.SafeFlags)0;
		this.avoid_light = avoid_light;
		return this;
	}

	// Token: 0x06001462 RID: 5218 RVA: 0x0006BA14 File Offset: 0x00069C14
	public static SafeCellQuery.SafeFlags GetFlags(int cell, MinionBrain brain, bool avoid_light = false)
	{
		int num = Grid.CellAbove(cell);
		if (!Grid.IsValidCell(num))
		{
			return (SafeCellQuery.SafeFlags)0;
		}
		if (Grid.Solid[cell] || Grid.Solid[num])
		{
			return (SafeCellQuery.SafeFlags)0;
		}
		if (Grid.IsTileUnderConstruction[cell] || Grid.IsTileUnderConstruction[num])
		{
			return (SafeCellQuery.SafeFlags)0;
		}
		bool flag = brain.IsCellClear(cell);
		bool flag2 = !Grid.Element[cell].IsLiquid;
		bool flag3 = !Grid.Element[num].IsLiquid;
		bool flag4 = Grid.Temperature[cell] > 285.15f && Grid.Temperature[cell] < 303.15f;
		bool flag5 = Grid.Radiation[cell] < 250f;
		bool flag6 = brain.OxygenBreather.IsBreathableElementAtCell(cell, Grid.DefaultOffset);
		bool flag7 = !brain.Navigator.NavGrid.NavTable.IsValid(cell, NavType.Ladder) && !brain.Navigator.NavGrid.NavTable.IsValid(cell, NavType.Pole);
		bool flag8 = !brain.Navigator.NavGrid.NavTable.IsValid(cell, NavType.Tube);
		bool flag9 = !avoid_light || SleepChore.IsDarkAtCell(cell);
		if (cell == Grid.PosToCell(brain))
		{
			flag6 = !brain.OxygenBreather.IsSuffocating;
		}
		SafeCellQuery.SafeFlags safeFlags = (SafeCellQuery.SafeFlags)0;
		if (flag)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsClear;
		}
		if (flag4)
		{
			safeFlags |= SafeCellQuery.SafeFlags.CorrectTemperature;
		}
		if (flag5)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotRadiated;
		}
		if (flag6)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsBreathable;
		}
		if (flag7)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotLadder;
		}
		if (flag8)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotTube;
		}
		if (flag2)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotLiquid;
		}
		if (flag3)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotLiquidOnMyFace;
		}
		if (flag9)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsLightOk;
		}
		return safeFlags;
	}

	// Token: 0x06001463 RID: 5219 RVA: 0x0006BBC4 File Offset: 0x00069DC4
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		SafeCellQuery.SafeFlags flags = SafeCellQuery.GetFlags(cell, this.brain, this.avoid_light);
		bool flag = flags > this.targetCellFlags;
		bool flag2 = flags == this.targetCellFlags && cost < this.targetCost;
		if (flag || flag2)
		{
			this.targetCellFlags = flags;
			this.targetCost = cost;
			this.targetCell = cell;
		}
		return false;
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x0006BC1D File Offset: 0x00069E1D
	public override int GetResultCell()
	{
		return this.targetCell;
	}

	// Token: 0x04000B67 RID: 2919
	private MinionBrain brain;

	// Token: 0x04000B68 RID: 2920
	private int targetCell;

	// Token: 0x04000B69 RID: 2921
	private int targetCost;

	// Token: 0x04000B6A RID: 2922
	public SafeCellQuery.SafeFlags targetCellFlags;

	// Token: 0x04000B6B RID: 2923
	private bool avoid_light;

	// Token: 0x02000FFA RID: 4090
	public enum SafeFlags
	{
		// Token: 0x0400561A RID: 22042
		IsClear = 1,
		// Token: 0x0400561B RID: 22043
		IsLightOk,
		// Token: 0x0400561C RID: 22044
		IsNotLadder = 4,
		// Token: 0x0400561D RID: 22045
		IsNotTube = 8,
		// Token: 0x0400561E RID: 22046
		CorrectTemperature = 16,
		// Token: 0x0400561F RID: 22047
		IsNotRadiated = 32,
		// Token: 0x04005620 RID: 22048
		IsBreathable = 64,
		// Token: 0x04005621 RID: 22049
		IsNotLiquidOnMyFace = 128,
		// Token: 0x04005622 RID: 22050
		IsNotLiquid = 256
	}
}
