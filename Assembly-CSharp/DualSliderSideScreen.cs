using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BB0 RID: 2992
public class DualSliderSideScreen : SideScreenContent
{
	// Token: 0x06005E19 RID: 24089 RVA: 0x00225F5C File Offset: 0x0022415C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		for (int i = 0; i < this.sliderSets.Count; i++)
		{
			this.sliderSets[i].SetupSlider(i);
		}
	}

	// Token: 0x06005E1A RID: 24090 RVA: 0x00225F97 File Offset: 0x00224197
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IDualSliderControl>() != null;
	}

	// Token: 0x06005E1B RID: 24091 RVA: 0x00225FA4 File Offset: 0x002241A4
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IDualSliderControl>();
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received does not contain a Manual Generator component");
			return;
		}
		this.titleKey = this.target.SliderTitleKey;
		for (int i = 0; i < this.sliderSets.Count; i++)
		{
			this.sliderSets[i].SetTarget(this.target);
		}
	}

	// Token: 0x04004055 RID: 16469
	private IDualSliderControl target;

	// Token: 0x04004056 RID: 16470
	public List<SliderSet> sliderSets;
}
