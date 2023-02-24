using System;

// Token: 0x02000827 RID: 2087
public class DoctorMonitor : GameStateMachine<DoctorMonitor, DoctorMonitor.Instance>
{
	// Token: 0x06003C74 RID: 15476 RVA: 0x00150F6B File Offset: 0x0014F16B
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.root.ToggleUrge(Db.Get().Urges.Doctor);
	}

	// Token: 0x0200159E RID: 5534
	public new class Instance : GameStateMachine<DoctorMonitor, DoctorMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008490 RID: 33936 RVA: 0x002EA476 File Offset: 0x002E8676
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}
	}
}
