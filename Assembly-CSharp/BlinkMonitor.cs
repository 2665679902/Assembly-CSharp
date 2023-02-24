using System;
using UnityEngine;

// Token: 0x0200081B RID: 2075
public class BlinkMonitor : GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>
{
	// Token: 0x06003C3C RID: 15420 RVA: 0x0014F904 File Offset: 0x0014DB04
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.Enter(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State.Callback(BlinkMonitor.CreateEyes)).Exit(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State.Callback(BlinkMonitor.DestroyEyes));
		this.satisfied.ScheduleGoTo(new Func<BlinkMonitor.Instance, float>(BlinkMonitor.GetRandomBlinkTime), this.blinking);
		this.blinking.EnterTransition(this.satisfied, GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.Not(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.Transition.ConditionCallback(BlinkMonitor.CanBlink))).Enter(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State.Callback(BlinkMonitor.BeginBlinking)).Update(new Action<BlinkMonitor.Instance, float>(BlinkMonitor.UpdateBlinking), UpdateRate.RENDER_EVERY_TICK, false)
			.Target(this.eyes)
			.OnAnimQueueComplete(this.satisfied)
			.Exit(new StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State.Callback(BlinkMonitor.EndBlinking));
	}

	// Token: 0x06003C3D RID: 15421 RVA: 0x0014F9CE File Offset: 0x0014DBCE
	private static bool CanBlink(BlinkMonitor.Instance smi)
	{
		return SpeechMonitor.IsAllowedToPlaySpeech(smi.gameObject) && smi.Get<Navigator>().CurrentNavType != NavType.Ladder;
	}

	// Token: 0x06003C3E RID: 15422 RVA: 0x0014F9F0 File Offset: 0x0014DBF0
	private static float GetRandomBlinkTime(BlinkMonitor.Instance smi)
	{
		return UnityEngine.Random.Range(TuningData<BlinkMonitor.Tuning>.Get().randomBlinkIntervalMin, TuningData<BlinkMonitor.Tuning>.Get().randomBlinkIntervalMax);
	}

	// Token: 0x06003C3F RID: 15423 RVA: 0x0014FA0C File Offset: 0x0014DC0C
	private static void CreateEyes(BlinkMonitor.Instance smi)
	{
		smi.eyes = Util.KInstantiate(Assets.GetPrefab(EyeAnimation.ID), null, null).GetComponent<KBatchedAnimController>();
		smi.eyes.gameObject.SetActive(true);
		smi.sm.eyes.Set(smi.eyes.gameObject, smi, false);
	}

	// Token: 0x06003C40 RID: 15424 RVA: 0x0014FA69 File Offset: 0x0014DC69
	private static void DestroyEyes(BlinkMonitor.Instance smi)
	{
		if (smi.eyes != null)
		{
			Util.KDestroyGameObject(smi.eyes);
			smi.eyes = null;
		}
	}

	// Token: 0x06003C41 RID: 15425 RVA: 0x0014FA8C File Offset: 0x0014DC8C
	public static void BeginBlinking(BlinkMonitor.Instance smi)
	{
		string text = "eyes1";
		smi.eyes.Play(text, KAnim.PlayMode.Once, 1f, 0f);
		BlinkMonitor.UpdateBlinking(smi, 0f);
	}

	// Token: 0x06003C42 RID: 15426 RVA: 0x0014FAC6 File Offset: 0x0014DCC6
	public static void EndBlinking(BlinkMonitor.Instance smi)
	{
		smi.GetComponent<SymbolOverrideController>().RemoveSymbolOverride(BlinkMonitor.HASH_SNAPTO_EYES, 3);
	}

	// Token: 0x06003C43 RID: 15427 RVA: 0x0014FADC File Offset: 0x0014DCDC
	public static void UpdateBlinking(BlinkMonitor.Instance smi, float dt)
	{
		int currentFrameIndex = smi.eyes.GetCurrentFrameIndex();
		KAnimBatch batch = smi.eyes.GetBatch();
		if (currentFrameIndex == -1 || batch == null)
		{
			return;
		}
		KAnim.Anim.Frame frame = smi.eyes.GetBatch().group.data.GetFrame(currentFrameIndex);
		if (frame == KAnim.Anim.Frame.InvalidFrame)
		{
			return;
		}
		HashedString hashedString = HashedString.Invalid;
		for (int i = 0; i < frame.numElements; i++)
		{
			int num = frame.firstElementIdx + i;
			if (num < batch.group.data.frameElements.Count)
			{
				KAnim.Anim.FrameElement frameElement = batch.group.data.frameElements[num];
				if (!(frameElement.symbol == HashedString.Invalid))
				{
					hashedString = frameElement.symbol;
					break;
				}
			}
		}
		smi.GetComponent<SymbolOverrideController>().AddSymbolOverride(BlinkMonitor.HASH_SNAPTO_EYES, smi.eyes.AnimFiles[0].GetData().build.GetSymbol(hashedString), 3);
	}

	// Token: 0x04002736 RID: 10038
	public GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State satisfied;

	// Token: 0x04002737 RID: 10039
	public GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.State blinking;

	// Token: 0x04002738 RID: 10040
	public StateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.TargetParameter eyes;

	// Token: 0x04002739 RID: 10041
	private static HashedString HASH_SNAPTO_EYES = "snapto_eyes";

	// Token: 0x0200157B RID: 5499
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200157C RID: 5500
	public class Tuning : TuningData<BlinkMonitor.Tuning>
	{
		// Token: 0x040066E4 RID: 26340
		public float randomBlinkIntervalMin;

		// Token: 0x040066E5 RID: 26341
		public float randomBlinkIntervalMax;
	}

	// Token: 0x0200157D RID: 5501
	public new class Instance : GameStateMachine<BlinkMonitor, BlinkMonitor.Instance, IStateMachineTarget, BlinkMonitor.Def>.GameInstance
	{
		// Token: 0x06008411 RID: 33809 RVA: 0x002E934D File Offset: 0x002E754D
		public Instance(IStateMachineTarget master, BlinkMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008412 RID: 33810 RVA: 0x002E9357 File Offset: 0x002E7557
		public bool IsBlinking()
		{
			return base.IsInsideState(base.sm.blinking);
		}

		// Token: 0x06008413 RID: 33811 RVA: 0x002E936A File Offset: 0x002E756A
		public void Blink()
		{
			this.GoTo(base.sm.blinking);
		}

		// Token: 0x040066E6 RID: 26342
		public KBatchedAnimController eyes;
	}
}
