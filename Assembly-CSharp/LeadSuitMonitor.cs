using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020007F2 RID: 2034
public class LeadSuitMonitor : GameStateMachine<LeadSuitMonitor, LeadSuitMonitor.Instance>
{
	// Token: 0x06003AAD RID: 15021 RVA: 0x00144EB8 File Offset: 0x001430B8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.wearingSuit;
		base.Target(this.owner);
		this.wearingSuit.DefaultState(this.wearingSuit.hasBattery);
		this.wearingSuit.hasBattery.Update(new Action<LeadSuitMonitor.Instance, float>(LeadSuitMonitor.CoolSuit), UpdateRate.SIM_200ms, false).TagTransition(GameTags.SuitBatteryOut, this.wearingSuit.noBattery, false);
		this.wearingSuit.noBattery.Enter(delegate(LeadSuitMonitor.Instance smi)
		{
			Attributes attributes = smi.sm.owner.Get(smi).GetAttributes();
			if (attributes != null)
			{
				foreach (AttributeModifier attributeModifier in smi.noBatteryModifiers)
				{
					attributes.Add(attributeModifier);
				}
			}
		}).Exit(delegate(LeadSuitMonitor.Instance smi)
		{
			Attributes attributes2 = smi.sm.owner.Get(smi).GetAttributes();
			if (attributes2 != null)
			{
				foreach (AttributeModifier attributeModifier2 in smi.noBatteryModifiers)
				{
					attributes2.Remove(attributeModifier2);
				}
			}
		}).TagTransition(GameTags.SuitBatteryOut, this.wearingSuit.hasBattery, true);
	}

	// Token: 0x06003AAE RID: 15022 RVA: 0x00144F90 File Offset: 0x00143190
	public static void CoolSuit(LeadSuitMonitor.Instance smi, float dt)
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
		ExternalTemperatureMonitor.Instance smi2 = gameObject.GetSMI<ExternalTemperatureMonitor.Instance>();
		if (smi2 != null && smi2.AverageExternalTemperature >= smi.lead_suit_tank.coolingOperationalTemperature)
		{
			smi.lead_suit_tank.batteryCharge -= 1f / smi.lead_suit_tank.batteryDuration * dt;
			if (smi.lead_suit_tank.IsEmpty())
			{
				gameObject.AddTag(GameTags.SuitBatteryOut);
			}
		}
	}

	// Token: 0x0400267D RID: 9853
	public LeadSuitMonitor.WearingSuit wearingSuit;

	// Token: 0x0400267E RID: 9854
	public StateMachine<LeadSuitMonitor, LeadSuitMonitor.Instance, IStateMachineTarget, object>.TargetParameter owner;

	// Token: 0x02001546 RID: 5446
	public class WearingSuit : GameStateMachine<LeadSuitMonitor, LeadSuitMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400661A RID: 26138
		public GameStateMachine<LeadSuitMonitor, LeadSuitMonitor.Instance, IStateMachineTarget, object>.State hasBattery;

		// Token: 0x0400661B RID: 26139
		public GameStateMachine<LeadSuitMonitor, LeadSuitMonitor.Instance, IStateMachineTarget, object>.State noBattery;
	}

	// Token: 0x02001547 RID: 5447
	public new class Instance : GameStateMachine<LeadSuitMonitor, LeadSuitMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008326 RID: 33574 RVA: 0x002E7748 File Offset: 0x002E5948
		public Instance(IStateMachineTarget master, GameObject owner)
			: base(master)
		{
			base.sm.owner.Set(owner, base.smi, false);
			this.navigator = owner.GetComponent<Navigator>();
			this.lead_suit_tank = master.GetComponent<LeadSuitTank>();
			this.noBatteryModifiers.Add(new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.INSULATION, (float)(-(float)TUNING.EQUIPMENT.SUITS.LEADSUIT_INSULATION), STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.SUIT_OUT_OF_BATTERIES, false, false, true));
			this.noBatteryModifiers.Add(new AttributeModifier(TUNING.EQUIPMENT.ATTRIBUTE_MOD_IDS.THERMAL_CONDUCTIVITY_BARRIER, -TUNING.EQUIPMENT.SUITS.LEADSUIT_THERMAL_CONDUCTIVITY_BARRIER, STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.SUIT_OUT_OF_BATTERIES, false, false, true));
		}

		// Token: 0x0400661C RID: 26140
		public Navigator navigator;

		// Token: 0x0400661D RID: 26141
		public LeadSuitTank lead_suit_tank;

		// Token: 0x0400661E RID: 26142
		public List<AttributeModifier> noBatteryModifiers = new List<AttributeModifier>();
	}
}
