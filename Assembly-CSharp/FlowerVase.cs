using System;

// Token: 0x020005BC RID: 1468
public class FlowerVase : StateMachineComponent<FlowerVase.SMInstance>
{
	// Token: 0x0600246B RID: 9323 RVA: 0x000C4E5E File Offset: 0x000C305E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600246C RID: 9324 RVA: 0x000C4E66 File Offset: 0x000C3066
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x040014F5 RID: 5365
	[MyCmpReq]
	private PlantablePlot plantablePlot;

	// Token: 0x040014F6 RID: 5366
	[MyCmpReq]
	private KBoxCollider2D boxCollider;

	// Token: 0x020011F9 RID: 4601
	public class SMInstance : GameStateMachine<FlowerVase.States, FlowerVase.SMInstance, FlowerVase, object>.GameInstance
	{
		// Token: 0x0600788E RID: 30862 RVA: 0x002BF5AE File Offset: 0x002BD7AE
		public SMInstance(FlowerVase master)
			: base(master)
		{
		}
	}

	// Token: 0x020011FA RID: 4602
	public class States : GameStateMachine<FlowerVase.States, FlowerVase.SMInstance, FlowerVase>
	{
		// Token: 0x0600788F RID: 30863 RVA: 0x002BF5B8 File Offset: 0x002BD7B8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			this.empty.EventTransition(GameHashes.OccupantChanged, this.full, (FlowerVase.SMInstance smi) => smi.master.plantablePlot.Occupant != null).PlayAnim("off");
			this.full.EventTransition(GameHashes.OccupantChanged, this.empty, (FlowerVase.SMInstance smi) => smi.master.plantablePlot.Occupant == null).PlayAnim("on");
		}

		// Token: 0x04005C94 RID: 23700
		public GameStateMachine<FlowerVase.States, FlowerVase.SMInstance, FlowerVase, object>.State empty;

		// Token: 0x04005C95 RID: 23701
		public GameStateMachine<FlowerVase.States, FlowerVase.SMInstance, FlowerVase, object>.State full;
	}
}
