using System;
using UnityEngine;

// Token: 0x020003EC RID: 1004
public class IdleCellSensor : Sensor
{
	// Token: 0x060014D3 RID: 5331 RVA: 0x0006D5D9 File Offset: 0x0006B7D9
	public IdleCellSensor(Sensors sensors)
		: base(sensors)
	{
		this.navigator = base.GetComponent<Navigator>();
		this.brain = base.GetComponent<MinionBrain>();
		this.prefabid = base.GetComponent<KPrefabID>();
	}

	// Token: 0x060014D4 RID: 5332 RVA: 0x0006D608 File Offset: 0x0006B808
	public override void Update()
	{
		if (!this.prefabid.HasTag(GameTags.Idle))
		{
			this.cell = Grid.InvalidCell;
			return;
		}
		MinionPathFinderAbilities minionPathFinderAbilities = (MinionPathFinderAbilities)this.navigator.GetCurrentAbilities();
		minionPathFinderAbilities.SetIdleNavMaskEnabled(true);
		IdleCellQuery idleCellQuery = PathFinderQueries.idleCellQuery.Reset(this.brain, UnityEngine.Random.Range(30, 60));
		this.navigator.RunQuery(idleCellQuery);
		minionPathFinderAbilities.SetIdleNavMaskEnabled(false);
		this.cell = idleCellQuery.GetResultCell();
	}

	// Token: 0x060014D5 RID: 5333 RVA: 0x0006D682 File Offset: 0x0006B882
	public int GetCell()
	{
		return this.cell;
	}

	// Token: 0x04000BA9 RID: 2985
	private MinionBrain brain;

	// Token: 0x04000BAA RID: 2986
	private Navigator navigator;

	// Token: 0x04000BAB RID: 2987
	private KPrefabID prefabid;

	// Token: 0x04000BAC RID: 2988
	private int cell;
}
