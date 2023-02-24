using System;

// Token: 0x02000119 RID: 281
public static class UnitsUtil
{
	// Token: 0x06000959 RID: 2393 RVA: 0x000252A2 File Offset: 0x000234A2
	public static bool IsTimeUnit(Units unit)
	{
		return unit - Units.PerDay <= 1;
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x000252AD File Offset: 0x000234AD
	public static string GetUnitSuffix(Units unit)
	{
		if (unit == Units.Kelvin)
		{
			return "K";
		}
		return "";
	}
}
