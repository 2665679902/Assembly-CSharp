using System;
using System.Collections.Generic;
using Klei.AI;
using ProcGen;
using STRINGS;
using UnityEngine;

// Token: 0x02000370 RID: 880
public class MinionBrain : Brain
{
	// Token: 0x06001202 RID: 4610 RVA: 0x0005EC9C File Offset: 0x0005CE9C
	public bool IsCellClear(int cell)
	{
		if (Grid.Reserved[cell])
		{
			return false;
		}
		GameObject gameObject = Grid.Objects[cell, 0];
		return !(gameObject != null) || !(base.gameObject != gameObject) || gameObject.GetComponent<Navigator>().IsMoving();
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x0005ECF2 File Offset: 0x0005CEF2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.Navigator.SetAbilities(new MinionPathFinderAbilities(this.Navigator));
		base.Subscribe<MinionBrain>(-1697596308, MinionBrain.AnimTrackStoredItemDelegate);
		base.Subscribe<MinionBrain>(-975551167, MinionBrain.OnUnstableGroundImpactDelegate);
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x0005ED34 File Offset: 0x0005CF34
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (GameObject gameObject in base.GetComponent<Storage>().items)
		{
			this.AddAnimTracker(gameObject);
		}
		Game.Instance.Subscribe(-107300940, new Action<object>(this.OnResearchComplete));
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x0005EDB0 File Offset: 0x0005CFB0
	private void AnimTrackStoredItem(object data)
	{
		Storage component = base.GetComponent<Storage>();
		GameObject gameObject = (GameObject)data;
		this.RemoveTracker(gameObject);
		if (component.items.Contains(gameObject))
		{
			this.AddAnimTracker(gameObject);
		}
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x0005EDE8 File Offset: 0x0005CFE8
	private void AddAnimTracker(GameObject go)
	{
		KAnimControllerBase component = go.GetComponent<KAnimControllerBase>();
		if (component == null)
		{
			return;
		}
		if (component.AnimFiles != null && component.AnimFiles.Length != 0 && component.AnimFiles[0] != null && component.GetComponent<Pickupable>().trackOnPickup)
		{
			KBatchedAnimTracker kbatchedAnimTracker = go.AddComponent<KBatchedAnimTracker>();
			kbatchedAnimTracker.useTargetPoint = false;
			kbatchedAnimTracker.fadeOut = false;
			kbatchedAnimTracker.symbol = new HashedString("snapTo_chest");
			kbatchedAnimTracker.forceAlwaysVisible = true;
		}
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x0005EE60 File Offset: 0x0005D060
	private void RemoveTracker(GameObject go)
	{
		KBatchedAnimTracker component = go.GetComponent<KBatchedAnimTracker>();
		if (component != null)
		{
			UnityEngine.Object.Destroy(component);
		}
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x0005EE84 File Offset: 0x0005D084
	public override void UpdateBrain()
	{
		base.UpdateBrain();
		if (Game.Instance == null)
		{
			return;
		}
		if (!Game.Instance.savedInfo.discoveredSurface)
		{
			int num = Grid.PosToCell(base.gameObject);
			if (global::World.Instance.zoneRenderData.GetSubWorldZoneType(num) == SubWorld.ZoneType.Space)
			{
				Game.Instance.savedInfo.discoveredSurface = true;
				DiscoveredSpaceMessage discoveredSpaceMessage = new DiscoveredSpaceMessage(base.gameObject.transform.GetPosition());
				Messenger.Instance.QueueMessage(discoveredSpaceMessage);
				Game.Instance.Trigger(-818188514, base.gameObject);
			}
		}
		if (!Game.Instance.savedInfo.discoveredOilField)
		{
			int num2 = Grid.PosToCell(base.gameObject);
			if (global::World.Instance.zoneRenderData.GetSubWorldZoneType(num2) == SubWorld.ZoneType.OilField)
			{
				Game.Instance.savedInfo.discoveredOilField = true;
			}
		}
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x0005EF5C File Offset: 0x0005D15C
	private void RegisterReactEmotePair(string reactable_id, Emote emote, float max_trigger_time)
	{
		if (base.gameObject == null)
		{
			return;
		}
		ReactionMonitor.Instance smi = base.gameObject.GetSMI<ReactionMonitor.Instance>();
		if (smi != null)
		{
			EmoteChore emoteChore = new EmoteChore(base.gameObject.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteIdle, emote, 1, null);
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.gameObject, reactable_id, Db.Get().ChoreTypes.Cough, max_trigger_time, 20f, float.PositiveInfinity, 0f);
			emoteChore.PairReactable(selfEmoteReactable);
			selfEmoteReactable.SetEmote(emote);
			selfEmoteReactable.PairEmote(emoteChore);
			smi.AddOneshotReactable(selfEmoteReactable);
		}
	}

	// Token: 0x0600120A RID: 4618 RVA: 0x0005EFF8 File Offset: 0x0005D1F8
	private void OnResearchComplete(object data)
	{
		if (Time.time - this.lastResearchCompleteEmoteTime > 1f)
		{
			this.RegisterReactEmotePair("ResearchComplete", Db.Get().Emotes.Minion.ResearchComplete, 3f);
			this.lastResearchCompleteEmoteTime = Time.time;
		}
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x0005F048 File Offset: 0x0005D248
	public Notification CreateCollapseNotification()
	{
		MinionIdentity component = base.GetComponent<MinionIdentity>();
		return new Notification(MISC.NOTIFICATIONS.TILECOLLAPSE.NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.TILECOLLAPSE.TOOLTIP + notificationList.ReduceMessages(false), "/t• " + component.GetProperName(), true, 0f, null, null, null, true, false, false);
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x0005F0A8 File Offset: 0x0005D2A8
	public void RemoveCollapseNotification(Notification notification)
	{
		Vector3 position = notification.clickFocus.GetPosition();
		position.z = -40f;
		WorldContainer myWorld = notification.clickFocus.gameObject.GetMyWorld();
		if (myWorld != null && myWorld.IsDiscovered)
		{
			CameraController.Instance.ActiveWorldStarWipe(myWorld.id, position, 10f, null);
		}
		base.gameObject.AddOrGet<Notifier>().Remove(notification);
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x0005F118 File Offset: 0x0005D318
	private void OnUnstableGroundImpact(object data)
	{
		GameObject telepad = GameUtil.GetTelepad(base.gameObject.GetMyWorld().id);
		Navigator component = base.GetComponent<Navigator>();
		Assignable assignable = base.GetComponent<MinionIdentity>().GetSoleOwner().GetAssignable(Db.Get().AssignableSlots.Bed);
		bool flag = assignable != null && component.CanReach(Grid.PosToCell(assignable.transform.GetPosition()));
		bool flag2 = telepad != null && component.CanReach(Grid.PosToCell(telepad.transform.GetPosition()));
		if (!flag && !flag2)
		{
			this.RegisterReactEmotePair("UnstableGroundShock", Db.Get().Emotes.Minion.Shock, 1f);
			Notification notification = this.CreateCollapseNotification();
			notification.customClickCallback = delegate(object o)
			{
				this.RemoveCollapseNotification(notification);
			};
			base.gameObject.AddOrGet<Notifier>().Add(notification, "");
		}
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x0005F21D File Offset: 0x0005D41D
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Game.Instance.Unsubscribe(-107300940, new Action<object>(this.OnResearchComplete));
	}

	// Token: 0x040009A7 RID: 2471
	[MyCmpReq]
	public Navigator Navigator;

	// Token: 0x040009A8 RID: 2472
	[MyCmpGet]
	public OxygenBreather OxygenBreather;

	// Token: 0x040009A9 RID: 2473
	private float lastResearchCompleteEmoteTime;

	// Token: 0x040009AA RID: 2474
	private static readonly EventSystem.IntraObjectHandler<MinionBrain> AnimTrackStoredItemDelegate = new EventSystem.IntraObjectHandler<MinionBrain>(delegate(MinionBrain component, object data)
	{
		component.AnimTrackStoredItem(data);
	});

	// Token: 0x040009AB RID: 2475
	private static readonly EventSystem.IntraObjectHandler<MinionBrain> OnUnstableGroundImpactDelegate = new EventSystem.IntraObjectHandler<MinionBrain>(delegate(MinionBrain component, object data)
	{
		component.OnUnstableGroundImpact(data);
	});
}
