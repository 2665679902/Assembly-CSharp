using System;
using KSerialization;
using UnityEngine;

// Token: 0x020006DF RID: 1759
public class MoltDropperMonitor : GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>
{
	// Token: 0x06002FE3 RID: 12259 RVA: 0x000FCFB8 File Offset: 0x000FB1B8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.EventHandler(GameHashes.NewDay, (MoltDropperMonitor.Instance smi) => GameClock.Instance, delegate(MoltDropperMonitor.Instance smi)
		{
			smi.spawnedThisCycle = false;
		});
		this.satisfied.OnSignal(this.cellChangedSignal, this.drop, (MoltDropperMonitor.Instance smi) => smi.ShouldDropElement());
		this.drop.Enter(delegate(MoltDropperMonitor.Instance smi)
		{
			smi.Drop();
		}).EventTransition(GameHashes.NewDay, (MoltDropperMonitor.Instance smi) => GameClock.Instance, this.satisfied, null);
	}

	// Token: 0x04001CDE RID: 7390
	public StateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.BoolParameter droppedThisCycle = new StateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.BoolParameter(false);

	// Token: 0x04001CDF RID: 7391
	public GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.State satisfied;

	// Token: 0x04001CE0 RID: 7392
	public GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.State drop;

	// Token: 0x04001CE1 RID: 7393
	public StateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.Signal cellChangedSignal;

	// Token: 0x020013EA RID: 5098
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040061FF RID: 25087
		public string onGrowDropID;

		// Token: 0x04006200 RID: 25088
		public float massToDrop;

		// Token: 0x04006201 RID: 25089
		public SimHashes blockedElement;
	}

	// Token: 0x020013EB RID: 5099
	public new class Instance : GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.GameInstance
	{
		// Token: 0x06007F8D RID: 32653 RVA: 0x002DD040 File Offset: 0x002DB240
		public Instance(IStateMachineTarget master, MoltDropperMonitor.Def def)
			: base(master, def)
		{
			Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange), "ElementDropperMonitor.Instance");
		}

		// Token: 0x06007F8E RID: 32654 RVA: 0x002DD06C File Offset: 0x002DB26C
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			Singleton<CellChangeMonitor>.Instance.UnregisterCellChangedHandler(base.transform, new System.Action(this.OnCellChange));
		}

		// Token: 0x06007F8F RID: 32655 RVA: 0x002DD091 File Offset: 0x002DB291
		private void OnCellChange()
		{
			base.sm.cellChangedSignal.Trigger(this);
		}

		// Token: 0x06007F90 RID: 32656 RVA: 0x002DD0A4 File Offset: 0x002DB2A4
		public bool ShouldDropElement()
		{
			return this.IsValidTimeToDrop() && !base.smi.HasTag(GameTags.Creatures.Hungry) && !base.smi.HasTag(GameTags.Creatures.Unhappy) && this.IsValidDropCell();
		}

		// Token: 0x06007F91 RID: 32657 RVA: 0x002DD0DC File Offset: 0x002DB2DC
		public void Drop()
		{
			GameObject gameObject = Scenario.SpawnPrefab(this.GetDropSpawnLocation(), 0, 0, base.def.onGrowDropID, Grid.SceneLayer.Ore);
			gameObject.SetActive(true);
			gameObject.GetComponent<PrimaryElement>().Mass = base.def.massToDrop;
			this.spawnedThisCycle = true;
			this.timeOfLastDrop = GameClock.Instance.GetTime();
		}

		// Token: 0x06007F92 RID: 32658 RVA: 0x002DD138 File Offset: 0x002DB338
		private int GetDropSpawnLocation()
		{
			int num = Grid.PosToCell(base.gameObject);
			int num2 = Grid.CellAbove(num);
			if (Grid.IsValidCell(num2) && !Grid.Solid[num2])
			{
				return num2;
			}
			return num;
		}

		// Token: 0x06007F93 RID: 32659 RVA: 0x002DD170 File Offset: 0x002DB370
		public bool IsValidTimeToDrop()
		{
			return !this.spawnedThisCycle && (this.timeOfLastDrop <= 0f || GameClock.Instance.GetTime() - this.timeOfLastDrop > 600f);
		}

		// Token: 0x06007F94 RID: 32660 RVA: 0x002DD1A4 File Offset: 0x002DB3A4
		public bool IsValidDropCell()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			return Grid.IsValidCell(num) && Grid.Element[num].id != base.def.blockedElement;
		}

		// Token: 0x04006202 RID: 25090
		[Serialize]
		public bool spawnedThisCycle;

		// Token: 0x04006203 RID: 25091
		[Serialize]
		public float timeOfLastDrop;
	}
}
