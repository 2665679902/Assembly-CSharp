using System;

// Token: 0x02000476 RID: 1142
public class Dreamer : GameStateMachine<Dreamer, Dreamer.Instance>
{
	// Token: 0x06001972 RID: 6514 RVA: 0x00088890 File Offset: 0x00086A90
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.notDreaming;
		this.notDreaming.OnSignal(this.startDreaming, this.dreaming, (Dreamer.Instance smi) => smi.currentDream != null);
		this.dreaming.Enter(new StateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.State.Callback(Dreamer.PrepareDream)).OnSignal(this.stopDreaming, this.notDreaming).Update(new Action<Dreamer.Instance, float>(this.UpdateDream), UpdateRate.SIM_EVERY_TICK, false)
			.Exit(new StateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.State.Callback(this.RemoveDream));
	}

	// Token: 0x06001973 RID: 6515 RVA: 0x00088929 File Offset: 0x00086B29
	private void RemoveDream(Dreamer.Instance smi)
	{
		smi.SetDream(null);
		NameDisplayScreen.Instance.StopDreaming(smi.gameObject);
	}

	// Token: 0x06001974 RID: 6516 RVA: 0x00088942 File Offset: 0x00086B42
	private void UpdateDream(Dreamer.Instance smi, float dt)
	{
		NameDisplayScreen.Instance.DreamTick(smi.gameObject, dt);
	}

	// Token: 0x06001975 RID: 6517 RVA: 0x00088955 File Offset: 0x00086B55
	private static void PrepareDream(Dreamer.Instance smi)
	{
		NameDisplayScreen.Instance.SetDream(smi.gameObject, smi.currentDream);
	}

	// Token: 0x04000E38 RID: 3640
	public StateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.Signal stopDreaming;

	// Token: 0x04000E39 RID: 3641
	public StateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.Signal startDreaming;

	// Token: 0x04000E3A RID: 3642
	public GameStateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.State notDreaming;

	// Token: 0x04000E3B RID: 3643
	public GameStateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.State dreaming;

	// Token: 0x020010BF RID: 4287
	public class DreamingState : GameStateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400588F RID: 22671
		public GameStateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.State hidden;

		// Token: 0x04005890 RID: 22672
		public GameStateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.State visible;
	}

	// Token: 0x020010C0 RID: 4288
	public new class Instance : GameStateMachine<Dreamer, Dreamer.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007434 RID: 29748 RVA: 0x002B269C File Offset: 0x002B089C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			NameDisplayScreen.Instance.RegisterComponent(base.gameObject, this, false);
		}

		// Token: 0x06007435 RID: 29749 RVA: 0x002B26B7 File Offset: 0x002B08B7
		public void SetDream(Dream dream)
		{
			this.currentDream = dream;
		}

		// Token: 0x06007436 RID: 29750 RVA: 0x002B26C0 File Offset: 0x002B08C0
		public void StartDreaming()
		{
			base.sm.startDreaming.Trigger(base.smi);
		}

		// Token: 0x06007437 RID: 29751 RVA: 0x002B26D8 File Offset: 0x002B08D8
		public void StopDreaming()
		{
			this.SetDream(null);
			base.sm.stopDreaming.Trigger(base.smi);
		}

		// Token: 0x04005891 RID: 22673
		public Dream currentDream;
	}
}
