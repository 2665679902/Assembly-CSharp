using System;
using Klei;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020009CD RID: 2509
public class WarmBlooded : StateMachineComponent<WarmBlooded.StatesInstance>
{
	// Token: 0x06004A90 RID: 19088 RVA: 0x001A1A18 File Offset: 0x0019FC18
	protected override void OnPrefabInit()
	{
		this.externalTemperature = Db.Get().Amounts.ExternalTemperature.Lookup(base.gameObject);
		this.externalTemperature.value = Grid.Temperature[Grid.PosToCell(this)];
		this.temperature = Db.Get().Amounts.Temperature.Lookup(base.gameObject);
		this.primaryElement = base.GetComponent<PrimaryElement>();
	}

	// Token: 0x06004A91 RID: 19089 RVA: 0x001A1A8C File Offset: 0x0019FC8C
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06004A92 RID: 19090 RVA: 0x001A1A99 File Offset: 0x0019FC99
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06004A93 RID: 19091 RVA: 0x001A1AA1 File Offset: 0x0019FCA1
	public bool IsAtReasonableTemperature()
	{
		return !base.smi.IsHot() && !base.smi.IsCold();
	}

	// Token: 0x06004A94 RID: 19092 RVA: 0x001A1AC0 File Offset: 0x0019FCC0
	public void SetTemperatureImmediate(float t)
	{
		this.temperature.value = t;
	}

	// Token: 0x040030FA RID: 12538
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x040030FB RID: 12539
	private AmountInstance externalTemperature;

	// Token: 0x040030FC RID: 12540
	public AmountInstance temperature;

	// Token: 0x040030FD RID: 12541
	private PrimaryElement primaryElement;

	// Token: 0x040030FE RID: 12542
	public const float TRANSITION_DELAY_HOT = 3f;

	// Token: 0x040030FF RID: 12543
	public const float TRANSITION_DELAY_COLD = 3f;

