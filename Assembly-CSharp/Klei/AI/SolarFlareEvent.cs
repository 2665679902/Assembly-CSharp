using System;
using STRINGS;

namespace Klei.AI
{
	// Token: 0x02000D91 RID: 3473
	public class SolarFlareEvent : GameplayEvent<SolarFlareEvent.StatesInstance>
	{
		// Token: 0x060069C9 RID: 27081 RVA: 0x0029273F File Offset: 0x0029093F
		public SolarFlareEvent()
			: base("SolarFlareEvent", 0, 0)
		{
			this.title = GAMEPLAY_EVENTS.EVENT_TYPES.SOLAR_FLARE.NAME;
			this.description = GAMEPLAY_EVENTS.EVENT_TYPES.SOLAR_FLARE.DESCRIPTION;
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x0029276E File Offset: 0x0029096E
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new SolarFlareEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x04004F9A RID: 20378
		public const string ID = "SolarFlareEvent";

		// Token: 0x04004F9B RID: 20379
		public const float DURATION = 7f;

		// Token: 0x02001E6C RID: 7788
		public class StatesInstance : GameplayEventStateMachine<SolarFlareEvent.States, SolarFlareEvent.StatesInstance, GameplayEventManager, SolarFlareEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x06009BB4 RID: 39860 RVA: 0x00339154 File Offset: 0x00337354
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, SolarFlareEvent solarFlareEvent)
				: base(master, eventInstance, solarFlareEvent)
			{
			}
		}

		// Token: 0x02001E6D RID: 7789
		public class States : GameplayEventStateMachine<SolarFlareEvent.States, SolarFlareEvent.StatesInstance, GameplayEventManager, SolarFlareEvent>
		{
			// Token: 0x06009BB5 RID: 39861 RVA: 0x0033915F File Offset: 0x0033735F
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.idle;
				base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
				this.idle.DoNothing();
				this.start.ScheduleGoTo(7f, this.finished);
				this.finished.ReturnSuccess();
			}

			// Token: 0x06009BB6 RID: 39862 RVA: 0x003391A0 File Offset: 0x003373A0
			public override EventInfoData GenerateEventPopupData(SolarFlareEvent.StatesInstance smi)
			{
				return new EventInfoData(smi.gameplayEvent.title, smi.gameplayEvent.description, smi.gameplayEvent.animFileName)
				{
					location = GAMEPLAY_EVENTS.LOCATIONS.SUN,
					whenDescription = GAMEPLAY_EVENTS.TIMES.NOW
				};
			}

			// Token: 0x040088AB RID: 34987
			public GameStateMachine<SolarFlareEvent.States, SolarFlareEvent.StatesInstance, GameplayEventManager, object>.State idle;

			// Token: 0x040088AC RID: 34988
			public GameStateMachine<SolarFlareEvent.States, SolarFlareEvent.StatesInstance, GameplayEventManager, object>.State start;

			// Token: 0x040088AD RID: 34989
			public GameStateMachine<SolarFlareEvent.States, SolarFlareEvent.StatesInstance, GameplayEventManager, object>.State finished;
		}
	}
}
