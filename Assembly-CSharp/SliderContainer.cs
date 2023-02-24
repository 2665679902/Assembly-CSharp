﻿using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000BFA RID: 3066
[AddComponentMenu("KMonoBehaviour/scripts/SliderContainer")]
public class SliderContainer : KMonoBehaviour
{
	// Token: 0x06006103 RID: 24835 RVA: 0x0023ACAE File Offset: 0x00238EAE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.slider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateSliderLabel));
	}

	// Token: 0x06006104 RID: 24836 RVA: 0x0023ACD4 File Offset: 0x00238ED4
	public void UpdateSliderLabel(float newValue)
	{
		if (this.isPercentValue)
		{
			this.valueLabel.text = (newValue * 100f).ToString("F0") + "%";
			return;
		}
		this.valueLabel.text = newValue.ToString();
	}

	// Token: 0x040042C9 RID: 17097
	public bool isPercentValue = true;

	// Token: 0x040042CA RID: 17098
	public KSlider slider;

	// Token: 0x040042CB RID: 17099
	public LocText nameLabel;

	// Token: 0x040042CC RID: 17100
	public LocText valueLabel;
}
