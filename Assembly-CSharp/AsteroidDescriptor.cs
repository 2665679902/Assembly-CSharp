using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A47 RID: 2631
public struct AsteroidDescriptor
{
	// Token: 0x06004FC2 RID: 20418 RVA: 0x001C640F File Offset: 0x001C460F
	public AsteroidDescriptor(string text, string tooltip, Color associatedColor, List<global::Tuple<string, Color, float>> bands = null, string associatedIcon = null)
	{
		this.text = text;
		this.tooltip = tooltip;
		this.associatedColor = associatedColor;
		this.bands = bands;
		this.associatedIcon = associatedIcon;
	}

	// Token: 0x0400356E RID: 13678
	public string text;

	// Token: 0x0400356F RID: 13679
	public string tooltip;

	// Token: 0x04003570 RID: 13680
	public List<global::Tuple<string, Color, float>> bands;

	// Token: 0x04003571 RID: 13681
	public Color associatedColor;

	// Token: 0x04003572 RID: 13682
	public string associatedIcon;
}
