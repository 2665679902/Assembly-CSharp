using System;
using Klei.AI;

// Token: 0x020003F0 RID: 1008
public class SafeCellSensor : Sensor
{
	// Token: 0x060014DD RID: 5341 RVA: 0x0006D808 File Offset: 0x0006BA08
	public SafeCellSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = base.GetComponent<Navigator>();
		this.brain = base.GetComponent<MinionBrain>();
		this.prefabid = base.GetComponent<KPrefabID>();
		this.traits = base.GetComponent<Traits>();
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x0006D858 File Offset: 0x0006BA58
	public override void Update()
	{
		if (!this.prefabid.HasTag(GameTags.Idle))
		{
			this.cell = Grid.InvalidCell;
			return;
		}
		bool flag = this.HasSafeCell();
		this.RunSafeCellQuery(false);
		bool flag2 = this.HasSafeCell();
		if (flag2 != flag)
		{
			if (flag2)
			{
				this.sensors.Trigger(982561777, null);
				return;
			}
			this.sensors.Trigger(506919987, null);
		}
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x0006D8C4 File Offset: 0x0006BAC4
	public void RunSafeCellQuery(bool avoid_light)
	{
		MinionPathFinderAbilities minionPathFinderAbilities = (MinionPathFinderAbilities)this.navigator.GetCurrentAbilities();
		minionPathFinderAbilities.SetIdleNavMaskEnabled(true);
		SafeCellQuery safeCellQuery = PathFinderQueries.safeCellQuery.Reset(this.brain, avoid_light);
		this.navigator.RunQuery(safeCellQuery);
		minionPathFinderAbilities.SetIdleNavMaskEnabled(false);
		this.cell = safeCellQuery.GetResultCell();
		if (this.cell == Grid.PosToCell(this.navigator))
		{
			this.cell = Grid.InvalidCell;
		}
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x0006D936 File Offset: 0x0006BB36
	public int GetSensorCell()
	{
		return this.cell;
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x0006D93E File Offset: 0x0006BB3E
	public int GetCellQuery()
	{
		if (this.cell == Grid.InvalidCell)
		{
			this.RunSafeCellQuery(false);
		}
		return this.cell;
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x0006D95A File Offset: 0x0006BB5A
	public int GetSleepCellQuery()
	{
		if (this.cell == Grid.InvalidCell)
		{
			this.RunSafeCellQuery(!this.traits.HasTrait("NightLight"));
		}
		return this.cell;
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x0006D98B File Offset: 0x0006BB8B
	public bool HasSafeCell()
	{
		return this.cell != Grid.InvalidCell && this.cell != Grid.PosToCell(this.sensors);
	}

	// Token: 0x04000BB3 RID: 2995
	private MinionBrain brain;

	// Token: 0x04000BB4 RID: 2996
	private Navigator navigator;

	// Token: 0x04000BB5 RID: 2997
	private KPrefabID prefabid;

	// Token: 0x04000BB6 RID: 2998
	private Traits traits;

	// Token: 0x04000BB7 RID: 2999
	private int cell = Grid.InvalidCell;
}
