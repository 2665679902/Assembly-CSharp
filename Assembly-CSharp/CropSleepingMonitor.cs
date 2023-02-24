using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006CA RID: 1738
public class CropSleepingMonitor : GameStateMachine<CropSleepingMonitor, CropSleepingMonitor.Instance, IStateMachineTarget, CropSleepingMonitor.Def>
{
	// Token: 0x06002F4D RID: 12109 RVA: 0x000F9F10 File Offset: 0x000F8110
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.awake;
		base.serializable = StateMachine.SerializeType.Never;
		this.root.Update("CropSleepingMonitor.root", delegate(CropSleepingMonitor.Instance smi, float dt)
		{
			int num = Grid.PosToCell(smi.master.gameObject);
			GameStateMachine<CropSleepingMonitor, CropSleepingMonitor.Instance, IStateMachineTarget, CropSleepingMonitor.Def>.State state = (smi.IsCellSafe(num) ? this.awake : this.sleeping);
			smi.GoTo(state);
		}, UpdateRate.SIM_1000ms, false);
		this.sleeping.TriggerOnEnter(GameHashes.CropSleep, null).ToggleStatusItem(Db.Get().CreatureStatusItems.CropSleeping, (CropSleepingMonitor.Instance smi) => smi);
		this.awake.TriggerOnEnter(GameHashes.CropWakeUp, null);
	}

	// Token: 0x04001C6D RID: 7277
	public GameStateMachine<CropSleepingMonitor, CropSleepingMonitor.Instance, IStateMachineTarget, CropSleepingMonitor.Def>.State sleeping;

	// Token: 0x04001C6E RID: 7278
	public GameStateMachine<CropSleepingMonitor, CropSleepingMonitor.Instance, IStateMachineTarget, CropSleepingMonitor.Def>.State awake;

	// Token: 0x020013B1 RID: 5041
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06007EA8 RID: 32424 RVA: 0x002D98C8 File Offset: 0x002D7AC8
		public List<Descriptor> GetDescriptors(GameObject obj)
		{
			if (this.prefersDarkness)
			{
				return new List<Descriptor>
				{
					new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_DARKNESS, UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_DARKNESS, Descriptor.DescriptorType.Requirement, false)
				};
			}
			Klei.AI.Attribute minLightLux = Db.Get().PlantAttributes.MinLightLux;
			AttributeInstance attributeInstance = minLightLux.Lookup(obj);
			int num = Mathf.RoundToInt((attributeInstance != null) ? attributeInstance.GetTotalValue() : obj.GetComponent<Modifiers>().GetPreModifiedAttributeValue(minLightLux));
			return new List<Descriptor>
			{
				new Descriptor(UI.GAMEOBJECTEFFECTS.REQUIRES_LIGHT.Replace("{Lux}", GameUtil.GetFormattedLux(num)), UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_LIGHT.Replace("{Lux}", GameUtil.GetFormattedLux(num)), Descriptor.DescriptorType.Requirement, false)
			};
		}

		// Token: 0x04006159 RID: 24921
		public bool prefersDarkness;
	}

	// Token: 0x020013B2 RID: 5042
	public new class Instance : GameStateMachine<CropSleepingMonitor, CropSleepingMonitor.Instance, IStateMachineTarget, CropSleepingMonitor.Def>.GameInstance
	{
		// Token: 0x06007EAA RID: 32426 RVA: 0x002D997D File Offset: 0x002D7B7D
		public Instance(IStateMachineTarget master, CropSleepingMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007EAB RID: 32427 RVA: 0x002D9987 File Offset: 0x002D7B87
		public bool IsSleeping()
		{
			return this.GetCurrentState() == base.smi.sm.sleeping;
		}

		// Token: 0x06007EAC RID: 32428 RVA: 0x002D99A4 File Offset: 0x002D7BA4
		public bool IsCellSafe(int cell)
		{
			AttributeInstance attributeInstance = Db.Get().PlantAttributes.MinLightLux.Lookup(base.gameObject);
			int num = Grid.LightIntensity[cell];
			if (!base.def.prefersDarkness)
			{
				return (float)num >= attributeInstance.GetTotalValue();
			}
			return num == 0;
		}
	}
}
