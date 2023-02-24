using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x020003AF RID: 943
[DebuggerDisplay("{base.Id}")]
public abstract class GameplayEvent : Resource, IComparable<GameplayEvent>
{
	// Token: 0x17000079 RID: 121
	// (get) Token: 0x0600136E RID: 4974 RVA: 0x000671D4 File Offset: 0x000653D4
	// (set) Token: 0x0600136F RID: 4975 RVA: 0x000671DC File Offset: 0x000653DC
	public int importance { get; private set; }

	// Token: 0x06001370 RID: 4976 RVA: 0x000671E8 File Offset: 0x000653E8
	public virtual bool IsAllowed()
	{
		if (this.WillNeverRunAgain())
		{
			return false;
		}
		if (!this.allowMultipleEventInstances && GameplayEventManager.Instance.IsGameplayEventActive(this))
		{
			return false;
		}
		foreach (GameplayEventPrecondition gameplayEventPrecondition in this.preconditions)
		{
			if (gameplayEventPrecondition.required && !gameplayEventPrecondition.condition())
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x00067274 File Offset: 0x00065474
	public virtual bool WillNeverRunAgain()
	{
		return this.numTimesAllowed != -1 && GameplayEventManager.Instance.NumberOfPastEvents(this.Id) >= this.numTimesAllowed;
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x000672A1 File Offset: 0x000654A1
	public int GetCashedPriority()
	{
		return this.calculatedPriority;
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x000672A9 File Offset: 0x000654A9
	public virtual int CalculatePriority()
	{
		this.calculatedPriority = this.basePriority + this.CalculateBoost();
		return this.calculatedPriority;
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x000672C4 File Offset: 0x000654C4
	public int CalculateBoost()
	{
		int num = 0;
		foreach (GameplayEventPrecondition gameplayEventPrecondition in this.preconditions)
		{
			if (!gameplayEventPrecondition.required && gameplayEventPrecondition.condition())
			{
				num += gameplayEventPrecondition.priorityModifier;
			}
		}
		return num;
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x00067334 File Offset: 0x00065534
	public GameplayEvent AddPrecondition(GameplayEventPrecondition precondition)
	{
		precondition.required = true;
		this.preconditions.Add(precondition);
		return this;
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x0006734A File Offset: 0x0006554A
	public GameplayEvent AddPriorityBoost(GameplayEventPrecondition precondition, int priorityBoost)
	{
		precondition.required = false;
		precondition.priorityModifier = priorityBoost;
		this.preconditions.Add(precondition);
		return this;
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x00067367 File Offset: 0x00065567
	public GameplayEvent AddMinionFilter(GameplayEventMinionFilter filter)
	{
		this.minionFilters.Add(filter);
		return this;
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x00067376 File Offset: 0x00065576
	public GameplayEvent TrySpawnEventOnSuccess(HashedString evt)
	{
		this.successEvents.Add(evt);
		return this;
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x00067385 File Offset: 0x00065585
	public GameplayEvent TrySpawnEventOnFailure(HashedString evt)
	{
		this.failureEvents.Add(evt);
		return this;
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x00067394 File Offset: 0x00065594
	public GameplayEvent SetVisuals(HashedString animFileName)
	{
		this.animFileName = animFileName;
		return this;
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x0006739E File Offset: 0x0006559E
	public virtual Sprite GetDisplaySprite()
	{
		return null;
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x000673A1 File Offset: 0x000655A1
	public virtual string GetDisplayString()
	{
		return null;
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x000673A4 File Offset: 0x000655A4
	public MinionIdentity GetRandomFilteredMinion()
	{
		List<MinionIdentity> list = new List<MinionIdentity>(Components.LiveMinionIdentities.Items);
		using (List<GameplayEventMinionFilter>.Enumerator enumerator = this.minionFilters.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				GameplayEventMinionFilter filter = enumerator.Current;
				list.RemoveAll((MinionIdentity x) => !filter.filter(x));
			}
		}
		if (list.Count != 0)
		{
			return list[UnityEngine.Random.Range(0, list.Count)];
		}
		return null;
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x0006743C File Offset: 0x0006563C
	public MinionIdentity GetRandomMinionPrioritizeFiltered()
	{
		MinionIdentity randomFilteredMinion = this.GetRandomFilteredMinion();
		if (!(randomFilteredMinion == null))
		{
			return randomFilteredMinion;
		}
		return Components.LiveMinionIdentities.Items[UnityEngine.Random.Range(0, Components.LiveMinionIdentities.Items.Count)];
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x00067480 File Offset: 0x00065680
	public int CompareTo(GameplayEvent other)
	{
		return -this.GetCashedPriority().CompareTo(other.GetCashedPriority());
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x000674A4 File Offset: 0x000656A4
	public GameplayEvent(string id, int priority, int importance)
		: base(id, null, null)
	{
		this.tags = new List<Tag>();
		this.basePriority = priority;
		this.preconditions = new List<GameplayEventPrecondition>();
		this.minionFilters = new List<GameplayEventMinionFilter>();
		this.successEvents = new List<HashedString>();
		this.failureEvents = new List<HashedString>();
		this.importance = importance;
		this.animFileName = id;
	}

	// Token: 0x06001381 RID: 4993
	public abstract StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance);

	// Token: 0x06001382 RID: 4994 RVA: 0x00067514 File Offset: 0x00065714
	public GameplayEventInstance CreateInstance(int worldId)
	{
		GameplayEventInstance gameplayEventInstance = new GameplayEventInstance(this, worldId);
		if (this.tags != null)
		{
			gameplayEventInstance.tags.AddRange(this.tags);
		}
		return gameplayEventInstance;
	}

	// Token: 0x04000A88 RID: 2696
	public const int INFINITE = -1;

	// Token: 0x04000A89 RID: 2697
	public int numTimesAllowed = -1;

	// Token: 0x04000A8A RID: 2698
	public bool allowMultipleEventInstances;

	// Token: 0x04000A8B RID: 2699
	public int durration;

	// Token: 0x04000A8C RID: 2700
	public int warning;

	// Token: 0x04000A8D RID: 2701
	protected int basePriority;

	// Token: 0x04000A8E RID: 2702
	protected int calculatedPriority;

	// Token: 0x04000A90 RID: 2704
	public List<GameplayEventPrecondition> preconditions;

	// Token: 0x04000A91 RID: 2705
	public List<GameplayEventMinionFilter> minionFilters;

	// Token: 0x04000A92 RID: 2706
	public List<HashedString> successEvents;

	// Token: 0x04000A93 RID: 2707
	public List<HashedString> failureEvents;

	// Token: 0x04000A94 RID: 2708
	public string title;

	// Token: 0x04000A95 RID: 2709
	public string description;

	// Token: 0x04000A96 RID: 2710
	public HashedString animFileName;

	// Token: 0x04000A97 RID: 2711
	public List<Tag> tags;

	// Token: 0x02000FBD RID: 4029
	public enum Occurance
	{
		// Token: 0x04005567 RID: 21863
		Once,
		// Token: 0x04005568 RID: 21864
		Infinity
	}
}
