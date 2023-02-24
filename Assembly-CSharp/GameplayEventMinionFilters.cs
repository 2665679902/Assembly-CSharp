using System;
using Database;

// Token: 0x020003B3 RID: 947
public class GameplayEventMinionFilters
{
	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06001393 RID: 5011 RVA: 0x0006782E File Offset: 0x00065A2E
	public static GameplayEventMinionFilters Instance
	{
		get
		{
			if (GameplayEventMinionFilters._instance == null)
			{
				GameplayEventMinionFilters._instance = new GameplayEventMinionFilters();
			}
			return GameplayEventMinionFilters._instance;
		}
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x00067848 File Offset: 0x00065A48
	public GameplayEventMinionFilter HasMasteredSkill(Skill skill)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => minion.GetComponent<MinionResume>().HasMasteredSkill(skill.Id),
			id = "HasMasteredSkill"
		};
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x00067884 File Offset: 0x00065A84
	public GameplayEventMinionFilter HasSkillAptitude(Skill skill)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => minion.GetComponent<MinionResume>().HasSkillAptitude(skill),
			id = "HasSkillAptitude"
		};
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000678C0 File Offset: 0x00065AC0
	public GameplayEventMinionFilter HasChoreGroupPriorityOrHigher(ChoreGroup choreGroup, int priority)
	{
		return new GameplayEventMinionFilter
		{
			filter = delegate(MinionIdentity minion)
			{
				ChoreConsumer component = minion.GetComponent<ChoreConsumer>();
				return !component.IsChoreGroupDisabled(choreGroup) && component.GetPersonalPriority(choreGroup) >= priority;
			},
			id = "HasChoreGroupPriorityOrHigher"
		};
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x00067904 File Offset: 0x00065B04
	public GameplayEventMinionFilter AgeRange(float min = 0f, float max = float.PositiveInfinity)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => minion.arrivalTime >= min && minion.arrivalTime <= max,
			id = "AgeRange"
		};
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x00067947 File Offset: 0x00065B47
	public GameplayEventMinionFilter PriorityIn()
	{
		GameplayEventMinionFilter gameplayEventMinionFilter = new GameplayEventMinionFilter();
		gameplayEventMinionFilter.filter = (MinionIdentity minion) => true;
		gameplayEventMinionFilter.id = "PriorityIn";
		return gameplayEventMinionFilter;
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x00067980 File Offset: 0x00065B80
	public GameplayEventMinionFilter Not(GameplayEventMinionFilter filter)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => !filter.filter(minion),
			id = "Not[" + filter.id + "]"
		};
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x000679D4 File Offset: 0x00065BD4
	public GameplayEventMinionFilter Or(GameplayEventMinionFilter precondition1, GameplayEventMinionFilter precondition2)
	{
		return new GameplayEventMinionFilter
		{
			filter = (MinionIdentity minion) => precondition1.filter(minion) || precondition2.filter(minion),
			id = string.Concat(new string[] { "[", precondition1.id, "]-OR-[", precondition2.id, "]" })
		};
	}

	// Token: 0x04000AA3 RID: 2723
	private static GameplayEventMinionFilters _instance;
}
