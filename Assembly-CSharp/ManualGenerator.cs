using System;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000602 RID: 1538
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/ManualGenerator")]
public class ManualGenerator : Workable, ISingleSliderControl, ISliderControl
{
	// Token: 0x17000289 RID: 649
	// (get) Token: 0x060027F6 RID: 10230 RVA: 0x000D4DA3 File Offset: 0x000D2FA3
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.MANUALGENERATORSIDESCREEN.TITLE";
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x060027F7 RID: 10231 RVA: 0x000D4DAA File Offset: 0x000D2FAA
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.PERCENT;
		}
	}

	// Token: 0x060027F8 RID: 10232 RVA: 0x000D4DB6 File Offset: 0x000D2FB6
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x060027F9 RID: 10233 RVA: 0x000D4DB9 File Offset: 0x000D2FB9
	public float GetSliderMin(int index)
	{
		return 0f;
	}

	// Token: 0x060027FA RID: 10234 RVA: 0x000D4DC0 File Offset: 0x000D2FC0
	public float GetSliderMax(int index)
	{
		return 100f;
	}

	// Token: 0x060027FB RID: 10235 RVA: 0x000D4DC7 File Offset: 0x000D2FC7
	public float GetSliderValue(int index)
	{
		return this.batteryRefillPercent * 100f;
	}

	// Token: 0x060027FC RID: 10236 RVA: 0x000D4DD5 File Offset: 0x000D2FD5
	public void SetSliderValue(float value, int index)
	{
		this.batteryRefillPercent = value / 100f;
	}

	// Token: 0x060027FD RID: 10237 RVA: 0x000D4DE4 File Offset: 0x000D2FE4
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.MANUALGENERATORSIDESCREEN.TOOLTIP";
	}

	// Token: 0x060027FE RID: 10238 RVA: 0x000D4DEB File Offset: 0x000D2FEB
	string ISliderControl.GetSliderTooltip()
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.MANUALGENERATORSIDESCREEN.TOOLTIP"), this.batteryRefillPercent * 100f);
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x060027FF RID: 10239 RVA: 0x000D4E12 File Offset: 0x000D3012
	public bool IsPowered
	{
		get
		{
			return this.operational.IsActive;
		}
	}

	// Token: 0x06002800 RID: 10240 RVA: 0x000D4E1F File Offset: 0x000D301F
	private ManualGenerator()
	{
		this.showProgressBar = false;
	}

	// Token: 0x06002801 RID: 10241 RVA: 0x000D4E3C File Offset: 0x000D303C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<ManualGenerator>(-592767678, ManualGenerator.OnOperationalChangedDelegate);
		base.Subscribe<ManualGenerator>(824508782, ManualGenerator.OnActiveChangedDelegate);
		this.workerStatusItem = Db.Get().DuplicantStatusItems.GeneratingPower;
		this.attributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
		this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
		this.skillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
		this.skillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		EnergyGenerator.EnsureStatusItemAvailable();
	}

	// Token: 0x06002802 RID: 10242 RVA: 0x000D4ED0 File Offset: 0x000D30D0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(float.PositiveInfinity);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		foreach (KAnimHashedString kanimHashedString in ManualGenerator.symbol_names)
		{
			component.SetSymbolVisiblity(kanimHashedString, false);
		}
		Building component2 = base.GetComponent<Building>();
		this.powerCell = component2.GetPowerOutputCell();
		this.OnActiveChanged(null);
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_generatormanual_kanim") };
		this.smi = new ManualGenerator.GeneratePowerSM.Instance(this);
		this.smi.StartSM();
		Game.Instance.energySim.AddManualGenerator(this);
	}

	// Token: 0x06002803 RID: 10243 RVA: 0x000D4F7A File Offset: 0x000D317A
	protected override void OnCleanUp()
	{
		Game.Instance.energySim.RemoveManualGenerator(this);
		this.smi.StopSM("cleanup");
		base.OnCleanUp();
	}

	// Token: 0x06002804 RID: 10244 RVA: 0x000D4FA2 File Offset: 0x000D31A2
	protected void OnActiveChanged(object is_active)
	{
		if (this.operational.IsActive)
		{
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.ManualGeneratorChargingUp, null);
		}
	}

	// Token: 0x06002805 RID: 10245 RVA: 0x000D4FDC File Offset: 0x000D31DC
	public void EnergySim200ms(float dt)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.operational.IsActive)
		{
			this.generator.GenerateJoules(this.generator.WattageRating * dt, false);
			component.SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.Wattage, this.generator);
			return;
		}
		this.generator.ResetJoules();
		component.SetStatusItem(Db.Get().StatusItemCategories.Power, Db.Get().BuildingStatusItems.GeneratorOffline, null);
		if (this.operational.IsOperational)
		{
			CircuitManager circuitManager = Game.Instance.circuitManager;
			if (circuitManager == null)
			{
				return;
			}
			ushort circuitID = circuitManager.GetCircuitID(this.generator);
			bool flag = circuitManager.HasBatteries(circuitID);
			bool flag2 = false;
			if (!flag && circuitManager.HasConsumers(circuitID))
			{
				flag2 = true;
			}
			else if (flag)
			{
				if (this.batteryRefillPercent <= 0f && circuitManager.GetMinBatteryPercentFullOnCircuit(circuitID) <= 0f)
				{
					flag2 = true;
				}
				else if (circuitManager.GetMinBatteryPercentFullOnCircuit(circuitID) < this.batteryRefillPercent)
				{
					flag2 = true;
				}
			}
			if (flag2)
			{
				if (this.chore == null && this.smi.GetCurrentState() == this.smi.sm.on)
				{
					this.chore = new WorkChore<ManualGenerator>(Db.Get().ChoreTypes.GeneratePower, this, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
				}
			}
			else if (this.chore != null)
			{
				this.chore.Cancel("No refill needed");
				this.chore = null;
			}
			component.ToggleStatusItem(EnergyGenerator.BatteriesSufficientlyFull, !flag2, null);
		}
	}

	// Token: 0x06002806 RID: 10246 RVA: 0x000D5178 File Offset: 0x000D3378
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.operational.SetActive(true, false);
	}

	// Token: 0x06002807 RID: 10247 RVA: 0x000D5190 File Offset: 0x000D3390
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		CircuitManager circuitManager = Game.Instance.circuitManager;
		bool flag = false;
		if (circuitManager != null)
		{
			ushort circuitID = circuitManager.GetCircuitID(this.generator);
			bool flag2 = circuitManager.HasBatteries(circuitID);
			flag = (flag2 && circuitManager.GetMinBatteryPercentFullOnCircuit(circuitID) < 1f) || (!flag2 && circuitManager.HasConsumers(circuitID));
		}
		AttributeLevels component = worker.GetComponent<AttributeLevels>();
		if (component != null)
		{
			component.AddExperience(Db.Get().Attributes.Athletics.Id, dt, DUPLICANTSTATS.ATTRIBUTE_LEVELING.ALL_DAY_EXPERIENCE);
		}
		return !flag;
	}

	// Token: 0x06002808 RID: 10248 RVA: 0x000D521C File Offset: 0x000D341C
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		this.operational.SetActive(false, false);
	}

	// Token: 0x06002809 RID: 10249 RVA: 0x000D5232 File Offset: 0x000D3432
	protected override void OnCompleteWork(Worker worker)
	{
		this.operational.SetActive(false, false);
		if (this.chore != null)
		{
			this.chore.Cancel("complete");
			this.chore = null;
		}
	}

	// Token: 0x0600280A RID: 10250 RVA: 0x000D5260 File Offset: 0x000D3460
	public override bool InstantlyFinish(Worker worker)
	{
		return false;
	}

	// Token: 0x0600280B RID: 10251 RVA: 0x000D5263 File Offset: 0x000D3463
	private void OnOperationalChanged(object data)
	{
		if (!this.buildingEnabledButton.IsEnabled)
		{
			this.generator.ResetJoules();
		}
	}

	// Token: 0x0400177E RID: 6014
	[Serialize]
	[SerializeField]
	private float batteryRefillPercent = 0.5f;

	// Token: 0x0400177F RID: 6015
	private const float batteryStopRunningPercent = 1f;

	// Token: 0x04001780 RID: 6016
	[MyCmpReq]
	private Generator generator;

	// Token: 0x04001781 RID: 6017
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001782 RID: 6018
	[MyCmpGet]
	private BuildingEnabledButton buildingEnabledButton;

	// Token: 0x04001783 RID: 6019
	private Chore chore;

	// Token: 0x04001784 RID: 6020
	private int powerCell;

	// Token: 0x04001785 RID: 6021
	private ManualGenerator.GeneratePowerSM.Instance smi;

	// Token: 0x04001786 RID: 6022
	private static readonly KAnimHashedString[] symbol_names = new KAnimHashedString[] { "meter", "meter_target", "meter_fill", "meter_frame", "meter_light", "meter_tubing" };

	// Token: 0x04001787 RID: 6023
	private static readonly EventSystem.IntraObjectHandler<ManualGenerator> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<ManualGenerator>(delegate(ManualGenerator component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001788 RID: 6024
	private static readonly EventSystem.IntraObjectHandler<ManualGenerator> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<ManualGenerator>(delegate(ManualGenerator component, object data)
	{
		component.OnActiveChanged(data);
	});

	// Token: 0x02001268 RID: 4712
	public class GeneratePowerSM : GameStateMachine<ManualGenerator.GeneratePowerSM, ManualGenerator.GeneratePowerSM.Instance>
	{
		// Token: 0x06007A1A RID: 31258 RVA: 0x002C6098 File Offset: 0x002C4298
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.off.EventTransition(GameHashes.OperationalChanged, this.on, (ManualGenerator.GeneratePowerSM.Instance smi) => smi.master.GetComponent<Operational>().IsOperational).PlayAnim("off");
			this.on.EventTransition(GameHashes.OperationalChanged, this.off, (ManualGenerator.GeneratePowerSM.Instance smi) => !smi.master.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.working.pre, (ManualGenerator.GeneratePowerSM.Instance smi) => smi.master.GetComponent<Operational>().IsActive).PlayAnim("on");
			this.working.DefaultState(this.working.pre);
			this.working.pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working.loop);
			this.working.loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.ActiveChanged, this.off, (ManualGenerator.GeneratePowerSM.Instance smi) => this.masterTarget.Get(smi) != null && !smi.master.GetComponent<Operational>().IsActive);
		}

		// Token: 0x04005DBD RID: 23997
		public GameStateMachine<ManualGenerator.GeneratePowerSM, ManualGenerator.GeneratePowerSM.Instance, IStateMachineTarget, object>.State off;

		// Token: 0x04005DBE RID: 23998
		public GameStateMachine<ManualGenerator.GeneratePowerSM, ManualGenerator.GeneratePowerSM.Instance, IStateMachineTarget, object>.State on;

		// Token: 0x04005DBF RID: 23999
		public ManualGenerator.GeneratePowerSM.WorkingStates working;

		// Token: 0x02001FC5 RID: 8133
		public class WorkingStates : GameStateMachine<ManualGenerator.GeneratePowerSM, ManualGenerator.GeneratePowerSM.Instance, IStateMachineTarget, object>.State
		{
			// Token: 0x04008D98 RID: 36248
			public GameStateMachine<ManualGenerator.GeneratePowerSM, ManualGenerator.GeneratePowerSM.Instance, IStateMachineTarget, object>.State pre;

			// Token: 0x04008D99 RID: 36249
			public GameStateMachine<ManualGenerator.GeneratePowerSM, ManualGenerator.GeneratePowerSM.Instance, IStateMachineTarget, object>.State loop;

			// Token: 0x04008D9A RID: 36250
			public GameStateMachine<ManualGenerator.GeneratePowerSM, ManualGenerator.GeneratePowerSM.Instance, IStateMachineTarget, object>.State pst;
		}

		// Token: 0x02001FC6 RID: 8134
		public new class Instance : GameStateMachine<ManualGenerator.GeneratePowerSM, ManualGenerator.GeneratePowerSM.Instance, IStateMachineTarget, object>.GameInstance
		{
			// Token: 0x0600A0C0 RID: 41152 RVA: 0x00342006 File Offset: 0x00340206
			public Instance(IStateMachineTarget master)
				: base(master)
			{
			}
		}
	}
}
