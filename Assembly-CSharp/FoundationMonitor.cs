using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x020006D8 RID: 1752
[AddComponentMenu("KMonoBehaviour/scripts/FoundationMonitor")]
public class FoundationMonitor : KMonoBehaviour
{
	// Token: 0x06002F9F RID: 12191 RVA: 0x000FBA94 File Offset: 0x000F9C94
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.position = Grid.PosToCell(base.gameObject);
		foreach (CellOffset cellOffset in this.monitorCells)
		{
			int num = Grid.OffsetCell(this.position, cellOffset);
			if (Grid.IsValidCell(this.position) && Grid.IsValidCell(num))
			{
				this.partitionerEntries.Add(GameScenePartitioner.Instance.Add("FoundationMonitor.OnSpawn", base.gameObject, num, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnGroundChanged)));
			}
			this.OnGroundChanged(null);
		}
	}

	// Token: 0x06002FA0 RID: 12192 RVA: 0x000FBB38 File Offset: 0x000F9D38
	protected override void OnCleanUp()
	{
		foreach (HandleVector<int>.Handle handle in this.partitionerEntries)
		{
			GameScenePartitioner.Instance.Free(ref handle);
		}
		base.OnCleanUp();
	}

	// Token: 0x06002FA1 RID: 12193 RVA: 0x000FBB98 File Offset: 0x000F9D98
	public bool CheckFoundationValid()
	{
		return !this.needsFoundation || this.IsSuitableFoundation(this.position);
	}

	// Token: 0x06002FA2 RID: 12194 RVA: 0x000FBBB0 File Offset: 0x000F9DB0
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

	// Token: 0x06002FA3 RID: 12195 RVA: 0x000FBC04 File Offset: 0x000F9E04
	public void OnGroundChanged(object callbackData)
	{
		if (!this.hasFoundation && this.CheckFoundationValid())
		{
			this.hasFoundation = true;
			base.GetComponent<KPrefabID>().RemoveTag(GameTags.Creatures.HasNoFoundation);
			base.Trigger(-1960061727, null);
		}
		if (this.hasFoundation && !this.CheckFoundationValid())
		{
			this.hasFoundation = false;
			base.GetComponent<KPrefabID>().AddTag(GameTags.Creatures.HasNoFoundation, false);
			base.Trigger(-1960061727, null);
		}
	}

	// Token: 0x04001CAE RID: 7342
	private int position;

	// Token: 0x04001CAF RID: 7343
	[Serialize]
	public bool needsFoundation = true;

	// Token: 0x04001CB0 RID: 7344
	[Serialize]
	private bool hasFoundation = true;

	// Token: 0x04001CB1 RID: 7345
	public CellOffset[] monitorCells = new CellOffset[]
	{
		new CellOffset(0, -1)
	};

	// Token: 0x04001CB2 RID: 7346
	private List<HandleVector<int>.Handle> partitionerEntries = new List<HandleVector<int>.Handle>();
}
