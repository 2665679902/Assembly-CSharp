using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000759 RID: 1881
public class EspressoMachine : StateMachineComponent<EspressoMachine.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x060033D5 RID: 13269 RVA: 0x00116F18 File Offset: 0x00115118
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		GameScheduler.Instance.Schedule("Scheduling Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, true);
		}, null, null);
	}

	// Token: 0x060033D6 RID: 13270 RVA: 0x00116F6C File Offset: 0x0011516C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x060033D7 RID: 13271 RVA: 0x00116F74 File Offset: 0x00115174
	private void AddRequirementDesc(List<Descriptor> descs, Tag tag, float mass)
	{
		string text = tag.ProperName();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement);
		descs.Add(descriptor);
	}

	// Token: 0x060033D8 RID: 13272 RVA: 0x00116FDC File Offset: 0x001151DC
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		Effect.AddModifierDescriptions(base.gameObject, list, "Espresso", true);
		this.AddRequirementDesc(list, EspressoMachine.INGREDIENT_TAG, EspressoMachine.INGREDIENT_MASS_PER_USE);
		this.AddRequirementDesc(list, GameTags.Water, EspressoMachine.WATER_MASS_PER_USE);
		return list;
	}

	// Token: 0x04001FCE RID: 8142
	public const string SPECIFIC_EFFECT = "Espresso";

	// Token: 0x04001FCF RID: 8143
	public const string TRACKING_EFFECT = "RecentlyRecDrink";

	// Token: 0x04001FD0 RID: 8144
	public static Tag INGREDIENT_TAG = new Tag("SpiceNut");

	// Token: 0x04001FD1 RID: 8145
	public static float INGREDIENT_MASS_PER_USE = 1f;

	// Token: 0x04001FD2 RID: 8146
	public static float WATER_MASS_PER_USE = 1f;

	// Token: 0x02001459 RID: 5209
	public class States : GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine>
	{
		// Token: 0x060080DA RID: 32986 RVA: 0x002DFEA4 File Offset: 0x002DE0A4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.operational, false);
			this.operational.PlayAnim("off").TagTransition(GameTags.Operational, this.unoperational, true).Transition(this.ready, new StateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.Transition.ConditionCallback(this.IsReady), UpdateRate.SIM_200ms)
				.EventTransition(GameHashes.OnStorageChange, this.ready, new StateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.Transition.ConditionCallback(this.IsReady));
			this.ready.TagTransition(GameTags.Operational, this.unoperational, true).DefaultState(this.ready.idle).ToggleChore(new Func<EspressoMachine.StatesInstance, Chore>(this.CreateChore), this.operational);
			this.ready.idle.PlayAnim("on", KAnim.PlayMode.Loop).WorkableStartTransition((EspressoMachine.StatesInstance smi) => smi.master.GetComponent<EspressoMachineWorkable>(), this.ready.working).Transition(this.operational, GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.Not(new StateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.Transition.ConditionCallback(this.IsReady)), UpdateRate.SIM_200ms)
				.EventTransition(GameHashes.OnStorageChange, this.operational, GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.Not(new StateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.Transition.ConditionCallback(this.IsReady)));
			this.ready.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).WorkableStopTransition((EspressoMachine.StatesInstance smi) => smi.master.GetComponent<EspressoMachineWorkable>(), this.ready.post);
			this.ready.post.PlayAnim("working_pst").OnAnimQueueComplete(this.ready);
		}

		// Token: 0x060080DB RID: 32987 RVA: 0x002E0068 File Offset: 0x002DE268
		private Chore CreateChore(EspressoMachine.StatesInstance smi)
		{
			Workable component = smi.master.GetComponent<EspressoMachineWorkable>();
			WorkChore<EspressoMachineWorkable> workChore = new WorkChore<EspressoMachineWorkable>(Db.Get().ChoreTypes.Relax, component, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, component);
			return workChore;
		}

		// Token: 0x060080DC RID: 32988 RVA: 0x002E00C8 File Offset: 0x002DE2C8
		private bool IsReady(EspressoMachine.StatesInstance smi)
		{
			PrimaryElement primaryElement = smi.GetComponent<Storage>().FindPrimaryElement(SimHashes.Water);
			return !(primaryElement == null) && primaryElement.Mass >= EspressoMachine.WATER_MASS_PER_USE && smi.GetComponent<Storage>().GetAmountAvailable(EspressoMachine.INGREDIENT_TAG) >= EspressoMachine.INGREDIENT_MASS_PER_USE;
		}

		// Token: 0x0400632B RID: 25387
		private GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.State unoperational;

		// Token: 0x0400632C RID: 25388
		private GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.State operational;

		// Token: 0x0400632D RID: 25389
		private EspressoMachine.States.ReadyStates ready;

		// Token: 0x02002050 RID: 8272
		public class ReadyStates : GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.State
		{
			// Token: 0x04008FFE RID: 36862
			public GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.State idle;

			// Token: 0x04008FFF RID: 36863
			public GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.State working;

			// Token: 0x04009000 RID: 36864
			public GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.State post;
		}
	}

	// Token: 0x0200145A RID: 5210
	public class StatesInstance : GameStateMachine<EspressoMachine.States, EspressoMachine.StatesInstance, EspressoMachine, object>.GameInstance
	{
		// Token: 0x060080DE RID: 32990 RVA: 0x002E0122 File Offset: 0x002DE322
		public StatesInstance(EspressoMachine smi)
			: base(smi)
		{
		}
	}
}
