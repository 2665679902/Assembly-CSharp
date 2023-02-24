using System;
using UnityEngine;

// Token: 0x0200053E RID: 1342
public class UpgradeFX : GameStateMachine<UpgradeFX, UpgradeFX.Instance>
{
	// Token: 0x06002013 RID: 8211 RVA: 0x000AF674 File Offset: 0x000AD874
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.Target(this.fx);
		this.root.PlayAnim("upgrade").OnAnimQueueComplete(null).ToggleReactable((UpgradeFX.Instance smi) => smi.CreateReactable())
			.Exit("DestroyFX", delegate(UpgradeFX.Instance smi)
			{
				smi.DestroyFX();
			});
	}

	// Token: 0x04001260 RID: 4704
	public StateMachine<UpgradeFX, UpgradeFX.Instance, IStateMachineTarget, object>.TargetParameter fx;

	// Token: 0x02001171 RID: 4465
	public new class Instance : GameStateMachine<UpgradeFX, UpgradeFX.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007697 RID: 30359 RVA: 0x002B840C File Offset: 0x002B660C
		public Instance(IStateMachineTarget master, Vector3 offset)
			: base(master)
		{
			KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("upgrade_fx_kanim", master.gameObject.transform.GetPosition() + offset, master.gameObject.transform, true, Grid.SceneLayer.Front, false);
			base.sm.fx.Set(kbatchedAnimController.gameObject, base.smi, false);
		}

		// Token: 0x06007698 RID: 30360 RVA: 0x002B846E File Offset: 0x002B666E
		public void DestroyFX()
		{
			Util.KDestroyGameObject(base.sm.fx.Get(base.smi));
		}

		// Token: 0x06007699 RID: 30361 RVA: 0x002B848C File Offset: 0x002B668C
		public Reactable CreateReactable()
		{
			return new EmoteReactable(base.master.gameObject, "UpgradeFX", Db.Get().ChoreTypes.Emote, 15, 8, 0f, 20f, float.PositiveInfinity, 0f).SetEmote(Db.Get().Emotes.Minion.Cheer);
		}
	}
}
