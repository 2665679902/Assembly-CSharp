using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020006F2 RID: 1778
[AddComponentMenu("KMonoBehaviour/scripts/UprootedMonitor")]
public class UprootedMonitor : KMonoBehaviour
{
	// Token: 0x17000379 RID: 889
	// (get) Token: 0x06003062 RID: 12386 RVA: 0x000FF637 File Offset: 0x000FD837
	public bool IsUprooted
	{
		get
		{
			return this.uprooted || base.GetComponent<KPrefabID>().HasTag(GameTags.Uprooted);
		}
	}

	// Token: 0x06003063 RID: 12387 RVA: 0x000FF654 File Offset: 0x000FD854
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<UprootedMonitor>(-216549700, UprootedMonitor.OnUprootedDelegate);
		this.position = Grid.PosToCell(base.gameObject);
		foreach (CellOffset cellOffset in this.monitorCells)
		{
			int num = Grid.OffsetCell(this.position, cellOffset);
			if (Grid.IsValidCell(this.position) && Grid.IsValidCell(num))
			{
				this.partitionerEntries.Add(GameScenePartitioner.Instance.Add("UprootedMonitor.OnSpawn", base.gameObject, num, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnGroundChanged)));
			}
			this.OnGroundChanged(null);
		}
	}

	// Token: 0x06003064 RID: 12388 RVA: 0x000FF708 File Offset: 0x000FD908
	protected override void OnCleanUp()
	{
		foreach (HandleVector<int>.Handle handle in this.partitionerEntries)
		{
			GameScenePartitioner.Instance.Free(ref handle);
		}
		base.OnCleanUp();
	}

	// Token: 0x06003065 RID: 12389 RVA: 0x000FF768 File Offset: 0x000FD968
	public bool CheckTileGrowable()
	{
		return !this.canBeUprooted || (!this.uprooted && this.IsSuitableFoundation(this.position));
	}

	// Token: 0x06003066 RID: 12390 RVA: 0x000FF790 File Offset: 0x000FD990
	public bool IsSuitableFoundation(int cell)
	{
		bool flag = true;
		foreach (CellOffset cellOffset in this.monitorCells)
		{
			if (!Grid.IsCellOffsetValid(cell, cellOffset))
			{
				return false;
			}
			int num = Grid.OffsetCell(cell, cellOffset);
			flag = Grid.Solid[num];
			if (!flag)
			{
				break;
			}
		}
		return flag;
	}

	// Token: 0x06003067 RID: 12391 RVA: 0x000FF7E1 File Offset: 0x000FD9E1
	public void OnGroundChanged(object callbackData)
	{
		if (!this.CheckTileGrowable())
		{
			this.uprooted = true;
		}
		if (this.uprooted)
		{
			base.GetComponent<KPrefabID>().AddTag(GameTags.Uprooted, false);
			base.Trigger(-216549700, null);
		}
	}

	// Token: 0x04001D2A RID: 7466
	private int position;

	// Token: 0x04001D2B RID: 7467
	[Serialize]
	public bool canBeUprooted = true;

	// Token: 0x04001D2C RID: 7468
	[Serialize]
	private bool uprooted;

	// Token: 0x04001D2D RID: 7469
	public CellOffset[] monitorCells = new CellOffset[]
	{
		new CellOffset(0, -1)
	};

	// Token: 0x04001D2E RID: 7470
	private List<HandleVector<int>.Handle> partitionerEntries = new List<HandleVector<int>.Handle>();

	// Token: 0x04001D2F RID: 7471
	private static readonly EventSystem.IntraObjectHandler<UprootedMonitor> OnUprootedDelegate = new EventSystem.IntraObjectHandler<UprootedMonitor>(delegate(UprootedMonitor component, object data)
	{
		if (!component.uprooted)
		{
			component.GetComponent<KPrefabID>().AddTag(GameTags.Uprooted, false);
			component.uprooted = true;
			component.Trigger(-216549700, null);
		}
	});
}
