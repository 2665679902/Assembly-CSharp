using System;
using System.Collections.Generic;

// Token: 0x0200081E RID: 2078
public class ColonyRationMonitor : GameStateMachine<ColonyRationMonitor, ColonyRationMonitor.Instance>
{
	// Token: 0x06003C55 RID: 15445 RVA: 0x00150298 File Offset: 0x0014E498
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.Update("UpdateOutOfRations", delegate(ColonyRationMonitor.Instance smi, float dt)
		{
			smi.UpdateIsOutOfRations();
		}, UpdateRate.SIM_200ms, false);
		this.satisfied.ParamTransition<bool>(this.isOutOfRations, this.outofrations, GameStateMachine<ColonyRationMonitor, ColonyRationMonitor.Instance, IStateMachineTarget, object>.IsTrue).TriggerOnEnter(GameHashes.ColonyHasRationsChanged, null);
		this.outofrations.ParamTransition<bool>(this.isOutOfRations, this.satisfied, GameStateMachine<ColonyRationMonitor, ColonyRationMonitor.Instance, IStateMachineTarget, object>.IsFalse).TriggerOnEnter(GameHashes.ColonyHasRationsChanged, null);
	}

	// Token: 0x04002742 RID: 10050
	public GameStateMachine<ColonyRationMonitor, ColonyRationMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x04002743 RID: 10051
	public GameStateMachine<ColonyRationMonitor, ColonyRationMonitor.Instance, IStateMachineTarget, object>.State outofrations;

	// Token: 0x04002744 RID: 10052
	private StateMachine<ColonyRationMonitor, ColonyRationMonitor.Instance, IStateMachineTarget, object>.BoolParameter isOutOfRations = new StateMachine<ColonyRationMonitor, ColonyRationMonitor.Instance, IStateMachineTarget, object>.BoolParameter();

	// Token: 0x02001584 RID: 5508
	public new class Instance : GameStateMachine<ColonyRationMonitor, ColonyRationMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008431 RID: 33841 RVA: 0x002E95D5 File Offset: 0x002E77D5
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.UpdateIsOutOfRations();
		}

		// Token: 0x06008432 RID: 33842 RVA: 0x002E95E4 File Offset: 0x002E77E4
		public void UpdateIsOutOfRations()
		{
			bool flag = true;
			using (List<Edible>.Enumerator enumerator = Components.Edibles.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetComponent<Pickupable>().UnreservedAmount > 0f)
					{
						flag = false;
						break;
					}
				}
			}
			base.smi.sm.isOutOfRations.Set(flag, base.smi, false);
		}

		// Token: 0x06008433 RID: 33843 RVA: 0x002E9668 File Offset: 0x002E7868
		public bool IsOutOfRations()
		{
			return base.smi.sm.isOutOfRations.Get(base.smi);
		}
	}
}
