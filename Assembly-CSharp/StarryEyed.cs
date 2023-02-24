using System;

// Token: 0x0200098F RID: 2447
[SkipSaveFileSerialization]
public class StarryEyed : StateMachineComponent<StarryEyed.StatesInstance>
{
	// Token: 0x06004875 RID: 18549 RVA: 0x001965E5 File Offset: 0x001947E5
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x02001789 RID: 6025
	public class StatesInstance : GameStateMachine<StarryEyed.States, StarryEyed.StatesInstance, StarryEyed, object>.GameInstance
	{
		// Token: 0x06008B39 RID: 35641 RVA: 0x002FEFBA File Offset: 0x002FD1BA
		public StatesInstance(StarryEyed master)
			: base(master)
		{
		}

		// Token: 0x06008B3A RID: 35642 RVA: 0x002FEFC4 File Offset: 0x002FD1C4
		public bool IsInSpace()
		{
			WorldContainer myWorld = this.GetMyWorld();
			if (!myWorld)
			{
				return false;
			}
			int parentWorldId = myWorld.ParentWorldId;
			int id = myWorld.id;
			return myWorld.GetComponent<Clustercraft>() && parentWorldId == id;
		}
	}

	// Token: 0x0200178A RID: 6026
	public class States : GameStateMachine<StarryEyed.States, StarryEyed.StatesInstance, StarryEyed>
	{
		// Token: 0x06008B3B RID: 35643 RVA: 0x002FF004 File Offset: 0x002FD204
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.Enter(delegate(StarryEyed.StatesInstance smi)
			{
				if (smi.IsInSpace())
				{
					smi.GoTo(this.inSpace);
				}
			});
			this.idle.EventTransition(GameHashes.MinionMigration, (StarryEyed.StatesInstance smi) => Game.Instance, this.inSpace, (StarryEyed.StatesInstance smi) => smi.IsInSpace());
			this.inSpace.EventTransition(GameHashes.MinionMigration, (StarryEyed.StatesInstance smi) => Game.Instance, this.idle, (StarryEyed.StatesInstance smi) => !smi.IsInSpace()).ToggleEffect("StarryEyed");
		}

		// Token: 0x04006D4D RID: 27981
		public GameStateMachine<StarryEyed.States, StarryEyed.StatesInstance, StarryEyed, object>.State idle;

		// Token: 0x04006D4E RID: 27982
		public GameStateMachine<StarryEyed.States, StarryEyed.StatesInstance, StarryEyed, object>.State inSpace;
	}
}
