using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AE8 RID: 2792
public class KScrollbarVisibility : MonoBehaviour
{
	// Token: 0x0600558D RID: 21901 RVA: 0x001EEDBA File Offset: 0x001ECFBA
	private void Start()
	{
		this.Update();
	}

	// Token: 0x0600558E RID: 21902 RVA: 0x001EEDC4 File Offset: 0x001ECFC4
	private void Update()
	{
		if (this.content.content == null)
		{
			return;
		}
		bool flag = false;
		Vector2 vector = new Vector2(this.parent.rect.width, this.parent.rect.height);
		Vector2 sizeDelta = this.content.content.GetComponent<RectTransform>().sizeDelta;
		if ((sizeDelta.x >= vector.x && this.checkWidth) || (sizeDelta.y >= vector.y && this.checkHeight))
		{
			flag = true;
		}
		if (this.scrollbar.gameObject.activeSelf != flag)
		{
			this.scrollbar.gameObject.SetActive(flag);
			if (this.others != null)
			{
				GameObject[] array = this.others;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].SetActive(flag);
				}
			}
		}
	}

	// Token: 0x04003A10 RID: 14864
	[SerializeField]
	private ScrollRect content;

	// Token: 0x04003A11 RID: 14865
	[SerializeField]
	private RectTransform parent;

	// Token: 0x04003A12 RID: 14866
	[SerializeField]
	private bool checkWidth = true;

	// Token: 0x04003A13 RID: 14867
	[SerializeField]
	private bool checkHeight = true;

	// Token: 0x04003A14 RID: 14868
	[SerializeField]
	private Scrollbar scrollbar;

	// Token: 0x04003A15 RID: 14869
	[SerializeField]
	private GameObject[] others;
}
