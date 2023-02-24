using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A31 RID: 2609
public class AlternateSiblingColor : KMonoBehaviour
{
	// Token: 0x06004F55 RID: 20309 RVA: 0x001C4B04 File Offset: 0x001C2D04
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int siblingIndex = base.transform.GetSiblingIndex();
		this.RefreshColor(siblingIndex % 2 == 0);
	}

	// Token: 0x06004F56 RID: 20310 RVA: 0x001C4B2F File Offset: 0x001C2D2F
	private void RefreshColor(bool evenIndex)
	{
		if (this.image == null)
		{
			return;
		}
		this.image.color = (evenIndex ? this.evenColor : this.oddColor);
	}

	// Token: 0x04003545 RID: 13637
	public Color evenColor;

	// Token: 0x04003546 RID: 13638
	public Color oddColor;

	// Token: 0x04003547 RID: 13639
	public Image image;
}
