using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006D RID: 109
[ExecuteInEditMode]
[AddComponentMenu("KMonoBehaviour/Plugins/SetTextStyleSetting")]
public class SetTextStyleSetting : KMonoBehaviour
{
	// Token: 0x06000486 RID: 1158 RVA: 0x000167D4 File Offset: 0x000149D4
	public static void ApplyStyle(TextMeshProUGUI sdfText, TextStyleSetting style)
	{
		if (!sdfText)
		{
			return;
		}
		if (!style)
		{
			return;
		}
		sdfText.enableWordWrapping = style.enableWordWrapping;
		sdfText.enableKerning = true;
		sdfText.extraPadding = true;
		sdfText.fontSize = (float)style.fontSize;
		sdfText.color = style.textColor;
		sdfText.font = style.sdfFont;
		sdfText.fontStyle = style.style;
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x0001683E File Offset: 0x00014A3E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00016848 File Offset: 0x00014A48
	public void SetStyle(TextStyleSetting newstyle)
	{
		if (this.sdfText == null)
		{
			this.sdfText = base.GetComponent<TextMeshProUGUI>();
		}
		if (this.currentStyle != newstyle)
		{
			this.currentStyle = newstyle;
			this.style = this.currentStyle;
			SetTextStyleSetting.ApplyStyle(this.sdfText, this.style);
		}
	}

	// Token: 0x040004C9 RID: 1225
	[MyCmpGet]
	private Text text;

	// Token: 0x040004CA RID: 1226
	[MyCmpGet]
	private TextMeshProUGUI sdfText;

	// Token: 0x040004CB RID: 1227
	[SerializeField]
	private TextStyleSetting style;

	// Token: 0x040004CC RID: 1228
	private TextStyleSetting currentStyle;

	// Token: 0x020009C0 RID: 2496
	public enum TextStyle
	{
		// Token: 0x040021C9 RID: 8649
		Standard,
		// Token: 0x040021CA RID: 8650
		Header
	}
}
