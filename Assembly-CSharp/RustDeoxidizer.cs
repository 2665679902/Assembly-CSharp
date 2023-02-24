using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200063C RID: 1596
[SerializationConfig(MemberSerialization.OptIn)]
public class RustDeoxidizer : StateMachineComponent<RustDeoxidizer.StatesInstance>
{
	// Token: 0x06002A3E RID: 10814 RVA: 0x000DF3ED File Offset: 0x000DD5ED
	protected override void OnSpawn()
	{
		base.smi.StartSM();
		Tutorial.Instance.oxygenGenerators.Add(base.gameObject);
	}

	// Token: 0x06002A3F RID: 10815 RVA: 0x000DF40F File Offset: 0x000DD60F
	protected override void OnCleanUp()
	{
		Tutorial.Instance.oxygenGenerators.Remove(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000DF430 File Offset: 0x000DD630
	private bool RoomForPressure
	{
		get
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			num = Grid.CellAbove(num);
			return !GameUtil.FloodFillCheck<RustDeoxidizer>(new Func<int, RustDeoxidizer, bool>(RustDeoxidizer.OverPressure), this, num, 3, true, true);
		}
	}

	// Token: 0x06002A41 RID: 10817 RVA: 0x000DF46E File Offset: 0x000DD66E
	private static bool OverPressure(int cell, RustDeoxidizer rustDeoxidizer)
	{
		return Grid.Mass[cell] > rustDeoxidizer.maxMass;
	}

	// Token: 0x040018F8 RID: 6392
	[SerializeField]
	public float maxMass = 2.5f;

	// Token: 0x040018F9 RID: 6393
	[MyCmpAdd]
	private Storage storage;

	// Token: 0x040018FA RID: 6394
	[MyCmpGet]
	private ElementConverter emitter;

	// Token: 0x040018FB RID: 6395
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040018FC RID: 6396
	private MeterController meter;

	// Token: 0x020012D3 RID: 4819
	public class StatesInstance : GameStateMachine<RustDeoxidizer.States, RustDeoxidizer.StatesInstance, RustDeoxidizer, object>.GameInstance
	{
		// Token: 0x06007BBB RID: 31675 RVA: 0x002CCCAA File Offset: 0x002CAEAA
		public StatesInstance(RustDeoxidizer smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020012D4 RID: 4820
	public class States : GameStateMachine<RustDeoxidizer.States, RustDeoxidizer.StatesInstance, RustDeoxidizer>
	{
		// Token: 0x06007BBC RID: 31676 RVA: 0x002CCCB4 File Offset: 0x002CAEB4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.root.EventTransition(GameHashes.OperationalChanged, this.disabled, (RustDeoxidizer.StatesInstance smi) => !smi.master.operational.IsOperational);
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.waiting, (RustDeoxidizer.StatesInstance smi) => smi.master.operational.IsOperational);
			this.waiting.Enter("Waiting", delegate(RustDeoxidizer.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).EventTransition(GameHashes.OnStorageChange, this.converting, (RustDeoxidizer.StatesInstance smi) => smi.master.GetComponent<ElementConverter>().HasEnoughMassToStartConverting(false));
			this.converting.Enter("Ready", delegate(RustDeoxidizer.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Transition(this.waiting, (RustDeoxidizer.StatesInstance smi) => !smi.master.GetComponent<ElementConverter>().CanConvertAtAll(), UpdateRate.SIM_200ms).Transition(this.overpressure, (RustDeoxidizer.StatesInstance smi) => !smi.master.RoomForPressure, UpdateRate.SIM_200ms);
			this.overpressure.Enter("OverPressure", delegate(RustDeoxidizer.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			}).ToggleStatusItem(Db.Get().BuildingStatusItems.PressureOk, null).Transition(this.converting, (RustDeoxidizer.StatesInstance smi) => smi.master.RoomForPressure, UpdateRate.SIM_200ms);
		}

		// Token: 0x04005ED2 RID: 24274
		public GameStateMachine<RustDeoxidizer.States, RustDeoxidizer.StatesInstance, RustDeoxidizer, object>.State disabled;

		// Token: 0x04005ED3 RID: 24275
		public GameStateMachine<RustDeoxidizer.States, RustDeoxidizer.StatesInstance, RustDeoxidizer, object>.State waiting;

		// Token: 0x04005ED4 RID: 24276
		public GameStateMachine<RustDeoxidizer.States, RustDeoxidizer.StatesInstance, RustDeoxidizer, object>.State converting;

		// Token: 0x04005ED5 RID: 24277
		public GameStateMachine<RustDeoxidizer.States, RustDeoxidizer.StatesInstance, RustDeoxidizer, object>.State overpressure;
	}
}
