using System;

// Token: 0x0200004B RID: 75
public class LightController : GameStateMachine<LightController, LightController.Instance>
{
	// Token: 0x0600015D RID: 349 RVA: 0x00009B64 File Offset: 0x00007D64
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (LightController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (LightController.Instance smi) => !smi.GetComponent<Operational>().IsOperational).ToggleStatusItem(Db.Get().BuildingStatusItems.EmittingLight, null)
			.Enter("SetActive", delegate(LightController.Instance smi)
			{
				smi.GetComponent<Operational>().SetActive(true, false);
			});
	}

	// Token: 0x040000BA RID: 186
	public GameStateMachine<LightController, LightController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040000BB RID: 187
	public GameStateMachine<LightController, LightController.Instance, IStateMachineTarget, object>.State on;

	// Token: 0x02000DC7 RID: 3527
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000DC8 RID: 3528
	public new class Instance : GameStateMachine<LightController, LightController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06006ACA RID: 27338 RVA: 0x00295A03 File Offset: 0x00293C03
		public Instance(IStateMachineTarget master, LightController.Def def)
			: base(master, def)
		{
		}
	}
}
