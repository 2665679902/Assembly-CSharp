using System;
using ProcGen;
using UnityEngine;

// Token: 0x020009D9 RID: 2521
[AddComponentMenu("KMonoBehaviour/scripts/ZoneTile")]
public class ZoneTile : KMonoBehaviour
{
	// Token: 0x06004B57 RID: 19287 RVA: 0x001A6078 File Offset: 0x001A4278
	protected override void OnSpawn()
	{
		int[] placementCells = this.building.PlacementCells;
		for (int i = 0; i < placementCells.Length; i++)
		{
			SimMessages.ModifyCellWorldZone(placementCells[i], 0);
		}
		base.Subscribe<ZoneTile>(1606648047, ZoneTile.OnObjectReplacedDelegate);
	}

	// Token: 0x06004B58 RID: 19288 RVA: 0x001A60B9 File Offset: 0x001A42B9
	protected override void OnCleanUp()
	{
		if (!this.wasReplaced)
		{
			this.ClearZone();
		}
	}

	// Token: 0x06004B59 RID: 19289 RVA: 0x001A60C9 File Offset: 0x001A42C9
	private void OnObjectReplaced(object data)
	{
		this.ClearZone();
		this.wasReplaced = true;
	}

	// Token: 0x06004B5A RID: 19290 RVA: 0x001A60D8 File Offset: 0x001A42D8
	private void ClearZone()
	{
		foreach (int num in this.building.PlacementCells)
		{
			SubWorld.ZoneType subWorldZoneType = global::World.Instance.zoneRenderData.GetSubWorldZoneType(num);
			byte b = ((subWorldZoneType == SubWorld.ZoneType.Space) ? byte.MaxValue : ((byte)subWorldZoneType));
			SimMessages.ModifyCellWorldZone(num, b);
		}
	}

	// Token: 0x0400315B RID: 12635
	[MyCmpReq]
	public Building building;

	// Token: 0x0400315C RID: 12636
	private bool wasReplaced;

	// Token: 0x0400315D RID: 12637
	private static readonly EventSystem.IntraObjectHandler<ZoneTile> OnObjectReplacedDelegate = new EventSystem.IntraObjectHandler<ZoneTile>(delegate(ZoneTile component, object data)
	{
		component.OnObjectReplaced(data);
	});
}
