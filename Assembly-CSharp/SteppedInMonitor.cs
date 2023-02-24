using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000848 RID: 2120
public class SteppedInMonitor : GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance>
{
	// Token: 0x06003D14 RID: 15636 RVA: 0x00155490 File Offset: 0x00153690
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.Transition(this.carpetedFloor, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsOnCarpet), UpdateRate.SIM_200ms).Transition(this.wetFloor, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsFloorWet), UpdateRate.SIM_200ms).Transition(this.wetBody, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsSubmerged), UpdateRate.SIM_200ms);
		this.carpetedFloor.Enter(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State.Callback(SteppedInMonitor.GetCarpetFeet)).ToggleExpression(Db.Get().Expressions.Tickled, null).Update(new Action<SteppedInMonitor.Instance, float>(SteppedInMonitor.GetCarpetFeet), UpdateRate.SIM_1000ms, false)
			.Transition(this.satisfied, GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsOnCarpet)), UpdateRate.SIM_200ms)
			.Transition(this.wetFloor, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsFloorWet), UpdateRate.SIM_200ms)
			.Transition(this.wetBody, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsSubmerged), UpdateRate.SIM_200ms);
		this.wetFloor.Enter(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State.Callback(SteppedInMonitor.GetWetFeet)).Update(new Action<SteppedInMonitor.Instance, float>(SteppedInMonitor.GetWetFeet), UpdateRate.SIM_1000ms, false).Transition(this.satisfied, GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsFloorWet)), UpdateRate.SIM_200ms)
			.Transition(this.wetBody, new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsSubmerged), UpdateRate.SIM_200ms);
		this.wetBody.Enter(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State.Callback(SteppedInMonitor.GetSoaked)).Update(new Action<SteppedInMonitor.Instance, float>(SteppedInMonitor.GetSoaked), UpdateRate.SIM_1000ms, false).Transition(this.wetFloor, GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Not(new StateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(SteppedInMonitor.IsSubmerged)), UpdateRate.SIM_200ms);
	}

	// Token: 0x06003D15 RID: 15637 RVA: 0x00155629 File Offset: 0x00153829
	private static void GetCarpetFeet(SteppedInMonitor.Instance smi, float dt)
	{
		SteppedInMonitor.GetCarpetFeet(smi);
	}

	// Token: 0x06003D16 RID: 15638 RVA: 0x00155631 File Offset: 0x00153831
	private static void GetCarpetFeet(SteppedInMonitor.Instance smi)
	{
		if (!smi.effects.HasEffect("SoakingWet") && !smi.effects.HasEffect("WetFeet"))
		{
			smi.effects.Add("CarpetFeet", true);
		}
	}

	// Token: 0x06003D17 RID: 15639 RVA: 0x00155669 File Offset: 0x00153869
	private static void GetWetFeet(SteppedInMonitor.Instance smi, float dt)
	{
		SteppedInMonitor.GetWetFeet(smi);
	}

	// Token: 0x06003D18 RID: 15640 RVA: 0x00155674 File Offset: 0x00153874
	private static void GetWetFeet(SteppedInMonitor.Instance smi)
	{
		if (!smi.effects.HasEffect("CarpetFeet"))
		{
			smi.effects.Remove("CarpetFeet");
		}
		if (!smi.effects.HasEffect("SoakingWet"))
		{
			smi.effects.Add("WetFeet", true);
		}
	}

	// Token: 0x06003D19 RID: 15641 RVA: 0x001556C7 File Offset: 0x001538C7
	private static void GetSoaked(SteppedInMonitor.Instance smi, float dt)
	{
		SteppedInMonitor.GetSoaked(smi);
	}

	// Token: 0x06003D1A RID: 15642 RVA: 0x001556D0 File Offset: 0x001538D0
	private static void GetSoaked(SteppedInMonitor.Instance smi)
	{
		if (!smi.effects.HasEffect("CarpetFeet"))
		{
			smi.effects.Remove("CarpetFeet");
		}
		if (smi.effects.HasEffect("WetFeet"))
		{
			smi.effects.Remove("WetFeet");
		}
		smi.effects.Add("SoakingWet", true);
	}

	// Token: 0x06003D1B RID: 15643 RVA: 0x00155734 File Offset: 0x00153934
	private static bool IsOnCarpet(SteppedInMonitor.Instance smi)
	{
		int num = Grid.CellBelow(Grid.PosToCell(smi));
		if (!Grid.IsValidCell(num))
		{
			return false;
		}
		GameObject gameObject = Grid.Objects[num, 9];
		return Grid.IsValidCell(num) && gameObject != null && gameObject.HasTag(GameTags.Carpeted);
	}

	// Token: 0x06003D1C RID: 15644 RVA: 0x00155784 File Offset: 0x00153984
	private static bool IsFloorWet(SteppedInMonitor.Instance smi)
	{
		int num = Grid.PosToCell(smi);
		return Grid.IsValidCell(num) && Grid.Element[num].IsLiquid;
	}

	// Token: 0x06003D1D RID: 15645 RVA: 0x001557B0 File Offset: 0x001539B0
	private static bool IsSubmerged(SteppedInMonitor.Instance smi)
	{
		int num = Grid.CellAbove(Grid.PosToCell(smi));
		return Grid.IsValidCell(num) && Grid.Element[num].IsLiquid;
	}

	// Token: 0x040027FB RID: 10235
	public GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040027FC RID: 10236
	public GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State carpetedFloor;

	// Token: 0x040027FD RID: 10237
	public GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State wetFloor;

	// Token: 0x040027FE RID: 10238
	public GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.State wetBody;

	// Token: 0x020015E9 RID: 5609
	public new class Instance : GameStateMachine<SteppedInMonitor, SteppedInMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060085E1 RID: 34273 RVA: 0x002ED656 File Offset: 0x002EB856
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.effects = base.GetComponent<Effects>();
		}

		// Token: 0x04006846 RID: 26694
		public Effects effects;
	}
}
