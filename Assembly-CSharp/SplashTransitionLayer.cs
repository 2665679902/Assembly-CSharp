using System;
using UnityEngine;

// Token: 0x020004F7 RID: 1271
public class SplashTransitionLayer : TransitionDriver.OverrideLayer
{
	// Token: 0x06001DEE RID: 7662 RVA: 0x0009F87C File Offset: 0x0009DA7C
	public SplashTransitionLayer(Navigator navigator)
		: base(navigator)
	{
		this.lastSplashTime = Time.time;
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x0009F890 File Offset: 0x0009DA90
	private void RefreshSplashes(Navigator navigator, Navigator.ActiveTransition transition)
	{
		if (navigator == null)
		{
			return;
		}
		if (transition.end == NavType.Tube)
		{
			return;
		}
		Vector3 position = navigator.transform.GetPosition();
		if (this.lastSplashTime + 1f < Time.time && Grid.Element[Grid.PosToCell(position)].IsLiquid)
		{
			this.lastSplashTime = Time.time;
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("splash_step_kanim", position + new Vector3(0f, 0.75f, -0.1f), null, false, Grid.SceneLayer.Front, false);
			kbatchedAnimController.Play("fx1", KAnim.PlayMode.Once, 1f, 0f);
			kbatchedAnimController.destroyOnAnimComplete = true;
		}
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x0009F938 File Offset: 0x0009DB38
	public override void BeginTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.BeginTransition(navigator, transition);
		this.RefreshSplashes(navigator, transition);
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x0009F94A File Offset: 0x0009DB4A
	public override void UpdateTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.UpdateTransition(navigator, transition);
		this.RefreshSplashes(navigator, transition);
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x0009F95C File Offset: 0x0009DB5C
	public override void EndTransition(Navigator navigator, Navigator.ActiveTransition transition)
	{
		base.EndTransition(navigator, transition);
		this.RefreshSplashes(navigator, transition);
	}

	// Token: 0x040010CA RID: 4298
	private float lastSplashTime;

	// Token: 0x040010CB RID: 4299
	private const float SPLASH_INTERVAL = 1f;
}
