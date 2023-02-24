using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class ColorStyleSetting : ScriptableObject
{
	// Token: 0x06000324 RID: 804 RVA: 0x00011033 File Offset: 0x0000F233
	public void Init(Color _color)
	{
		this.activeColor = _color;
		this.inactiveColor = _color;
		this.disabledColor = _color;
		this.disabledActiveColor = _color;
		this.hoverColor = _color;
		this.disabledhoverColor = _color;
	}

	// Token: 0x040003DA RID: 986
	public Color activeColor;

	// Token: 0x040003DB RID: 987
	public Color inactiveColor;

	// Token: 0x040003DC RID: 988
	public Color disabledColor;

	// Token: 0x040003DD RID: 989
	public Color disabledActiveColor;

	// Token: 0x040003DE RID: 990
	public Color hoverColor;

	// Token: 0x040003DF RID: 991
	public Color disabledhoverColor = Color.grey;
}
