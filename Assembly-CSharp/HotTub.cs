using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007AD RID: 1965
[SerializationConfig(MemberSerialization.OptIn)]
public class HotTub : StateMachineComponent<HotTub.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x17000413 RID: 1043
	// (get) Token: 0x060037AB RID: 14251 RVA: 0x00135228 File Offset: 0x00133428
	public float PercentFull
	{
		get
		{
			return 100f * this.waterStorage.GetMassAvailable(SimHashes.Water) / this.hotTubCapacity;
		}
	}

	// Token: 0x060037AC RID: 14252 RVA: 0x00135248 File Offset: 0x00133448
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("Scheduling Tutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Schedule, true);
		}, null, null);
		this.workables = new HotTubWorkable[this.choreOffsets.Length];
		this.chores = new Chore[this.choreOffsets.Length];
		for (int i = 0; i < this.workables.Length; i++)
		{
			Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(this), this.choreOffsets[i]), Grid.SceneLayer.Move);
			GameObject gameObject = ChoreHelpers.CreateLocator("HotTubWorkable", vector);
			KSelectable kselectable = gameObject.AddOrGet<KSelectable>();
			kselectable.SetName(this.GetProperName());
			kselectable.IsSelectable = false;
			HotTubWorkable hotTubWorkable = gameObject.AddOrGet<HotTubWorkable>();
			int player_index = i;
			HotTubWorkable hotTubWorkable2 = hotTubWorkable;
			hotTubWorkable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(hotTubWorkable2.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(delegate(Workable workable, Workable.WorkableEvent ev)
			{
				this.OnWorkableEvent(player_index, ev);
			}));
			this.workables[i] = hotTubWorkable;
			this.workables[i].hotTub = this;
		}
		this.waterMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_water_target", "meter_water", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_water_target" });
		base.smi.UpdateWaterMeter();
		this.tempMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_temperature_target", "meter_temp", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_temperature_target" });
		base.smi.TestWaterTemperature();
		base.smi.StartSM();
	}

	// Token: 0x060037AD RID: 14253 RVA: 0x001353E0 File Offset: 0x001335E0
	protected override void OnCleanUp()
	{
		this.UpdateChores(false);
		for (int i = 0; i < this.workables.Length; i++)
		{
			if (this.workables[i])
			{
				Util.KDestroyGameObject(this.workables[i]);
				this.workables[i] = null;
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x060037AE RID: 14254 RVA: 0x00135434 File Offset: 0x00133634
	private Chore CreateChore(int i)
	{
		Workable workable = this.workables[i];
		ChoreType relax = Db.Get().ChoreTypes.Relax;
		IStateMachineTarget stateMachineTarget = workable;
		ChoreProvider choreProvider = null;
		bool flag = true;
		Action<Chore> action = null;
		Action<Chore> action2 = null;
		ScheduleBlockType recreation = Db.Get().ScheduleBlockTypes.Recreation;
		WorkChore<HotTubWorkable> workChore = new WorkChore<HotTubWorkable>(relax, stateMachineTarget, choreProvider, flag, action, action2, new Action<Chore>(this.OnSocialChoreEnd), false, recreation, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, workable);
		return workChore;
	}

	// Token: 0x060037AF RID: 14255 RVA: 0x0013549C File Offset: 0x0013369C
	private void OnSocialChoreEnd(Chore chore)
	{
		if (base.gameObject.HasTag(GameTags.Operational))
		{
			this.UpdateChores(true);
		}
	}

	// Token: 0x060037B0 RID: 14256 RVA: 0x001354B8 File Offset: 0x001336B8
	public void UpdateChores(bool update = true)
	{
		for (int i = 0; i < this.choreOffsets.Length; i++)
		{
			Chore chore = this.chores[i];
			if (update)
			{
				if (chore == null || chore.isComplete)
				{
					this.chores[i] = this.CreateChore(i);
				}
			}
			else if (chore != null)
			{
				chore.Cancel("locator invalidated");
				this.chores[i] = null;
			}
		}
	}

	// Token: 0x060037B1 RID: 14257 RVA: 0x00135518 File Offset: 0x00133718
	public void OnWorkableEvent(int player, Workable.WorkableEvent ev)
	{
		if (ev == Workable.WorkableEvent.WorkStarted)
		{
			this.occupants.Add(player);
		}
		else
		{
			this.occupants.Remove(player);
		}
		base.smi.sm.userCount.Set(this.occupants.Count, base.smi, false);
	}

	// Token: 0x060037B2 RID: 14258 RVA: 0x00135570 File Offset: 0x00133770
	List<Descriptor> IGameObjectEffectDescriptor.GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Element element = ElementLoader.FindElementByHash(SimHashes.Water);
		list.Add(new Descriptor(BUILDINGS.PREFABS.HOTTUB.WATER_REQUIREMENT.Replace("{element}", element.name).Replace("{amount}", GameUtil.GetFormattedMass(this.hotTubCapacity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), BUILDINGS.PREFABS.HOTTUB.WATER_REQUIREMENT_TOOLTIP.Replace("{element}", element.name).Replace("{amount}", GameUtil.GetFormattedMass(this.hotTubCapacity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		list.Add(new Descriptor(BUILDINGS.PREFABS.HOTTUB.TEMPERATURE_REQUIREMENT.Replace("{element}", element.name).Replace("{temperature}", GameUtil.GetFormattedTemperature(this.minimumWaterTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), BUILDINGS.PREFABS.HOTTUB.TEMPERATURE_REQUIREMENT_TOOLTIP.Replace("{element}", element.name).Replace("{temperature}", GameUtil.GetFormattedTemperature(this.minimumWaterTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Requirement, false));
		list.Add(new Descriptor(UI.BUILDINGEFFECTS.RECREATION, UI.BUILDINGEFFECTS.TOOLTIPS.RECREATION, Descriptor.DescriptorType.Effect, false));
		Effect.AddModifierDescriptions(base.gameObject, list, this.specificEffect, true);
		return list;
	}

	// Token: 0x04002548 RID: 9544
	public string specificEffect;

	// Token: 0x04002549 RID: 9545
	public string trackingEffect;

	// Token: 0x0400254A RID: 9546
	public int basePriority;

	// Token: 0x0400254B RID: 9547
	public CellOffset[] choreOffsets = new CellOffset[]
	{
		new CellOffset(-1, 0),
		new CellOffset(1, 0),
		new CellOffset(0, 0),
		new CellOffset(2, 0)
	};

	// Token: 0x0400254C RID: 9548
	private HotTubWorkable[] workables;

	// Token: 0x0400254D RID: 9549
	private Chore[] chores;

	// Token: 0x0400254E RID: 9550
	public HashSet<int> occupants = new HashSet<int>();

	// Token: 0x0400254F RID: 9551
	public float waterCoolingRate;

	// Token: 0x04002550 RID: 9552
	public float hotTubCapacity = 100f;

	// Token: 0x04002551 RID: 9553
	public float minimumWaterTemperature;

	// Token: 0x04002552 RID: 9554
	public float bleachStoneConsumption;

	// Token: 0x04002553 RID: 9555
	public float maxOperatingTemperature;

	// Token: 0x04002554 RID: 9556
	[MyCmpGet]
	public Storage waterStorage;

	// Token: 0x04002555 RID: 9557
	private MeterController waterMeter;

	// Token: 0x04002556 RID: 9558
	private MeterController tempMeter;

	// Token: 0x02001512 RID: 5394
	public class States : GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub>
	{
		// Token: 0x06008293 RID: 33427 RVA: 0x002E5100 File Offset: 0x002E3300
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.ready;
			this.root.Update(delegate(HotTub.StatesInstance smi, float dt)
			{
				smi.SapHeatFromWater(dt);
				smi.TestWaterTemperature();
			}, UpdateRate.SIM_4000ms, false).EventHandler(GameHashes.OnStorageChange, delegate(HotTub.StatesInstance smi)
			{
				smi.UpdateWaterMeter();
				smi.TestWaterTemperature();
			});
			this.unoperational.TagTransition(GameTags.Operational, this.off, false).PlayAnim("off");
			this.off.TagTransition(GameTags.Operational, this.unoperational, true).DefaultState(this.off.filling);
			this.off.filling.DefaultState(this.off.filling.normal).Transition(this.ready, (HotTub.StatesInstance smi) => smi.master.waterStorage.GetMassAvailable(SimHashes.Water) >= smi.master.hotTubCapacity, UpdateRate.SIM_200ms).PlayAnim("off")
				.Enter(delegate(HotTub.StatesInstance smi)
				{
					smi.GetComponent<ConduitConsumer>().SetOnState(true);
				})
				.Exit(delegate(HotTub.StatesInstance smi)
				{
					smi.GetComponent<ConduitConsumer>().SetOnState(false);
				})
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.HotTubFilling, (HotTub.StatesInstance smi) => smi.master);
			this.off.filling.normal.ParamTransition<bool>(this.waterTooCold, this.off.filling.too_cold, GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.IsTrue);
			this.off.filling.too_cold.ParamTransition<bool>(this.waterTooCold, this.off.filling.normal, GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.IsFalse).ToggleStatusItem(Db.Get().BuildingStatusItems.HotTubWaterTooCold, (HotTub.StatesInstance smi) => smi.master);
			this.off.draining.Transition(this.off.filling, (HotTub.StatesInstance smi) => smi.master.waterStorage.GetMassAvailable(SimHashes.Water) <= 0f, UpdateRate.SIM_200ms).ToggleMainStatusItem(Db.Get().BuildingStatusItems.HotTubWaterTooCold, (HotTub.StatesInstance smi) => smi.master).PlayAnim("off")
				.Enter(delegate(HotTub.StatesInstance smi)
				{
					smi.GetComponent<ConduitDispenser>().SetOnState(true);
				})
				.Exit(delegate(HotTub.StatesInstance smi)
				{
					smi.GetComponent<ConduitDispenser>().SetOnState(false);
				});
			this.off.too_hot.Transition(this.ready, (HotTub.StatesInstance smi) => !smi.IsTubTooHot(), UpdateRate.SIM_200ms).PlayAnim("overheated").ToggleMainStatusItem(Db.Get().BuildingStatusItems.HotTubTooHot, (HotTub.StatesInstance smi) => smi.master);
			this.off.awaiting_delivery.EventTransition(GameHashes.OnStorageChange, this.ready, (HotTub.StatesInstance smi) => smi.HasBleachStone());
			this.ready.DefaultState(this.ready.idle).Enter("CreateChore", delegate(HotTub.StatesInstance smi)
			{
				smi.master.UpdateChores(true);
			}).Exit("CancelChore", delegate(HotTub.StatesInstance smi)
			{
				smi.master.UpdateChores(false);
			})
				.TagTransition(GameTags.Operational, this.unoperational, true)
				.ParamTransition<bool>(this.waterTooCold, this.off.draining, GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.IsTrue)
				.EventTransition(GameHashes.OnStorageChange, this.off.awaiting_delivery, (HotTub.StatesInstance smi) => !smi.HasBleachStone())
				.Transition(this.off.filling, (HotTub.StatesInstance smi) => smi.master.waterStorage.IsEmpty(), UpdateRate.SIM_200ms)
				.Transition(this.off.too_hot, (HotTub.StatesInstance smi) => smi.IsTubTooHot(), UpdateRate.SIM_200ms)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.Normal, null);
			this.ready.idle.PlayAnim("on").ParamTransition<int>(this.userCount, this.ready.on.pre, (HotTub.StatesInstance smi, int p) => p > 0);
			this.ready.on.Enter(delegate(HotTub.StatesInstance smi)
			{
				smi.SetActive(true);
			}).Update(delegate(HotTub.StatesInstance smi, float dt)
			{
				smi.ConsumeBleachstone(dt);
			}, UpdateRate.SIM_4000ms, false).Exit(delegate(HotTub.StatesInstance smi)
			{
				smi.SetActive(false);
			});
			this.ready.on.pre.PlayAnim("working_pre").OnAnimQueueComplete(this.ready.on.relaxing);
			this.ready.on.relaxing.PlayAnim("working_loop", KAnim.PlayMode.Loop).ParamTransition<int>(this.userCount, this.ready.on.post, (HotTub.StatesInstance smi, int p) => p == 0).ParamTransition<int>(this.userCount, this.ready.on.relaxing_together, (HotTub.StatesInstance smi, int p) => p > 1);
			this.ready.on.relaxing_together.PlayAnim("working_loop", KAnim.PlayMode.Loop).ParamTransition<int>(this.userCount, this.ready.on.post, (HotTub.StatesInstance smi, int p) => p == 0).ParamTransition<int>(this.userCount, this.ready.on.relaxing, (HotTub.StatesInstance smi, int p) => p == 1);
			this.ready.on.post.PlayAnim("working_pst").OnAnimQueueComplete(this.ready.idle);
		}

		// Token: 0x06008294 RID: 33428 RVA: 0x002E5810 File Offset: 0x002E3A10
		private string GetRelaxingAnim(HotTub.StatesInstance smi)
		{
			bool flag = smi.master.occupants.Contains(0);
			bool flag2 = smi.master.occupants.Contains(1);
			if (flag && !flag2)
			{
				return "working_loop_one_p";
			}
			if (flag2 && !flag)
			{
				return "working_loop_two_p";
			}
			return "working_loop_coop_p";
		}

		// Token: 0x04006582 RID: 25986
		public StateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.IntParameter userCount;

		// Token: 0x04006583 RID: 25987
		public StateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.BoolParameter waterTooCold;

		// Token: 0x04006584 RID: 25988
		public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State unoperational;

		// Token: 0x04006585 RID: 25989
		public HotTub.States.OffStates off;

		// Token: 0x04006586 RID: 25990
		public HotTub.States.ReadyStates ready;

		// Token: 0x02002069 RID: 8297
		public class OffStates : GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State
		{
			// Token: 0x0400906B RID: 36971
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State draining;

			// Token: 0x0400906C RID: 36972
			public HotTub.States.FillingStates filling;

			// Token: 0x0400906D RID: 36973
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State too_hot;

			// Token: 0x0400906E RID: 36974
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State awaiting_delivery;
		}

		// Token: 0x0200206A RID: 8298
		public class OnStates : GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State
		{
			// Token: 0x0400906F RID: 36975
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State pre;

			// Token: 0x04009070 RID: 36976
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State relaxing;

			// Token: 0x04009071 RID: 36977
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State relaxing_together;

			// Token: 0x04009072 RID: 36978
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State post;
		}

		// Token: 0x0200206B RID: 8299
		public class ReadyStates : GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State
		{
			// Token: 0x04009073 RID: 36979
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State idle;

			// Token: 0x04009074 RID: 36980
			public HotTub.States.OnStates on;
		}

		// Token: 0x0200206C RID: 8300
		public class FillingStates : GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State
		{
			// Token: 0x04009075 RID: 36981
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State normal;

			// Token: 0x04009076 RID: 36982
			public GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.State too_cold;
		}
	}

	// Token: 0x02001513 RID: 5395
	public class StatesInstance : GameStateMachine<HotTub.States, HotTub.StatesInstance, HotTub, object>.GameInstance
	{
		// Token: 0x06008296 RID: 33430 RVA: 0x002E5866 File Offset: 0x002E3A66
		public StatesInstance(HotTub smi)
			: base(smi)
		{
			this.operational = base.master.GetComponent<Operational>();
		}

		// Token: 0x06008297 RID: 33431 RVA: 0x002E5880 File Offset: 0x002E3A80
		public void SetActive(bool active)
		{
			this.operational.SetActive(this.operational.IsOperational && active, false);
		}

		// Token: 0x06008298 RID: 33432 RVA: 0x002E589C File Offset: 0x002E3A9C
		public void UpdateWaterMeter()
		{
			base.smi.master.waterMeter.SetPositionPercent(Mathf.Clamp(base.smi.master.waterStorage.GetMassAvailable(SimHashes.Water) / base.smi.master.hotTubCapacity, 0f, 1f));
		}

		// Token: 0x06008299 RID: 33433 RVA: 0x002E58F8 File Offset: 0x002E3AF8
		public void UpdateTemperatureMeter(float waterTemp)
		{
			Element element = ElementLoader.GetElement(SimHashes.Water.CreateTag());
			base.smi.master.tempMeter.SetPositionPercent(Mathf.Clamp((waterTemp - base.smi.master.minimumWaterTemperature) / (element.highTemp - base.smi.master.minimumWaterTemperature), 0f, 1f));
		}

		// Token: 0x0600829A RID: 33434 RVA: 0x002E5964 File Offset: 0x002E3B64
		public void TestWaterTemperature()
		{
			GameObject gameObject = base.smi.master.waterStorage.FindFirst(new Tag(1836671383));
			float num = 0f;
			if (!gameObject)
			{
				this.UpdateTemperatureMeter(num);
				base.smi.sm.waterTooCold.Set(false, base.smi, false);
				return;
			}
			num = gameObject.GetComponent<PrimaryElement>().Temperature;
			this.UpdateTemperatureMeter(num);
			if (num < base.smi.master.minimumWaterTemperature)
			{
				base.smi.sm.waterTooCold.Set(true, base.smi, false);
				return;
			}
			base.smi.sm.waterTooCold.Set(false, base.smi, false);
		}

		// Token: 0x0600829B RID: 33435 RVA: 0x002E5A28 File Offset: 0x002E3C28
		public bool IsTubTooHot()
		{
			return base.smi.master.GetComponent<PrimaryElement>().Temperature > base.smi.master.maxOperatingTemperature;
		}

		// Token: 0x0600829C RID: 33436 RVA: 0x002E5A54 File Offset: 0x002E3C54
		public bool HasBleachStone()
		{
			GameObject gameObject = base.smi.master.waterStorage.FindFirst(new Tag(-839728230));
			return gameObject != null && gameObject.GetComponent<PrimaryElement>().Mass > 0f;
		}

		// Token: 0x0600829D RID: 33437 RVA: 0x002E5AA0 File Offset: 0x002E3CA0
		public void SapHeatFromWater(float dt)
		{
			float num = base.smi.master.waterCoolingRate * dt / (float)base.smi.master.waterStorage.items.Count;
			foreach (GameObject gameObject in base.smi.master.waterStorage.items)
			{
				GameUtil.DeltaThermalEnergy(gameObject.GetComponent<PrimaryElement>(), -num, base.smi.master.minimumWaterTemperature);
				GameUtil.DeltaThermalEnergy(base.GetComponent<PrimaryElement>(), num, base.GetComponent<PrimaryElement>().Element.highTemp);
			}
		}

		// Token: 0x0600829E RID: 33438 RVA: 0x002E5B64 File Offset: 0x002E3D64
		public void ConsumeBleachstone(float dt)
		{
			base.smi.master.waterStorage.ConsumeIgnoringDisease(new Tag(-839728230), base.smi.master.bleachStoneConsumption * dt);
		}

		// Token: 0x04006587 RID: 25991
		private Operational operational;
	}
}
