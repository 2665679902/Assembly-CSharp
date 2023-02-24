using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E4 RID: 1252
public class WorkTimeTracker : WorldTracker
{
	// Token: 0x06001DAA RID: 7594 RVA: 0x0009E696 File Offset: 0x0009C896
	public WorkTimeTracker(int worldID, ChoreGroup group)
		: base(worldID)
	{
		this.choreGroup = group;
	}

	// Token: 0x06001DAB RID: 7595 RVA: 0x0009E6A8 File Offset: 0x0009C8A8
	public override void UpdateData()
	{
		float num = 0f;
		List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(base.WorldID, false);
		Chore chore;
		Predicate<ChoreType> <>9__0;
		foreach (MinionIdentity minionIdentity in worldItems)
		{
			chore = minionIdentity.GetComponent<ChoreConsumer>().choreDriver.GetCurrentChore();
			if (chore != null)
			{
				List<ChoreType> choreTypes = this.choreGroup.choreTypes;
				Predicate<ChoreType> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (ChoreType match) => match == chore.choreType);
				}
				if (choreTypes.Find(predicate) != null)
				{
					num += 1f;
				}
			}
		}
		base.AddPoint(num / (float)worldItems.Count * 100f);
	}

	// Token: 0x06001DAC RID: 7596 RVA: 0x0009E780 File Offset: 0x0009C980
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedPercent(Mathf.Round(value), GameUtil.TimeSlice.None).ToString();
	}

	// Token: 0x040010B5 RID: 4277
	public ChoreGroup choreGroup;
}
