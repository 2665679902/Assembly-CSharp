using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200061E RID: 1566
[SerializationConfig(MemberSerialization.OptIn)]
public class OxyliteRefinery : StateMachineComponent<OxyliteRefinery.StatesInstance>
{
	// Token: 0x0600290D RID: 10509 RVA: 0x000D90A5 File Offset: 0x000D72A5
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x04001823 RID: 6179
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x04001824 RID: 6180
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001825 RID: 6181
	public Tag emitTag;

	// Token: 0x04001826 RID: 6182
	public float emitMass;

	// Token: 0x04001827 RID: 6183
	public Vector3 dropOffset;

	// Token: 0x0200129A RID: 4762
	public class StatesInstance : GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery, object>.GameInstance
	{
		// Token: 0x06007AE0 RID: 31456 RVA: 0x002C9710 File Offset: 0x002C7910
		public StatesInstance(OxyliteRefinery smi)
			: base(smi)
		{
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x002C971C File Offset: 0x002C791C
		public void TryEmit()
		{
			Storage storage = base.smi.master.storage;
			GameObject gameObject = storage.FindFirst(base.smi.master.emitTag);
			if (gameObject != null && gameObject.GetComponent<PrimaryElement>().Mass >= base.master.emitMass)
			{
				Vector3 vector = base.transform.GetPosition() + base.master.dropOffset;
				vector.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
				gameObject.transform.SetPosition(vector);
				storage.Drop(gameObject, true);
			}
		}
	}

	// Token: 0x0200129B RID: 4763
	public class States : GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery>
	{
		// Token: 0x06007AE2 RID: 31458 RVA: 0x002C97B4 File Offset: 0x002C79B4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.EventTransition(GameHashes.OperationalChanged, this.disabled, (OxyliteRefinery.StatesInstance smi) => !smi.master.operational.IsOperational);
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.waiting, (OxyliteRefinery.StatesInstance smi) => smi.master.operational.IsOperational);
			this.waiting.EventTransition(GameHashes.OnStorageChange, this.converting, (OxyliteRefinery.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.converting.Enter(delegate(OxyliteRefinery.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit(delegate(OxyliteRefinery.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).Transition(this.waiting, (OxyliteRefinery.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().CanConvertAtAll(), UpdateRate.SIM_200ms)
				.EventHandler(GameHashes.OnStorageChange, delegate(OxyliteRefinery.StatesInstance smi)
				{
					smi.TryEmit();
				});
		}

		// Token: 0x04005E37 RID: 24119
		public GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery, object>.State disabled;

		// Token: 0x04005E38 RID: 24120
		public GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery, object>.State waiting;

		// Token: 0x04005E39 RID: 24121
		public GameStateMachine<OxyliteRefinery.States, OxyliteRefinery.StatesInstance, OxyliteRefinery, object>.State converting;
	}
}
