using System;

// Token: 0x020009A9 RID: 2473
[SkipSaveFileSerialization]
public class Thriver : StateMachineComponent<Thriver.StatesInstance>
{
	// Token: 0x0600495D RID: 18781 RVA: 0x0019B18F File Offset: 0x0019938F
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x020017AB RID: 6059
	public class StatesInstance : GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver, object>.GameInstance
	{
		// Token: 0x06008B8B RID: 35723 RVA: 0x002FFF20 File Offset: 0x002FE120
		public StatesInstance(Thriver master)
			: base(master)
		{
		}

		// Token: 0x06008B8C RID: 35724 RVA: 0x002FFF2C File Offset: 0x002FE12C
		public bool IsStressed()
		{
			StressMonitor.Instance smi = base.master.GetSMI<StressMonitor.Instance>();
			return smi != null && smi.IsStressed();
		}
	}

	// Token: 0x020017AC RID: 6060
	public class States : GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver>
	{
		// Token: 0x06008B8D RID: 35725 RVA: 0x002FFF50 File Offset: 0x002FE150
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.EventTransition(GameHashes.NotStressed, this.idle, null).EventTransition(GameHashes.Stressed, this.stressed, null).EventTransition(GameHashes.StressedHadEnough, this.stressed, null)
				.Enter(delegate(Thriver.StatesInstance smi)
				{
					StressMonitor.Instance smi2 = smi.master.GetSMI<StressMonitor.Instance>();
					if (smi2 != null && smi2.IsStressed())
					{
						smi.GoTo(this.stressed);
					}
				});
			this.idle.DoNothing();
			this.stressed.ToggleEffect("Thriver");
			this.toostressed.DoNothing();
		}

		// Token: 0x04006DB4 RID: 28084
		public GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver, object>.State idle;

		// Token: 0x04006DB5 RID: 28085
		public GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver, object>.State stressed;

		// Token: 0x04006DB6 RID: 28086
		public GameStateMachine<Thriver.States, Thriver.StatesInstance, Thriver, object>.State toostressed;
	}
}
