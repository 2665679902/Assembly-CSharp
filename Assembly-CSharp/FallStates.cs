using System;
using STRINGS;

// Token: 0x020000BD RID: 189
public class FallStates : GameStateMachine<FallStates, FallStates.Instance, IStateMachineTarget, FallStates.Def>
{
	// Token: 0x06000351 RID: 849 RVA: 0x0001A214 File Offset: 0x00018414
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.FALLING.NAME, CREATURES.STATUSITEMS.FALLING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.loop.PlayAnim((FallStates.Instance smi) => smi.GetSMI<CreatureFallMonitor.Instance>().anim, KAnim.PlayMode.Loop).ToggleGravity().EventTransition(GameHashes.Landed, this.snaptoground, null)
			.Transition(this.pst, (FallStates.Instance smi) => smi.GetSMI<CreatureFallMonitor.Instance>().CanSwimAtCurrentLocation(), UpdateRate.SIM_33ms);
		this.snaptoground.Enter(delegate(FallStates.Instance smi)
		{
			smi.GetSMI<CreatureFallMonitor.Instance>().SnapToGround();
		}).GoTo(this.pst);
		this.pst.Enter(new StateMachine<FallStates, FallStates.Instance, IStateMachineTarget, FallStates.Def>.State.Callback(FallStates.PlayLandAnim)).BehaviourComplete(GameTags.Creatures.Falling, false);
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0001A336 File Offset: 0x00018536
	private static void PlayLandAnim(FallStates.Instance smi)
	{
		smi.GetComponent<KBatchedAnimController>().Queue(smi.def.getLandAnim(smi), KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x0400022A RID: 554
	private GameStateMachine<FallStates, FallStates.Instance, IStateMachineTarget, FallStates.Def>.State loop;

	// Token: 0x0400022B RID: 555
	private GameStateMachine<FallStates, FallStates.Instance, IStateMachineTarget, FallStates.Def>.State snaptoground;

	// Token: 0x0400022C RID: 556
	private GameStateMachine<FallStates, FallStates.Instance, IStateMachineTarget, FallStates.Def>.State pst;

	// Token: 0x02000E54 RID: 3668
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400514B RID: 20811
		public Func<FallStates.Instance, string> getLandAnim = (FallStates.Instance smi) => "idle_loop";
	}

	// Token: 0x02000E55 RID: 3669
	public new class Instance : GameStateMachine<FallStates, FallStates.Instance, IStateMachineTarget, FallStates.Def>.GameInstance
	{
		// Token: 0x06006C05 RID: 27653 RVA: 0x00297465 File Offset: 0x00295665
		public Instance(Chore<FallStates.Instance> chore, FallStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Falling);
		}
	}
}
