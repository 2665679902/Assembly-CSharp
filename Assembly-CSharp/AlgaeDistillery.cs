using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000571 RID: 1393
[SerializationConfig(MemberSerialization.OptIn)]
public class AlgaeDistillery : StateMachineComponent<AlgaeDistillery.StatesInstance>
{
	// Token: 0x060021B1 RID: 8625 RVA: 0x000B7842 File Offset: 0x000B5A42
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x04001367 RID: 4967
	[SerializeField]
	public Tag emitTag;

	// Token: 0x04001368 RID: 4968
	[SerializeField]
	public float emitMass;

	// Token: 0x04001369 RID: 4969
	[SerializeField]
	public Vector3 emitOffset;

	// Token: 0x0400136A RID: 4970
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x0400136B RID: 4971
	[MyCmpGet]
	private ElementConverter emitter;

	// Token: 0x0400136C RID: 4972
	[MyCmpReq]
	private Operational operational;

	// Token: 0x02001197 RID: 4503
	public class StatesInstance : GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.GameInstance
	{
		// Token: 0x06007731 RID: 30513 RVA: 0x002BA80D File Offset: 0x002B8A0D
		public StatesInstance(AlgaeDistillery smi)
			: base(smi)
		{
		}

		// Token: 0x06007732 RID: 30514 RVA: 0x002BA818 File Offset: 0x002B8A18
		public void TryEmit()
		{
			Storage storage = base.smi.master.storage;
			GameObject gameObject = storage.FindFirst(base.smi.master.emitTag);
			if (gameObject != null && gameObject.GetComponent<PrimaryElement>().Mass >= base.master.emitMass)
			{
				storage.Drop(gameObject, true).transform.SetPosition(base.transform.GetPosition() + base.master.emitOffset);
			}
		}
	}

	// Token: 0x02001198 RID: 4504
	public class States : GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery>
	{
		// Token: 0x06007733 RID: 30515 RVA: 0x002BA89C File Offset: 0x002B8A9C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.EventTransition(GameHashes.OperationalChanged, this.disabled, (AlgaeDistillery.StatesInstance smi) => !smi.master.operational.IsOperational);
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.waiting, (AlgaeDistillery.StatesInstance smi) => smi.master.operational.IsOperational);
			this.waiting.Enter("Waiting", delegate(AlgaeDistillery.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).EventTransition(GameHashes.OnStorageChange, this.converting, (AlgaeDistillery.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.converting.Enter("Ready", delegate(AlgaeDistillery.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Transition(this.waiting, (AlgaeDistillery.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().CanConvertAtAll(), UpdateRate.SIM_200ms).EventHandler(GameHashes.OnStorageChange, delegate(AlgaeDistillery.StatesInstance smi)
			{
				smi.TryEmit();
			});
		}

		// Token: 0x04005B52 RID: 23378
		public GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.State disabled;

		// Token: 0x04005B53 RID: 23379
		public GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.State waiting;

		// Token: 0x04005B54 RID: 23380
		public GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.State converting;

		// Token: 0x04005B55 RID: 23381
		public GameStateMachine<AlgaeDistillery.States, AlgaeDistillery.StatesInstance, AlgaeDistillery, object>.State overpressure;
	}
}
