using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000820 RID: 2080
public class CoughMonitor : GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>
{
	// Token: 0x06003C59 RID: 15449 RVA: 0x001503BC File Offset: 0x0014E5BC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.idle.EventHandler(GameHashes.PoorAirQuality, new GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.GameEvent.Callback(this.OnBreatheDirtyAir)).ParamTransition<bool>(this.shouldCough, this.coughing, (CoughMonitor.Instance smi, bool bShouldCough) => bShouldCough);
		this.coughing.ToggleStatusItem(Db.Get().DuplicantStatusItems.Coughing, null).ToggleReactable((CoughMonitor.Instance smi) => smi.GetReactable()).ParamTransition<bool>(this.shouldCough, this.idle, (CoughMonitor.Instance smi, bool bShouldCough) => !bShouldCough);
	}

	// Token: 0x06003C5A RID: 15450 RVA: 0x00150498 File Offset: 0x0014E698
	private void OnBreatheDirtyAir(CoughMonitor.Instance smi, object data)
	{
		float timeInCycles = GameClock.Instance.GetTimeInCycles();
		if (timeInCycles > 0.1f && timeInCycles - smi.lastCoughTime <= 0.1f)
		{
			return;
		}
		Sim.MassConsumedCallback massConsumedCallback = (Sim.MassConsumedCallback)data;
		float num = ((smi.lastConsumeTime <= 0f) ? 0f : (timeInCycles - smi.lastConsumeTime));
		smi.lastConsumeTime = timeInCycles;
		smi.amountConsumed -= 0.05f * num;
		smi.amountConsumed = Mathf.Max(smi.amountConsumed, 0f);
		smi.amountConsumed += massConsumedCallback.mass;
		if (smi.amountConsumed >= 1f)
		{
			this.shouldCough.Set(true, smi, false);
			smi.lastConsumeTime = 0f;
			smi.amountConsumed = 0f;
		}
	}

	// Token: 0x04002749 RID: 10057
	private const float amountToCough = 1f;

	// Token: 0x0400274A RID: 10058
	private const float decayRate = 0.05f;

	// Token: 0x0400274B RID: 10059
	private const float coughInterval = 0.1f;

	// Token: 0x0400274C RID: 10060
	public GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.State idle;

	// Token: 0x0400274D RID: 10061
	public GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.State coughing;

	// Token: 0x0400274E RID: 10062
	public StateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.BoolParameter shouldCough = new StateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.BoolParameter(false);

	// Token: 0x02001589 RID: 5513
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200158A RID: 5514
	public new class Instance : GameStateMachine<CoughMonitor, CoughMonitor.Instance, IStateMachineTarget, CoughMonitor.Def>.GameInstance
	{
		// Token: 0x06008443 RID: 33859 RVA: 0x002E98EA File Offset: 0x002E7AEA
		public Instance(IStateMachineTarget master, CoughMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008444 RID: 33860 RVA: 0x002E98F4 File Offset: 0x002E7AF4
		public Reactable GetReactable()
		{
			Emote cough_Small = Db.Get().Emotes.Minion.Cough_Small;
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, "BadAirCough", Db.Get().ChoreTypes.Cough, 0f, 0f, float.PositiveInfinity, 0f);
			selfEmoteReactable.SetEmote(cough_Small);
			selfEmoteReactable.preventChoreInterruption = true;
			return selfEmoteReactable.RegisterEmoteStepCallbacks("react_small", null, new Action<GameObject>(this.FinishedCoughing));
		}

		// Token: 0x06008445 RID: 33861 RVA: 0x002E9980 File Offset: 0x002E7B80
		private void FinishedCoughing(GameObject cougher)
		{
			cougher.GetComponent<Effects>().Add("ContaminatedLungs", true);
			base.sm.shouldCough.Set(false, base.smi, false);
			base.smi.lastCoughTime = GameClock.Instance.GetTimeInCycles();
		}

		// Token: 0x04006708 RID: 26376
		[Serialize]
		public float lastCoughTime;

		// Token: 0x04006709 RID: 26377
		[Serialize]
		public float lastConsumeTime;

		// Token: 0x0400670A RID: 26378
		[Serialize]
		public float amountConsumed;
	}
}
