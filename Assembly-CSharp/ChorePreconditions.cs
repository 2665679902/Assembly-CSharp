using System;
using System.Collections.Generic;
using Database;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class ChorePreconditions
{
	// Token: 0x1700005C RID: 92
	// (get) Token: 0x0600122D RID: 4653 RVA: 0x00060311 File Offset: 0x0005E511
	public static ChorePreconditions instance
	{
		get
		{
			if (ChorePreconditions._instance == null)
			{
				ChorePreconditions._instance = new ChorePreconditions();
			}
			return ChorePreconditions._instance;
		}
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x00060329 File Offset: 0x0005E529
	public static void DestroyInstance()
	{
		ChorePreconditions._instance = null;
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x00060334 File Offset: 0x0005E534
	public ChorePreconditions()
	{
		Chore.Precondition precondition = default(Chore.Precondition);
		precondition.id = "IsPreemptable";
		precondition.sortOrder = 1;
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_PREEMPTABLE;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return context.isAttemptingOverride || context.chore.CanPreempt(context) || context.chore.driver == null;
		};
		this.IsPreemptable = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "HasUrge";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_URGE;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.chore.choreType.urge == null)
			{
				return true;
			}
			foreach (Urge urge in context.consumerState.consumer.GetUrges())
			{
				if (context.chore.SatisfiesUrge(urge))
				{
					return true;
				}
			}
			return false;
		};
		this.HasUrge = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsValid";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_VALID;
		precondition.sortOrder = -4;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !context.chore.isNull && context.chore.IsValid();
		};
		this.IsValid = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsPermitted";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_PERMITTED;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return context.consumerState.consumer.IsPermittedOrEnabled(context.choreTypeForPermission, context.chore);
		};
		this.IsPermitted = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsAssignedToMe";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_ASSIGNED_TO_ME;
		precondition.sortOrder = 10;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Assignable assignable = (Assignable)data;
			IAssignableIdentity component = context.consumerState.gameObject.GetComponent<IAssignableIdentity>();
			return component != null && assignable.IsAssignedTo(component);
		};
		this.IsAssignedtoMe = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsInMyWorld";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_IN_MY_WORLD;
		precondition.sortOrder = -1;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !context.chore.isNull && context.chore.gameObject.IsMyWorld(context.consumerState.gameObject);
		};
		this.IsInMyWorld = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsInMyParentWorld";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_IN_MY_WORLD;
		precondition.sortOrder = -1;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !context.chore.isNull && context.chore.gameObject.IsMyParentWorld(context.consumerState.gameObject);
		};
		this.IsInMyParentWorld = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsCellNotInMyWorld";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_CELL_NOT_IN_MY_WORLD;
		precondition.sortOrder = -1;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (!context.chore.isNull)
			{
				int num = (int)data;
				return !Grid.IsValidCell(num) || (int)Grid.WorldIdx[num] != context.consumerState.gameObject.GetMyWorldId();
			}
			return false;
		};
		this.IsCellNotInMyWorld = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsInMyRoom";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_IN_MY_ROOM;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			int num2 = (int)data;
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num2);
			Room room = null;
			if (cavityForCell != null)
			{
				room = cavityForCell.room;
			}
			if (room != null)
			{
				if (context.consumerState.ownable != null)
				{
					using (List<Ownables>.Enumerator enumerator2 = room.GetOwners().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							if (enumerator2.Current.gameObject == context.consumerState.gameObject)
							{
								return true;
							}
						}
						return false;
					}
				}
				Room room2 = null;
				FetchChore fetchChore = context.chore as FetchChore;
				if (fetchChore != null && fetchChore.destination != null)
				{
					CavityInfo cavityForCell2 = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(fetchChore.destination));
					if (cavityForCell2 != null)
					{
						room2 = cavityForCell2.room;
					}
					return room2 != null && room2 == room;
				}
				if (context.chore is WorkChore<Tinkerable>)
				{
					CavityInfo cavityForCell3 = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell((context.chore as WorkChore<Tinkerable>).gameObject));
					if (cavityForCell3 != null)
					{
						room2 = cavityForCell3.room;
					}
					return room2 != null && room2 == room;
				}
				return false;
			}
			return false;
		};
		this.IsInMyRoom = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsPreferredAssignable";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_PREFERRED_ASSIGNABLE;
		precondition.sortOrder = 10;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Assignable assignable2 = (Assignable)data;
			return Game.Instance.assignmentManager.GetPreferredAssignables(context.consumerState.assignables, assignable2.slot).Contains(assignable2);
		};
		this.IsPreferredAssignable = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsPreferredAssignableOrUrgent";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_PREFERRED_ASSIGNABLE_OR_URGENT_BLADDER;
		precondition.sortOrder = 10;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Assignable assignable3 = (Assignable)data;
			if (Game.Instance.assignmentManager.IsPreferredAssignable(context.consumerState.assignables, assignable3))
			{
				return true;
			}
			PeeChoreMonitor.Instance smi = context.consumerState.gameObject.GetSMI<PeeChoreMonitor.Instance>();
			return smi != null && smi.IsInsideState(smi.sm.critical);
		};
		this.IsPreferredAssignableOrUrgentBladder = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsNotTransferArm";
		precondition.description = "";
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !context.consumerState.hasSolidTransferArm;
		};
		this.IsNotTransferArm = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "HasSkillPerk";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_SKILL_PERK;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			MinionResume resume = context.consumerState.resume;
			if (!resume)
			{
				return false;
			}
			if (data is SkillPerk)
			{
				SkillPerk skillPerk = data as SkillPerk;
				return resume.HasPerk(skillPerk);
			}
			if (data is HashedString)
			{
				HashedString hashedString = (HashedString)data;
				return resume.HasPerk(hashedString);
			}
			if (data is string)
			{
				HashedString hashedString2 = (string)data;
				return resume.HasPerk(hashedString2);
			}
			return false;
		};
		this.HasSkillPerk = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsMinion";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_MINION;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return context.consumerState.resume != null;
		};
		this.IsMinion = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsMoreSatisfyingEarly";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_MORE_SATISFYING;
		precondition.sortOrder = -2;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.isAttemptingOverride)
			{
				return true;
			}
			if (context.skipMoreSatisfyingEarlyPrecondition)
			{
				return true;
			}
			if (context.consumerState.selectable.IsSelected)
			{
				return true;
			}
			Chore currentChore = context.consumerState.choreDriver.GetCurrentChore();
			if (currentChore == null)
			{
				return true;
			}
			if (context.masterPriority.priority_class != currentChore.masterPriority.priority_class)
			{
				return context.masterPriority.priority_class > currentChore.masterPriority.priority_class;
			}
			if (context.consumerState.consumer != null && context.personalPriority != context.consumerState.consumer.GetPersonalPriority(currentChore.choreType))
			{
				return context.personalPriority > context.consumerState.consumer.GetPersonalPriority(currentChore.choreType);
			}
			if (context.masterPriority.priority_value != currentChore.masterPriority.priority_value)
			{
				return context.masterPriority.priority_value > currentChore.masterPriority.priority_value;
			}
			return context.priority > currentChore.choreType.priority;
		};
		this.IsMoreSatisfyingEarly = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsMoreSatisfyingLate";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_MORE_SATISFYING;
		precondition.sortOrder = 10000;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.isAttemptingOverride)
			{
				return true;
			}
			if (!context.consumerState.selectable.IsSelected)
			{
				return true;
			}
			Chore currentChore2 = context.consumerState.choreDriver.GetCurrentChore();
			if (currentChore2 == null)
			{
				return true;
			}
			if (context.masterPriority.priority_class != currentChore2.masterPriority.priority_class)
			{
				return context.masterPriority.priority_class > currentChore2.masterPriority.priority_class;
			}
			if (context.consumerState.consumer != null && context.personalPriority != context.consumerState.consumer.GetPersonalPriority(currentChore2.choreType))
			{
				return context.personalPriority > context.consumerState.consumer.GetPersonalPriority(currentChore2.choreType);
			}
			if (context.masterPriority.priority_value != currentChore2.masterPriority.priority_value)
			{
				return context.masterPriority.priority_value > currentChore2.masterPriority.priority_value;
			}
			return context.priority > currentChore2.choreType.priority;
		};
		this.IsMoreSatisfyingLate = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "CanChat";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_CHAT;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			KMonoBehaviour kmonoBehaviour = (KMonoBehaviour)data;
			return !(context.consumerState.consumer == null) && !(context.consumerState.navigator == null) && !(kmonoBehaviour == null) && context.consumerState.navigator.CanReach(kmonoBehaviour.GetComponent<Chattable>());
		};
		this.IsChattable = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsNotRedAlert";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_NOT_RED_ALERT;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return context.chore.masterPriority.priority_class == PriorityScreen.PriorityClass.topPriority || !context.chore.gameObject.GetMyWorld().IsRedAlert();
		};
		this.IsNotRedAlert = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsScheduledTime";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_SCHEDULED_TIME;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.chore.gameObject.GetMyWorld().IsRedAlert())
			{
				return true;
			}
			ScheduleBlockType scheduleBlockType = (ScheduleBlockType)data;
			ScheduleBlock scheduleBlock = context.consumerState.scheduleBlock;
			return scheduleBlock == null || scheduleBlock.IsAllowed(scheduleBlockType);
		};
		this.IsScheduledTime = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "CanMoveTo";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_MOVE_TO;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.consumerState.consumer == null)
			{
				return false;
			}
			KMonoBehaviour kmonoBehaviour2 = (KMonoBehaviour)data;
			if (kmonoBehaviour2 == null)
			{
				return false;
			}
			IApproachable approachable = (IApproachable)kmonoBehaviour2;
			int num3;
			if (context.consumerState.consumer.GetNavigationCost(approachable, out num3))
			{
				context.cost += num3;
				return true;
			}
			return false;
		};
		this.CanMoveTo = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "CanMoveToCell";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_MOVE_TO;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.consumerState.consumer == null)
			{
				return false;
			}
			int num4 = (int)data;
			if (!Grid.IsValidCell(num4))
			{
				return false;
			}
			int num5;
			if (context.consumerState.consumer.GetNavigationCost(num4, out num5))
			{
				context.cost += num5;
				return true;
			}
			return false;
		};
		this.CanMoveToCell = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "CanPickup";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_PICKUP;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Pickupable pickupable = (Pickupable)data;
			return !(pickupable == null) && !(context.consumerState.consumer == null) && !pickupable.HasTag(GameTags.StoredPrivate) && pickupable.CouldBePickedUpByMinion(context.consumerState.gameObject) && context.consumerState.consumer.CanReach(pickupable);
		};
		this.CanPickup = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsAwake";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_AWAKE;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.consumerState.consumer == null)
			{
				return false;
			}
			StaminaMonitor.Instance smi2 = context.consumerState.consumer.GetSMI<StaminaMonitor.Instance>();
			return !smi2.IsInsideState(smi2.sm.sleepy.sleeping);
		};
		this.IsAwake = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsStanding";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_STANDING;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !(context.consumerState.consumer == null) && !(context.consumerState.navigator == null) && context.consumerState.navigator.CurrentNavType == NavType.Floor;
		};
		this.IsStanding = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsMoving";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_MOVING;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !(context.consumerState.consumer == null) && !(context.consumerState.navigator == null) && context.consumerState.navigator.IsMoving();
		};
		this.IsMoving = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsOffLadder";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_OFF_LADDER;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !(context.consumerState.consumer == null) && !(context.consumerState.navigator == null) && context.consumerState.navigator.CurrentNavType != NavType.Ladder && context.consumerState.navigator.CurrentNavType != NavType.Pole;
		};
		this.IsOffLadder = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "NotInTube";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.NOT_IN_TUBE;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return !(context.consumerState.consumer == null) && !(context.consumerState.navigator == null) && context.consumerState.navigator.CurrentNavType != NavType.Tube;
		};
		this.NotInTube = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "ConsumerHasTrait";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.HAS_TRAIT;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			string text = (string)data;
			Traits traits = context.consumerState.traits;
			return !(traits == null) && traits.HasTrait(text);
		};
		this.ConsumerHasTrait = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsOperational";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_OPERATIONAL;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return (data as Operational).IsOperational;
		};
		this.IsOperational = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsNotMarkedForDeconstruction";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_MARKED_FOR_DECONSTRUCTION;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Deconstructable deconstructable = data as Deconstructable;
			return deconstructable == null || !deconstructable.IsMarkedForDeconstruction();
		};
		this.IsNotMarkedForDeconstruction = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsNotMarkedForDisable";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_MARKED_FOR_DISABLE;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			BuildingEnabledButton buildingEnabledButton = data as BuildingEnabledButton;
			return buildingEnabledButton == null || (buildingEnabledButton.IsEnabled && !buildingEnabledButton.WaitingForDisable);
		};
		this.IsNotMarkedForDisable = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsFunctional";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_FUNCTIONAL;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return (data as Operational).IsFunctional;
		};
		this.IsFunctional = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsOverrideTargetNullOrMe";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_OVERRIDE_TARGET_NULL_OR_ME;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return context.isAttemptingOverride || context.chore.overrideTarget == null || context.chore.overrideTarget == context.consumerState.consumer;
		};
		this.IsOverrideTargetNullOrMe = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "NotChoreCreator";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.NOT_CHORE_CREATOR;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			GameObject gameObject = (GameObject)data;
			return !(context.consumerState.consumer == null) && !(context.consumerState.gameObject == gameObject);
		};
		this.NotChoreCreator = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsGettingMoreStressed";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_GETTING_MORE_STRESSED;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return Db.Get().Amounts.Stress.Lookup(context.consumerState.gameObject).GetDelta() > 0f;
		};
		this.IsGettingMoreStressed = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsAllowedByAutomation";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_ALLOWED_BY_AUTOMATION;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((Automatable)data).AllowedByAutomation(context.consumerState.hasSolidTransferArm);
		};
		this.IsAllowedByAutomation = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "HasTag";
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Tag tag = (Tag)data;
			return context.consumerState.prefabid.HasTag(tag);
		};
		this.HasTag = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "CheckBehaviourPrecondition";
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Tag tag2 = (Tag)data;
			return context.consumerState.consumer.RunBehaviourPrecondition(tag2);
		};
		this.CheckBehaviourPrecondition = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "CanDoWorkerPrioritizable";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_DO_RECREATION;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.consumerState.consumer == null)
			{
				return false;
			}
			IWorkerPrioritizable workerPrioritizable = data as IWorkerPrioritizable;
			if (workerPrioritizable == null)
			{
				return false;
			}
			int num6 = 0;
			if (workerPrioritizable.GetWorkerPriority(context.consumerState.worker, out num6))
			{
				context.consumerPriority += num6;
				return true;
			}
			return false;
		};
		this.CanDoWorkerPrioritizable = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsExclusivelyAvailableWithOtherChores";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.EXCLUSIVELY_AVAILABLE;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			foreach (Chore chore in ((List<Chore>)data))
			{
				if (chore != context.chore && chore.driver != null)
				{
					return false;
				}
			}
			return true;
		};
		this.IsExclusivelyAvailableWithOtherChores = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsBladderFull";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.BLADDER_FULL;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			BladderMonitor.Instance smi3 = context.consumerState.gameObject.GetSMI<BladderMonitor.Instance>();
			return smi3 != null && smi3.NeedsToPee();
		};
		this.IsBladderFull = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsBladderNotFull";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.BLADDER_NOT_FULL;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			BladderMonitor.Instance smi4 = context.consumerState.gameObject.GetSMI<BladderMonitor.Instance>();
			return smi4 == null || !smi4.NeedsToPee();
		};
		this.IsBladderNotFull = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "NoDeadBodies";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.NO_DEAD_BODIES;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return Components.LiveMinionIdentities.Count == Components.MinionIdentities.Count;
		};
		this.NoDeadBodies = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "NoRobots";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.NOT_A_ROBOT;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return context.consumerState.gameObject.GetComponent<MinionResume>() != null;
		};
		this.IsNotARobot = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "NotCurrentlyPeeing";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.CURRENTLY_PEEING;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			bool flag = true;
			Chore currentChore3 = context.consumerState.choreDriver.GetCurrentChore();
			if (currentChore3 != null)
			{
				string id = currentChore3.choreType.Id;
				flag = id != Db.Get().ChoreTypes.BreakPee.Id && id != Db.Get().ChoreTypes.Pee.Id;
			}
			return flag;
		};
		this.NotCurrentlyPeeing = precondition;
		precondition = default(Chore.Precondition);
		precondition.id = "IsRocketTravelling";
		precondition.description = DUPLICANTS.CHORES.PRECONDITIONS.IS_ROCKET_TRAVELLING;
		precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			Clustercraft component2 = ClusterManager.Instance.GetWorld(context.chore.gameObject.GetMyWorldId()).GetComponent<Clustercraft>();
			return !(component2 == null) && component2.IsTravellingAndFueled();
		};
		this.IsRocketTravelling = precondition;
		base..ctor();
	}

	// Token: 0x040009B9 RID: 2489
	private static ChorePreconditions _instance;

	// Token: 0x040009BA RID: 2490
	public Chore.Precondition IsPreemptable;

	// Token: 0x040009BB RID: 2491
	public Chore.Precondition HasUrge;

	// Token: 0x040009BC RID: 2492
	public Chore.Precondition IsValid;

	// Token: 0x040009BD RID: 2493
	public Chore.Precondition IsPermitted;

	// Token: 0x040009BE RID: 2494
	public Chore.Precondition IsAssignedtoMe;

	// Token: 0x040009BF RID: 2495
	public Chore.Precondition IsInMyWorld;

	// Token: 0x040009C0 RID: 2496
	public Chore.Precondition IsInMyParentWorld;

	// Token: 0x040009C1 RID: 2497
	public Chore.Precondition IsCellNotInMyWorld;

	// Token: 0x040009C2 RID: 2498
	public Chore.Precondition IsInMyRoom;

	// Token: 0x040009C3 RID: 2499
	public Chore.Precondition IsPreferredAssignable;

	// Token: 0x040009C4 RID: 2500
	public Chore.Precondition IsPreferredAssignableOrUrgentBladder;

	// Token: 0x040009C5 RID: 2501
	public Chore.Precondition IsNotTransferArm;

	// Token: 0x040009C6 RID: 2502
	public Chore.Precondition HasSkillPerk;

	// Token: 0x040009C7 RID: 2503
	public Chore.Precondition IsMinion;

	// Token: 0x040009C8 RID: 2504
	public Chore.Precondition IsMoreSatisfyingEarly;

	// Token: 0x040009C9 RID: 2505
	public Chore.Precondition IsMoreSatisfyingLate;

	// Token: 0x040009CA RID: 2506
	public Chore.Precondition IsChattable;

	// Token: 0x040009CB RID: 2507
	public Chore.Precondition IsNotRedAlert;

	// Token: 0x040009CC RID: 2508
	public Chore.Precondition IsScheduledTime;

	// Token: 0x040009CD RID: 2509
	public Chore.Precondition CanMoveTo;

	// Token: 0x040009CE RID: 2510
	public Chore.Precondition CanMoveToCell;

	// Token: 0x040009CF RID: 2511
	public Chore.Precondition CanPickup;

	// Token: 0x040009D0 RID: 2512
	public Chore.Precondition IsAwake;

	// Token: 0x040009D1 RID: 2513
	public Chore.Precondition IsStanding;

	// Token: 0x040009D2 RID: 2514
	public Chore.Precondition IsMoving;

	// Token: 0x040009D3 RID: 2515
	public Chore.Precondition IsOffLadder;

	// Token: 0x040009D4 RID: 2516
	public Chore.Precondition NotInTube;

	// Token: 0x040009D5 RID: 2517
	public Chore.Precondition ConsumerHasTrait;

	// Token: 0x040009D6 RID: 2518
	public Chore.Precondition IsOperational;

	// Token: 0x040009D7 RID: 2519
	public Chore.Precondition IsNotMarkedForDeconstruction;

	// Token: 0x040009D8 RID: 2520
	public Chore.Precondition IsNotMarkedForDisable;

	// Token: 0x040009D9 RID: 2521
	public Chore.Precondition IsFunctional;

	// Token: 0x040009DA RID: 2522
	public Chore.Precondition IsOverrideTargetNullOrMe;

	// Token: 0x040009DB RID: 2523
	public Chore.Precondition NotChoreCreator;

	// Token: 0x040009DC RID: 2524
	public Chore.Precondition IsGettingMoreStressed;

	// Token: 0x040009DD RID: 2525
	public Chore.Precondition IsAllowedByAutomation;

	// Token: 0x040009DE RID: 2526
	public Chore.Precondition HasTag;

	// Token: 0x040009DF RID: 2527
	public Chore.Precondition CheckBehaviourPrecondition;

	// Token: 0x040009E0 RID: 2528
	public Chore.Precondition CanDoWorkerPrioritizable;

	// Token: 0x040009E1 RID: 2529
	public Chore.Precondition IsExclusivelyAvailableWithOtherChores;

	// Token: 0x040009E2 RID: 2530
	public Chore.Precondition IsBladderFull;

	// Token: 0x040009E3 RID: 2531
	public Chore.Precondition IsBladderNotFull;

	// Token: 0x040009E4 RID: 2532
	public Chore.Precondition NoDeadBodies;

	// Token: 0x040009E5 RID: 2533
	public Chore.Precondition IsNotARobot;

	// Token: 0x040009E6 RID: 2534
	public Chore.Precondition NotCurrentlyPeeing;

	// Token: 0x040009E7 RID: 2535
	public Chore.Precondition IsRocketTravelling;
}
