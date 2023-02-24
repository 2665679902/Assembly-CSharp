using System;

// Token: 0x020004F8 RID: 1272
public class FullPuftTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x06001DF3 RID: 7667 RVA: 0x0009F96E File Offset: 0x0009DB6E
	public FullPuftTransitionLayer(Navigator navigator)
		: base(navigator)
	{
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x0009F978 File Offset: 0x0009DB78
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		CreatureCalorieMonitor.Instance smi = navigator.GetSMI<CreatureCalorieMonitor.Instance>();
		if (smi != null && smi.stomach.IsReadyToPoop())
		{
			KAnimControllerBase component = navigator.GetComponent<KBatchedAnimController>();
			string text = HashCache.Get().Get(transition.anim.HashValue) + "_full";
			if (component.HasAnimation(text))
			{
				transition.anim = text;
			}
		}
	}
}
