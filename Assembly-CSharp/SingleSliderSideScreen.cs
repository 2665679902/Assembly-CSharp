using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000BE5 RID: 3045
public class SingleSliderSideScreen : SideScreenContent
{
	// Token: 0x06005FEB RID: 24555 RVA: 0x00231898 File Offset: 0x0022FA98
	protected override void OnSpawn()
	{
		base.OnSpawn();
		for (int i = 0; i < this.sliderSets.Count; i++)
		{
			this.sliderSets[i].SetupSlider(i);
		}
	}

	// Token: 0x06005FEC RID: 24556 RVA: 0x002318D4 File Offset: 0x0022FAD4
	public override bool IsValidForTarget(GameObject target)
	{
		KPrefabID component = target.GetComponent<KPrefabID>();
		return (target.GetComponent<ISingleSliderControl>() != null || target.GetSMI<ISingleSliderControl>() != null) && !component.IsPrefabID("HydrogenGenerator".ToTag()) && !component.IsPrefabID("MethaneGenerator".ToTag()) && !component.IsPrefabID("PetroleumGenerator".ToTag()) && !component.IsPrefabID("DevGenerator".ToTag()) && !component.HasTag(GameTags.DeadReactor);
	}

	// Token: 0x06005FED RID: 24557 RVA: 0x00231950 File Offset: 0x0022FB50
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<ISingleSliderControl>();
		if (this.target == null)
		{
			this.target = new_target.GetSMI<ISingleSliderControl>();
			if (this.target == null)
			{
				global::Debug.LogError("The gameObject received does not contain a ISingleSliderControl implementation");
				return;
			}
		}
		this.titleKey = this.target.SliderTitleKey;
		for (int i = 0; i < this.sliderSets.Count; i++)
		{
			this.sliderSets[i].SetTarget(this.target);
		}
	}

	// Token: 0x040041AC RID: 16812
	private ISingleSliderControl target;

	// Token: 0x040041AD RID: 16813
	public List<SliderSet> sliderSets;
}
