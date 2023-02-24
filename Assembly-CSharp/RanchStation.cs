using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200062E RID: 1582
public class RanchStation : GameStateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>
{
	// Token: 0x060029A0 RID: 10656 RVA: 0x000DBD3C File Offset: 0x000D9F3C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.Operational;
		this.Unoperational.TagTransition(GameTags.Operational, this.Operational, false);
		this.Operational.TagTransition(GameTags.Operational, this.Unoperational, true).ToggleChore((RanchStation.Instance smi) => smi.CreateChore(), this.Unoperational, this.Unoperational).Update("FindRanachable", delegate(RanchStation.Instance smi, float dt)
		{
			smi.FindRanchable(null);
		}, UpdateRate.SIM_200ms, false);
	}

	// Token: 0x040018AB RID: 6315
	public StateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.BoolParameter RancherIsReady;

	// Token: 0x040018AC RID: 6316
	public GameStateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.State Unoperational;

	// Token: 0x040018AD RID: 6317
	public RanchStation.OperationalState Operational;

	// Token: 0x020012B6 RID: 4790
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005E73 RID: 24179
		public Func<GameObject, RanchStation.Instance, bool> IsCritterEligibleToBeRanchedCb;

		// Token: 0x04005E74 RID: 24180
		public Action<GameObject> OnRanchCompleteCb;

		// Token: 0x04005E75 RID: 24181
		public HashedString RanchedPreAnim = "idle_loop";

		// Token: 0x04005E76 RID: 24182
		public HashedString RanchedLoopAnim = "idle_loop";

		// Token: 0x04005E77 RID: 24183
		public HashedString RanchedPstAnim = "idle_loop";

		// Token: 0x04005E78 RID: 24184
		public HashedString RanchedAbortAnim = "idle_loop";

		// Token: 0x04005E79 RID: 24185
		public HashedString RancherInteractAnim = "anim_interacts_rancherstation_kanim";

		// Token: 0x04005E7A RID: 24186
		public StatusItem RanchingStatusItem = Db.Get().DuplicantStatusItems.Ranching;

		// Token: 0x04005E7B RID: 24187
		public float WorkTime = 12f;

		// Token: 0x04005E7C RID: 24188
		public Func<RanchStation.Instance, int> GetTargetRanchCell = (RanchStation.Instance smi) => Grid.PosToCell(smi);
	}

	// Token: 0x020012B7 RID: 4791
	public class OperationalState : GameStateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.State
	{
	}

	// Token: 0x020012B8 RID: 4792
	public new class Instance : GameStateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.GameInstance
	{
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06007B36 RID: 31542 RVA: 0x002CAD50 File Offset: 0x002C8F50
		public RanchedStates.Instance ActiveRanchable
		{
			get
			{
				return this.activeRanchable;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06007B37 RID: 31543 RVA: 0x002CAD58 File Offset: 0x002C8F58
		private bool isCritterAvailableForRanching
		{
			get
			{
				return this.targetRanchables.Count > 0;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06007B38 RID: 31544 RVA: 0x002CAD68 File Offset: 0x002C8F68
		public bool IsCritterAvailableForRanching
		{
			get
			{
				this.ValidateTargetRanchables();
				return this.isCritterAvailableForRanching;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06007B39 RID: 31545 RVA: 0x002CAD76 File Offset: 0x002C8F76
		public bool HasRancher
		{
			get
			{
				return this.rancher != null;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06007B3A RID: 31546 RVA: 0x002CAD84 File Offset: 0x002C8F84
		public bool IsRancherReady
		{
			get
			{
				return this.rancherReadyContext.value;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06007B3B RID: 31547 RVA: 0x002CAD91 File Offset: 0x002C8F91
		// (set) Token: 0x06007B3C RID: 31548 RVA: 0x002CAD9E File Offset: 0x002C8F9E
		public Action<RanchStation.Instance> RancherStateChanged
		{
			get
			{
				return this.rancherReadyContext.onDirty;
			}
			set
			{
				this.rancherReadyContext.onDirty = value;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06007B3D RID: 31549 RVA: 0x002CADAC File Offset: 0x002C8FAC
		public Extents StationExtents
		{
			get
			{
				return this.station.GetExtents();
			}
		}

		// Token: 0x06007B3E RID: 31550 RVA: 0x002CADB9 File Offset: 0x002C8FB9
		public int GetRanchNavTarget()
		{
			return base.def.GetTargetRanchCell(this);
		}

		// Token: 0x06007B3F RID: 31551 RVA: 0x002CADCC File Offset: 0x002C8FCC
		public Instance(IStateMachineTarget master, RanchStation.Def def)
			: base(master, def)
		{
			base.gameObject.AddOrGet<RancherChore.RancherWorkable>();
			this.station = base.GetComponent<BuildingComplete>();
			this.rancherReadyContext = base.GetParameterContext(base.sm.RancherIsReady) as StateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.BoolParameter.Context;
		}

		// Token: 0x06007B40 RID: 31552 RVA: 0x002CAE20 File Offset: 0x002C9020
		public Chore CreateChore()
		{
			RancherChore rancherChore = new RancherChore(base.GetComponent<KPrefabID>());
			StateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.TargetParameter targetParameter = rancherChore.smi.sm.rancher;
			StateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.Parameter<GameObject>.Context context = targetParameter.GetContext(rancherChore.smi);
			context.onDirty = (Action<RancherChore.RancherChoreStates.Instance>)Delegate.Combine(context.onDirty, new Action<RancherChore.RancherChoreStates.Instance>(this.OnRancherChanged));
			this.rancher = targetParameter.Get<Worker>(rancherChore.smi);
			return rancherChore;
		}

		// Token: 0x06007B41 RID: 31553 RVA: 0x002CAE8A File Offset: 0x002C908A
		public int GetTargetRanchCell()
		{
			return base.def.GetTargetRanchCell(this);
		}

		// Token: 0x06007B42 RID: 31554 RVA: 0x002CAEA0 File Offset: 0x002C90A0
		public override void StartSM()
		{
			base.StartSM();
			base.Subscribe(144050788, new Action<object>(this.OnRoomUpdated));
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(this.GetTargetRanchCell());
			if (cavityForCell != null && cavityForCell.room != null)
			{
				this.OnRoomUpdated(cavityForCell.room);
			}
		}

		// Token: 0x06007B43 RID: 31555 RVA: 0x002CAEF7 File Offset: 0x002C90F7
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			base.Unsubscribe(144050788, new Action<object>(this.OnRoomUpdated));
		}

		// Token: 0x06007B44 RID: 31556 RVA: 0x002CAF17 File Offset: 0x002C9117
		private void OnRoomUpdated(object data)
		{
			if (data == null)
			{
				return;
			}
			this.ranch = data as Room;
			if (this.ranch.roomType != Db.Get().RoomTypes.CreaturePen)
			{
				this.TriggerRanchStationNoLongerAvailable();
				this.ranch = null;
			}
		}

		// Token: 0x06007B45 RID: 31557 RVA: 0x002CAF52 File Offset: 0x002C9152
		private void OnRancherChanged(RancherChore.RancherChoreStates.Instance choreInstance)
		{
			this.rancher = choreInstance.sm.rancher.Get<Worker>(choreInstance);
			this.TriggerRanchStationNoLongerAvailable();
		}

		// Token: 0x06007B46 RID: 31558 RVA: 0x002CAF71 File Offset: 0x002C9171
		public bool TryGetRanched(RanchedStates.Instance ranchable)
		{
			return this.activeRanchable == null || this.activeRanchable == ranchable;
		}

		// Token: 0x06007B47 RID: 31559 RVA: 0x002CAF86 File Offset: 0x002C9186
		public void MessageCreatureArrived(RanchedStates.Instance critter)
		{
			this.activeRanchable = critter;
			this.rancherReadyContext.Set(false, this, false);
			base.smi.ScheduleNextFrame(new Action<object>(this.DelayedNotification), null);
		}

		// Token: 0x06007B48 RID: 31560 RVA: 0x002CAFB6 File Offset: 0x002C91B6
		public void DelayedNotification(object _)
		{
			base.Trigger(-1357116271, null);
		}

		// Token: 0x06007B49 RID: 31561 RVA: 0x002CAFC4 File Offset: 0x002C91C4
		public void MessageRancherReady()
		{
			this.rancherReadyContext.Set(true, this, false);
		}

		// Token: 0x06007B4A RID: 31562 RVA: 0x002CAFD4 File Offset: 0x002C91D4
		private bool CanRanchableBeRanchedAtRanchStation(RanchableMonitor.Instance ranchable)
		{
			bool flag = !ranchable.IsNullOrStopped();
			if (flag && ranchable.TargetRanchStation != null && ranchable.TargetRanchStation != this)
			{
				flag = !ranchable.TargetRanchStation.IsRunning() || !ranchable.TargetRanchStation.HasRancher;
			}
			flag = flag && base.def.IsCritterEligibleToBeRanchedCb(ranchable.gameObject, this);
			flag = flag && ranchable.ChoreConsumer.IsChoreEqualOrAboveCurrentChorePriority<RanchedStates>();
			if (flag)
			{
				int num = Grid.PosToCell(ranchable.transform.GetPosition());
				CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
				if (cavityForCell == null || cavityForCell != this.ranch.cavity)
				{
					flag = false;
				}
				else
				{
					int num2 = this.GetRanchNavTarget();
					if (ranchable.HasTag(GameTags.Creatures.Flyer))
					{
						num2 = Grid.CellAbove(num2);
					}
					flag = ranchable.NavComponent.GetNavigationCost(num2) != -1;
				}
			}
			return flag;
		}

		// Token: 0x06007B4B RID: 31563 RVA: 0x002CB0B8 File Offset: 0x002C92B8
		public void ValidateTargetRanchables()
		{
			if (!this.HasRancher)
			{
				return;
			}
			foreach (RanchableMonitor.Instance instance in this.targetRanchables.ToArray())
			{
				if (instance.States == null)
				{
					this.Abandon(instance);
				}
				else if (!this.CanRanchableBeRanchedAtRanchStation(instance))
				{
					instance.States.AbandonRanchStation();
				}
			}
		}

		// Token: 0x06007B4C RID: 31564 RVA: 0x002CB114 File Offset: 0x002C9314
		public void FindRanchable(object _ = null)
		{
			if (this.ranch == null)
			{
				return;
			}
			this.ValidateTargetRanchables();
			if (this.targetRanchables.Count == 2)
			{
				return;
			}
			List<KPrefabID> creatures = this.ranch.cavity.creatures;
			if (this.HasRancher && !this.isCritterAvailableForRanching && creatures.Count == 0)
			{
				this.TryNotifyEmptyRanch();
			}
			for (int i = 0; i < creatures.Count; i++)
			{
				KPrefabID kprefabID = creatures[i];
				if (!(kprefabID == null))
				{
					RanchableMonitor.Instance smi = kprefabID.GetSMI<RanchableMonitor.Instance>();
					if (!this.targetRanchables.Contains(smi) && this.CanRanchableBeRanchedAtRanchStation(smi))
					{
						if (smi != null)
						{
							smi.States.AbandonRanchStation();
							smi.TargetRanchStation = this;
						}
						this.targetRanchables.Add(smi);
						return;
					}
				}
			}
		}

		// Token: 0x06007B4D RID: 31565 RVA: 0x002CB1D0 File Offset: 0x002C93D0
		public void RanchCreature()
		{
			if (this.activeRanchable.IsNullOrStopped())
			{
				return;
			}
			global::Debug.Assert(this.activeRanchable != null, "targetRanchable was null");
			global::Debug.Assert(this.activeRanchable.GetMaster() != null, "GetMaster was null");
			global::Debug.Assert(base.def != null, "def was null");
			global::Debug.Assert(base.def.OnRanchCompleteCb != null, "onRanchCompleteCb cb was null");
			base.def.OnRanchCompleteCb(this.activeRanchable.gameObject);
			this.targetRanchables.Remove(this.activeRanchable.Monitor);
			this.activeRanchable.Trigger(1827504087, null);
			this.activeRanchable = null;
			base.smi.ScheduleNextFrame(new Action<object>(this.FindRanchable), null);
		}

		// Token: 0x06007B4E RID: 31566 RVA: 0x002CB2A4 File Offset: 0x002C94A4
		public void TriggerRanchStationNoLongerAvailable()
		{
			for (int i = this.targetRanchables.Count - 1; i >= 0; i--)
			{
				RanchableMonitor.Instance instance = this.targetRanchables[i];
				if (!instance.IsNullOrStopped() && !instance.States.IsNullOrStopped())
				{
					instance.Trigger(1689625967, null);
				}
			}
			this.targetRanchables.Clear();
			this.RancherStateChanged = null;
			this.rancherReadyContext.Set(false, this, false);
		}

		// Token: 0x06007B4F RID: 31567 RVA: 0x002CB318 File Offset: 0x002C9518
		public void Abandon(RanchableMonitor.Instance critter)
		{
			this.targetRanchables.Remove(critter);
			if (critter.States == null)
			{
				return;
			}
			bool flag = !this.isCritterAvailableForRanching;
			if (critter.States == this.activeRanchable)
			{
				flag = true;
				this.activeRanchable = null;
			}
			critter.TargetRanchStation = null;
			if (flag)
			{
				this.TryNotifyEmptyRanch();
			}
		}

		// Token: 0x06007B50 RID: 31568 RVA: 0x002CB36C File Offset: 0x002C956C
		private void TryNotifyEmptyRanch()
		{
			if (!this.HasRancher)
			{
				return;
			}
			this.rancher.Trigger(-364750427, null);
		}

		// Token: 0x04005E7D RID: 24189
		private const int QUEUE_SIZE = 2;

		// Token: 0x04005E7E RID: 24190
		private List<RanchableMonitor.Instance> targetRanchables = new List<RanchableMonitor.Instance>();

		// Token: 0x04005E7F RID: 24191
		private RanchedStates.Instance activeRanchable;

		// Token: 0x04005E80 RID: 24192
		private Room ranch;

		// Token: 0x04005E81 RID: 24193
		private Worker rancher;

		// Token: 0x04005E82 RID: 24194
		private BuildingComplete station;

		// Token: 0x04005E83 RID: 24195
		private StateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.BoolParameter.Context rancherReadyContext;
	}
}
