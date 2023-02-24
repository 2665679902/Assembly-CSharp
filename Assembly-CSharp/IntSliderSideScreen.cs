using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BBD RID: 3005
public class IntSliderSideScreen : SideScreenContent
{
	// Token: 0x06005E6B RID: 24171 RVA: 0x00227EB0 File Offset: 0x002260B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		for (int i = 0; i < this.sliderSets.Count; i++)
		{
			this.sliderSets[i].SetupSlider(i);
			this.sliderSets[i].valueSlider.wholeNumbers = true;
		}
	}

	// Token: 0x06005E6C RID: 24172 RVA: 0x00227F02 File Offset: 0x00226102
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IIntSliderControl>() != null || target.GetSMI<IIntSliderControl>() != null;
	}

	// Token: 0x06005E6D RID: 24173 RVA: 0x00227F18 File Offset: 0x00226118
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IIntSliderControl>();
		if (this.target == null)
		{
			this.target = new_target.GetSMI<IIntSliderControl>();
		}
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

	// Token: 0x04004096 RID: 16534
	private IIntSliderControl target;

	// Token: 0x04004097 RID: 16535
	public List<SliderSet> sliderSets;
}
