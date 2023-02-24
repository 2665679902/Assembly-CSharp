using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200055B RID: 1371
public class BeachChair : StateMachineComponent<BeachChair.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x060020FE RID: 8446 RVA: 0x000B3C6F File Offset: 0x000B1E6F
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x060020FF RID: 8447 RVA: 0x000B3C82 File Offset: 0x000B1E82
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06002100 RID: 8448 RVA: 0x000B3C8C File Offset: 0x000B1E8C
	public static void AddModifierDescriptions(List<Descriptor> descs, string effect_id, bool high_lux)
	{
		Klei.AI.Modifier modifier = Db.Get().effects.Get(effect_id);
		LocString locString = (high_lux ? BUILDINGS.PREFABS.BEACHCHAIR.LIGHTEFFECT_HIGH : BUILDINGS.PREFABS.BEACHCHAIR.LIGHTEFFECT_LOW);
		LocString locString2 = (high_lux ? BUILDINGS.PREFABS.BEACHCHAIR.LIGHTEFFECT_HIGH_TOOLTIP : BUILDINGS.PREFABS.BEACHCHAIR.LIGHTEFFECT_LOW_TOOLTIP);
		foreach (AttributeModifier attributeModifier in modifier.SelfModifiers)
		{
			Descriptor descriptor = new Descriptor(locString.Replace("{attrib}", Strings.Get("STRINGS.DUPLICANTS.ATTRIBUTES." + attributeModifier.AttributeId.ToUpper() + ".NAME")).Replace("{amount}", attributeModifier.GetFormattedString()).Replace("{lux}", GameUtil.GetFormattedLux(10000)), locString2.Replace("{attrib}", Strings.Get("STRINGS.DUPLICANTS.ATTRIBUTES." + attributeModifier.AttributeId.ToUpper() + ".NAME")).Replace("{amount}", attributeModifier.GetFormattedString()).Replace("{lux}", GameUtil.GetFormattedLux(10000)), Descriptor.DescriptorType.Effect, false);
			descriptor.IncreaseIndent();
			descs.Add(descriptor);
		}
	}

	// Token: 0x06002101 RID: 8449 RVA: 0x000B3DCC File Offset: 0x000B1FCC
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.Add(new Descriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect, false));
		BeachChair.AddModifierDescriptions(list, this.specificEffectLit, true);
		BeachChair.AddModifierDescriptions(list, this.specificEffectUnlit, false);
		return list;
	}

	// Token: 0x06002102 RID: 8450 RVA: 0x000B3E19 File Offset: 0x000B2019
	public void SetLit(bool v)
	{
		base.smi.sm.lit.Set(v, base.smi, false);
	}

	// Token: 0x06002103 RID: 8451 RVA: 0x000B3E39 File Offset: 0x000B2039
	public void SetWorker(Worker worker)
	{
		base.smi.sm.worker.Set(worker, base.smi);
	}

	// Token: 0x040012F8 RID: 4856
	public string specificEffectUnlit;

	// Token: 0x040012F9 RID: 4857
	public string specificEffectLit;

	// Token: 0x040012FA RID: 4858
	public string trackingEffect;

	// Token: 0x040012FB RID: 4859
	public const float LIT_RATIO_FOR_POSITIVE_EFFECT = 0.75f;

	// Token: 0x02001184 RID: 4484
	public class States : GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair>
	{
		// Token: 0x060076CE RID: 30414 RVA: 0x002B8C1C File Offset: 0x002B6E1C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.ready, false).ToggleMainStatusItem(Db.Get().BuildingStatusItems.MissingRequirements, null);
			this.ready.TagTransition(GameTags.Operational, this.inoperational, true).DefaultState(this.ready.idle).ToggleChore(new Func<BeachChair.StatesInstance, Chore>(this.CreateChore), this.inoperational)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Working, null);
			this.ready.idle.PlayAnim("on", KAnim.PlayMode.Loop).WorkableStartTransition((BeachChair.StatesInstance smi) => smi.master.GetComponent<BeachChairWorkable>(), this.ready.working_pre);
			this.ready.working_pre.PlayAnim("working_pre").QueueAnim("working_loop", true, null).Target(this.worker)
				.PlayAnim("working_pre")
				.EventHandler(GameHashes.AnimQueueComplete, delegate(BeachChair.StatesInstance smi)
				{
					if (this.lit.Get(smi))
					{
						smi.GoTo(this.ready.working_lit);
						return;
					}
					smi.GoTo(this.ready.working_unlit);
				});
			this.ready.working_unlit.DefaultState(this.ready.working_unlit.working).Enter(delegate(BeachChair.StatesInstance smi)
			{
				BeachChairWorkable component = smi.master.GetComponent<BeachChairWorkable>();
				component.workingPstComplete = (component.workingPstFailed = this.UNLIT_PST_ANIMS);
			}).ToggleStatusItem(Db.Get().BuildingStatusItems.TanningLightInsufficient, null)
				.WorkableStopTransition((BeachChair.StatesInstance smi) => smi.master.GetComponent<BeachChairWorkable>(), this.ready.post)
				.Target(this.worker)
				.PlayAnim("working_unlit_pre");
			this.ready.working_unlit.working.ParamTransition<bool>(this.lit, this.ready.working_unlit.post, GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.IsTrue).Target(this.worker).QueueAnim("working_unlit_loop", true, null);
			this.ready.working_unlit.post.Target(this.worker).PlayAnim("working_unlit_pst").EventHandler(GameHashes.AnimQueueComplete, delegate(BeachChair.StatesInstance smi)
			{
				if (this.lit.Get(smi))
				{
					smi.GoTo(this.ready.working_lit);
					return;
				}
				smi.GoTo(this.ready.working_unlit.working);
			});
			this.ready.working_lit.DefaultState(this.ready.working_lit.working).Enter(delegate(BeachChair.StatesInstance smi)
			{
				BeachChairWorkable component2 = smi.master.GetComponent<BeachChairWorkable>();
				component2.workingPstComplete = (component2.workingPstFailed = this.LIT_PST_ANIMS);
			}).ToggleStatusItem(Db.Get().BuildingStatusItems.TanningLightSufficient, null)
				.WorkableStopTransition((BeachChair.StatesInstance smi) => smi.master.GetComponent<BeachChairWorkable>(), this.ready.post)
				.Target(this.worker)
				.PlayAnim("working_lit_pre");
			this.ready.working_lit.working.ParamTransition<bool>(this.lit, this.ready.working_lit.post, GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.IsFalse).Target(this.worker).QueueAnim("working_lit_loop", true, null)
				.ScheduleGoTo((BeachChair.StatesInstance smi) => UnityEngine.Random.Range(5f, 15f), this.ready.working_lit.silly);
			this.ready.working_lit.silly.ParamTransition<bool>(this.lit, this.ready.working_lit.post, GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.IsFalse).Target(this.worker).PlayAnim((BeachChair.StatesInstance smi) => this.SILLY_ANIMS[UnityEngine.Random.Range(0, this.SILLY_ANIMS.Length)], KAnim.PlayMode.Once)
				.OnAnimQueueComplete(this.ready.working_lit.working);
			this.ready.working_lit.post.Target(this.worker).PlayAnim("working_lit_pst").EventHandler(GameHashes.AnimQueueComplete, delegate(BeachChair.StatesInstance smi)
			{
				if (!this.lit.Get(smi))
				{
					smi.GoTo(this.ready.working_unlit);
					return;
				}
				smi.GoTo(this.ready.working_lit.working);
			});
			this.ready.post.PlayAnim("working_pst").Exit(delegate(BeachChair.StatesInstance smi)
			{
				this.worker.Set(null, smi);
			}).OnAnimQueueComplete(this.ready);
		}

		// Token: 0x060076CF RID: 30415 RVA: 0x002B9038 File Offset: 0x002B7238
		private Chore CreateChore(BeachChair.StatesInstance smi)
		{
			Workable component = smi.master.GetComponent<BeachChairWorkable>();
			WorkChore<BeachChairWorkable> workChore = new WorkChore<BeachChairWorkable>(Db.Get().ChoreTypes.Relax, component, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, component);
			return workChore;
		}

		// Token: 0x04005B10 RID: 23312
		public StateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.BoolParameter lit;

		// Token: 0x04005B11 RID: 23313
		public StateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.TargetParameter worker;

		// Token: 0x04005B12 RID: 23314
		private GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State inoperational;

		// Token: 0x04005B13 RID: 23315
		private BeachChair.States.ReadyStates ready;

		// Token: 0x04005B14 RID: 23316
		private HashedString[] UNLIT_PST_ANIMS = new HashedString[] { "working_unlit_pst", "working_pst" };

		// Token: 0x04005B15 RID: 23317
		private HashedString[] LIT_PST_ANIMS = new HashedString[] { "working_lit_pst", "working_pst" };

		// Token: 0x04005B16 RID: 23318
		private string[] SILLY_ANIMS = new string[] { "working_lit_loop1", "working_lit_loop2", "working_lit_loop3" };

		// Token: 0x02001F85 RID: 8069
		public class LitWorkingStates : GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State
		{
			// Token: 0x04008C1C RID: 35868
			public GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State working;

			// Token: 0x04008C1D RID: 35869
			public GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State silly;

			// Token: 0x04008C1E RID: 35870
			public GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State post;
		}

		// Token: 0x02001F86 RID: 8070
		public class WorkingStates : GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State
		{
			// Token: 0x04008C1F RID: 35871
			public GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State working;

			// Token: 0x04008C20 RID: 35872
			public GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State post;
		}

		// Token: 0x02001F87 RID: 8071
		public class ReadyStates : GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State
		{
			// Token: 0x04008C21 RID: 35873
			public GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State idle;

			// Token: 0x04008C22 RID: 35874
			public GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State working_pre;

			// Token: 0x04008C23 RID: 35875
			public BeachChair.States.WorkingStates working_unlit;

			// Token: 0x04008C24 RID: 35876
			public BeachChair.States.LitWorkingStates working_lit;

			// Token: 0x04008C25 RID: 35877
			public GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.State post;
		}
	}

	// Token: 0x02001185 RID: 4485
	public class StatesInstance : GameStateMachine<BeachChair.States, BeachChair.StatesInstance, BeachChair, object>.GameInstance
	{
		// Token: 0x060076D8 RID: 30424 RVA: 0x002B924E File Offset: 0x002B744E
		public StatesInstance(BeachChair smi)
			: base(smi)
		{
		}
	}
}
