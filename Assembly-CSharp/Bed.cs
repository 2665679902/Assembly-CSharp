using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x0200057B RID: 1403
[AddComponentMenu("KMonoBehaviour/Workable/Bed")]
public class Bed : Workable, IGameObjectEffectDescriptor, IBasicBuilding
{
	// Token: 0x06002223 RID: 8739 RVA: 0x000B931A File Offset: 0x000B751A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = false;
	}

	// Token: 0x06002224 RID: 8740 RVA: 0x000B932C File Offset: 0x000B752C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.BasicBuildings.Add(this);
		this.sleepable = base.GetComponent<Sleepable>();
		Sleepable sleepable = this.sleepable;
		sleepable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(sleepable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkableEvent));
	}

	// Token: 0x06002225 RID: 8741 RVA: 0x000B937D File Offset: 0x000B757D
	private void OnWorkableEvent(Workable workable, Workable.WorkableEvent workable_event)
	{
		if (workable_event == Workable.WorkableEvent.WorkStarted)
		{
			this.AddEffects();
			return;
		}
		if (workable_event == Workable.WorkableEvent.WorkStopped)
		{
			this.RemoveEffects();
		}
	}

	// Token: 0x06002226 RID: 8742 RVA: 0x000B9394 File Offset: 0x000B7594
	private void AddEffects()
	{
		this.targetWorker = this.sleepable.worker;
		if (this.effects != null)
		{
			foreach (string text in this.effects)
			{
				this.targetWorker.GetComponent<Effects>().Add(text, false);
			}
		}
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject == null)
		{
			return;
		}
		RoomType roomType = roomOfGameObject.roomType;
		foreach (KeyValuePair<string, string> keyValuePair in Bed.roomSleepingEffects)
		{
			if (keyValuePair.Key == roomType.Id)
			{
				this.targetWorker.GetComponent<Effects>().Add(keyValuePair.Value, false);
			}
		}
		roomType.TriggerRoomEffects(base.GetComponent<KPrefabID>(), this.targetWorker.GetComponent<Effects>());
	}

	// Token: 0x06002227 RID: 8743 RVA: 0x000B9490 File Offset: 0x000B7690
	private void RemoveEffects()
	{
		if (this.targetWorker == null)
		{
			return;
		}
		if (this.effects != null)
		{
			foreach (string text in this.effects)
			{
				this.targetWorker.GetComponent<Effects>().Remove(text);
			}
		}
		foreach (KeyValuePair<string, string> keyValuePair in Bed.roomSleepingEffects)
		{
			this.targetWorker.GetComponent<Effects>().Remove(keyValuePair.Value);
		}
		this.targetWorker = null;
	}

	// Token: 0x06002228 RID: 8744 RVA: 0x000B953C File Offset: 0x000B773C
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.effects != null)
		{
			foreach (string text in this.effects)
			{
				if (text != null && text != "")
				{
					Effect.AddModifierDescriptions(base.gameObject, list, text, false);
				}
			}
		}
		return list;
	}

	// Token: 0x06002229 RID: 8745 RVA: 0x000B9590 File Offset: 0x000B7790
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.BasicBuildings.Remove(this);
		if (this.sleepable != null)
		{
			Sleepable sleepable = this.sleepable;
			sleepable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(sleepable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkableEvent));
		}
	}

	// Token: 0x040013B7 RID: 5047
	[MyCmpReq]
	private Sleepable sleepable;

	// Token: 0x040013B8 RID: 5048
	private Worker targetWorker;

	// Token: 0x040013B9 RID: 5049
	public string[] effects;

	// Token: 0x040013BA RID: 5050
	private static Dictionary<string, string> roomSleepingEffects = new Dictionary<string, string>
	{
		{ "Barracks", "BarracksStamina" },
		{ "Luxury Barracks", "BarracksStamina" },
		{ "Private Bedroom", "BedroomStamina" }
	};
}
