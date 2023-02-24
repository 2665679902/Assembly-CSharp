using System;
using STRINGS;

// Token: 0x02000606 RID: 1542
public class MassiveHeatSink : StateMachineComponent<MassiveHeatSink.StatesInstance>
{
	// Token: 0x0600283C RID: 10300 RVA: 0x000D5D64 File Offset: 0x000D3F64
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x040017AC RID: 6060
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040017AD RID: 6061
	[MyCmpReq]
	private ElementConverter elementConverter;

	// Token: 0x02001272 RID: 4722
	public class States : GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink>
	{
		// Token: 0x06007A41 RID: 31297 RVA: 0x002C6D84 File Offset: 0x002C4F84
		private string AwaitingFuelResolveString(string str, object obj)
		{
			ElementConverter elementConverter = ((MassiveHeatSink.StatesInstance)obj).master.elementConverter;
			string text = elementConverter.consumedElements[0].Tag.ProperName();
			string formattedMass = GameUtil.GetFormattedMass(elementConverter.consumedElements[0].MassConsumptionRate, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
			str = string.Format(str, text, formattedMass);
			return str;
		}

		// Token: 0x06007A42 RID: 31298 RVA: 0x002C6DE4 File Offset: 0x002C4FE4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.disabled;
			this.disabled.EventTransition(GameHashes.OperationalChanged, this.idle, (MassiveHeatSink.StatesInstance smi) => smi.master.operational.IsOperational);
			this.idle.EventTransition(GameHashes.OperationalChanged, this.disabled, (MassiveHeatSink.StatesInstance smi) => !smi.master.operational.IsOperational).ToggleStatusItem(BUILDING.STATUSITEMS.AWAITINGFUEL.NAME, BUILDING.STATUSITEMS.AWAITINGFUEL.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, default(HashedString), 129022, new Func<string, MassiveHeatSink.StatesInstance, string>(this.AwaitingFuelResolveString), null, null).EventTransition(GameHashes.OnStorageChange, this.active, (MassiveHeatSink.StatesInstance smi) => smi.master.elementConverter.HasEnoughMassToStartConverting(false));
			this.active.EventTransition(GameHashes.OperationalChanged, this.disabled, (MassiveHeatSink.StatesInstance smi) => !smi.master.operational.IsOperational).EventTransition(GameHashes.OnStorageChange, this.idle, (MassiveHeatSink.StatesInstance smi) => !smi.master.elementConverter.HasEnoughMassToStartConverting(false)).Enter(delegate(MassiveHeatSink.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			})
				.Exit(delegate(MassiveHeatSink.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				});
		}

		// Token: 0x04005DCB RID: 24011
		public GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink, object>.State disabled;

		// Token: 0x04005DCC RID: 24012
		public GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink, object>.State idle;

		// Token: 0x04005DCD RID: 24013
		public GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink, object>.State active;
	}

	// Token: 0x02001273 RID: 4723
	public class StatesInstance : GameStateMachine<MassiveHeatSink.States, MassiveHeatSink.StatesInstance, MassiveHeatSink, object>.GameInstance
	{
		// Token: 0x06007A44 RID: 31300 RVA: 0x002C6F86 File Offset: 0x002C5186
		public StatesInstance(MassiveHeatSink master)
			: base(master)
		{
		}
	}
}
