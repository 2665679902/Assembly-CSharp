using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000640 RID: 1600
[SerializationConfig(MemberSerialization.OptIn)]
public class SolarPanel : Generator
{
	// Token: 0x06002A73 RID: 10867 RVA: 0x000E0234 File Offset: 0x000DE434
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SolarPanel>(824508782, SolarPanel.OnActiveChangedDelegate);
		this.smi = new SolarPanel.StatesInstance(this);
		this.smi.StartSM();
		this.accumulator = Game.Instance.accumulators.Add("Element", this);
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
	}

	// Token: 0x06002A74 RID: 10868 RVA: 0x000E02CE File Offset: 0x000DE4CE
	protected override void OnCleanUp()
	{
		this.smi.StopSM("cleanup");
		Game.Instance.accumulators.Remove(this.accumulator);
		base.OnCleanUp();
	}

	// Token: 0x06002A75 RID: 10869 RVA: 0x000E02FC File Offset: 0x000DE4FC
	protected void OnActiveChanged(object data)
	{
		StatusItem statusItem = (((Operational)data).IsActive ? Db.Get().BuildingStatusItems.Wattage : Db.Get().BuildingStatusItems.GeneratorOffline);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, this);
	}

	// Token: 0x06002A76 RID: 10870 RVA: 0x000E0354 File Offset: 0x000DE554
	private void UpdateStatusItem()
	{
		this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.Wattage, false);
		if (this.statusHandle == Guid.Empty)
		{
			this.statusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.SolarPanelWattage, this);
			return;
		}
		if (this.statusHandle != Guid.Empty)
		{
			base.GetComponent<KSelectable>().ReplaceStatusItem(this.statusHandle, Db.Get().BuildingStatusItems.SolarPanelWattage, this);
		}
	}

	// Token: 0x06002A77 RID: 10871 RVA: 0x000E03E8 File Offset: 0x000DE5E8
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		ushort circuitID = base.CircuitID;
		this.operational.SetFlag(Generator.wireConnectedFlag, circuitID != ushort.MaxValue);
		if (!this.operational.IsOperational)
		{
			return;
		}
		float num = 0f;
		foreach (CellOffset cellOffset in this.solarCellOffsets)
		{
			int num2 = Grid.LightIntensity[Grid.OffsetCell(Grid.PosToCell(this), cellOffset)];
			num += (float)num2 * 0.00053f;
		}
		this.operational.SetActive(num > 0f, false);
		num = Mathf.Clamp(num, 0f, 380f);
		Game.Instance.accumulators.Accumulate(this.accumulator, num * dt);
		if (num > 0f)
		{
			num *= dt;
			num = Mathf.Max(num, 1f * dt);
			base.GenerateJoules(num, false);
		}
		this.meter.SetPositionPercent(Game.Instance.accumulators.GetAverageRate(this.accumulator) / 380f);
		this.UpdateStatusItem();
	}

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06002A78 RID: 10872 RVA: 0x000E0500 File Offset: 0x000DE700
	public float CurrentWattage
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.accumulator);
		}
	}

	// Token: 0x04001917 RID: 6423
	private MeterController meter;

	// Token: 0x04001918 RID: 6424
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x04001919 RID: 6425
	private SolarPanel.StatesInstance smi;

	// Token: 0x0400191A RID: 6426
	private Guid statusHandle;

	// Token: 0x0400191B RID: 6427
	private CellOffset[] solarCellOffsets = new CellOffset[]
	{
		new CellOffset(-3, 2),
		new CellOffset(-2, 2),
		new CellOffset(-1, 2),
		new CellOffset(0, 2),
		new CellOffset(1, 2),
		new CellOffset(2, 2),
		new CellOffset(3, 2),
		new CellOffset(-3, 1),
		new CellOffset(-2, 1),
		new CellOffset(-1, 1),
		new CellOffset(0, 1),
		new CellOffset(1, 1),
		new CellOffset(2, 1),
		new CellOffset(3, 1)
	};

	// Token: 0x0400191C RID: 6428
	private static readonly EventSystem.IntraObjectHandler<SolarPanel> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<SolarPanel>(delegate(SolarPanel component, object data)
	{
		component.OnActiveChanged(data);
	});

	// Token: 0x020012DB RID: 4827
	public class StatesInstance : GameStateMachine<SolarPanel.States, SolarPanel.StatesInstance, SolarPanel, object>.GameInstance
	{
		// Token: 0x06007BD2 RID: 31698 RVA: 0x002CD249 File Offset: 0x002CB449
		public StatesInstance(SolarPanel master)
			: base(master)
		{
		}
	}

	// Token: 0x020012DC RID: 4828
	public class States : GameStateMachine<SolarPanel.States, SolarPanel.StatesInstance, SolarPanel>
	{
		// Token: 0x06007BD3 RID: 31699 RVA: 0x002CD252 File Offset: 0x002CB452
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.DoNothing();
		}

		// Token: 0x04005EE6 RID: 24294
		public GameStateMachine<SolarPanel.States, SolarPanel.StatesInstance, SolarPanel, object>.State idle;
	}
}
