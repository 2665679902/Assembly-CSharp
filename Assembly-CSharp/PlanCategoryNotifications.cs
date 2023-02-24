using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B58 RID: 2904
public class PlanCategoryNotifications : MonoBehaviour
{
	// Token: 0x06005A59 RID: 23129 RVA: 0x0020BBE3 File Offset: 0x00209DE3
	public void ToggleAttention(bool active)
	{
		if (!this.AttentionImage)
		{
			return;
		}
		this.AttentionImage.gameObject.SetActive(active);
	}

	// Token: 0x04003D14 RID: 15636
	public Image AttentionImage;
}
