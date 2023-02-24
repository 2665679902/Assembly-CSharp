using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200092A RID: 2346
public class SodaFountain : StateMachineComponent<SodaFountain.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06004495 RID: 17557 RVA: 0x00182D74 File Offset: 0x00180F74
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		GameScheduler.Instance.Schedule("Scheduling Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, true);
		}, null, null);
	}

	// Token: 0x06004496 RID: 17558 RVA: 0x00182DC8 File Offset: 0x00180FC8
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06004497 RID: 17559 RVA: 0x00182DD0 File Offset: 0x00180FD0
	private void AddRequirementDesc(List<Descriptor> descs, Tag tag, float mass)
	{
		string text = tag.ProperName();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement);
		descs.Add(descriptor);
	}

	// Token: 0x06004498 RID: 17560 RVA: 0x00182E38 File Offset: 0x00181038
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		Effect.AddModifierDescriptions(base.gameObject, list, this.specificEffect, true);
		this.AddRequirementDesc(list, this.ingredientTag, this.ingredientMassPerUse);
		this.AddRequirementDesc(list, GameTags.Water, this.waterMassPerUse);
		return list;
	}

	// Token: 0x04002DB9 RID: 11705
	public string specificEffect;

	// Token: 0x04002DBA RID: 11706
	public string trackingEffect;

	// Token: 0x04002DBB RID: 11707
	public Tag ingredientTag;

	// Token: 0x04002DBC RID: 11708
	public float ingredientMassPerUse;

	// Token: 0x04002DBD RID: 11709
	public float waterMassPerUse;

	// Token: 0x02001709 RID: 5897
	public class States : GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain>
	{
		// Token: 0x06008951 RID: 35153 RVA: 0x002F8740 File Offset: 0x002F6940
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.operational, false);
			this.operational.PlayAnim("off").TagTransition(GameTags.Operational, this.unoperational, true).Transition(this.ready, new StateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.Transition.ConditionCallback(this.IsReady), UpdateRate.SIM_200ms)
				.EventTransition(GameHashes.OnStorageChange, this.ready, new StateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.Transition.ConditionCallback(this.IsReady));
			this.ready.TagTransition(GameTags.Operational, this.unoperational, true).DefaultState(this.ready.idle).ToggleChore(new Func<SodaFountain.StatesInstance, Chore>(this.CreateChore), this.operational);
			this.ready.idle.Transition(this.operational, GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.Not(new StateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.Transition.ConditionCallback(this.IsReady)), UpdateRate.SIM_200ms).EventTransition(GameHashes.OnStorageChange, this.operational, GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.Not(new StateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.Transition.ConditionCallback(this.IsReady))).WorkableStartTransition((SodaFountain.StatesInstance smi) => smi.master.GetComponent<SodaFountainWorkable>(), this.ready.working);
			this.ready.working.PlayAnim("working_pre").WorkableStopTransition((SodaFountain.StatesInstance smi) => smi.master.GetComponent<SodaFountainWorkable>(), this.ready.post);
			this.ready.post.PlayAnim("working_pst").OnAnimQueueComplete(this.ready);
		}

		// Token: 0x06008952 RID: 35154 RVA: 0x002F88EC File Offset: 0x002F6AEC
		private Chore CreateChore(SodaFountain.StatesInstance smi)
		{
			Workable component = smi.master.GetComponent<SodaFountainWorkable>();
			WorkChore<SodaFountainWorkable> workChore = new WorkChore<SodaFountainWorkable>(Db.Get().ChoreTypes.Relax, component, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, component);
			return workChore;
		}

		// Token: 0x06008953 RID: 35155 RVA: 0x002F894C File Offset: 0x002F6B4C
		private bool IsReady(SodaFountain.StatesInstance smi)
		{
			PrimaryElement primaryElement = smi.GetComponent<Storage>().FindPrimaryElement(SimHashes.Water);
			return !(primaryElement == null) && primaryElement.Mass >= smi.master.waterMassPerUse && smi.GetComponent<Storage>().GetAmountAvailable(smi.master.ingredientTag) >= smi.master.ingredientMassPerUse;
		}

		// Token: 0x04006BCC RID: 27596
		private GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.State unoperational;

		// Token: 0x04006BCD RID: 27597
		private GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.State operational;

		// Token: 0x04006BCE RID: 27598
		private SodaFountain.States.ReadyStates ready;

		// Token: 0x020020B6 RID: 8374
		public class ReadyStates : GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.State
		{
			// Token: 0x040091B8 RID: 37304
			public GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.State idle;

			// Token: 0x040091B9 RID: 37305
			public GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.State working;

			// Token: 0x040091BA RID: 37306
			public GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.State post;
		}
	}

	// Token: 0x0200170A RID: 5898
	public class StatesInstance : GameStateMachine<SodaFountain.States, SodaFountain.StatesInstance, SodaFountain, object>.GameInstance
	{
		// Token: 0x06008955 RID: 35157 RVA: 0x002F89B8 File Offset: 0x002F6BB8
		public StatesInstance(SodaFountain smi)
			: base(smi)
		{
		}
	}
}
