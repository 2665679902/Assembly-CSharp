using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020007E9 RID: 2025
public class Juicer : StateMachineComponent<Juicer.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06003A5A RID: 14938 RVA: 0x0014320C File Offset: 0x0014140C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		GameScheduler.Instance.Schedule("Scheduling Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, true);
		}, null, null);
	}

	// Token: 0x06003A5B RID: 14939 RVA: 0x00143260 File Offset: 0x00141460
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06003A5C RID: 14940 RVA: 0x00143268 File Offset: 0x00141468
	private void AddRequirementDesc(List<Descriptor> descs, Tag tag, float mass)
	{
		string text = tag.ProperName();
		Descriptor descriptor = default(Descriptor);
		string text2 = ((EdiblesManager.GetFoodInfo(tag.Name) != null) ? GameUtil.GetFormattedCaloriesForItem(tag, mass, GameUtil.TimeSlice.None, true) : GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"));
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, text2), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, text2), Descriptor.DescriptorType.Requirement);
		descs.Add(descriptor);
	}

	// Token: 0x06003A5D RID: 14941 RVA: 0x001432E0 File Offset: 0x001414E0
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		Effect.AddModifierDescriptions(base.gameObject, list, this.specificEffect, true);
		for (int i = 0; i < this.ingredientTags.Length; i++)
		{
			this.AddRequirementDesc(list, this.ingredientTags[i], this.ingredientMassesPerUse[i]);
		}
		this.AddRequirementDesc(list, GameTags.Water, this.waterMassPerUse);
		return list;
	}

	// Token: 0x0400264C RID: 9804
	public string specificEffect;

	// Token: 0x0400264D RID: 9805
	public string trackingEffect;

	// Token: 0x0400264E RID: 9806
	public Tag[] ingredientTags;

	// Token: 0x0400264F RID: 9807
	public float[] ingredientMassesPerUse;

	// Token: 0x04002650 RID: 9808
	public float waterMassPerUse;

	// Token: 0x0200153B RID: 5435
	public class States : GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer>
	{
		// Token: 0x06008308 RID: 33544 RVA: 0x002E6CBC File Offset: 0x002E4EBC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.operational, false);
			this.operational.PlayAnim("off").TagTransition(GameTags.Operational, this.unoperational, true).Transition(this.ready, new StateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.Transition.ConditionCallback(this.IsReady), UpdateRate.SIM_200ms)
				.EventTransition(GameHashes.OnStorageChange, this.ready, new StateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.Transition.ConditionCallback(this.IsReady));
			this.ready.TagTransition(GameTags.Operational, this.unoperational, true).DefaultState(this.ready.idle).ToggleChore(new Func<Juicer.StatesInstance, Chore>(this.CreateChore), this.operational);
			this.ready.idle.Transition(this.operational, GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.Not(new StateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.Transition.ConditionCallback(this.IsReady)), UpdateRate.SIM_200ms).EventTransition(GameHashes.OnStorageChange, this.operational, GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.Not(new StateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.Transition.ConditionCallback(this.IsReady))).PlayAnim("on")
				.WorkableStartTransition((Juicer.StatesInstance smi) => smi.master.GetComponent<JuicerWorkable>(), this.ready.working);
			this.ready.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).WorkableStopTransition((Juicer.StatesInstance smi) => smi.master.GetComponent<JuicerWorkable>(), this.ready.post);
			this.ready.post.PlayAnim("working_pst").OnAnimQueueComplete(this.ready);
		}

		// Token: 0x06008309 RID: 33545 RVA: 0x002E6E80 File Offset: 0x002E5080
		private Chore CreateChore(Juicer.StatesInstance smi)
		{
			Workable component = smi.master.GetComponent<JuicerWorkable>();
			WorkChore<JuicerWorkable> workChore = new WorkChore<JuicerWorkable>(Db.Get().ChoreTypes.Relax, component, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, component);
			return workChore;
		}

		// Token: 0x0600830A RID: 33546 RVA: 0x002E6EE0 File Offset: 0x002E50E0
		private bool IsReady(Juicer.StatesInstance smi)
		{
			PrimaryElement primaryElement = smi.GetComponent<Storage>().FindPrimaryElement(SimHashes.Water);
			if (primaryElement == null)
			{
				return false;
			}
			if (primaryElement.Mass < smi.master.waterMassPerUse)
			{
				return false;
			}
			for (int i = 0; i < smi.master.ingredientTags.Length; i++)
			{
				if (smi.GetComponent<Storage>().GetAmountAvailable(smi.master.ingredientTags[i]) < smi.master.ingredientMassesPerUse[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040065F7 RID: 26103
		private GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.State unoperational;

		// Token: 0x040065F8 RID: 26104
		private GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.State operational;

		// Token: 0x040065F9 RID: 26105
		private Juicer.States.ReadyStates ready;

		// Token: 0x02002070 RID: 8304
		public class ReadyStates : GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.State
		{
			// Token: 0x0400909F RID: 37023
			public GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.State idle;

			// Token: 0x040090A0 RID: 37024
			public GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.State working;

			// Token: 0x040090A1 RID: 37025
			public GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.State post;
		}
	}

	// Token: 0x0200153C RID: 5436
	public class StatesInstance : GameStateMachine<Juicer.States, Juicer.StatesInstance, Juicer, object>.GameInstance
	{
		// Token: 0x0600830C RID: 33548 RVA: 0x002E6F6C File Offset: 0x002E516C
		public StatesInstance(Juicer smi)
			: base(smi)
		{
		}
	}
}