	// Token: 0x020017CC RID: 6092
	public class StatesInstance : GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.GameInstance
	{
		// Token: 0x06008BF2 RID: 35826 RVA: 0x00300ADC File Offset: 0x002FECDC
		public StatesInstance(WarmBlooded smi)
			: base(smi)
		{
			this.baseTemperatureModification = new AttributeModifier("TemperatureDelta", 0f, DUPLICANTS.MODIFIERS.BASEDUPLICANT.NAME, false, true, false);
			this.bodyRegulator = new AttributeModifier("TemperatureDelta", 0f, DUPLICANTS.MODIFIERS.HOMEOSTASIS.NAME, false, true, false);
			this.burningCalories = new AttributeModifier("CaloriesDelta", 0f, DUPLICANTS.MODIFIERS.BURNINGCALORIES.NAME, false, false, false);
			base.master.GetAttributes().Add(this.bodyRegulator);
			base.master.GetAttributes().Add(this.burningCalories);
			base.master.GetAttributes().Add(this.baseTemperatureModification);
			base.master.SetTemperatureImmediate(310.15f);
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06008BF3 RID: 35827 RVA: 0x00300BA8 File Offset: 0x002FEDA8
		public float TemperatureDelta
		{
			get
			{
				return this.bodyRegulator.Value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06008BF4 RID: 35828 RVA: 0x00300BB5 File Offset: 0x002FEDB5
		public float BodyTemperature
		{
			get
			{
				return base.master.primaryElement.Temperature;
			}
		}

		// Token: 0x06008BF5 RID: 35829 RVA: 0x00300BC7 File Offset: 0x002FEDC7
		public bool IsHot()
		{
			return this.BodyTemperature > 310.15f;
		}

		// Token: 0x06008BF6 RID: 35830 RVA: 0x00300BD6 File Offset: 0x002FEDD6
		public bool IsCold()
		{
			return this.BodyTemperature < 310.15f;
		}

		// Token: 0x04006E0F RID: 28175
		public AttributeModifier baseTemperatureModification;

		// Token: 0x04006E10 RID: 28176
		public AttributeModifier bodyRegulator;

		// Token: 0x04006E11 RID: 28177
		public AttributeModifier averageBodyRegulation;

		// Token: 0x04006E12 RID: 28178
		public AttributeModifier burningCalories;

		// Token: 0x04006E13 RID: 28179
		public float averageInternalTemperature;
	}

	// Token: 0x020017CD RID: 6093
	public class States : GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded>
	{
		// Token: 0x06008BF7 RID: 35831 RVA: 0x00300BE8 File Offset: 0x002FEDE8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.alive.normal;
			this.root.TagTransition(GameTags.Dead, this.dead, false).Enter(delegate(WarmBlooded.StatesInstance smi)
			{
				PrimaryElement component = smi.master.GetComponent<PrimaryElement>();
				float num = SimUtil.EnergyFlowToTemperatureDelta(0.08368001f, component.Element.specificHeatCapacity, component.Mass);
				smi.baseTemperatureModification.SetValue(num);
				CreatureSimTemperatureTransfer component2 = smi.master.GetComponent<CreatureSimTemperatureTransfer>();
				component2.NonSimTemperatureModifiers.Add(smi.baseTemperatureModification);
				component2.NonSimTemperatureModifiers.Add(smi.bodyRegulator);
			});
			this.alive.normal.Transition(this.alive.cold.transition, (WarmBlooded.StatesInstance smi) => smi.IsCold(), UpdateRate.SIM_200ms).Transition(this.alive.hot.transition, (WarmBlooded.StatesInstance smi) => smi.IsHot(), UpdateRate.SIM_200ms);
			this.alive.cold.transition.ScheduleGoTo(3f, this.alive.cold.regulating).Transition(this.alive.normal, (WarmBlooded.StatesInstance smi) => !smi.IsCold(), UpdateRate.SIM_200ms);
			this.alive.cold.regulating.Transition(this.alive.normal, (WarmBlooded.StatesInstance smi) => !smi.IsCold(), UpdateRate.SIM_200ms).Update("ColdRegulating", delegate(WarmBlooded.StatesInstance smi, float dt)
			{
				PrimaryElement component3 = smi.master.GetComponent<PrimaryElement>();
				float num2 = SimUtil.EnergyFlowToTemperatureDelta(0.08368001f, component3.Element.specificHeatCapacity, component3.Mass);
				float num3 = SimUtil.EnergyFlowToTemperatureDelta(0.5578667f, component3.Element.specificHeatCapacity, component3.Mass);
				float num4 = 310.15f - smi.BodyTemperature;
				float num5 = 1f;
				if (num3 + num2 > num4)
				{
					num5 = Mathf.Max(0f, num4 - num2) / num3;
				}
				smi.bodyRegulator.SetValue(num3 * num5);
				smi.burningCalories.SetValue(-0.5578667f * num5 * 1000f / 4184f);
			}, UpdateRate.SIM_200ms, false).Exit(delegate(WarmBlooded.StatesInstance smi)
			{
				smi.bodyRegulator.SetValue(0f);
				smi.burningCalories.SetValue(0f);
			});
			this.alive.hot.transition.ScheduleGoTo(3f, this.alive.hot.regulating).Transition(this.alive.normal, (WarmBlooded.StatesInstance smi) => !smi.IsHot(), UpdateRate.SIM_200ms);
			this.alive.hot.regulating.Transition(this.alive.normal, (WarmBlooded.StatesInstance smi) => !smi.IsHot(), UpdateRate.SIM_200ms).Update("WarmRegulating", delegate(WarmBlooded.StatesInstance smi, float dt)
			{
				PrimaryElement component4 = smi.master.GetComponent<PrimaryElement>();
				float num6 = SimUtil.EnergyFlowToTemperatureDelta(0.5578667f, component4.Element.specificHeatCapacity, component4.Mass);
				float num7 = 310.15f - smi.BodyTemperature;
				float num8 = 1f;
				if ((num6 - smi.baseTemperatureModification.Value) * dt < num7)
				{
					num8 = Mathf.Clamp(num7 / ((num6 - smi.baseTemperatureModification.Value) * dt), 0f, 1f);
				}
				smi.bodyRegulator.SetValue(-num6 * num8);
				smi.burningCalories.SetValue(-0.5578667f * num8 / 4184f);
			}, UpdateRate.SIM_200ms, false).Exit(delegate(WarmBlooded.StatesInstance smi)
			{
				smi.bodyRegulator.SetValue(0f);
			});
			this.dead.Enter(delegate(WarmBlooded.StatesInstance smi)
			{
				smi.master.enabled = false;
			});
		}

		// Token: 0x04006E14 RID: 28180
		public WarmBlooded.States.AliveState alive;

		// Token: 0x04006E15 RID: 28181
		public GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State dead;

		// Token: 0x020020D9 RID: 8409
		public class RegulatingState : GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State
		{
			// Token: 0x0400924E RID: 37454
			public GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State transition;

			// Token: 0x0400924F RID: 37455
			public GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State regulating;
		}

		// Token: 0x020020DA RID: 8410
		public class AliveState : GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State
		{
			// Token: 0x04009250 RID: 37456
			public GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State normal;

			// Token: 0x04009251 RID: 37457
			public WarmBlooded.States.RegulatingState cold;

			// Token: 0x04009252 RID: 37458
			public WarmBlooded.States.RegulatingState hot;
		}
	}
}
