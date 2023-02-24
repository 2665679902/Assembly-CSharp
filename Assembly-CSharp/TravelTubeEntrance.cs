using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000663 RID: 1635
[SerializationConfig(MemberSerialization.OptIn)]
public class TravelTubeEntrance : StateMachineComponent<TravelTubeEntrance.SMInstance>, ISaveLoadable, ISim200ms
{
	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000E682F File Offset: 0x000E4A2F
	public float AvailableJoules
	{
		get
		{
			return this.availableJoules;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x000E6837 File Offset: 0x000E4A37
	public float TotalCapacity
	{
		get
		{
			return this.jouleCapacity;
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000E683F File Offset: 0x000E4A3F
	public float UsageJoules
	{
		get
		{
			return this.joulesPerLaunch;
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06002BEB RID: 11243 RVA: 0x000E6847 File Offset: 0x000E4A47
	public bool HasLaunchPower
	{
		get
		{
			return this.availableJoules > this.joulesPerLaunch;
		}
	}

	// Token: 0x06002BEC RID: 11244 RVA: 0x000E6857 File Offset: 0x000E4A57
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.energyConsumer.OnConnectionChanged += this.OnConnectionChanged;
	}

	// Token: 0x06002BED RID: 11245 RVA: 0x000E6878 File Offset: 0x000E4A78
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = (int)base.transform.GetPosition().x;
		int num2 = (int)base.transform.GetPosition().y + 2;
		Extents extents = new Extents(num, num2, 1, 1);
		UtilityConnections connections = Game.Instance.travelTubeSystem.GetConnections(Grid.XYToCell(num, num2), true);
		this.TubeConnectionsChanged(connections);
		this.tubeChangedEntry = GameScenePartitioner.Instance.Add("TravelTubeEntrance.TubeListener", base.gameObject, extents, GameScenePartitioner.Instance.objectLayers[35], new Action<object>(this.TubeChanged));
		base.Subscribe<TravelTubeEntrance>(-592767678, TravelTubeEntrance.OnOperationalChangedDelegate);
		this.meter = new MeterController(this, Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		this.CreateNewWaitReactable();
		Grid.RegisterTubeEntrance(Grid.PosToCell(this), Mathf.FloorToInt(this.availableJoules / this.joulesPerLaunch));
		base.smi.StartSM();
		this.UpdateCharge();
	}

	// Token: 0x06002BEE RID: 11246 RVA: 0x000E6970 File Offset: 0x000E4B70
	protected override void OnCleanUp()
	{
		if (this.travelTube != null)
		{
			this.travelTube.Unsubscribe(-1041684577, new Action<object>(this.TubeConnectionsChanged));
			this.travelTube = null;
		}
		Grid.UnregisterTubeEntrance(Grid.PosToCell(this));
		this.ClearWaitReactable();
		GameScenePartitioner.Instance.Free(ref this.tubeChangedEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002BEF RID: 11247 RVA: 0x000E69D8 File Offset: 0x000E4BD8
	private void TubeChanged(object data)
	{
		if (this.travelTube != null)
		{
			this.travelTube.Unsubscribe(-1041684577, new Action<object>(this.TubeConnectionsChanged));
			this.travelTube = null;
		}
		GameObject gameObject = data as GameObject;
		if (data == null)
		{
			this.TubeConnectionsChanged(0);
			return;
		}
		TravelTube component = gameObject.GetComponent<TravelTube>();
		if (component != null)
		{
			component.Subscribe(-1041684577, new Action<object>(this.TubeConnectionsChanged));
			this.travelTube = component;
			return;
		}
		this.TubeConnectionsChanged(0);
	}

	// Token: 0x06002BF0 RID: 11248 RVA: 0x000E6A6C File Offset: 0x000E4C6C
	private void TubeConnectionsChanged(object data)
	{
		bool flag = (UtilityConnections)data == UtilityConnections.Up;
		this.operational.SetFlag(TravelTubeEntrance.tubeConnected, flag);
	}

	// Token: 0x06002BF1 RID: 11249 RVA: 0x000E6A94 File Offset: 0x000E4C94
	private bool CanAcceptMorePower()
	{
		return this.operational.IsOperational && (this.button == null || this.button.IsEnabled) && this.energyConsumer.IsExternallyPowered && this.availableJoules < this.jouleCapacity;
	}

	// Token: 0x06002BF2 RID: 11250 RVA: 0x000E6AE8 File Offset: 0x000E4CE8
	public void Sim200ms(float dt)
	{
		if (this.CanAcceptMorePower())
		{
			this.availableJoules = Mathf.Min(this.jouleCapacity, this.availableJoules + this.energyConsumer.WattsUsed * dt);
			this.UpdateCharge();
		}
		this.energyConsumer.SetSustained(this.HasLaunchPower);
		this.UpdateActive();
		this.UpdateConnectionStatus();
	}

	// Token: 0x06002BF3 RID: 11251 RVA: 0x000E6B45 File Offset: 0x000E4D45
	public void Reserve(TubeTraveller.Instance traveller, int prefabInstanceID)
	{
		Grid.ReserveTubeEntrance(Grid.PosToCell(this), prefabInstanceID, true);
	}

	// Token: 0x06002BF4 RID: 11252 RVA: 0x000E6B55 File Offset: 0x000E4D55
	public void Unreserve(TubeTraveller.Instance traveller, int prefabInstanceID)
	{
		Grid.ReserveTubeEntrance(Grid.PosToCell(this), prefabInstanceID, false);
	}

	// Token: 0x06002BF5 RID: 11253 RVA: 0x000E6B65 File Offset: 0x000E4D65
	public bool IsTraversable(Navigator agent)
	{
		return Grid.HasUsableTubeEntrance(Grid.PosToCell(this), agent.gameObject.GetComponent<KPrefabID>().InstanceID);
	}

	// Token: 0x06002BF6 RID: 11254 RVA: 0x000E6B82 File Offset: 0x000E4D82
	public bool HasChargeSlotReserved(Navigator agent)
	{
		return Grid.HasReservedTubeEntrance(Grid.PosToCell(this), agent.gameObject.GetComponent<KPrefabID>().InstanceID);
	}

	// Token: 0x06002BF7 RID: 11255 RVA: 0x000E6B9F File Offset: 0x000E4D9F
	public bool HasChargeSlotReserved(TubeTraveller.Instance tube_traveller, int prefabInstanceID)
	{
		return Grid.HasReservedTubeEntrance(Grid.PosToCell(this), prefabInstanceID);
	}

	// Token: 0x06002BF8 RID: 11256 RVA: 0x000E6BAD File Offset: 0x000E4DAD
	public bool IsChargedSlotAvailable(TubeTraveller.Instance tube_traveller, int prefabInstanceID)
	{
		return Grid.HasUsableTubeEntrance(Grid.PosToCell(this), prefabInstanceID);
	}

	// Token: 0x06002BF9 RID: 11257 RVA: 0x000E6BBC File Offset: 0x000E4DBC
	public bool ShouldWait(GameObject reactor)
	{
		if (!this.operational.IsOperational)
		{
			return false;
		}
		if (!this.HasLaunchPower)
		{
			return false;
		}
		if (this.launch_workable.worker == null)
		{
			return false;
		}
		TubeTraveller.Instance smi = reactor.GetSMI<TubeTraveller.Instance>();
		return this.HasChargeSlotReserved(smi, reactor.GetComponent<KPrefabID>().InstanceID);
	}

	// Token: 0x06002BFA RID: 11258 RVA: 0x000E6C10 File Offset: 0x000E4E10
	public void ConsumeCharge(GameObject reactor)
	{
		if (this.HasLaunchPower)
		{
			this.availableJoules -= this.joulesPerLaunch;
			this.UpdateCharge();
		}
	}

	// Token: 0x06002BFB RID: 11259 RVA: 0x000E6C33 File Offset: 0x000E4E33
	private void CreateNewWaitReactable()
	{
		if (this.wait_reactable == null)
		{
			this.wait_reactable = new TravelTubeEntrance.WaitReactable(this);
		}
	}

	// Token: 0x06002BFC RID: 11260 RVA: 0x000E6C49 File Offset: 0x000E4E49
	private void OrphanWaitReactable()
	{
		this.wait_reactable = null;
	}

	// Token: 0x06002BFD RID: 11261 RVA: 0x000E6C52 File Offset: 0x000E4E52
	private void ClearWaitReactable()
	{
		if (this.wait_reactable != null)
		{
			this.wait_reactable.Cleanup();
			this.wait_reactable = null;
		}
	}

	// Token: 0x06002BFE RID: 11262 RVA: 0x000E6C70 File Offset: 0x000E4E70
	private void OnOperationalChanged(object data)
	{
		bool flag = (bool)data;
		Grid.SetTubeEntranceOperational(Grid.PosToCell(this), flag);
		this.UpdateActive();
	}

	// Token: 0x06002BFF RID: 11263 RVA: 0x000E6C96 File Offset: 0x000E4E96
	private void OnConnectionChanged()
	{
		this.UpdateActive();
		this.UpdateConnectionStatus();
	}

	// Token: 0x06002C00 RID: 11264 RVA: 0x000E6CA4 File Offset: 0x000E4EA4
	private void UpdateActive()
	{
		this.operational.SetActive(this.CanAcceptMorePower(), false);
	}

	// Token: 0x06002C01 RID: 11265 RVA: 0x000E6CB8 File Offset: 0x000E4EB8
	private void UpdateCharge()
	{
		base.smi.sm.hasLaunchCharges.Set(this.HasLaunchPower, base.smi, false);
		float num = Mathf.Clamp01(this.availableJoules / this.jouleCapacity);
		this.meter.SetPositionPercent(num);
		this.energyConsumer.UpdatePoweredStatus();
		Grid.SetTubeEntranceReservationCapacity(Grid.PosToCell(this), Mathf.FloorToInt(this.availableJoules / this.joulesPerLaunch));
	}

	// Token: 0x06002C02 RID: 11266 RVA: 0x000E6D30 File Offset: 0x000E4F30
	private void UpdateConnectionStatus()
	{
		bool flag = this.button != null && !this.button.IsEnabled;
		bool isConnected = this.energyConsumer.IsConnected;
		bool hasLaunchPower = this.HasLaunchPower;
		if (flag || !isConnected || hasLaunchPower)
		{
			this.connectedStatus = this.selectable.RemoveStatusItem(this.connectedStatus, false);
			return;
		}
		if (this.connectedStatus == Guid.Empty)
		{
			this.connectedStatus = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.NotEnoughPower, null);
		}
	}

	// Token: 0x040019F9 RID: 6649
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040019FA RID: 6650
	[MyCmpReq]
	private TravelTubeEntrance.Work launch_workable;

	// Token: 0x040019FB RID: 6651
	[MyCmpReq]
	private EnergyConsumerSelfSustaining energyConsumer;

	// Token: 0x040019FC RID: 6652
	[MyCmpGet]
	private BuildingEnabledButton button;

	// Token: 0x040019FD RID: 6653
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040019FE RID: 6654
	public float jouleCapacity = 1f;

	// Token: 0x040019FF RID: 6655
	public float joulesPerLaunch = 1f;

	// Token: 0x04001A00 RID: 6656
	[Serialize]
	private float availableJoules;

	// Token: 0x04001A01 RID: 6657
	private TravelTube travelTube;

	// Token: 0x04001A02 RID: 6658
	private TravelTubeEntrance.WaitReactable wait_reactable;

	// Token: 0x04001A03 RID: 6659
	private MeterController meter;

	// Token: 0x04001A04 RID: 6660
	private const int MAX_CHARGES = 3;

	// Token: 0x04001A05 RID: 6661
	private const float RECHARGE_TIME = 10f;

	// Token: 0x04001A06 RID: 6662
	private static readonly Operational.Flag tubeConnected = new Operational.Flag("tubeConnected", Operational.Flag.Type.Functional);

	// Token: 0x04001A07 RID: 6663
	private HandleVector<int>.Handle tubeChangedEntry;

	// Token: 0x04001A08 RID: 6664
	private static readonly EventSystem.IntraObjectHandler<TravelTubeEntrance> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<TravelTubeEntrance>(delegate(TravelTubeEntrance component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001A09 RID: 6665
	private Guid connectedStatus;

	// Token: 0x0200131A RID: 4890
	private class LaunchReactable : WorkableReactable
	{
		// Token: 0x06007CA6 RID: 31910 RVA: 0x002D0E5D File Offset: 0x002CF05D
		public LaunchReactable(Workable workable, TravelTubeEntrance entrance)
			: base(workable, "LaunchReactable", Db.Get().ChoreTypes.TravelTubeEntrance, WorkableReactable.AllowedDirection.Any)
		{
			this.entrance = entrance;
		}

		// Token: 0x06007CA7 RID: 31911 RVA: 0x002D0E88 File Offset: 0x002CF088
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (base.InternalCanBegin(new_reactor, transition))
			{
				Navigator component = new_reactor.GetComponent<Navigator>();
				return component && this.entrance.HasChargeSlotReserved(component);
			}
			return false;
		}

		// Token: 0x04005F8D RID: 24461
		private TravelTubeEntrance entrance;
	}

	// Token: 0x0200131B RID: 4891
	private class WaitReactable : Reactable
	{
		// Token: 0x06007CA8 RID: 31912 RVA: 0x002D0EC0 File Offset: 0x002CF0C0
		public WaitReactable(TravelTubeEntrance entrance)
			: base(entrance.gameObject, "WaitReactable", Db.Get().ChoreTypes.TravelTubeEntrance, 2, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
		{
			this.entrance = entrance;
			this.preventChoreInterruption = false;
		}

		// Token: 0x06007CA9 RID: 31913 RVA: 0x002D0F19 File Offset: 0x002CF119
		public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
		{
			if (this.reactor != null)
			{
				return false;
			}
			if (this.entrance == null)
			{
				base.Cleanup();
				return false;
			}
			return this.entrance.ShouldWait(new_reactor);
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x002D0F50 File Offset: 0x002CF150
		protected override void InternalBegin()
		{
			KBatchedAnimController component = this.reactor.GetComponent<KBatchedAnimController>();
			component.AddAnimOverrides(Assets.GetAnim("anim_idle_distracted_kanim"), 1f);
			component.Play("idle_pre", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
			this.entrance.OrphanWaitReactable();
			this.entrance.CreateNewWaitReactable();
		}

		// Token: 0x06007CAB RID: 31915 RVA: 0x002D0FCD File Offset: 0x002CF1CD
		public override void Update(float dt)
		{
			if (this.entrance == null)
			{
				base.Cleanup();
				return;
			}
			if (!this.entrance.ShouldWait(this.reactor))
			{
				base.Cleanup();
			}
		}

		// Token: 0x06007CAC RID: 31916 RVA: 0x002D0FFD File Offset: 0x002CF1FD
		protected override void InternalEnd()
		{
			if (this.reactor != null)
			{
				this.reactor.GetComponent<KBatchedAnimController>().RemoveAnimOverrides(Assets.GetAnim("anim_idle_distracted_kanim"));
			}
		}

		// Token: 0x06007CAD RID: 31917 RVA: 0x002D102C File Offset: 0x002CF22C
		protected override void InternalCleanup()
		{
		}

		// Token: 0x04005F8E RID: 24462
		private TravelTubeEntrance entrance;
	}

	// Token: 0x0200131C RID: 4892
	public class SMInstance : GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.GameInstance
	{
		// Token: 0x06007CAE RID: 31918 RVA: 0x002D102E File Offset: 0x002CF22E
		public SMInstance(TravelTubeEntrance master)
			: base(master)
		{
		}
	}

	// Token: 0x0200131D RID: 4893
	public class States : GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance>
	{
		// Token: 0x06007CAF RID: 31919 RVA: 0x002D1038 File Offset: 0x002CF238
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.notoperational;
			this.root.ToggleStatusItem(Db.Get().BuildingStatusItems.StoredCharge, null);
			this.notoperational.DefaultState(this.notoperational.normal).PlayAnim("off").TagTransition(GameTags.Operational, this.ready, false);
			this.notoperational.normal.EventTransition(GameHashes.OperationalFlagChanged, this.notoperational.notube, (TravelTubeEntrance.SMInstance smi) => !smi.master.operational.GetFlag(TravelTubeEntrance.tubeConnected));
			this.notoperational.notube.EventTransition(GameHashes.OperationalFlagChanged, this.notoperational.normal, (TravelTubeEntrance.SMInstance smi) => smi.master.operational.GetFlag(TravelTubeEntrance.tubeConnected)).ToggleStatusItem(Db.Get().BuildingStatusItems.NoTubeConnected, null);
			this.notready.PlayAnim("off").ParamTransition<bool>(this.hasLaunchCharges, this.ready, (TravelTubeEntrance.SMInstance smi, bool hasLaunchCharges) => hasLaunchCharges).TagTransition(GameTags.Operational, this.notoperational, true);
			this.ready.DefaultState(this.ready.free).ToggleReactable((TravelTubeEntrance.SMInstance smi) => new TravelTubeEntrance.LaunchReactable(smi.master.GetComponent<TravelTubeEntrance.Work>(), smi.master.GetComponent<TravelTubeEntrance>())).ParamTransition<bool>(this.hasLaunchCharges, this.notready, (TravelTubeEntrance.SMInstance smi, bool hasLaunchCharges) => !hasLaunchCharges)
				.TagTransition(GameTags.Operational, this.notoperational, true);
			this.ready.free.PlayAnim("on").WorkableStartTransition((TravelTubeEntrance.SMInstance smi) => smi.GetComponent<TravelTubeEntrance.Work>(), this.ready.occupied);
			this.ready.occupied.PlayAnim("working_pre").QueueAnim("working_loop", true, null).WorkableStopTransition((TravelTubeEntrance.SMInstance smi) => smi.GetComponent<TravelTubeEntrance.Work>(), this.ready.post);
			this.ready.post.PlayAnim("working_pst").OnAnimQueueComplete(this.ready);
		}

		// Token: 0x04005F8F RID: 24463
		public StateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.BoolParameter hasLaunchCharges;

		// Token: 0x04005F90 RID: 24464
		public TravelTubeEntrance.States.NotOperationalStates notoperational;

		// Token: 0x04005F91 RID: 24465
		public GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.State notready;

		// Token: 0x04005F92 RID: 24466
		public TravelTubeEntrance.States.ReadyStates ready;

		// Token: 0x02002013 RID: 8211
		public class NotOperationalStates : GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.State
		{
			// Token: 0x04008F16 RID: 36630
			public GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.State normal;

			// Token: 0x04008F17 RID: 36631
			public GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.State notube;
		}

		// Token: 0x02002014 RID: 8212
		public class ReadyStates : GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.State
		{
			// Token: 0x04008F18 RID: 36632
			public GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.State free;

			// Token: 0x04008F19 RID: 36633
			public GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.State occupied;

			// Token: 0x04008F1A RID: 36634
			public GameStateMachine<TravelTubeEntrance.States, TravelTubeEntrance.SMInstance, TravelTubeEntrance, object>.State post;
		}
	}

	// Token: 0x0200131E RID: 4894
	[AddComponentMenu("KMonoBehaviour/Workable/Work")]
	public class Work : Workable, IGameObjectEffectDescriptor
	{
		// Token: 0x06007CB1 RID: 31921 RVA: 0x002D12BD File Offset: 0x002CF4BD
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.resetProgressOnStop = true;
			this.showProgressBar = false;
			this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_tube_launcher_kanim") };
			this.workLayer = Grid.SceneLayer.BuildingUse;
		}

		// Token: 0x06007CB2 RID: 31922 RVA: 0x002D12F9 File Offset: 0x002CF4F9
		protected override void OnStartWork(Worker worker)
		{
			base.SetWorkTime(1f);
		}
	}
}
