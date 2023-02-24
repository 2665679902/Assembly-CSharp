using System;
using Database;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000375 RID: 885
public class BalloonArtistChore : Chore<BalloonArtistChore.StatesInstance>, IWorkerPrioritizable
{
	// Token: 0x06001222 RID: 4642 RVA: 0x0005FF7C File Offset: 0x0005E17C
	public BalloonArtistChore(IStateMachineTarget target)
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "HasBalloonStallCell";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_BALLOON_STALL_CELL;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((BalloonArtistChore)data).smi.HasBalloonStallCell();
		};
		this.HasBalloonStallCell = precondition;
		base..ctor(Db.Get().ChoreTypes.JoyReaction, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.high, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime);
		this.showAvailabilityInHoverText = false;
		base.smi = new BalloonArtistChore.StatesInstance(this, target.gameObject);
		base.AddPrecondition(this.HasBalloonStallCell, this);
		base.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		base.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Recreation);
		base.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, this);
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x00060075 File Offset: 0x0005E275
	public bool GetWorkerPriority(Worker worker, out int priority)
	{
		priority = this.basePriority;
		return true;
	}

	// Token: 0x040009B1 RID: 2481
	private int basePriority = RELAXATION.PRIORITY.TIER1;

	// Token: 0x040009B2 RID: 2482
	private Chore.Precondition HasBalloonStallCell;

	// Token: 0x02000F4C RID: 3916
	public class States : GameStateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore>
	{
		// Token: 0x06006EDC RID: 28380 RVA: 0x0029DC64 File Offset: 0x0029BE64
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.goToStand;
			base.Target(this.balloonArtist);
			this.root.EventTransition(GameHashes.ScheduleBlocksChanged, this.idle, (BalloonArtistChore.StatesInstance smi) => !smi.IsRecTime());
			this.idle.DoNothing();
			this.goToStand.Transition(null, (BalloonArtistChore.StatesInstance smi) => !smi.HasBalloonStallCell(), UpdateRate.SIM_200ms).MoveTo((BalloonArtistChore.StatesInstance smi) => smi.GetBalloonStallCell(), this.balloonStand, null, false);
			this.balloonStand.ToggleAnims("anim_interacts_balloon_artist_kanim", 0f, "").Enter(delegate(BalloonArtistChore.StatesInstance smi)
			{
				smi.SpawnBalloonStand();
			}).Enter(delegate(BalloonArtistChore.StatesInstance smi)
			{
				this.balloonArtist.GetSMI<BalloonArtist.Instance>(smi).Internal_InitBalloons();
			})
				.Exit(delegate(BalloonArtistChore.StatesInstance smi)
				{
					smi.DestroyBalloonStand();
				})
				.DefaultState(this.balloonStand.idle);
			this.balloonStand.idle.PlayAnim("working_pre").QueueAnim("working_loop", true, null).OnSignal(this.giveBalloonOut, this.balloonStand.giveBalloon);
			this.balloonStand.giveBalloon.PlayAnim("working_pst").OnAnimQueueComplete(this.balloonStand.idle);
		}

		// Token: 0x040053C7 RID: 21447
		public StateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.TargetParameter balloonArtist;

		// Token: 0x040053C8 RID: 21448
		public StateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.IntParameter balloonsGivenOut = new StateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.IntParameter(0);

		// Token: 0x040053C9 RID: 21449
		public StateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.Signal giveBalloonOut;

		// Token: 0x040053CA RID: 21450
		public GameStateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.State idle;

		// Token: 0x040053CB RID: 21451
		public GameStateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.State goToStand;

		// Token: 0x040053CC RID: 21452
		public BalloonArtistChore.States.BalloonStandStates balloonStand;

		// Token: 0x02001EA0 RID: 7840
		public class BalloonStandStates : GameStateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.State
		{
			// Token: 0x04008933 RID: 35123
			public GameStateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.State idle;

			// Token: 0x04008934 RID: 35124
			public GameStateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.State giveBalloon;
		}
	}

	// Token: 0x02000F4D RID: 3917
	public class StatesInstance : GameStateMachine<BalloonArtistChore.States, BalloonArtistChore.StatesInstance, BalloonArtistChore, object>.GameInstance
	{
		// Token: 0x06006EDF RID: 28383 RVA: 0x0029DE29 File Offset: 0x0029C029
		public StatesInstance(BalloonArtistChore master, GameObject balloonArtist)
			: base(master)
		{
			this.balloonArtist = balloonArtist;
			base.sm.balloonArtist.Set(balloonArtist, base.smi, false);
		}

		// Token: 0x06006EE0 RID: 28384 RVA: 0x0029DE52 File Offset: 0x0029C052
		public bool IsRecTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Recreation);
		}

		// Token: 0x06006EE1 RID: 28385 RVA: 0x0029DE73 File Offset: 0x0029C073
		public int GetBalloonStallCell()
		{
			return this.balloonArtistCellSensor.GetCell();
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x0029DE80 File Offset: 0x0029C080
		public int GetBalloonStallTargetCell()
		{
			return this.balloonArtistCellSensor.GetStandCell();
		}

		// Token: 0x06006EE3 RID: 28387 RVA: 0x0029DE8D File Offset: 0x0029C08D
		public bool HasBalloonStallCell()
		{
			if (this.balloonArtistCellSensor == null)
			{
				this.balloonArtistCellSensor = base.GetComponent<Sensors>().GetSensor<BalloonStandCellSensor>();
			}
			return this.balloonArtistCellSensor.GetCell() != Grid.InvalidCell;
		}

		// Token: 0x06006EE4 RID: 28388 RVA: 0x0029DEC0 File Offset: 0x0029C0C0
		public bool IsSameRoom()
		{
			int num = Grid.PosToCell(this.balloonArtist);
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
			CavityInfo cavityForCell2 = Game.Instance.roomProber.GetCavityForCell(this.GetBalloonStallCell());
			return cavityForCell != null && cavityForCell2 != null && cavityForCell.handle == cavityForCell2.handle;
		}

		// Token: 0x06006EE5 RID: 28389 RVA: 0x0029DF1C File Offset: 0x0029C11C
		public void SpawnBalloonStand()
		{
			Vector3 vector = Grid.CellToPos(this.GetBalloonStallTargetCell());
			this.balloonArtist.GetComponent<Facing>().Face(vector);
			this.balloonStand = Util.KInstantiate(Assets.GetPrefab("BalloonStand"), vector, Quaternion.identity, null, null, true, 0);
			this.balloonStand.SetActive(true);
			this.balloonStand.GetComponent<GetBalloonWorkable>().SetBalloonArtist(base.smi);
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x0029DF8C File Offset: 0x0029C18C
		public void DestroyBalloonStand()
		{
			this.balloonStand.DeleteObject();
		}

		// Token: 0x06006EE7 RID: 28391 RVA: 0x0029DF99 File Offset: 0x0029C199
		public BalloonOverrideSymbol GetBalloonOverride()
		{
			return this.balloonArtist.GetSMI<BalloonArtist.Instance>().GetCurrentBalloonSymbolOverride();
		}

		// Token: 0x06006EE8 RID: 28392 RVA: 0x0029DFAB File Offset: 0x0029C1AB
		public void NextBalloonOverride()
		{
			this.balloonArtist.GetSMI<BalloonArtist.Instance>().ApplyNextBalloonSymbolOverride();
		}

		// Token: 0x06006EE9 RID: 28393 RVA: 0x0029DFC0 File Offset: 0x0029C1C0
		public void GiveBalloon(BalloonOverrideSymbol balloonOverride)
		{
			BalloonArtist.Instance smi = this.balloonArtist.GetSMI<BalloonArtist.Instance>();
			smi.GiveBalloon();
			balloonOverride.ApplyTo(smi);
			base.smi.sm.giveBalloonOut.Trigger(base.smi);
		}

		// Token: 0x040053CD RID: 21453
		private BalloonStandCellSensor balloonArtistCellSensor;

		// Token: 0x040053CE RID: 21454
		private GameObject balloonArtist;

		// Token: 0x040053CF RID: 21455
		private GameObject balloonStand;
	}
}
