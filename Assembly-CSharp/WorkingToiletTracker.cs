using System;
using System.Collections.Generic;

// Token: 0x020004F1 RID: 1265
public class WorkingToiletTracker : WorldTracker
{
	// Token: 0x06001DD3 RID: 7635 RVA: 0x0009EEA8 File Offset: 0x0009D0A8
	public WorkingToiletTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DD4 RID: 7636 RVA: 0x0009EEB4 File Offset: 0x0009D0B4
	public override void UpdateData()
	{
		int num = 0;
		List<IUsable> worldItems = Components.Toilets.GetWorldItems(base.WorldID, false);
		for (int i = 0; i < worldItems.Count; i++)
		{
			if (worldItems[i].IsUsable())
			{
				num++;
			}
		}
		base.AddPoint((float)num);
	}

	// Token: 0x06001DD5 RID: 7637 RVA: 0x0009EF00 File Offset: 0x0009D100
	public override string FormatValueString(float value)
	{
		return value.ToString();
	}
}
