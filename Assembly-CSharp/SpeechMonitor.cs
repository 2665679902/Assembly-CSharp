using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000846 RID: 2118
public class SpeechMonitor : GameStateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>
{
	// Token: 0x06003D06 RID: 15622 RVA: 0x00154DB4 File Offset: 0x00152FB4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.Enter(new StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State.Callback(SpeechMonitor.CreateMouth)).Exit(new StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State.Callback(SpeechMonitor.DestroyMouth));
		this.satisfied.DoNothing();
		this.talking.Enter(new StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State.Callback(SpeechMonitor.BeginTalking)).Update(new Action<SpeechMonitor.Instance, float>(SpeechMonitor.UpdateTalking), UpdateRate.RENDER_EVERY_TICK, false).Target(this.mouth)
			.OnAnimQueueComplete(this.satisfied)
			.Exit(new StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State.Callback(SpeechMonitor.EndTalking));
	}

	// Token: 0x06003D07 RID: 15623 RVA: 0x00154E50 File Offset: 0x00153050
	private static void CreateMouth(SpeechMonitor.Instance smi)
	{
		smi.mouth = global::Util.KInstantiate(Assets.GetPrefab(MouthAnimation.ID), null, null).GetComponent<KBatchedAnimController>();
		smi.mouth.gameObject.SetActive(true);
		smi.sm.mouth.Set(smi.mouth.gameObject, smi, false);
	}

	// Token: 0x06003D08 RID: 15624 RVA: 0x00154EAD File Offset: 0x001530AD
	private static void DestroyMouth(SpeechMonitor.Instance smi)
	{
		if (smi.mouth != null)
		{
			global::Util.KDestroyGameObject(smi.mouth);
			smi.mouth = null;
		}
	}

	// Token: 0x06003D09 RID: 15625 RVA: 0x00154ED0 File Offset: 0x001530D0
	private static string GetRandomSpeechAnim(string speech_prefix)
	{
		return speech_prefix + UnityEngine.Random.Range(1, TuningData<SpeechMonitor.Tuning>.Get().speechCount).ToString();
	}

	// Token: 0x06003D0A RID: 15626 RVA: 0x00154EFC File Offset: 0x001530FC
	public static bool IsAllowedToPlaySpeech(GameObject go)
	{
		if (go.HasTag(GameTags.Dead))
		{
			return false;
		}
		KBatchedAnimController component = go.GetComponent<KBatchedAnimController>();
		KAnim.Anim currentAnim = component.GetCurrentAnim();
		return currentAnim == null || (GameAudioSheets.Get().IsAnimAllowedToPlaySpeech(currentAnim) && SpeechMonitor.CanOverrideHead(component));
	}

	// Token: 0x06003D0B RID: 15627 RVA: 0x00154F40 File Offset: 0x00153140
	private static bool CanOverrideHead(KBatchedAnimController kbac)
	{
		bool flag = true;
		KAnim.Anim currentAnim = kbac.GetCurrentAnim();
		if (currentAnim == null)
		{
			return false;
		}
		int currentFrameIndex = kbac.GetCurrentFrameIndex();
		if (currentFrameIndex <= 0)
		{
			return false;
		}
		KBatchGroupData batchGroupData = KAnimBatchManager.Instance().GetBatchGroupData(currentAnim.animFile.animBatchTag);
		KAnim.Anim.Frame frame = batchGroupData.GetFrame(currentFrameIndex);
		for (int i = 0; i < frame.numElements; i++)
		{
			if (batchGroupData.GetFrameElement(frame.firstElementIdx + i).folder == SpeechMonitor.ANIM_HASH_HEAD_ANIM)
			{
				flag = false;
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003D0C RID: 15628 RVA: 0x00154FC4 File Offset: 0x001531C4
	public static void BeginTalking(SpeechMonitor.Instance smi)
	{
		smi.ev.clearHandle();
		if (smi.voiceEvent != null)
		{
			smi.ev = VoiceSoundEvent.PlayVoice(smi.voiceEvent, smi.GetComponent<KBatchedAnimController>(), 0f, false, false);
		}
		if (smi.ev.isValid())
		{
			smi.mouth.Play(SpeechMonitor.GetRandomSpeechAnim(smi.speechPrefix), KAnim.PlayMode.Once, 1f, 0f);
			smi.mouth.Queue(SpeechMonitor.GetRandomSpeechAnim(smi.speechPrefix), KAnim.PlayMode.Once, 1f, 0f);
			smi.mouth.Queue(SpeechMonitor.GetRandomSpeechAnim(smi.speechPrefix), KAnim.PlayMode.Once, 1f, 0f);
			smi.mouth.Queue(SpeechMonitor.GetRandomSpeechAnim(smi.speechPrefix), KAnim.PlayMode.Once, 1f, 0f);
		}
		else
		{
			smi.mouth.Play(SpeechMonitor.GetRandomSpeechAnim(smi.speechPrefix), KAnim.PlayMode.Once, 1f, 0f);
			smi.mouth.Queue(SpeechMonitor.GetRandomSpeechAnim(smi.speechPrefix), KAnim.PlayMode.Once, 1f, 0f);
		}
		SpeechMonitor.UpdateTalking(smi, 0f);
	}

	// Token: 0x06003D0D RID: 15629 RVA: 0x00155103 File Offset: 0x00153303
	public static void EndTalking(SpeechMonitor.Instance smi)
	{
		smi.GetComponent<SymbolOverrideController>().RemoveSymbolOverride(SpeechMonitor.HASH_SNAPTO_MOUTH, 3);
	}

	// Token: 0x06003D0E RID: 15630 RVA: 0x00155118 File Offset: 0x00153318
	public static KAnim.Anim.FrameElement GetFirstFrameElement(KBatchedAnimController controller)
	{
		KAnim.Anim.FrameElement frameElement = default(KAnim.Anim.FrameElement);
		frameElement.symbol = HashedString.Invalid;
		int currentFrameIndex = controller.GetCurrentFrameIndex();
		KAnimBatch batch = controller.GetBatch();
		if (currentFrameIndex == -1 || batch == null)
		{
			return frameElement;
		}
		KAnim.Anim.Frame frame = controller.GetBatch().group.data.GetFrame(currentFrameIndex);
		if (frame == KAnim.Anim.Frame.InvalidFrame)
		{
			return frameElement;
		}
		for (int i = 0; i < frame.numElements; i++)
		{
			int num = frame.firstElementIdx + i;
			if (num < batch.group.data.frameElements.Count)
			{
				KAnim.Anim.FrameElement frameElement2 = batch.group.data.frameElements[num];
				if (!(frameElement2.symbol == HashedString.Invalid))
				{
					frameElement = frameElement2;
					break;
				}
			}
		}
		return frameElement;
	}

	// Token: 0x06003D0F RID: 15631 RVA: 0x001551E8 File Offset: 0x001533E8
	public static void UpdateTalking(SpeechMonitor.Instance smi, float dt)
	{
		if (smi.ev.isValid())
		{
			PLAYBACK_STATE playback_STATE;
			smi.ev.getPlaybackState(out playback_STATE);
			if (playback_STATE == PLAYBACK_STATE.STOPPING || playback_STATE == PLAYBACK_STATE.STOPPED)
			{
				smi.GoTo(smi.sm.satisfied);
				smi.ev.clearHandle();
				return;
			}
		}
		KAnim.Anim.FrameElement firstFrameElement = SpeechMonitor.GetFirstFrameElement(smi.mouth);
		if (firstFrameElement.symbol == HashedString.Invalid)
		{
			return;
		}
		smi.Get<SymbolOverrideController>().AddSymbolOverride(SpeechMonitor.HASH_SNAPTO_MOUTH, smi.mouth.AnimFiles[0].GetData().build.GetSymbol(firstFrameElement.symbol), 3);
	}

	// Token: 0x040027F0 RID: 10224
	public GameStateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State satisfied;

	// Token: 0x040027F1 RID: 10225
	public GameStateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.State talking;

	// Token: 0x040027F2 RID: 10226
	public static string PREFIX_SAD = "sad";

	// Token: 0x040027F3 RID: 10227
	public static string PREFIX_HAPPY = "happy";

	// Token: 0x040027F4 RID: 10228
	public static string PREFIX_SINGER = "sing";

	// Token: 0x040027F5 RID: 10229
	public StateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.TargetParameter mouth;

	// Token: 0x040027F6 RID: 10230
	private static HashedString HASH_SNAPTO_MOUTH = "snapto_mouth";

	// Token: 0x040027F7 RID: 10231
	private static KAnimHashedString ANIM_HASH_HEAD_ANIM = "head_anim";

	// Token: 0x020015E3 RID: 5603
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020015E4 RID: 5604
	public class Tuning : TuningData<SpeechMonitor.Tuning>
	{
		// Token: 0x04006832 RID: 26674
		public float randomSpeechIntervalMin;

		// Token: 0x04006833 RID: 26675
		public float randomSpeechIntervalMax;

		// Token: 0x04006834 RID: 26676
		public int speechCount;
	}

	// Token: 0x020015E5 RID: 5605
	public new class Instance : GameStateMachine<SpeechMonitor, SpeechMonitor.Instance, IStateMachineTarget, SpeechMonitor.Def>.GameInstance
	{
		// Token: 0x060085CC RID: 34252 RVA: 0x002ED2D1 File Offset: 0x002EB4D1
		public Instance(IStateMachineTarget master, SpeechMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060085CD RID: 34253 RVA: 0x002ED2E6 File Offset: 0x002EB4E6
		public bool IsPlayingSpeech()
		{
			return base.IsInsideState(base.sm.talking);
		}

		// Token: 0x060085CE RID: 34254 RVA: 0x002ED2F9 File Offset: 0x002EB4F9
		public void PlaySpeech(string speech_prefix, string voice_event)
		{
			this.speechPrefix = speech_prefix;
			this.voiceEvent = voice_event;
			this.GoTo(base.sm.talking);
		}

		// Token: 0x060085CF RID: 34255 RVA: 0x002ED31C File Offset: 0x002EB51C
		public void DrawMouth()
		{
			KAnim.Anim.FrameElement firstFrameElement = SpeechMonitor.GetFirstFrameElement(base.smi.mouth);
			if (firstFrameElement.symbol == HashedString.Invalid)
			{
				return;
			}
			KAnim.Build.Symbol symbol = base.smi.mouth.AnimFiles[0].GetData().build.GetSymbol(firstFrameElement.symbol);
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			base.GetComponent<SymbolOverrideController>().AddSymbolOverride(SpeechMonitor.HASH_SNAPTO_MOUTH, base.smi.mouth.AnimFiles[0].GetData().build.GetSymbol(firstFrameElement.symbol), 3);
			KAnim.Build.Symbol symbol2 = KAnimBatchManager.Instance().GetBatchGroupData(component.batchGroupID).GetSymbol(SpeechMonitor.HASH_SNAPTO_MOUTH);
			KAnim.Build.SymbolFrameInstance symbolFrameInstance = KAnimBatchManager.Instance().GetBatchGroupData(symbol.build.batchTag).symbolFrameInstances[symbol.firstFrameIdx + firstFrameElement.frame];
			symbolFrameInstance.buildImageIdx = base.GetComponent<SymbolOverrideController>().GetAtlasIdx(symbol.build.GetTexture(0));
			component.SetSymbolOverride(symbol2.firstFrameIdx, ref symbolFrameInstance);
		}

		// Token: 0x04006835 RID: 26677
		public KBatchedAnimController mouth;

		// Token: 0x04006836 RID: 26678
		public string speechPrefix = "happy";

		// Token: 0x04006837 RID: 26679
		public string voiceEvent;

		// Token: 0x04006838 RID: 26680
		public EventInstance ev;
	}
}
