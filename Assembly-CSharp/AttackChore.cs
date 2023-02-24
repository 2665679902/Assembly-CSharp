using System;
using UnityEngine;

// Token: 0x02000374 RID: 884
public class AttackChore : Chore<AttackChore.StatesInstance>
{
	// Token: 0x0600121A RID: 4634 RVA: 0x0005FC85 File Offset: 0x0005DE85
	protected override void OnStateMachineStop(string reason, StateMachine.Status status)
	{
		this.CleanUpMultitool();
		base.OnStateMachineStop(reason, status);
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x0005FC98 File Offset: 0x0005DE98
	public AttackChore(IStateMachineTarget target, GameObject enemy)
		: base(Db.Get().ChoreTypes.Attack, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new AttackChore.StatesInstance(this);
		base.smi.sm.attackTarget.Set(enemy, base.smi, false);
		Game.Instance.Trigger(1980521255, enemy);
		base.SetPrioritizable(enemy.GetComponent<Prioritizable>());
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x0005FD14 File Offset: 0x0005DF14
	public string GetHitAnim()
	{
		Workable component = base.smi.sm.attackTarget.Get(base.smi).gameObject.GetComponent<Workable>();
		if (component)
		{
			return MultitoolController.GetAnimationStrings(component, this.gameObject.GetComponent<Worker>(), "hit")[1].Replace("_loop", "");
		}
		return "hit";
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x0005FD7C File Offset: 0x0005DF7C
	public void OnTargetMoved(object data)
	{
		int num = Grid.PosToCell(base.smi.master.gameObject);
		if (base.smi.sm.attackTarget.Get(base.smi) == null)
		{
			this.CleanUpMultitool();
			return;
		}
		if (base.smi.GetCurrentState() == base.smi.sm.attack)
		{
			int num2 = Grid.PosToCell(base.smi.sm.attackTarget.Get(base.smi).gameObject);
			IApproachable component = base.smi.sm.attackTarget.Get(base.smi).gameObject.GetComponent<IApproachable>();
			if (component != null)
			{
				CellOffset[] offsets = component.GetOffsets();
				if (num == num2 || !Grid.IsCellOffsetOf(num, num2, offsets))
				{
					if (this.multiTool != null)
					{
						this.CleanUpMultitool();
					}
					base.smi.GoTo(base.smi.sm.approachtarget);
				}
			}
			else
			{
				global::Debug.Log("has no approachable");
			}
		}
		if (this.multiTool != null)
		{
			this.multiTool.UpdateHitEffectTarget();
		}
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x0005FE97 File Offset: 0x0005E097
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.attacker.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x0005FEC8 File Offset: 0x0005E0C8
	protected override void End(string reason)
	{
		this.CleanUpMultitool();
		if (!base.smi.sm.attackTarget.IsNull(base.smi))
		{
			GameObject gameObject = base.smi.sm.attackTarget.Get(base.smi);
			Prioritizable component = gameObject.GetComponent<Prioritizable>();
			if (component != null && component.IsPrioritizable())
			{
				Prioritizable.RemoveRef(gameObject);
			}
		}
		base.End(reason);
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x0005FF39 File Offset: 0x0005E139
	public void OnTargetDestroyed(object data)
	{
		this.Fail("target destroyed");
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x0005FF46 File Offset: 0x0005E146
	private void CleanUpMultitool()
	{
		if (base.smi.master.multiTool != null)
		{
			this.multiTool.DestroyHitEffect();
			this.multiTool.StopSM("attack complete");
			this.multiTool = null;
		}
	}

	// Token: 0x040009B0 RID: 2480
	private MultitoolController.Instance multiTool;

	// Token: 0x02000F4A RID: 3914
	public class StatesInstance : GameStateMachine<AttackChore.States, AttackChore.StatesInstance, AttackChore, object>.GameInstance
	{
		// Token: 0x06006ED5 RID: 28373 RVA: 0x0029D8AD File Offset: 0x0029BAAD
		public StatesInstance(AttackChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02000F4B RID: 3915
	public class States : GameStateMachine<AttackChore.States, AttackChore.StatesInstance, AttackChore>
	{
		// Token: 0x06006ED6 RID: 28374 RVA: 0x0029D8B8 File Offset: 0x0029BAB8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.approachtarget;
			this.root.ToggleStatusItem(Db.Get().DuplicantStatusItems.Fighting, (AttackChore.StatesInstance smi) => smi.master.gameObject).EventHandler(GameHashes.TargetLost, delegate(AttackChore.StatesInstance smi)
			{
				smi.master.Fail("target lost");
			}).Enter(delegate(AttackChore.StatesInstance smi)
			{
				smi.master.GetComponent<Weapon>().Configure(1f, 1f, AttackProperties.DamageType.Standard, AttackProperties.TargetType.Single, 1, 0f);
			});
			this.approachtarget.InitializeStates(this.attacker, this.attackTarget, this.attack, null, null, NavigationTactics.Range_3_ProhibitOverlap).Enter(delegate(AttackChore.StatesInstance smi)
			{
				smi.master.CleanUpMultitool();
				smi.master.Trigger(1039067354, this.attackTarget.Get(smi));
				Health component = this.attackTarget.Get(smi).GetComponent<Health>();
				if (component == null || component.IsDefeated())
				{
					smi.StopSM("target defeated approachtarget");
				}
			});
			this.attack.Target(this.attacker).Enter(delegate(AttackChore.StatesInstance smi)
			{
				this.attackTarget.Get(smi).Subscribe(1088554450, new Action<object>(smi.master.OnTargetMoved));
				if (this.attackTarget != null && smi.master.multiTool == null)
				{
					smi.master.multiTool = new MultitoolController.Instance(this.attackTarget.Get(smi).GetComponent<Workable>(), smi.master.GetComponent<Worker>(), "attack", Assets.GetPrefab(EffectConfigs.AttackSplashId));
					smi.master.multiTool.StartSM();
				}
				this.attackTarget.Get(smi).Subscribe(1969584890, new Action<object>(smi.master.OnTargetDestroyed));
				smi.ScheduleGoTo(0.5f, this.success);
			}).Update(delegate(AttackChore.StatesInstance smi, float dt)
			{
				if (smi.master.multiTool != null)
				{
					smi.master.multiTool.UpdateHitEffectTarget();
				}
			}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(AttackChore.StatesInstance smi)
				{
					if (this.attackTarget.Get(smi) != null)
					{
						this.attackTarget.Get(smi).Unsubscribe(1088554450, new Action<object>(smi.master.OnTargetMoved));
					}
				});
			this.success.Enter("finishAttack", delegate(AttackChore.StatesInstance smi)
			{
				if (this.attackTarget.Get(smi) != null)
				{
					Transform transform = this.attackTarget.Get(smi).transform;
					Weapon component2 = this.attacker.Get(smi).gameObject.GetComponent<Weapon>();
					if (!(component2 != null))
					{
						smi.master.CleanUpMultitool();
						smi.StopSM("no weapon");
						return;
					}
					component2.AttackTarget(transform.gameObject);
					Health component3 = this.attackTarget.Get(smi).GetComponent<Health>();
					if (component3 != null)
					{
						if (!component3.IsDefeated())
						{
							smi.GoTo(this.attack);
							return;
						}
						smi.master.CleanUpMultitool();
						smi.StopSM("target defeated success");
						return;
					}
				}
				else
				{
					smi.master.CleanUpMultitool();
					smi.StopSM("no target");
				}
			}).ReturnSuccess();
		}

		// Token: 0x040053C2 RID: 21442
		public StateMachine<AttackChore.States, AttackChore.StatesInstance, AttackChore, object>.TargetParameter attackTarget;

		// Token: 0x040053C3 RID: 21443
		public StateMachine<AttackChore.States, AttackChore.StatesInstance, AttackChore, object>.TargetParameter attacker;

		// Token: 0x040053C4 RID: 21444
		public GameStateMachine<AttackChore.States, AttackChore.StatesInstance, AttackChore, object>.ApproachSubState<RangedAttackable> approachtarget;

		// Token: 0x040053C5 RID: 21445
		public GameStateMachine<AttackChore.States, AttackChore.StatesInstance, AttackChore, object>.State attack;

		// Token: 0x040053C6 RID: 21446
		public GameStateMachine<AttackChore.States, AttackChore.StatesInstance, AttackChore, object>.State success;
	}
}
