using System;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D8F RID: 3471
	public class SatelliteCrashEvent : GameplayEvent<SatelliteCrashEvent.StatesInstance>
	{
		// Token: 0x060069C5 RID: 27077 RVA: 0x002926C6 File Offset: 0x002908C6
		public SatelliteCrashEvent()
			: base("SatelliteCrash", 0, 0)
		{
			this.title = GAMEPLAY_EVENTS.EVENT_TYPES.SATELLITE_CRASH.NAME;
			this.description = GAMEPLAY_EVENTS.EVENT_TYPES.SATELLITE_CRASH.DESCRIPTION;
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x002926F5 File Offset: 0x002908F5
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new SatelliteCrashEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x02001E68 RID: 7784
		public class StatesInstance : GameplayEventStateMachine<SatelliteCrashEvent.States, SatelliteCrashEvent.StatesInstance, GameplayEventManager, SatelliteCrashEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x06009BA9 RID: 39849 RVA: 0x00338E19 File Offset: 0x00337019
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, SatelliteCrashEvent crashEvent)
				: base(master, eventInstance, crashEvent)
			{
			}

			// Token: 0x06009BAA RID: 39850 RVA: 0x00338E24 File Offset: 0x00337024
			public Notification Plan()
			{
				Vector3 vector = new Vector3((float)(Grid.WidthInCells / 2 + UnityEngine.Random.Range(-Grid.WidthInCells / 3, Grid.WidthInCells / 3)), (float)(Grid.HeightInCells - 1), Grid.GetLayerZ(Grid.SceneLayer.FXFront));
				GameObject spawn = Util.KInstantiate(Assets.GetPrefab(SatelliteCometConfig.ID), vector);
				spawn.SetActive(true);
				Notification notification = EventInfoScreen.CreateNotification(base.smi.sm.GenerateEventPopupData(base.smi), null);
				notification.clickFocus = spawn.transform;
				Comet component = spawn.GetComponent<Comet>();
				component.OnImpact = (System.Action)Delegate.Combine(component.OnImpact, new System.Action(delegate
				{
					GameObject gameObject = new GameObject();
					gameObject.transform.position = spawn.transform.position;
					notification.clickFocus = gameObject.transform;
					GridVisibility.Reveal(Grid.PosToXY(gameObject.transform.position).x, Grid.PosToXY(gameObject.transform.position).y, 6, 4f);
				}));
				return notification;
			}
		}

		// Token: 0x02001E69 RID: 7785
		public class States : GameplayEventStateMachine<SatelliteCrashEvent.States, SatelliteCrashEvent.StatesInstance, GameplayEventManager, SatelliteCrashEvent>
		{
			// Token: 0x06009BAB RID: 39851 RVA: 0x00338EFC File Offset: 0x003370FC
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.notify;
				this.notify.ToggleNotification((SatelliteCrashEvent.StatesInstance smi) => smi.Plan());
				this.ending.ReturnSuccess();
			}

			// Token: 0x06009BAC RID: 39852 RVA: 0x00338F48 File Offset: 0x00337148
			public override EventInfoData GenerateEventPopupData(SatelliteCrashEvent.StatesInstance smi)
			{
				EventInfoData eventInfoData = new EventInfoData(smi.gameplayEvent.title, smi.gameplayEvent.description, smi.gameplayEvent.animFileName);
				eventInfoData.location = GAMEPLAY_EVENTS.LOCATIONS.SURFACE;
				eventInfoData.whenDescription = GAMEPLAY_EVENTS.TIMES.NOW;
				eventInfoData.AddDefaultOption(delegate
				{
					smi.GoTo(smi.sm.ending);
				});
				return eventInfoData;
			}

			// Token: 0x040088A4 RID: 34980
			public GameStateMachine<SatelliteCrashEvent.States, SatelliteCrashEvent.StatesInstance, GameplayEventManager, object>.State notify;

			// Token: 0x040088A5 RID: 34981
			public GameStateMachine<SatelliteCrashEvent.States, SatelliteCrashEvent.StatesInstance, GameplayEventManager, object>.State ending;
		}
	}
}
