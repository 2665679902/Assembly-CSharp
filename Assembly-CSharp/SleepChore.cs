using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000396 RID: 918
public class SleepChore : Chore<SleepChore.StatesInstance>
{
	// Token: 0x06001295 RID: 4757 RVA: 0x000637F4 File Offset: 0x000619F4
	public SleepChore(ChoreType choreType, IStateMachineTarget target, GameObject bed, bool bedIsLocator, bool isInterruptable)
		: base(choreType, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime)
	{
		base.smi = new SleepChore.StatesInstance(this, target.gameObject, bed, bedIsLocator, isInterruptable);
		if (isInterruptable)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		}
		base.AddPrecondition(SleepChore.IsOkayTimeToSleep, null);
		Operational component = bed.GetComponent<Operational>();
		if (component != null)
		{
			base.AddPrecondition(ChorePreconditions.instance.IsOperational, component);
		}
	}

	// Token: 0x06001296 RID: 4758 RVA: 0x00063874 File Offset: 0x00061A74
	public static Sleepable GetSafeFloorLocator(GameObject sleeper)
	{
		int num = sleeper.GetComponent<Sensors>().GetSensor<SafeCellSensor>().GetSleepCellQuery();
		if (num == Grid.InvalidCell)
		{
			num = Grid.PosToCell(sleeper.transform.GetPosition());
		}
		return ChoreHelpers.CreateSleepLocator(Grid.CellToPosCBC(num, Grid.SceneLayer.Move)).GetComponent<Sleepable>();
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x000638BD File Offset: 0x00061ABD
	public static bool IsDarkAtCell(int cell)
	{
		return Grid.LightIntensity[cell] <= 0;
	}

	// Token: 0x04000A06 RID: 2566
	public static readonly Chore.Precondition IsOkayTimeToSleep = new Chore.Precondition
	{
		id = "IsOkayTimeToSleep",
		description = DUPLICANTS.CHORES.PRECONDITIONS.IS_OKAY_TIME_TO_SLEEP,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Narcolepsy component = context.consumerState.consumer.GetComponent<Narcolepsy>();
			bool flag = component != null && component.IsNarcolepsing();
			StaminaMonitor.Instance smi = context.consumerState.consumer.GetSMI<StaminaMonitor.Instance>();
			bool flag2 = smi != null && smi.NeedsToSleep();
			bool flag3 = ChorePreconditions.instance.IsScheduledTime.fn(ref context, Db.Get().ScheduleBlockTypes.Sleep);
			return flag || flag3 || flag2;
		}
	};

	// Token: 0x02000F93 RID: 3987
	public class StatesInstance : GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.GameInstance
	{
		// Token: 0x06006FE9 RID: 28649 RVA: 0x002A4868 File Offset: 0x002A2A68
		public StatesInstance(SleepChore master, GameObject sleeper, GameObject bed, bool bedIsLocator, bool isInterruptable)
			: base(master)
		{
			base.sm.sleeper.Set(sleeper, base.smi, false);
			base.sm.isInterruptable.Set(isInterruptable, base.smi, false);
			Traits component = sleeper.GetComponent<Traits>();
			if (component != null)
			{
				base.sm.needsNightLight.Set(component.HasTrait("NightLight"), base.smi, false);
			}
			if (bedIsLocator)
			{
				this.AddLocator(bed);
				return;
			}
			base.sm.bed.Set(bed, base.smi, false);
		}

		// Token: 0x06006FEA RID: 28650 RVA: 0x002A491C File Offset: 0x002A2B1C
		public void CheckLightLevel()
		{
			GameObject gameObject = base.sm.sleeper.Get(base.smi);
			int num = Grid.PosToCell(gameObject);
			if (Grid.IsValidCell(num))
			{
				bool flag = SleepChore.IsDarkAtCell(num);
				if (base.sm.needsNightLight.Get(base.smi))
				{
					if (flag)
					{
						gameObject.Trigger(-1307593733, null);
						return;
					}
				}
				else if (!flag && !this.IsLoudSleeper() && !this.IsGlowStick())
				{
					gameObject.Trigger(-1063113160, null);
				}
			}
		}

		// Token: 0x06006FEB RID: 28651 RVA: 0x002A499D File Offset: 0x002A2B9D
		public bool IsLoudSleeper()
		{
			return base.sm.sleeper.Get(base.smi).GetComponent<Snorer>() != null;
		}

		// Token: 0x06006FEC RID: 28652 RVA: 0x002A49C5 File Offset: 0x002A2BC5
		public bool IsGlowStick()
		{
			return base.sm.sleeper.Get(base.smi).GetComponent<GlowStick>() != null;
		}

		// Token: 0x06006FED RID: 28653 RVA: 0x002A49ED File Offset: 0x002A2BED
		public void EvaluateSleepQuality()
		{
		}

		// Token: 0x06006FEE RID: 28654 RVA: 0x002A49F0 File Offset: 0x002A2BF0
		public void AddLocator(GameObject sleepable)
		{
			this.locator = sleepable;
			int num = Grid.PosToCell(this.locator);
			Grid.Reserved[num] = true;
			base.sm.bed.Set(this.locator, this, false);
		}

		// Token: 0x06006FEF RID: 28655 RVA: 0x002A4A38 File Offset: 0x002A2C38
		public void DestroyLocator()
		{
			if (this.locator != null)
			{
				Grid.Reserved[Grid.PosToCell(this.locator)] = false;
				ChoreHelpers.DestroyLocator(this.locator);
				base.sm.bed.Set(null, this);
				this.locator = null;
			}
		}

		// Token: 0x06006FF0 RID: 28656 RVA: 0x002A4A90 File Offset: 0x002A2C90
		public void SetAnim()
		{
			Sleepable sleepable = base.sm.bed.Get<Sleepable>(base.smi);
			if (sleepable.GetComponent<Building>() == null)
			{
				NavType currentNavType = base.sm.sleeper.Get<Navigator>(base.smi).CurrentNavType;
				string text;
				if (currentNavType != NavType.Ladder)
				{
					if (currentNavType != NavType.Pole)
					{
						text = "anim_sleep_floor_kanim";
					}
					else
					{
						text = "anim_sleep_pole_kanim";
					}
				}
				else
				{
					text = "anim_sleep_ladder_kanim";
				}
				sleepable.overrideAnims = new KAnimFile[] { Assets.GetAnim(text) };
			}
		}

		// Token: 0x040054DC RID: 21724
		public bool hadPeacefulSleep;

		// Token: 0x040054DD RID: 21725
		public bool hadNormalSleep;

		// Token: 0x040054DE RID: 21726
		public bool hadBadSleep;

		// Token: 0x040054DF RID: 21727
		public bool hadTerribleSleep;

		// Token: 0x040054E0 RID: 21728
		public int lastEvaluatedDay = -1;

		// Token: 0x040054E1 RID: 21729
		public float wakeUpBuffer = 2f;

		// Token: 0x040054E2 RID: 21730
		public string stateChangeNoiseSource;

		// Token: 0x040054E3 RID: 21731
		private GameObject locator;
	}

	// Token: 0x02000F94 RID: 3988
	public class States : GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore>
	{
		// Token: 0x06006FF1 RID: 28657 RVA: 0x002A4B18 File Offset: 0x002A2D18
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.approach;
			base.Target(this.sleeper);
			this.root.Exit("DestroyLocator", delegate(SleepChore.StatesInstance smi)
			{
				smi.DestroyLocator();
			});
			this.approach.InitializeStates(this.sleeper, this.bed, this.sleep, null, null, null);
			this.sleep.Enter("SetAnims", delegate(SleepChore.StatesInstance smi)
			{
				smi.SetAnim();
			}).DefaultState(this.sleep.normal).ToggleTag(GameTags.Asleep)
				.DoSleep(this.sleeper, this.bed, this.success, null)
				.TriggerOnExit(GameHashes.SleepFinished, null)
				.EventHandler(GameHashes.SleepDisturbedByLight, delegate(SleepChore.StatesInstance smi)
				{
					this.isDisturbedByLight.Set(true, smi, false);
				})
				.EventHandler(GameHashes.SleepDisturbedByNoise, delegate(SleepChore.StatesInstance smi)
				{
					this.isDisturbedByNoise.Set(true, smi, false);
				})
				.EventHandler(GameHashes.SleepDisturbedByFearOfDark, delegate(SleepChore.StatesInstance smi)
				{
					this.isScaredOfDark.Set(true, smi, false);
				})
				.EventHandler(GameHashes.SleepDisturbedByMovement, delegate(SleepChore.StatesInstance smi)
				{
					this.isDisturbedByMovement.Set(true, smi, false);
				});
			this.sleep.uninterruptable.DoNothing();
			this.sleep.normal.ParamTransition<bool>(this.isInterruptable, this.sleep.uninterruptable, GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.IsFalse).ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().DuplicantStatusItems.Sleeping, null).QueueAnim("working_loop", true, null)
				.ParamTransition<bool>(this.isDisturbedByNoise, this.sleep.interrupt_noise, GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.IsTrue)
				.ParamTransition<bool>(this.isDisturbedByLight, this.sleep.interrupt_light, GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.IsTrue)
				.ParamTransition<bool>(this.isScaredOfDark, this.sleep.interrupt_scared, GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.IsTrue)
				.ParamTransition<bool>(this.isDisturbedByMovement, this.sleep.interrupt_movement, GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.IsTrue)
				.Update(delegate(SleepChore.StatesInstance smi, float dt)
				{
					smi.CheckLightLevel();
				}, UpdateRate.SIM_200ms, false);
			this.sleep.interrupt_scared.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().DuplicantStatusItems.SleepingInterruptedByFearOfDark, null).QueueAnim("interrupt_afraid", false, null).OnAnimQueueComplete(this.sleep.interrupt_scared_transition);
			this.sleep.interrupt_scared_transition.Enter(delegate(SleepChore.StatesInstance smi)
			{
				if (!smi.master.GetComponent<Effects>().HasEffect(Db.Get().effects.Get("TerribleSleep")))
				{
					smi.master.GetComponent<Effects>().Add(Db.Get().effects.Get("BadSleepAfraidOfDark"), true);
				}
				GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State state = (smi.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Sleep) ? this.sleep.normal : this.success);
				this.isScaredOfDark.Set(false, smi, false);
				smi.GoTo(state);
			});
			this.sleep.interrupt_movement.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().DuplicantStatusItems.SleepingInterruptedByMovement, null).PlayAnim("interrupt_light").OnAnimQueueComplete(this.sleep.interrupt_movement_transition)
				.Enter(delegate(SleepChore.StatesInstance smi)
				{
					GameObject gameObject = smi.sm.bed.Get(smi);
					if (gameObject != null)
					{
						gameObject.Trigger(-717201811, null);
					}
				});
			this.sleep.interrupt_movement_transition.Enter(delegate(SleepChore.StatesInstance smi)
			{
				if (!smi.master.GetComponent<Effects>().HasEffect(Db.Get().effects.Get("TerribleSleep")))
				{
					smi.master.GetComponent<Effects>().Add(Db.Get().effects.Get("BadSleepMovement"), true);
				}
				GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State state2 = (smi.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Sleep) ? this.sleep.normal : this.success);
				this.isDisturbedByMovement.Set(false, smi, false);
				smi.GoTo(state2);
			});
			this.sleep.interrupt_noise.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().DuplicantStatusItems.SleepingInterruptedByNoise, null).QueueAnim("interrupt_light", false, null).OnAnimQueueComplete(this.sleep.interrupt_noise_transition);
			this.sleep.interrupt_noise_transition.Enter(delegate(SleepChore.StatesInstance smi)
			{
				Effects component = smi.master.GetComponent<Effects>();
				component.Add(Db.Get().effects.Get("TerribleSleep"), true);
				if (component.HasEffect(Db.Get().effects.Get("BadSleep")))
				{
					component.Remove(Db.Get().effects.Get("BadSleep"));
				}
				this.isDisturbedByNoise.Set(false, smi, false);
				GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State state3 = (smi.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Sleep) ? this.sleep.normal : this.success);
				smi.GoTo(state3);
			});
			this.sleep.interrupt_light.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().DuplicantStatusItems.SleepingInterruptedByLight, null).QueueAnim("interrupt", false, null).OnAnimQueueComplete(this.sleep.interrupt_light_transition);
			this.sleep.interrupt_light_transition.Enter(delegate(SleepChore.StatesInstance smi)
			{
				if (!smi.master.GetComponent<Effects>().HasEffect(Db.Get().effects.Get("TerribleSleep")))
				{
					smi.master.GetComponent<Effects>().Add(Db.Get().effects.Get("BadSleep"), true);
				}
				GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State state4 = (smi.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Sleep) ? this.sleep.normal : this.success);
				this.isDisturbedByLight.Set(false, smi, false);
				smi.GoTo(state4);
			});
			this.success.Enter(delegate(SleepChore.StatesInstance smi)
			{
				smi.EvaluateSleepQuality();
			}).ReturnSuccess();
		}

		// Token: 0x040054E4 RID: 21732
		public StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.TargetParameter sleeper;

		// Token: 0x040054E5 RID: 21733
		public StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.TargetParameter bed;

		// Token: 0x040054E6 RID: 21734
		public StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.BoolParameter isInterruptable;

		// Token: 0x040054E7 RID: 21735
		public StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.BoolParameter isDisturbedByNoise;

		// Token: 0x040054E8 RID: 21736
		public StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.BoolParameter isDisturbedByLight;

		// Token: 0x040054E9 RID: 21737
		public StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.BoolParameter isDisturbedByMovement;

		// Token: 0x040054EA RID: 21738
		public StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.BoolParameter isScaredOfDark;

		// Token: 0x040054EB RID: 21739
		public StateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.BoolParameter needsNightLight;

		// Token: 0x040054EC RID: 21740
		public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.ApproachSubState<IApproachable> approach;

		// Token: 0x040054ED RID: 21741
		public SleepChore.States.SleepStates sleep;

		// Token: 0x040054EE RID: 21742
		public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State success;

		// Token: 0x02001ECD RID: 7885
		public class SleepStates : GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State
		{
			// Token: 0x040089F6 RID: 35318
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State condition_transition;

			// Token: 0x040089F7 RID: 35319
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State condition_transition_pre;

			// Token: 0x040089F8 RID: 35320
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State uninterruptable;

			// Token: 0x040089F9 RID: 35321
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State normal;

			// Token: 0x040089FA RID: 35322
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State interrupt_noise;

			// Token: 0x040089FB RID: 35323
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State interrupt_noise_transition;

			// Token: 0x040089FC RID: 35324
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State interrupt_light;

			// Token: 0x040089FD RID: 35325
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State interrupt_light_transition;

			// Token: 0x040089FE RID: 35326
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State interrupt_scared;

			// Token: 0x040089FF RID: 35327
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State interrupt_scared_transition;

			// Token: 0x04008A00 RID: 35328
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State interrupt_movement;

			// Token: 0x04008A01 RID: 35329
			public GameStateMachine<SleepChore.States, SleepChore.StatesInstance, SleepChore, object>.State interrupt_movement_transition;
		}
	}
}
