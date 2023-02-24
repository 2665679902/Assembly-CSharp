using System;
using UnityEngine;

// Token: 0x02000834 RID: 2100
public class InspirationEffectMonitor : GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>
{
	// Token: 0x06003CA3 RID: 15523 RVA: 0x0015254C File Offset: 0x0015074C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.EventHandler(GameHashes.CatchyTune, new GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.GameEvent.Callback(this.OnCatchyTune)).ParamTransition<bool>(this.shouldCatchyTune, this.catchyTune, (InspirationEffectMonitor.Instance smi, bool shouldCatchyTune) => shouldCatchyTune);
		this.catchyTune.Exit(delegate(InspirationEffectMonitor.Instance smi)
		{
			this.shouldCatchyTune.Set(false, smi, false);
		}).ToggleEffect("HeardJoySinger").ToggleThought(Db.Get().Thoughts.CatchyTune, null)
			.EventHandler(GameHashes.StartWork, new GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.GameEvent.Callback(this.TryThinkCatchyTune))
			.ToggleStatusItem(Db.Get().DuplicantStatusItems.JoyResponse_HeardJoySinger, null)
			.Enter(delegate(InspirationEffectMonitor.Instance smi)
			{
				this.SingCatchyTune(smi);
			})
			.Update(delegate(InspirationEffectMonitor.Instance smi, float dt)
			{
				this.TryThinkCatchyTune(smi, null);
				this.inspirationTimeRemaining.Delta(-dt, smi);
			}, UpdateRate.SIM_4000ms, false)
			.ParamTransition<float>(this.inspirationTimeRemaining, this.idle, (InspirationEffectMonitor.Instance smi, float p) => p <= 0f);
	}

	// Token: 0x06003CA4 RID: 15524 RVA: 0x0015266B File Offset: 0x0015086B
	private void OnCatchyTune(InspirationEffectMonitor.Instance smi, object data)
	{
		this.inspirationTimeRemaining.Set(600f, smi, false);
		this.shouldCatchyTune.Set(true, smi, false);
	}

	// Token: 0x06003CA5 RID: 15525 RVA: 0x0015268F File Offset: 0x0015088F
	private void TryThinkCatchyTune(InspirationEffectMonitor.Instance smi, object data)
	{
		if (UnityEngine.Random.Range(1, 101) > 66)
		{
			this.SingCatchyTune(smi);
		}
	}

	// Token: 0x06003CA6 RID: 15526 RVA: 0x001526A4 File Offset: 0x001508A4
	private void SingCatchyTune(InspirationEffectMonitor.Instance smi)
	{
		smi.master.gameObject.GetSMI<ThoughtGraph.Instance>().AddThought(Db.Get().Thoughts.CatchyTune);
		if (!smi.GetSpeechMonitor().IsPlayingSpeech() && SpeechMonitor.IsAllowedToPlaySpeech(smi.gameObject))
		{
			smi.GetSpeechMonitor().PlaySpeech(Db.Get().Thoughts.CatchyTune.speechPrefix, Db.Get().Thoughts.CatchyTune.sound);
		}
	}

	// Token: 0x04002797 RID: 10135
	public StateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.BoolParameter shouldCatchyTune;

	// Token: 0x04002798 RID: 10136
	public StateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.FloatParameter inspirationTimeRemaining;

	// Token: 0x04002799 RID: 10137
	public GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.State idle;

	// Token: 0x0400279A RID: 10138
	public GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.State catchyTune;

	// Token: 0x020015BB RID: 5563
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020015BC RID: 5564
	public new class Instance : GameStateMachine<InspirationEffectMonitor, InspirationEffectMonitor.Instance, IStateMachineTarget, InspirationEffectMonitor.Def>.GameInstance
	{
		// Token: 0x06008524 RID: 34084 RVA: 0x002EBF88 File Offset: 0x002EA188
		public Instance(IStateMachineTarget master, InspirationEffectMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008525 RID: 34085 RVA: 0x002EBF92 File Offset: 0x002EA192
		public SpeechMonitor.Instance GetSpeechMonitor()
		{
			if (this.speechMonitor == null)
			{
				this.speechMonitor = base.master.gameObject.GetSMI<SpeechMonitor.Instance>();
			}
			return this.speechMonitor;
		}

		// Token: 0x040067BE RID: 26558
		public SpeechMonitor.Instance speechMonitor;
	}
}
