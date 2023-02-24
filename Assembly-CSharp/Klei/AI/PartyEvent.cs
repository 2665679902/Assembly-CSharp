using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D8D RID: 3469
	public class PartyEvent : GameplayEvent<PartyEvent.StatesInstance>
	{
		// Token: 0x060069C1 RID: 27073 RVA: 0x00292624 File Offset: 0x00290824
		public PartyEvent()
			: base("Party", 0, 0)
		{
			this.animFileName = "event_pop_up_assets_kanim";
			this.title = GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.NAME;
			this.description = GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.DESCRIPTION;
		}

		// Token: 0x060069C2 RID: 27074 RVA: 0x00292663 File Offset: 0x00290863
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new PartyEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x04004F91 RID: 20369
		public const string cancelEffect = "NoFunAllowed";

		// Token: 0x04004F92 RID: 20370
		public const float FUTURE_TIME = 60f;

		// Token: 0x04004F93 RID: 20371
		public const float DURATION = 60f;

		// Token: 0x02001E64 RID: 7780
		public class StatesInstance : GameplayEventStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, PartyEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x06009B8D RID: 39821 RVA: 0x00337FF3 File Offset: 0x003361F3
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, PartyEvent partyEvent)
				: base(master, eventInstance, partyEvent)
			{
			}

			// Token: 0x06009B8E RID: 39822 RVA: 0x00338000 File Offset: 0x00336200
			public void AddNewChore(Room room)
			{
				List<KPrefabID> list = room.buildings.FindAll((KPrefabID match) => match.HasTag(RoomConstraints.ConstraintTags.RecBuilding));
				if (list.Count == 0)
				{
					DebugUtil.LogWarningArgs(new object[] { "Tried adding a party chore but the room wasn't valid! This probably happened on load? It's because rooms aren't built yet!" });
					return;
				}
				int num = 0;
				bool flag = false;
				int locator_cell = Grid.InvalidCell;
				Predicate<Chore> <>9__2;
				while (num < 20 && !flag)
				{
					num++;
					KPrefabID kprefabID = list[UnityEngine.Random.Range(0, list.Count)];
					CellOffset cellOffset = new CellOffset(UnityEngine.Random.Range(-2, 3), 0);
					locator_cell = Grid.OffsetCell(Grid.PosToCell(kprefabID), cellOffset);
					if (!Grid.HasDoor[locator_cell] && Game.Instance.roomProber.GetCavityForCell(locator_cell) == room.cavity)
					{
						List<Chore> list2 = this.chores;
						Predicate<Chore> predicate;
						if ((predicate = <>9__2) == null)
						{
							predicate = (<>9__2 = (Chore match) => Grid.PosToCell(match.target.gameObject) == locator_cell);
						}
						if (list2.Find(predicate) == null)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return;
				}
				Vector3 vector = Grid.CellToPosCBC(locator_cell, Grid.SceneLayer.Move);
				GameObject locator = ChoreHelpers.CreateLocator("PartyWorkable", vector);
				PartyPointWorkable partyPointWorkable = locator.AddOrGet<PartyPointWorkable>();
				partyPointWorkable.SetWorkTime((float)UnityEngine.Random.Range(10, 30));
				partyPointWorkable.basePriority = RELAXATION.PRIORITY.SPECIAL_EVENT;
				partyPointWorkable.faceTargetWhenWorking = true;
				PartyChore partyChore = new PartyChore(locator.GetComponent<IStateMachineTarget>(), partyPointWorkable, null, null, delegate(Chore data)
				{
					if (this.chores != null)
					{
						this.chores.Remove(data);
						Util.KDestroyGameObject(locator);
					}
				});
				this.chores.Add(partyChore);
			}

			// Token: 0x06009B8F RID: 39823 RVA: 0x003381A4 File Offset: 0x003363A4
			public void ClearChores()
			{
				if (this.chores != null)
				{
					for (int i = this.chores.Count - 1; i >= 0; i--)
					{
						if (this.chores[i] != null)
						{
							Util.KDestroyGameObject(this.chores[i].gameObject);
						}
					}
				}
				this.chores = null;
			}

			// Token: 0x06009B90 RID: 39824 RVA: 0x003381FC File Offset: 0x003363FC
			public void UpdateChores(Room room)
			{
				if (room == null)
				{
					return;
				}
				if (this.chores == null)
				{
					this.chores = new List<Chore>();
				}
				for (int i = this.chores.Count; i < Components.LiveMinionIdentities.Count * 2; i++)
				{
					this.AddNewChore(room);
				}
			}

			// Token: 0x04008894 RID: 34964
			private List<Chore> chores;

			// Token: 0x04008895 RID: 34965
			public Notification mainNotification;
		}

		// Token: 0x02001E65 RID: 7781
		public class States : GameplayEventStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, PartyEvent>
		{
			// Token: 0x06009B91 RID: 39825 RVA: 0x00338248 File Offset: 0x00336448
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				base.InitializeStates(out default_state);
				default_state = this.planning.prepare_entities;
				base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
				this.root.Enter(new StateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State.Callback(this.PopulateTargetsAndText));
				this.planning.DefaultState(this.planning.prepare_entities);
				this.planning.prepare_entities.Enter(delegate(PartyEvent.StatesInstance smi)
				{
					if (this.planner.Get(smi) != null && this.guest.Get(smi) != null)
					{
						smi.GoTo(this.planning.wait_for_input);
						return;
					}
					smi.GoTo(this.ending);
				});
				this.planning.wait_for_input.ToggleNotification((PartyEvent.StatesInstance smi) => EventInfoScreen.CreateNotification(this.GenerateEventPopupData(smi), null));
				this.warmup.ToggleNotification((PartyEvent.StatesInstance smi) => EventInfoScreen.CreateNotification(this.GenerateEventPopupData(smi), null));
				this.warmup.wait.ScheduleGoTo(60f, this.warmup.start);
				this.warmup.start.Enter(new StateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State.Callback(this.PopulateTargetsAndText)).Enter(delegate(PartyEvent.StatesInstance smi)
				{
					if (this.GetChosenRoom(smi) == null)
					{
						smi.GoTo(this.canceled);
						return;
					}
					smi.GoTo(this.partying);
				});
				this.partying.ToggleNotification((PartyEvent.StatesInstance smi) => new Notification(GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.UNDERWAY, NotificationType.Good, (List<Notification> a, object b) => GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.UNDERWAY_TOOLTIP, null, false, 0f, null, null, this.roomObject.Get(smi).transform, true, false, false)).Update(delegate(PartyEvent.StatesInstance smi, float dt)
				{
					smi.UpdateChores(this.GetChosenRoom(smi));
				}, UpdateRate.SIM_4000ms, false).ScheduleGoTo(60f, this.ending);
				this.ending.ReturnSuccess();
				this.canceled.DoNotification((PartyEvent.StatesInstance smi) => GameplayEventManager.CreateStandardCancelledNotification(this.GenerateEventPopupData(smi))).Enter(delegate(PartyEvent.StatesInstance smi)
				{
					if (this.planner.Get(smi) != null)
					{
						this.planner.Get(smi).GetComponent<Effects>().Add("NoFunAllowed", true);
					}
					if (this.guest.Get(smi) != null)
					{
						this.guest.Get(smi).GetComponent<Effects>().Add("NoFunAllowed", true);
					}
				}).ReturnFailure();
			}

			// Token: 0x06009B92 RID: 39826 RVA: 0x003383B5 File Offset: 0x003365B5
			public Room GetChosenRoom(PartyEvent.StatesInstance smi)
			{
				return Game.Instance.roomProber.GetRoomOfGameObject(this.roomObject.Get(smi));
			}

			// Token: 0x06009B93 RID: 39827 RVA: 0x003383D4 File Offset: 0x003365D4
			public override EventInfoData GenerateEventPopupData(PartyEvent.StatesInstance smi)
			{
				EventInfoData eventInfoData = new EventInfoData(smi.gameplayEvent.title, smi.gameplayEvent.description, smi.gameplayEvent.animFileName);
				Room chosenRoom = this.GetChosenRoom(smi);
				string text = ((chosenRoom != null) ? chosenRoom.GetProperName() : GAMEPLAY_EVENTS.LOCATIONS.NONE_AVAILABLE.ToString());
				Effect effect = Db.Get().effects.Get("Socialized");
				Effect effect2 = Db.Get().effects.Get("NoFunAllowed");
				eventInfoData.location = text;
				eventInfoData.whenDescription = string.Format(GAMEPLAY_EVENTS.TIMES.IN_CYCLES, 0.1f);
				eventInfoData.minions = new GameObject[]
				{
					smi.sm.guest.Get(smi),
					smi.sm.planner.Get(smi)
				};
				bool flag = true;
				EventInfoData.Option option = eventInfoData.AddOption(GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.ACCEPT_OPTION_NAME, GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.ACCEPT_OPTION_DESC);
				option.callback = delegate
				{
					smi.GoTo(smi.sm.warmup.wait);
				};
				option.AddPositiveIcon(Assets.GetSprite("overlay_materials"), Effect.CreateFullTooltip(effect, true), 1f);
				option.tooltip = GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.ACCEPT_OPTION_DESC;
				if (!flag)
				{
					option.AddInformationIcon("Cake must be built", 1f);
					option.allowed = false;
					option.tooltip = GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.ACCEPT_OPTION_INVALID_TOOLTIP;
				}
				EventInfoData.Option option2 = eventInfoData.AddOption(GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.REJECT_OPTION_NAME, GAMEPLAY_EVENTS.EVENT_TYPES.PARTY.REJECT_OPTION_DESC);
				option2.callback = delegate
				{
					smi.GoTo(smi.sm.canceled);
				};
				option2.AddNegativeIcon(Assets.GetSprite("overlay_decor"), Effect.CreateFullTooltip(effect2, true), 1f);
				eventInfoData.AddDefaultConsiderLaterOption(null);
				eventInfoData.SetTextParameter("host", this.planner.Get(smi).GetProperName());
				eventInfoData.SetTextParameter("dupe", this.guest.Get(smi).GetProperName());
				eventInfoData.SetTextParameter("goodEffect", effect.Name);
				eventInfoData.SetTextParameter("badEffect", effect2.Name);
				return eventInfoData;
			}

			// Token: 0x06009B94 RID: 39828 RVA: 0x00338630 File Offset: 0x00336830
			public void PopulateTargetsAndText(PartyEvent.StatesInstance smi)
			{
				if (this.roomObject.Get(smi) == null)
				{
					Room room = Game.Instance.roomProber.rooms.Find((Room match) => match.roomType == Db.Get().RoomTypes.RecRoom);
					this.roomObject.Set((room != null) ? room.GetPrimaryEntities()[0] : null, smi);
				}
				if (Components.LiveMinionIdentities.Count > 0)
				{
					if (this.planner.Get(smi) == null)
					{
						MinionIdentity minionIdentity = Components.LiveMinionIdentities[UnityEngine.Random.Range(0, Components.LiveMinionIdentities.Count)];
						this.planner.Set(minionIdentity, smi);
					}
					if (this.guest.Get(smi) == null)
					{
						MinionIdentity minionIdentity2 = Components.LiveMinionIdentities[UnityEngine.Random.Range(0, Components.LiveMinionIdentities.Count)];
						this.guest.Set(minionIdentity2, smi);
					}
				}
			}

			// Token: 0x04008896 RID: 34966
			public StateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.TargetParameter roomObject;

			// Token: 0x04008897 RID: 34967
			public StateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.TargetParameter planner;

			// Token: 0x04008898 RID: 34968
			public StateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.TargetParameter guest;

			// Token: 0x04008899 RID: 34969
			public PartyEvent.States.PlanningStates planning;

			// Token: 0x0400889A RID: 34970
			public PartyEvent.States.WarmupStates warmup;

			// Token: 0x0400889B RID: 34971
			public GameStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State partying;

			// Token: 0x0400889C RID: 34972
			public GameStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State ending;

			// Token: 0x0400889D RID: 34973
			public GameStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State canceled;

			// Token: 0x02002D9B RID: 11675
			public class PlanningStates : GameStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State
			{
				// Token: 0x0400BA2A RID: 47658
				public GameStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State prepare_entities;

				// Token: 0x0400BA2B RID: 47659
				public GameStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State wait_for_input;
			}

			// Token: 0x02002D9C RID: 11676
			public class WarmupStates : GameStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State
			{
				// Token: 0x0400BA2C RID: 47660
				public GameStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State wait;

				// Token: 0x0400BA2D RID: 47661
				public GameStateMachine<PartyEvent.States, PartyEvent.StatesInstance, GameplayEventManager, object>.State start;
			}
		}
	}
}
