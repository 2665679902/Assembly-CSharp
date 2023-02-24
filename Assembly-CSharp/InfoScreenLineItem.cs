using System;
using UnityEngine;

// Token: 0x02000ABD RID: 2749
[AddComponentMenu("KMonoBehaviour/scripts/InfoScreenLineItem")]
public class InfoScreenLineItem : KMonoBehaviour
{
	// Token: 0x06005417 RID: 21527 RVA: 0x001E8859 File Offset: 0x001E6A59
	public void SetText(string text)
	{
		this.locText.text = text;
	}

	// Token: 0x06005418 RID: 21528 RVA: 0x001E8867 File Offset: 0x001E6A67
	public void SetTooltip(string tooltip)
	{
		this.toolTip.toolTip = tooltip;
	}

	// Token: 0x04003924 RID: 14628
	[SerializeField]
	private LocText locText;

	// Token: 0x04003925 RID: 14629
	[SerializeField]
	private ToolTip toolTip;

	// Token: 0x04003926 RID: 14630
	private string text;

	// Token: 0x04003927 RID: 14631
	private string tooltip;
}
