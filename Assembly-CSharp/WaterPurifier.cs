using System;
using KSerialization;

// Token: 0x0200066F RID: 1647
[SerializationConfig(MemberSerialization.OptIn)]
public class WaterPurifier : StateMachineComponent<WaterPurifier.StatesInstance>
{
	// Token: 0x06002C72 RID: 11378 RVA: 0x000E9694 File Offset: 0x000E7894
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.deliveryComponents = base.GetComponents<ManualDeliveryKG>();
		this.OnConduitConnectionChanged(base.GetComponent<ConduitConsumer>().IsConnected);
		base.Subscribe<WaterPurifier>(-2094018600, WaterPurifier.OnConduitConnectionChangedDelegate);
		base.smi.StartSM();
	}

	// Token: 0x06002C73 RID: 11379 RVA: 0x000E96E8 File Offset: 0x000E78E8
	private void OnConduitConnectionChanged(object data)
	{
		bool flag = (bool)data;
		foreach (ManualDeliveryKG manualDeliveryKG in this.deliveryComponents)
		{
			Element element = ElementLoader.GetElement(manualDeliveryKG.RequestedItemTag);
			if (element != null && element.IsLiquid)
			{
				manualDeliveryKG.Pause(flag, "pipe connected");
			}
		}
	}

	// Token: 0x04001A77 RID: 6775
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001A78 RID: 6776
	private ManualDeliveryKG[] deliveryComponents;

	// Token: 0x04001A79 RID: 6777
	private static readonly EventSystem.IntraObjectHandler<WaterPurifier> OnConduitConnectionChangedDelegate = new EventSystem.IntraObjectHandler<WaterPurifier>(delegate(WaterPurifier component, object data)
	{
		component.OnConduitConnectionChanged(data);
	});

	// Token: 0x02001337 RID: 4919
	public class StatesInstance : GameStateMachine<WaterPurifier.States, WaterPurifier.StatesInstance, WaterPurifier, object>.GameInstance
	{
		// Token: 0x06007D06 RID: 32006 RVA: 0x002D2C8C File Offset: 0x002D0E8C
		public StatesInstance(WaterPurifier smi)
			: base(smi)
		{
		}
	}

	// Token: 0x02001338 RID: 4920
	public class States : GameStateMachine<WaterPurifier.States, WaterPurifier.StatesInstance, WaterPurifier>
	{
		// Token: 0x06007D07 RID: 32007 RVA: 0x002D2C98 File Offset: 0x002D0E98
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (WaterPurifier.StatesInstance smi) => smi.master.operational.IsOperational);
			this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (WaterPurifier.StatesInstance smi) => !smi.master.operational.IsOperational).DefaultState(this.on.waiting);
			this.on.waiting.EventTransition(GameHashes.OnStorageChange, this.on.working_pre, (WaterPurifier.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.on.working_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.on.working);
			this.on.working.Enter(delegate(WaterPurifier.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).QueueAnim("working_loop", true, null).EventTransition(GameHashes.OnStorageChange, this.on.working_pst, (WaterPurifier.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().CanConvertAtAll())
				.Exit(delegate(WaterPurifier.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				});
			this.on.working_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.on.waiting);
		}

		// Token: 0x04005FE5 RID: 24549
		public GameStateMachine<WaterPurifier.States, WaterPurifier.StatesInstance, WaterPurifier, object>.State off;

		// Token: 0x04005FE6 RID: 24550
		public WaterPurifier.States.OnStates on;

		// Token: 0x02002020 RID: 8224
		public class OnStates : GameStateMachine<WaterPurifier.States, WaterPurifier.StatesInstance, WaterPurifier, object>.State
		{
			// Token: 0x04008F50 RID: 36688
			public GameStateMachine<WaterPurifier.States, WaterPurifier.StatesInstance, WaterPurifier, object>.State waiting;

			// Token: 0x04008F51 RID: 36689
			public GameStateMachine<WaterPurifier.States, WaterPurifier.StatesInstance, WaterPurifier, object>.State working_pre;

			// Token: 0x04008F52 RID: 36690
			public GameStateMachine<WaterPurifier.States, WaterPurifier.StatesInstance, WaterPurifier, object>.State working;

			// Token: 0x04008F53 RID: 36691
			public GameStateMachine<WaterPurifier.States, WaterPurifier.StatesInstance, WaterPurifier, object>.State working_pst;
		}
	}
}
