using System;
using UnityEngine;

// Token: 0x020004A9 RID: 1193
public class NavTactic
{
	// Token: 0x06001AFD RID: 6909 RVA: 0x0009033E File Offset: 0x0008E53E
	public NavTactic(int preferredRange, int rangePenalty = 1, int overlapPenalty = 1, int pathCostPenalty = 1)
	{
		this._overlapPenalty = overlapPenalty;
		this._preferredRange = preferredRange;
		this._rangePenalty = rangePenalty;
		this._pathCostPenalty = pathCostPenalty;
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x00090378 File Offset: 0x0008E578
	public int GetCellPreferences(int root, CellOffset[] offsets, Navigator navigator)
	{
		int num = NavigationReservations.InvalidReservation;
		int num2 = int.MaxValue;
		for (int i = 0; i < offsets.Length; i++)
		{
			int num3 = Grid.OffsetCell(root, offsets[i]);
			int num4 = 0;
			num4 += this._overlapPenalty * NavigationReservations.Instance.GetOccupancyCount(num3);
			num4 += this._rangePenalty * Mathf.Abs(this._preferredRange - Grid.GetCellDistance(root, num3));
			num4 += this._pathCostPenalty * Mathf.Max(navigator.GetNavigationCost(num3), 0);
			if (num4 < num2 && navigator.CanReach(num3))
			{
				num2 = num4;
				num = num3;
			}
		}
		return num;
	}

	// Token: 0x04000EFD RID: 3837
	private int _overlapPenalty = 3;

	// Token: 0x04000EFE RID: 3838
	private int _preferredRange;

	// Token: 0x04000EFF RID: 3839
	private int _rangePenalty = 2;

	// Token: 0x04000F00 RID: 3840
	private int _pathCostPenalty = 1;
}
