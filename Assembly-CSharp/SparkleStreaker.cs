using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020003B8 RID: 952
public class SparkleStreaker : GameStateMachine<SparkleStreaker, SparkleStreaker.Instance>
{
	// Token: 0x060013B3 RID: 5043 RVA: 0x00068314 File Offset: 0x00066514
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false);
		this.overjoyed.DefaultState(this.overjoyed.idle).TagTransition(GameTags.Overjoyed, this.neutral, true).ToggleEffect("IsSparkleStreaker")
			.ToggleLoopingSound(this.soundPath, null, true, true, true)
			.Enter(delegate(SparkleStreaker.Instance smi)
			{
				smi.sparkleStreakFX = Util.KInstantiate(EffectPrefabs.Instance.SparkleStreakFX, smi.master.transform.GetPosition() + this.offset);
				smi.sparkleStreakFX.transform.SetParent(smi.master.transform);
				smi.sparkleStreakFX.SetActive(true);
				smi.CreatePasserbyReactable();
			})
			.Exit(delegate(SparkleStreaker.Instance smi)
			{
				Util.KDestroyGameObject(smi.sparkleStreakFX);
				smi.ClearPasserbyReactable();
			});
		this.overjoyed.idle.Enter(delegate(SparkleStreaker.Instance smi)
		{
			smi.SetSparkleSoundParam(0f);
		}).EventTransition(GameHashes.ObjectMovementStateChanged, this.overjoyed.moving, (SparkleStreaker.Instance smi) => smi.IsMoving());
		this.overjoyed.moving.Enter(delegate(SparkleStreaker.Instance smi)
		{
			smi.SetSparkleSoundParam(1f);
		}).EventTransition(GameHashes.ObjectMovementStateChanged, this.overjoyed.idle, (SparkleStreaker.Instance smi) => !smi.IsMoving());
	}

	// Token: 0x04000AB0 RID: 2736
	private Vector3 offset = new Vector3(0f, 0f, 0.1f);

	// Token: 0x04000AB1 RID: 2737
	public GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000AB2 RID: 2738
	public SparkleStreaker.OverjoyedStates overjoyed;

	// Token: 0x04000AB3 RID: 2739
	public string soundPath = GlobalAssets.GetSound("SparkleStreaker_lp", false);

	// Token: 0x04000AB4 RID: 2740
	public HashedString SPARKLE_STREAKER_MOVING_PARAMETER = "sparkleStreaker_moving";

	// Token: 0x02000FDC RID: 4060
	public class OverjoyedStates : GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040055A5 RID: 21925
		public GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x040055A6 RID: 21926
		public GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.State moving;
	}

	// Token: 0x02000FDD RID: 4061
	public new class Instance : GameStateMachine<SparkleStreaker, SparkleStreaker.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060070B7 RID: 28855 RVA: 0x002A7691 File Offset: 0x002A5891
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x060070B8 RID: 28856 RVA: 0x002A769C File Offset: 0x002A589C
		public void CreatePasserbyReactable()
		{
			if (this.passerbyReactable == null)
			{
				EmoteReactable emoteReactable = new EmoteReactable(base.gameObject, "WorkPasserbyAcknowledgement", Db.Get().ChoreTypes.Emote, 5, 5, 0f, 600f, float.PositiveInfinity, 0f);
				Emote clapCheer = Db.Get().Emotes.Minion.ClapCheer;
				emoteReactable.SetEmote(clapCheer).SetThought(Db.Get().Thoughts.Happy).AddPrecondition(new Reactable.ReactablePrecondition(this.ReactorIsOnFloor));
				emoteReactable.RegisterEmoteStepCallbacks("clapcheer_pre", new Action<GameObject>(this.AddReactionEffect), null);
				this.passerbyReactable = emoteReactable;
			}
		}

		// Token: 0x060070B9 RID: 28857 RVA: 0x002A7756 File Offset: 0x002A5956
		private void AddReactionEffect(GameObject reactor)
		{
			reactor.GetComponent<Effects>().Add("SawSparkleStreaker", true);
		}

		// Token: 0x060070BA RID: 28858 RVA: 0x002A776A File Offset: 0x002A596A
		private bool ReactorIsOnFloor(GameObject reactor, Navigator.ActiveTransition transition)
		{
			return transition.end == NavType.Floor;
		}

		// Token: 0x060070BB RID: 28859 RVA: 0x002A7775 File Offset: 0x002A5975
		public void ClearPasserbyReactable()
		{
			if (this.passerbyReactable != null)
			{
				this.passerbyReactable.Cleanup();
				this.passerbyReactable = null;
			}
		}

		// Token: 0x060070BC RID: 28860 RVA: 0x002A7791 File Offset: 0x002A5991
		public bool IsMoving()
		{
			return base.smi.master.GetComponent<Navigator>().IsMoving();
		}

		// Token: 0x060070BD RID: 28861 RVA: 0x002A77A8 File Offset: 0x002A59A8
		public void SetSparkleSoundParam(float val)
		{
			base.GetComponent<LoopingSounds>().SetParameter(GlobalAssets.GetSound("SparkleStreaker_lp", false), "sparkleStreaker_moving", val);
		}

		// Token: 0x040055A7 RID: 21927
		private Reactable passerbyReactable;

		// Token: 0x040055A8 RID: 21928
		public GameObject sparkleStreakFX;
	}
}
