using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200059E RID: 1438
public class ContactConductivePipeBridge : GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>
{
	// Token: 0x06002366 RID: 9062 RVA: 0x000BF1A2 File Offset: 0x000BD3A2
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.noLiquid;
		this.root.PlayAnim("on", KAnim.PlayMode.Loop).Update("", new Action<ContactConductivePipeBridge.Instance, float>(ContactConductivePipeBridge.Flow200ms), UpdateRate.SIM_200ms, false);
	}

	// Token: 0x06002367 RID: 9063 RVA: 0x000BF1D8 File Offset: 0x000BD3D8
	private static void ExpirationTimerUpdate(ContactConductivePipeBridge.Instance smi, float dt)
	{
		float num = smi.sm.noLiquidTimer.Get(smi);
		num -= dt;
		smi.sm.noLiquidTimer.Set(num, smi, false);
	}

	// Token: 0x06002368 RID: 9064 RVA: 0x000BF210 File Offset: 0x000BD410
	private static void Flow200ms(ContactConductivePipeBridge.Instance smi, float dt)
	{
		if (smi.storage != null && smi.storage.items.Count > 0)
		{
			ContactConductivePipeBridge.ExchangeStorageTemperatureWithBuilding200ms(smi, smi.storage, smi.building, smi.tag, dt);
			List<GameObject> items = smi.storage.items;
			for (int i = 0; i < items.Count; i++)
			{
				PrimaryElement component = items[i].GetComponent<PrimaryElement>();
				if (component.Mass > 0f)
				{
					float num = ((smi.def.type == ConduitType.Liquid) ? Game.Instance.liquidConduitFlow : Game.Instance.gasConduitFlow).AddElement(smi.outputCell, component.ElementID, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount);
					component.KeepZeroMassObject = true;
					float num2 = num / component.Mass;
					int num3 = (int)((float)component.DiseaseCount * num2);
					component.Mass -= num;
					component.ModifyDiseaseCount(-num3, "ContactConductivePipeBridge.Flow200ms");
				}
			}
		}
	}

	// Token: 0x06002369 RID: 9065 RVA: 0x000BF328 File Offset: 0x000BD528
	private static void ExchangeStorageTemperatureWithBuilding200ms(ContactConductivePipeBridge.Instance smi, Storage storage, Building building, Tag tag, float dt)
	{
		List<GameObject> items = storage.items;
		for (int i = 0; i < items.Count; i++)
		{
			PrimaryElement component = items[i].GetComponent<PrimaryElement>();
			if (component.Mass > 0f && component.HasTag(tag))
			{
				PrimaryElement primaryElement = component;
				PrimaryElement component2 = building.GetComponent<PrimaryElement>();
				float num = primaryElement.Mass * primaryElement.Element.specificHeatCapacity;
				float num2 = building.Def.MassForTemperatureModification * component2.Element.specificHeatCapacity;
				float temperature = component2.Temperature;
				float temperature2 = primaryElement.Temperature;
				float finalContentTemperature = ContactConductivePipeBridge.GetFinalContentTemperature(ContactConductivePipeBridge.GetKilloJoulesTransfered(ContactConductivePipeBridge.CalculateMaxWattsTransfered(temperature, component2.Element.thermalConductivity, temperature2, primaryElement.Element.thermalConductivity), dt, temperature, num2, temperature2, num), temperature, num2, temperature2, num);
				float finalBuildingTemperature = ContactConductivePipeBridge.GetFinalBuildingTemperature(temperature2, finalContentTemperature, num, temperature, num2);
				if ((finalBuildingTemperature >= 0f && finalBuildingTemperature <= 10000f) & (finalContentTemperature >= 0f && finalContentTemperature <= 10000f))
				{
					primaryElement.Temperature = finalContentTemperature;
					component2.Temperature = finalBuildingTemperature;
				}
			}
		}
	}

	// Token: 0x0600236A RID: 9066 RVA: 0x000BF458 File Offset: 0x000BD658
	private static float CalculateMaxWattsTransfered(float buildingTemperature, float building_thermal_conductivity, float content_temperature, float content_thermal_conductivity)
	{
		float num = 1f;
		float num2 = 1f;
		float num3 = 50f;
		float num4 = content_temperature - buildingTemperature;
		float num5 = (content_thermal_conductivity + building_thermal_conductivity) * 0.5f;
		return num4 * num5 * num * num3 / num2;
	}

	// Token: 0x0600236B RID: 9067 RVA: 0x000BF48C File Offset: 0x000BD68C
	private static float GetKilloJoulesTransfered(float maxWattsTransfered, float dt, float building_Temperature, float building_heat_capacity, float content_temperature, float content_heat_capacity)
	{
		float num = maxWattsTransfered * dt / 1000f;
		float num2 = Mathf.Min(content_temperature, building_Temperature);
		float num3 = Mathf.Max(content_temperature, building_Temperature);
		float num4 = content_temperature - num / content_heat_capacity;
		float num5 = building_Temperature + num / building_heat_capacity;
		float num6 = Mathf.Clamp(num4, num2, num3);
		num5 = Mathf.Clamp(num5, num2, num3);
		float num7 = Mathf.Abs(num6 - content_temperature);
		float num8 = Mathf.Abs(num5 - building_Temperature);
		float num9 = num7 * content_heat_capacity;
		float num10 = num8 * building_heat_capacity;
		return Mathf.Min(num9, num10) * Mathf.Sign(maxWattsTransfered);
	}

	// Token: 0x0600236C RID: 9068 RVA: 0x000BF500 File Offset: 0x000BD700
	private static float GetFinalContentTemperature(float KJT, float building_Temperature, float building_heat_capacity, float content_temperature, float content_heat_capacity)
	{
		float num = -KJT;
		float num2 = Mathf.Max(0f, content_temperature + num / content_heat_capacity);
		float num3 = Mathf.Max(0f, building_Temperature - num / building_heat_capacity);
		if ((content_temperature - building_Temperature) * (num2 - num3) < 0f)
		{
			return content_temperature * content_heat_capacity / (content_heat_capacity + building_heat_capacity) + building_Temperature * building_heat_capacity / (content_heat_capacity + building_heat_capacity);
		}
		return num2;
	}

	// Token: 0x0600236D RID: 9069 RVA: 0x000BF554 File Offset: 0x000BD754
	private static float GetFinalBuildingTemperature(float content_temperature, float content_final_temperature, float content_heat_capacity, float building_temperature, float building_heat_capacity)
	{
		float num = (content_temperature - content_final_temperature) * content_heat_capacity;
		float num2 = Mathf.Min(content_temperature, building_temperature);
		float num3 = Mathf.Max(content_temperature, building_temperature);
		float num4 = num / building_heat_capacity;
		return Mathf.Clamp(building_temperature + num4, num2, num3);
	}

	// Token: 0x0400144F RID: 5199
	private const string loopAnimName = "on";

	// Token: 0x04001450 RID: 5200
	private const string loopAnim_noWater = "off";

	// Token: 0x04001451 RID: 5201
	private GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.State withLiquid;

	// Token: 0x04001452 RID: 5202
	private GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.State noLiquid;

	// Token: 0x04001453 RID: 5203
	private StateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.FloatParameter noLiquidTimer;

	// Token: 0x020011CC RID: 4556
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005C06 RID: 23558
		public ConduitType type = ConduitType.Liquid;

		// Token: 0x04005C07 RID: 23559
		public float pumpKGRate;
	}

	// Token: 0x020011CD RID: 4557
	public new class Instance : GameStateMachine<ContactConductivePipeBridge, ContactConductivePipeBridge.Instance, IStateMachineTarget, ContactConductivePipeBridge.Def>.GameInstance
	{
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06007817 RID: 30743 RVA: 0x002BD360 File Offset: 0x002BB560
		public Tag tag
		{
			get
			{
				if (this.type != ConduitType.Liquid)
				{
					return GameTags.Gas;
				}
				return GameTags.Liquid;
			}
		}

		// Token: 0x06007818 RID: 30744 RVA: 0x002BD376 File Offset: 0x002BB576
		public Instance(IStateMachineTarget master, ContactConductivePipeBridge.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007819 RID: 30745 RVA: 0x002BD38E File Offset: 0x002BB58E
		public override void StartSM()
		{
			base.StartSM();
			this.outputCell = this.building.GetUtilityOutputCell();
			this.structureHandle = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		}

		// Token: 0x0600781A RID: 30746 RVA: 0x002BD3C0 File Offset: 0x002BB5C0
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			this.storage.DropAll(false, false, default(Vector3), true, null);
		}

		// Token: 0x04005C08 RID: 23560
		public ConduitType type = ConduitType.Liquid;

		// Token: 0x04005C09 RID: 23561
		public HandleVector<int>.Handle structureHandle;

		// Token: 0x04005C0A RID: 23562
		public int outputCell = -1;

		// Token: 0x04005C0B RID: 23563
		[MyCmpGet]
		public Storage storage;

		// Token: 0x04005C0C RID: 23564
		[MyCmpGet]
		public Building building;

		// Token: 0x04005C0D RID: 23565
		[MyCmpGet]
		public ConduitDispenser conduitDispenser;
	}
}
