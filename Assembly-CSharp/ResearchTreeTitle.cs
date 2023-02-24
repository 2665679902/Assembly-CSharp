using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000007 RID: 7
public class ResearchTreeTitle : MonoBehaviour
{
	// Token: 0x0600001E RID: 30 RVA: 0x00002752 File Offset: 0x00000952
	public void SetLabel(string txt)
	{
		this.treeLabel.text = txt;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002760 File Offset: 0x00000960
	public void SetColor(int id)
	{
		this.BG.enabled = id % 2 != 0;
	}

	// Token: 0x0400000D RID: 13
	[Header("References")]
	[SerializeField]
	private LocText treeLabel;

	// Token: 0x0400000E RID: 14
	[SerializeField]
	private Image BG;
}
