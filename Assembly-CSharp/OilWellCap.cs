using System;
using Klei;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000619 RID: 1561
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/OilWellCap")]
public class OilWellCap : Workable, ISingleSliderControl, ISliderControl, IElementEmitter
{
	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x060028D6 RID: 10454 RVA: 0x000D85B2 File Offset: 0x000D67B2
	public SimHashes Element
	{
		get
		{
			return this.gasElement;
		}
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x060028D7 RID: 10455 RVA: 0x000D85BA File Offset: 0x000D67BA
	public float AverageEmitRate
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.accumulator);
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x060028D8 RID: 10456 RVA: 0x000D85D1 File Offset: 0x000D67D1
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.OIL_WELL_CAP_SIDE_SCREEN.TITLE";
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x060028D9 RID: 10457 RVA: 0x000D85D8 File Offset: 0x000D67D8
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.PERCENT;
		}
	}

	// Token: 0x060028DA RID: 10458 RVA: 0x000D85E4 File Offset: 0x000D67E4
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x000D85E7 File Offset: 0x000D67E7
	public float GetSliderMin(int index)
	{
		return 0f;
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x000D85EE File Offset: 0x000D67EE
	public float GetSliderMax(int index)
	{
		return 100f;
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x000D85F5 File Offset: 0x000D67F5
	public float GetSliderValue(int index)
	{
		return this.depressurizePercent * 100f;
	}

	// Token: 0x060028DE RID: 10462 RVA: 0x000D8603 File Offset: 0x000D6803
	public void SetSliderValue(float value, int index)
	{
		this.depressurizePercent = value / 100f;
	}

	// Token: 0x060028DF RID: 10463 RVA: 0x000D8612 File Offset: 0x000D6812
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.OIL_WELL_CAP_SIDE_SCREEN.TOOLTIP";
	}

	// Token: 0x060028E0 RID: 10464 RVA: 0x000D8619 File Offset: 0x000D6819
	string ISliderControl.GetSliderTooltip()
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.OIL_WELL_CAP_SIDE_SCREEN.TOOLTIP"), this.depressurizePercent * 100f);
	}

	// Token: 0x060028E1 RID: 10465 RVA: 0x000D8640 File Offset: 0x000D6840
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<OilWellCap>(-905833192, OilWellCap.OnCopySettingsDelegate);
	}

	// Token: 0x060028E2 RID: 10466 RVA: 0x000D865C File Offset: 0x000D685C
	private void OnCopySettings(object data)
	{
		OilWellCap component = ((GameObject)data).GetComponent<OilWellCap>();
		if (component != null)
		{
			this.depressurizePercent = component.depressurizePercent;
		}
	}

	// Token: 0x060028E3 RID: 10467 RVA: 0x000D868C File Offset: 0x000D688C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Prioritizable.AddRef(base.gameObject);
		this.accumulator = Game.Instance.accumulators.Add("pressuregas", this);
		this.showProgressBar = false;
		base.SetWorkTime(float.PositiveInfinity);
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_oil_cap_kanim") };
		this.workingStatusItem = Db.Get().BuildingStatusItems.ReleasingPressure;
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.pressureMeter = new MeterController(component, "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new Vector3(0f, 0f, 0f), null);
		this.smi = new OilWellCap.StatesInstance(this);
		this.smi.StartSM();
		this.UpdatePressurePercent();
	}

	// Token: 0x060028E4 RID: 10468 RVA: 0x000D87A1 File Offset: 0x000D69A1
	protected override void OnCleanUp()
	{
		Game.Instance.accumulators.Remove(this.accumulator);
		Prioritizable.RemoveRef(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x060028E5 RID: 10469 RVA: 0x000D87CA File Offset: 0x000D69CA
	public void AddGasPressure(float dt)
	{
		this.storage.AddGasChunk(this.gasElement, this.addGasRate * dt, this.gasTemperature, 0, 0, true, true);
		this.UpdatePressurePercent();
	}

	// Token: 0x060028E6 RID: 10470 RVA: 0x000D87F8 File Offset: 0x000D69F8
	public void ReleaseGasPressure(float dt)
	{
		PrimaryElement primaryElement = this.storage.FindPrimaryElement(this.gasElement);
		if (primaryElement != null && primaryElement.Mass > 0f)
		{
			float num = this.releaseGasRate * dt;
			if (base.worker != null)
			{
				num *= this.GetEfficiencyMultiplier(base.worker);
			}
			num = Mathf.Min(num, primaryElement.Mass);
			SimUtil.DiseaseInfo percentOfDisease = SimUtil.GetPercentOfDisease(primaryElement, num / primaryElement.Mass);
			primaryElement.Mass -= num;
			Game.Instance.accumulators.Accumulate(this.accumulator, num);
			SimMessages.AddRemoveSubstance(Grid.PosToCell(this), ElementLoader.GetElementIndex(this.gasElement), null, num, primaryElement.Temperature, percentOfDisease.idx, percentOfDisease.count, true, -1);
		}
		this.UpdatePressurePercent();
	}

	// Token: 0x060028E7 RID: 10471 RVA: 0x000D88CC File Offset: 0x000D6ACC
	private void UpdatePressurePercent()
	{
		float num = this.storage.GetMassAvailable(this.gasElement) / this.maxGasPressure;
		num = Mathf.Clamp01(num);
		this.smi.sm.pressurePercent.Set(num, this.smi, false);
		this.pressureMeter.SetPositionPercent(num);
	}

	// Token: 0x060028E8 RID: 10472 RVA: 0x000D8923 File Offset: 0x000D6B23
	public bool NeedsDepressurizing()
	{
		return this.smi.GetPressurePercent() >= this.depressurizePercent;
	}

	// Token: 0x060028E9 RID: 10473 RVA: 0x000D893C File Offset: 0x000D6B3C
	private WorkChore<OilWellCap> CreateWorkChore()
	{
		WorkChore<OilWellCap> workChore = new WorkChore<OilWellCap>(Db.Get().ChoreTypes.Depressurize, this, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		workChore.AddPrecondition(OilWellCap.AllowedToDepressurize, this);
		return workChore;
	}

	// Token: 0x060028EA RID: 10474 RVA: 0x000D897B File Offset: 0x000D6B7B
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.smi.sm.working.Set(true, this.smi, false);
	}

	// Token: 0x060028EB RID: 10475 RVA: 0x000D89A2 File Offset: 0x000D6BA2
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		this.smi.sm.working.Set(false, this.smi, false);
	}

	// Token: 0x060028EC RID: 10476 RVA: 0x000D89C9 File Offset: 0x000D6BC9
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		return this.smi.GetPressurePercent() <= 0f;
	}

	// Token: 0x060028ED RID: 10477 RVA: 0x000D89E0 File Offset: 0x000D6BE0
	public override bool InstantlyFinish(Worker worker)
	{
		this.ReleaseGasPressure(60f);
		return true;
	}

	// Token: 0x04001806 RID: 6150
	private OilWellCap.StatesInstance smi;

	// Token: 0x04001807 RID: 6151
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001808 RID: 6152
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04001809 RID: 6153
	public SimHashes gasElement;

	// Token: 0x0400180A RID: 6154
	public float gasTemperature;

	// Token: 0x0400180B RID: 6155
	public float addGasRate = 1f;

	// Token: 0x0400180C RID: 6156
	public float maxGasPressure = 10f;

	// Token: 0x0400180D RID: 6157
	public float releaseGasRate = 10f;

	// Token: 0x0400180E RID: 6158
	[Serialize]
	private float depressurizePercent = 0.75f;

	// Token: 0x0400180F RID: 6159
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x04001810 RID: 6160
	private MeterController pressureMeter;

	// Token: 0x04001811 RID: 6161
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001812 RID: 6162
	private static readonly EventSystem.IntraObjectHandler<OilWellCap> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<OilWellCap>(delegate(OilWellCap component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001813 RID: 6163
	private static readonly Chore.Precondition AllowedToDepressurize = new Chore.Precondition
	{
		id = "AllowedToDepressurize",
		description = DUPLICANTS.CHORES.PRECONDITIONS.ALLOWED_TO_DEPRESSURIZE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((OilWellCap)data).NeedsDepressurizing();
		}
	};

	// Token: 0x02001290 RID: 4752
	public class StatesInstance : GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.GameInstance
	{
		// Token: 0x06007ABE RID: 31422 RVA: 0x002C8D18 File Offset: 0x002C6F18
		public StatesInstance(OilWellCap master)
			: base(master)
		{
		}

		// Token: 0x06007ABF RID: 31423 RVA: 0x002C8D21 File Offset: 0x002C6F21
		public float GetPressurePercent()
		{
			return base.sm.pressurePercent.Get(base.smi);
		}
	}

	// Token: 0x02001291 RID: 4753
	public class States : GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap>
	{
		// Token: 0x06007AC0 RID: 31424 RVA: 0x002C8D3C File Offset: 0x002C6F3C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.operational, new StateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.Transition.ConditionCallback(this.IsOperational));
			this.operational.ToggleRecurringChore((OilWellCap.StatesInstance smi) => smi.master.CreateWorkChore(), null).DefaultState(this.operational.idle);
			this.operational.idle.PlayAnim("off").ToggleStatusItem(Db.Get().BuildingStatusItems.WellPressurizing, null).ParamTransition<float>(this.pressurePercent, this.operational.overpressure, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.IsGTEOne)
				.ParamTransition<bool>(this.working, this.operational.releasing_pressure, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.IsTrue)
				.EventTransition(GameHashes.OperationalChanged, this.inoperational, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.Not(new StateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.Transition.ConditionCallback(this.IsOperational)))
				.EventTransition(GameHashes.OnStorageChange, this.operational.active, new StateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.Transition.ConditionCallback(this.IsAbleToPump));
			this.operational.active.DefaultState(this.operational.active.pre).ToggleStatusItem(Db.Get().BuildingStatusItems.WellPressurizing, null).Enter(delegate(OilWellCap.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			})
				.Exit(delegate(OilWellCap.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				})
				.Update(delegate(OilWellCap.StatesInstance smi, float dt)
				{
					smi.master.AddGasPressure(dt);
				}, UpdateRate.SIM_200ms, false);
			this.operational.active.pre.PlayAnim("working_pre").ParamTransition<float>(this.pressurePercent, this.operational.overpressure, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.IsGTEOne).ParamTransition<bool>(this.working, this.operational.releasing_pressure, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.IsTrue)
				.OnAnimQueueComplete(this.operational.active.loop);
			this.operational.active.loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).ParamTransition<float>(this.pressurePercent, this.operational.active.pst, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.IsGTEOne).ParamTransition<bool>(this.working, this.operational.active.pst, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.IsTrue)
				.EventTransition(GameHashes.OperationalChanged, this.operational.active.pst, new StateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.Transition.ConditionCallback(this.MustStopPumping))
				.EventTransition(GameHashes.OnStorageChange, this.operational.active.pst, new StateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.Transition.ConditionCallback(this.MustStopPumping));
			this.operational.active.pst.PlayAnim("working_pst").OnAnimQueueComplete(this.operational.idle);
			this.operational.overpressure.PlayAnim("over_pressured_pre", KAnim.PlayMode.Once).QueueAnim("over_pressured_loop", true, null).ToggleStatusItem(Db.Get().BuildingStatusItems.WellOverpressure, null)
				.ParamTransition<float>(this.pressurePercent, this.operational.idle, (OilWellCap.StatesInstance smi, float p) => p <= 0f)
				.ParamTransition<bool>(this.working, this.operational.releasing_pressure, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.IsTrue);
			this.operational.releasing_pressure.DefaultState(this.operational.releasing_pressure.pre).ToggleStatusItem(Db.Get().BuildingStatusItems.EmittingElement, (OilWellCap.StatesInstance smi) => smi.master);
			this.operational.releasing_pressure.pre.PlayAnim("steam_out_pre").OnAnimQueueComplete(this.operational.releasing_pressure.loop);
			this.operational.releasing_pressure.loop.PlayAnim("steam_out_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.operational.releasing_pressure.pst, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.Not(new StateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.Transition.ConditionCallback(this.IsOperational))).ParamTransition<bool>(this.working, this.operational.releasing_pressure.pst, GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.IsFalse)
				.Update(delegate(OilWellCap.StatesInstance smi, float dt)
				{
					smi.master.ReleaseGasPressure(dt);
				}, UpdateRate.SIM_200ms, false);
			this.operational.releasing_pressure.pst.PlayAnim("steam_out_pst").OnAnimQueueComplete(this.operational.idle);
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x002C91FE File Offset: 0x002C73FE
		private bool IsOperational(OilWellCap.StatesInstance smi)
		{
			return smi.master.operational.IsOperational;
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x002C9210 File Offset: 0x002C7410
		private bool IsAbleToPump(OilWellCap.StatesInstance smi)
		{
			return smi.master.operational.IsOperational && smi.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false);
		}

		// Token: 0x06007AC3 RID: 31427 RVA: 0x002C9232 File Offset: 0x002C7432
		private bool MustStopPumping(OilWellCap.StatesInstance smi)
		{
			return !smi.master.operational.IsOperational || !smi.GetComponent<ElementConverter>().CanConvertAtAll();
		}

		// Token: 0x04005E29 RID: 24105
		public StateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.FloatParameter pressurePercent;

		// Token: 0x04005E2A RID: 24106
		public StateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.BoolParameter working;

		// Token: 0x04005E2B RID: 24107
		public GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.State inoperational;

		// Token: 0x04005E2C RID: 24108
		public OilWellCap.States.OperationalStates operational;

		// Token: 0x02001FD9 RID: 8153
		public class OperationalStates : GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.State
		{
			// Token: 0x04008DE3 RID: 36323
			public GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.State idle;

			// Token: 0x04008DE4 RID: 36324
			public GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.PreLoopPostState active;

			// Token: 0x04008DE5 RID: 36325
			public GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.State overpressure;

			// Token: 0x04008DE6 RID: 36326
			public GameStateMachine<OilWellCap.States, OilWellCap.StatesInstance, OilWellCap, object>.PreLoopPostState releasing_pressure;
		}
	}
}
