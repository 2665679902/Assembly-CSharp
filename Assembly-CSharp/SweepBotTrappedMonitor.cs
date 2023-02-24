using System;
using UnityEngine;

// Token: 0x020008ED RID: 2285
public class SweepBotTrappedMonitor : GameStateMachine<SweepBotTrappedMonitor, SweepBotTrappedMonitor.Instance, IStateMachineTarget, SweepBotTrappedMonitor.Def>
{
	// Token: 0x060041C9 RID: 16841 RVA: 0x001721D8 File Offset: 0x001703D8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.notTrapped;
		this.notTrapped.Update(delegate(SweepBotTrappedMonitor.Instance smi, float dt)
		{
			StorageUnloadMonitor.Instance smi2 = smi.master.gameObject.GetSMI<StorageUnloadMonitor.Instance>();
			Storage storage = smi2.sm.sweepLocker.Get(smi2);
			Navigator component = smi.master.GetComponent<Navigator>();
			if (storage == null)
			{
				smi.GoTo(this.death);
				return;
			}
			if ((smi.master.gameObject.HasTag(GameTags.Robots.Behaviours.RechargeBehaviour) || smi.master.gameObject.HasTag(GameTags.Robots.Behaviours.UnloadBehaviour)) && !component.CanReach(Grid.PosToCell(storage), SweepBotTrappedMonitor.defaultOffsets))
			{
				smi.GoTo(this.trapped);
			}
		}, UpdateRate.SIM_1000ms, false);
		this.trapped.ToggleBehaviour(GameTags.Robots.Behaviours.TrappedBehaviour, (SweepBotTrappedMonitor.Instance data) => true, null).ToggleStatusItem(Db.Get().RobotStatusItems.CantReachStation, null, Db.Get().StatusItemCategories.Main).Update(delegate(SweepBotTrappedMonitor.Instance smi, float dt)
		{
			StorageUnloadMonitor.Instance smi3 = smi.master.gameObject.GetSMI<StorageUnloadMonitor.Instance>();
			Storage storage2 = smi3.sm.sweepLocker.Get(smi3);
			Navigator component2 = smi.master.GetComponent<Navigator>();
			if (storage2 == null)
			{
				smi.GoTo(this.death);
			}
			else if ((!smi.master.gameObject.HasTag(GameTags.Robots.Behaviours.RechargeBehaviour) && !smi.master.gameObject.HasTag(GameTags.Robots.Behaviours.UnloadBehaviour)) || component2.CanReach(Grid.PosToCell(storage2), SweepBotTrappedMonitor.defaultOffsets))
			{
				smi.GoTo(this.notTrapped);
			}
			if (storage2 != null && component2.CanReach(Grid.PosToCell(storage2), SweepBotTrappedMonitor.defaultOffsets))
			{
				smi.GoTo(this.notTrapped);
				return;
			}
			if (storage2 == null)
			{
				smi.GoTo(this.death);
			}
		}, UpdateRate.SIM_1000ms, false);
		this.death.Enter(delegate(SweepBotTrappedMonitor.Instance smi)
		{
			smi.master.gameObject.GetComponent<OrnamentReceptacle>().OrderRemoveOccupant();
			smi.master.gameObject.GetSMI<AnimInterruptMonitor.Instance>().PlayAnim("death");
		}).OnAnimQueueComplete(this.destroySelf);
		this.destroySelf.Enter(delegate(SweepBotTrappedMonitor.Instance smi)
		{
			Game.Instance.SpawnFX(SpawnFXHashes.MeteorImpactDust, smi.master.transform.position, 0f);
			foreach (Storage storage3 in smi.master.gameObject.GetComponents<Storage>())
			{
				for (int j = storage3.items.Count - 1; j >= 0; j--)
				{
					GameObject gameObject = storage3.Drop(storage3.items[j], true);
					if (gameObject != null)
					{
						if (GameComps.Fallers.Has(gameObject))
						{
							GameComps.Fallers.Remove(gameObject);
						}
						GameComps.Fallers.Add(gameObject, new Vector2((float)UnityEngine.Random.Range(-5, 5), 8f));
					}
				}
			}
			PrimaryElement component3 = smi.master.GetComponent<PrimaryElement>();
			component3.Element.substance.SpawnResource(Grid.CellToPosCCC(Grid.PosToCell(smi.master.gameObject), Grid.SceneLayer.Ore), SweepBotConfig.MASS, component3.Temperature, component3.DiseaseIdx, component3.DiseaseCount, false, false, false);
			Util.KDestroyGameObject(smi.master.gameObject);
		});
	}

	// Token: 0x04002BD7 RID: 11223
	public static CellOffset[] defaultOffsets = new CellOffset[]
	{
		new CellOffset(0, 0)
	};

	// Token: 0x04002BD8 RID: 11224
	public GameStateMachine<SweepBotTrappedMonitor, SweepBotTrappedMonitor.Instance, IStateMachineTarget, SweepBotTrappedMonitor.Def>.State notTrapped;

	// Token: 0x04002BD9 RID: 11225
	public GameStateMachine<SweepBotTrappedMonitor, SweepBotTrappedMonitor.Instance, IStateMachineTarget, SweepBotTrappedMonitor.Def>.State trapped;

	// Token: 0x04002BDA RID: 11226
	public GameStateMachine<SweepBotTrappedMonitor, SweepBotTrappedMonitor.Instance, IStateMachineTarget, SweepBotTrappedMonitor.Def>.State death;

	// Token: 0x04002BDB RID: 11227
	public GameStateMachine<SweepBotTrappedMonitor, SweepBotTrappedMonitor.Instance, IStateMachineTarget, SweepBotTrappedMonitor.Def>.State destroySelf;

	// Token: 0x020016B2 RID: 5810
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020016B3 RID: 5811
	public new class Instance : GameStateMachine<SweepBotTrappedMonitor, SweepBotTrappedMonitor.Instance, IStateMachineTarget, SweepBotTrappedMonitor.Def>.GameInstance
	{
		// Token: 0x06008842 RID: 34882 RVA: 0x002F4F6C File Offset: 0x002F316C
		public Instance(IStateMachineTarget master, SweepBotTrappedMonitor.Def def)
			: base(master, def)
		{
		}
	}
}
