using System;

// Token: 0x0200084F RID: 2127
public class TiredMonitor : GameStateMachine<TiredMonitor, TiredMonitor.Instance>
{
	// Token: 0x06003D34 RID: 15668 RVA: 0x00156314 File Offset: 0x00154514
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventTransition(GameHashes.SleepFail, this.tired, null);
		this.tired.Enter(delegate(TiredMonitor.Instance smi)
		{
			smi.SetInterruptDay();
		}).EventTransition(GameHashes.NewDay, (TiredMonitor.Instance smi) => GameClock.Instance, this.root, (TiredMonitor.Instance smi) => smi.AllowInterruptClear()).ToggleExpression(Db.Get().Expressions.Tired, null)
			.ToggleAnims("anim_loco_walk_slouch_kanim", 0f, "")
			.ToggleAnims("anim_idle_slouch_kanim", 0f, "");
	}

	// Token: 0x04002818 RID: 10264
	public GameStateMachine<TiredMonitor, TiredMonitor.Instance, IStateMachineTarget, object>.State tired;

	// Token: 0x020015FE RID: 5630
	public new class Instance : GameStateMachine<TiredMonitor, TiredMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600864A RID: 34378 RVA: 0x002EEA54 File Offset: 0x002ECC54
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600864B RID: 34379 RVA: 0x002EEA6B File Offset: 0x002ECC6B
		public void SetInterruptDay()
		{
			this.interruptedDay = GameClock.Instance.GetCycle();
		}

		// Token: 0x0600864C RID: 34380 RVA: 0x002EEA7D File Offset: 0x002ECC7D
		public bool AllowInterruptClear()
		{
			bool flag = GameClock.Instance.GetCycle() > this.interruptedDay + 1;
			if (flag)
			{
				this.interruptedDay = -1;
			}
			return flag;
		}

		// Token: 0x040068A0 RID: 26784
		public int disturbedDay = -1;

		// Token: 0x040068A1 RID: 26785
		public int interruptedDay = -1;
	}
}
