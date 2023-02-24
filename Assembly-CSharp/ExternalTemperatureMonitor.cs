using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200082B RID: 2091
public class ExternalTemperatureMonitor : GameStateMachine<ExternalTemperatureMonitor, ExternalTemperatureMonitor.Instance>
{
	// Token: 0x06003C7C RID: 15484 RVA: 0x00151154 File Offset: 0x0014F354
	public static float GetExternalColdThreshold(Attributes affected_attributes)
	{
		if (affected_attributes == null)
		{
			return -0.36261335f;
		}
		return -(0.36261335f - affected_attributes.GetValue(Db.Get().Attributes.RoomTemperaturePreference.Id));
	}

	// Token: 0x06003C7D RID: 15485 RVA: 0x00151180 File Offset: 0x0014F380
	public static float GetExternalWarmThreshold(Attributes affected_attributes)
	{
		if (affected_attributes == null)
		{
			return 0.19525334f;
		}
		return -(-0.19525334f - affected_attributes.GetValue(Db.Get().Attributes.RoomTemperaturePreference.Id));
	}

	// Token: 0x06003C7E RID: 15486 RVA: 0x001511AC File Offset: 0x0014F3AC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.comfortable;
		this.root.Enter(delegate(ExternalTemperatureMonitor.Instance smi)
		{
			smi.AverageExternalTemperature = smi.GetCurrentExternalTemperature;
		}).Update(delegate(ExternalTemperatureMonitor.Instance smi, float dt)
		{
			smi.AverageExternalTemperature *= Mathf.Max(0f, 1f - dt / 6f);
			smi.AverageExternalTemperature += smi.GetCurrentExternalTemperature * (dt / 6f);
		}, UpdateRate.SIM_200ms, false);
		this.comfortable.Transition(this.transitionToTooWarm, (ExternalTemperatureMonitor.Instance smi) => smi.IsTooHot() && smi.timeinstate > 6f, UpdateRate.SIM_200ms).Transition(this.transitionToTooCool, (ExternalTemperatureMonitor.Instance smi) => smi.IsTooCold() && smi.timeinstate > 6f, UpdateRate.SIM_200ms);
		this.transitionToTooWarm.Transition(this.comfortable, (ExternalTemperatureMonitor.Instance smi) => !smi.IsTooHot(), UpdateRate.SIM_200ms).Transition(this.tooWarm, (ExternalTemperatureMonitor.Instance smi) => smi.IsTooHot() && smi.timeinstate > 1f, UpdateRate.SIM_200ms);
		this.transitionToTooCool.Transition(this.comfortable, (ExternalTemperatureMonitor.Instance smi) => !smi.IsTooCold(), UpdateRate.SIM_200ms).Transition(this.tooCool, (ExternalTemperatureMonitor.Instance smi) => smi.IsTooCold() && smi.timeinstate > 1f, UpdateRate.SIM_200ms);
		this.transitionToScalding.Transition(this.tooWarm, (ExternalTemperatureMonitor.Instance smi) => !smi.IsScalding(), UpdateRate.SIM_200ms).Transition(this.scalding, (ExternalTemperatureMonitor.Instance smi) => smi.IsScalding() && smi.timeinstate > 1f, UpdateRate.SIM_200ms);
		this.tooWarm.Transition(this.comfortable, (ExternalTemperatureMonitor.Instance smi) => !smi.IsTooHot() && smi.timeinstate > 6f, UpdateRate.SIM_200ms).Transition(this.transitionToScalding, (ExternalTemperatureMonitor.Instance smi) => smi.IsScalding(), UpdateRate.SIM_200ms).ToggleExpression(Db.Get().Expressions.Hot, null)
			.ToggleThought(Db.Get().Thoughts.Hot, null)
			.ToggleStatusItem(Db.Get().DuplicantStatusItems.Hot, (ExternalTemperatureMonitor.Instance smi) => smi)
			.ToggleEffect("WarmAir")
			.Enter(delegate(ExternalTemperatureMonitor.Instance smi)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_ThermalComfort, true);
			});
		this.scalding.Transition(this.tooWarm, (ExternalTemperatureMonitor.Instance smi) => !smi.IsScalding() && smi.timeinstate > 6f, UpdateRate.SIM_200ms).ToggleExpression(Db.Get().Expressions.Hot, null).ToggleThought(Db.Get().Thoughts.Hot, null)
			.ToggleStatusItem(Db.Get().CreatureStatusItems.Scalding, (ExternalTemperatureMonitor.Instance smi) => smi)
			.Update("ScaldDamage", delegate(ExternalTemperatureMonitor.Instance smi, float dt)
			{
				smi.ScaldDamage(dt);
			}, UpdateRate.SIM_1000ms, false);
		this.tooCool.Transition(this.comfortable, (ExternalTemperatureMonitor.Instance smi) => !smi.IsTooCold() && smi.timeinstate > 6f, UpdateRate.SIM_200ms).ToggleExpression(Db.Get().Expressions.Cold, null).ToggleThought(Db.Get().Thoughts.Cold, null)
			.ToggleStatusItem(Db.Get().DuplicantStatusItems.Cold, (ExternalTemperatureMonitor.Instance smi) => smi)
			.ToggleEffect("ColdAir")
			.Enter(delegate(ExternalTemperatureMonitor.Instance smi)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_ThermalComfort, true);
			});
	}

	// Token: 0x04002765 RID: 10085
	public GameStateMachine<ExternalTemperatureMonitor, ExternalTemperatureMonitor.Instance, IStateMachineTarget, object>.State comfortable;

	// Token: 0x04002766 RID: 10086
	public GameStateMachine<ExternalTemperatureMonitor, ExternalTemperatureMonitor.Instance, IStateMachineTarget, object>.State transitionToTooWarm;

	// Token: 0x04002767 RID: 10087
	public GameStateMachine<ExternalTemperatureMonitor, ExternalTemperatureMonitor.Instance, IStateMachineTarget, object>.State tooWarm;

	// Token: 0x04002768 RID: 10088
	public GameStateMachine<ExternalTemperatureMonitor, ExternalTemperatureMonitor.Instance, IStateMachineTarget, object>.State transitionToTooCool;

	// Token: 0x04002769 RID: 10089
	public GameStateMachine<ExternalTemperatureMonitor, ExternalTemperatureMonitor.Instance, IStateMachineTarget, object>.State tooCool;

	// Token: 0x0400276A RID: 10090
	public GameStateMachine<ExternalTemperatureMonitor, ExternalTemperatureMonitor.Instance, IStateMachineTarget, object>.State transitionToScalding;

	// Token: 0x0400276B RID: 10091
	public GameStateMachine<ExternalTemperatureMonitor, ExternalTemperatureMonitor.Instance, IStateMachineTarget, object>.State scalding;

	// Token: 0x0400276C RID: 10092
	private const float SCALDING_DAMAGE_AMOUNT = 10f;

	// Token: 0x0400276D RID: 10093
	private const float BODY_TEMPERATURE_AFFECT_EXTERNAL_FEEL_THRESHOLD = 0.5f;

	// Token: 0x0400276E RID: 10094
	public const float BASE_STRESS_TOLERANCE_COLD = 0.27893335f;

	// Token: 0x0400276F RID: 10095
	public const float BASE_STRESS_TOLERANCE_WARM = 0.27893335f;

	// Token: 0x04002770 RID: 10096
	private const float START_GAME_AVERAGING_DELAY = 6f;

	// Token: 0x04002771 RID: 10097
	private const float TRANSITION_TO_DELAY = 1f;

	// Token: 0x04002772 RID: 10098
	private const float TRANSITION_OUT_DELAY = 6f;

	// Token: 0x04002773 RID: 10099
	private const float TEMPERATURE_AVERAGING_RANGE = 6f;

	// Token: 0x020015A5 RID: 5541
	public new class Instance : GameStateMachine<ExternalTemperatureMonitor, ExternalTemperatureMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x060084A1 RID: 33953 RVA: 0x002EA584 File Offset: 0x002E8784
		public float GetCurrentExternalTemperature
		{
			get
			{
				int num = Grid.PosToCell(base.gameObject);
				if (this.occupyArea != null)
				{
					float num2 = 0f;
					int num3 = 0;
					for (int i = 0; i < this.occupyArea.OccupiedCellsOffsets.Length; i++)
					{
						int num4 = Grid.OffsetCell(num, this.occupyArea.OccupiedCellsOffsets[i]);
						if (Grid.IsValidCell(num4))
						{
							num3++;
							num2 += Grid.Temperature[num4];
						}
					}
					return num2 / (float)Mathf.Max(1, num3);
				}
				return Grid.Temperature[num];
			}
		}

		// Token: 0x060084A2 RID: 33954 RVA: 0x002EA61A File Offset: 0x002E881A
		public override void StartSM()
		{
			base.StartSM();
			base.smi.attributes.Get(Db.Get().Attributes.ScaldingThreshold).Add(this.baseScalindingThreshold);
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060084A3 RID: 33955 RVA: 0x002EA64C File Offset: 0x002E884C
		public float GetCurrentColdThreshold
		{
			get
			{
				if (this.internalTemperatureMonitor.IdealTemperatureDelta() > 0.5f)
				{
					return 0f;
				}
				return CreatureSimTemperatureTransfer.PotentialEnergyFlowToCreature(Grid.PosToCell(base.gameObject), this.primaryElement, this.temperatureTransferer, 1f);
			}
		}

		// Token: 0x060084A4 RID: 33956 RVA: 0x002EA687 File Offset: 0x002E8887
		public float GetScaldingThreshold()
		{
			return base.smi.attributes.GetValue("ScaldingThreshold");
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060084A5 RID: 33957 RVA: 0x002EA69E File Offset: 0x002E889E
		public float GetCurrentHotThreshold
		{
			get
			{
				return this.HotThreshold;
			}
		}

		// Token: 0x060084A6 RID: 33958 RVA: 0x002EA6A8 File Offset: 0x002E88A8
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.health = base.GetComponent<Health>();
			this.occupyArea = base.GetComponent<OccupyArea>();
			this.internalTemperatureMonitor = base.gameObject.GetSMI<TemperatureMonitor.Instance>();
			this.internalTemperature = Db.Get().Amounts.Temperature.Lookup(base.gameObject);
			this.temperatureTransferer = base.gameObject.GetComponent<CreatureSimTemperatureTransfer>();
			this.primaryElement = base.gameObject.GetComponent<PrimaryElement>();
			this.attributes = base.gameObject.GetAttributes();
		}

		// Token: 0x060084A7 RID: 33959 RVA: 0x002EA770 File Offset: 0x002E8970
		public bool IsTooHot()
		{
			return this.internalTemperatureMonitor.IdealTemperatureDelta() >= -0.5f && base.smi.temperatureTransferer.average_kilowatts_exchanged.GetWeightedAverage > ExternalTemperatureMonitor.GetExternalWarmThreshold(base.smi.attributes);
		}

		// Token: 0x060084A8 RID: 33960 RVA: 0x002EA7B0 File Offset: 0x002E89B0
		public bool IsTooCold()
		{
			return this.internalTemperatureMonitor.IdealTemperatureDelta() <= 0.5f && base.smi.temperatureTransferer.average_kilowatts_exchanged.GetWeightedAverage < ExternalTemperatureMonitor.GetExternalColdThreshold(base.smi.attributes);
		}

		// Token: 0x060084A9 RID: 33961 RVA: 0x002EA7F0 File Offset: 0x002E89F0
		public bool IsScalding()
		{
			return this.AverageExternalTemperature > base.smi.attributes.GetValue("ScaldingThreshold");
		}

		// Token: 0x060084AA RID: 33962 RVA: 0x002EA80F File Offset: 0x002E8A0F
		public void ScaldDamage(float dt)
		{
			if (this.health != null && Time.time - this.lastScaldTime > 5f)
			{
				this.lastScaldTime = Time.time;
				this.health.Damage(dt * 10f);
			}
		}

		// Token: 0x060084AB RID: 33963 RVA: 0x002EA84F File Offset: 0x002E8A4F
		public float CurrentWorldTransferWattage()
		{
			return this.temperatureTransferer.currentExchangeWattage;
		}

		// Token: 0x0400674F RID: 26447
		public float AverageExternalTemperature;

		// Token: 0x04006750 RID: 26448
		public float ColdThreshold = 283.15f;

		// Token: 0x04006751 RID: 26449
		public float HotThreshold = 306.15f;

		// Token: 0x04006752 RID: 26450
		private AttributeModifier baseScalindingThreshold = new AttributeModifier("ScaldingThreshold", 345f, DUPLICANTS.STATS.SKIN_DURABILITY.NAME, false, false, true);

		// Token: 0x04006753 RID: 26451
		public Attributes attributes;

		// Token: 0x04006754 RID: 26452
		public OccupyArea occupyArea;

		// Token: 0x04006755 RID: 26453
		public AmountInstance internalTemperature;

		// Token: 0x04006756 RID: 26454
		private TemperatureMonitor.Instance internalTemperatureMonitor;

		// Token: 0x04006757 RID: 26455
		public CreatureSimTemperatureTransfer temperatureTransferer;

		// Token: 0x04006758 RID: 26456
		public Health health;

		// Token: 0x04006759 RID: 26457
		public PrimaryElement primaryElement;

		// Token: 0x0400675A RID: 26458
		private const float MIN_SCALD_INTERVAL = 5f;

		// Token: 0x0400675B RID: 26459
		private float lastScaldTime;
	}
}
