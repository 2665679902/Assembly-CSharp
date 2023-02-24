using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000648 RID: 1608
[SerializationConfig(MemberSerialization.OptIn)]
public class SpaceHeater : StateMachineComponent<SpaceHeater.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x170002DC RID: 732
	// (get) Token: 0x06002ABB RID: 10939 RVA: 0x000E1778 File Offset: 0x000DF978
	public float TargetTemperature
	{
		get
		{
			return this.targetTemperature;
		}
	}

	// Token: 0x06002ABC RID: 10940 RVA: 0x000E1780 File Offset: 0x000DF980
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("InsulationTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, true);
		}, null, null);
		base.smi.StartSM();
	}

	// Token: 0x06002ABD RID: 10941 RVA: 0x000E17D4 File Offset: 0x000DF9D4
	public void SetLiquidHeater()
	{
		this.heatLiquid = true;
	}

	// Token: 0x06002ABE RID: 10942 RVA: 0x000E17E0 File Offset: 0x000DF9E0
	private SpaceHeater.MonitorState MonitorHeating(float dt)
	{
		this.monitorCells.Clear();
		GameUtil.GetNonSolidCells(Grid.PosToCell(base.transform.GetPosition()), this.radius, this.monitorCells);
		int num = 0;
		float num2 = 0f;
		for (int i = 0; i < this.monitorCells.Count; i++)
		{
			if (Grid.Mass[this.monitorCells[i]] > this.minimumCellMass && ((Grid.Element[this.monitorCells[i]].IsGas && !this.heatLiquid) || (Grid.Element[this.monitorCells[i]].IsLiquid && this.heatLiquid)))
			{
				num++;
				num2 += Grid.Temperature[this.monitorCells[i]];
			}
		}
		if (num == 0)
		{
			if (!this.heatLiquid)
			{
				return SpaceHeater.MonitorState.NotEnoughGas;
			}
			return SpaceHeater.MonitorState.NotEnoughLiquid;
		}
		else
		{
			if (num2 / (float)num >= this.targetTemperature)
			{
				return SpaceHeater.MonitorState.TooHot;
			}
			return SpaceHeater.MonitorState.ReadyToHeat;
		}
	}

	// Token: 0x06002ABF RID: 10943 RVA: 0x000E18E0 File Offset: 0x000DFAE0
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.HEATER_TARGETTEMPERATURE, GameUtil.GetFormattedTemperature(this.targetTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.HEATER_TARGETTEMPERATURE, GameUtil.GetFormattedTemperature(this.targetTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x0400194D RID: 6477
	public float targetTemperature = 308.15f;

	// Token: 0x0400194E RID: 6478
	public float minimumCellMass;

	// Token: 0x0400194F RID: 6479
	public int radius = 2;

	// Token: 0x04001950 RID: 6480
	[SerializeField]
	private bool heatLiquid;

	// Token: 0x04001951 RID: 6481
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001952 RID: 6482
	private List<int> monitorCells = new List<int>();

	// Token: 0x020012EF RID: 4847
	public class StatesInstance : GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.GameInstance
	{
		// Token: 0x06007BFA RID: 31738 RVA: 0x002CDC05 File Offset: 0x002CBE05
		public StatesInstance(SpaceHeater master)
			: base(master)
		{
		}
	}

	// Token: 0x020012F0 RID: 4848
	public class States : GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater>
	{
		// Token: 0x06007BFB RID: 31739 RVA: 0x002CDC10 File Offset: 0x002CBE10
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.offline;
			base.serializable = StateMachine.SerializeType.Never;
			this.statusItemUnderMassLiquid = new StatusItem("statusItemUnderMassLiquid", BUILDING.STATUSITEMS.HEATINGSTALLEDLOWMASS_LIQUID.NAME, BUILDING.STATUSITEMS.HEATINGSTALLEDLOWMASS_LIQUID.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
			this.statusItemUnderMassGas = new StatusItem("statusItemUnderMassGas", BUILDING.STATUSITEMS.HEATINGSTALLEDLOWMASS_GAS.NAME, BUILDING.STATUSITEMS.HEATINGSTALLEDLOWMASS_GAS.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
			this.statusItemOverTemp = new StatusItem("statusItemOverTemp", BUILDING.STATUSITEMS.HEATINGSTALLEDHOTENV.NAME, BUILDING.STATUSITEMS.HEATINGSTALLEDHOTENV.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
			this.statusItemOverTemp.resolveStringCallback = delegate(string str, object obj)
			{
				SpaceHeater.StatesInstance statesInstance = (SpaceHeater.StatesInstance)obj;
				return string.Format(str, GameUtil.GetFormattedTemperature(statesInstance.master.TargetTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			};
			this.offline.EventTransition(GameHashes.OperationalChanged, this.online, (SpaceHeater.StatesInstance smi) => smi.master.operational.IsOperational);
			this.online.EventTransition(GameHashes.OperationalChanged, this.offline, (SpaceHeater.StatesInstance smi) => !smi.master.operational.IsOperational).DefaultState(this.online.heating).Update("spaceheater_online", delegate(SpaceHeater.StatesInstance smi, float dt)
			{
				switch (smi.master.MonitorHeating(dt))
				{
				case SpaceHeater.MonitorState.ReadyToHeat:
					smi.GoTo(this.online.heating);
					return;
				case SpaceHeater.MonitorState.TooHot:
					smi.GoTo(this.online.overtemp);
					return;
				case SpaceHeater.MonitorState.NotEnoughLiquid:
					smi.GoTo(this.online.undermassliquid);
					return;
				case SpaceHeater.MonitorState.NotEnoughGas:
					smi.GoTo(this.online.undermassgas);
					return;
				default:
					return;
				}
			}, UpdateRate.SIM_4000ms, false);
			this.online.heating.Enter(delegate(SpaceHeater.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).Exit(delegate(SpaceHeater.StatesInstance smi)
			{
				smi.master.operational.SetActive(false, false);
			});
			this.online.undermassliquid.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Heat, this.statusItemUnderMassLiquid, null);
			this.online.undermassgas.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Heat, this.statusItemUnderMassGas, null);
			this.online.overtemp.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Heat, this.statusItemOverTemp, null);
		}

		// Token: 0x04005F0B RID: 24331
		public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State offline;

		// Token: 0x04005F0C RID: 24332
		public SpaceHeater.States.OnlineStates online;

		// Token: 0x04005F0D RID: 24333
		private StatusItem statusItemUnderMassLiquid;

		// Token: 0x04005F0E RID: 24334
		private StatusItem statusItemUnderMassGas;

		// Token: 0x04005F0F RID: 24335
		private StatusItem statusItemOverTemp;

		// Token: 0x02001FFF RID: 8191
		public class OnlineStates : GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State
		{
			// Token: 0x04008EA4 RID: 36516
			public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State heating;

			// Token: 0x04008EA5 RID: 36517
			public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State overtemp;

			// Token: 0x04008EA6 RID: 36518
			public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State undermassliquid;

			// Token: 0x04008EA7 RID: 36519
			public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State undermassgas;
		}
	}

	// Token: 0x020012F1 RID: 4849
	private enum MonitorState
	{
		// Token: 0x04005F11 RID: 24337
		ReadyToHeat,
		// Token: 0x04005F12 RID: 24338
		TooHot,
		// Token: 0x04005F13 RID: 24339
		NotEnoughLiquid,
		// Token: 0x04005F14 RID: 24340
		NotEnoughGas
	}
}
