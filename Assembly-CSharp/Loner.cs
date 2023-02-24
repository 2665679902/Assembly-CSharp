using System;

// Token: 0x02000806 RID: 2054
[SkipSaveFileSerialization]
public class Loner : StateMachineComponent<Loner.StatesInstance>
{
	// Token: 0x06003B82 RID: 15234 RVA: 0x0014A240 File Offset: 0x00148440
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x0200155A RID: 5466
	public class StatesInstance : GameStateMachine<Loner.States, Loner.StatesInstance, Loner, object>.GameInstance
	{
		// Token: 0x060083B2 RID: 33714 RVA: 0x002E7F47 File Offset: 0x002E6147
		public StatesInstance(Loner master)
			: base(master)
		{
		}

		// Token: 0x060083B3 RID: 33715 RVA: 0x002E7F50 File Offset: 0x002E6150
		public bool IsAlone()
		{
			WorldContainer myWorld = this.GetMyWorld();
			if (!myWorld)
			{
				return false;
			}
			int parentWorldId = myWorld.ParentWorldId;
			int id = myWorld.id;
			MinionIdentity component = base.GetComponent<MinionIdentity>();
			foreach (object obj in Components.LiveMinionIdentities)
			{
				MinionIdentity minionIdentity = (MinionIdentity)obj;
				if (component != minionIdentity)
				{
					int myWorldId = minionIdentity.GetMyWorldId();
					if (id == myWorldId || parentWorldId == myWorldId)
					{
						return false;
					}
				}
			}
			return true;
		}
	}

	// Token: 0x0200155B RID: 5467
	public class States : GameStateMachine<Loner.States, Loner.StatesInstance, Loner>
	{
		// Token: 0x060083B4 RID: 33716 RVA: 0x002E7FF8 File Offset: 0x002E61F8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.Enter(delegate(Loner.StatesInstance smi)
			{
				if (smi.IsAlone())
				{
					smi.GoTo(this.alone);
				}
			});
			this.idle.EventTransition(GameHashes.MinionMigration, (Loner.StatesInstance smi) => Game.Instance, this.alone, (Loner.StatesInstance smi) => smi.IsAlone()).EventTransition(GameHashes.MinionDelta, (Loner.StatesInstance smi) => Game.Instance, this.alone, (Loner.StatesInstance smi) => smi.IsAlone());
			this.alone.EventTransition(GameHashes.MinionMigration, (Loner.StatesInstance smi) => Game.Instance, this.idle, (Loner.StatesInstance smi) => !smi.IsAlone()).EventTransition(GameHashes.MinionDelta, (Loner.StatesInstance smi) => Game.Instance, this.idle, (Loner.StatesInstance smi) => !smi.IsAlone()).ToggleEffect("Loner");
		}

		// Token: 0x04006682 RID: 26242
		public GameStateMachine<Loner.States, Loner.StatesInstance, Loner, object>.State idle;

		// Token: 0x04006683 RID: 26243
		public GameStateMachine<Loner.States, Loner.StatesInstance, Loner, object>.State alone;
	}
}
