using System;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public class FlopMonitor : GameStateMachine<FlopMonitor, FlopMonitor.Instance, IStateMachineTarget, FlopMonitor.Def>
{
	// Token: 0x06001905 RID: 6405 RVA: 0x00085B22 File Offset: 0x00083D22
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Flopping, (FlopMonitor.Instance smi) => smi.ShouldBeginFlopping(), null);
	}

	// Token: 0x0200109F RID: 4255
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020010A0 RID: 4256
	public new class Instance : GameStateMachine<FlopMonitor, FlopMonitor.Instance, IStateMachineTarget, FlopMonitor.Def>.GameInstance
	{
		// Token: 0x060073C6 RID: 29638 RVA: 0x002B1756 File Offset: 0x002AF956
		public Instance(IStateMachineTarget master, FlopMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060073C7 RID: 29639 RVA: 0x002B1760 File Offset: 0x002AF960
		public bool ShouldBeginFlopping()
		{
			Vector3 position = base.transform.GetPosition();
			position.y += CreatureFallMonitor.FLOOR_DISTANCE;
			int num = Grid.PosToCell(base.transform.GetPosition());
			int num2 = Grid.PosToCell(position);
			return Grid.IsValidCell(num2) && Grid.Solid[num2] && !Grid.IsSubstantialLiquid(num, 0.35f) && !Grid.IsLiquid(Grid.CellAbove(num));
		}
	}
}
