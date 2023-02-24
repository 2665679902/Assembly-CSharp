using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000632 RID: 1586
public class RefrigeratorController : GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>
{
	// Token: 0x060029D7 RID: 10711 RVA: 0x000DCC08 File Offset: 0x000DAE08
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational, new StateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.Transition.ConditionCallback(this.IsOperational));
		this.operational.DefaultState(this.operational.steady).EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.Not(new StateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.Transition.ConditionCallback(this.IsOperational))).Enter(delegate(RefrigeratorController.StatesInstance smi)
		{
			smi.operational.SetActive(true, false);
		})
			.Exit(delegate(RefrigeratorController.StatesInstance smi)
			{
				smi.operational.SetActive(false, false);
			});
		this.operational.cooling.Update("Cooling exhaust", delegate(RefrigeratorController.StatesInstance smi, float dt)
		{
			smi.ApplyCoolingExhaust(dt);
		}, UpdateRate.SIM_200ms, true).UpdateTransition(this.operational.steady, new Func<RefrigeratorController.StatesInstance, float, bool>(this.AllFoodCool), UpdateRate.SIM_4000ms, true).ToggleStatusItem(Db.Get().BuildingStatusItems.FridgeCooling, (RefrigeratorController.StatesInstance smi) => smi, Db.Get().StatusItemCategories.Main);
		this.operational.steady.Update("Cooling exhaust", delegate(RefrigeratorController.StatesInstance smi, float dt)
		{
			smi.ApplySteadyExhaust(dt);
		}, UpdateRate.SIM_200ms, true).UpdateTransition(this.operational.cooling, new Func<RefrigeratorController.StatesInstance, float, bool>(this.AnyWarmFood), UpdateRate.SIM_4000ms, true).ToggleStatusItem(Db.Get().BuildingStatusItems.FridgeSteady, (RefrigeratorController.StatesInstance smi) => smi, Db.Get().StatusItemCategories.Main)
			.Enter(delegate(RefrigeratorController.StatesInstance smi)
			{
				smi.SetEnergySaver(true);
			})
			.Exit(delegate(RefrigeratorController.StatesInstance smi)
			{
				smi.SetEnergySaver(false);
			});
	}

	// Token: 0x060029D8 RID: 10712 RVA: 0x000DCE38 File Offset: 0x000DB038
	private bool AllFoodCool(RefrigeratorController.StatesInstance smi, float dt)
	{
		foreach (GameObject gameObject in smi.storage.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null) && component.Mass >= 0.01f && component.Temperature >= smi.def.simulatedInternalTemperature + smi.def.activeCoolingStopBuffer)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060029D9 RID: 10713 RVA: 0x000DCED8 File Offset: 0x000DB0D8
	private bool AnyWarmFood(RefrigeratorController.StatesInstance smi, float dt)
	{
		foreach (GameObject gameObject in smi.storage.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (!(component == null) && component.Mass >= 0.01f && component.Temperature >= smi.def.simulatedInternalTemperature + smi.def.activeCoolingStartBuffer)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060029DA RID: 10714 RVA: 0x000DCF78 File Offset: 0x000DB178
	private bool IsOperational(RefrigeratorController.StatesInstance smi)
	{
		return smi.operational.IsOperational;
	}

	// Token: 0x040018CA RID: 6346
	public GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.State inoperational;

	// Token: 0x040018CB RID: 6347
	public RefrigeratorController.OperationalStates operational;

	// Token: 0x020012BF RID: 4799
	public class Def : StateMachine.BaseDef, IGameObjectEffectDescriptor
	{
		// Token: 0x06007B65 RID: 31589 RVA: 0x002CBC94 File Offset: 0x002C9E94
		public List<Descriptor> GetDescriptors(GameObject go)
		{
			List<Descriptor> list = new List<Descriptor>();
			list.AddRange(SimulatedTemperatureAdjuster.GetDescriptors(this.simulatedInternalTemperature));
			Descriptor descriptor = default(Descriptor);
			string formattedHeatEnergy = GameUtil.GetFormattedHeatEnergy(this.coolingHeatKW * 1000f, GameUtil.HeatEnergyFormatterUnit.Automatic);
			descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.HEATGENERATED, formattedHeatEnergy), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.HEATGENERATED, formattedHeatEnergy), Descriptor.DescriptorType.Effect);
			list.Add(descriptor);
			return list;
		}

		// Token: 0x04005E98 RID: 24216
		public float activeCoolingStartBuffer = 2f;

		// Token: 0x04005E99 RID: 24217
		public float activeCoolingStopBuffer = 0.1f;

		// Token: 0x04005E9A RID: 24218
		public float simulatedInternalTemperature = 274.15f;

		// Token: 0x04005E9B RID: 24219
		public float simulatedInternalHeatCapacity = 400f;

		// Token: 0x04005E9C RID: 24220
		public float simulatedThermalConductivity = 1000f;

		// Token: 0x04005E9D RID: 24221
		public float powerSaverEnergyUsage;

		// Token: 0x04005E9E RID: 24222
		public float coolingHeatKW;

		// Token: 0x04005E9F RID: 24223
		public float steadyHeatKW;
	}

	// Token: 0x020012C0 RID: 4800
	public class OperationalStates : GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.State
	{
		// Token: 0x04005EA0 RID: 24224
		public GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.State cooling;

		// Token: 0x04005EA1 RID: 24225
		public GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.State steady;
	}

	// Token: 0x020012C1 RID: 4801
	public class StatesInstance : GameStateMachine<RefrigeratorController, RefrigeratorController.StatesInstance, IStateMachineTarget, RefrigeratorController.Def>.GameInstance
	{
		// Token: 0x06007B68 RID: 31592 RVA: 0x002CBD48 File Offset: 0x002C9F48
		public StatesInstance(IStateMachineTarget master, RefrigeratorController.Def def)
			: base(master, def)
		{
			this.temperatureAdjuster = new SimulatedTemperatureAdjuster(def.simulatedInternalTemperature, def.simulatedInternalHeatCapacity, def.simulatedThermalConductivity, this.storage);
			this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		}

		// Token: 0x06007B69 RID: 31593 RVA: 0x002CBD96 File Offset: 0x002C9F96
		protected override void OnCleanUp()
		{
			this.temperatureAdjuster.CleanUp();
			base.OnCleanUp();
		}

		// Token: 0x06007B6A RID: 31594 RVA: 0x002CBDA9 File Offset: 0x002C9FA9
		public float GetSaverPower()
		{
			return base.def.powerSaverEnergyUsage;
		}

		// Token: 0x06007B6B RID: 31595 RVA: 0x002CBDB6 File Offset: 0x002C9FB6
		public float GetNormalPower()
		{
			return base.GetComponent<EnergyConsumer>().WattsNeededWhenActive;
		}

		// Token: 0x06007B6C RID: 31596 RVA: 0x002CBDC4 File Offset: 0x002C9FC4
		public void SetEnergySaver(bool energySaving)
		{
			EnergyConsumer component = base.GetComponent<EnergyConsumer>();
			if (energySaving)
			{
				component.BaseWattageRating = this.GetSaverPower();
				return;
			}
			component.BaseWattageRating = this.GetNormalPower();
		}

		// Token: 0x06007B6D RID: 31597 RVA: 0x002CBDF4 File Offset: 0x002C9FF4
		public void ApplyCoolingExhaust(float dt)
		{
			GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, base.def.coolingHeatKW * dt, BUILDING.STATUSITEMS.OPERATINGENERGY.FOOD_TRANSFER, dt);
		}

		// Token: 0x06007B6E RID: 31598 RVA: 0x002CBE1E File Offset: 0x002CA01E
		public void ApplySteadyExhaust(float dt)
		{
			GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, base.def.steadyHeatKW * dt, BUILDING.STATUSITEMS.OPERATINGENERGY.FOOD_TRANSFER, dt);
		}

		// Token: 0x04005EA2 RID: 24226
		[MyCmpReq]
		public Operational operational;

		// Token: 0x04005EA3 RID: 24227
		[MyCmpReq]
		public Storage storage;

		// Token: 0x04005EA4 RID: 24228
		private HandleVector<int>.Handle structureTemperature;

		// Token: 0x04005EA5 RID: 24229
		private SimulatedTemperatureAdjuster temperatureAdjuster;
	}
}
