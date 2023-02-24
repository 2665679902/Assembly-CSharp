using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020008AE RID: 2222
[SkipSaveFileSerialization]
public class RadiationEater : StateMachineComponent<RadiationEater.StatesInstance>
{
	// Token: 0x06003FEC RID: 16364 RVA: 0x001653F0 File Offset: 0x001635F0
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x02001681 RID: 5761
	public class StatesInstance : GameStateMachine<RadiationEater.States, RadiationEater.StatesInstance, RadiationEater, object>.GameInstance
	{
		// Token: 0x060087DD RID: 34781 RVA: 0x002F4086 File Offset: 0x002F2286
		public StatesInstance(RadiationEater master)
			: base(master)
		{
			this.radiationEating = new AttributeModifier(Db.Get().Attributes.RadiationRecovery.Id, TRAITS.RADIATION_EATER_RECOVERY, DUPLICANTS.TRAITS.RADIATIONEATER.NAME, false, false, true);
		}

		// Token: 0x060087DE RID: 34782 RVA: 0x002F40C0 File Offset: 0x002F22C0
		public void OnEatRads(float radsEaten)
		{
			float num = Mathf.Abs(radsEaten) * TRAITS.RADS_TO_CALS;
			base.smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.Calories).ApplyDelta(num);
		}

		// Token: 0x04006A07 RID: 27143
		public AttributeModifier radiationEating;
	}

	// Token: 0x02001682 RID: 5762
	public class States : GameStateMachine<RadiationEater.States, RadiationEater.StatesInstance, RadiationEater>
	{
		// Token: 0x060087DF RID: 34783 RVA: 0x002F410C File Offset: 0x002F230C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.ToggleAttributeModifier("Radiation Eating", (RadiationEater.StatesInstance smi) => smi.radiationEating, null).EventHandler(GameHashes.RadiationRecovery, delegate(RadiationEater.StatesInstance smi, object data)
			{
				float num = (float)data;
				smi.OnEatRads(num);
			});
		}
	}
}
