using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000861 RID: 2145
[SkipSaveFileSerialization]
public class PrefersColder : StateMachineComponent<PrefersColder.StatesInstance>
{
	// Token: 0x06003DA7 RID: 15783 RVA: 0x00158AC4 File Offset: 0x00156CC4
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x02001625 RID: 5669
	public class StatesInstance : GameStateMachine<PrefersColder.States, PrefersColder.StatesInstance, PrefersColder, object>.GameInstance
	{
		// Token: 0x060086DD RID: 34525 RVA: 0x002F025A File Offset: 0x002EE45A
		public StatesInstance(PrefersColder master)
			: base(master)
		{
		}
	}

	// Token: 0x02001626 RID: 5670
	public class States : GameStateMachine<PrefersColder.States, PrefersColder.StatesInstance, PrefersColder>
	{
		// Token: 0x060086DE RID: 34526 RVA: 0x002F0263 File Offset: 0x002EE463
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleAttributeModifier(DUPLICANTS.TRAITS.NEEDS.PREFERSCOOLER.NAME, (PrefersColder.StatesInstance smi) => this.modifier, null);
		}

		// Token: 0x04006923 RID: 26915
		private AttributeModifier modifier = new AttributeModifier("ThermalConductivityBarrier", 0.005f, DUPLICANTS.TRAITS.NEEDS.PREFERSCOOLER.NAME, false, false, true);
	}
}
