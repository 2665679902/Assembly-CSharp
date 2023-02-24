using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000862 RID: 2146
[SkipSaveFileSerialization]
public class PrefersWarmer : StateMachineComponent<PrefersWarmer.StatesInstance>
{
	// Token: 0x06003DA9 RID: 15785 RVA: 0x00158AD9 File Offset: 0x00156CD9
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x02001627 RID: 5671
	public class StatesInstance : GameStateMachine<PrefersWarmer.States, PrefersWarmer.StatesInstance, PrefersWarmer, object>.GameInstance
	{
		// Token: 0x060086E1 RID: 34529 RVA: 0x002F02C2 File Offset: 0x002EE4C2
		public StatesInstance(PrefersWarmer master)
			: base(master)
		{
		}
	}

	// Token: 0x02001628 RID: 5672
	public class States : GameStateMachine<PrefersWarmer.States, PrefersWarmer.StatesInstance, PrefersWarmer>
	{
		// Token: 0x060086E2 RID: 34530 RVA: 0x002F02CB File Offset: 0x002EE4CB
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleAttributeModifier(DUPLICANTS.TRAITS.NEEDS.PREFERSWARMER.NAME, (PrefersWarmer.StatesInstance smi) => this.modifier, null);
		}

		// Token: 0x04006924 RID: 26916
		private AttributeModifier modifier = new AttributeModifier("ThermalConductivityBarrier", -0.005f, DUPLICANTS.TRAITS.NEEDS.PREFERSWARMER.NAME, false, false, true);
	}
}
