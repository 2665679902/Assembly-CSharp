using System;
using Klei;
using KSerialization;
using UnityEngine;

// Token: 0x02000666 RID: 1638
[AddComponentMenu("KMonoBehaviour/scripts/Turbine")]
public class Turbine : KMonoBehaviour
{
	// Token: 0x06002C1B RID: 11291 RVA: 0x000E790C File Offset: 0x000E5B0C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.simEmitCBHandle = Game.Instance.massEmitCallbackManager.Add(new Action<Sim.MassEmittedCallback, object>(Turbine.OnSimEmittedCallback), this, "TurbineEmit");
		BuildingDef def = base.GetComponent<BuildingComplete>().Def;
		this.srcCells = new int[def.WidthInCells];
		this.destCells = new int[def.WidthInCells];
		int num = Grid.PosToCell(this);
		for (int i = 0; i < def.WidthInCells; i++)
		{
			int num2 = i - (def.WidthInCells - 1) / 2;
			this.srcCells[i] = Grid.OffsetCell(num, new CellOffset(num2, -1));
			this.destCells[i] = Grid.OffsetCell(num, new CellOffset(num2, def.HeightInCells - 1));
		}
		this.smi = new Turbine.Instance(this);
		this.smi.StartSM();
		this.CreateMeter();
	}

	// Token: 0x06002C1C RID: 11292 RVA: 0x000E79E8 File Offset: 0x000E5BE8
	private void CreateMeter()
	{
		this.meter = new MeterController(base.gameObject.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_OL", "meter_frame", "meter_fill" });
		this.smi.UpdateMeter();
	}

	// Token: 0x06002C1D RID: 11293 RVA: 0x000E7A44 File Offset: 0x000E5C44
	protected override void OnCleanUp()
	{
		if (this.smi != null)
		{
			this.smi.StopSM("cleanup");
		}
		Game.Instance.massEmitCallbackManager.Release(this.simEmitCBHandle, "Turbine");
		this.simEmitCBHandle.Clear();
		base.OnCleanUp();
	}

	// Token: 0x06002C1E RID: 11294 RVA: 0x000E7A98 File Offset: 0x000E5C98
	private void Pump(float dt)
	{
		float num = this.pumpKGRate * dt / (float)this.srcCells.Length;
		foreach (int num2 in this.srcCells)
		{
			HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(Turbine.OnSimConsumeCallback), this, "TurbineConsume");
			SimMessages.ConsumeMass(num2, this.srcElem, num, 1, handle.index);
		}
	}

	// Token: 0x06002C1F RID: 11295 RVA: 0x000E7B06 File Offset: 0x000E5D06
	private static void OnSimConsumeCallback(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		((Turbine)data).OnSimConsume(mass_cb_info);
	}

	// Token: 0x06002C20 RID: 11296 RVA: 0x000E7B14 File Offset: 0x000E5D14
	private void OnSimConsume(Sim.MassConsumedCallback mass_cb_info)
	{
		if (mass_cb_info.mass > 0f)
		{
			this.storedTemperature = SimUtil.CalculateFinalTemperature(this.storedMass, this.storedTemperature, mass_cb_info.mass, mass_cb_info.temperature);
			this.storedMass += mass_cb_info.mass;
			SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(this.diseaseIdx, this.diseaseCount, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount);
			this.diseaseIdx = diseaseInfo.idx;
			this.diseaseCount = diseaseInfo.count;
			if (this.storedMass > this.minEmitMass && this.simEmitCBHandle.IsValid())
			{
				float num = this.storedMass / (float)this.destCells.Length;
				int num2 = this.diseaseCount / this.destCells.Length;
				Game.Instance.massEmitCallbackManager.GetItem(this.simEmitCBHandle);
				int[] array = this.destCells;
				for (int i = 0; i < array.Length; i++)
				{
					SimMessages.EmitMass(array[i], mass_cb_info.elemIdx, num, this.emitTemperature, this.diseaseIdx, num2, this.simEmitCBHandle.index);
				}
				this.storedMass = 0f;
				this.storedTemperature = 0f;
				this.diseaseIdx = byte.MaxValue;
				this.diseaseCount = 0;
			}
		}
	}

	// Token: 0x06002C21 RID: 11297 RVA: 0x000E7C5E File Offset: 0x000E5E5E
	private static void OnSimEmittedCallback(Sim.MassEmittedCallback info, object data)
	{
		((Turbine)data).OnSimEmitted(info);
	}

	// Token: 0x06002C22 RID: 11298 RVA: 0x000E7C6C File Offset: 0x000E5E6C
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

	// Token: 0x06002C23 RID: 11299 RVA: 0x000E7D30 File Offset: 0x000E5F30
	public static void InitializeStatusItems()
	{
		Turbine.inputBlockedStatusItem = new StatusItem("TURBINE_BLOCKED_INPUT", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		Turbine.outputBlockedStatusItem = new StatusItem("TURBINE_BLOCKED_OUTPUT", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		Turbine.spinningUpStatusItem = new StatusItem("TURBINE_SPINNING_UP", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022, null);
		Turbine.activeStatusItem = new StatusItem("TURBINE_ACTIVE", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022, null);
		Turbine.activeStatusItem.resolveStringCallback = delegate(string str, object data)
		{
			Turbine turbine = (Turbine)data;
			str = string.Format(str, (int)turbine.currentRPM);
			return str;
		};
		Turbine.insufficientMassStatusItem = new StatusItem("TURBINE_INSUFFICIENT_MASS", "BUILDING", "status_item_resource_unavailable", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022, null);
		Turbine.insufficientMassStatusItem.resolveTooltipCallback = delegate(string str, object data)
		{
			Turbine turbine2 = (Turbine)data;
			str = str.Replace("{MASS}", GameUtil.GetFormattedMass(turbine2.requiredMassFlowDifferential, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			str = str.Replace("{SRC_ELEMENT}", ElementLoader.FindElementByHash(turbine2.srcElem).name);
			return str;
		};
		Turbine.insufficientTemperatureStatusItem = new StatusItem("TURBINE_INSUFFICIENT_TEMPERATURE", "BUILDING", "status_item_plant_temperature", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022, null);
		Turbine.insufficientTemperatureStatusItem.resolveStringCallback = new Func<string, object, string>(Turbine.ResolveStrings);
		Turbine.insufficientTemperatureStatusItem.resolveTooltipCallback = new Func<string, object, string>(Turbine.ResolveStrings);
	}

	// Token: 0x06002C24 RID: 11300 RVA: 0x000E7EAC File Offset: 0x000E60AC
	private static string ResolveStrings(string str, object data)
	{
		Turbine turbine = (Turbine)data;
		str = str.Replace("{SRC_ELEMENT}", ElementLoader.FindElementByHash(turbine.srcElem).name);
		str = str.Replace("{ACTIVE_TEMPERATURE}", GameUtil.GetFormattedTemperature(turbine.minActiveTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		return str;
	}

	// Token: 0x04001A22 RID: 6690
	public SimHashes srcElem;

	// Token: 0x04001A23 RID: 6691
	public float requiredMassFlowDifferential = 3f;

	// Token: 0x04001A24 RID: 6692
	public float activePercent = 0.75f;

	// Token: 0x04001A25 RID: 6693
	public float minEmitMass;

	// Token: 0x04001A26 RID: 6694
	public float minActiveTemperature = 400f;

	// Token: 0x04001A27 RID: 6695
	public float emitTemperature = 300f;

	// Token: 0x04001A28 RID: 6696
	public float maxRPM;

	// Token: 0x04001A29 RID: 6697
	public float rpmAcceleration;

	// Token: 0x04001A2A RID: 6698
	public float rpmDeceleration;

	// Token: 0x04001A2B RID: 6699
	public float minGenerationRPM;

	// Token: 0x04001A2C RID: 6700
	public float pumpKGRate;

	// Token: 0x04001A2D RID: 6701
	private static readonly HashedString TINT_SYMBOL = new HashedString("meter_fill");

	// Token: 0x04001A2E RID: 6702
	[Serialize]
	private float storedMass;

	// Token: 0x04001A2F RID: 6703
	[Serialize]
	private float storedTemperature;

	// Token: 0x04001A30 RID: 6704
	[Serialize]
	private byte diseaseIdx = byte.MaxValue;

	// Token: 0x04001A31 RID: 6705
	[Serialize]
	private int diseaseCount;

	// Token: 0x04001A32 RID: 6706
	[MyCmpGet]
	private Generator generator;

	// Token: 0x04001A33 RID: 6707
	[Serialize]
	private float currentRPM;

	// Token: 0x04001A34 RID: 6708
	private int[] srcCells;

	// Token: 0x04001A35 RID: 6709
	private int[] destCells;

	// Token: 0x04001A36 RID: 6710
	private Turbine.Instance smi;

	// Token: 0x04001A37 RID: 6711
	private static StatusItem inputBlockedStatusItem;

	// Token: 0x04001A38 RID: 6712
	private static StatusItem outputBlockedStatusItem;

	// Token: 0x04001A39 RID: 6713
	private static StatusItem insufficientMassStatusItem;

	// Token: 0x04001A3A RID: 6714
	private static StatusItem insufficientTemperatureStatusItem;

	// Token: 0x04001A3B RID: 6715
	private static StatusItem activeStatusItem;

	// Token: 0x04001A3C RID: 6716
	private static StatusItem spinningUpStatusItem;

	// Token: 0x04001A3D RID: 6717
	private MeterController meter;

	// Token: 0x04001A3E RID: 6718
	private HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.Handle simEmitCBHandle = HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.InvalidHandle;

	// Token: 0x02001328 RID: 4904
	public class States : GameStateMachine<Turbine.States, Turbine.Instance, Turbine>
	{
		// Token: 0x06007CD5 RID: 31957 RVA: 0x002D16B4 File Offset: 0x002CF8B4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			Turbine.InitializeStatusItems();
			default_state = this.operational;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational.spinningUp, (Turbine.Instance smi) => smi.master.GetComponent<Operational>().IsOperational).QueueAnim("off", false, null).Enter(delegate(Turbine.Instance smi)
			{
				smi.master.currentRPM = 0f;
				smi.UpdateMeter();
			});
			this.operational.DefaultState(this.operational.spinningUp).EventTransition(GameHashes.OperationalChanged, this.inoperational, (Turbine.Instance smi) => !smi.master.GetComponent<Operational>().IsOperational).Update("UpdateOperational", delegate(Turbine.Instance smi, float dt)
			{
				smi.UpdateState(dt);
			}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(Turbine.Instance smi)
				{
					smi.DisableStatusItems();
				});
			this.operational.idle.QueueAnim("on", false, null);
			this.operational.spinningUp.ToggleStatusItem((Turbine.Instance smi) => Turbine.spinningUpStatusItem, (Turbine.Instance smi) => smi.master).QueueAnim("buildup", true, null);
			this.operational.active.Update("UpdateActive", delegate(Turbine.Instance smi, float dt)
			{
				smi.master.Pump(dt);
			}, UpdateRate.SIM_200ms, false).ToggleStatusItem((Turbine.Instance smi) => Turbine.activeStatusItem, (Turbine.Instance smi) => smi.master).Enter(delegate(Turbine.Instance smi)
			{
				smi.GetComponent<KAnimControllerBase>().Play(Turbine.States.ACTIVE_ANIMS, KAnim.PlayMode.Loop);
				smi.GetComponent<Operational>().SetActive(true, false);
			})
				.Exit(delegate(Turbine.Instance smi)
				{
					smi.master.GetComponent<Generator>().ResetJoules();
					smi.GetComponent<Operational>().SetActive(false, false);
				});
		}

		// Token: 0x04005FB0 RID: 24496
		public GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State inoperational;

		// Token: 0x04005FB1 RID: 24497
		public Turbine.States.OperationalStates operational;

		// Token: 0x04005FB2 RID: 24498
		private static readonly HashedString[] ACTIVE_ANIMS = new HashedString[] { "working_pre", "working_loop" };

		// Token: 0x02002016 RID: 8214
		public class OperationalStates : GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State
		{
			// Token: 0x04008F23 RID: 36643
			public GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State idle;

			// Token: 0x04008F24 RID: 36644
			public GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State spinningUp;

			// Token: 0x04008F25 RID: 36645
			public GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.State active;
		}
	}

	// Token: 0x02001329 RID: 4905
	public class Instance : GameStateMachine<Turbine.States, Turbine.Instance, Turbine, object>.GameInstance
	{
		// Token: 0x06007CD8 RID: 31960 RVA: 0x002D1941 File Offset: 0x002CFB41
		public Instance(Turbine master)
			: base(master)
		{
		}

		// Token: 0x06007CD9 RID: 31961 RVA: 0x002D1978 File Offset: 0x002CFB78
		public void UpdateState(float dt)
		{
			float num = (this.CanSteamFlow(ref this.insufficientMass, ref this.insufficientTemperature) ? base.master.rpmAcceleration : (-base.master.rpmDeceleration));
			base.master.currentRPM = Mathf.Clamp(base.master.currentRPM + dt * num, 0f, base.master.maxRPM);
			this.UpdateMeter();
			this.UpdateStatusItems();
			StateMachine.BaseState currentState = base.smi.GetCurrentState();
			if (base.master.currentRPM >= base.master.minGenerationRPM)
			{
				if (currentState != base.sm.operational.active)
				{
					base.smi.GoTo(base.sm.operational.active);
				}
				base.smi.master.generator.GenerateJoules(base.smi.master.generator.WattageRating * dt, false);
				return;
			}
			if (base.master.currentRPM > 0f)
			{
				if (currentState != base.sm.operational.spinningUp)
				{
					base.smi.GoTo(base.sm.operational.spinningUp);
					return;
				}
			}
			else if (currentState != base.sm.operational.idle)
			{
				base.smi.GoTo(base.sm.operational.idle);
			}
		}

		// Token: 0x06007CDA RID: 31962 RVA: 0x002D1AE0 File Offset: 0x002CFCE0
		public void UpdateMeter()
		{
			if (base.master.meter != null)
			{
				float num = Mathf.Clamp01(base.master.currentRPM / base.master.maxRPM);
				base.master.meter.SetPositionPercent(num);
				base.master.meter.SetSymbolTint(Turbine.TINT_SYMBOL, (num >= base.master.activePercent) ? Color.green : Color.red);
			}
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x002D1B64 File Offset: 0x002CFD64
		private bool CanSteamFlow(ref bool insufficient_mass, ref bool insufficient_temperature)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = float.PositiveInfinity;
			this.isInputBlocked = false;
			for (int i = 0; i < base.master.srcCells.Length; i++)
			{
				int num4 = base.master.srcCells[i];
				float num5 = Grid.Mass[num4];
				if (Grid.Element[num4].id == base.master.srcElem)
				{
					num = Mathf.Max(num, num5);
				}
				float num6 = Grid.Temperature[num4];
				num2 = Mathf.Max(num2, num6);
				ushort num7 = Grid.ElementIdx[num4];
				Element element = ElementLoader.elements[(int)num7];
				if (element.IsLiquid || element.IsSolid)
				{
					this.isInputBlocked = true;
				}
			}
			this.isOutputBlocked = false;
			for (int j = 0; j < base.master.destCells.Length; j++)
			{
				int num8 = base.master.destCells[j];
				float num9 = Grid.Mass[num8];
				num3 = Mathf.Min(num3, num9);
				ushort num10 = Grid.ElementIdx[num8];
				Element element2 = ElementLoader.elements[(int)num10];
				if (element2.IsLiquid || element2.IsSolid)
				{
					this.isOutputBlocked = true;
				}
			}
			insufficient_mass = num - num3 < base.master.requiredMassFlowDifferential;
			insufficient_temperature = num2 < base.master.minActiveTemperature;
			return !insufficient_mass && !insufficient_temperature;
		}

		// Token: 0x06007CDC RID: 31964 RVA: 0x002D1CE0 File Offset: 0x002CFEE0
		public void UpdateStatusItems()
		{
			KSelectable component = base.GetComponent<KSelectable>();
			this.inputBlockedHandle = this.UpdateStatusItem(Turbine.inputBlockedStatusItem, this.isInputBlocked, this.inputBlockedHandle, component);
			this.outputBlockedHandle = this.UpdateStatusItem(Turbine.outputBlockedStatusItem, this.isOutputBlocked, this.outputBlockedHandle, component);
			this.insufficientMassHandle = this.UpdateStatusItem(Turbine.insufficientMassStatusItem, this.insufficientMass, this.insufficientMassHandle, component);
			this.insufficientTemperatureHandle = this.UpdateStatusItem(Turbine.insufficientTemperatureStatusItem, this.insufficientTemperature, this.insufficientTemperatureHandle, component);
		}

		// Token: 0x06007CDD RID: 31965 RVA: 0x002D1D6C File Offset: 0x002CFF6C
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

		// Token: 0x06007CDE RID: 31966 RVA: 0x002D1DA8 File Offset: 0x002CFFA8
		public void DisableStatusItems()
		{
			KSelectable component = base.GetComponent<KSelectable>();
			component.RemoveStatusItem(this.inputBlockedHandle, false);
			component.RemoveStatusItem(this.outputBlockedHandle, false);
			component.RemoveStatusItem(this.insufficientMassHandle, false);
			component.RemoveStatusItem(this.insufficientTemperatureHandle, false);
		}

		// Token: 0x04005FB3 RID: 24499
		public bool isInputBlocked;

		// Token: 0x04005FB4 RID: 24500
		public bool isOutputBlocked;

		// Token: 0x04005FB5 RID: 24501
		public bool insufficientMass;

		// Token: 0x04005FB6 RID: 24502
		public bool insufficientTemperature;

		// Token: 0x04005FB7 RID: 24503
		private Guid inputBlockedHandle = Guid.Empty;

		// Token: 0x04005FB8 RID: 24504
		private Guid outputBlockedHandle = Guid.Empty;

		// Token: 0x04005FB9 RID: 24505
		private Guid insufficientMassHandle = Guid.Empty;

		// Token: 0x04005FBA RID: 24506
		private Guid insufficientTemperatureHandle = Guid.Empty;
	}
}
