using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000A5 RID: 165
public class AttackStates : GameStateMachine<AttackStates, AttackStates.Instance, IStateMachineTarget, AttackStates.Def>
{
	// Token: 0x060002D2 RID: 722 RVA: 0x00016C08 File Offset: 0x00014E08
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.waitBeforeAttack;
		this.root.Enter("SetTarget", delegate(AttackStates.Instance smi)
		{
			this.target.Set(smi.GetSMI<ThreatMonitor.Instance>().MainThreat, smi, false);
			this.cellOffsets = smi.def.cellOffsets;
		});
		this.waitBeforeAttack.ScheduleGoTo((AttackStates.Instance smi) => UnityEngine.Random.Range(0f, 4f), this.approach);
		this.approach.InitializeStates(this.masterTarget, this.target, this.attack, null, this.cellOffsets, null).ToggleStatusItem(CREATURES.STATUSITEMS.ATTACK_APPROACH.NAME, CREATURES.STATUSITEMS.ATTACK_APPROACH.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.attack.DefaultState(this.attack.pre).ToggleStatusItem(CREATURES.STATUSITEMS.ATTACK.NAME, CREATURES.STATUSITEMS.ATTACK.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.attack.pre.PlayAnim((AttackStates.Instance smi) => smi.def.preAnim, KAnim.PlayMode.Once).Exit(delegate(AttackStates.Instance smi)
		{
			smi.GetComponent<Weapon>().AttackTarget(this.target.Get(smi));
		}).OnAnimQueueComplete(this.attack.pst);
		this.attack.pst.PlayAnim((AttackStates.Instance smi) => smi.def.pstAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.Attack, false);
	}

	// Token: 0x040001DE RID: 478
	public StateMachine<AttackStates, AttackStates.Instance, IStateMachineTarget, AttackStates.Def>.TargetParameter target;

	// Token: 0x040001DF RID: 479
	public GameStateMachine<AttackStates, AttackStates.Instance, IStateMachineTarget, AttackStates.Def>.ApproachSubState<AttackableBase> approach;

	// Token: 0x040001E0 RID: 480
	public CellOffset[] cellOffsets;

	// Token: 0x040001E1 RID: 481
	public GameStateMachine<AttackStates, AttackStates.Instance, IStateMachineTarget, AttackStates.Def>.State waitBeforeAttack;

	// Token: 0x040001E2 RID: 482
	public AttackStates.AttackingStates attack;

	// Token: 0x040001E3 RID: 483
	public GameStateMachine<AttackStates, AttackStates.Instance, IStateMachineTarget, AttackStates.Def>.State behaviourcomplete;

	// Token: 0x02000DFE RID: 3582
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06006B55 RID: 27477 RVA: 0x00296464 File Offset: 0x00294664
		public Def(string pre_anim = "eat_pre", string pst_anim = "eat_pst", CellOffset[] cell_offsets = null)
		{
			this.preAnim = pre_anim;
			this.pstAnim = pst_anim;
			if (cell_offsets != null)
			{
				this.cellOffsets = cell_offsets;
			}
		}

		// Token: 0x040050A2 RID: 20642
		public string preAnim;

		// Token: 0x040050A3 RID: 20643
		public string pstAnim;

		// Token: 0x040050A4 RID: 20644
		public CellOffset[] cellOffsets = new CellOffset[]
		{
			new CellOffset(0, 0),
			new CellOffset(1, 0),
			new CellOffset(-1, 0),
			new CellOffset(1, 1),
			new CellOffset(-1, 1)
		};
	}

	// Token: 0x02000DFF RID: 3583
	public class AttackingStates : GameStateMachine<AttackStates, AttackStates.Instance, IStateMachineTarget, AttackStates.Def>.State
	{
		// Token: 0x040050A5 RID: 20645
		public GameStateMachine<AttackStates, AttackStates.Instance, IStateMachineTarget, AttackStates.Def>.State pre;

		// Token: 0x040050A6 RID: 20646
		public GameStateMachine<AttackStates, AttackStates.Instance, IStateMachineTarget, AttackStates.Def>.State pst;
	}

	// Token: 0x02000E00 RID: 3584
	public new class Instance : GameStateMachine<AttackStates, AttackStates.Instance, IStateMachineTarget, AttackStates.Def>.GameInstance
	{
		// Token: 0x06006B57 RID: 27479 RVA: 0x002964E9 File Offset: 0x002946E9
		public Instance(Chore<AttackStates.Instance> chore, AttackStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Attack);
		}
	}
}
