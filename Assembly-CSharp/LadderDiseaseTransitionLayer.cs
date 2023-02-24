﻿using System;
using Klei;
using UnityEngine;

// Token: 0x020004FB RID: 1275
public class LadderDiseaseTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x06001E00 RID: 7680 RVA: 0x0009FDD7 File Offset: 0x0009DFD7
	public LadderDiseaseTransitionLayer(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x0009FDE0 File Offset: 0x0009DFE0
	public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.EndTransition(navigator, transition);
		if (transition.end == NavType.Ladder)
		{
			int num = Grid.PosToCell(navigator);
			GameObject gameObject = Grid.Objects[num, 1];
			if (gameObject != null)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component != null)
				{
					PrimaryElement component2 = navigator.GetComponent<PrimaryElement>();
					if (component2 != null)
					{
						SimUtil.DiseaseInfo invalid = SimUtil.DiseaseInfo.Invalid;
						invalid.idx = component2.DiseaseIdx;
						invalid.count = (int)((float)component2.DiseaseCount * 0.005f);
						SimUtil.DiseaseInfo invalid2 = SimUtil.DiseaseInfo.Invalid;
						invalid2.idx = component.DiseaseIdx;
						invalid2.count = (int)((float)component.DiseaseCount * 0.005f);
						component2.ModifyDiseaseCount(-invalid.count, "Navigator.EndTransition");
						component.ModifyDiseaseCount(-invalid2.count, "Navigator.EndTransition");
						if (invalid.count > 0)
						{
							component.AddDisease(invalid.idx, invalid.count, "TransitionDriver.EndTransition");
						}
						if (invalid2.count > 0)
						{
							component2.AddDisease(invalid2.idx, invalid2.count, "TransitionDriver.EndTransition");
						}
					}
				}
			}
		}
	}
}
