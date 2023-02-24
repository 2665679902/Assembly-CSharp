using System;
using STRINGS;

namespace Klei.AI
{
	// Token: 0x02000D88 RID: 3464
	public class EclipseEvent : GameplayEvent<EclipseEvent.StatesInstance>
	{
		// Token: 0x060069B0 RID: 27056 RVA: 0x00292127 File Offset: 0x00290327
		public EclipseEvent()
			: base("EclipseEvent", 0, 0)
		{
			this.title = GAMEPLAY_EVENTS.EVENT_TYPES.ECLIPSE.NAME;
			this.description = GAMEPLAY_EVENTS.EVENT_TYPES.ECLIPSE.DESCRIPTION;
		}

		// Token: 0x060069B1 RID: 27057 RVA: 0x00292156 File Offset: 0x00290356
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new EclipseEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x04004F71 RID: 20337
		public const string ID = "EclipseEvent";

		// Token: 0x04004F72 RID: 20338
		public const float duration = 30f;

		// Token: 0x02001E5B RID: 7771
		public class StatesInstance : GameplayEventStateMachine<EclipseEvent.States, EclipseEvent.StatesInstance, GameplayEventManager, EclipseEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x06009B68 RID: 39784 RVA: 0x003373D3 File Offset: 0x003355D3
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, EclipseEvent eclipseEvent)
				: base(master, eventInstance, eclipseEvent)
			{
			}
		}

		// Token: 0x02001E5C RID: 7772
		public class States : GameplayEventStateMachine<EclipseEvent.States, EclipseEvent.StatesInstance, GameplayEventManager, EclipseEvent>
		{
			// Token: 0x06009B69 RID: 39785 RVA: 0x003373E0 File Offset: 0x003355E0
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.planning;
				base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
				this.planning.GoTo(this.eclipse);
				this.eclipse.ToggleNotification((EclipseEvent.StatesInstance smi) => EventInfoScreen.CreateNotification(this.GenerateEventPopupData(smi), null)).Enter(delegate(EclipseEvent.StatesInstance smi)
				{
					TimeOfDay.Instance.SetEclipse(true);
				}).Exit(delegate(EclipseEvent.StatesInstance smi)
				{
					TimeOfDay.Instance.SetEclipse(false);
				})
					.ScheduleGoTo(30f, this.finished);
				this.finished.ReturnSuccess();
			}

			// Token: 0x06009B6A RID: 39786 RVA: 0x0033748C File Offset: 0x0033568C
			public override EventInfoData GenerateEventPopupData(EclipseEvent.StatesInstance smi)
			{
				return new EventInfoData(smi.gameplayEvent.title, smi.gameplayEvent.description, smi.gameplayEvent.animFileName)
				{
					location = GAMEPLAY_EVENTS.LOCATIONS.SUN,
					whenDescription = GAMEPLAY_EVENTS.TIMES.NOW
				};
			}

			// Token: 0x04008878 RID: 34936
			public GameStateMachine<EclipseEvent.States, EclipseEvent.StatesInstance, GameplayEventManager, object>.State planning;

			// Token: 0x04008879 RID: 34937
			public GameStateMachine<EclipseEvent.States, EclipseEvent.StatesInstance, GameplayEventManager, object>.State eclipse;

			// Token: 0x0400887A RID: 34938
			public GameStateMachine<EclipseEvent.States, EclipseEvent.StatesInstance, GameplayEventManager, object>.State finished;
		}
	}
}
