using System;
using System.Collections.Generic;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005CD RID: 1485
public class HandSanitizer : StateMachineComponent<HandSanitizer.SMInstance>, IGameObjectEffectDescriptor, IBasicBuilding
{
	// Token: 0x060024F2 RID: 9458 RVA: 0x000C7D53 File Offset: 0x000C5F53
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.FindOrAddComponent<Workable>();
	}

	// Token: 0x060024F3 RID: 9459 RVA: 0x000C7D68 File Offset: 0x000C5F68
	private void RefreshMeters()
	{
		float num = 0f;
		PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(this.consumedElement);
		float num2 = (float)this.maxUses * this.massConsumedPerUse;
		ConduitConsumer component = base.GetComponent<ConduitConsumer>();
		if (component != null)
		{
			num2 = component.capacityKG;
		}
		if (primaryElement != null)
		{
			num = Mathf.Clamp01(primaryElement.Mass / num2);
		}
		float num3 = 0f;
		PrimaryElement primaryElement2 = base.GetComponent<Storage>().FindPrimaryElement(this.outputElement);
		if (primaryElement2 != null)
		{
			num3 = Mathf.Clamp01(primaryElement2.Mass / ((float)this.maxUses * this.massConsumedPerUse));
		}
		this.cleanMeter.SetPositionPercent(num);
		this.dirtyMeter.SetPositionPercent(num3);
	}

	// Token: 0x060024F4 RID: 9460 RVA: 0x000C7E24 File Offset: 0x000C6024
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		this.cleanMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_clean_target", "meter_clean", this.cleanMeterOffset, Grid.SceneLayer.NoLayer, new string[] { "meter_clean_target" });
		this.dirtyMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_dirty_target", "meter_dirty", this.dirtyMeterOffset, Grid.SceneLayer.NoLayer, new string[] { "meter_dirty_target" });
		this.RefreshMeters();
		Components.HandSanitizers.Add(this);
		Components.BasicBuildings.Add(this);
		base.Subscribe<HandSanitizer>(-1697596308, HandSanitizer.OnStorageChangeDelegate);
		DirectionControl component = base.GetComponent<DirectionControl>();
		component.onDirectionChanged = (Action<WorkableReactable.AllowedDirection>)Delegate.Combine(component.onDirectionChanged, new Action<WorkableReactable.AllowedDirection>(this.OnDirectionChanged));
		this.OnDirectionChanged(base.GetComponent<DirectionControl>().allowedDirection);
	}

	// Token: 0x060024F5 RID: 9461 RVA: 0x000C7F09 File Offset: 0x000C6109
	protected override void OnCleanUp()
	{
		Components.BasicBuildings.Remove(this);
		Components.HandSanitizers.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x060024F6 RID: 9462 RVA: 0x000C7F27 File Offset: 0x000C6127
	private void OnDirectionChanged(WorkableReactable.AllowedDirection allowed_direction)
	{
		if (this.reactable != null)
		{
			this.reactable.allowedDirection = allowed_direction;
		}
	}

	// Token: 0x060024F7 RID: 9463 RVA: 0x000C7F40 File Offset: 0x000C6140
	public List<Descriptor> RequirementDescriptors()
	{
		return new List<Descriptor>
		{
			new Descriptor(string.Format(UI.BUILDINGEFFECTS.ELEMENTCONSUMEDPERUSE, ElementLoader.FindElementByHash(this.consumedElement).name, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.ELEMENTCONSUMEDPERUSE, ElementLoader.FindElementByHash(this.consumedElement).name, GameUtil.GetFormattedMass(this.massConsumedPerUse, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x060024F8 RID: 9464 RVA: 0x000C7FC4 File Offset: 0x000C61C4
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

	// Token: 0x060024F9 RID: 9465 RVA: 0x000C8099 File Offset: 0x000C6299
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.AddRange(this.RequirementDescriptors());
		list.AddRange(this.EffectDescriptors());
		return list;
	}

	// Token: 0x060024FA RID: 9466 RVA: 0x000C80B8 File Offset: 0x000C62B8
	private void OnStorageChange(object data)
	{
		if (this.dumpWhenFull && base.smi.OutputFull())
		{
			base.smi.DumpOutput();
		}
		this.RefreshMeters();
	}

	// Token: 0x0400154C RID: 5452
	public float massConsumedPerUse = 1f;

	// Token: 0x0400154D RID: 5453
	public SimHashes consumedElement = SimHashes.BleachStone;

	// Token: 0x0400154E RID: 5454
	public int diseaseRemovalCount = 10000;

	// Token: 0x0400154F RID: 5455
	public int maxUses = 10;

	// Token: 0x04001550 RID: 5456
	public SimHashes outputElement = SimHashes.Vacuum;

	// Token: 0x04001551 RID: 5457
	public bool dumpWhenFull;

	// Token: 0x04001552 RID: 5458
	public bool alwaysUse;

	// Token: 0x04001553 RID: 5459
	public bool canSanitizeSuit;

	// Token: 0x04001554 RID: 5460
	public bool canSanitizeStorage;

	// Token: 0x04001555 RID: 5461
	private WorkableReactable reactable;

	// Token: 0x04001556 RID: 5462
	private MeterController cleanMeter;

	// Token: 0x04001557 RID: 5463
	private MeterController dirtyMeter;

	// Token: 0x04001558 RID: 5464
	public Meter.Offset cleanMeterOffset;

	// Token: 0x04001559 RID: 5465
	public Meter.Offset dirtyMeterOffset;

	// Token: 0x0400155A RID: 5466
	[Serialize]
	public int maxPossiblyRemoved;

	// Token: 0x0400155B RID: 5467
	private static readonly EventSystem.IntraObjectHandler<HandSanitizer> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<HandSanitizer>(delegate(HandSanitizer component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x0200121C RID: 4636
	private class WashHandsReactable : WorkableReactable
	{
		// Token: 0x0600791B RID: 31003 RVA: 0x002C1E3E File Offset: 0x002C003E
		public WashHandsReactable(Workable workable, ChoreType chore_type, WorkableReactable.AllowedDirection allowed_direction = WorkableReactable.AllowedDirection.Any)
			: base(workable, "WashHands", chore_type, allowed_direction)
		{
		}

		// Token: 0x0600791C RID: 31004 RVA: 0x002C1E54 File Offset: 0x002C0054
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (base.InternalCanBegin(new_reactor, transition))
			{
				HandSanitizer component = this.workable.GetComponent<HandSanitizer>();
				if (!component.smi.IsReady())
				{
					return false;
				}
				if (component.alwaysUse)
				{
					return true;
				}
				PrimaryElement component2 = new_reactor.GetComponent<PrimaryElement>();
				if (component2 != null)
				{
					return component2.DiseaseIdx != byte.MaxValue;
				}
			}
			return false;
		}
	}

	// Token: 0x0200121D RID: 4637
	public class SMInstance : GameStateMachine<HandSanitizer.States, HandSanitizer.SMInstance, HandSanitizer, object>.GameInstance
	{
		// Token: 0x0600791D RID: 31005 RVA: 0x002C1EB2 File Offset: 0x002C00B2
		public SMInstance(HandSanitizer master)
			: base(master)
		{
		}

		// Token: 0x0600791E RID: 31006 RVA: 0x002C1EBC File Offset: 0x002C00BC
		private bool HasSufficientMass()
		{
			bool flag = false;
			PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(base.master.consumedElement);
			if (primaryElement != null)
			{
				flag = primaryElement.Mass >= base.master.massConsumedPerUse;
			}
			return flag;
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x002C1F04 File Offset: 0x002C0104
		public bool OutputFull()
		{
			PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(base.master.outputElement);
			return primaryElement != null && primaryElement.Mass >= (float)base.master.maxUses * base.master.massConsumedPerUse;
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x002C1F56 File Offset: 0x002C0156
		public bool IsReady()
		{
			return this.HasSufficientMass() && !this.OutputFull();
		}

		// Token: 0x06007921 RID: 31009 RVA: 0x002C1F70 File Offset: 0x002C0170
		public void DumpOutput()
		{
			Storage component = base.master.GetComponent<Storage>();
			if (base.master.outputElement != SimHashes.Vacuum)
			{
				component.Drop(ElementLoader.FindElementByHash(base.master.outputElement).tag);
			}
		}
	}

	// Token: 0x0200121E RID: 4638
	public class States : GameStateMachine<HandSanitizer.States, HandSanitizer.SMInstance, HandSanitizer>
	{
		// Token: 0x06007922 RID: 31010 RVA: 0x002C1FB8 File Offset: 0x002C01B8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.notready;
			this.root.Update(new Action<HandSanitizer.SMInstance, float>(this.UpdateStatusItems), UpdateRate.SIM_200ms, false);
			this.notoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.notready, false);
			this.notready.PlayAnim("off").EventTransition(GameHashes.OnStorageChange, this.ready, (HandSanitizer.SMInstance smi) => smi.IsReady()).TagTransition(GameTags.Operational, this.notoperational, true);
			this.ready.DefaultState(this.ready.free).ToggleReactable((HandSanitizer.SMInstance smi) => smi.master.reactable = new HandSanitizer.WashHandsReactable(smi.master.GetComponent<HandSanitizer.Work>(), Db.Get().ChoreTypes.WashHands, smi.master.GetComponent<DirectionControl>().allowedDirection)).TagTransition(GameTags.Operational, this.notoperational, true);
			this.ready.free.PlayAnim("on").WorkableStartTransition((HandSanitizer.SMInstance smi) => smi.GetComponent<HandSanitizer.Work>(), this.ready.occupied);
			this.ready.occupied.PlayAnim("working_pre").QueueAnim("working_loop", true, null).Enter(delegate(HandSanitizer.SMInstance smi)
			{
				ConduitConsumer component = smi.GetComponent<ConduitConsumer>();
				if (component != null)
				{
					component.enabled = false;
				}
			})
				.Exit(delegate(HandSanitizer.SMInstance smi)
				{
					ConduitConsumer component2 = smi.GetComponent<ConduitConsumer>();
					if (component2 != null)
					{
						component2.enabled = true;
					}
				})
				.WorkableStopTransition((HandSanitizer.SMInstance smi) => smi.GetComponent<HandSanitizer.Work>(), this.notready);
		}

		// Token: 0x06007923 RID: 31011 RVA: 0x002C2180 File Offset: 0x002C0380
		private void UpdateStatusItems(HandSanitizer.SMInstance smi, float dt)
		{
			if (smi.OutputFull())
			{
				smi.master.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, this);
				return;
			}
			smi.master.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, false);
		}

		// Token: 0x04005D1B RID: 23835
		public GameStateMachine<HandSanitizer.States, HandSanitizer.SMInstance, HandSanitizer, object>.State notready;

		// Token: 0x04005D1C RID: 23836
		public HandSanitizer.States.ReadyStates ready;

		// Token: 0x04005D1D RID: 23837
		public GameStateMachine<HandSanitizer.States, HandSanitizer.SMInstance, HandSanitizer, object>.State notoperational;

		// Token: 0x04005D1E RID: 23838
		public GameStateMachine<HandSanitizer.States, HandSanitizer.SMInstance, HandSanitizer, object>.State full;

		// Token: 0x04005D1F RID: 23839
		public GameStateMachine<HandSanitizer.States, HandSanitizer.SMInstance, HandSanitizer, object>.State empty;

		// Token: 0x02001FB2 RID: 8114
		public class ReadyStates : GameStateMachine<HandSanitizer.States, HandSanitizer.SMInstance, HandSanitizer, object>.State
		{
			// Token: 0x04008D2C RID: 36140
			public GameStateMachine<HandSanitizer.States, HandSanitizer.SMInstance, HandSanitizer, object>.State free;

			// Token: 0x04008D2D RID: 36141
			public GameStateMachine<HandSanitizer.States, HandSanitizer.SMInstance, HandSanitizer, object>.State occupied;
		}
	}

	// Token: 0x0200121F RID: 4639
	[AddComponentMenu("KMonoBehaviour/Workable/Work")]
	public class Work : Workable, IGameObjectEffectDescriptor
	{
		// Token: 0x06007925 RID: 31013 RVA: 0x002C21E0 File Offset: 0x002C03E0
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.resetProgressOnStop = true;
			this.shouldTransferDiseaseWithWorker = false;
			GameScheduler.Instance.Schedule("WaterFetchingTutorial", 2f, delegate(object obj)
			{
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_FetchingWater, true);
			}, null, null);
		}

		// Token: 0x06007926 RID: 31014 RVA: 0x002C2237 File Offset: 0x002C0437
		protected override void OnStartWork(Worker worker)
		{
			base.OnStartWork(worker);
			this.diseaseRemoved = 0;
		}

		// Token: 0x06007927 RID: 31015 RVA: 0x002C2248 File Offset: 0x002C0448
		protected override bool OnWorkTick(Worker worker, float dt)
		{
			base.OnWorkTick(worker, dt);
			HandSanitizer component = base.GetComponent<HandSanitizer>();
			Storage component2 = base.GetComponent<Storage>();
			float massAvailable = component2.GetMassAvailable(component.consumedElement);
			if (massAvailable == 0f)
			{
				return true;
			}
			PrimaryElement component3 = worker.GetComponent<PrimaryElement>();
			float num = Mathf.Min(component.massConsumedPerUse * dt / this.workTime, massAvailable);
			int num2 = Math.Min((int)(dt / this.workTime * (float)component.diseaseRemovalCount), component3.DiseaseCount);
			this.diseaseRemoved += num2;
			SimUtil.DiseaseInfo invalid = SimUtil.DiseaseInfo.Invalid;
			invalid.idx = component3.DiseaseIdx;
			invalid.count = num2;
			component3.ModifyDiseaseCount(-num2, "HandSanitizer.OnWorkTick");
			component.maxPossiblyRemoved += num2;
			if (component.canSanitizeStorage && worker.GetComponent<Storage>())
			{
				foreach (GameObject gameObject in worker.GetComponent<Storage>().GetItems())
				{
					PrimaryElement component4 = gameObject.GetComponent<PrimaryElement>();
					if (component4)
					{
						int num3 = Math.Min((int)(dt / this.workTime * (float)component.diseaseRemovalCount), component4.DiseaseCount);
						component4.ModifyDiseaseCount(-num3, "HandSanitizer.OnWorkTick");
						component.maxPossiblyRemoved += num3;
					}
				}
			}
			SimUtil.DiseaseInfo diseaseInfo = SimUtil.DiseaseInfo.Invalid;
			float num4;
			float num5;
			component2.ConsumeAndGetDisease(ElementLoader.FindElementByHash(component.consumedElement).tag, num, out num4, out diseaseInfo, out num5);
			if (component.outputElement != SimHashes.Vacuum)
			{
				diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(invalid, diseaseInfo);
				component2.AddLiquid(component.outputElement, num4, num5, diseaseInfo.idx, diseaseInfo.count, false, true);
			}
			return false;
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x002C2414 File Offset: 0x002C0614
		protected override void OnCompleteWork(Worker worker)
		{
			base.OnCompleteWork(worker);
			if (this.removeIrritation && !worker.HasTag(GameTags.HasSuitTank))
			{
				GasLiquidExposureMonitor.Instance smi = worker.GetSMI<GasLiquidExposureMonitor.Instance>();
				if (smi != null)
				{
					smi.ResetExposure();
				}
			}
		}

		// Token: 0x04005D20 RID: 23840
		public bool removeIrritation;

		// Token: 0x04005D21 RID: 23841
		private int diseaseRemoved;
	}
}
