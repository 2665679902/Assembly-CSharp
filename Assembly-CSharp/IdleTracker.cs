using System;
using System.Collections.Generic;

// Token: 0x020004ED RID: 1261
public class IdleTracker : WorldTracker
{
	// Token: 0x06001DC7 RID: 7623 RVA: 0x0009ECC0 File Offset: 0x0009CEC0
	public IdleTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x0009ECCC File Offset: 0x0009CECC
	public override void UpdateData()
	{
		int num = 0;
		List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(base.WorldID, false);
		for (int i = 0; i < worldItems.Count; i++)
		{
			if (worldItems[i].HasTag(GameTags.Idle))
			{
				num++;
			}
		}
		base.AddPoint((float)num);
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x0009ED1D File Offset: 0x0009CF1D
	public override string FormatValueString(float value)
	{
		return value.ToString();
	}
}
