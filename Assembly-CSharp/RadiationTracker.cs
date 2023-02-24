using System;
using System.Collections.Generic;
using Klei.AI;

// Token: 0x020004EE RID: 1262
public class RadiationTracker : WorldTracker
{
	// Token: 0x06001DCA RID: 7626 RVA: 0x0009ED26 File Offset: 0x0009CF26
	public RadiationTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x0009ED30 File Offset: 0x0009CF30
	public override void UpdateData()
	{
		float num = 0f;
		List<MinionIdentity> worldItems = Components.MinionIdentities.GetWorldItems(base.WorldID, false);
		if (worldItems.Count == 0)
		{
			base.AddPoint(0f);
			return;
		}
		foreach (MinionIdentity minionIdentity in worldItems)
		{
			num += minionIdentity.GetAmounts().Get(Db.Get().Amounts.RadiationBalance.Id).value;
		}
		float num2 = num / (float)worldItems.Count;
		base.AddPoint(num2);
	}

	// Token: 0x06001DCC RID: 7628 RVA: 0x0009EDE0 File Offset: 0x0009CFE0
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedRads(value, GameUtil.TimeSlice.None);
	}
}
