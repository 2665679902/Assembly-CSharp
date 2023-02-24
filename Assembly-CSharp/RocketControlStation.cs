using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000639 RID: 1593
public class RocketControlStation : StateMachineComponent<RocketControlStation.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000DEF8F File Offset: 0x000DD18F
	// (set) Token: 0x06002A2C RID: 10796 RVA: 0x000DEF97 File Offset: 0x000DD197
	public bool RestrictWhenGrounded
	{
		get
		{
			return this.m_restrictWhenGrounded;
		}
		set
		{
			this.m_restrictWhenGrounded = value;
			base.Trigger(1861523068, null);
		}
	}

	// Token: 0x06002A2D RID: 10797 RVA: 0x000DEFAC File Offset: 0x000DD1AC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		Components.RocketControlStations.Add(this);
		base.Subscribe<RocketControlStation>(-801688580, RocketControlStation.OnLogicValueChangedDelegate);
		base.Subscribe<RocketControlStation>(1861523068, RocketControlStation.OnRocketRestrictionChanged);
		this.UpdateRestrictionAnimSymbol(null);
	}

	// Token: 0x06002A2E RID: 10798 RVA: 0x000DEFFE File Offset: 0x000DD1FE
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.RocketControlStations.Remove(this);
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06002A2F RID: 10799 RVA: 0x000DF014 File Offset: 0x000DD214
	public bool BuildingRestrictionsActive
	{
		get
		{
			if (this.IsLogicInputConnected())
			{
				return this.m_logicUsageRestrictionState;
			}
			base.smi.sm.AquireClustercraft(base.smi);
			GameObject gameObject = base.smi.sm.clusterCraft.Get(base.smi);
			return this.RestrictWhenGrounded && gameObject != null && gameObject.gameObject.HasTag(GameTags.RocketOnGround);
		}
	}

	// Token: 0x06002A30 RID: 10800 RVA: 0x000DF085 File Offset: 0x000DD285
	public bool IsLogicInputConnected()
	{
		return this.GetNetwork() != null;
	}

	// Token: 0x06002A31 RID: 10801 RVA: 0x000DF090 File Offset: 0x000DD290
	public void OnLogicValueChanged(object data)
	{
		if (((LogicValueChanged)data).portID == RocketControlStation.PORT_ID)
		{
			LogicCircuitNetwork network = this.GetNetwork();
			int num = ((network != null) ? network.OutputValue : 1);
			bool flag = LogicCircuitNetwork.IsBitActive(0, num);
			this.m_logicUsageRestrictionState = flag;
			base.Trigger(1861523068, null);
		}
	}

	// Token: 0x06002A32 RID: 10802 RVA: 0x000DF0E3 File Offset: 0x000DD2E3
	public void OnTagsChanged(object obj)
	{
		if (((TagChangedEventData)obj).tag == GameTags.RocketOnGround)
		{
			base.Trigger(1861523068, null);
		}
	}

	// Token: 0x06002A33 RID: 10803 RVA: 0x000DF108 File Offset: 0x000DD308
	private LogicCircuitNetwork GetNetwork()
	{
		int portCell = base.GetComponent<LogicPorts>().GetPortCell(RocketControlStation.PORT_ID);
		return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
	}

	// Token: 0x06002A34 RID: 10804 RVA: 0x000DF136 File Offset: 0x000DD336
	private void UpdateRestrictionAnimSymbol(object o = null)
	{
		base.GetComponent<KAnimControllerBase>().SetSymbolVisiblity("restriction_sign", this.BuildingRestrictionsActive);
	}

	// Token: 0x06002A35 RID: 10805 RVA: 0x000DF154 File Offset: 0x000DD354
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		list.Add(new Descriptor(UI.BUILDINGEFFECTS.ROCKETRESTRICTION_HEADER, UI.BUILDINGEFFECTS.TOOLTIPS.ROCKETRESTRICTION_HEADER, Descriptor.DescriptorType.Effect, false));
		string text = string.Join(", ", RocketControlStation.CONTROLLED_BUILDINGS.Select((Tag t) => Strings.Get("STRINGS.BUILDINGS.PREFABS." + t.Name.ToUpper() + ".NAME").String).ToArray<string>());
		list.Add(new Descriptor(UI.BUILDINGEFFECTS.ROCKETRESTRICTION_BUILDINGS.text.Replace("{buildinglist}", text), UI.BUILDINGEFFECTS.TOOLTIPS.ROCKETRESTRICTION_BUILDINGS.text.Replace("{buildinglist}", text), Descriptor.DescriptorType.Effect, false));
		return list;
	}

	// Token: 0x040018EE RID: 6382
	public static List<Tag> CONTROLLED_BUILDINGS = new List<Tag>();

	// Token: 0x040018EF RID: 6383
	private const int UNNETWORKED_VALUE = 1;

	// Token: 0x040018F0 RID: 6384
	[Serialize]
	public float TimeRemaining;

	// Token: 0x040018F1 RID: 6385
	private bool m_logicUsageRestrictionState;

	// Token: 0x040018F2 RID: 6386
	[Serialize]
	private bool m_restrictWhenGrounded;

	// Token: 0x040018F3 RID: 6387
	public static readonly HashedString PORT_ID = "LogicUsageRestriction";

	// Token: 0x040018F4 RID: 6388
	private static readonly EventSystem.IntraObjectHandler<RocketControlStation> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<RocketControlStation>(delegate(RocketControlStation component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x040018F5 RID: 6389
	private static readonly EventSystem.IntraObjectHandler<RocketControlStation> OnRocketRestrictionChanged = new EventSystem.IntraObjectHandler<RocketControlStation>(delegate(RocketControlStation component, object data)
	{
		component.UpdateRestrictionAnimSymbol(data);
	});

	// Token: 0x020012D0 RID: 4816
	public class States : GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation>
	{
		// Token: 0x06007B9D RID: 31645 RVA: 0x002CC394 File Offset: 0x002CA594
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			default_state = this.unoperational;
			this.root.Enter("SetTarget", new StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State.Callback(this.AquireClustercraft)).Target(this.masterTarget).Exit(delegate(RocketControlStation.StatesInstance smi)
			{
				this.SetRocketSpeedModifiers(smi, 0.5f, 1f);
			});
			this.unoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.operational, false);
			this.operational.Enter(delegate(RocketControlStation.StatesInstance smi)
			{
				this.SetRocketSpeedModifiers(smi, 1f, smi.pilotSpeedMult);
			}).PlayAnim("on").TagTransition(GameTags.Operational, this.unoperational, true)
				.Transition(this.ready, new StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Transition.ConditionCallback(this.IsInFlight), UpdateRate.SIM_4000ms)
				.Target(this.clusterCraft)
				.EventTransition(GameHashes.RocketRequestLaunch, this.launch, new StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Transition.ConditionCallback(this.RocketReadyForLaunch))
				.EventTransition(GameHashes.LaunchConditionChanged, this.launch, new StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Transition.ConditionCallback(this.RocketReadyForLaunch))
				.Target(this.masterTarget)
				.Exit(delegate(RocketControlStation.StatesInstance smi)
				{
					this.timeRemaining.Set(120f, smi, false);
				});
			this.launch.Enter(delegate(RocketControlStation.StatesInstance smi)
			{
				this.SetRocketSpeedModifiers(smi, 1f, smi.pilotSpeedMult);
			}).ToggleChore(new Func<RocketControlStation.StatesInstance, Chore>(this.CreateLaunchChore), this.operational).Transition(this.launch.fadein, new StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Transition.ConditionCallback(this.IsInFlight), UpdateRate.SIM_200ms)
				.Target(this.clusterCraft)
				.EventTransition(GameHashes.RocketRequestLaunch, this.operational, GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Not(new StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Transition.ConditionCallback(this.RocketReadyForLaunch)))
				.EventTransition(GameHashes.LaunchConditionChanged, this.operational, GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Not(new StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Transition.ConditionCallback(this.RocketReadyForLaunch)))
				.Target(this.masterTarget);
			this.launch.fadein.Enter(delegate(RocketControlStation.StatesInstance smi)
			{
				if (CameraController.Instance.cameraActiveCluster == this.clusterCraft.Get(smi).GetComponent<WorldContainer>().id)
				{
					CameraController.Instance.FadeIn(0f, 1f, null);
				}
			});
			this.running.PlayAnim("on").TagTransition(GameTags.Operational, this.unoperational, true).Transition(this.operational, GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Not(new StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Transition.ConditionCallback(this.IsInFlight)), UpdateRate.SIM_200ms)
				.ParamTransition<float>(this.timeRemaining, this.ready, (RocketControlStation.StatesInstance smi, float p) => p <= 0f)
				.Enter(delegate(RocketControlStation.StatesInstance smi)
				{
					this.SetRocketSpeedModifiers(smi, 1f, smi.pilotSpeedMult);
				})
				.Update("Decrement time", new Action<RocketControlStation.StatesInstance, float>(this.DecrementTime), UpdateRate.SIM_200ms, false)
				.Exit(delegate(RocketControlStation.StatesInstance smi)
				{
					this.timeRemaining.Set(30f, smi, false);
				});
			this.ready.TagTransition(GameTags.Operational, this.unoperational, true).DefaultState(this.ready.idle).ToggleChore(new Func<RocketControlStation.StatesInstance, Chore>(this.CreateChore), this.ready.post, this.ready)
				.Transition(this.operational, GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Not(new StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Transition.ConditionCallback(this.IsInFlight)), UpdateRate.SIM_200ms)
				.OnSignal(this.pilotSuccessful, this.ready.post)
				.Update("Decrement time", new Action<RocketControlStation.StatesInstance, float>(this.DecrementTime), UpdateRate.SIM_200ms, false);
			this.ready.idle.PlayAnim("on", KAnim.PlayMode.Loop).WorkableStartTransition((RocketControlStation.StatesInstance smi) => smi.master.GetComponent<RocketControlStationIdleWorkable>(), this.ready.working).ParamTransition<float>(this.timeRemaining, this.ready.warning, (RocketControlStation.StatesInstance smi, float p) => p <= 15f);
			this.ready.warning.PlayAnim("on_alert", KAnim.PlayMode.Loop).WorkableStartTransition((RocketControlStation.StatesInstance smi) => smi.master.GetComponent<RocketControlStationIdleWorkable>(), this.ready.working).ToggleMainStatusItem(Db.Get().BuildingStatusItems.PilotNeeded, null)
				.ParamTransition<float>(this.timeRemaining, this.ready.autopilot, (RocketControlStation.StatesInstance smi, float p) => p <= 0f);
			this.ready.autopilot.PlayAnim("on_failed", KAnim.PlayMode.Loop).ToggleMainStatusItem(Db.Get().BuildingStatusItems.AutoPilotActive, null).WorkableStartTransition((RocketControlStation.StatesInstance smi) => smi.master.GetComponent<RocketControlStationIdleWorkable>(), this.ready.working)
				.Enter(delegate(RocketControlStation.StatesInstance smi)
				{
					this.SetRocketSpeedModifiers(smi, 0.5f, smi.pilotSpeedMult);
				});
			this.ready.working.PlayAnim("working_pre").QueueAnim("working_loop", true, null).Enter(delegate(RocketControlStation.StatesInstance smi)
			{
				this.SetRocketSpeedModifiers(smi, 1f, smi.pilotSpeedMult);
			})
				.WorkableStopTransition((RocketControlStation.StatesInstance smi) => smi.master.GetComponent<RocketControlStationIdleWorkable>(), this.ready.idle);
			this.ready.post.PlayAnim("working_pst").OnAnimQueueComplete(this.running).Exit(delegate(RocketControlStation.StatesInstance smi)
			{
				this.timeRemaining.Set(120f, smi, false);
			});
		}

		// Token: 0x06007B9E RID: 31646 RVA: 0x002CC8C0 File Offset: 0x002CAAC0
		public void AquireClustercraft(RocketControlStation.StatesInstance smi)
		{
			if (this.clusterCraft.IsNull(smi))
			{
				GameObject rocket = this.GetRocket(smi);
				this.clusterCraft.Set(rocket, smi, false);
				if (rocket != null)
				{
					rocket.Subscribe(-1582839653, new Action<object>(smi.master.OnTagsChanged));
				}
			}
		}

		// Token: 0x06007B9F RID: 31647 RVA: 0x002CC918 File Offset: 0x002CAB18
		private void DecrementTime(RocketControlStation.StatesInstance smi, float dt)
		{
			this.timeRemaining.Delta(-dt, smi);
		}

		// Token: 0x06007BA0 RID: 31648 RVA: 0x002CC92C File Offset: 0x002CAB2C
		private bool RocketReadyForLaunch(RocketControlStation.StatesInstance smi)
		{
			Clustercraft component = this.clusterCraft.Get(smi).GetComponent<Clustercraft>();
			return component.LaunchRequested && component.CheckReadyToLaunch();
		}

		// Token: 0x06007BA1 RID: 31649 RVA: 0x002CC95C File Offset: 0x002CAB5C
		private GameObject GetRocket(RocketControlStation.StatesInstance smi)
		{
			WorldContainer world = ClusterManager.Instance.GetWorld(smi.GetMyWorldId());
			if (world == null)
			{
				return null;
			}
			return world.gameObject.GetComponent<Clustercraft>().gameObject;
		}

		// Token: 0x06007BA2 RID: 31650 RVA: 0x002CC995 File Offset: 0x002CAB95
		private void SetRocketSpeedModifiers(RocketControlStation.StatesInstance smi, float autoPilotSpeedMultiplier, float pilotSkillMultiplier = 1f)
		{
			this.clusterCraft.Get(smi).GetComponent<Clustercraft>().AutoPilotMultiplier = autoPilotSpeedMultiplier;
			this.clusterCraft.Get(smi).GetComponent<Clustercraft>().PilotSkillMultiplier = pilotSkillMultiplier;
		}

		// Token: 0x06007BA3 RID: 31651 RVA: 0x002CC9C8 File Offset: 0x002CABC8
		private Chore CreateChore(RocketControlStation.StatesInstance smi)
		{
			Workable component = smi.master.GetComponent<RocketControlStationIdleWorkable>();
			WorkChore<RocketControlStationIdleWorkable> workChore = new WorkChore<RocketControlStationIdleWorkable>(Db.Get().ChoreTypes.RocketControl, component, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Work, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanUseRocketControlStation);
			workChore.AddPrecondition(ChorePreconditions.instance.IsRocketTravelling, null);
			return workChore;
		}

		// Token: 0x06007BA4 RID: 31652 RVA: 0x002CCA48 File Offset: 0x002CAC48
		private Chore CreateLaunchChore(RocketControlStation.StatesInstance smi)
		{
			Workable component = smi.master.GetComponent<RocketControlStationLaunchWorkable>();
			WorkChore<RocketControlStationLaunchWorkable> workChore = new WorkChore<RocketControlStationLaunchWorkable>(Db.Get().ChoreTypes.RocketControl, component, null, true, null, null, null, true, null, true, true, null, false, true, false, PriorityScreen.PriorityClass.topPriority, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanUseRocketControlStation);
			return workChore;
		}

		// Token: 0x06007BA5 RID: 31653 RVA: 0x002CCAA6 File Offset: 0x002CACA6
		public void LaunchRocket(RocketControlStation.StatesInstance smi)
		{
			this.clusterCraft.Get(smi).GetComponent<Clustercraft>().Launch(false);
		}

		// Token: 0x06007BA6 RID: 31654 RVA: 0x002CCABF File Offset: 0x002CACBF
		public bool IsInFlight(RocketControlStation.StatesInstance smi)
		{
			return this.clusterCraft.Get(smi).GetComponent<Clustercraft>().Status == Clustercraft.CraftStatus.InFlight;
		}

		// Token: 0x06007BA7 RID: 31655 RVA: 0x002CCADA File Offset: 0x002CACDA
		public bool IsLaunching(RocketControlStation.StatesInstance smi)
		{
			return this.clusterCraft.Get(smi).GetComponent<Clustercraft>().Status == Clustercraft.CraftStatus.Launching;
		}

		// Token: 0x04005EC7 RID: 24263
		public StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.TargetParameter clusterCraft;

		// Token: 0x04005EC8 RID: 24264
		private GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State unoperational;

		// Token: 0x04005EC9 RID: 24265
		private GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State operational;

		// Token: 0x04005ECA RID: 24266
		private GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State running;

		// Token: 0x04005ECB RID: 24267
		private RocketControlStation.States.ReadyStates ready;

		// Token: 0x04005ECC RID: 24268
		private RocketControlStation.States.LaunchStates launch;

		// Token: 0x04005ECD RID: 24269
		public StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.Signal pilotSuccessful;

		// Token: 0x04005ECE RID: 24270
		public StateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.FloatParameter timeRemaining;

		// Token: 0x02001FF0 RID: 8176
		public class ReadyStates : GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State
		{
			// Token: 0x04008E65 RID: 36453
			public GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State idle;

			// Token: 0x04008E66 RID: 36454
			public GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State working;

			// Token: 0x04008E67 RID: 36455
			public GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State post;

			// Token: 0x04008E68 RID: 36456
			public GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State warning;

			// Token: 0x04008E69 RID: 36457
			public GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State autopilot;
		}

		// Token: 0x02001FF1 RID: 8177
		public class LaunchStates : GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State
		{
			// Token: 0x04008E6A RID: 36458
			public GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State launch;

			// Token: 0x04008E6B RID: 36459
			public GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.State fadein;
		}
	}

	// Token: 0x020012D1 RID: 4817
	public class StatesInstance : GameStateMachine<RocketControlStation.States, RocketControlStation.StatesInstance, RocketControlStation, object>.GameInstance
	{
		// Token: 0x06007BB3 RID: 31667 RVA: 0x002CCBEC File Offset: 0x002CADEC
		public StatesInstance(RocketControlStation smi)
			: base(smi)
		{
		}

		// Token: 0x06007BB4 RID: 31668 RVA: 0x002CCC00 File Offset: 0x002CAE00
		public void LaunchRocket()
		{
			base.sm.LaunchRocket(this);
		}

		// Token: 0x06007BB5 RID: 31669 RVA: 0x002CCC10 File Offset: 0x002CAE10
		public void SetPilotSpeedMult(Worker pilot)
		{
			AttributeConverter pilotingSpeed = Db.Get().AttributeConverters.PilotingSpeed;
			AttributeConverterInstance converter = pilot.GetComponent<AttributeConverters>().GetConverter(pilotingSpeed.Id);
			float num = 1f + converter.Evaluate();
			this.pilotSpeedMult = Mathf.Max(num, 0.1f);
		}

		// Token: 0x04005ECF RID: 24271
		public float pilotSpeedMult = 1f;
	}
}
