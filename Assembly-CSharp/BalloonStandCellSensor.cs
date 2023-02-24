using System;
using UnityEngine;

// Token: 0x020003E9 RID: 1001
public class BalloonStandCellSensor : Sensor
{
	// Token: 0x060014C8 RID: 5320 RVA: 0x0006D112 File Offset: 0x0006B312
	public BalloonStandCellSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = base.GetComponent<Navigator>();
		this.brain = base.GetComponent<MinionBrain>();
	}

	// Token: 0x060014C9 RID: 5321 RVA: 0x0006D134 File Offset: 0x0006B334
	public override void Update()
	{
		this.cell = Grid.InvalidCell;
		int num = int.MaxValue;
		ListPool<int[], BalloonStandCellSensor>.PooledList pooledList = ListPool<int[], BalloonStandCellSensor>.Allocate();
		int num2 = 50;
		foreach (int num3 in Game.Instance.mingleCellTracker.mingleCells)
		{
			if (this.brain.IsCellClear(num3))
			{
				int navigationCost = this.navigator.GetNavigationCost(num3);
				if (navigationCost != -1)
				{
					if (num3 == Grid.InvalidCell || navigationCost < num)
					{
						this.cell = num3;
						num = navigationCost;
					}
					if (navigationCost < num2)
					{
						int num4 = Grid.CellRight(num3);
						int num5 = Grid.CellRight(num4);
						int num6 = Grid.CellLeft(num3);
						int num7 = Grid.CellLeft(num6);
						CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(this.cell);
						CavityInfo cavityForCell2 = Game.Instance.roomProber.GetCavityForCell(num7);
						CavityInfo cavityForCell3 = Game.Instance.roomProber.GetCavityForCell(num5);
						if (cavityForCell != null)
						{
							if (cavityForCell3 != null && cavityForCell3.handle == cavityForCell.handle && this.navigator.NavGrid.NavTable.IsValid(num4, NavType.Floor) && this.navigator.NavGrid.NavTable.IsValid(num5, NavType.Floor))
							{
								pooledList.Add(new int[] { num3, num5 });
							}
							if (cavityForCell2 != null && cavityForCell2.handle == cavityForCell.handle && this.navigator.NavGrid.NavTable.IsValid(num6, NavType.Floor) && this.navigator.NavGrid.NavTable.IsValid(num7, NavType.Floor))
							{
								pooledList.Add(new int[] { num3, num7 });
							}
						}
					}
				}
			}
		}
		if (pooledList.Count > 0)
		{
			int[] array = pooledList[UnityEngine.Random.Range(0, pooledList.Count)];
			this.cell = array[0];
			this.standCell = array[1];
		}
		else if (Components.Telepads.Count > 0)
		{
			Telepad telepad = Components.Telepads.Items[0];
			if (telepad == null || !telepad.GetComponent<Operational>().IsOperational)
			{
				return;
			}
			int num8 = Grid.PosToCell(telepad.transform.GetPosition());
			num8 = Grid.CellLeft(num8);
			int num9 = Grid.CellRight(num8);
			int num10 = Grid.CellRight(num9);
			bool cavityForCell4 = Game.Instance.roomProber.GetCavityForCell(num8) != null;
			CavityInfo cavityForCell5 = Game.Instance.roomProber.GetCavityForCell(num10);
			if (cavityForCell4 && cavityForCell5 != null && this.navigator.NavGrid.NavTable.IsValid(num8, NavType.Floor) && this.navigator.NavGrid.NavTable.IsValid(num9, NavType.Floor) && this.navigator.NavGrid.NavTable.IsValid(num10, NavType.Floor))
			{
				this.cell = num8;
				this.standCell = num10;
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x0006D45C File Offset: 0x0006B65C
	public int GetCell()
	{
		return this.cell;
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x0006D464 File Offset: 0x0006B664
	public int GetStandCell()
	{
		return this.standCell;
	}

	// Token: 0x04000BA0 RID: 2976
	private MinionBrain brain;

	// Token: 0x04000BA1 RID: 2977
	private Navigator navigator;

	// Token: 0x04000BA2 RID: 2978
	private int cell;

	// Token: 0x04000BA3 RID: 2979
	private int standCell;
}
