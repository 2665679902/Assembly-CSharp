using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020004EB RID: 1259
public class StressTracker : WorldTracker
{
	// Token: 0x06001DC1 RID: 7617 RVA: 0x0009EBE6 File Offset: 0x0009CDE6
	public StressTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x0009EBF0 File Offset: 0x0009CDF0
	public override void UpdateData()
	{
		float num = 0f;
		for (int i = 0; i < Components.LiveMinionIdentities.Count; i++)
		{
			if (Components.LiveMinionIdentities[i].GetMyWorldId() == base.WorldID)
			{
				num = Mathf.Max(num, Components.LiveMinionIdentities[i].gameObject.GetAmounts().GetValue(Db.Get().Amounts.Stress.Id));
			}
		}
		base.AddPoint(Mathf.Round(num));
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x0009EC71 File Offset: 0x0009CE71
	public override string FormatValueString(float value)
	{
		return value.ToString() + "%";
	}
}
