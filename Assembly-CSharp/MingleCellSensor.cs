using System;
using UnityEngine;

// Token: 0x020003ED RID: 1005
public class MingleCellSensor : Sensor
{
	// Token: 0x060014D6 RID: 5334 RVA: 0x0006D68A File Offset: 0x0006B88A
	public MingleCellSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = base.GetComponent<Navigator>();
		this.brain = base.GetComponent<MinionBrain>();
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x0006D6AC File Offset: 0x0006B8AC
	public override void Update()
	{
		this.cell = Grid.InvalidCell;
		int num = int.MaxValue;
		ListPool<int, MingleCellSensor>.PooledList pooledList = ListPool<int, MingleCellSensor>.Allocate();
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
						pooledList.Add(num3);
					}
				}
			}
		}
		if (pooledList.Count > 0)
		{
			this.cell = pooledList[UnityEngine.Random.Range(0, pooledList.Count)];
		}
		pooledList.Recycle();
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x0006D78C File Offset: 0x0006B98C
	public int GetCell()
	{
		return this.cell;
	}

	// Token: 0x04000BAD RID: 2989
	private MinionBrain brain;

	// Token: 0x04000BAE RID: 2990
	private Navigator navigator;

	// Token: 0x04000BAF RID: 2991
	private int cell;
}
