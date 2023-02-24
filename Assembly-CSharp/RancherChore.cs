using System;
using Klei.AI;
using STRINGS;
using TUNING;

// Token: 0x02000390 RID: 912
public class RancherChore : Chore<RancherChore.RancherChoreStates.Instance>
{
	// Token: 0x06001283 RID: 4739 RVA: 0x00063058 File Offset: 0x00061258
	public RancherChore(KPrefabID rancher_station)
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "IsCreatureAvailableForRanching";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_CREATURE_AVAILABLE_FOR_RANCHING;
		precondition.sortOrder = -3;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			RanchStation.Instance instance = data as RanchStation.Instance;
			return !instance.HasRancher && instance.IsCritterAvailableForRanching;
		};
		this.IsOpenForRanching = precondition;
		base..ctor(Db.Get().ChoreTypes.Ranch, rancher_station, null, false, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime);
		base.AddPrecondition(this.IsOpenForRanching, rancher_station.GetSMI<RanchStation.Instance>());
		base.AddPrecondition(ChorePreconditions.instance.HasSkillPerk, Db.Get().SkillPerks.CanUseRanchStation.Id);
		base.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Work);
		base.AddPrecondition(ChorePreconditions.instance.CanMoveTo, rancher_station.GetComponent<Building>());
		Operational component = rancher_station.GetComponent<Operational>();
		base.AddPrecondition(ChorePreconditions.instance.IsOperational, component);
		Deconstructable component2 = rancher_station.GetComponent<Deconstructable>();
		base.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDeconstruction, component2);
		BuildingEnabledButton component3 = rancher_station.GetComponent<BuildingEnabledButton>();
		base.AddPrecondition(ChorePreconditions.instance.IsNotMarkedForDisable, component3);
		base.smi = new RancherChore.RancherChoreStates.Instance(rancher_station);
		base.SetPrioritizable(rancher_station.GetComponent<Prioritizable>());
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x000631AE File Offset: 0x000613AE
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.rancher.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x000631DF File Offset: 0x000613DF
	protected override void End(string reason)
	{
		base.End(reason);
		base.smi.sm.rancher.Set(null, base.smi);
	}

	// Token: 0x04000A01 RID: 2561
	public Chore.Precondition IsOpenForRanching;

	// Token: 0x02000F84 RID: 3972
	public class RancherChoreStates : GameStateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance>
	{
		// Token: 0x06006FB5 RID: 28597 RVA: 0x002A3680 File Offset: 0x002A1880
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.moveToRanch;
			base.Target(this.rancher);
			this.root.Exit("TriggerRanchStationNoLongerAvailable", delegate(RancherChore.RancherChoreStates.Instance smi)
			{
				smi.TriggerRanchStationNoLongerAvailable();
			});
			this.moveToRanch.MoveTo((RancherChore.RancherChoreStates.Instance smi) => Grid.PosToCell(smi.transform.GetPosition()), this.waitForAvailableRanchable, null, false);
			this.waitForAvailableRanchable.Enter("FindRanchable", delegate(RancherChore.RancherChoreStates.Instance smi)
			{
				smi.WaitForAvailableRanchable(0f);
			}).Update("FindRanchable", delegate(RancherChore.RancherChoreStates.Instance smi, float dt)
			{
				smi.WaitForAvailableRanchable(dt);
			}, UpdateRate.SIM_200ms, false);
			this.ranchCritter.ScheduleGoTo(0.5f, this.ranchCritter.callForCritter).EventTransition(GameHashes.CreatureAbandonedRanchStation, this.waitForAvailableRanchable, null);
			this.ranchCritter.callForCritter.ToggleAnims("anim_interacts_rancherstation_kanim", 0f, "").PlayAnim("calling_loop", KAnim.PlayMode.Loop).ScheduleActionNextFrame("TellCreatureRancherIsReady", delegate(RancherChore.RancherChoreStates.Instance smi)
			{
				smi.TellCreatureRancherIsReady();
			})
				.Target(this.masterTarget)
				.EventTransition(GameHashes.CreatureArrivedAtRanchStation, this.ranchCritter.working, null);
			this.ranchCritter.working.ToggleWork<RancherChore.RancherWorkable>(this.masterTarget, this.ranchCritter.pst, this.waitForAvailableRanchable, null);
			this.ranchCritter.pst.ToggleAnims(new Func<RancherChore.RancherChoreStates.Instance, HashedString>(RancherChore.RancherChoreStates.GetRancherInteractAnim)).QueueAnim("wipe_brow", false, null).OnAnimQueueComplete(this.waitForAvailableRanchable);
		}

		// Token: 0x06006FB6 RID: 28598 RVA: 0x002A385E File Offset: 0x002A1A5E
		private static HashedString GetRancherInteractAnim(RancherChore.RancherChoreStates.Instance smi)
		{
			return smi.ranchStation.def.RancherInteractAnim;
		}

		// Token: 0x06006FB7 RID: 28599 RVA: 0x002A3870 File Offset: 0x002A1A70
		public static bool TryRanchCreature(RancherChore.RancherChoreStates.Instance smi)
		{
			Debug.Assert(smi.ranchStation != null, "smi.ranchStation was null");
			RanchedStates.Instance activeRanchable = smi.ranchStation.ActiveRanchable;
			if (activeRanchable.IsNullOrStopped())
			{
				return false;
			}
			KPrefabID component = activeRanchable.GetComponent<KPrefabID>();
			smi.sm.rancher.Get(smi).Trigger(937885943, component.PrefabTag.Name);
			smi.ranchStation.RanchCreature();
			return true;
		}

		// Token: 0x040054B9 RID: 21689
		public StateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.TargetParameter rancher;

		// Token: 0x040054BA RID: 21690
		private GameStateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.State moveToRanch;

		// Token: 0x040054BB RID: 21691
		private RancherChore.RancherChoreStates.RanchState ranchCritter;

		// Token: 0x040054BC RID: 21692
		private GameStateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.State waitForAvailableRanchable;

		// Token: 0x02001EC3 RID: 7875
		private class RanchState : GameStateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.State
		{
			// Token: 0x040089CD RID: 35277
			public GameStateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.State callForCritter;

			// Token: 0x040089CE RID: 35278
			public GameStateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.State working;

			// Token: 0x040089CF RID: 35279
			public GameStateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.State pst;
		}

		// Token: 0x02001EC4 RID: 7876
		public new class Instance : GameStateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.GameInstance
		{
			// Token: 0x06009CC7 RID: 40135 RVA: 0x0033AD3A File Offset: 0x00338F3A
			public Instance(KPrefabID rancher_station)
				: base(rancher_station)
			{
				this.ranchStation = rancher_station.GetSMI<RanchStation.Instance>();
			}

			// Token: 0x06009CC8 RID: 40136 RVA: 0x0033AD50 File Offset: 0x00338F50
			public void WaitForAvailableRanchable(float dt)
			{
				this.waitTime += dt;
				GameStateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.State state = (this.ranchStation.IsCritterAvailableForRanching ? base.sm.ranchCritter : null);
				if (state != null || this.waitTime >= 2f)
				{
					this.waitTime = 0f;
					this.GoTo(state);
				}
			}

			// Token: 0x06009CC9 RID: 40137 RVA: 0x0033ADA9 File Offset: 0x00338FA9
			public void TriggerRanchStationNoLongerAvailable()
			{
				this.ranchStation.TriggerRanchStationNoLongerAvailable();
			}

			// Token: 0x06009CCA RID: 40138 RVA: 0x0033ADB6 File Offset: 0x00338FB6
			public void TellCreatureRancherIsReady()
			{
				this.ranchStation.MessageRancherReady();
			}

			// Token: 0x040089D0 RID: 35280
			private const float WAIT_FOR_RANCHABLE_TIMEOUT = 2f;

			// Token: 0x040089D1 RID: 35281
			public RanchStation.Instance ranchStation;

			// Token: 0x040089D2 RID: 35282
			private float waitTime;
		}
	}

	// Token: 0x02000F85 RID: 3973
	public class RancherWorkable : Workable
	{
		// Token: 0x06006FB9 RID: 28601 RVA: 0x002A38E8 File Offset: 0x002A1AE8
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			this.ranch = base.gameObject.GetSMI<RanchStation.Instance>();
			this.overrideAnims = new KAnimFile[] { Assets.GetAnim(this.ranch.def.RancherInteractAnim) };
			base.SetWorkTime(this.ranch.def.WorkTime);
			base.SetWorkerStatusItem(this.ranch.def.RanchingStatusItem);
			this.attributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
			this.skillExperienceSkillGroup = Db.Get().SkillGroups.Ranching.Id;
			this.skillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
			this.lightEfficiencyBonus = false;
		}

		// Token: 0x06006FBA RID: 28602 RVA: 0x002A3993 File Offset: 0x002A1B93
		public override Klei.AI.Attribute GetWorkAttribute()
		{
			return Db.Get().Attributes.Ranching;
		}

		// Token: 0x06006FBB RID: 28603 RVA: 0x002A39A4 File Offset: 0x002A1BA4
		protected override void OnStartWork(Worker worker)
		{
			if (this.ranch == null)
			{
				return;
			}
			this.critterAnimController = this.ranch.ActiveRanchable.AnimController;
			this.critterAnimController.Play(this.ranch.def.RanchedPreAnim, KAnim.PlayMode.Once, 1f, 0f);
			this.critterAnimController.Queue(this.ranch.def.RanchedLoopAnim, KAnim.PlayMode.Loop, 1f, 0f);
		}

		// Token: 0x06006FBC RID: 28604 RVA: 0x002A3A1C File Offset: 0x002A1C1C
		public override void OnPendingCompleteWork(Worker work)
		{
			RancherChore.RancherChoreStates.Instance smi = base.gameObject.GetSMI<RancherChore.RancherChoreStates.Instance>();
			if (this.ranch == null || smi == null)
			{
				return;
			}
			if (RancherChore.RancherChoreStates.TryRanchCreature(smi))
			{
				this.critterAnimController.Play(this.ranch.def.RanchedPstAnim, KAnim.PlayMode.Once, 1f, 0f);
			}
		}

		// Token: 0x06006FBD RID: 28605 RVA: 0x002A3A6F File Offset: 0x002A1C6F
		protected override void OnAbortWork(Worker worker)
		{
			if (this.ranch == null || this.critterAnimController == null)
			{
				return;
			}
			this.critterAnimController.Play(this.ranch.def.RanchedAbortAnim, KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x040054BD RID: 21693
		private RanchStation.Instance ranch;

		// Token: 0x040054BE RID: 21694
		private KBatchedAnimController critterAnimController;
	}
}
