using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007B6 RID: 1974
[SerializationConfig(MemberSerialization.OptIn)]
public class IceMachine : StateMachineComponent<IceMachine.StatesInstance>
{
	// Token: 0x060037D3 RID: 14291 RVA: 0x00135A74 File Offset: 0x00133C74
	public void SetStorages(Storage waterStorage, Storage iceStorage)
	{
		this.waterStorage = waterStorage;
		this.iceStorage = iceStorage;
	}

	// Token: 0x060037D4 RID: 14292 RVA: 0x00135A84 File Offset: 0x00133C84
	private bool CanMakeIce()
	{
		bool flag = this.waterStorage != null && this.waterStorage.GetMassAvailable(SimHashes.Water) >= 0.1f;
		bool flag2 = this.iceStorage != null && this.iceStorage.IsFull();
		return flag && !flag2;
	}

	// Token: 0x060037D5 RID: 14293 RVA: 0x00135AE4 File Offset: 0x00133CE4
	private void MakeIce(IceMachine.StatesInstance smi, float dt)
	{
		float num = this.heatRemovalRate * dt / (float)this.waterStorage.items.Count;
		foreach (GameObject gameObject in this.waterStorage.items)
		{
			GameUtil.DeltaThermalEnergy(gameObject.GetComponent<PrimaryElement>(), -num, smi.master.targetTemperature);
		}
		for (int i = this.waterStorage.items.Count; i > 0; i--)
		{
			GameObject gameObject2 = this.waterStorage.items[i - 1];
			if (gameObject2 && gameObject2.GetComponent<PrimaryElement>().Temperature < gameObject2.GetComponent<PrimaryElement>().Element.lowTemp)
			{
				PrimaryElement component = gameObject2.GetComponent<PrimaryElement>();
				this.waterStorage.AddOre(component.Element.lowTempTransitionTarget, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount, false, true);
				this.waterStorage.ConsumeIgnoringDisease(gameObject2);
			}
		}
		smi.UpdateIceState();
	}

	// Token: 0x060037D6 RID: 14294 RVA: 0x00135C10 File Offset: 0x00133E10
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x04002562 RID: 9570
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002563 RID: 9571
	public Storage waterStorage;

	// Token: 0x04002564 RID: 9572
	public Storage iceStorage;

	// Token: 0x04002565 RID: 9573
	public float targetTemperature;

	// Token: 0x04002566 RID: 9574
	public float heatRemovalRate;

	// Token: 0x04002567 RID: 9575
	private static StatusItem iceStorageFullStatusItem;

	// Token: 0x02001518 RID: 5400
	public class StatesInstance : GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.GameInstance
	{
		// Token: 0x060082AC RID: 33452 RVA: 0x002E5CAC File Offset: 0x002E3EAC
		public StatesInstance(IceMachine smi)
			: base(smi)
		{
			this.meter = new MeterController(base.gameObject.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_OL", "meter_frame", "meter_fill" });
			this.UpdateMeter();
			base.Subscribe(-1697596308, new Action<object>(this.OnStorageChange));
		}

		// Token: 0x060082AD RID: 33453 RVA: 0x002E5D1E File Offset: 0x002E3F1E
		private void OnStorageChange(object data)
		{
			this.UpdateMeter();
		}

		// Token: 0x060082AE RID: 33454 RVA: 0x002E5D26 File Offset: 0x002E3F26
		public void UpdateMeter()
		{
			this.meter.SetPositionPercent(Mathf.Clamp01(base.smi.master.iceStorage.MassStored() / base.smi.master.iceStorage.Capacity()));
		}

		// Token: 0x060082AF RID: 33455 RVA: 0x002E5D64 File Offset: 0x002E3F64
		public void UpdateIceState()
		{
			bool flag = false;
			for (int i = base.smi.master.waterStorage.items.Count; i > 0; i--)
			{
				GameObject gameObject = base.smi.master.waterStorage.items[i - 1];
				if (gameObject && gameObject.GetComponent<PrimaryElement>().Temperature <= base.smi.master.targetTemperature)
				{
					flag = true;
				}
			}
			base.sm.doneFreezingIce.Set(flag, this, false);
		}

		// Token: 0x04006592 RID: 26002
		private MeterController meter;

		// Token: 0x04006593 RID: 26003
		public Chore emptyChore;
	}

	// Token: 0x02001519 RID: 5401
	public class States : GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine>
	{
		// Token: 0x060082B0 RID: 33456 RVA: 0x002E5DF4 File Offset: 0x002E3FF4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (IceMachine.StatesInstance smi) => smi.master.operational.IsOperational);
			this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (IceMachine.StatesInstance smi) => !smi.master.operational.IsOperational).DefaultState(this.on.waiting);
			this.on.waiting.EventTransition(GameHashes.OnStorageChange, this.on.working_pre, (IceMachine.StatesInstance smi) => smi.master.CanMakeIce());
			this.on.working_pre.Enter(delegate(IceMachine.StatesInstance smi)
			{
				smi.UpdateIceState();
			}).PlayAnim("working_pre").OnAnimQueueComplete(this.on.working);
			this.on.working.QueueAnim("working_loop", true, null).Update("UpdateWorking", delegate(IceMachine.StatesInstance smi, float dt)
			{
				smi.master.MakeIce(smi, dt);
			}, UpdateRate.SIM_200ms, false).ParamTransition<bool>(this.doneFreezingIce, this.on.working_pst, GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.IsTrue)
				.Enter(delegate(IceMachine.StatesInstance smi)
				{
					smi.master.operational.SetActive(true, false);
					smi.master.gameObject.GetComponent<ManualDeliveryKG>().Pause(true, "Working");
				})
				.Exit(delegate(IceMachine.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
					smi.master.gameObject.GetComponent<ManualDeliveryKG>().Pause(false, "Done Working");
				});
			this.on.working_pst.Exit(new StateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State.Callback(this.DoTransfer)).PlayAnim("working_pst").OnAnimQueueComplete(this.on);
		}

		// Token: 0x060082B1 RID: 33457 RVA: 0x002E6004 File Offset: 0x002E4204
		private void DoTransfer(IceMachine.StatesInstance smi)
		{
			for (int i = smi.master.waterStorage.items.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = smi.master.waterStorage.items[i];
				if (gameObject && gameObject.GetComponent<PrimaryElement>().Temperature <= smi.master.targetTemperature)
				{
					smi.master.waterStorage.Transfer(gameObject, smi.master.iceStorage, false, true);
				}
			}
			smi.UpdateMeter();
		}

		// Token: 0x04006594 RID: 26004
		public StateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.BoolParameter doneFreezingIce;

		// Token: 0x04006595 RID: 26005
		public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State off;

		// Token: 0x04006596 RID: 26006
		public IceMachine.States.OnStates on;

		// Token: 0x0200206E RID: 8302
		public class OnStates : GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State
		{
			// Token: 0x04009093 RID: 37011
			public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State waiting;

			// Token: 0x04009094 RID: 37012
			public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State working_pre;

			// Token: 0x04009095 RID: 37013
			public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State working;

			// Token: 0x04009096 RID: 37014
			public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State working_pst;
		}
	}
}
