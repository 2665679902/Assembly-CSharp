using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000614 RID: 1556
[SerializationConfig(MemberSerialization.OptIn)]
public class ModuleSolarPanel : Generator
{
	// Token: 0x0600289D RID: 10397 RVA: 0x000D7730 File Offset: 0x000D5930
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.IsVirtual = true;
	}

	// Token: 0x0600289E RID: 10398 RVA: 0x000D7740 File Offset: 0x000D5940
	protected override void OnSpawn()
	{
		CraftModuleInterface craftInterface = base.GetComponent<RocketModuleCluster>().CraftInterface;
		base.VirtualCircuitKey = craftInterface;
		base.OnSpawn();
		base.Subscribe<ModuleSolarPanel>(824508782, ModuleSolarPanel.OnActiveChangedDelegate);
		this.smi = new ModuleSolarPanel.StatesInstance(this);
		this.smi.StartSM();
		this.accumulator = Game.Instance.accumulators.Add("Element", this);
		BuildingDef def = base.GetComponent<BuildingComplete>().Def;
		Grid.PosToCell(this);
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		this.meter.gameObject.GetComponent<KBatchedAnimTracker>().matchParentOffset = true;
	}

	// Token: 0x0600289F RID: 10399 RVA: 0x000D7816 File Offset: 0x000D5A16
	protected override void OnCleanUp()
	{
		this.smi.StopSM("cleanup");
		Game.Instance.accumulators.Remove(this.accumulator);
		base.OnCleanUp();
	}

	// Token: 0x060028A0 RID: 10400 RVA: 0x000D7844 File Offset: 0x000D5A44
	protected void OnActiveChanged(object data)
	{
		StatusItem statusItem = (((Operational)data).IsActive ? Db.Get().BuildingStatusItems.Wattage : Db.Get().BuildingStatusItems.GeneratorOffline);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, this);
	}

	// Token: 0x060028A1 RID: 10401 RVA: 0x000D789C File Offset: 0x000D5A9C
	private void UpdateStatusItem()
	{
		this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.Wattage, false);
		if (this.statusHandle == Guid.Empty)
		{
			this.statusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.ModuleSolarPanelWattage, this);
			return;
		}
		if (this.statusHandle != Guid.Empty)
		{
			base.GetComponent<KSelectable>().ReplaceStatusItem(this.statusHandle, Db.Get().BuildingStatusItems.ModuleSolarPanelWattage, this);
		}
	}

	// Token: 0x060028A2 RID: 10402 RVA: 0x000D7930 File Offset: 0x000D5B30
	public override void EnergySim200ms(float dt)
	{
		ushort circuitID = base.CircuitID;
		this.operational.SetFlag(Generator.wireConnectedFlag, true);
		this.operational.SetFlag(Generator.generatorConnectedFlag, true);
		if (!this.operational.IsOperational)
		{
			return;
		}
		float num = 0f;
		if (Grid.IsValidCell(Grid.PosToCell(this)) && Grid.WorldIdx[Grid.PosToCell(this)] != ClusterManager.INVALID_WORLD_IDX)
		{
			foreach (CellOffset cellOffset in this.solarCellOffsets)
			{
				int num2 = Grid.LightIntensity[Grid.OffsetCell(Grid.PosToCell(this), cellOffset)];
				num += (float)num2 * 0.00053f;
			}
		}
		else
		{
			num = 60f;
		}
		num = Mathf.Clamp(num, 0f, 60f);
		this.operational.SetActive(num > 0f, false);
		Game.Instance.accumulators.Accumulate(this.accumulator, num * dt);
		if (num > 0f)
		{
			num *= dt;
			num = Mathf.Max(num, 1f * dt);
			base.GenerateJoules(num, false);
		}
		this.meter.SetPositionPercent(Game.Instance.accumulators.GetAverageRate(this.accumulator) / 60f);
		this.UpdateStatusItem();
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x060028A3 RID: 10403 RVA: 0x000D7A6E File Offset: 0x000D5C6E
	public float CurrentWattage
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.accumulator);
		}
	}

	// Token: 0x040017E1 RID: 6113
	private MeterController meter;

	// Token: 0x040017E2 RID: 6114
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x040017E3 RID: 6115
	private ModuleSolarPanel.StatesInstance smi;

	// Token: 0x040017E4 RID: 6116
	private Guid statusHandle;

	// Token: 0x040017E5 RID: 6117
	private CellOffset[] solarCellOffsets = new CellOffset[]
	{
		new CellOffset(-1, 0),
		new CellOffset(0, 0),
		new CellOffset(1, 0)
	};

	// Token: 0x040017E6 RID: 6118
	private static readonly EventSystem.IntraObjectHandler<ModuleSolarPanel> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<ModuleSolarPanel>(delegate(ModuleSolarPanel component, object data)
	{
		component.OnActiveChanged(data);
	});

	// Token: 0x02001286 RID: 4742
	public class StatesInstance : GameStateMachine<ModuleSolarPanel.States, ModuleSolarPanel.StatesInstance, ModuleSolarPanel, object>.GameInstance
	{
		// Token: 0x06007A9C RID: 31388 RVA: 0x002C8669 File Offset: 0x002C6869
		public StatesInstance(ModuleSolarPanel master)
			: base(master)
		{
		}
	}

	// Token: 0x02001287 RID: 4743
	public class States : GameStateMachine<ModuleSolarPanel.States, ModuleSolarPanel.StatesInstance, ModuleSolarPanel>
	{
		// Token: 0x06007A9D RID: 31389 RVA: 0x002C8672 File Offset: 0x002C6872
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.EventTransition(GameHashes.DoLaunchRocket, this.launch, null).DoNothing();
			this.launch.EventTransition(GameHashes.RocketLanded, this.idle, null);
		}

		// Token: 0x04005E14 RID: 24084
		public GameStateMachine<ModuleSolarPanel.States, ModuleSolarPanel.StatesInstance, ModuleSolarPanel, object>.State idle;

		// Token: 0x04005E15 RID: 24085
		public GameStateMachine<ModuleSolarPanel.States, ModuleSolarPanel.StatesInstance, ModuleSolarPanel, object>.State launch;
	}
}
