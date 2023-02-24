using System;
using System.Collections.Generic;

// Token: 0x020006E0 RID: 1760
public class NearbyCreatureMonitor : GameStateMachine<NearbyCreatureMonitor, NearbyCreatureMonitor.Instance, IStateMachineTarget>
{
	// Token: 0x06002FE5 RID: 12261 RVA: 0x000FD0C2 File Offset: 0x000FB2C2
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update("UpdateNearbyCreatures", delegate(NearbyCreatureMonitor.Instance smi, float dt)
		{
			smi.UpdateNearbyCreatures(dt);
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x020013ED RID: 5101
	public new class Instance : GameStateMachine<NearbyCreatureMonitor, NearbyCreatureMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06007F9C RID: 32668 RVA: 0x002DD224 File Offset: 0x002DB424
		// (remove) Token: 0x06007F9D RID: 32669 RVA: 0x002DD25C File Offset: 0x002DB45C
		public event Action<float, List<KPrefabID>> OnUpdateNearbyCreatures;

		// Token: 0x06007F9E RID: 32670 RVA: 0x002DD291 File Offset: 0x002DB491
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06007F9F RID: 32671 RVA: 0x002DD29C File Offset: 0x002DB49C
		public void UpdateNearbyCreatures(float dt)
		{
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(base.gameObject));
			if (cavityForCell != null)
			{
				this.OnUpdateNearbyCreatures(dt, cavityForCell.creatures);
			}
		}
	}
}
