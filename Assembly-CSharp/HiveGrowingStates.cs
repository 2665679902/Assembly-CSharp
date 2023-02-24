using System;
using STRINGS;

// Token: 0x020000C4 RID: 196
public class HiveGrowingStates : GameStateMachine<HiveGrowingStates, HiveGrowingStates.Instance, IStateMachineTarget, HiveGrowingStates.Def>
{
	// Token: 0x0600036B RID: 875 RVA: 0x0001ADE8 File Offset: 0x00018FE8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.growing;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.GROWINGUP.NAME, CREATURES.STATUSITEMS.GROWINGUP.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.growing.DefaultState(this.growing.loop);
		this.growing.loop.PlayAnim((HiveGrowingStates.Instance smi) => "grow", KAnim.PlayMode.Paused).Enter(delegate(HiveGrowingStates.Instance smi)
		{
			smi.RefreshPositionPercent();
		}).Update(delegate(HiveGrowingStates.Instance smi, float dt)
		{
			smi.RefreshPositionPercent();
			if (smi.hive.IsFullyGrown())
			{
				smi.GoTo(this.growing.pst);
			}
		}, UpdateRate.SIM_4000ms, false);
		this.growing.pst.PlayAnim("grow_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Behaviours.GrowUpBehaviour, false);
	}

	// Token: 0x0400023C RID: 572
	public HiveGrowingStates.GrowUpStates growing;

	// Token: 0x0400023D RID: 573
	public GameStateMachine<HiveGrowingStates, HiveGrowingStates.Instance, IStateMachineTarget, HiveGrowingStates.Def>.State behaviourcomplete;

	// Token: 0x02000E68 RID: 3688
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E69 RID: 3689
	public new class Instance : GameStateMachine<HiveGrowingStates, HiveGrowingStates.Instance, IStateMachineTarget, HiveGrowingStates.Def>.GameInstance
	{
		// Token: 0x06006C30 RID: 27696 RVA: 0x00297989 File Offset: 0x00295B89
		public Instance(Chore<HiveGrowingStates.Instance> chore, HiveGrowingStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Behaviours.GrowUpBehaviour);
		}

		// Token: 0x06006C31 RID: 27697 RVA: 0x002979AD File Offset: 0x00295BAD
		public void RefreshPositionPercent()
		{
			this.animController.SetPositionPercent(this.hive.sm.hiveGrowth.Get(this.hive));
		}

		// Token: 0x0400516B RID: 20843
		[MySmiReq]
		public BeeHive.StatesInstance hive;

		// Token: 0x0400516C RID: 20844
		[MyCmpReq]
		private KAnimControllerBase animController;
	}

	// Token: 0x02000E6A RID: 3690
	public class GrowUpStates : GameStateMachine<HiveGrowingStates, HiveGrowingStates.Instance, IStateMachineTarget, HiveGrowingStates.Def>.State
	{
		// Token: 0x0400516D RID: 20845
		public GameStateMachine<HiveGrowingStates, HiveGrowingStates.Instance, IStateMachineTarget, HiveGrowingStates.Def>.State loop;

		// Token: 0x0400516E RID: 20846
		public GameStateMachine<HiveGrowingStates, HiveGrowingStates.Instance, IStateMachineTarget, HiveGrowingStates.Def>.State pst;
	}
}
