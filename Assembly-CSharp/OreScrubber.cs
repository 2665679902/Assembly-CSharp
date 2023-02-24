﻿using System;
using System.Collections.Generic;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200061C RID: 1564
public class OreScrubber : StateMachineComponent<OreScrubber.SMInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x060028FD RID: 10493 RVA: 0x000D8C4C File Offset: 0x000D6E4C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.FindOrAddComponent<Workable>();
	}

	// Token: 0x060028FE RID: 10494 RVA: 0x000D8C60 File Offset: 0x000D6E60
	private void RefreshMeters()
	{
		float num = 0f;
		PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(this.consumedElement);
		if (primaryElement != null)
		{
			num = Mathf.Clamp01(primaryElement.Mass / base.GetComponent<ConduitConsumer>().capacityKG);
		}
		this.cleanMeter.SetPositionPercent(num);
	}

	// Token: 0x060028FF RID: 10495 RVA: 0x000D8CB4 File Offset: 0x000D6EB4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.cleanMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_clean_target", "meter_clean", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_clean_target" });
		this.RefreshMeters();
		base.Subscribe<OreScrubber>(-1697596308, OreScrubber.OnStorageChangeDelegate);
		DirectionControl component = base.GetComponent<DirectionControl>();
		component.onDirectionChanged = (Action<WorkableReactable.AllowedDirection>)Delegate.Combine(component.onDirectionChanged, new Action<WorkableReactable.AllowedDirection>(this.OnDirectionChanged));
		this.OnDirectionChanged(base.GetComponent<DirectionControl>().allowedDirection);
	}

	// Token: 0x06002900 RID: 10496 RVA: 0x000D8D4D File Offset: 0x000D6F4D
	private void OnDirectionChanged(WorkableReactable.AllowedDirection allowed_direction)
	{
		if (this.reactable != null)
		{
			this.reactable.allowedDirection = allowed_direction;
		}
	}

	// Token: 0x06002901 RID: 10497 RVA: 0x000D8D64 File Offset: 0x000D6F64
	public List<Descriptor> RequirementDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		string name = ElementLoader.FindElementByHash(this.consumedElement).name;
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, name, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, name, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		return list;
	}

	// Token: 0x06002902 RID: 10498 RVA: 0x000D8DDC File Offset: 0x000D6FDC
	public List<Descriptor> EffectDescriptors()
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.outputElement != SimHashes.Vacuum)
		{
			list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTEMITTEDPERUSE, ElementLoader.FindElementByHash(this.outputElement).name, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTEMITTEDPERUSE, ElementLoader.FindElementByHash(this.outputElement).name, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Effect, false));
		}
		list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.DISEASECONSUMEDPERUSE, GameUtil.GetFormattedDiseaseAmount(this.diseaseRemovalCount, GameUtil.TimeSlice.None)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.DISEASECONSUMEDPERUSE, GameUtil.GetFormattedDiseaseAmount(this.diseaseRemovalCount, GameUtil.TimeSlice.None)), Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x06002903 RID: 10499 RVA: 0x000D8EB1 File Offset: 0x000D70B1
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.AddRange(this.RequirementDescriptors());
		list.AddRange(this.EffectDescriptors());
		return list;
	}

	// Token: 0x06002904 RID: 10500 RVA: 0x000D8ED0 File Offset: 0x000D70D0
	private void OnStorageChange(object data)
	{
		this.RefreshMeters();
	}

	// Token: 0x06002905 RID: 10501 RVA: 0x000D8ED8 File Offset: 0x000D70D8
	private static PrimaryElement GetFirstInfected(Storage storage)
	{
		foreach (GameObject gameObject in storage.items)
		{
			if (!(gameObject == null))
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				if (component.DiseaseIdx != 255 && !gameObject.HasTag(GameTags.Edible))
				{
					return component;
				}
			}
		}
		return null;
	}

	// Token: 0x04001818 RID: 6168
	public float massConsumedPerUse = 1f;

	// Token: 0x04001819 RID: 6169
	public SimHashes consumedElement = SimHashes.BleachStone;

	// Token: 0x0400181A RID: 6170
	public int diseaseRemovalCount = 10000;

	// Token: 0x0400181B RID: 6171
	public SimHashes outputElement = SimHashes.Vacuum;

	// Token: 0x0400181C RID: 6172
	private WorkableReactable reactable;

	// Token: 0x0400181D RID: 6173
	private MeterController cleanMeter;

	// Token: 0x0400181E RID: 6174
	[Serialize]
	public int maxPossiblyRemoved;

	// Token: 0x0400181F RID: 6175
	private static readonly EventSystem.IntraObjectHandler<OreScrubber> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<OreScrubber>(delegate(OreScrubber component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x02001295 RID: 4757
	private class ScrubOreReactable : WorkableReactable
	{
		// Token: 0x06007ACF RID: 31439 RVA: 0x002C92C2 File Offset: 0x002C74C2
		public ScrubOreReactable(Workable workable, ChoreType chore_type, WorkableReactable.AllowedDirection allowed_direction = WorkableReactable.AllowedDirection.Any)
			: base(workable, "ScrubOre", chore_type, allowed_direction)
		{
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x002C92D8 File Offset: 0x002C74D8
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (base.InternalCanBegin(new_reactor, transition))
			{
				Storage component = new_reactor.GetComponent<Storage>();
				if (component != null && OreScrubber.GetFirstInfected(component) != null)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x02001296 RID: 4758
	public class SMInstance : GameStateMachine<OreScrubber.States, OreScrubber.SMInstance, OreScrubber, object>.GameInstance
	{
		// Token: 0x06007AD1 RID: 31441 RVA: 0x002C9310 File Offset: 0x002C7510
		public SMInstance(OreScrubber master)
			: base(master)
		{
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x002C931C File Offset: 0x002C751C
		public bool HasSufficientMass()
		{
			bool flag = false;
			PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(base.master.consumedElement);
			if (primaryElement != null)
			{
				flag = primaryElement.Mass > 0f;
			}
			return flag;
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x002C935A File Offset: 0x002C755A
		public Dictionary<Tag, float> GetNeededMass()
		{
			return new Dictionary<Tag, float> { 
			{
				base.master.consumedElement.CreateTag(),
				base.master.massConsumedPerUse
			} };
		}

		// Token: 0x06007AD4 RID: 31444 RVA: 0x002C9382 File Offset: 0x002C7582
		public void OnCompleteWork(Worker worker)
		{
		}

		// Token: 0x06007AD5 RID: 31445 RVA: 0x002C9384 File Offset: 0x002C7584
		public void DumpOutput()
		{
			Storage component = base.master.GetComponent<Storage>();
			if (base.master.outputElement != SimHashes.Vacuum)
			{
				component.Drop(ElementLoader.FindElementByHash(base.master.outputElement).tag);
			}
		}
	}

	// Token: 0x02001297 RID: 4759
	public class States : GameStateMachine<OreScrubber.States, OreScrubber.SMInstance, OreScrubber>
	{
		// Token: 0x06007AD6 RID: 31446 RVA: 0x002C93CC File Offset: 0x002C75CC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.notready;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.notoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.notready, false);
			this.notready.PlayAnim("off").EventTransition(GameHashes.OnStorageChange, this.ready, (OreScrubber.SMInstance smi) => smi.HasSufficientMass()).ToggleStatusItem(Db.Get().BuildingStatusItems.MaterialsUnavailable, (OreScrubber.SMInstance smi) => smi.GetNeededMass())
				.TagTransition(GameTags.Operational, this.notoperational, true);
			this.ready.DefaultState(this.ready.free).ToggleReactable((OreScrubber.SMInstance smi) => smi.master.reactable = new OreScrubber.ScrubOreReactable(smi.master.GetComponent<OreScrubber.Work>(), Db.Get().ChoreTypes.ScrubOre, smi.master.GetComponent<DirectionControl>().allowedDirection)).EventTransition(GameHashes.OnStorageChange, this.notready, (OreScrubber.SMInstance smi) => !smi.HasSufficientMass())
				.TagTransition(GameTags.Operational, this.notoperational, true);
			this.ready.free.PlayAnim("on").WorkableStartTransition((OreScrubber.SMInstance smi) => smi.GetComponent<OreScrubber.Work>(), this.ready.occupied);
			this.ready.occupied.PlayAnim("working_pre").QueueAnim("working_loop", true, null).WorkableStopTransition((OreScrubber.SMInstance smi) => smi.GetComponent<OreScrubber.Work>(), this.ready);
		}

		// Token: 0x04005E30 RID: 24112
		public GameStateMachine<OreScrubber.States, OreScrubber.SMInstance, OreScrubber, object>.State notready;

		// Token: 0x04005E31 RID: 24113
		public OreScrubber.States.ReadyStates ready;

		// Token: 0x04005E32 RID: 24114
		public GameStateMachine<OreScrubber.States, OreScrubber.SMInstance, OreScrubber, object>.State notoperational;

		// Token: 0x04005E33 RID: 24115
		public GameStateMachine<OreScrubber.States, OreScrubber.SMInstance, OreScrubber, object>.State full;

		// Token: 0x04005E34 RID: 24116
		public GameStateMachine<OreScrubber.States, OreScrubber.SMInstance, OreScrubber, object>.State empty;

		// Token: 0x02001FDB RID: 8155
		public class ReadyStates : GameStateMachine<OreScrubber.States, OreScrubber.SMInstance, OreScrubber, object>.State
		{
			// Token: 0x04008DEF RID: 36335
			public GameStateMachine<OreScrubber.States, OreScrubber.SMInstance, OreScrubber, object>.State free;

			// Token: 0x04008DF0 RID: 36336
			public GameStateMachine<OreScrubber.States, OreScrubber.SMInstance, OreScrubber, object>.State occupied;
		}
	}

	// Token: 0x02001298 RID: 4760
	[AddComponentMenu("KMonoBehaviour/Workable/Work")]
	public class Work : Workable, IGameObjectEffectDescriptor
	{
		// Token: 0x06007AD8 RID: 31448 RVA: 0x002C95A2 File Offset: 0x002C77A2
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.resetProgressOnStop = true;
			this.shouldTransferDiseaseWithWorker = false;
		}

		// Token: 0x06007AD9 RID: 31449 RVA: 0x002C95B8 File Offset: 0x002C77B8
		protected override void OnStartWork(Worker worker)
		{
			base.OnStartWork(worker);
			this.diseaseRemoved = 0;
		}

		// Token: 0x06007ADA RID: 31450 RVA: 0x002C95C8 File Offset: 0x002C77C8
		protected override bool OnWorkTick(Worker worker, float dt)
		{
			base.OnWorkTick(worker, dt);
			OreScrubber component = base.GetComponent<OreScrubber>();
			Storage component2 = base.GetComponent<Storage>();
			PrimaryElement firstInfected = OreScrubber.GetFirstInfected(worker.GetComponent<Storage>());
			int num = 0;
			SimUtil.DiseaseInfo invalid = SimUtil.DiseaseInfo.Invalid;
			if (firstInfected != null)
			{
				num = Math.Min((int)(dt / this.workTime * (float)component.diseaseRemovalCount), firstInfected.DiseaseCount);
				this.diseaseRemoved += num;
				invalid.idx = firstInfected.DiseaseIdx;
				invalid.count = num;
				firstInfected.ModifyDiseaseCount(-num, "OreScrubber.OnWorkTick");
			}
			component.maxPossiblyRemoved += num;
			float num2 = component.massConsumedPerUse * dt / this.workTime;
			SimUtil.DiseaseInfo diseaseInfo = SimUtil.DiseaseInfo.Invalid;
			float num3;
			float num4;
			component2.ConsumeAndGetDisease(ElementLoader.FindElementByHash(component.consumedElement).tag, num2, out num3, out diseaseInfo, out num4);
			if (component.outputElement != SimHashes.Vacuum)
			{
				diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(invalid, diseaseInfo);
				component2.AddLiquid(component.outputElement, num3, num4, diseaseInfo.idx, diseaseInfo.count, false, true);
			}
			return this.diseaseRemoved > component.diseaseRemovalCount;
		}

		// Token: 0x06007ADB RID: 31451 RVA: 0x002C96E2 File Offset: 0x002C78E2
		protected override void OnCompleteWork(Worker worker)
		{
			base.OnCompleteWork(worker);
		}

		// Token: 0x04005E35 RID: 24117
		private int diseaseRemoved;
	}
}
