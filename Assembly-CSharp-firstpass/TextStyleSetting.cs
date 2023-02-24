using System;
using TMPro;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class TextStyleSetting : ScriptableObject
{
	// Token: 0x0600048D RID: 1165 RVA: 0x00016930 File Offset: 0x00014B30
	public void Init(TMP_FontAsset _sdfFont, int _fontSize, Color _color, bool _enableWordWrapping)
	{
		this.sdfFont = _sdfFont;
		this.fontSize = _fontSize;
		this.textColor = _color;
		this.enableWordWrapping = _enableWordWrapping;
	}

	// Token: 0x040004CF RID: 1231
	public TMP_FontAsset sdfFont;

	// Token: 0x040004D0 RID: 1232
	public int fontSize;

	// Token: 0x040004D1 RID: 1233
	public Color textColor;

	// Token: 0x040004D2 RID: 1234
	public FontStyles style;

	// Token: 0x040004D3 RID: 1235
	public bool enableWordWrapping = true;
}
