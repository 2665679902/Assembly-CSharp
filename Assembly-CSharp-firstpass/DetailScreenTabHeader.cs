using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004F RID: 79
public class DetailScreenTabHeader : KTabMenuHeader
{
	// Token: 0x06000326 RID: 806 RVA: 0x00011074 File Offset: 0x0000F274
	public override void ActivateTabArtwork(int tabIdx)
	{
		base.ActivateTabArtwork(tabIdx);
		if (tabIdx >= base.transform.childCount)
		{
			return;
		}
		for (int i = 0; i < base.transform.childCount; i++)
		{
			LayoutElement component = base.transform.GetChild(i).GetComponent<LayoutElement>();
			if (component != null)
			{
				if (i == tabIdx)
				{
					component.preferredHeight = this.SelectedHeight;
					component.transform.Find("Icon").GetComponent<Image>().color = new Color(0.14509805f, 0.16470589f, 0.23137255f);
				}
				else
				{
					component.preferredHeight = this.UnselectedHeight;
					component.transform.Find("Icon").GetComponent<Image>().color = new Color(0.35686275f, 0.37254903f, 0.4509804f);
				}
			}
		}
	}

	// Token: 0x040003E0 RID: 992
	public float SelectedHeight = 36f;

	// Token: 0x040003E1 RID: 993
	public float UnselectedHeight = 30f;
}
