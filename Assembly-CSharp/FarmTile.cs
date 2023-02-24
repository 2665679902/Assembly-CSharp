using System;

// Token: 0x020005B7 RID: 1463
public class FarmTile : StateMachineComponent<FarmTile.SMInstance>
{
	// Token: 0x0600243B RID: 9275 RVA: 0x000C42C7 File Offset: 0x000C24C7
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x040014DA RID: 5338
	[MyCmpReq]
	private PlantablePlot plantablePlot;

	// Token: 0x040014DB RID: 5339
	[MyCmpReq]
	private Storage storage;

	// Token: 0x020011EF RID: 4591
	public class SMInstance : GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile, object>.GameInstance
	{
		// Token: 0x06007875 RID: 30837 RVA: 0x002BED5A File Offset: 0x002BCF5A
		public SMInstance(FarmTile master)
			: base(master)
		{
		}

		// Token: 0x06007876 RID: 30838 RVA: 0x002BED64 File Offset: 0x002BCF64
		public bool HasWater()
		{
			PrimaryElement primaryElement = base.master.storage.FindPrimaryElement(SimHashes.Water);
			return primaryElement != null && primaryElement.Mass > 0f;
		}
	}

	// Token: 0x020011F0 RID: 4592
	public class States : GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile>
	{
		// Token: 0x06007877 RID: 30839 RVA: 0x002BEDA0 File Offset: 0x002BCFA0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.empty;
			this.empty.EventTransition(GameHashes.OccupantChanged, this.full, (FarmTile.SMInstance smi) => smi.master.plantablePlot.Occupant != null);
			this.empty.wet.EventTransition(GameHashes.OnStorageChange, this.empty.dry, (FarmTile.SMInstance smi) => !smi.HasWater());
			this.empty.dry.EventTransition(GameHashes.OnStorageChange, this.empty.wet, (FarmTile.SMInstance smi) => !smi.HasWater());
			this.full.EventTransition(GameHashes.OccupantChanged, this.empty, (FarmTile.SMInstance smi) => smi.master.plantablePlot.Occupant == null);
			this.full.wet.EventTransition(GameHashes.OnStorageChange, this.full.dry, (FarmTile.SMInstance smi) => !smi.HasWater());
			this.full.dry.EventTransition(GameHashes.OnStorageChange, this.full.wet, (FarmTile.SMInstance smi) => !smi.HasWater());
		}

		// Token: 0x04005C79 RID: 23673
		public FarmTile.States.FarmStates empty;

		// Token: 0x04005C7A RID: 23674
		public FarmTile.States.FarmStates full;

		// Token: 0x02001FA1 RID: 8097
		public class FarmStates : GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile, object>.State
		{
			// Token: 0x04008CE2 RID: 36066
			public GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile, object>.State wet;

			// Token: 0x04008CE3 RID: 36067
			public GameStateMachine<FarmTile.States, FarmTile.SMInstance, FarmTile, object>.State dry;
		}
	}
}
