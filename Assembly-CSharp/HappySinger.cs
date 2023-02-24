using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020003B7 RID: 951
public class HappySinger : GameStateMachine<HappySinger, HappySinger.Instance>
{
	// Token: 0x060013B0 RID: 5040 RVA: 0x0006813C File Offset: 0x0006633C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false);
		this.overjoyed.DefaultState(this.overjoyed.idle).TagTransition(GameTags.Overjoyed, this.neutral, true).ToggleEffect("IsJoySinger")
			.ToggleLoopingSound(this.soundPath, null, true, true, true)
			.ToggleAnims("anim_loco_singer_kanim", 0f, "")
			.ToggleAnims("anim_idle_singer_kanim", 0f, "")
			.EventHandler(GameHashes.TagsChanged, delegate(HappySinger.Instance smi, object obj)
			{
				smi.musicParticleFX.SetActive(!smi.HasTag(GameTags.Asleep));
			})
			.Enter(delegate(HappySinger.Instance smi)
			{
				smi.musicParticleFX = Util.KInstantiate(EffectPrefabs.Instance.HappySingerFX, smi.master.transform.GetPosition() + this.offset);
				smi.musicParticleFX.transform.SetParent(smi.master.transform);
				smi.CreatePasserbyReactable();
				smi.musicParticleFX.SetActive(!smi.HasTag(GameTags.Asleep));
			})
			.Update(delegate(HappySinger.Instance smi, float dt)
			{
				if (!smi.GetSpeechMonitor().IsPlayingSpeech() && SpeechMonitor.IsAllowedToPlaySpeech(smi.gameObject))
				{
					smi.GetSpeechMonitor().PlaySpeech(Db.Get().Thoughts.CatchyTune.speechPrefix, Db.Get().Thoughts.CatchyTune.sound);
				}
			}, UpdateRate.SIM_1000ms, false)
			.Exit(delegate(HappySinger.Instance smi)
			{
				Util.KDestroyGameObject(smi.musicParticleFX);
				smi.ClearPasserbyReactable();
				smi.musicParticleFX.SetActive(false);
			});
	}

	// Token: 0x04000AAC RID: 2732
	private Vector3 offset = new Vector3(0f, 0f, 0.1f);

	// Token: 0x04000AAD RID: 2733
	public GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000AAE RID: 2734
	public HappySinger.OverjoyedStates overjoyed;

	// Token: 0x04000AAF RID: 2735
	public string soundPath = GlobalAssets.GetSound("DupeSinging_NotesFX_LP", false);

	// Token: 0x02000FD9 RID: 4057
	public class OverjoyedStates : GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400559C RID: 21916
		public GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x0400559D RID: 21917
		public GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.State moving;
	}

	// Token: 0x02000FDA RID: 4058
	public new class Instance : GameStateMachine<HappySinger, HappySinger.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060070AB RID: 28843 RVA: 0x002A74C0 File Offset: 0x002A56C0
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x060070AC RID: 28844 RVA: 0x002A74CC File Offset: 0x002A56CC
		public void CreatePasserbyReactable()
		{
			if (this.passerbyReactable == null)
			{
				EmoteReactable emoteReactable = new EmoteReactable(base.gameObject, "WorkPasserbyAcknowledgement", Db.Get().ChoreTypes.Emote, 5, 5, 0f, 600f, float.PositiveInfinity, 0f);
				Emote sing = Db.Get().Emotes.Minion.Sing;
				emoteReactable.SetEmote(sing).SetThought(Db.Get().Thoughts.CatchyTune).AddPrecondition(new Reactable.ReactablePrecondition(this.ReactorIsOnFloor));
				emoteReactable.RegisterEmoteStepCallbacks("react", new Action<GameObject>(this.AddReactionEffect), null);
				this.passerbyReactable = emoteReactable;
			}
		}

		// Token: 0x060070AD RID: 28845 RVA: 0x002A7586 File Offset: 0x002A5786
		public SpeechMonitor.Instance GetSpeechMonitor()
		{
			if (this.speechMonitor == null)
			{
				this.speechMonitor = base.master.gameObject.GetSMI<SpeechMonitor.Instance>();
			}
			return this.speechMonitor;
		}

		// Token: 0x060070AE RID: 28846 RVA: 0x002A75AC File Offset: 0x002A57AC
		private void AddReactionEffect(GameObject reactor)
		{
			reactor.Trigger(-1278274506, null);
		}

		// Token: 0x060070AF RID: 28847 RVA: 0x002A75BA File Offset: 0x002A57BA
		private bool ReactorIsOnFloor(GameObject reactor, Navigator.ActiveTransition transition)
		{
			return transition.end == NavType.Floor;
		}

		// Token: 0x060070B0 RID: 28848 RVA: 0x002A75C5 File Offset: 0x002A57C5
		public void ClearPasserbyReactable()
		{
			if (this.passerbyReactable != null)
			{
				this.passerbyReactable.Cleanup();
				this.passerbyReactable = null;
			}
		}

		// Token: 0x0400559E RID: 21918
		private Reactable passerbyReactable;

		// Token: 0x0400559F RID: 21919
		public GameObject musicParticleFX;

		// Token: 0x040055A0 RID: 21920
		public SpeechMonitor.Instance speechMonitor;
	}
}
