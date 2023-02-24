using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000A03 RID: 2563
public class HoverTextHelper
{
	// Token: 0x06004CF1 RID: 19697 RVA: 0x001B116D File Offset: 0x001AF36D
	public static void DestroyStatics()
	{
		HoverTextHelper.cachedElement = null;
		HoverTextHelper.cachedMass = -1f;
	}

	// Token: 0x06004CF2 RID: 19698 RVA: 0x001B1180 File Offset: 0x001AF380
	public static string[] MassStringsReadOnly(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return HoverTextHelper.invalidCellMassStrings;
		}
		Element element = Grid.Element[cell];
		float num = Grid.Mass[cell];
		if (element == HoverTextHelper.cachedElement && num == HoverTextHelper.cachedMass)
		{
			return HoverTextHelper.massStrings;
		}
		HoverTextHelper.cachedElement = element;
		HoverTextHelper.cachedMass = num;
		HoverTextHelper.massStrings[3] = " " + GameUtil.GetBreathableString(element, num);
		if (element.id == SimHashes.Vacuum)
		{
			HoverTextHelper.massStrings[0] = UI.NA;
			HoverTextHelper.massStrings[1] = "";
			HoverTextHelper.massStrings[2] = "";
		}
		else if (element.id == SimHashes.Unobtanium)
		{
			HoverTextHelper.massStrings[0] = UI.NEUTRONIUMMASS;
			HoverTextHelper.massStrings[1] = "";
			HoverTextHelper.massStrings[2] = "";
		}
		else
		{
			HoverTextHelper.massStrings[2] = UI.UNITSUFFIXES.MASS.KILOGRAM;
			if (num < 5f)
			{
				num *= 1000f;
				HoverTextHelper.massStrings[2] = UI.UNITSUFFIXES.MASS.GRAM;
			}
			if (num < 5f)
			{
				num *= 1000f;
				HoverTextHelper.massStrings[2] = UI.UNITSUFFIXES.MASS.MILLIGRAM;
			}
			if (num < 5f)
			{
				num *= 1000f;
				HoverTextHelper.massStrings[2] = UI.UNITSUFFIXES.MASS.MICROGRAM;
				num = Mathf.Floor(num);
			}
			int num2 = Mathf.FloorToInt(num);
			int num3 = Mathf.RoundToInt(10f * (num - (float)num2));
			if (num3 == 10)
			{
				num2++;
				num3 = 0;
			}
			HoverTextHelper.massStrings[0] = num2.ToString();
			HoverTextHelper.massStrings[1] = "." + num3.ToString();
		}
		return HoverTextHelper.massStrings;
	}

	// Token: 0x040032B8 RID: 12984
	private static readonly string[] massStrings = new string[4];

	// Token: 0x040032B9 RID: 12985
	private static readonly string[] invalidCellMassStrings = new string[] { "", "", "", "" };

	// Token: 0x040032BA RID: 12986
	private static float cachedMass = -1f;

	// Token: 0x040032BB RID: 12987
	private static Element cachedElement;
}
