using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000500 RID: 1280
[AddComponentMenu("KMonoBehaviour/scripts/Uncoverable")]
public class Uncoverable : KMonoBehaviour
{
	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06001E34 RID: 7732 RVA: 0x000A1EC1 File Offset: 0x000A00C1
	public bool IsUncovered
	{
		get
		{
			return this.hasBeenUncovered;
		}
	}

	// Token: 0x06001E35 RID: 7733 RVA: 0x000A1ECC File Offset: 0x000A00CC
	private bool IsAnyCellShowing()
	{
		int num = Grid.PosToCell(this);
		return !this.occupyArea.TestArea(num, null, Uncoverable.IsCellBlockedDelegate);
	}

	// Token: 0x06001E36 RID: 7734 RVA: 0x000A1EF5 File Offset: 0x000A00F5
	private static bool IsCellBlocked(int cell, object data)
	{
		return Grid.Element[cell].IsSolid && !Grid.Foundation[cell];
	}

	// Token: 0x06001E37 RID: 7735 RVA: 0x000A1F15 File Offset: 0x000A0115
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06001E38 RID: 7736 RVA: 0x000A1F20 File Offset: 0x000A0120
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.IsAnyCellShowing())
		{
			this.hasBeenUncovered = true;
		}
		if (!this.hasBeenUncovered)
		{
			base.GetComponent<KSelectable>().IsSelectable = false;
			Extents extents = this.occupyArea.GetExtents();
			this.partitionerEntry = GameScenePartitioner.Instance.Add("Uncoverable.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		}
	}

	// Token: 0x06001E39 RID: 7737 RVA: 0x000A1F94 File Offset: 0x000A0194
	private void OnSolidChanged(object data)
	{
		if (this.IsAnyCellShowing() && !this.hasBeenUncovered && this.partitionerEntry.IsValid())
		{
			GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
			this.hasBeenUncovered = true;
			base.GetComponent<KSelectable>().IsSelectable = true;
			Notification notification = new Notification(MISC.STATUSITEMS.BURIEDITEM.NOTIFICATION, NotificationType.Good, new Func<List<Notification>, object, string>(Uncoverable.OnNotificationToolTip), this, true, 0f, null, null, null, true, false, false);
			base.gameObject.AddOrGet<Notifier>().Add(notification, "");
		}
	}

	// Token: 0x06001E3A RID: 7738 RVA: 0x000A2024 File Offset: 0x000A0224
	private static string OnNotificationToolTip(List<Notification> notifications, object data)
	{
		Uncoverable uncoverable = (Uncoverable)data;
		return MISC.STATUSITEMS.BURIEDITEM.NOTIFICATION_TOOLTIP.Replace("{Uncoverable}", uncoverable.GetProperName());
	}

	// Token: 0x06001E3B RID: 7739 RVA: 0x000A204D File Offset: 0x000A024D
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x040010E9 RID: 4329
	[MyCmpReq]
	private OccupyArea occupyArea;

	// Token: 0x040010EA RID: 4330
	[Serialize]
	private bool hasBeenUncovered;

	// Token: 0x040010EB RID: 4331
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x040010EC RID: 4332
	private static readonly Func<int, object, bool> IsCellBlockedDelegate = (int cell, object data) => Uncoverable.IsCellBlocked(cell, data);
}
