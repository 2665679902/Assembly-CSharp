using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B6A RID: 2922
[AddComponentMenu("KMonoBehaviour/scripts/ReportScreenHeaderRow")]
public class ReportScreenHeaderRow : KMonoBehaviour
{
	// Token: 0x06005B3B RID: 23355 RVA: 0x00211FFC File Offset: 0x002101FC
	public void SetLine(ReportManager.ReportGroup reportGroup)
	{
		LayoutElement component = this.name.GetComponent<LayoutElement>();
		component.minWidth = (component.preferredWidth = this.nameWidth);
		this.spacer.minWidth = this.groupSpacerWidth;
		this.name.text = reportGroup.stringKey;
	}

	// Token: 0x04003DE5 RID: 15845
	[SerializeField]
	public new LocText name;

	// Token: 0x04003DE6 RID: 15846
	[SerializeField]
	private LayoutElement spacer;

	// Token: 0x04003DE7 RID: 15847
	[SerializeField]
	private Image bgImage;

	// Token: 0x04003DE8 RID: 15848
	public float groupSpacerWidth;

	// Token: 0x04003DE9 RID: 15849
	private float nameWidth = 164f;

	// Token: 0x04003DEA RID: 15850
	[SerializeField]
	private Color oddRowColor;
}
