using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Klei.AI;

// Token: 0x020003B5 RID: 949
public class GameplayEventPreconditions
{
	// Token: 0x1700007E RID: 126
	// (get) Token: 0x0600139D RID: 5021 RVA: 0x00067A63 File Offset: 0x00065C63
	public static GameplayEventPreconditions Instance
	{
		get
		{
			if (GameplayEventPreconditions._instance == null)
			{
				GameplayEventPreconditions._instance = new GameplayEventPreconditions();
			}
			return GameplayEventPreconditions._instance;
		}
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x00067A7C File Offset: 0x00065C7C
	public GameplayEventPrecondition LiveMinions(int count = 1)
	{
		return new GameplayEventPrecondition
		{
			condition = () => Components.LiveMinionIdentities.Count >= count,
			description = string.Format("At least {0} dupes alive", count)
		};
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x00067AC8 File Offset: 0x00065CC8
	public GameplayEventPrecondition BuildingExists(string buildingId, int count = 1)
	{
		return new GameplayEventPrecondition
		{
			condition = () => BuildingInventory.Instance.BuildingCount(new Tag(buildingId)) >= count,
			description = string.Format("{0} {1} has been built", count, buildingId)
		};
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x00067B24 File Offset: 0x00065D24
	public GameplayEventPrecondition ResearchCompleted(string techName)
	{
		return new GameplayEventPrecondition
		{
			condition = () => Research.Instance.Get(Db.Get().Techs.Get(techName)).IsComplete(),
			description = "Has researched " + techName + "."
		};
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x00067B70 File Offset: 0x00065D70
	public GameplayEventPrecondition AchievementUnlocked(ColonyAchievement achievement)
	{
		return new GameplayEventPrecondition
		{
			condition = () => SaveGame.Instance.GetComponent<ColonyAchievementTracker>().IsAchievementUnlocked(achievement),
			description = "Unlocked the " + achievement.Id + " achievement"
		};
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x00067BC4 File Offset: 0x00065DC4
	public GameplayEventPrecondition RoomBuilt(RoomType roomType)
	{
		Predicate<Room> <>9__1;
		return new GameplayEventPrecondition
		{
			condition = delegate
			{
				List<Room> rooms = Game.Instance.roomProber.rooms;
				Predicate<Room> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = (Room match) => match.roomType == roomType);
				}
				return rooms.Exists(predicate);
			},
			description = "Built a " + roomType.Id + " room"
		};
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x00067C18 File Offset: 0x00065E18
	public GameplayEventPrecondition CycleRestriction(float min = 0f, float max = float.PositiveInfinity)
	{
		return new GameplayEventPrecondition
		{
			condition = () => GameUtil.GetCurrentTimeInCycles() >= min && GameUtil.GetCurrentTimeInCycles() <= max,
			description = string.Format("After cycle {0} and before cycle {1}", min, max)
		};
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x00067C78 File Offset: 0x00065E78
	public GameplayEventPrecondition MinionsWithEffect(string effectId, int count = 1)
	{
		Func<MinionIdentity, bool> <>9__1;
		return new GameplayEventPrecondition
		{
			condition = delegate
			{
				IEnumerable<MinionIdentity> items = Components.LiveMinionIdentities.Items;
				Func<MinionIdentity, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (MinionIdentity minion) => minion.GetComponent<Effects>().Get(effectId) != null);
				}
				return items.Count(func) >= count;
			},
			description = string.Format("At least {0} dupes have the {1} effect applied", count, effectId)
		};
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x00067CD4 File Offset: 0x00065ED4
	public GameplayEventPrecondition MinionsWithStatusItem(StatusItem statusItem, int count = 1)
	{
		Func<MinionIdentity, bool> <>9__1;
		return new GameplayEventPrecondition
		{
			condition = delegate
			{
				IEnumerable<MinionIdentity> items = Components.LiveMinionIdentities.Items;
				Func<MinionIdentity, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (MinionIdentity minion) => minion.GetComponent<KSelectable>().HasStatusItem(statusItem));
				}
				return items.Count(func) >= count;
			},
			description = string.Format("At least {0} dupes have the {1} status item", count, statusItem)
		};
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x00067D30 File Offset: 0x00065F30
	public GameplayEventPrecondition MinionsWithChoreGroupPriorityOrGreater(ChoreGroup choreGroup, int count, int priority)
	{
		Func<MinionIdentity, bool> <>9__1;
		return new GameplayEventPrecondition
		{
			condition = delegate
			{
				IEnumerable<MinionIdentity> items = Components.LiveMinionIdentities.Items;
				Func<MinionIdentity, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(MinionIdentity minion)
					{
						ChoreConsumer component = minion.GetComponent<ChoreConsumer>();
						return !component.IsChoreGroupDisabled(choreGroup) && component.GetPersonalPriority(choreGroup) >= priority;
					});
				}
				return items.Count(func) >= count;
			},
			description = string.Format("At least {0} dupes have their {1} set to {2} or higher.", count, choreGroup.Name, priority)
		};
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x00067DA0 File Offset: 0x00065FA0
	public GameplayEventPrecondition PastEventCount(string evtId, int count = 1)
	{
		return new GameplayEventPrecondition
		{
			condition = () => GameplayEventManager.Instance.NumberOfPastEvents(evtId) >= count,
			description = string.Format("The {0} event has triggered {1} times.", evtId, count)
		};
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x00067DFC File Offset: 0x00065FFC
	public GameplayEventPrecondition PastEventCountAndNotActive(GameplayEvent evt, int count = 1)
	{
		return new GameplayEventPrecondition
		{
			condition = () => GameplayEventManager.Instance.NumberOfPastEvents(evt.IdHash) >= count && !GameplayEventManager.Instance.IsGameplayEventActive(evt),
			description = string.Format("The {0} event has triggered {1} times and is not active.", evt.Id, count)
		};
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x00067E5C File Offset: 0x0006605C
	public GameplayEventPrecondition Not(GameplayEventPrecondition precondition)
	{
		return new GameplayEventPrecondition
		{
			condition = () => !precondition.condition(),
			description = "Not[" + precondition.description + "]"
		};
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x00067EB0 File Offset: 0x000660B0
	public GameplayEventPrecondition Or(GameplayEventPrecondition precondition1, GameplayEventPrecondition precondition2)
	{
		return new GameplayEventPrecondition
		{
			condition = () => precondition1.condition() || precondition2.condition(),
			description = string.Concat(new string[] { "[", precondition1.description, "]-OR-[", precondition2.description, "]" })
		};
	}

	// Token: 0x04000AA8 RID: 2728
	private static GameplayEventPreconditions _instance;
}
