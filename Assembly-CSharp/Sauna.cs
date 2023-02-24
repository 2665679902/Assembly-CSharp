﻿using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020008FB RID: 2299
public class Sauna : StateMachineComponent<Sauna.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x06004269 RID: 17001 RVA: 0x00176722 File Offset: 0x00174922
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x0600426A RID: 17002 RVA: 0x00176735 File Offset: 0x00174935
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x0600426B RID: 17003 RVA: 0x00176740 File Offset: 0x00174940
	private void AddRequirementDesc(List<Descriptor> descs, Tag tag, float mass)
	{
		string text = tag.ProperName();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, text, GameUtil.GetFormattedMass(mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement);
		descs.Add(descriptor);
	}

	// Token: 0x0600426C RID: 17004 RVA: 0x001767A8 File Offset: 0x001749A8
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.Add(new Descriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect, false));
		Effect.AddModifierDescriptions(base.gameObject, list, this.specificEffect, true);
		Element element = ElementLoader.FindElementByHash(SimHashes.Steam);
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, element.name, GameUtil.GetFormattedMass(this.steamPerUseKG, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, element.name, GameUtil.GetFormattedMass(this.steamPerUseKG, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Requirement, false));
		Element element2 = ElementLoader.FindElementByHash(SimHashes.Water);
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTEDPERUSE, element2.name, GameUtil.GetFormattedMass(this.steamPerUseKG, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTEDPERUSE, element2.name, GameUtil.GetFormattedMass(this.steamPerUseKG, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.##}")), Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x04002C60 RID: 11360
	public string specificEffect;

	// Token: 0x04002C61 RID: 11361
	public string trackingEffect;

	// Token: 0x04002C62 RID: 11362
	public float steamPerUseKG;

	// Token: 0x04002C63 RID: 11363
	public float waterOutputTemp;

	// Token: 0x04002C64 RID: 11364
	public static readonly Operational.Flag sufficientSteam = new Operational.Flag("sufficientSteam", Operational.Flag.Type.Requirement);

	// Token: 0x020016C7 RID: 5831
	public class States : GameStateMachine<Sauna.States, Sauna.StatesInstance, Sauna>
	{
		// Token: 0x060088AE RID: 34990 RVA: 0x002F64A0 File Offset: 0x002F46A0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.inoperational;
			this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.operational, false).ToggleMainStatusItem(Db.Get().BuildingStatusItems.MissingRequirements, null);
			this.operational.TagTransition(GameTags.Operational, this.inoperational, true).ToggleMainStatusItem(Db.Get().BuildingStatusItems.GettingReady, null).EventTransition(GameHashes.OnStorageChange, this.ready, new StateMachine<Sauna.States, Sauna.StatesInstance, Sauna, object>.Transition.ConditionCallback(this.IsReady));
			this.ready.TagTransition(GameTags.Operational, this.inoperational, true).DefaultState(this.ready.idle).ToggleChore(new Func<Sauna.StatesInstance, Chore>(this.CreateChore), this.inoperational)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Working, null);
			this.ready.idle.WorkableStartTransition((Sauna.StatesInstance smi) => smi.master.GetComponent<SaunaWorkable>(), this.ready.working).EventTransition(GameHashes.OnStorageChange, this.operational, GameStateMachine<Sauna.States, Sauna.StatesInstance, Sauna, object>.Not(new StateMachine<Sauna.States, Sauna.StatesInstance, Sauna, object>.Transition.ConditionCallback(this.IsReady)));
			this.ready.working.WorkableCompleteTransition((Sauna.StatesInstance smi) => smi.master.GetComponent<SaunaWorkable>(), this.ready.idle).WorkableStopTransition((Sauna.StatesInstance smi) => smi.master.GetComponent<SaunaWorkable>(), this.ready.idle);
		}

		// Token: 0x060088AF RID: 34991 RVA: 0x002F6650 File Offset: 0x002F4850
		private Chore CreateChore(Sauna.StatesInstance smi)
		{
			Workable component = smi.master.GetComponent<SaunaWorkable>();
			WorkChore<SaunaWorkable> workChore = new WorkChore<SaunaWorkable>(Db.Get().ChoreTypes.Relax, component, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, component);
			return workChore;
		}

		// Token: 0x060088B0 RID: 34992 RVA: 0x002F66B0 File Offset: 0x002F48B0
		private bool IsReady(Sauna.StatesInstance smi)
		{
			PrimaryElement primaryElement = smi.GetComponent<Storage>().FindPrimaryElement(SimHashes.Steam);
			return primaryElement != null && primaryElement.Mass >= smi.master.steamPerUseKG;
		}

		// Token: 0x04006AF0 RID: 27376
		private GameStateMachine<Sauna.States, Sauna.StatesInstance, Sauna, object>.State inoperational;

		// Token: 0x04006AF1 RID: 27377
		private GameStateMachine<Sauna.States, Sauna.StatesInstance, Sauna, object>.State operational;

		// Token: 0x04006AF2 RID: 27378
		private Sauna.States.ReadyStates ready;

		// Token: 0x020020AC RID: 8364
		public class ReadyStates : GameStateMachine<Sauna.States, Sauna.StatesInstance, Sauna, object>.State
		{
			// Token: 0x04009195 RID: 37269
			public GameStateMachine<Sauna.States, Sauna.StatesInstance, Sauna, object>.State idle;

			// Token: 0x04009196 RID: 37270
			public GameStateMachine<Sauna.States, Sauna.StatesInstance, Sauna, object>.State working;
		}
	}

	// Token: 0x020016C8 RID: 5832
	public class StatesInstance : GameStateMachine<Sauna.States, Sauna.StatesInstance, Sauna, object>.GameInstance
	{
		// Token: 0x060088B2 RID: 34994 RVA: 0x002F66F7 File Offset: 0x002F48F7
		public StatesInstance(Sauna smi)
			: base(smi)
		{
		}
	}
}
