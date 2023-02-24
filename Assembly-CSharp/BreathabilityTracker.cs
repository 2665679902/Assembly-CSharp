using System;
using UnityEngine;

// Token: 0x020004E6 RID: 1254
public class BreathabilityTracker : WorldTracker
{
	// Token: 0x06001DB0 RID: 7600 RVA: 0x0009E847 File Offset: 0x0009CA47
	public BreathabilityTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DB1 RID: 7601 RVA: 0x0009E850 File Offset: 0x0009CA50
	public override void UpdateData()
	{
		float num = 0f;
		int count = Components.LiveMinionIdentities.GetWorldItems(base.WorldID, false).Count;
		if (count == 0)
		{
			base.AddPoint(0f);
			return;
		}
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.GetWorldItems(base.WorldID, false))
		{
			OxygenBreather component = minionIdentity.GetComponent<OxygenBreather>();
			if (component.GetGasProvider() is GasBreatherFromWorldProvider)
			{
				if (component.IsBreathableElement)
				{
					num += 100f;
					if (component.IsLowOxygen())
					{
						num -= 50f;
					}
				}
			}
			else if (!component.IsSuffocating)
			{
				num += 100f;
				if (component.IsLowOxygen())
				{
					num -= 50f;
				}
			}
		}
		num /= (float)count;
		base.AddPoint((float)Mathf.RoundToInt(num));
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x0009E93C File Offset: 0x0009CB3C
	public override string FormatValueString(float value)
	{
		return value.ToString() + "%";
	}
}
