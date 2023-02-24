using System;
using UnityEngine;

// Token: 0x02000B51 RID: 2897
public class LegendEntry
{
	// Token: 0x060059FC RID: 23036 RVA: 0x00208D30 File Offset: 0x00206F30
	public LegendEntry(string name, string desc, Color colour, string desc_arg = null, Sprite sprite = null, bool displaySprite = true)
	{
		this.name = name;
		this.desc = desc;
		this.colour = colour;
		this.desc_arg = desc_arg;
		this.sprite = ((sprite == null) ? Assets.instance.LegendColourBox : sprite);
		this.displaySprite = displaySprite;
	}

	// Token: 0x04003CCD RID: 15565
	public string name;

	// Token: 0x04003CCE RID: 15566
	public string desc;

	// Token: 0x04003CCF RID: 15567
	public string desc_arg;

	// Token: 0x04003CD0 RID: 15568
	public Color colour;

	// Token: 0x04003CD1 RID: 15569
	public Sprite sprite;

	// Token: 0x04003CD2 RID: 15570
	public bool displaySprite;
}
