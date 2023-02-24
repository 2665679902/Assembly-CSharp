using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200086D RID: 2157
public class NuclearResearchCenter : StateMachineComponent<NuclearResearchCenter.StatesInstance>, IResearchCenter, IGameObjectEffectDescriptor
{
	// Token: 0x06003DF3 RID: 15859 RVA: 0x00159B34 File Offset: 0x00157D34
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.ResearchCenters.Add(this);
		this.particleMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", this.particleMeterOffset, Grid.SceneLayer.NoLayer, new string[] { "meter_target" });
		base.Subscribe<NuclearResearchCenter>(-1837862626, NuclearResearchCenter.OnStorageChangeDelegate);
		this.RefreshMeter();
		base.smi.StartSM();
		Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Radiation, true);
	}

	// Token: 0x06003DF4 RID: 15860 RVA: 0x00159BB3 File Offset: 0x00157DB3
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.ResearchCenters.Remove(this);
	}

	// Token: 0x06003DF5 RID: 15861 RVA: 0x00159BC6 File Offset: 0x00157DC6
	public string GetResearchType()
	{
		return this.researchTypeID;
	}

	// Token: 0x06003DF6 RID: 15862 RVA: 0x00159BCE File Offset: 0x00157DCE
	private void OnStorageChange(object data)
	{
		this.RefreshMeter();
	}

	// Token: 0x06003DF7 RID: 15863 RVA: 0x00159BD8 File Offset: 0x00157DD8
	private void RefreshMeter()
	{
		float num = Mathf.Clamp01(this.particleStorage.Particles / this.particleStorage.Capacity());
		this.particleMeter.SetPositionPercent(num);
	}

	// Token: 0x06003DF8 RID: 15864 RVA: 0x00159C10 File Offset: 0x00157E10
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(string.Format(UI.BUILDINGEFFECTS.RESEARCH_MATERIALS, this.inputMaterial.ProperName(), GameUtil.GetFormattedByTag(this.inputMaterial, this.materialPerPoint, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.RESEARCH_MATERIALS, this.inputMaterial.ProperName(), GameUtil.GetFormattedByTag(this.inputMaterial, this.materialPerPoint, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Requirement, false),
			new Descriptor(string.Format(UI.BUILDINGEFFECTS.PRODUCES_RESEARCH_POINTS, Research.Instance.researchTypes.GetResearchType(this.researchTypeID).name), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.PRODUCES_RESEARCH_POINTS, Research.Instance.researchTypes.GetResearchType(this.researchTypeID).name), Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x0400288F RID: 10383
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002890 RID: 10384
	public string researchTypeID;

	// Token: 0x04002891 RID: 10385
	public float materialPerPoint = 50f;

	// Token: 0x04002892 RID: 10386
	public float timePerPoint;

	// Token: 0x04002893 RID: 10387
	public Tag inputMaterial;

	// Token: 0x04002894 RID: 10388
	[MyCmpReq]
	private HighEnergyParticleStorage particleStorage;

	// Token: 0x04002895 RID: 10389
	public Meter.Offset particleMeterOffset;

	// Token: 0x04002896 RID: 10390
	private MeterController particleMeter;

	// Token: 0x04002897 RID: 10391
	private static readonly EventSystem.IntraObjectHandler<NuclearResearchCenter> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<NuclearResearchCenter>(delegate(NuclearResearchCenter component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x02001633 RID: 5683
	public class States : GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter>
	{
		// Token: 0x060086FC RID: 34556 RVA: 0x002F070C File Offset: 0x002EE90C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.requirements, false);
			this.requirements.PlayAnim("on").TagTransition(GameTags.Operational, this.inoperational, true).DefaultState(this.requirements.highEnergyParticlesNeeded);
			this.requirements.highEnergyParticlesNeeded.ToggleMainStatusItem(Db.Get().BuildingStatusItems.WaitingForHighEnergyParticles, null).EventTransition(GameHashes.OnParticleStorageChanged, this.requirements.noResearchSelected, new StateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Transition.ConditionCallback(this.IsReady));
			this.requirements.noResearchSelected.Enter(delegate(NuclearResearchCenter.StatesInstance smi)
			{
				this.UpdateNoResearchSelectedStatusItem(smi, true);
			}).Exit(delegate(NuclearResearchCenter.StatesInstance smi)
			{
				this.UpdateNoResearchSelectedStatusItem(smi, false);
			}).EventTransition(GameHashes.ActiveResearchChanged, this.requirements.noApplicableResearch, new StateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Transition.ConditionCallback(this.IsResearchSelected));
			this.requirements.noApplicableResearch.EventTransition(GameHashes.ActiveResearchChanged, this.ready, new StateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Transition.ConditionCallback(this.IsResearchApplicable)).EventTransition(GameHashes.ActiveResearchChanged, this.requirements, GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Not(new StateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Transition.ConditionCallback(this.IsResearchSelected)));
			this.ready.Enter(delegate(NuclearResearchCenter.StatesInstance smi)
			{
				smi.CreateChore();
			}).TagTransition(GameTags.Operational, this.inoperational, true).DefaultState(this.ready.idle)
				.Exit(delegate(NuclearResearchCenter.StatesInstance smi)
				{
					smi.DestroyChore();
				})
				.EventTransition(GameHashes.ActiveResearchChanged, this.requirements.noResearchSelected, GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Not(new StateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Transition.ConditionCallback(this.IsResearchSelected)))
				.EventTransition(GameHashes.ActiveResearchChanged, this.requirements.noApplicableResearch, GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Not(new StateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Transition.ConditionCallback(this.IsResearchApplicable)))
				.EventTransition(GameHashes.ResearchPointsChanged, this.requirements.noApplicableResearch, GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Not(new StateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Transition.ConditionCallback(this.IsResearchApplicable)))
				.EventTransition(GameHashes.OnParticleStorageEmpty, this.requirements.highEnergyParticlesNeeded, GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Not(new StateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.Transition.ConditionCallback(this.HasRadiation)));
			this.ready.idle.WorkableStartTransition((NuclearResearchCenter.StatesInstance smi) => smi.master.GetComponent<NuclearResearchCenterWorkable>(), this.ready.working);
			this.ready.working.Enter("SetActive(true)", delegate(NuclearResearchCenter.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit("SetActive(false)", delegate(NuclearResearchCenter.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).WorkableStopTransition((NuclearResearchCenter.StatesInstance smi) => smi.master.GetComponent<NuclearResearchCenterWorkable>(), this.ready.idle)
				.WorkableCompleteTransition((NuclearResearchCenter.StatesInstance smi) => smi.master.GetComponent<NuclearResearchCenterWorkable>(), this.ready.idle);
		}

		// Token: 0x060086FD RID: 34557 RVA: 0x002F0A50 File Offset: 0x002EEC50
		protected bool IsAllResearchComplete()
		{
			using (List<Tech>.Enumerator enumerator = Db.Get().Techs.resources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsComplete())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060086FE RID: 34558 RVA: 0x002F0AB4 File Offset: 0x002EECB4
		private void UpdateNoResearchSelectedStatusItem(NuclearResearchCenter.StatesInstance smi, bool entering)
		{
			bool flag = entering && !this.IsResearchSelected(smi) && !this.IsAllResearchComplete();
			KSelectable component = smi.GetComponent<KSelectable>();
			if (flag)
			{
				component.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.NoResearchSelected, null);
				return;
			}
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.NoResearchSelected, false);
		}

		// Token: 0x060086FF RID: 34559 RVA: 0x002F0B20 File Offset: 0x002EED20
		private bool IsReady(NuclearResearchCenter.StatesInstance smi)
		{
			return smi.GetComponent<HighEnergyParticleStorage>().Particles > smi.master.materialPerPoint;
		}

		// Token: 0x06008700 RID: 34560 RVA: 0x002F0B3A File Offset: 0x002EED3A
		private bool IsResearchSelected(NuclearResearchCenter.StatesInstance smi)
		{
			return Research.Instance.GetActiveResearch() != null;
		}

		// Token: 0x06008701 RID: 34561 RVA: 0x002F0B4C File Offset: 0x002EED4C
		private bool IsResearchApplicable(NuclearResearchCenter.StatesInstance smi)
		{
			TechInstance activeResearch = Research.Instance.GetActiveResearch();
			if (activeResearch != null && activeResearch.tech.costsByResearchTypeID.ContainsKey(smi.master.researchTypeID))
			{
				float num = activeResearch.progressInventory.PointsByTypeID[smi.master.researchTypeID];
				float num2 = activeResearch.tech.costsByResearchTypeID[smi.master.researchTypeID];
				return num < num2;
			}
			return false;
		}

		// Token: 0x06008702 RID: 34562 RVA: 0x002F0BC0 File Offset: 0x002EEDC0
		private bool HasRadiation(NuclearResearchCenter.StatesInstance smi)
		{
			return !smi.GetComponent<HighEnergyParticleStorage>().IsEmpty();
		}

		// Token: 0x0400692F RID: 26927
		public GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.State inoperational;

		// Token: 0x04006930 RID: 26928
		public NuclearResearchCenter.States.RequirementsState requirements;

		// Token: 0x04006931 RID: 26929
		public NuclearResearchCenter.States.ReadyState ready;

		// Token: 0x02002082 RID: 8322
		public class RequirementsState : GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.State
		{
			// Token: 0x040090D6 RID: 37078
			public GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.State highEnergyParticlesNeeded;

			// Token: 0x040090D7 RID: 37079
			public GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.State noResearchSelected;

			// Token: 0x040090D8 RID: 37080
			public GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.State noApplicableResearch;
		}

		// Token: 0x02002083 RID: 8323
		public class ReadyState : GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.State
		{
			// Token: 0x040090D9 RID: 37081
			public GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.State idle;

			// Token: 0x040090DA RID: 37082
			public GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.State working;
		}
	}

	// Token: 0x02001634 RID: 5684
	public class StatesInstance : GameStateMachine<NuclearResearchCenter.States, NuclearResearchCenter.StatesInstance, NuclearResearchCenter, object>.GameInstance
	{
		// Token: 0x06008706 RID: 34566 RVA: 0x002F0BEC File Offset: 0x002EEDEC
		public StatesInstance(NuclearResearchCenter master)
			: base(master)
		{
		}

		// Token: 0x06008707 RID: 34567 RVA: 0x002F0BF8 File Offset: 0x002EEDF8
		public void CreateChore()
		{
			Workable component = base.smi.master.GetComponent<NuclearResearchCenterWorkable>();
			this.chore = new WorkChore<NuclearResearchCenterWorkable>(Db.Get().ChoreTypes.Research, component, null, true, null, null, null, true, null, false, true, null, true, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			this.chore.preemption_cb = new Func<Chore.Precondition.Context, bool>(NuclearResearchCenter.StatesInstance.CanPreemptCB);
		}

		// Token: 0x06008708 RID: 34568 RVA: 0x002F0C59 File Offset: 0x002EEE59
		public void DestroyChore()
		{
			this.chore.Cancel("destroy me!");
			this.chore = null;
		}

		// Token: 0x06008709 RID: 34569 RVA: 0x002F0C74 File Offset: 0x002EEE74
		private static bool CanPreemptCB(Chore.Precondition.Context context)
		{
			Worker component = context.chore.driver.GetComponent<Worker>();
			float num = Db.Get().AttributeConverters.ResearchSpeed.Lookup(component).Evaluate();
			Worker worker = context.consumerState.worker;
			float num2 = Db.Get().AttributeConverters.ResearchSpeed.Lookup(worker).Evaluate();
			TechInstance activeResearch = Research.Instance.GetActiveResearch();
			if (activeResearch != null)
			{
				NuclearResearchCenter.StatesInstance smi = context.chore.gameObject.GetSMI<NuclearResearchCenter.StatesInstance>();
				if (smi != null)
				{
					return num2 > num && activeResearch.PercentageCompleteResearchType(smi.master.researchTypeID) < 1f;
				}
			}
			return false;
		}

		// Token: 0x04006932 RID: 26930
		private WorkChore<NuclearResearchCenterWorkable> chore;
	}
}
