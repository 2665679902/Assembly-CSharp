using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class FlopStates : GameStateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>
{
	// Token: 0x0600035C RID: 860 RVA: 0x0001A87C File Offset: 0x00018A7C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.flop_pre;
		this.root.ToggleStatusItem(CREATURES.STATUSITEMS.FLOPPING.NAME, CREATURES.STATUSITEMS.FLOPPING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main);
		this.flop_pre.Enter(new StateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>.State.Callback(FlopStates.ChooseDirection)).Transition(this.flop_cycle, new StateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>.Transition.ConditionCallback(FlopStates.ShouldFlop), UpdateRate.SIM_200ms).Transition(this.pst, GameStateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>.Not(new StateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>.Transition.ConditionCallback(FlopStates.ShouldFlop)), UpdateRate.SIM_200ms);
		this.flop_cycle.PlayAnim("flop_loop", KAnim.PlayMode.Once).Transition(this.pst, new StateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>.Transition.ConditionCallback(FlopStates.IsSubstantialLiquid), UpdateRate.SIM_200ms).Update("Flop", new Action<FlopStates.Instance, float>(FlopStates.FlopForward), UpdateRate.SIM_33ms, false)
			.OnAnimQueueComplete(this.flop_pre);
		this.pst.QueueAnim("flop_loop", true, null).BehaviourComplete(GameTags.Creatures.Flopping, false);
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0001A990 File Offset: 0x00018B90
	public static bool ShouldFlop(FlopStates.Instance smi)
	{
		int num = Grid.CellBelow(Grid.PosToCell(smi.transform.GetPosition()));
		return Grid.IsValidCell(num) && Grid.Solid[num];
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0001A9C8 File Offset: 0x00018BC8
	public static void ChooseDirection(FlopStates.Instance smi)
	{
		int num = Grid.PosToCell(smi.transform.GetPosition());
		if (FlopStates.SearchForLiquid(num, 1))
		{
			smi.currentDir = 1f;
			return;
		}
		if (FlopStates.SearchForLiquid(num, -1))
		{
			smi.currentDir = -1f;
			return;
		}
		if (UnityEngine.Random.value > 0.5f)
		{
			smi.currentDir = 1f;
			return;
		}
		smi.currentDir = -1f;
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0001AA34 File Offset: 0x00018C34
	private static bool SearchForLiquid(int cell, int delta_x)
	{
		while (Grid.IsValidCell(cell))
		{
			if (Grid.IsSubstantialLiquid(cell, 0.35f))
			{
				return true;
			}
			if (Grid.Solid[cell])
			{
				return false;
			}
			if (Grid.CritterImpassable[cell])
			{
				return false;
			}
			int num = Grid.CellBelow(cell);
			if (Grid.IsValidCell(num) && Grid.Solid[num])
			{
				cell += delta_x;
			}
			else
			{
				cell = num;
			}
		}
		return false;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0001AAA0 File Offset: 0x00018CA0
	public static void FlopForward(FlopStates.Instance smi, float dt)
	{
		KBatchedAnimController component = smi.GetComponent<KBatchedAnimController>();
		int currentFrame = component.currentFrame;
		if (component.IsVisible() && (currentFrame < 23 || currentFrame > 36))
		{
			return;
		}
		Vector3 position = smi.transform.GetPosition();
		Vector3 vector = position;
		vector.x = position.x + smi.currentDir * dt * 1f;
		int num = Grid.PosToCell(vector);
		if (Grid.IsValidCell(num) && !Grid.Solid[num] && !Grid.CritterImpassable[num])
		{
			smi.transform.SetPosition(vector);
			return;
		}
		smi.currentDir = -smi.currentDir;
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0001AB3A File Offset: 0x00018D3A
	public static bool IsSubstantialLiquid(FlopStates.Instance smi)
	{
		return Grid.IsSubstantialLiquid(Grid.PosToCell(smi.transform.GetPosition()), 0.35f);
	}

	// Token: 0x04000235 RID: 565
	private GameStateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>.State flop_pre;

	// Token: 0x04000236 RID: 566
	private GameStateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>.State flop_cycle;

	// Token: 0x04000237 RID: 567
	private GameStateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>.State pst;

	// Token: 0x02000E5E RID: 3678
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02000E5F RID: 3679
	public new class Instance : GameStateMachine<FlopStates, FlopStates.Instance, IStateMachineTarget, FlopStates.Def>.GameInstance
	{
		// Token: 0x06006C1E RID: 27678 RVA: 0x002976DE File Offset: 0x002958DE
		public Instance(Chore<FlopStates.Instance> chore, FlopStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Flopping);
		}

		// Token: 0x0400515D RID: 20829
		public float currentDir = 1f;
	}
}
