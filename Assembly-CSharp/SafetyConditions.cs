using System;
using System.Collections.Generic;

// Token: 0x020003D7 RID: 983
public class SafetyConditions
{
	// Token: 0x06001458 RID: 5208 RVA: 0x0006B56C File Offset: 0x0006976C
	public SafetyConditions()
	{
		int num = 1;
		this.IsNearby = new SafetyChecker.Condition("IsNearby", num *= 2, (int cell, int cost, SafetyChecker.Context context) => cost > 5);
		this.IsNotLedge = new SafetyChecker.Condition("IsNotLedge", num *= 2, delegate(int cell, int cost, SafetyChecker.Context context)
		{
			int num2 = Grid.CellBelow(Grid.CellLeft(cell));
			if (Grid.Solid[num2])
			{
				return false;
			}
			int num3 = Grid.CellBelow(Grid.CellRight(cell));
			return Grid.Solid[num3];
		});
		this.IsNotLiquid = new SafetyChecker.Condition("IsNotLiquid", num *= 2, (int cell, int cost, SafetyChecker.Context context) => !Grid.Element[cell].IsLiquid);
		this.IsNotLadder = new SafetyChecker.Condition("IsNotLadder", num *= 2, (int cell, int cost, SafetyChecker.Context context) => !context.navigator.NavGrid.NavTable.IsValid(cell, NavType.Ladder) && !context.navigator.NavGrid.NavTable.IsValid(cell, NavType.Pole));
		this.IsNotDoor = new SafetyChecker.Condition("IsNotDoor", num *= 2, delegate(int cell, int cost, SafetyChecker.Context context)
		{
			int num4 = Grid.CellAbove(cell);
			return !Grid.HasDoor[cell] && Grid.IsValidCell(num4) && !Grid.HasDoor[num4];
		});
		this.IsCorrectTemperature = new SafetyChecker.Condition("IsCorrectTemperature", num *= 2, (int cell, int cost, SafetyChecker.Context context) => Grid.Temperature[cell] > 285.15f && Grid.Temperature[cell] < 303.15f);
		this.IsWarming = new SafetyChecker.Condition("IsWarming", num *= 2, (int cell, int cost, SafetyChecker.Context context) => true);
		this.IsCooling = new SafetyChecker.Condition("IsCooling", num *= 2, (int cell, int cost, SafetyChecker.Context context) => false);
		this.HasSomeOxygen = new SafetyChecker.Condition("HasSomeOxygen", num *= 2, (int cell, int cost, SafetyChecker.Context context) => context.oxygenBreather.IsBreathableElementAtCell(cell, null));
		this.IsClear = new SafetyChecker.Condition("IsClear", num * 2, (int cell, int cost, SafetyChecker.Context context) => context.minionBrain.IsCellClear(cell));
		this.WarmUpChecker = new SafetyChecker(new List<SafetyChecker.Condition> { this.IsWarming }.ToArray());
		this.CoolDownChecker = new SafetyChecker(new List<SafetyChecker.Condition> { this.IsCooling }.ToArray());
		List<SafetyChecker.Condition> list = new List<SafetyChecker.Condition>();
		list.Add(this.HasSomeOxygen);
		list.Add(this.IsNotDoor);
		this.RecoverBreathChecker = new SafetyChecker(list.ToArray());
		List<SafetyChecker.Condition> list2 = new List<SafetyChecker.Condition>(list);
		list2.Add(this.IsNotLiquid);
		list2.Add(this.IsCorrectTemperature);
		this.SafeCellChecker = new SafetyChecker(list2.ToArray());
		this.IdleCellChecker = new SafetyChecker(new List<SafetyChecker.Condition>(list2) { this.IsClear, this.IsNotLadder }.ToArray());
		this.VomitCellChecker = new SafetyChecker(new List<SafetyChecker.Condition> { this.IsNotLiquid, this.IsNotLedge, this.IsNearby }.ToArray());
	}

	// Token: 0x04000B4E RID: 2894
	public SafetyChecker.Condition IsNotLiquid;

	// Token: 0x04000B4F RID: 2895
	public SafetyChecker.Condition IsNotLadder;

	// Token: 0x04000B50 RID: 2896
	public SafetyChecker.Condition IsCorrectTemperature;

	// Token: 0x04000B51 RID: 2897
	public SafetyChecker.Condition IsWarming;

	// Token: 0x04000B52 RID: 2898
	public SafetyChecker.Condition IsCooling;

	// Token: 0x04000B53 RID: 2899
	public SafetyChecker.Condition HasSomeOxygen;

	// Token: 0x04000B54 RID: 2900
	public SafetyChecker.Condition IsClear;

	// Token: 0x04000B55 RID: 2901
	public SafetyChecker.Condition IsNotFoundation;

	// Token: 0x04000B56 RID: 2902
	public SafetyChecker.Condition IsNotDoor;

	// Token: 0x04000B57 RID: 2903
	public SafetyChecker.Condition IsNotLedge;

	// Token: 0x04000B58 RID: 2904
	public SafetyChecker.Condition IsNearby;

	// Token: 0x04000B59 RID: 2905
	public SafetyChecker WarmUpChecker;

	// Token: 0x04000B5A RID: 2906
	public SafetyChecker CoolDownChecker;

	// Token: 0x04000B5B RID: 2907
	public SafetyChecker RecoverBreathChecker;

	// Token: 0x04000B5C RID: 2908
	public SafetyChecker VomitCellChecker;

	// Token: 0x04000B5D RID: 2909
	public SafetyChecker SafeCellChecker;

	// Token: 0x04000B5E RID: 2910
	public SafetyChecker IdleCellChecker;
}
