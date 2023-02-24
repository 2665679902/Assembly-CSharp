using System;
using UnityEngine;

// Token: 0x02000791 RID: 1937
public class ElementSpout : StateMachineComponent<ElementSpout.StatesInstance>
{
	// Token: 0x06003631 RID: 13873 RVA: 0x0012C5E0 File Offset: 0x0012A7E0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = Grid.PosToCell(base.transform.GetPosition());
		Grid.Objects[num, 2] = base.gameObject;
		base.smi.StartSM();
	}

	// Token: 0x06003632 RID: 13874 RVA: 0x0012C621 File Offset: 0x0012A821
	public void SetEmitter(ElementEmitter emitter)
	{
		this.emitter = emitter;
	}

	// Token: 0x06003633 RID: 13875 RVA: 0x0012C62A File Offset: 0x0012A82A
	public void ConfigureEmissionSettings(float emissionPollFrequency = 3f, float emissionIrregularity = 1.5f, float maxPressure = 1.5f, float perEmitAmount = 0.5f)
	{
		this.maxPressure = maxPressure;
		this.emissionPollFrequency = emissionPollFrequency;
		this.emissionIrregularity = emissionIrregularity;
		this.perEmitAmount = perEmitAmount;
	}

	// Token: 0x04002429 RID: 9257
	[SerializeField]
	private ElementEmitter emitter;

	// Token: 0x0400242A RID: 9258
	[MyCmpAdd]
	private KBatchedAnimController anim;

	// Token: 0x0400242B RID: 9259
	public float maxPressure = 1.5f;

	// Token: 0x0400242C RID: 9260
	public float emissionPollFrequency = 3f;

	// Token: 0x0400242D RID: 9261
	public float emissionIrregularity = 1.5f;

	// Token: 0x0400242E RID: 9262
	public float perEmitAmount = 0.5f;

	// Token: 0x020014C3 RID: 5315
	public class StatesInstance : GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.GameInstance
	{
		// Token: 0x060081E3 RID: 33251 RVA: 0x002E34C8 File Offset: 0x002E16C8
		public StatesInstance(ElementSpout smi)
			: base(smi)
		{
		}

		// Token: 0x060081E4 RID: 33252 RVA: 0x002E34D1 File Offset: 0x002E16D1
		private bool CanEmitOnCell(int cell, float max_pressure, Element.State expected_state)
		{
			return Grid.Mass[cell] < max_pressure && (Grid.Element[cell].IsState(expected_state) || Grid.Element[cell].IsVacuum);
		}

		// Token: 0x060081E5 RID: 33253 RVA: 0x002E3500 File Offset: 0x002E1700
		public bool CanEmitAnywhere()
		{
			int num = Grid.PosToCell(base.smi.transform.GetPosition());
			int num2 = Grid.CellLeft(num);
			int num3 = Grid.CellRight(num);
			int num4 = Grid.CellAbove(num);
			Element.State state = ElementLoader.FindElementByHash(base.smi.master.emitter.outputElement.elementHash).state;
			return false || this.CanEmitOnCell(num, base.smi.master.maxPressure, state) || this.CanEmitOnCell(num2, base.smi.master.maxPressure, state) || this.CanEmitOnCell(num3, base.smi.master.maxPressure, state) || this.CanEmitOnCell(num4, base.smi.master.maxPressure, state);
		}
	}

	// Token: 0x020014C4 RID: 5316
	public class States : GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout>
	{
		// Token: 0x060081E6 RID: 33254 RVA: 0x002E35D8 File Offset: 0x002E17D8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.DefaultState(this.idle.unblocked).Enter(delegate(ElementSpout.StatesInstance smi)
			{
				smi.Play("idle", KAnim.PlayMode.Once);
			}).ScheduleGoTo((ElementSpout.StatesInstance smi) => smi.master.emissionPollFrequency, this.emit);
			this.idle.unblocked.ToggleStatusItem(Db.Get().MiscStatusItems.SpoutPressureBuilding, null).Transition(this.idle.blocked, (ElementSpout.StatesInstance smi) => !smi.CanEmitAnywhere(), UpdateRate.SIM_200ms);
			this.idle.blocked.ToggleStatusItem(Db.Get().MiscStatusItems.SpoutOverPressure, null).Transition(this.idle.blocked, (ElementSpout.StatesInstance smi) => smi.CanEmitAnywhere(), UpdateRate.SIM_200ms);
			this.emit.DefaultState(this.emit.unblocked).Enter(delegate(ElementSpout.StatesInstance smi)
			{
				float num = 1f + UnityEngine.Random.Range(0f, smi.master.emissionIrregularity);
				float num2 = smi.master.perEmitAmount / num;
				smi.master.emitter.SetEmitting(true);
				smi.master.emitter.emissionFrequency = 1f;
				smi.master.emitter.outputElement.massGenerationRate = num2;
				smi.ScheduleGoTo(num, this.idle);
			});
			this.emit.unblocked.ToggleStatusItem(Db.Get().MiscStatusItems.SpoutEmitting, null).Enter(delegate(ElementSpout.StatesInstance smi)
			{
				smi.Play("emit", KAnim.PlayMode.Once);
				smi.master.emitter.SetEmitting(true);
			}).Transition(this.emit.blocked, (ElementSpout.StatesInstance smi) => !smi.CanEmitAnywhere(), UpdateRate.SIM_200ms);
			this.emit.blocked.ToggleStatusItem(Db.Get().MiscStatusItems.SpoutOverPressure, null).Enter(delegate(ElementSpout.StatesInstance smi)
			{
				smi.Play("idle", KAnim.PlayMode.Once);
				smi.master.emitter.SetEmitting(false);
			}).Transition(this.emit.unblocked, (ElementSpout.StatesInstance smi) => smi.CanEmitAnywhere(), UpdateRate.SIM_200ms);
		}

		// Token: 0x040064AF RID: 25775
		public ElementSpout.States.Idle idle;

		// Token: 0x040064B0 RID: 25776
		public ElementSpout.States.Emitting emit;

		// Token: 0x0200205C RID: 8284
		public class Idle : GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State
		{
			// Token: 0x0400902D RID: 36909
			public GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State unblocked;

			// Token: 0x0400902E RID: 36910
			public GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State blocked;
		}

		// Token: 0x0200205D RID: 8285
		public class Emitting : GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State
		{
			// Token: 0x0400902F RID: 36911
			public GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State unblocked;

			// Token: 0x04009030 RID: 36912
			public GameStateMachine<ElementSpout.States, ElementSpout.StatesInstance, ElementSpout, object>.State blocked;
		}
	}
}
