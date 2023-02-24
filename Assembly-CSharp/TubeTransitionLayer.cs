using System;
using UnityEngine;

// Token: 0x020004FA RID: 1274
public class TubeTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x06001DFC RID: 7676 RVA: 0x0009FC95 File Offset: 0x0009DE95
	public TubeTransitionLayer(Navigator navigator)
		: base(navigator)
	{
		this.tube_traveller = navigator.GetSMI<TubeTraveller.Instance>();
		if (this.tube_traveller != null && navigator.CurrentNavType == NavType.Tube && !this.tube_traveller.inTube)
		{
			this.tube_traveller.OnTubeTransition(true);
		}
	}

	// Token: 0x06001DFD RID: 7677 RVA: 0x0009FCD4 File Offset: 0x0009DED4
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		this.tube_traveller.OnPathAdvanced(null);
		if (transition.start != NavType.Tube && transition.end == NavType.Tube)
		{
			int num = Grid.PosToCell(navigator);
			this.entrance = this.GetEntrance(num);
			return;
		}
		this.entrance = null;
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x0009FD24 File Offset: 0x0009DF24
	public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.EndTransition(navigator, transition);
		if (transition.start != NavType.Tube && transition.end == NavType.Tube && this.entrance)
		{
			this.entrance.ConsumeCharge(navigator.gameObject);
			this.entrance = null;
		}
		this.tube_traveller.OnTubeTransition(transition.end == NavType.Tube);
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x0009FD84 File Offset: 0x0009DF84
	private TravelTubeEntrance GetEntrance(int cell)
	{
		if (!Grid.HasUsableTubeEntrance(cell, this.tube_traveller.prefabInstanceID))
		{
			return null;
		}
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject != null)
		{
			TravelTubeEntrance component = gameObject.GetComponent<TravelTubeEntrance>();
			if (component != null && component.isSpawned)
			{
				return component;
			}
		}
		return null;
	}

	// Token: 0x040010CD RID: 4301
	private TubeTraveller.Instance tube_traveller;

	// Token: 0x040010CE RID: 4302
	private TravelTubeEntrance entrance;
}
