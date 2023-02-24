using System;
using Database;
using UnityEngine;

// Token: 0x02000536 RID: 1334
public class BalloonFX : GameStateMachine<BalloonFX, BalloonFX.Instance>
{
	// Token: 0x06001FFE RID: 8190 RVA: 0x000AE9C0 File Offset: 0x000ACBC0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.Target(this.fx);
		this.root.Exit("DestroyFX", delegate(BalloonFX.Instance smi)
		{
			smi.DestroyFX();
		});
	}

	// Token: 0x04001243 RID: 4675
	public StateMachine<BalloonFX, BalloonFX.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x04001244 RID: 4676
	public KAnimFile defaultAnim = Assets.GetAnim("balloon_anim_kanim");

	// Token: 0x04001245 RID: 4677
	private KAnimFile defaultBalloon = Assets.GetAnim("balloon_basic_red_kanim");

	// Token: 0x04001246 RID: 4678
	private const string defaultAnimName = "ballon_anim_kanim";

	// Token: 0x04001247 RID: 4679
	private const string balloonAnimName = "balloon_basic_red_kanim";

	// Token: 0x04001248 RID: 4680
	private const string TARGET_SYMBOL_TO_OVERRIDE = "body";

	// Token: 0x04001249 RID: 4681
	private const int TARGET_OVERRIDE_PRIORITY = 0;

	// Token: 0x02001161 RID: 4449
	public new class Instance : GameStateMachine<BalloonFX, BalloonFX.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600765B RID: 30299 RVA: 0x002B79B0 File Offset: 0x002B5BB0
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.balloonAnimController = FXHelpers.CreateEffectOverride(new string[] { "ballon_anim_kanim", "balloon_basic_red_kanim" }, master.gameObject.transform.GetPosition() + new Vector3(0f, 0.3f, 1f), master.transform, true, Grid.SceneLayer.Creatures, false);
			base.sm.fx.Set(this.balloonAnimController.gameObject, base.smi, false);
			this.balloonAnimController.defaultAnim = "idle_default";
			master.GetComponent<KBatchedAnimController>().GetSynchronizer().Add(this.balloonAnimController.GetComponent<KBatchedAnimController>());
		}

		// Token: 0x0600765C RID: 30300 RVA: 0x002B7A68 File Offset: 0x002B5C68
		public void SetBalloonSymbolOverride(BalloonOverrideSymbol balloonOverride)
		{
			KAnimFile kanimFile = (balloonOverride.animFile.IsSome() ? balloonOverride.animFile.Unwrap() : base.smi.sm.defaultBalloon);
			this.balloonAnimController.SwapAnims(new KAnimFile[]
			{
				base.smi.sm.defaultAnim,
				kanimFile
			});
			SymbolOverrideController component = this.balloonAnimController.GetComponent<SymbolOverrideController>();
			if (this.currentBodyOverrideSymbol.IsSome())
			{
				component.RemoveSymbolOverride("body", 0);
			}
			if (balloonOverride.symbol.IsNone())
			{
				if (this.currentBodyOverrideSymbol.IsSome())
				{
					component.AddSymbolOverride("body", base.smi.sm.defaultAnim.GetData().build.GetSymbol("body"), 0);
				}
			}
			else
			{
				component.AddSymbolOverride("body", balloonOverride.symbol.Unwrap(), 0);
			}
			this.currentBodyOverrideSymbol = balloonOverride;
		}

		// Token: 0x0600765D RID: 30301 RVA: 0x002B7B77 File Offset: 0x002B5D77
		public void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
		}

		// Token: 0x04005AAA RID: 23210
		private KBatchedAnimController balloonAnimController;

		// Token: 0x04005AAB RID: 23211
		private Option<BalloonOverrideSymbol> currentBodyOverrideSymbol;
	}
}
