using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005B2 RID: 1458
[SerializationConfig(MemberSerialization.OptIn)]
public class Electrolyzer : StateMachineComponent<Electrolyzer.StatesInstance>
{
	// Token: 0x0600241A RID: 9242 RVA: 0x000C34CC File Offset: 0x000C16CC
	protected override void OnSpawn()
	{
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (this.hasMeter)
		{
			this.meter = new MeterController(component, "U2H_meter_target", "meter", Meter.Offset.Behind, Grid.SceneLayer.NoLayer, new Vector3(-0.4f, 0.5f, -0.1f), new string[] { "U2H_meter_target", "U2H_meter_tank", "U2H_meter_waterbody", "U2H_meter_level" });
		}
		base.smi.StartSM();
		this.UpdateMeter();
		Tutorial.Instance.oxygenGenerators.Add(base.gameObject);
	}

	// Token: 0x0600241B RID: 9243 RVA: 0x000C3561 File Offset: 0x000C1761
	protected override void OnCleanUp()
	{
		Tutorial.Instance.oxygenGenerators.Remove(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x0600241C RID: 9244 RVA: 0x000C3580 File Offset: 0x000C1780
	public void UpdateMeter()
	{
		if (this.hasMeter)
		{
			float num = Mathf.Clamp01(this.storage.MassStored() / this.storage.capacityKg);
			this.meter.SetPositionPercent(num);
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x0600241D RID: 9245 RVA: 0x000C35C0 File Offset: 0x000C17C0
	private bool RoomForPressure
	{
		get
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			num = Grid.OffsetCell(num, this.emissionOffset);
			return !GameUtil.FloodFillCheck<Electrolyzer>(new Func<int, Electrolyzer, bool>(Electrolyzer.OverPressure), this, num, 3, true, true);
		}
	}

	// Token: 0x0600241E RID: 9246 RVA: 0x000C3604 File Offset: 0x000C1804
	private static bool OverPressure(int cell, Electrolyzer electrolyzer)
	{
		return Grid.Mass[cell] > electrolyzer.maxMass;
	}

	// Token: 0x040014BC RID: 5308
	[SerializeField]
	public float maxMass = 2.5f;

	// Token: 0x040014BD RID: 5309
	[SerializeField]
	public bool hasMeter = true;

	// Token: 0x040014BE RID: 5310
	[SerializeField]
	public CellOffset emissionOffset = CellOffset.none;

	// Token: 0x040014BF RID: 5311
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x040014C0 RID: 5312
	[MyCmpGet]
	private ElementConverter emitter;

	// Token: 0x040014C1 RID: 5313
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040014C2 RID: 5314
	private MeterController meter;

	// Token: 0x020011EB RID: 4587
	public class StatesInstance : GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.GameInstance
	{
		// Token: 0x0600786C RID: 30828 RVA: 0x002BEA9E File Offset: 0x002BCC9E
		public StatesInstance(Electrolyzer smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020011EC RID: 4588
	public class States : GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer>
	{
		// Token: 0x0600786D RID: 30829 RVA: 0x002BEAA8 File Offset: 0x002BCCA8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.EventTransition(GameHashes.OperationalChanged, this.disabled, (Electrolyzer.StatesInstance smi) => !smi.master.operational.IsOperational).EventHandler(GameHashes.OnStorageChange, delegate(Electrolyzer.StatesInstance smi)
			{
				smi.master.UpdateMeter();
			});
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.waiting, (Electrolyzer.StatesInstance smi) => smi.master.operational.IsOperational);
			this.waiting.Enter("Waiting", delegate(Electrolyzer.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).EventTransition(GameHashes.OnStorageChange, this.converting, (Electrolyzer.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.converting.Enter("Ready", delegate(Electrolyzer.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Transition(this.waiting, (Electrolyzer.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().CanConvertAtAll(), UpdateRate.SIM_200ms).Transition(this.overpressure, (Electrolyzer.StatesInstance smi) => !smi.master.RoomForPressure, UpdateRate.SIM_200ms);
			this.overpressure.Enter("OverPressure", delegate(Electrolyzer.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).ToggleStatusItem(Db.Get().BuildingStatusItems.PressureOk, null).Transition(this.converting, (Electrolyzer.StatesInstance smi) => smi.master.RoomForPressure, UpdateRate.SIM_200ms);
		}

		// Token: 0x04005C72 RID: 23666
		public GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.State disabled;

		// Token: 0x04005C73 RID: 23667
		public GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.State waiting;

		// Token: 0x04005C74 RID: 23668
		public GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.State converting;

		// Token: 0x04005C75 RID: 23669
		public GameStateMachine<Electrolyzer.States, Electrolyzer.StatesInstance, Electrolyzer, object>.State overpressure;
	}
}
