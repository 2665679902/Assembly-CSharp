using System;
using UnityEngine;

// Token: 0x02000927 RID: 2343
[SkipSaveFileSerialization]
public class Snorer : StateMachineComponent<Snorer.StatesInstance>
{
	// Token: 0x06004482 RID: 17538 RVA: 0x001827C6 File Offset: 0x001809C6
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x04002DAB RID: 11691
	private static readonly HashedString HeadHash = "snapTo_mouth";

	// Token: 0x02001705 RID: 5893
	public class StatesInstance : GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.GameInstance
	{
		// Token: 0x06008943 RID: 35139 RVA: 0x002F8385 File Offset: 0x002F6585
		public StatesInstance(Snorer master)
			: base(master)
		{
		}

		// Token: 0x06008944 RID: 35140 RVA: 0x002F8390 File Offset: 0x002F6590
		public bool IsSleeping()
		{
			StaminaMonitor.Instance smi = base.master.GetSMI<StaminaMonitor.Instance>();
			return smi != null && smi.IsSleeping();
		}

		// Token: 0x06008945 RID: 35141 RVA: 0x002F83B4 File Offset: 0x002F65B4
		public void StartSmallSnore()
		{
			this.snoreHandle = GameScheduler.Instance.Schedule("snorelines", 2f, new Action<object>(this.StartSmallSnoreInternal), null, null);
		}

		// Token: 0x06008946 RID: 35142 RVA: 0x002F83E0 File Offset: 0x002F65E0
		private void StartSmallSnoreInternal(object data)
		{
			this.snoreHandle.ClearScheduler();
			bool flag;
			Matrix4x4 symbolTransform = base.smi.master.GetComponent<KBatchedAnimController>().GetSymbolTransform(Snorer.HeadHash, out flag);
			if (flag)
			{
				Vector3 vector = symbolTransform.GetColumn(3);
				vector.z = Grid.GetLayerZ(Grid.SceneLayer.FXFront);
				this.snoreEffect = FXHelpers.CreateEffect("snore_fx_kanim", vector, null, false, Grid.SceneLayer.Front, false);
				this.snoreEffect.destroyOnAnimComplete = true;
				this.snoreEffect.Play("snore", KAnim.PlayMode.Loop, 1f, 0f);
			}
		}

		// Token: 0x06008947 RID: 35143 RVA: 0x002F8476 File Offset: 0x002F6676
		public void StopSmallSnore()
		{
			this.snoreHandle.ClearScheduler();
			if (this.snoreEffect != null)
			{
				this.snoreEffect.PlayMode = KAnim.PlayMode.Once;
			}
			this.snoreEffect = null;
		}

		// Token: 0x06008948 RID: 35144 RVA: 0x002F84A4 File Offset: 0x002F66A4
		public void StartSnoreBGEffect()
		{
			AcousticDisturbance.Emit(base.smi.master.gameObject, 3);
		}

		// Token: 0x06008949 RID: 35145 RVA: 0x002F84BC File Offset: 0x002F66BC
		public void StopSnoreBGEffect()
		{
		}

		// Token: 0x04006BC4 RID: 27588
		private SchedulerHandle snoreHandle;

		// Token: 0x04006BC5 RID: 27589
		private KBatchedAnimController snoreEffect;

		// Token: 0x04006BC6 RID: 27590
		private KBatchedAnimController snoreBGEffect;

		// Token: 0x04006BC7 RID: 27591
		private const float BGEmissionRadius = 3f;
	}

	// Token: 0x02001706 RID: 5894
	public class States : GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer>
	{
		// Token: 0x0600894A RID: 35146 RVA: 0x002F84C0 File Offset: 0x002F66C0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.root.TagTransition(GameTags.Dead, null, false);
			this.idle.Transition(this.sleeping, (Snorer.StatesInstance smi) => smi.IsSleeping(), UpdateRate.SIM_200ms);
			this.sleeping.DefaultState(this.sleeping.quiet).Enter(delegate(Snorer.StatesInstance smi)
			{
				smi.StartSmallSnore();
			}).Exit(delegate(Snorer.StatesInstance smi)
			{
				smi.StopSmallSnore();
			})
				.Transition(this.idle, (Snorer.StatesInstance smi) => !smi.master.GetSMI<StaminaMonitor.Instance>().IsSleeping(), UpdateRate.SIM_200ms);
			this.sleeping.quiet.Enter("ScheduleNextSnore", delegate(Snorer.StatesInstance smi)
			{
				smi.ScheduleGoTo(this.GetNewInterval(), this.sleeping.snoring);
			});
			this.sleeping.snoring.Enter(delegate(Snorer.StatesInstance smi)
			{
				smi.StartSnoreBGEffect();
			}).ToggleExpression(Db.Get().Expressions.Relief, null).ScheduleGoTo(3f, this.sleeping.quiet)
				.Exit(delegate(Snorer.StatesInstance smi)
				{
					smi.StopSnoreBGEffect();
				});
		}

		// Token: 0x0600894B RID: 35147 RVA: 0x002F8644 File Offset: 0x002F6844
		private float GetNewInterval()
		{
			return Mathf.Min(Mathf.Max(Util.GaussianRandom(5f, 1f), 3f), 10f);
		}

		// Token: 0x04006BC8 RID: 27592
		public GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.State idle;

		// Token: 0x04006BC9 RID: 27593
		public Snorer.States.SleepStates sleeping;

		// Token: 0x020020B3 RID: 8371
		public class SleepStates : GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.State
		{
			// Token: 0x040091AC RID: 37292
			public GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.State quiet;

			// Token: 0x040091AD RID: 37293
			public GameStateMachine<Snorer.States, Snorer.StatesInstance, Snorer, object>.State snoring;
		}
	}
}
