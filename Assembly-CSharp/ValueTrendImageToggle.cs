using System;
using Klei.AI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C1F RID: 3103
public class ValueTrendImageToggle : MonoBehaviour
{
	// Token: 0x06006240 RID: 25152 RVA: 0x002447C8 File Offset: 0x002429C8
	public void SetValue(AmountInstance ainstance)
	{
		float delta = ainstance.GetDelta();
		Sprite sprite = null;
		if (ainstance.paused || delta == 0f)
		{
			this.targetImage.gameObject.SetActive(false);
		}
		else
		{
			this.targetImage.gameObject.SetActive(true);
			if (delta <= -ainstance.amount.visualDeltaThreshold * 2f)
			{
				sprite = this.Down_Three;
			}
			else if (delta <= -ainstance.amount.visualDeltaThreshold)
			{
				sprite = this.Down_Two;
			}
			else if (delta <= 0f)
			{
				sprite = this.Down_One;
			}
			else if (delta > ainstance.amount.visualDeltaThreshold * 2f)
			{
				sprite = this.Up_Three;
			}
			else if (delta > ainstance.amount.visualDeltaThreshold)
			{
				sprite = this.Up_Two;
			}
			else if (delta > 0f)
			{
				sprite = this.Up_One;
			}
		}
		this.targetImage.sprite = sprite;
	}

	// Token: 0x040043F0 RID: 17392
	public Image targetImage;

	// Token: 0x040043F1 RID: 17393
	public Sprite Up_One;

	// Token: 0x040043F2 RID: 17394
	public Sprite Up_Two;

	// Token: 0x040043F3 RID: 17395
	public Sprite Up_Three;

	// Token: 0x040043F4 RID: 17396
	public Sprite Down_One;

	// Token: 0x040043F5 RID: 17397
	public Sprite Down_Two;

	// Token: 0x040043F6 RID: 17398
	public Sprite Down_Three;

	// Token: 0x040043F7 RID: 17399
	public Sprite Zero;
}
