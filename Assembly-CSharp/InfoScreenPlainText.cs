using System;
using UnityEngine;

// Token: 0x02000ABE RID: 2750
[AddComponentMenu("KMonoBehaviour/scripts/InfoScreenPlainText")]
public class InfoScreenPlainText : KMonoBehaviour
{
	// Token: 0x0600541A RID: 21530 RVA: 0x001E887D File Offset: 0x001E6A7D
	public void SetText(string text)
	{
		this.locText.text = text;
	}

	// Token: 0x04003928 RID: 14632
	[SerializeField]
	private LocText locText;
}
