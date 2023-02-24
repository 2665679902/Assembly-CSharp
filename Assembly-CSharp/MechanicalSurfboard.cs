using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200080C RID: 2060
public class MechanicalSurfboard : StateMachineComponent<MechanicalSurfboard.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06003BC1 RID: 15297 RVA: 0x0014B1F8 File Offset: 0x001493F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003BC2 RID: 15298 RVA: 0x0014B20B File Offset: 0x0014940B
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06003BC3 RID: 15299 RVA: 0x0014B214 File Offset: 0x00149414
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Element element = ElementLoader.FindElementByHash(SimHashes.Water);
		list.Add(new Descriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect, false));
		Effect.AddModifierDescriptions(base.gameObject, list, this.specificEffect, true);
		list.Add(new Descriptor(BUILDINGS.PREFABS.MECHANICALSURFBOARD.WATER_REQUIREMENT.Replace("{element}", element.name).Replace("{amount}", GameUtil.GetFormattedMass(this.minOperationalWaterKG, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), BUILDINGS.PREFABS.MECHANICALSURFBOARD.WATER_REQUIREMENT_TOOLTIP.Replace("{element}", element.name).Replace("{amount}", GameUtil.GetFormattedMass(this.minOperationalWaterKG, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		list.Add(new Descriptor(BUILDINGS.PREFABS.MECHANICALSURFBOARD.LEAK_REQUIREMENT.Replace("{amount}", GameUtil.GetFormattedMass(this.waterSpillRateKG, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), BUILDINGS.PREFABS.MECHANICALSURFBOARD.LEAK_REQUIREMENT_TOOLTIP.Replace("{amount}", GameUtil.GetFormattedMass(this.waterSpillRateKG, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false).IncreaseIndent());
		return list;
	}

	// Token: 0x040026EF RID: 9967
	public string specificEffect;

	// Token: 0x040026F0 RID: 9968
	public string trackingEffect;

	// Token: 0x040026F1 RID: 9969
	public float waterSpillRateKG;

	// Token: 0x040026F2 RID: 9970
	public float minOperationalWaterKG;

	// Token: 0x040026F3 RID: 9971
	public string[] interactAnims = new string[] { "anim_interacts_mechanical_surfboard_kanim", "anim_interacts_mechanical_surfboard2_kanim", "anim_interacts_mechanical_surfboard3_kanim" };

	// Token: 0x02001562 RID: 5474
	public class States : GameStateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard>
	{
		// Token: 0x060083C7 RID: 33735 RVA: 0x002E8344 File Offset: 0x002E6544
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.operational, false).ToggleMainStatusItem(Db.Get().BuildingStatusItems.MissingRequirements, null);
			this.operational.PlayAnim("off").TagTransition(GameTags.Operational, this.inoperational, true).EventTransition(GameHashes.OnStorageChange, this.ready, new StateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.Transition.ConditionCallback(this.IsReady))
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GettingReady, null);
			this.ready.TagTransition(GameTags.Operational, this.inoperational, true).DefaultState(this.ready.idle).ToggleChore(new Func<MechanicalSurfboard.StatesInstance, Chore>(this.CreateChore), this.operational)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Working, null);
			this.ready.idle.PlayAnim("on", KAnim.PlayMode.Loop).WorkableStartTransition((MechanicalSurfboard.StatesInstance smi) => smi.master.GetComponent<MechanicalSurfboardWorkable>(), this.ready.working).EventTransition(GameHashes.OnStorageChange, this.operational, GameStateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.Not(new StateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.Transition.ConditionCallback(this.IsReady)));
			this.ready.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).WorkableStopTransition((MechanicalSurfboard.StatesInstance smi) => smi.master.GetComponent<MechanicalSurfboardWorkable>(), this.ready.post);
			this.ready.post.PlayAnim("working_pst").OnAnimQueueComplete(this.ready);
		}

		// Token: 0x060083C8 RID: 33736 RVA: 0x002E8510 File Offset: 0x002E6710
		private Chore CreateChore(MechanicalSurfboard.StatesInstance smi)
		{
			Workable component = smi.master.GetComponent<MechanicalSurfboardWorkable>();
			WorkChore<MechanicalSurfboardWorkable> workChore = new WorkChore<MechanicalSurfboardWorkable>(Db.Get().ChoreTypes.Relax, component, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, component);
			return workChore;
		}

		// Token: 0x060083C9 RID: 33737 RVA: 0x002E8570 File Offset: 0x002E6770
		private bool IsReady(MechanicalSurfboard.StatesInstance smi)
		{
			PrimaryElement primaryElement = smi.GetComponent<Storage>().FindPrimaryElement(SimHashes.Water);
			return !(primaryElement == null) && primaryElement.Mass >= smi.master.minOperationalWaterKG;
		}

		// Token: 0x0400668F RID: 26255
		private GameStateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.State inoperational;

		// Token: 0x04006690 RID: 26256
		private GameStateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.State operational;

		// Token: 0x04006691 RID: 26257
		private MechanicalSurfboard.States.ReadyStates ready;

		// Token: 0x02002074 RID: 8308
		public class ReadyStates : GameStateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.State
		{
			// Token: 0x040090B0 RID: 37040
			public GameStateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.State idle;

			// Token: 0x040090B1 RID: 37041
			public GameStateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.State working;

			// Token: 0x040090B2 RID: 37042
			public GameStateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.State post;
		}
	}

	// Token: 0x02001563 RID: 5475
	public class StatesInstance : GameStateMachine<MechanicalSurfboard.States, MechanicalSurfboard.StatesInstance, MechanicalSurfboard, object>.GameInstance
	{
		// Token: 0x060083CB RID: 33739 RVA: 0x002E85B7 File Offset: 0x002E67B7
		public StatesInstance(MechanicalSurfboard smi)
			: base(smi)
		{
		}
	}
}
