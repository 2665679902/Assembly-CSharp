using System;
using System.Runtime.Serialization;
using Database;
using KSerialization;
using TUNING;

// Token: 0x020003B6 RID: 950
public class BalloonArtist : GameStateMachine<BalloonArtist, BalloonArtist.Instance>
{
	// Token: 0x060013AC RID: 5036 RVA: 0x00067F38 File Offset: 0x00066138
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false);
		this.overjoyed.TagTransition(GameTags.Overjoyed, this.neutral, true).DefaultState(this.overjoyed.idle).ParamTransition<int>(this.balloonsGivenOut, this.overjoyed.exitEarly, (BalloonArtist.Instance smi, int p) => p >= TRAITS.JOY_REACTIONS.BALLOON_ARTIST.NUM_BALLOONS_TO_GIVE)
			.Exit(delegate(BalloonArtist.Instance smi)
			{
				smi.numBalloonsGiven = 0;
				this.balloonsGivenOut.Set(0, smi, false);
			});
		this.overjoyed.idle.Enter(delegate(BalloonArtist.Instance smi)
		{
			if (smi.IsRecTime())
			{
				smi.GoTo(this.overjoyed.balloon_stand);
			}
		}).ToggleStatusItem(Db.Get().DuplicantStatusItems.BalloonArtistPlanning, null).EventTransition(GameHashes.ScheduleBlocksChanged, this.overjoyed.balloon_stand, (BalloonArtist.Instance smi) => smi.IsRecTime());
		this.overjoyed.balloon_stand.ToggleStatusItem(Db.Get().DuplicantStatusItems.BalloonArtistHandingOut, null).EventTransition(GameHashes.ScheduleBlocksChanged, this.overjoyed.idle, (BalloonArtist.Instance smi) => !smi.IsRecTime()).ToggleChore((BalloonArtist.Instance smi) => new BalloonArtistChore(smi.master), this.overjoyed.idle);
		this.overjoyed.exitEarly.Enter(delegate(BalloonArtist.Instance smi)
		{
			smi.ExitJoyReactionEarly();
		});
	}

	// Token: 0x04000AA9 RID: 2729
	public StateMachine<BalloonArtist, BalloonArtist.Instance, IStateMachineTarget, object>.IntParameter balloonsGivenOut;

	// Token: 0x04000AAA RID: 2730
	public GameStateMachine<BalloonArtist, BalloonArtist.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000AAB RID: 2731
	public BalloonArtist.OverjoyedStates overjoyed;

	// Token: 0x02000FD6 RID: 4054
	public class OverjoyedStates : GameStateMachine<BalloonArtist, BalloonArtist.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400558F RID: 21903
		public GameStateMachine<BalloonArtist, BalloonArtist.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x04005590 RID: 21904
		public GameStateMachine<BalloonArtist, BalloonArtist.Instance, IStateMachineTarget, object>.State balloon_stand;

		// Token: 0x04005591 RID: 21905
		public GameStateMachine<BalloonArtist, BalloonArtist.Instance, IStateMachineTarget, object>.State exitEarly;
	}

	// Token: 0x02000FD7 RID: 4055
	public new class Instance : GameStateMachine<BalloonArtist, BalloonArtist.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600709A RID: 28826 RVA: 0x002A7224 File Offset: 0x002A5424
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600709B RID: 28827 RVA: 0x002A722D File Offset: 0x002A542D
		[OnDeserialized]
		private void OnDeserialized()
		{
			base.smi.sm.balloonsGivenOut.Set(this.numBalloonsGiven, base.smi, false);
		}

		// Token: 0x0600709C RID: 28828 RVA: 0x002A7254 File Offset: 0x002A5454
		public void Internal_InitBalloons()
		{
			JoyResponseOutfitTarget joyResponseOutfitTarget = JoyResponseOutfitTarget.FromMinion(base.master.gameObject);
			if (!this.balloonSymbolIter.IsNullOrDestroyed())
			{
				if (this.balloonSymbolIter.facade.AndThen<string>((BalloonArtistFacadeResource f) => f.Id) == joyResponseOutfitTarget.ReadFacadeId())
				{
					return;
				}
			}
			this.balloonSymbolIter = joyResponseOutfitTarget.ReadFacadeId().AndThen<BalloonArtistFacadeResource>((string id) => Db.Get().Permits.BalloonArtistFacades.Get(id)).AndThen<BalloonOverrideSymbolIter>((BalloonArtistFacadeResource permit) => permit.GetSymbolIter())
				.UnwrapOr(new BalloonOverrideSymbolIter(Option.None), null);
			this.SetBalloonSymbolOverride(this.balloonSymbolIter.Current());
		}

		// Token: 0x0600709D RID: 28829 RVA: 0x002A7341 File Offset: 0x002A5541
		public bool IsRecTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Recreation);
		}

		// Token: 0x0600709E RID: 28830 RVA: 0x002A7364 File Offset: 0x002A5564
		public void SetBalloonSymbolOverride(BalloonOverrideSymbol balloonOverrideSymbol)
		{
			if (balloonOverrideSymbol.animFile.IsNone())
			{
				base.master.GetComponent<SymbolOverrideController>().AddSymbolOverride("body", Assets.GetAnim("balloon_anim_kanim").GetData().build.GetSymbol("body"), 0);
				return;
			}
			base.master.GetComponent<SymbolOverrideController>().AddSymbolOverride("body", balloonOverrideSymbol.symbol.Unwrap(), 0);
		}

		// Token: 0x0600709F RID: 28831 RVA: 0x002A73EC File Offset: 0x002A55EC
		public BalloonOverrideSymbol GetCurrentBalloonSymbolOverride()
		{
			return this.balloonSymbolIter.Current();
		}

		// Token: 0x060070A0 RID: 28832 RVA: 0x002A73F9 File Offset: 0x002A55F9
		public void ApplyNextBalloonSymbolOverride()
		{
			this.SetBalloonSymbolOverride(this.balloonSymbolIter.Next());
		}

		// Token: 0x060070A1 RID: 28833 RVA: 0x002A740C File Offset: 0x002A560C
		public void GiveBalloon()
		{
			this.numBalloonsGiven++;
			base.smi.sm.balloonsGivenOut.Set(this.numBalloonsGiven, base.smi, false);
		}

		// Token: 0x060070A2 RID: 28834 RVA: 0x002A7440 File Offset: 0x002A5640
		public void ExitJoyReactionEarly()
		{
			JoyBehaviourMonitor.Instance smi = base.master.gameObject.GetSMI<JoyBehaviourMonitor.Instance>();
			smi.sm.exitEarly.Trigger(smi);
		}

		// Token: 0x04005592 RID: 21906
		[Serialize]
		public int numBalloonsGiven;

		// Token: 0x04005593 RID: 21907
		[NonSerialized]
		private BalloonOverrideSymbolIter balloonSymbolIter;

		// Token: 0x04005594 RID: 21908
		private const string TARGET_SYMBOL_TO_OVERRIDE = "body";

		// Token: 0x04005595 RID: 21909
		private const int TARGET_OVERRIDE_PRIORITY = 0;
	}
}
