using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020004C0 RID: 1216
[AddComponentMenu("KMonoBehaviour/scripts/RoomTracker")]
public class RoomTracker : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00095968 File Offset: 0x00093B68
	// (set) Token: 0x06001C24 RID: 7204 RVA: 0x00095970 File Offset: 0x00093B70
	public Room room { get; private set; }

	// Token: 0x06001C25 RID: 7205 RVA: 0x0009597C File Offset: 0x00093B7C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		global::Debug.Assert(!string.IsNullOrEmpty(this.requiredRoomType) && this.requiredRoomType != Db.Get().RoomTypes.Neutral.Id, "RoomTracker must have a requiredRoomType!");
		base.Subscribe<RoomTracker>(144050788, RoomTracker.OnUpdateRoomDelegate);
		this.FindAndSetRoom();
	}

	// Token: 0x06001C26 RID: 7206 RVA: 0x000959E0 File Offset: 0x00093BE0
	public void FindAndSetRoom()
	{
		CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(base.gameObject));
		if (cavityForCell != null && cavityForCell.room != null)
		{
			this.OnUpdateRoom(cavityForCell.room);
			return;
		}
		this.OnUpdateRoom(null);
	}

	// Token: 0x06001C27 RID: 7207 RVA: 0x00095A27 File Offset: 0x00093C27
	public bool IsInCorrectRoom()
	{
		return this.room != null && this.room.roomType.Id == this.requiredRoomType;
	}

	// Token: 0x06001C28 RID: 7208 RVA: 0x00095A50 File Offset: 0x00093C50
	public bool SufficientBuildLocation(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		if (this.requirement == RoomTracker.Requirement.Required || this.requirement == RoomTracker.Requirement.CustomRequired)
		{
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(cell);
			if (((cavityForCell != null) ? cavityForCell.room : null) == null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x00095A9C File Offset: 0x00093C9C
	private void OnUpdateRoom(object data)
	{
		this.room = (Room)data;
		if (this.room != null && !(this.room.roomType.Id != this.requiredRoomType))
		{
			this.statusItemGuid = base.GetComponent<KSelectable>().RemoveStatusItem(this.statusItemGuid, false);
			return;
		}
		switch (this.requirement)
		{
		case RoomTracker.Requirement.TrackingOnly:
			this.statusItemGuid = base.GetComponent<KSelectable>().RemoveStatusItem(this.statusItemGuid, false);
			return;
		case RoomTracker.Requirement.Recommended:
			this.statusItemGuid = base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.RequiredRoom, Db.Get().BuildingStatusItems.NotInRecommendedRoom, this.requiredRoomType);
			return;
		case RoomTracker.Requirement.Required:
			this.statusItemGuid = base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.RequiredRoom, Db.Get().BuildingStatusItems.NotInRequiredRoom, this.requiredRoomType);
			return;
		case RoomTracker.Requirement.CustomRecommended:
		case RoomTracker.Requirement.CustomRequired:
			this.statusItemGuid = base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.RequiredRoom, Db.Get().BuildingStatusItems.Get(this.customStatusItemID), this.requiredRoomType);
			return;
		default:
			return;
		}
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x00095BD8 File Offset: 0x00093DD8
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (!string.IsNullOrEmpty(this.requiredRoomType))
		{
			string name = Db.Get().RoomTypes.Get(this.requiredRoomType).Name;
			switch (this.requirement)
			{
			case RoomTracker.Requirement.Recommended:
			case RoomTracker.Requirement.CustomRecommended:
				list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.PREFERS_ROOM, name), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.PREFERS_ROOM, name), Descriptor.DescriptorType.Requirement, false));
				break;
			case RoomTracker.Requirement.Required:
			case RoomTracker.Requirement.CustomRequired:
				list.Add(new Descriptor(string.Format(UI.BUILDINGEFFECTS.REQUIRESROOM, name), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.REQUIRESROOM, name), Descriptor.DescriptorType.Requirement, false));
				break;
			}
		}
		return list;
	}

	// Token: 0x04000FB8 RID: 4024
	public RoomTracker.Requirement requirement;

	// Token: 0x04000FB9 RID: 4025
	public string requiredRoomType;

	// Token: 0x04000FBA RID: 4026
	public string customStatusItemID;

	// Token: 0x04000FBB RID: 4027
	private Guid statusItemGuid;

	// Token: 0x04000FBD RID: 4029
	private static readonly EventSystem.IntraObjectHandler<RoomTracker> OnUpdateRoomDelegate = new EventSystem.IntraObjectHandler<RoomTracker>(delegate(RoomTracker component, object data)
	{
		component.OnUpdateRoom(data);
	});

	// Token: 0x02001106 RID: 4358
	public enum Requirement
	{
		// Token: 0x04005984 RID: 22916
		TrackingOnly,
		// Token: 0x04005985 RID: 22917
		Recommended,
		// Token: 0x04005986 RID: 22918
		Required,
		// Token: 0x04005987 RID: 22919
		CustomRecommended,
		// Token: 0x04005988 RID: 22920
		CustomRequired
	}
}
