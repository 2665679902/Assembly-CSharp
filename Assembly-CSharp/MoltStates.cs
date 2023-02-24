using System;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class MoltStates : GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>
{
	// Token: 0x060003A5 RID: 933 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.moltpre;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.MOLTING.NAME, CREATURES.STATUSITEMS.MOLTING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.moltpre.Enter(new StateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.State.Callback(MoltStates.Molt)).QueueAnim("lay_egg_pre", false, null).OnAnimQueueComplete(this.moltpst);
		this.moltpst.QueueAnim("lay_egg_pst", false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.ScalesGrown, false);
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x0001C49B File Offset: 0x0001A69B
	private static void Molt(MoltStates.Instance smi)
	{
		smi.eggPos = smi.transform.GetPosition();
		smi.GetSMI<ScaleGrowthMonitor.Instance>().Shear();
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x0001C4BC File Offset: 0x0001A6BC
	private static int GetMoveAsideCell(MoltStates.Instance smi)
	{
		int num = 1;
		if (GenericGameSettings.instance.acceleratedLifecycle)
		{
			num = 8;
		}
		int num2 = Grid.PosToCell(smi);
		if (Grid.IsValidCell(num2))
		{
			int num3 = Grid.OffsetCell(num2, num, 0);
			if (Grid.IsValidCell(num3) && !Grid.Solid[num3])
			{
				return num3;
			}
			int num4 = Grid.OffsetCell(num2, -num, 0);
			if (Grid.IsValidCell(num4))
			{
				return num4;
			}
		}
		return Grid.InvalidCell;
	}

	// Token: 0x0400025A RID: 602
	public GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.State moltpre;

	// Token: 0x0400025B RID: 603
	public GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.State moltpst;

	// Token: 0x0400025C RID: 604
	public GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.State behaviourcomplete;

	// Token: 0x02000E90 RID: 3728
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E91 RID: 3729
	public new class Instance : GameStateMachine<MoltStates, MoltStates.Instance, IStateMachineTarget, MoltStates.Def>.GameInstance
	{
		// Token: 0x06006C8C RID: 27788 RVA: 0x002981C4 File Offset: 0x002963C4
		public Instance(Chore<MoltStates.Instance> chore, MoltStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.ScalesGrown);
		}

		// Token: 0x040051C0 RID: 20928
		public Vector3 eggPos;
	}
}
