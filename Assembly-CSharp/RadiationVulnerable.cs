using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006E6 RID: 1766
public class RadiationVulnerable : GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>
{
	// Token: 0x0600300E RID: 12302 RVA: 0x000FE098 File Offset: 0x000FC298
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.comfortable;
		this.comfortable.Transition(this.too_dark, (RadiationVulnerable.StatesInstance smi) => smi.GetRadiationThresholdCrossed() == -1, UpdateRate.SIM_1000ms).Transition(this.too_bright, (RadiationVulnerable.StatesInstance smi) => smi.GetRadiationThresholdCrossed() == 1, UpdateRate.SIM_1000ms).TriggerOnEnter(GameHashes.RadiationComfort, null);
		this.too_dark.Transition(this.comfortable, (RadiationVulnerable.StatesInstance smi) => smi.GetRadiationThresholdCrossed() != -1, UpdateRate.SIM_1000ms).TriggerOnEnter(GameHashes.RadiationDiscomfort, null);
		this.too_bright.Transition(this.comfortable, (RadiationVulnerable.StatesInstance smi) => smi.GetRadiationThresholdCrossed() != 1, UpdateRate.SIM_1000ms).TriggerOnEnter(GameHashes.RadiationDiscomfort, null);
	}

	// Token: 0x04001CFF RID: 7423
	public GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>.State comfortable;

	// Token: 0x04001D00 RID: 7424
	public GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>.State too_dark;

	// Token: 0x04001D01 RID: 7425
	public GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>.State too_bright;

	// Token: 0x020013F5 RID: 5109
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06007FAD RID: 32685 RVA: 0x002DD7B4 File Offset: 0x002DB9B4
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			Modifiers component = go.GetComponent<Modifiers>();
			float preModifiedAttributeValue = component.GetPreModifiedAttributeValue(Db.Get().PlantAttributes.MinRadiationThreshold);
			string preModifiedAttributeFormattedValue = component.GetPreModifiedAttributeFormattedValue(Db.Get().PlantAttributes.MinRadiationThreshold);
			string preModifiedAttributeFormattedValue2 = component.GetPreModifiedAttributeFormattedValue(Db.Get().PlantAttributes.MaxRadiationThreshold);
			MutantPlant component2 = go.GetComponent<MutantPlant>();
			bool flag = component2 != null && component2.IsOriginal;
			if (preModifiedAttributeValue <= 0f)
			{
				return new List<Descriptor>
				{
					new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_NO_MIN_RADIATION.Replace("{MaxRads}", preModifiedAttributeFormattedValue2), UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_NO_MIN_RADIATION.Replace("{MaxRads}", preModifiedAttributeFormattedValue2) + (flag ? UI.GAMEOBJECTEFFECTS.TOOLTIPS.MUTANT_SEED_TOOLTIP.ToString() : ""), Descriptor.DescriptorType.Requirement, false)
				};
			}
			return new List<Descriptor>
			{
				new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_RADIATION.Replace("{MinRads}", preModifiedAttributeFormattedValue).Replace("{MaxRads}", preModifiedAttributeFormattedValue2), UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_RADIATION.Replace("{MinRads}", preModifiedAttributeFormattedValue).Replace("{MaxRads}", preModifiedAttributeFormattedValue2) + (flag ? UI.GAMEOBJECTEFFECTS.TOOLTIPS.MUTANT_SEED_TOOLTIP.ToString() : ""), Descriptor.DescriptorType.Requirement, false)
			};
		}
	}

	// Token: 0x020013F6 RID: 5110
	public class StatesInstance : GameStateMachine<RadiationVulnerable, RadiationVulnerable.StatesInstance, IStateMachineTarget, RadiationVulnerable.Def>.GameInstance, IWiltCause
	{
		// Token: 0x06007FAF RID: 32687 RVA: 0x002DD8E4 File Offset: 0x002DBAE4
		public StatesInstance(IStateMachineTarget master, RadiationVulnerable.Def def)
			: base(master, def)
		{
			this.minRadiationAttributeInstance = Db.Get().PlantAttributes.MinRadiationThreshold.Lookup(base.gameObject);
			this.maxRadiationAttributeInstance = Db.Get().PlantAttributes.MaxRadiationThreshold.Lookup(base.gameObject);
		}

		// Token: 0x06007FB0 RID: 32688 RVA: 0x002DD93C File Offset: 0x002DBB3C
		public int GetRadiationThresholdCrossed()
		{
			int num = Grid.PosToCell(base.master.gameObject);
			if (!Grid.IsValidCell(num))
			{
				return 0;
			}
			if (Grid.Radiation[num] < this.minRadiationAttributeInstance.GetTotalValue())
			{
				return -1;
			}
			if (Grid.Radiation[num] <= this.maxRadiationAttributeInstance.GetTotalValue())
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06007FB1 RID: 32689 RVA: 0x002DD999 File Offset: 0x002DBB99
		public WiltCondition.Condition[] Conditions
		{
			get
			{
				return new WiltCondition.Condition[] { WiltCondition.Condition.Radiation };
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06007FB2 RID: 32690 RVA: 0x002DD9A8 File Offset: 0x002DBBA8
		public string WiltStateString
		{
			get
			{
				if (base.smi.IsInsideState(base.smi.sm.too_dark))
				{
					return Db.Get().CreatureStatusItems.Crop_Too_NonRadiated.GetName(this);
				}
				if (base.smi.IsInsideState(base.smi.sm.too_bright))
				{
					return Db.Get().CreatureStatusItems.Crop_Too_Radiated.GetName(this);
				}
				return "";
			}
		}

		// Token: 0x04006221 RID: 25121
		private AttributeInstance minRadiationAttributeInstance;

		// Token: 0x04006222 RID: 25122
		private AttributeInstance maxRadiationAttributeInstance;
	}
}
