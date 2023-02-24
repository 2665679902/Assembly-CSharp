using System;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200064B RID: 1611
public class SteamTurbine : Generator
{
	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06002ACC RID: 10956 RVA: 0x000E1C01 File Offset: 0x000DFE01
	// (set) Token: 0x06002ACD RID: 10957 RVA: 0x000E1C09 File Offset: 0x000DFE09
	public int BlockedInputs { get; private set; }

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06002ACE RID: 10958 RVA: 0x000E1C12 File Offset: 0x000DFE12
	public int TotalInputs
	{
		get
		{
			return this.srcCells.Length;
		}
	}

	// Token: 0x06002ACF RID: 10959 RVA: 0x000E1C1C File Offset: 0x000DFE1C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.accumulator = Game.Instance.accumulators.Add("Power", this);
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		this.simEmitCBHandle = Game.Instance.massEmitCallbackManager.Add(new Action<Sim.MassEmittedCallback, object>(SteamTurbine.OnSimEmittedCallback), this, "SteamTurbineEmit");
		BuildingDef def = base.GetComponent<BuildingComplete>().Def;
		this.srcCells = new int[def.WidthInCells];
		int num = Grid.PosToCell(this);
		for (int i = 0; i < def.WidthInCells; i++)
		{
			int num2 = i - (def.WidthInCells - 1) / 2;
			this.srcCells[i] = Grid.OffsetCell(num, new CellOffset(num2, -2));
		}
		this.smi = new SteamTurbine.Instance(this);
		this.smi.StartSM();
		this.CreateMeter();
	}

	// Token: 0x06002AD0 RID: 10960 RVA: 0x000E1CFC File Offset: 0x000DFEFC
	private void CreateMeter()
	{
		this.meter = new MeterController(base.gameObject.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_OL", "meter_frame", "meter_fill" });
	}

	// Token: 0x06002AD1 RID: 10961 RVA: 0x000E1D4C File Offset: 0x000DFF4C
	protected override void OnCleanUp()
	{
		if (this.smi != null)
		{
			this.smi.StopSM("cleanup");
		}
		Game.Instance.massEmitCallbackManager.Release(this.simEmitCBHandle, "SteamTurbine");
		this.simEmitCBHandle.Clear();
		base.OnCleanUp();
	}

	// Token: 0x06002AD2 RID: 10962 RVA: 0x000E1DA0 File Offset: 0x000DFFA0
	private void Pump(float dt)
	{
		float num = this.pumpKGRate * dt / (float)this.srcCells.Length;
		foreach (int num2 in this.srcCells)
		{
			HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(SteamTurbine.OnSimConsumeCallback), this, "SteamTurbineConsume");
			SimMessages.ConsumeMass(num2, this.srcElem, num, 1, handle.index);
		}
	}

	// Token: 0x06002AD3 RID: 10963 RVA: 0x000E1E0E File Offset: 0x000E000E
	private static void OnSimConsumeCallback(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		((SteamTurbine)data).OnSimConsume(mass_cb_info);
	}

	// Token: 0x06002AD4 RID: 10964 RVA: 0x000E1E1C File Offset: 0x000E001C
	private void OnSimConsume(Sim.MassConsumedCallback mass_cb_info)
	{
		if (mass_cb_info.mass > 0f)
		{
			this.storedTemperature = SimUtil.CalculateFinalTemperature(this.storedMass, this.storedTemperature, mass_cb_info.mass, mass_cb_info.temperature);
			this.storedMass += mass_cb_info.mass;
			SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(this.diseaseIdx, this.diseaseCount, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount);
			this.diseaseIdx = diseaseInfo.idx;
			this.diseaseCount = diseaseInfo.count;
			if (this.storedMass > this.minConvertMass && this.simEmitCBHandle.IsValid())
			{
				Game.Instance.massEmitCallbackManager.GetItem(this.simEmitCBHandle);
				this.gasStorage.AddGasChunk(this.srcElem, this.storedMass, this.storedTemperature, this.diseaseIdx, this.diseaseCount, true, true);
				this.storedMass = 0f;
				this.storedTemperature = 0f;
				this.diseaseIdx = byte.MaxValue;
				this.diseaseCount = 0;
			}
		}
	}

	// Token: 0x06002AD5 RID: 10965 RVA: 0x000E1F2A File Offset: 0x000E012A
	private static void OnSimEmittedCallback(Sim.MassEmittedCallback info, object data)
	{
		((SteamTurbine)data).OnSimEmitted(info);
	}

	// Token: 0x06002AD6 RID: 10966 RVA: 0x000E1F38 File Offset: 0x000E0138
	private void OnSimEmitted(Sim.MassEmittedCallback info)
	{
		if (info.suceeded != 1)
		{
			this.storedTemperature = SimUtil.CalculateFinalTemperature(this.storedMass, this.storedTemperature, info.mass, info.temperature);
			this.storedMass += info.mass;
			if (info.diseaseIdx != 255)
			{
				SimUtil.DiseaseInfo diseaseInfo = new SimUtil.DiseaseInfo
				{
					idx = this.diseaseIdx,
					count = this.diseaseCount
				};
				SimUtil.DiseaseInfo diseaseInfo2 = new SimUtil.DiseaseInfo
				{
					idx = info.diseaseIdx,
					count = info.diseaseCount
				};
				SimUtil.DiseaseInfo diseaseInfo3 = SimUtil.CalculateFinalDiseaseInfo(diseaseInfo, diseaseInfo2);
				this.diseaseIdx = diseaseInfo3.idx;
				this.diseaseCount = diseaseInfo3.count;
			}
		}
	}

	// Token: 0x06002AD7 RID: 10967 RVA: 0x000E1FFC File Offset: 0x000E01FC
	public static void InitializeStatusItems()
	{
		SteamTurbine.activeStatusItem = new StatusItem("TURBINE_ACTIVE", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022, null);
		SteamTurbine.inputBlockedStatusItem = new StatusItem("TURBINE_BLOCKED_INPUT", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		SteamTurbine.inputPartiallyBlockedStatusItem = new StatusItem("TURBINE_PARTIALLY_BLOCKED_INPUT", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		SteamTurbine.inputPartiallyBlockedStatusItem.resolveStringCallback = new Func<string, object, string>(SteamTurbine.ResolvePartialBlockedStatus);
		SteamTurbine.insufficientMassStatusItem = new StatusItem("TURBINE_INSUFFICIENT_MASS", "BUILDING", "status_item_resource_unavailable", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022, null);
		SteamTurbine.insufficientMassStatusItem.resolveStringCallback = new Func<string, object, string>(SteamTurbine.ResolveStrings);
		SteamTurbine.buildingTooHotItem = new StatusItem("TURBINE_TOO_HOT", "BUILDING", "status_item_plant_temperature", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		SteamTurbine.buildingTooHotItem.resolveTooltipCallback = new Func<string, object, string>(SteamTurbine.ResolveStrings);
		SteamTurbine.insufficientTemperatureStatusItem = new StatusItem("TURBINE_INSUFFICIENT_TEMPERATURE", "BUILDING", "status_item_plant_temperature", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022, null);
		SteamTurbine.insufficientTemperatureStatusItem.resolveStringCallback = new Func<string, object, string>(SteamTurbine.ResolveStrings);
		SteamTurbine.insufficientTemperatureStatusItem.resolveTooltipCallback = new Func<string, object, string>(SteamTurbine.ResolveStrings);
		SteamTurbine.activeWattageStatusItem = new StatusItem("TURBINE_ACTIVE_WATTAGE", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022, null);
		SteamTurbine.activeWattageStatusItem.resolveStringCallback = new Func<string, object, string>(SteamTurbine.ResolveWattageStatus);
	}

	// Token: 0x06002AD8 RID: 10968 RVA: 0x000E21A8 File Offset: 0x000E03A8
	private static string ResolveWattageStatus(string str, object data)
	{
		SteamTurbine steamTurbine = (SteamTurbine)data;
		float num = Game.Instance.accumulators.GetAverageRate(steamTurbine.accumulator) / steamTurbine.WattageRating;
		return str.Replace("{Wattage}", GameUtil.GetFormattedWattage(steamTurbine.CurrentWattage, GameUtil.WattageFormatterUnit.Automatic, true)).Replace("{Max_Wattage}", GameUtil.GetFormattedWattage(steamTurbine.WattageRating, GameUtil.WattageFormatterUnit.Automatic, true)).Replace("{Efficiency}", GameUtil.GetFormattedPercent(num * 100f, GameUtil.TimeSlice.None))
			.Replace("{Src_Element}", ElementLoader.FindElementByHash(steamTurbine.srcElem).name);
	}

	// Token: 0x06002AD9 RID: 10969 RVA: 0x000E223C File Offset: 0x000E043C
	private static string ResolvePartialBlockedStatus(string str, object data)
	{
		SteamTurbine steamTurbine = (SteamTurbine)data;
		return str.Replace("{Blocked}", steamTurbine.BlockedInputs.ToString()).Replace("{Total}", steamTurbine.TotalInputs.ToString());
	}

	// Token: 0x06002ADA RID: 10970 RVA: 0x000E2284 File Offset: 0x000E0484
	private static string ResolveStrings(string str, object data)
	{
		SteamTurbine steamTurbine = (SteamTurbine)data;
		str = str.Replace("{Src_Element}", ElementLoader.FindElementByHash(steamTurbine.srcElem).name);
		str = str.Replace("{Dest_Element}", ElementLoader.FindElementByHash(steamTurbine.destElem).name);
		str = str.Replace("{Overheat_Temperature}", GameUtil.GetFormattedTemperature(steamTurbine.maxBuildingTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		str = str.Replace("{Active_Temperature}", GameUtil.GetFormattedTemperature(steamTurbine.minActiveTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		str = str.Replace("{Min_Mass}", GameUtil.GetFormattedMass(steamTurbine.requiredMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		return str;
	}

	// Token: 0x06002ADB RID: 10971 RVA: 0x000E232B File Offset: 0x000E052B
	public void SetStorage(Storage steamStorage, Storage waterStorage)
	{
		this.gasStorage = steamStorage;
		this.liquidStorage = waterStorage;
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x000E233C File Offset: 0x000E053C
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
		if (this.gasStorage != null && this.gasStorage.items.Count > 0)
		{
			GameObject gameObject = this.gasStorage.FindFirst(ElementLoader.FindElementByHash(this.srcElem).tag);
			if (gameObject != null)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				float num2 = 0.1f;
				if (component.Mass > num2)
				{
					num2 = Mathf.Min(component.Mass, this.pumpKGRate * dt);
					num = Mathf.Min(this.JoulesToGenerate(component) * (num2 / this.pumpKGRate), base.WattageRating * dt);
					float num3 = this.HeatFromCoolingSteam(component) * (num2 / component.Mass);
					float num4 = num2 / component.Mass;
					int num5 = Mathf.RoundToInt((float)component.DiseaseCount * num4);
					component.Mass -= num2;
					component.ModifyDiseaseCount(-num5, "SteamTurbine.EnergySim200ms");
					float num6 = ((this.lastSampleTime > 0f) ? (Time.time - this.lastSampleTime) : 1f);
					this.lastSampleTime = Time.time;
					GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, num3 * this.wasteHeatToTurbinePercent, BUILDINGS.PREFABS.STEAMTURBINE2.HEAT_SOURCE, num6);
					this.liquidStorage.AddLiquid(this.destElem, num2, this.outputElementTemperature, component.DiseaseIdx, num5, true, true);
				}
			}
		}
		num = Mathf.Clamp(num, 0f, base.WattageRating);
		Game.Instance.accumulators.Accumulate(this.accumulator, num);
		if (num > 0f)
		{
			base.GenerateJoules(num, false);
		}
		this.meter.SetPositionPercent(Game.Instance.accumulators.GetAverageRate(this.accumulator) / base.WattageRating);
		this.meter.SetSymbolTint(SteamTurbine.TINT_SYMBOL, Color.Lerp(Color.red, Color.green, Game.Instance.accumulators.GetAverageRate(this.accumulator) / base.WattageRating));
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x000E258C File Offset: 0x000E078C
	public float HeatFromCoolingSteam(PrimaryElement steam)
	{
		float temperature = steam.Temperature;
		return -GameUtil.CalculateEnergyDeltaForElement(steam, temperature, this.outputElementTemperature);
	}

	// Token: 0x06002ADE RID: 10974 RVA: 0x000E25B0 File Offset: 0x000E07B0
	public float JoulesToGenerate(PrimaryElement steam)
	{
		float num = (steam.Temperature - this.outputElementTemperature) / (this.idealSourceElementTemperature - this.outputElementTemperature);
		return base.WattageRating * (float)Math.Pow((double)num, 1.0);
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06002ADF RID: 10975 RVA: 0x000E25F1 File Offset: 0x000E07F1
	public float CurrentWattage
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.accumulator);
		}
	}

	// Token: 0x04001959 RID: 6489
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x0400195A RID: 6490
	public SimHashes srcElem;

	// Token: 0x0400195B RID: 6491
	public SimHashes destElem;

	// Token: 0x0400195C RID: 6492
	public float requiredMass = 0.001f;

	// Token: 0x0400195D RID: 6493
	public float minActiveTemperature = 398.15f;

	// Token: 0x0400195E RID: 6494
	public float idealSourceElementTemperature = 473.15f;

	// Token: 0x0400195F RID: 6495
	public float maxBuildingTemperature = 373.15f;

	// Token: 0x04001960 RID: 6496
	public float outputElementTemperature = 368.15f;

	// Token: 0x04001961 RID: 6497
	public float minConvertMass;

	// Token: 0x04001962 RID: 6498
	public float pumpKGRate;

	// Token: 0x04001963 RID: 6499
	public float maxSelfHeat;

	// Token: 0x04001964 RID: 6500
	public float wasteHeatToTurbinePercent;

	// Token: 0x04001965 RID: 6501
	private static readonly HashedString TINT_SYMBOL = new HashedString("meter_fill");

	// Token: 0x04001966 RID: 6502
	[Serialize]
	private float storedMass;

	// Token: 0x04001967 RID: 6503
	[Serialize]
	private float storedTemperature;

	// Token: 0x04001968 RID: 6504
	[Serialize]
	private byte diseaseIdx = byte.MaxValue;

	// Token: 0x04001969 RID: 6505
	[Serialize]
	private int diseaseCount;

	// Token: 0x0400196A RID: 6506
	private static StatusItem inputBlockedStatusItem;

	// Token: 0x0400196B RID: 6507
	private static StatusItem inputPartiallyBlockedStatusItem;

	// Token: 0x0400196C RID: 6508
	private static StatusItem insufficientMassStatusItem;

	// Token: 0x0400196D RID: 6509
	private static StatusItem insufficientTemperatureStatusItem;

	// Token: 0x0400196E RID: 6510
	private static StatusItem activeWattageStatusItem;

	// Token: 0x0400196F RID: 6511
	private static StatusItem buildingTooHotItem;

	// Token: 0x04001970 RID: 6512
	private static StatusItem activeStatusItem;

	// Token: 0x04001972 RID: 6514
	private const Sim.Cell.Properties floorCellProperties = (Sim.Cell.Properties)39;

	// Token: 0x04001973 RID: 6515
	private MeterController meter;

	// Token: 0x04001974 RID: 6516
	private HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.Handle simEmitCBHandle = HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.InvalidHandle;

	// Token: 0x04001975 RID: 6517
	private SteamTurbine.Instance smi;

	// Token: 0x04001976 RID: 6518
	private int[] srcCells;

	// Token: 0x04001977 RID: 6519
	private Storage gasStorage;

	// Token: 0x04001978 RID: 6520
	private Storage liquidStorage;

	// Token: 0x04001979 RID: 6521
	private ElementConsumer consumer;

	// Token: 0x0400197A RID: 6522
	private Guid statusHandle;

	// Token: 0x0400197B RID: 6523
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x0400197C RID: 6524
	private float lastSampleTime = -1f;

	// Token: 0x020012F5 RID: 4853
	public class States : GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine>
	{
		// Token: 0x06007C04 RID: 31748 RVA: 0x002CDFBC File Offset: 0x002CC1BC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			SteamTurbine.InitializeStatusItems();
			default_state = this.operational;
			this.root.Update("UpdateBlocked", delegate(SteamTurbine.Instance smi, float dt)
			{
				smi.UpdateBlocked(dt);
			}, UpdateRate.SIM_200ms, false);
			this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational.active, (SteamTurbine.Instance smi) => smi.master.GetComponent<Operational>().IsOperational).QueueAnim("off", false, null);
			this.operational.DefaultState(this.operational.active).EventTransition(GameHashes.OperationalChanged, this.inoperational, (SteamTurbine.Instance smi) => !smi.master.GetComponent<Operational>().IsOperational).Update("UpdateOperational", delegate(SteamTurbine.Instance smi, float dt)
			{
				smi.UpdateState(dt);
			}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(SteamTurbine.Instance smi)
				{
					smi.DisableStatusItems();
				});
			this.operational.idle.QueueAnim("on", false, null);
			this.operational.active.Update("UpdateActive", delegate(SteamTurbine.Instance smi, float dt)
			{
				smi.master.Pump(dt);
			}, UpdateRate.SIM_200ms, false).ToggleStatusItem((SteamTurbine.Instance smi) => SteamTurbine.activeStatusItem, (SteamTurbine.Instance smi) => smi.master).Enter(delegate(SteamTurbine.Instance smi)
			{
				smi.GetComponent<KAnimControllerBase>().Play(SteamTurbine.States.ACTIVE_ANIMS, KAnim.PlayMode.Loop);
				smi.GetComponent<Operational>().SetActive(true, false);
			})
				.Exit(delegate(SteamTurbine.Instance smi)
				{
					smi.master.GetComponent<Generator>().ResetJoules();
					smi.GetComponent<Operational>().SetActive(false, false);
				});
			this.operational.tooHot.Enter(delegate(SteamTurbine.Instance smi)
			{
				smi.GetComponent<KAnimControllerBase>().Play(SteamTurbine.States.TOOHOT_ANIMS, KAnim.PlayMode.Loop);
			});
		}

		// Token: 0x04005F19 RID: 24345
		public GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State inoperational;

		// Token: 0x04005F1A RID: 24346
		public SteamTurbine.States.OperationalStates operational;

		// Token: 0x04005F1B RID: 24347
		private static readonly HashedString[] ACTIVE_ANIMS = new HashedString[] { "working_pre", "working_loop" };

		// Token: 0x04005F1C RID: 24348
		private static readonly HashedString[] TOOHOT_ANIMS = new HashedString[] { "working_pre" };

		// Token: 0x02002002 RID: 8194
		public class OperationalStates : GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State
		{
			// Token: 0x04008EB2 RID: 36530
			public GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State idle;

			// Token: 0x04008EB3 RID: 36531
			public GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State active;

			// Token: 0x04008EB4 RID: 36532
			public GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State tooHot;
		}
	}

	// Token: 0x020012F6 RID: 4854
	public class Instance : GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.GameInstance
	{
		// Token: 0x06007C07 RID: 31751 RVA: 0x002CE250 File Offset: 0x002CC450
		public Instance(SteamTurbine master)
			: base(master)
		{
		}

		// Token: 0x06007C08 RID: 31752 RVA: 0x002CE2A8 File Offset: 0x002CC4A8
		public void UpdateBlocked(float dt)
		{
			base.master.BlockedInputs = 0;
			for (int i = 0; i < base.master.TotalInputs; i++)
			{
				int num = base.master.srcCells[i];
				Element element = Grid.Element[num];
				if (element.IsLiquid || element.IsSolid)
				{
					SteamTurbine master = base.master;
					int blockedInputs = master.BlockedInputs;
					master.BlockedInputs = blockedInputs + 1;
				}
			}
			KSelectable component = base.GetComponent<KSelectable>();
			this.inputBlockedHandle = this.UpdateStatusItem(SteamTurbine.inputBlockedStatusItem, base.master.BlockedInputs == base.master.TotalInputs, this.inputBlockedHandle, component);
			this.inputPartiallyBlockedHandle = this.UpdateStatusItem(SteamTurbine.inputPartiallyBlockedStatusItem, base.master.BlockedInputs > 0 && base.master.BlockedInputs < base.master.TotalInputs, this.inputPartiallyBlockedHandle, component);
		}

		// Token: 0x06007C09 RID: 31753 RVA: 0x002CE38C File Offset: 0x002CC58C
		public void UpdateState(float dt)
		{
			bool flag = this.CanSteamFlow(ref this.insufficientMass, ref this.insufficientTemperature);
			bool flag2 = this.IsTooHot(ref this.buildingTooHot);
			this.UpdateStatusItems();
			StateMachine.BaseState currentState = base.smi.GetCurrentState();
			if (flag2)
			{
				if (currentState != base.sm.operational.tooHot)
				{
					base.smi.GoTo(base.sm.operational.tooHot);
					return;
				}
			}
			else if (flag)
			{
				if (currentState != base.sm.operational.active)
				{
					base.smi.GoTo(base.sm.operational.active);
					return;
				}
			}
			else if (currentState != base.sm.operational.idle)
			{
				base.smi.GoTo(base.sm.operational.idle);
			}
		}

		// Token: 0x06007C0A RID: 31754 RVA: 0x002CE45B File Offset: 0x002CC65B
		private bool IsTooHot(ref bool building_too_hot)
		{
			building_too_hot = base.gameObject.GetComponent<PrimaryElement>().Temperature > base.smi.master.maxBuildingTemperature;
			return building_too_hot;
		}

		// Token: 0x06007C0B RID: 31755 RVA: 0x002CE484 File Offset: 0x002CC684
		private bool CanSteamFlow(ref bool insufficient_mass, ref bool insufficient_temperature)
		{
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < base.master.srcCells.Length; i++)
			{
				int num3 = base.master.srcCells[i];
				float num4 = Grid.Mass[num3];
				if (Grid.Element[num3].id == base.master.srcElem)
				{
					num = Mathf.Max(num, num4);
					float num5 = Grid.Temperature[num3];
					num2 = Mathf.Max(num2, num5);
				}
			}
			insufficient_mass = num < base.master.requiredMass;
			insufficient_temperature = num2 < base.master.minActiveTemperature;
			return !insufficient_mass && !insufficient_temperature;
		}

		// Token: 0x06007C0C RID: 31756 RVA: 0x002CE534 File Offset: 0x002CC734
		public void UpdateStatusItems()
		{
			KSelectable component = base.GetComponent<KSelectable>();
			this.insufficientMassHandle = this.UpdateStatusItem(SteamTurbine.insufficientMassStatusItem, this.insufficientMass, this.insufficientMassHandle, component);
			this.insufficientTemperatureHandle = this.UpdateStatusItem(SteamTurbine.insufficientTemperatureStatusItem, this.insufficientTemperature, this.insufficientTemperatureHandle, component);
			this.buildingTooHotHandle = this.UpdateStatusItem(SteamTurbine.buildingTooHotItem, this.buildingTooHot, this.buildingTooHotHandle, component);
			StatusItem statusItem = (base.master.operational.IsActive ? SteamTurbine.activeWattageStatusItem : Db.Get().BuildingStatusItems.GeneratorOffline);
			this.activeWattageHandle = component.SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, base.master);
		}

		// Token: 0x06007C0D RID: 31757 RVA: 0x002CE5F0 File Offset: 0x002CC7F0
		private Guid UpdateStatusItem(StatusItem item, bool show, Guid current_handle, KSelectable ksel)
		{
			Guid guid = current_handle;
			if (show != (current_handle != Guid.Empty))
			{
				if (show)
				{
					guid = ksel.AddStatusItem(item, base.master);
				}
				else
				{
					guid = ksel.RemoveStatusItem(current_handle, false);
				}
			}
			return guid;
		}

		// Token: 0x06007C0E RID: 31758 RVA: 0x002CE62C File Offset: 0x002CC82C
		public void DisableStatusItems()
		{
			KSelectable component = base.GetComponent<KSelectable>();
			component.RemoveStatusItem(this.buildingTooHotHandle, false);
			component.RemoveStatusItem(this.insufficientMassHandle, false);
			component.RemoveStatusItem(this.insufficientTemperatureHandle, false);
			component.RemoveStatusItem(this.activeWattageHandle, false);
		}

		// Token: 0x04005F1D RID: 24349
		public bool insufficientMass;

		// Token: 0x04005F1E RID: 24350
		public bool insufficientTemperature;

		// Token: 0x04005F1F RID: 24351
		public bool buildingTooHot;

		// Token: 0x04005F20 RID: 24352
		private Guid inputBlockedHandle = Guid.Empty;

		// Token: 0x04005F21 RID: 24353
		private Guid inputPartiallyBlockedHandle = Guid.Empty;

		// Token: 0x04005F22 RID: 24354
		private Guid insufficientMassHandle = Guid.Empty;

		// Token: 0x04005F23 RID: 24355
		private Guid insufficientTemperatureHandle = Guid.Empty;

		// Token: 0x04005F24 RID: 24356
		private Guid buildingTooHotHandle = Guid.Empty;

		// Token: 0x04005F25 RID: 24357
		private Guid activeWattageHandle = Guid.Empty;
	}
}
