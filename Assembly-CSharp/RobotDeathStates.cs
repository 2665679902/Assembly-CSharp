using System;
using STRINGS;

// Token: 0x020000D6 RID: 214
public class RobotDeathStates : GameStateMachine<RobotDeathStates, RobotDeathStates.Instance, IStateMachineTarget, RobotDeathStates.Def>
{
	// Token: 0x060003BE RID: 958 RVA: 0x0001CE28 File Offset: 0x0001B028
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.loop;
		this.loop.ToggleStatusItem(CREATURES.STATUSITEMS.DEAD.NAME, CREATURES.STATUSITEMS.DEAD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).PlayAnim("death").OnAnimQueueComplete(this.pst);
		this.pst.TriggerOnEnter(GameHashes.DeathAnimComplete, null).TriggerOnEnter(GameHashes.Died, null).BehaviourComplete(GameTags.Creatures.Die, false);
	}

	// Token: 0x04000265 RID: 613
	private GameStateMachine<RobotDeathStates, RobotDeathStates.Instance, IStateMachineTarget, RobotDeathStates.Def>.State loop;

	// Token: 0x04000266 RID: 614
	private GameStateMachine<RobotDeathStates, RobotDeathStates.Instance, IStateMachineTarget, RobotDeathStates.Def>.State pst;

	// Token: 0x02000EA1 RID: 3745
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000EA2 RID: 3746
	public new class Instance : GameStateMachine<RobotDeathStates, RobotDeathStates.Instance, IStateMachineTarget, RobotDeathStates.Def>.GameInstance
	{
		// Token: 0x06006CC3 RID: 27843 RVA: 0x00298ACA File Offset: 0x00296CCA
		public Instance(Chore<RobotDeathStates.Instance> chore, RobotDeathStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Die);
		}
	}
}
