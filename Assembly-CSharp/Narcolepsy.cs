using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200085C RID: 2140
[SkipSaveFileSerialization]
public class Narcolepsy : StateMachineComponent<Narcolepsy.StatesInstance>
{
	// Token: 0x06003D8E RID: 15758 RVA: 0x0015875C File Offset: 0x0015695C
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06003D8F RID: 15759 RVA: 0x00158769 File Offset: 0x00156969
	public bool IsNarcolepsing()
	{
		return base.smi.IsNarcolepsing();
	}

	// Token: 0x04002869 RID: 10345
	public static readonly Chore.Precondition IsNarcolepsingPrecondition = new Chore.Precondition
	{
		id = "IsNarcolepsingPrecondition",
		description = DUPLICANTS.CHORES.PRECONDITIONS.IS_NARCOLEPSING,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Narcolepsy component = context.consumerState.consumer.GetComponent<Narcolepsy>();
			return component != null && component.IsNarcolepsing();
		}
	};

	// Token: 0x0200161B RID: 5659
	public class StatesInstance : GameStateMachine<Narcolepsy.States, Narcolepsy.StatesInstance, Narcolepsy, object>.GameInstance
	{
		// Token: 0x060086C2 RID: 34498 RVA: 0x002EFE30 File Offset: 0x002EE030
		public StatesInstance(Narcolepsy master)
			: base(master)
		{
		}

		// Token: 0x060086C3 RID: 34499 RVA: 0x002EFE3C File Offset: 0x002EE03C
		public bool IsSleeping()
		{
			StaminaMonitor.Instance smi = base.master.GetSMI<StaminaMonitor.Instance>();
			return smi != null && smi.IsSleeping();
		}

		// Token: 0x060086C4 RID: 34500 RVA: 0x002EFE60 File Offset: 0x002EE060
		public bool IsNarcolepsing()
		{
			return this.GetCurrentState() == base.sm.sleepy;
		}

		// Token: 0x060086C5 RID: 34501 RVA: 0x002EFE75 File Offset: 0x002EE075
		public GameObject CreateFloorLocator()
		{
			Sleepable safeFloorLocator = SleepChore.GetSafeFloorLocator(base.master.gameObject);
			safeFloorLocator.effectName = "NarcolepticSleep";
			safeFloorLocator.stretchOnWake = false;
			return safeFloorLocator.gameObject;
		}
	}

	// Token: 0x0200161C RID: 5660
	public class States : GameStateMachine<Narcolepsy.States, Narcolepsy.StatesInstance, Narcolepsy>
	{
		// Token: 0x060086C6 RID: 34502 RVA: 0x002EFEA0 File Offset: 0x002EE0A0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.TagTransition(GameTags.Dead, null, false);
			this.idle.Enter("ScheduleNextSleep", delegate(Narcolepsy.StatesInstance smi)
			{
				smi.ScheduleGoTo(this.GetNewInterval(TRAITS.NARCOLEPSY_INTERVAL_MIN, TRAITS.NARCOLEPSY_INTERVAL_MAX), this.sleepy);
			});
			this.sleepy.Enter("Is Already Sleeping Check", delegate(Narcolepsy.StatesInstance smi)
			{
				if (smi.master.GetSMI<StaminaMonitor.Instance>().IsSleeping())
				{
					smi.GoTo(this.idle);
					return;
				}
				smi.ScheduleGoTo(this.GetNewInterval(TRAITS.NARCOLEPSY_SLEEPDURATION_MIN, TRAITS.NARCOLEPSY_SLEEPDURATION_MAX), this.idle);
			}).ToggleUrge(Db.Get().Urges.Narcolepsy).ToggleChore(new Func<Narcolepsy.StatesInstance, Chore>(this.CreateNarcolepsyChore), this.idle);
		}

		// Token: 0x060086C7 RID: 34503 RVA: 0x002EFF30 File Offset: 0x002EE130
		private Chore CreateNarcolepsyChore(Narcolepsy.StatesInstance smi)
		{
			GameObject gameObject = smi.CreateFloorLocator();
			SleepChore sleepChore = new SleepChore(Db.Get().ChoreTypes.Narcolepsy, smi.master, gameObject, true, false);
			sleepChore.AddPrecondition(Narcolepsy.IsNarcolepsingPrecondition, null);
			return sleepChore;
		}

		// Token: 0x060086C8 RID: 34504 RVA: 0x002EFF6D File Offset: 0x002EE16D
		private float GetNewInterval(float min, float max)
		{
			Mathf.Min(Mathf.Max(Util.GaussianRandom(max - min, 1f), min), max);
			return UnityEngine.Random.Range(min, max);
		}

		// Token: 0x04006917 RID: 26903
		public GameStateMachine<Narcolepsy.States, Narcolepsy.StatesInstance, Narcolepsy, object>.State idle;

		// Token: 0x04006918 RID: 26904
		public GameStateMachine<Narcolepsy.States, Narcolepsy.StatesInstance, Narcolepsy, object>.State sleepy;
	}
}
