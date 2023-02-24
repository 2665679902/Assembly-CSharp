using System;
using UnityEngine;

// Token: 0x020007E7 RID: 2023
public class JetSuitMonitor : GameStateMachine<JetSuitMonitor, JetSuitMonitor.Instance>
{
	// Token: 0x06003A4C RID: 14924 RVA: 0x00142FE8 File Offset: 0x001411E8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		base.Target(this.owner);
		this.off.EventTransition(GameHashes.PathAdvanced, this.flying, new StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(JetSuitMonitor.ShouldStartFlying));
		this.flying.Enter(new StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.State.Callback(JetSuitMonitor.StartFlying)).Exit(new StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.State.Callback(JetSuitMonitor.StopFlying)).EventTransition(GameHashes.PathAdvanced, this.off, new StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.Transition.ConditionCallback(JetSuitMonitor.ShouldStopFlying))
			.Update(new Action<JetSuitMonitor.Instance, float>(JetSuitMonitor.Emit), UpdateRate.SIM_200ms, false);
	}

	// Token: 0x06003A4D RID: 14925 RVA: 0x00143084 File Offset: 0x00141284
	public static bool ShouldStartFlying(JetSuitMonitor.Instance smi)
	{
		return smi.navigator && smi.navigator.CurrentNavType == NavType.Hover;
	}

	// Token: 0x06003A4E RID: 14926 RVA: 0x001430A3 File Offset: 0x001412A3
	public static bool ShouldStopFlying(JetSuitMonitor.Instance smi)
	{
		return !smi.navigator || smi.navigator.CurrentNavType != NavType.Hover;
	}

	// Token: 0x06003A4F RID: 14927 RVA: 0x001430C5 File Offset: 0x001412C5
	public static void StartFlying(JetSuitMonitor.Instance smi)
	{
	}

	// Token: 0x06003A50 RID: 14928 RVA: 0x001430C7 File Offset: 0x001412C7
	public static void StopFlying(JetSuitMonitor.Instance smi)
	{
	}

	// Token: 0x06003A51 RID: 14929 RVA: 0x001430CC File Offset: 0x001412CC
	public static void Emit(JetSuitMonitor.Instance smi, float dt)
	{
		if (!smi.navigator)
		{
			return;
		}
		GameObject gameObject = smi.sm.owner.Get(smi);
		if (!gameObject)
		{
			return;
		}
		int num = Grid.PosToCell(gameObject.transform.GetPosition());
		float num2 = 0.1f * dt;
		num2 = Mathf.Min(num2, smi.jet_suit_tank.amount);
		smi.jet_suit_tank.amount -= num2;
		float num3 = num2 * 3f;
		if (num3 > 1E-45f)
		{
			SimMessages.AddRemoveSubstance(num, SimHashes.CarbonDioxide, CellEventLogger.Instance.ElementConsumerSimUpdate, num3, 473.15f, byte.MaxValue, 0, true, -1);
		}
		if (smi.jet_suit_tank.amount == 0f)
		{
			smi.navigator.AddTag(GameTags.JetSuitOutOfFuel);
			smi.navigator.SetCurrentNavType(NavType.Floor);
		}
	}

	// Token: 0x04002648 RID: 9800
	public GameStateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x04002649 RID: 9801
	public GameStateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.State flying;

	// Token: 0x0400264A RID: 9802
	public StateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.TargetParameter owner;

	// Token: 0x02001537 RID: 5431
	public new class Instance : GameStateMachine<JetSuitMonitor, JetSuitMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060082F9 RID: 33529 RVA: 0x002E6BE2 File Offset: 0x002E4DE2
		public Instance(IStateMachineTarget master, GameObject owner)
			: base(master)
		{
			base.sm.owner.Set(owner, base.smi, false);
			this.navigator = owner.GetComponent<Navigator>();
			this.jet_suit_tank = master.GetComponent<JetSuitTank>();
		}

		// Token: 0x040065F2 RID: 26098
		public Navigator navigator;

		// Token: 0x040065F3 RID: 26099
		public JetSuitTank jet_suit_tank;
	}
}
