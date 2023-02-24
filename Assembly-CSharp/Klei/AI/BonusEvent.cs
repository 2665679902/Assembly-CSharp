using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D86 RID: 3462
	public class BonusEvent : GameplayEvent<BonusEvent.StatesInstance>
	{
		// Token: 0x060069A3 RID: 27043 RVA: 0x00291E28 File Offset: 0x00290028
		public BonusEvent(string id, string overrideEffect = null, int numTimesAllowed = 1, bool preSelectMinion = false, int priority = 0)
			: base(id, priority, 0)
		{
			this.title = Strings.Get("STRINGS.GAMEPLAY_EVENTS.BONUS." + id.ToUpper() + ".NAME");
			this.description = Strings.Get("STRINGS.GAMEPLAY_EVENTS.BONUS." + id.ToUpper() + ".DESCRIPTION");
			this.effect = ((overrideEffect != null) ? overrideEffect : id);
			this.numTimesAllowed = numTimesAllowed;
			this.preSelectMinion = preSelectMinion;
			this.animFileName = id.ToLower() + "_kanim";
			base.AddPrecondition(GameplayEventPreconditions.Instance.LiveMinions(1));
		}

		// Token: 0x060069A4 RID: 27044 RVA: 0x00291ED2 File Offset: 0x002900D2
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new BonusEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x060069A5 RID: 27045 RVA: 0x00291EDC File Offset: 0x002900DC
		public BonusEvent TriggerOnNewBuilding(int triggerCount, params string[] buildings)
		{
			DebugUtil.DevAssert(this.triggerType == BonusEvent.TriggerType.None, "Only one trigger per event", null);
			this.triggerType = BonusEvent.TriggerType.NewBuilding;
			this.buildingTrigger = new HashSet<Tag>(buildings.ToTagList());
			this.numTimesToTrigger = triggerCount;
			return this;
		}

		// Token: 0x060069A6 RID: 27046 RVA: 0x00291F12 File Offset: 0x00290112
		public BonusEvent TriggerOnUseBuilding(int triggerCount, params string[] buildings)
		{
			DebugUtil.DevAssert(this.triggerType == BonusEvent.TriggerType.None, "Only one trigger per event", null);
			this.triggerType = BonusEvent.TriggerType.UseBuilding;
			this.buildingTrigger = new HashSet<Tag>(buildings.ToTagList());
			this.numTimesToTrigger = triggerCount;
			return this;
		}

		// Token: 0x060069A7 RID: 27047 RVA: 0x00291F48 File Offset: 0x00290148
		public BonusEvent TriggerOnWorkableComplete(int triggerCount, params Type[] types)
		{
			DebugUtil.DevAssert(this.triggerType == BonusEvent.TriggerType.None, "Only one trigger per event", null);
			this.triggerType = BonusEvent.TriggerType.WorkableComplete;
			this.workableType = new HashSet<Type>(types);
			this.numTimesToTrigger = triggerCount;
			return this;
		}

		// Token: 0x060069A8 RID: 27048 RVA: 0x00291F79 File Offset: 0x00290179
		public BonusEvent SetExtraCondition(BonusEvent.ConditionFn extraCondition)
		{
			this.extraCondition = extraCondition;
			return this;
		}

		// Token: 0x060069A9 RID: 27049 RVA: 0x00291F83 File Offset: 0x00290183
		public BonusEvent SetRoomConstraints(bool hasOwnableInRoom, params RoomType[] types)
		{
			this.roomHasOwnable = hasOwnableInRoom;
			this.roomRestrictions = ((types == null) ? null : new HashSet<RoomType>(types));
			return this;
		}

		// Token: 0x060069AA RID: 27050 RVA: 0x00291F9F File Offset: 0x0029019F
		public string GetEffectTooltip(Effect effect)
		{
			return effect.Name + "\n\n" + Effect.CreateTooltip(effect, true, "\n    • ", true);
		}

		// Token: 0x060069AB RID: 27051 RVA: 0x00291FC0 File Offset: 0x002901C0
		public override Sprite GetDisplaySprite()
		{
			Effect effect = Db.Get().effects.Get(this.effect);
			if (effect.SelfModifiers.Count > 0)
			{
				return Assets.GetSprite(Db.Get().Attributes.TryGet(effect.SelfModifiers[0].AttributeId).uiFullColourSprite);
			}
			return null;
		}

		// Token: 0x060069AC RID: 27052 RVA: 0x00292024 File Offset: 0x00290224
		public override string GetDisplayString()
		{
			Effect effect = Db.Get().effects.Get(this.effect);
			if (effect.SelfModifiers.Count > 0)
			{
				return Db.Get().Attributes.TryGet(effect.SelfModifiers[0].AttributeId).Name;
			}
			return null;
		}

		// Token: 0x04004F62 RID: 20322
		public const int PRE_SELECT_MINION_TIMEOUT = 5;

		// Token: 0x04004F63 RID: 20323
		public string effect;

		// Token: 0x04004F64 RID: 20324
		public bool preSelectMinion;

		// Token: 0x04004F65 RID: 20325
		public int numTimesToTrigger;

		// Token: 0x04004F66 RID: 20326
		public BonusEvent.TriggerType triggerType;

		// Token: 0x04004F67 RID: 20327
		public HashSet<Tag> buildingTrigger;

		// Token: 0x04004F68 RID: 20328
		public HashSet<Type> workableType;

		// Token: 0x04004F69 RID: 20329
		public HashSet<RoomType> roomRestrictions;

		// Token: 0x04004F6A RID: 20330
		public BonusEvent.ConditionFn extraCondition;

		// Token: 0x04004F6B RID: 20331
		public bool roomHasOwnable;

		// Token: 0x02001E54 RID: 7764
		public enum TriggerType
		{
			// Token: 0x0400885E RID: 34910
			None,
			// Token: 0x0400885F RID: 34911
			NewBuilding,
			// Token: 0x04008860 RID: 34912
			UseBuilding,
			// Token: 0x04008861 RID: 34913
			WorkableComplete,
			// Token: 0x04008862 RID: 34914
			AchievementUnlocked
		}

		// Token: 0x02001E55 RID: 7765
		// (Invoke) Token: 0x06009B49 RID: 39753
		public delegate bool ConditionFn(BonusEvent.GameplayEventData data);

		// Token: 0x02001E56 RID: 7766
		public class GameplayEventData
		{
			// Token: 0x04008863 RID: 34915
			public GameHashes eventTrigger;

			// Token: 0x04008864 RID: 34916
			public BuildingComplete building;

			// Token: 0x04008865 RID: 34917
			public Workable workable;

			// Token: 0x04008866 RID: 34918
			public Worker worker;
		}

		// Token: 0x02001E57 RID: 7767
		public class States : GameplayEventStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, BonusEvent>
		{
			// Token: 0x06009B4D RID: 39757 RVA: 0x00336840 File Offset: 0x00334A40
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.load;
				base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
				this.load.Enter(new StateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State.Callback(this.AssignPreSelectedMinionIfNeeded)).Transition(this.waitNewBuilding, (BonusEvent.StatesInstance smi) => smi.gameplayEvent.triggerType == BonusEvent.TriggerType.NewBuilding, UpdateRate.SIM_200ms).Transition(this.waitUseBuilding, (BonusEvent.StatesInstance smi) => smi.gameplayEvent.triggerType == BonusEvent.TriggerType.UseBuilding, UpdateRate.SIM_200ms)
					.Transition(this.waitforWorkables, (BonusEvent.StatesInstance smi) => smi.gameplayEvent.triggerType == BonusEvent.TriggerType.WorkableComplete, UpdateRate.SIM_200ms)
					.Transition(this.waitForAchievement, (BonusEvent.StatesInstance smi) => smi.gameplayEvent.triggerType == BonusEvent.TriggerType.AchievementUnlocked, UpdateRate.SIM_200ms)
					.Transition(this.immediate, (BonusEvent.StatesInstance smi) => smi.gameplayEvent.triggerType == BonusEvent.TriggerType.None, UpdateRate.SIM_200ms);
				this.waitNewBuilding.EventHandlerTransition(GameHashes.NewBuilding, this.active, new Func<BonusEvent.StatesInstance, object, bool>(this.BuildingEventTrigger));
				this.waitUseBuilding.EventHandlerTransition(GameHashes.UseBuilding, this.active, new Func<BonusEvent.StatesInstance, object, bool>(this.BuildingEventTrigger));
				this.waitforWorkables.EventHandlerTransition(GameHashes.UseBuilding, this.active, new Func<BonusEvent.StatesInstance, object, bool>(this.WorkableEventTrigger));
				this.immediate.Enter(delegate(BonusEvent.StatesInstance smi)
				{
					GameObject gameObject = smi.sm.chosen.Get(smi);
					if (gameObject == null)
					{
						gameObject = smi.gameplayEvent.GetRandomMinionPrioritizeFiltered().gameObject;
						smi.sm.chosen.Set(gameObject, smi, false);
					}
				}).GoTo(this.active);
				this.active.Enter(delegate(BonusEvent.StatesInstance smi)
				{
					smi.sm.chosen.Get(smi).GetComponent<Effects>().Add(smi.gameplayEvent.effect, true);
				}).Enter(delegate(BonusEvent.StatesInstance smi)
				{
					base.MonitorStart(this.chosen, smi);
				}).Exit(delegate(BonusEvent.StatesInstance smi)
				{
					base.MonitorStop(this.chosen, smi);
				})
					.ScheduleGoTo(delegate(BonusEvent.StatesInstance smi)
					{
						Effect effect = this.GetEffect(smi);
						if (effect != null)
						{
							return effect.duration;
						}
						return 0f;
					}, this.ending)
					.DefaultState(this.active.notify)
					.OnTargetLost(this.chosen, this.ending)
					.Target(this.chosen)
					.TagTransition(GameTags.Dead, this.ending, false);
				this.active.notify.ToggleNotification((BonusEvent.StatesInstance smi) => EventInfoScreen.CreateNotification(this.GenerateEventPopupData(smi), null));
				this.active.seenNotification.Enter(delegate(BonusEvent.StatesInstance smi)
				{
					smi.eventInstance.seenNotification = true;
				});
				this.ending.ReturnSuccess();
			}

			// Token: 0x06009B4E RID: 39758 RVA: 0x00336AE4 File Offset: 0x00334CE4
			public override EventInfoData GenerateEventPopupData(BonusEvent.StatesInstance smi)
			{
				EventInfoData eventInfoData = new EventInfoData(smi.gameplayEvent.title, smi.gameplayEvent.description, smi.gameplayEvent.animFileName);
				GameObject gameObject = smi.sm.chosen.Get(smi);
				if (gameObject == null)
				{
					DebugUtil.LogErrorArgs(new object[] { "Minion not set for " + smi.gameplayEvent.Id });
					return null;
				}
				Effect effect = this.GetEffect(smi);
				if (effect == null)
				{
					return null;
				}
				eventInfoData.clickFocus = gameObject.transform;
				eventInfoData.minions = new GameObject[] { gameObject };
				eventInfoData.SetTextParameter("dupe", gameObject.GetProperName());
				if (smi.building != null)
				{
					eventInfoData.SetTextParameter("building", UI.FormatAsLink(smi.building.GetProperName(), smi.building.GetProperName().ToUpper()));
				}
				EventInfoData.Option option = eventInfoData.AddDefaultOption(delegate
				{
					smi.GoTo(smi.sm.active.seenNotification);
				});
				GAMEPLAY_EVENTS.BONUS_EVENT_DESCRIPTION.Replace("{effects}", Effect.CreateTooltip(effect, false, " ", false)).Replace("{durration}", GameUtil.GetFormattedCycles(effect.duration, "F1", false));
				foreach (AttributeModifier attributeModifier in effect.SelfModifiers)
				{
					Attribute attribute = Db.Get().Attributes.TryGet(attributeModifier.AttributeId);
					string text = string.Format(DUPLICANTS.MODIFIERS.MODIFIER_FORMAT, attribute.Name, attributeModifier.GetFormattedString());
					text = text + "\n" + string.Format(DUPLICANTS.MODIFIERS.TIME_TOTAL, GameUtil.GetFormattedCycles(effect.duration, "F1", false));
					Sprite sprite = Assets.GetSprite(attribute.uiFullColourSprite);
					option.AddPositiveIcon(sprite, text, 1.75f);
				}
				return eventInfoData;
			}

			// Token: 0x06009B4F RID: 39759 RVA: 0x00336D2C File Offset: 0x00334F2C
			private void AssignPreSelectedMinionIfNeeded(BonusEvent.StatesInstance smi)
			{
				if (smi.gameplayEvent.preSelectMinion && smi.sm.chosen.Get(smi) == null)
				{
					smi.sm.chosen.Set(smi.gameplayEvent.GetRandomMinionPrioritizeFiltered().gameObject, smi, false);
					smi.timesTriggered = 0;
				}
			}

			// Token: 0x06009B50 RID: 39760 RVA: 0x00336D8C File Offset: 0x00334F8C
			private bool IsCorrectMinion(BonusEvent.StatesInstance smi, BonusEvent.GameplayEventData gameplayEventData)
			{
				if (!smi.gameplayEvent.preSelectMinion || !(smi.sm.chosen.Get(smi) != gameplayEventData.worker.gameObject))
				{
					return true;
				}
				if (GameUtil.GetCurrentTimeInCycles() - smi.lastTriggered > 5f && smi.PercentageUntilTriggered() < 0.5f)
				{
					smi.sm.chosen.Set(gameplayEventData.worker.gameObject, smi, false);
					smi.timesTriggered = 0;
					return true;
				}
				return false;
			}

			// Token: 0x06009B51 RID: 39761 RVA: 0x00336E14 File Offset: 0x00335014
			private bool OtherConditionsAreSatisfied(BonusEvent.StatesInstance smi, BonusEvent.GameplayEventData gameplayEventData)
			{
				if (smi.gameplayEvent.roomRestrictions != null)
				{
					Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(gameplayEventData.worker.gameObject);
					if (roomOfGameObject == null)
					{
						return false;
					}
					if (!smi.gameplayEvent.roomRestrictions.Contains(roomOfGameObject.roomType))
					{
						return false;
					}
					if (smi.gameplayEvent.roomHasOwnable)
					{
						bool flag = false;
						using (List<Ownables>.Enumerator enumerator = roomOfGameObject.GetOwners().GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.gameObject == gameplayEventData.worker.gameObject)
								{
									flag = true;
									break;
								}
							}
						}
						if (!flag)
						{
							return false;
						}
					}
				}
				return smi.gameplayEvent.extraCondition == null || smi.gameplayEvent.extraCondition(gameplayEventData);
			}

			// Token: 0x06009B52 RID: 39762 RVA: 0x00336EFC File Offset: 0x003350FC
			private bool IncrementAndTrigger(BonusEvent.StatesInstance smi, BonusEvent.GameplayEventData gameplayEventData)
			{
				smi.timesTriggered++;
				smi.lastTriggered = GameUtil.GetCurrentTimeInCycles();
				if (smi.timesTriggered < smi.gameplayEvent.numTimesToTrigger)
				{
					return false;
				}
				smi.building = gameplayEventData.building;
				smi.sm.chosen.Set(gameplayEventData.worker.gameObject, smi, false);
				return true;
			}

			// Token: 0x06009B53 RID: 39763 RVA: 0x00336F64 File Offset: 0x00335164
			private bool BuildingEventTrigger(BonusEvent.StatesInstance smi, object data)
			{
				BonusEvent.GameplayEventData gameplayEventData = data as BonusEvent.GameplayEventData;
				if (gameplayEventData == null)
				{
					return false;
				}
				this.AssignPreSelectedMinionIfNeeded(smi);
				return !(gameplayEventData.building == null) && (smi.gameplayEvent.buildingTrigger.Count <= 0 || smi.gameplayEvent.buildingTrigger.Contains(gameplayEventData.building.PrefabID())) && this.OtherConditionsAreSatisfied(smi, gameplayEventData) && this.IsCorrectMinion(smi, gameplayEventData) && this.IncrementAndTrigger(smi, gameplayEventData);
			}

			// Token: 0x06009B54 RID: 39764 RVA: 0x00336FE8 File Offset: 0x003351E8
			private bool WorkableEventTrigger(BonusEvent.StatesInstance smi, object data)
			{
				BonusEvent.GameplayEventData gameplayEventData = data as BonusEvent.GameplayEventData;
				if (gameplayEventData == null)
				{
					return false;
				}
				this.AssignPreSelectedMinionIfNeeded(smi);
				return (smi.gameplayEvent.workableType.Count <= 0 || smi.gameplayEvent.workableType.Contains(gameplayEventData.workable.GetType())) && this.OtherConditionsAreSatisfied(smi, gameplayEventData) && this.IsCorrectMinion(smi, gameplayEventData) && this.IncrementAndTrigger(smi, gameplayEventData);
			}

			// Token: 0x06009B55 RID: 39765 RVA: 0x0033705A File Offset: 0x0033525A
			private bool ChosenMinionDied(BonusEvent.StatesInstance smi, object data)
			{
				return smi.sm.chosen.Get(smi) == data as GameObject;
			}

			// Token: 0x06009B56 RID: 39766 RVA: 0x00337078 File Offset: 0x00335278
			private Effect GetEffect(BonusEvent.StatesInstance smi)
			{
				GameObject gameObject = smi.sm.chosen.Get(smi);
				if (gameObject == null)
				{
					return null;
				}
				EffectInstance effectInstance = gameObject.GetComponent<Effects>().Get(smi.gameplayEvent.effect);
				if (effectInstance == null)
				{
					global::Debug.LogWarning(string.Format("Effect {0} not found on {1} in BonusEvent", smi.gameplayEvent.effect, gameObject));
					return null;
				}
				return effectInstance.effect;
			}

			// Token: 0x04008867 RID: 34919
			public StateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.TargetParameter chosen;

			// Token: 0x04008868 RID: 34920
			public GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State load;

			// Token: 0x04008869 RID: 34921
			public GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State waitNewBuilding;

			// Token: 0x0400886A RID: 34922
			public GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State waitUseBuilding;

			// Token: 0x0400886B RID: 34923
			public GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State waitForAchievement;

			// Token: 0x0400886C RID: 34924
			public GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State waitforWorkables;

			// Token: 0x0400886D RID: 34925
			public GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State immediate;

			// Token: 0x0400886E RID: 34926
			public BonusEvent.States.ActiveStates active;

			// Token: 0x0400886F RID: 34927
			public GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State ending;

			// Token: 0x02002D8D RID: 11661
			public class ActiveStates : GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State
			{
				// Token: 0x0400BA00 RID: 47616
				public GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State notify;

				// Token: 0x0400BA01 RID: 47617
				public GameStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, object>.State seenNotification;
			}
		}

		// Token: 0x02001E58 RID: 7768
		public class StatesInstance : GameplayEventStateMachine<BonusEvent.States, BonusEvent.StatesInstance, GameplayEventManager, BonusEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x06009B5C RID: 39772 RVA: 0x0033713B File Offset: 0x0033533B
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, BonusEvent bonusEvent)
				: base(master, eventInstance, bonusEvent)
			{
				this.lastTriggered = GameUtil.GetCurrentTimeInCycles();
			}

			// Token: 0x06009B5D RID: 39773 RVA: 0x00337151 File Offset: 0x00335351
			public float PercentageUntilTriggered()
			{
				return (float)this.timesTriggered / (float)base.smi.gameplayEvent.numTimesToTrigger;
			}

			// Token: 0x04008870 RID: 34928
			[Serialize]
			public int timesTriggered;

			// Token: 0x04008871 RID: 34929
			[Serialize]
			public float lastTriggered;

			// Token: 0x04008872 RID: 34930
			public BuildingComplete building;
		}
	}
}
