using System;
using STRINGS;

// Token: 0x020000B6 RID: 182
public class DefendStates : GameStateMachine<DefendStates, DefendStates.Instance, IStateMachineTarget, DefendStates.Def>
{
	// Token: 0x06000339 RID: 825 RVA: 0x00019958 File Offset: 0x00017B58
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.protectEntity.moveToThreat;
		this.root.Enter("SetTarget", delegate(DefendStates.Instance smi)
		{
			this.target.Set(smi.GetSMI<EggProtectionMonitor.Instance>().MainThreat, smi, false);
		}).ToggleStatusItem(CREATURES.STATUSITEMS.ATTACKINGENTITY.NAME, CREATURES.STATUSITEMS.ATTACKINGENTITY.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.protectEntity.moveToThreat.InitializeStates(this.masterTarget, this.target, this.protectEntity.attackThreat, null, new CellOffset[]
		{
			new CellOffset(0, 0),
			new CellOffset(1, 0),
			new CellOffset(-1, 0),
			new CellOffset(1, 1),
			new CellOffset(-1, 1)
		}, null);
		this.protectEntity.attackThreat.Enter(delegate(DefendStates.Instance smi)
		{
			smi.Play("slap_pre", KAnim.PlayMode.Once);
			smi.Queue("slap", KAnim.PlayMode.Once);
			smi.Queue("slap_pst", KAnim.PlayMode.Once);
			smi.Schedule(0.5f, delegate
			{
				smi.GetComponent<Weapon>().AttackTarget(this.target.Get(smi));
			}, null);
		}).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Defend, false);
	}

	// Token: 0x04000217 RID: 535
	public StateMachine<DefendStates, DefendStates.Instance, IStateMachineTarget, DefendStates.Def>.TargetParameter target;

	// Token: 0x04000218 RID: 536
	public DefendStates.ProtectStates protectEntity;

	// Token: 0x04000219 RID: 537
	public GameStateMachine<DefendStates, DefendStates.Instance, IStateMachineTarget, DefendStates.Def>.State behaviourcomplete;

	// Token: 0x02000E3E RID: 3646
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E3F RID: 3647
	public new class Instance : GameStateMachine<DefendStates, DefendStates.Instance, IStateMachineTarget, DefendStates.Def>.GameInstance
	{
		// Token: 0x06006BE3 RID: 27619 RVA: 0x002971EA File Offset: 0x002953EA
		public Instance(Chore<DefendStates.Instance> chore, DefendStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Defend);
		}
	}

	// Token: 0x02000E40 RID: 3648
	public class ProtectStates : GameStateMachine<DefendStates, DefendStates.Instance, IStateMachineTarget, DefendStates.Def>.State
	{
		// Token: 0x04005138 RID: 20792
		public GameStateMachine<DefendStates, DefendStates.Instance, IStateMachineTarget, DefendStates.Def>.ApproachSubState<AttackableBase> moveToThreat;

		// Token: 0x04005139 RID: 20793
		public GameStateMachine<DefendStates, DefendStates.Instance, IStateMachineTarget, DefendStates.Def>.State attackThreat;
	}
}
