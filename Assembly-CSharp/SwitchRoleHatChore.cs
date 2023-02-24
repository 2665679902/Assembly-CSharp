using System;
using UnityEngine;

// Token: 0x02000399 RID: 921
public class SwitchRoleHatChore : Chore<SwitchRoleHatChore.StatesInstance>
{
	// Token: 0x0600129D RID: 4765 RVA: 0x00063A88 File Offset: 0x00061C88
	public SwitchRoleHatChore(IStateMachineTarget target, ChoreType chore_type)
		: base(chore_type, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new SwitchRoleHatChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x02000F9A RID: 3994
	public class StatesInstance : GameStateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore, object>.GameInstance
	{
		// Token: 0x06007004 RID: 28676 RVA: 0x002A5400 File Offset: 0x002A3600
		public StatesInstance(SwitchRoleHatChore master, GameObject duplicant)
			: base(master)
		{
			base.sm.duplicant.Set(duplicant, base.smi, false);
		}
	}

	// Token: 0x02000F9B RID: 3995
	public class States : GameStateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore>
	{
		// Token: 0x06007005 RID: 28677 RVA: 0x002A5424 File Offset: 0x002A3624
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.start;
			base.Target(this.duplicant);
			this.start.Enter(delegate(SwitchRoleHatChore.StatesInstance smi)
			{
				if (this.duplicant.Get(smi).GetComponent<MinionResume>().CurrentHat == null)
				{
					smi.GoTo(this.delay);
					return;
				}
				smi.GoTo(this.remove_hat);
			});
			this.remove_hat.ToggleAnims("anim_hat_kanim", 0f, "").PlayAnim("hat_off").OnAnimQueueComplete(this.delay);
			this.delay.ToggleThought(Db.Get().Thoughts.NewRole, null).ToggleExpression(Db.Get().Expressions.Happy, null).ToggleAnims("anim_selfish_kanim", 0f, "")
				.QueueAnim("working_pre", false, null)
				.QueueAnim("working_loop", false, null)
				.QueueAnim("working_pst", false, null)
				.OnAnimQueueComplete(this.applyHat_pre);
			this.applyHat_pre.ToggleAnims("anim_hat_kanim", 0f, "").Enter(delegate(SwitchRoleHatChore.StatesInstance smi)
			{
				this.duplicant.Get(smi).GetComponent<MinionResume>().ApplyTargetHat();
			}).PlayAnim("hat_first")
				.OnAnimQueueComplete(this.applyHat);
			this.applyHat.ToggleAnims("anim_hat_kanim", 0f, "").PlayAnim("working_pst").OnAnimQueueComplete(this.complete);
			this.complete.ReturnSuccess();
		}

		// Token: 0x040054F6 RID: 21750
		public StateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore, object>.TargetParameter duplicant;

		// Token: 0x040054F7 RID: 21751
		public GameStateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore, object>.State remove_hat;

		// Token: 0x040054F8 RID: 21752
		public GameStateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore, object>.State start;

		// Token: 0x040054F9 RID: 21753
		public GameStateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore, object>.State delay;

		// Token: 0x040054FA RID: 21754
		public GameStateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore, object>.State delay_pst;

		// Token: 0x040054FB RID: 21755
		public GameStateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore, object>.State applyHat_pre;

		// Token: 0x040054FC RID: 21756
		public GameStateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore, object>.State applyHat;

		// Token: 0x040054FD RID: 21757
		public GameStateMachine<SwitchRoleHatChore.States, SwitchRoleHatChore.StatesInstance, SwitchRoleHatChore, object>.State complete;
	}
}
