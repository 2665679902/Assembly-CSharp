using System;

// Token: 0x02000462 RID: 1122
public class CleaningMonitor : GameStateMachine<CleaningMonitor, CleaningMonitor.Instance, IStateMachineTarget, CleaningMonitor.Def>
{
	// Token: 0x060018EC RID: 6380 RVA: 0x00085174 File Offset: 0x00083374
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.clean;
		this.clean.ToggleBehaviour(GameTags.Creatures.Cleaning, (CleaningMonitor.Instance smi) => smi.CanCleanElementState(), delegate(CleaningMonitor.Instance smi)
		{
			smi.GoTo(this.cooldown);
		});
		this.cooldown.ScheduleGoTo((CleaningMonitor.Instance smi) => smi.def.coolDown, this.clean);
	}

	// Token: 0x04000DEF RID: 3567
	public GameStateMachine<CleaningMonitor, CleaningMonitor.Instance, IStateMachineTarget, CleaningMonitor.Def>.State cooldown;

	// Token: 0x04000DF0 RID: 3568
	public GameStateMachine<CleaningMonitor, CleaningMonitor.Instance, IStateMachineTarget, CleaningMonitor.Def>.State clean;

	// Token: 0x02001089 RID: 4233
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040057F1 RID: 22513
		public Element.State elementState = Element.State.Liquid;

		// Token: 0x040057F2 RID: 22514
		public CellOffset[] cellOffsets;

		// Token: 0x040057F3 RID: 22515
		public float coolDown = 30f;
	}

	// Token: 0x0200108A RID: 4234
	public new class Instance : GameStateMachine<CleaningMonitor, CleaningMonitor.Instance, IStateMachineTarget, CleaningMonitor.Def>.GameInstance
	{
		// Token: 0x06007368 RID: 29544 RVA: 0x002B037E File Offset: 0x002AE57E
		public Instance(IStateMachineTarget master, CleaningMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007369 RID: 29545 RVA: 0x002B0388 File Offset: 0x002AE588
		public bool CanCleanElementState()
		{
			int num = Grid.PosToCell(base.smi.transform.GetPosition());
			if (!Grid.IsValidCell(num))
			{
				return false;
			}
			if (!Grid.IsLiquid(num) && base.smi.def.elementState == Element.State.Liquid)
			{
				return false;
			}
			if (Grid.DiseaseCount[num] > 0)
			{
				return true;
			}
			if (base.smi.def.cellOffsets != null)
			{
				foreach (CellOffset cellOffset in base.smi.def.cellOffsets)
				{
					int num2 = Grid.OffsetCell(num, cellOffset);
					if (Grid.IsValidCell(num2) && Grid.DiseaseCount[num2] > 0)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
