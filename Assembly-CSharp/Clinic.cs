using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000685 RID: 1669
[AddComponentMenu("KMonoBehaviour/Workable/Clinic")]
public class Clinic : Workable, IGameObjectEffectDescriptor, ISingleSliderControl, ISliderControl
{
	// Token: 0x06002D0D RID: 11533 RVA: 0x000ECC60 File Offset: 0x000EAE60
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = false;
		this.assignable.subSlots = new AssignableSlot[] { Db.Get().AssignableSlots.MedicalBed };
		this.assignable.AddAutoassignPrecondition(new Func<MinionAssignablesProxy, bool>(this.CanAutoAssignTo));
		this.assignable.AddAssignPrecondition(new Func<MinionAssignablesProxy, bool>(this.CanManuallyAssignTo));
	}

	// Token: 0x06002D0E RID: 11534 RVA: 0x000ECCCB File Offset: 0x000EAECB
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Prioritizable.AddRef(base.gameObject);
		Components.Clinics.Add(this);
		base.SetWorkTime(float.PositiveInfinity);
		this.clinicSMI = new Clinic.ClinicSM.Instance(this);
		this.clinicSMI.StartSM();
	}

	// Token: 0x06002D0F RID: 11535 RVA: 0x000ECD0B File Offset: 0x000EAF0B
	protected override void OnCleanUp()
	{
		Prioritizable.RemoveRef(base.gameObject);
		Components.Clinics.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06002D10 RID: 11536 RVA: 0x000ECD2C File Offset: 0x000EAF2C
	private KAnimFile[] GetAppropriateOverrideAnims(Worker worker)
	{
		KAnimFile[] array = null;
		if (!worker.GetSMI<WoundMonitor.Instance>().ShouldExitInfirmary())
		{
			array = this.workerInjuredAnims;
		}
		else if (this.workerDiseasedAnims != null && this.IsValidEffect(this.diseaseEffect) && worker.GetSMI<SicknessMonitor.Instance>().IsSick())
		{
			array = this.workerDiseasedAnims;
		}
		return array;
	}

	// Token: 0x06002D11 RID: 11537 RVA: 0x000ECD7C File Offset: 0x000EAF7C
	public override Workable.AnimInfo GetAnim(Worker worker)
	{
		this.overrideAnims = this.GetAppropriateOverrideAnims(worker);
		return base.GetAnim(worker);
	}

	// Token: 0x06002D12 RID: 11538 RVA: 0x000ECD92 File Offset: 0x000EAF92
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		worker.GetComponent<Effects>().Add("Sleep", false);
	}

	// Token: 0x06002D13 RID: 11539 RVA: 0x000ECDB0 File Offset: 0x000EAFB0
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		KAnimFile[] appropriateOverrideAnims = this.GetAppropriateOverrideAnims(worker);
		if (appropriateOverrideAnims == null || appropriateOverrideAnims != this.overrideAnims)
		{
			return true;
		}
		base.OnWorkTick(worker, dt);
		return false;
	}

	// Token: 0x06002D14 RID: 11540 RVA: 0x000ECDDD File Offset: 0x000EAFDD
	protected override void OnStopWork(Worker worker)
	{
		worker.GetComponent<Effects>().Remove("Sleep");
		base.OnStopWork(worker);
	}

	// Token: 0x06002D15 RID: 11541 RVA: 0x000ECDF8 File Offset: 0x000EAFF8
	protected override void OnCompleteWork(Worker worker)
	{
		this.assignable.Unassign();
		base.OnCompleteWork(worker);
		Effects component = worker.GetComponent<Effects>();
		for (int i = 0; i < Clinic.EffectsRemoved.Length; i++)
		{
			string text = Clinic.EffectsRemoved[i];
			component.Remove(text);
		}
	}

	// Token: 0x06002D16 RID: 11542 RVA: 0x000ECE3F File Offset: 0x000EB03F
	public override bool InstantlyFinish(Worker worker)
	{
		return false;
	}

	// Token: 0x06002D17 RID: 11543 RVA: 0x000ECE44 File Offset: 0x000EB044
	private Chore CreateWorkChore(ChoreType chore_type, bool allow_prioritization, bool allow_in_red_alert, PriorityScreen.PriorityClass priority_class, bool ignore_schedule_block = false)
	{
		return new WorkChore<Clinic>(chore_type, this, null, true, null, null, null, allow_in_red_alert, null, ignore_schedule_block, true, null, false, true, allow_prioritization, priority_class, 5, false, false);
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x000ECE70 File Offset: 0x000EB070
	private bool CanAutoAssignTo(MinionAssignablesProxy worker)
	{
		bool flag = false;
		MinionIdentity minionIdentity = worker.target as MinionIdentity;
		if (minionIdentity != null)
		{
			if (this.IsValidEffect(this.healthEffect))
			{
				Health component = minionIdentity.GetComponent<Health>();
				if (component != null && component.hitPoints < component.maxHitPoints)
				{
					flag = true;
				}
			}
			if (!flag && this.IsValidEffect(this.diseaseEffect))
			{
				flag = minionIdentity.GetComponent<MinionModifiers>().sicknesses.Count > 0;
			}
		}
		return flag;
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x000ECEE8 File Offset: 0x000EB0E8
	private bool CanManuallyAssignTo(MinionAssignablesProxy worker)
	{
		bool flag = false;
		MinionIdentity minionIdentity = worker.target as MinionIdentity;
		if (minionIdentity != null)
		{
			flag = this.IsHealthBelowThreshold(minionIdentity.gameObject);
		}
		return flag;
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x000ECF1C File Offset: 0x000EB11C
	private bool IsHealthBelowThreshold(GameObject minion)
	{
		Health health = ((minion != null) ? minion.GetComponent<Health>() : null);
		if (health != null)
		{
			float num = health.hitPoints / health.maxHitPoints;
			if (health != null)
			{
				return num < this.MedicalAttentionMinimum;
			}
		}
		return false;
	}

	// Token: 0x06002D1B RID: 11547 RVA: 0x000ECF67 File Offset: 0x000EB167
	private bool IsValidEffect(string effect)
	{
		return effect != null && effect != "";
	}

	// Token: 0x06002D1C RID: 11548 RVA: 0x000ECF79 File Offset: 0x000EB179
	private bool AllowDoctoring()
	{
		return this.IsValidEffect(this.doctoredDiseaseEffect) || this.IsValidEffect(this.doctoredHealthEffect);
	}

	// Token: 0x06002D1D RID: 11549 RVA: 0x000ECF98 File Offset: 0x000EB198
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		if (this.IsValidEffect(this.healthEffect))
		{
			Effect.AddModifierDescriptions(base.gameObject, descriptors, this.healthEffect, false);
		}
		if (this.diseaseEffect != this.healthEffect && this.IsValidEffect(this.diseaseEffect))
		{
			Effect.AddModifierDescriptions(base.gameObject, descriptors, this.diseaseEffect, false);
		}
		if (this.AllowDoctoring())
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.DOCTORING, UI.BUILDINGEFFECTS.TOOLTIPS.DOCTORING, Descriptor.DescriptorType.Effect);
			descriptors.Add(descriptor);
			if (this.IsValidEffect(this.doctoredHealthEffect))
			{
				Effect.AddModifierDescriptions(base.gameObject, descriptors, this.doctoredHealthEffect, true);
			}
			if (this.doctoredDiseaseEffect != this.doctoredHealthEffect && this.IsValidEffect(this.doctoredDiseaseEffect))
			{
				Effect.AddModifierDescriptions(base.gameObject, descriptors, this.doctoredDiseaseEffect, true);
			}
		}
		return descriptors;
	}

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x06002D1E RID: 11550 RVA: 0x000ED08E File Offset: 0x000EB28E
	public float MedicalAttentionMinimum
	{
		get
		{
			return this.sicknessSliderValue / 100f;
		}
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x06002D1F RID: 11551 RVA: 0x000ED09C File Offset: 0x000EB29C
	string ISliderControl.SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.MEDICALCOTSIDESCREEN.TITLE";
		}
	}

	// Token: 0x17000325 RID: 805
	// (get) Token: 0x06002D20 RID: 11552 RVA: 0x000ED0A3 File Offset: 0x000EB2A3
	string ISliderControl.SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.PERCENT;
		}
	}

	// Token: 0x06002D21 RID: 11553 RVA: 0x000ED0AF File Offset: 0x000EB2AF
	int ISliderControl.SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x06002D22 RID: 11554 RVA: 0x000ED0B2 File Offset: 0x000EB2B2
	float ISliderControl.GetSliderMin(int index)
	{
		return 0f;
	}

	// Token: 0x06002D23 RID: 11555 RVA: 0x000ED0B9 File Offset: 0x000EB2B9
	float ISliderControl.GetSliderMax(int index)
	{
		return 100f;
	}

	// Token: 0x06002D24 RID: 11556 RVA: 0x000ED0C0 File Offset: 0x000EB2C0
	float ISliderControl.GetSliderValue(int index)
	{
		return this.sicknessSliderValue;
	}

	// Token: 0x06002D25 RID: 11557 RVA: 0x000ED0C8 File Offset: 0x000EB2C8
	void ISliderControl.SetSliderValue(float percent, int index)
	{
		if (percent != this.sicknessSliderValue)
		{
			this.sicknessSliderValue = (float)Mathf.RoundToInt(percent);
			Game.Instance.Trigger(875045922, null);
		}
	}

	// Token: 0x06002D26 RID: 11558 RVA: 0x000ED0F0 File Offset: 0x000EB2F0
	string ISliderControl.GetSliderTooltip()
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.MEDICALCOTSIDESCREEN.TOOLTIP"), this.sicknessSliderValue);
	}

	// Token: 0x06002D27 RID: 11559 RVA: 0x000ED111 File Offset: 0x000EB311
	string ISliderControl.GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.MEDICALCOTSIDESCREEN.TOOLTIP";
	}

	// Token: 0x04001ADB RID: 6875
	[MyCmpReq]
	private Assignable assignable;

	// Token: 0x04001ADC RID: 6876
	private static readonly string[] EffectsRemoved = new string[] { "SoreBack" };

	// Token: 0x04001ADD RID: 6877
	private const int MAX_RANGE = 10;

	// Token: 0x04001ADE RID: 6878
	private const float CHECK_RANGE_INTERVAL = 10f;

	// Token: 0x04001ADF RID: 6879
	public float doctorVisitInterval = 300f;

	// Token: 0x04001AE0 RID: 6880
	public KAnimFile[] workerInjuredAnims;

	// Token: 0x04001AE1 RID: 6881
	public KAnimFile[] workerDiseasedAnims;

	// Token: 0x04001AE2 RID: 6882
	public string diseaseEffect;

	// Token: 0x04001AE3 RID: 6883
	public string healthEffect;

	// Token: 0x04001AE4 RID: 6884
	public string doctoredDiseaseEffect;

	// Token: 0x04001AE5 RID: 6885
	public string doctoredHealthEffect;

	// Token: 0x04001AE6 RID: 6886
	public string doctoredPlaceholderEffect;

	// Token: 0x04001AE7 RID: 6887
	private Clinic.ClinicSM.Instance clinicSMI;

	// Token: 0x04001AE8 RID: 6888
	public static readonly Chore.Precondition IsOverSicknessThreshold = new Chore.Precondition
	{
		id = "IsOverSicknessThreshold",
		description = DUPLICANTS.CHORES.PRECONDITIONS.IS_NOT_BEING_ATTACKED,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((Clinic)data).IsHealthBelowThreshold(context.consumerState.gameObject);
		}
	};

	// Token: 0x04001AE9 RID: 6889
	[Serialize]
	private float sicknessSliderValue = 70f;

	// Token: 0x0200134A RID: 4938
	public class ClinicSM : GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic>
	{
		// Token: 0x06007D41 RID: 32065 RVA: 0x002D38EC File Offset: 0x002D1AEC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Never;
			default_state = this.unoperational;
			this.unoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (Clinic.ClinicSM.Instance smi) => smi.GetComponent<Operational>().IsOperational).Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.master.GetComponent<Assignable>().Unassign();
			});
			this.operational.DefaultState(this.operational.idle).EventTransition(GameHashes.OperationalChanged, this.unoperational, (Clinic.ClinicSM.Instance smi) => !smi.master.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.AssigneeChanged, this.unoperational, null)
				.ToggleRecurringChore((Clinic.ClinicSM.Instance smi) => smi.master.CreateWorkChore(Db.Get().ChoreTypes.Heal, false, true, PriorityScreen.PriorityClass.personalNeeds, false), (Clinic.ClinicSM.Instance smi) => !string.IsNullOrEmpty(smi.master.healthEffect))
				.ToggleRecurringChore((Clinic.ClinicSM.Instance smi) => smi.master.CreateWorkChore(Db.Get().ChoreTypes.HealCritical, false, true, PriorityScreen.PriorityClass.personalNeeds, false), (Clinic.ClinicSM.Instance smi) => !string.IsNullOrEmpty(smi.master.healthEffect))
				.ToggleRecurringChore((Clinic.ClinicSM.Instance smi) => smi.master.CreateWorkChore(Db.Get().ChoreTypes.RestDueToDisease, false, true, PriorityScreen.PriorityClass.personalNeeds, true), (Clinic.ClinicSM.Instance smi) => !string.IsNullOrEmpty(smi.master.diseaseEffect))
				.ToggleRecurringChore((Clinic.ClinicSM.Instance smi) => smi.master.CreateWorkChore(Db.Get().ChoreTypes.SleepDueToDisease, false, true, PriorityScreen.PriorityClass.personalNeeds, true), (Clinic.ClinicSM.Instance smi) => !string.IsNullOrEmpty(smi.master.diseaseEffect));
			this.operational.idle.WorkableStartTransition((Clinic.ClinicSM.Instance smi) => smi.master, this.operational.healing);
			this.operational.healing.DefaultState(this.operational.healing.undoctored).WorkableStopTransition((Clinic.ClinicSM.Instance smi) => smi.GetComponent<Clinic>(), this.operational.idle).Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.master.GetComponent<Operational>().SetActive(true, false);
			})
				.Exit(delegate(Clinic.ClinicSM.Instance smi)
				{
					smi.master.GetComponent<Operational>().SetActive(false, false);
				});
			this.operational.healing.undoctored.Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.StartEffect(smi.master.healthEffect, false);
				smi.StartEffect(smi.master.diseaseEffect, false);
				bool flag = false;
				if (smi.master.worker != null)
				{
					flag = smi.HasEffect(smi.master.doctoredHealthEffect) || smi.HasEffect(smi.master.doctoredDiseaseEffect) || smi.HasEffect(smi.master.doctoredPlaceholderEffect);
				}
				if (smi.master.AllowDoctoring())
				{
					if (flag)
					{
						smi.GoTo(this.operational.healing.doctored);
						return;
					}
					smi.StartDoctorChore();
				}
			}).Exit(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.StopEffect(smi.master.healthEffect);
				smi.StopEffect(smi.master.diseaseEffect);
				smi.StopDoctorChore();
			});
			this.operational.healing.newlyDoctored.Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.StartEffect(smi.master.doctoredDiseaseEffect, true);
				smi.StartEffect(smi.master.doctoredHealthEffect, true);
				smi.GoTo(this.operational.healing.doctored);
			});
			this.operational.healing.doctored.Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				Effects component = smi.master.worker.GetComponent<Effects>();
				if (smi.HasEffect(smi.master.doctoredPlaceholderEffect))
				{
					EffectInstance effectInstance = component.Get(smi.master.doctoredPlaceholderEffect);
					EffectInstance effectInstance2 = smi.StartEffect(smi.master.doctoredDiseaseEffect, true);
					if (effectInstance2 != null)
					{
						float num = effectInstance.effect.duration - effectInstance.timeRemaining;
						effectInstance2.timeRemaining = effectInstance2.effect.duration - num;
					}
					EffectInstance effectInstance3 = smi.StartEffect(smi.master.doctoredHealthEffect, true);
					if (effectInstance3 != null)
					{
						float num2 = effectInstance.effect.duration - effectInstance.timeRemaining;
						effectInstance3.timeRemaining = effectInstance3.effect.duration - num2;
					}
					component.Remove(smi.master.doctoredPlaceholderEffect);
				}
			}).ScheduleGoTo(delegate(Clinic.ClinicSM.Instance smi)
			{
				Effects component2 = smi.master.worker.GetComponent<Effects>();
				float num3 = smi.master.doctorVisitInterval;
				if (smi.HasEffect(smi.master.doctoredHealthEffect))
				{
					EffectInstance effectInstance4 = component2.Get(smi.master.doctoredHealthEffect);
					num3 = Mathf.Min(num3, effectInstance4.GetTimeRemaining());
				}
				if (smi.HasEffect(smi.master.doctoredDiseaseEffect))
				{
					EffectInstance effectInstance4 = component2.Get(smi.master.doctoredDiseaseEffect);
					num3 = Mathf.Min(num3, effectInstance4.GetTimeRemaining());
				}
				return num3;
			}, this.operational.healing.undoctored).Exit(delegate(Clinic.ClinicSM.Instance smi)
			{
				Effects component3 = smi.master.worker.GetComponent<Effects>();
				if (smi.HasEffect(smi.master.doctoredDiseaseEffect) || smi.HasEffect(smi.master.doctoredHealthEffect))
				{
					EffectInstance effectInstance5 = component3.Get(smi.master.doctoredDiseaseEffect);
					if (effectInstance5 == null)
					{
						effectInstance5 = component3.Get(smi.master.doctoredHealthEffect);
					}
					EffectInstance effectInstance6 = smi.StartEffect(smi.master.doctoredPlaceholderEffect, true);
					effectInstance6.timeRemaining = effectInstance6.effect.duration - (effectInstance5.effect.duration - effectInstance5.timeRemaining);
					component3.Remove(smi.master.doctoredDiseaseEffect);
					component3.Remove(smi.master.doctoredHealthEffect);
				}
			});
		}

		// Token: 0x0400601E RID: 24606
		public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State unoperational;

		// Token: 0x0400601F RID: 24607
		public Clinic.ClinicSM.OperationalStates operational;

		// Token: 0x02002023 RID: 8227
		public class OperationalStates : GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State
		{
			// Token: 0x04008F5D RID: 36701
			public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State idle;

			// Token: 0x04008F5E RID: 36702
			public Clinic.ClinicSM.HealingStates healing;
		}

		// Token: 0x02002024 RID: 8228
		public class HealingStates : GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State
		{
			// Token: 0x04008F5F RID: 36703
			public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State undoctored;

			// Token: 0x04008F60 RID: 36704
			public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State doctored;

			// Token: 0x04008F61 RID: 36705
			public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State newlyDoctored;
		}

		// Token: 0x02002025 RID: 8229
		public new class Instance : GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.GameInstance
		{
			// Token: 0x0600A291 RID: 41617 RVA: 0x00344602 File Offset: 0x00342802
			public Instance(Clinic master)
				: base(master)
			{
			}

			// Token: 0x0600A292 RID: 41618 RVA: 0x0034460C File Offset: 0x0034280C
			public void StartDoctorChore()
			{
				if (base.master.IsValidEffect(base.master.doctoredHealthEffect) || base.master.IsValidEffect(base.master.doctoredDiseaseEffect))
				{
					this.doctorChore = new WorkChore<DoctorChoreWorkable>(Db.Get().ChoreTypes.Doctor, base.smi.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
					WorkChore<DoctorChoreWorkable> workChore = this.doctorChore;
					workChore.onComplete = (Action<Chore>)Delegate.Combine(workChore.onComplete, new Action<Chore>(delegate(Chore chore)
					{
						base.smi.GoTo(base.smi.sm.operational.healing.newlyDoctored);
					}));
				}
			}

			// Token: 0x0600A293 RID: 41619 RVA: 0x003446A6 File Offset: 0x003428A6
			public void StopDoctorChore()
			{
				if (this.doctorChore != null)
				{
					this.doctorChore.Cancel("StopDoctorChore");
					this.doctorChore = null;
				}
			}

			// Token: 0x0600A294 RID: 41620 RVA: 0x003446C8 File Offset: 0x003428C8
			public bool HasEffect(string effect)
			{
				bool flag = false;
				if (base.master.IsValidEffect(effect))
				{
					flag = base.smi.master.worker.GetComponent<Effects>().HasEffect(effect);
				}
				return flag;
			}

			// Token: 0x0600A295 RID: 41621 RVA: 0x00344704 File Offset: 0x00342904
			public EffectInstance StartEffect(string effect, bool should_save)
			{
				if (base.master.IsValidEffect(effect))
				{
					Worker worker = base.smi.master.worker;
					if (worker != null)
					{
						Effects component = worker.GetComponent<Effects>();
						if (!component.HasEffect(effect))
						{
							return component.Add(effect, should_save);
						}
					}
				}
				return null;
			}

			// Token: 0x0600A296 RID: 41622 RVA: 0x00344754 File Offset: 0x00342954
			public void StopEffect(string effect)
			{
				if (base.master.IsValidEffect(effect))
				{
					Worker worker = base.smi.master.worker;
					if (worker != null)
					{
						Effects component = worker.GetComponent<Effects>();
						if (component.HasEffect(effect))
						{
							component.Remove(effect);
						}
					}
				}
			}

			// Token: 0x04008F62 RID: 36706
			private WorkChore<DoctorChoreWorkable> doctorChore;
		}
	}
}
