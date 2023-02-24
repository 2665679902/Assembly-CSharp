using System;
using KSerialization;
using STRINGS;

// Token: 0x020006D4 RID: 1748
public class EntombVulnerable : KMonoBehaviour, IWiltCause
{
	// Token: 0x17000358 RID: 856
	// (get) Token: 0x06002F87 RID: 12167 RVA: 0x000FB1DD File Offset: 0x000F93DD
	private OccupyArea occupyArea
	{
		get
		{
			if (this._occupyArea == null)
			{
				this._occupyArea = base.GetComponent<OccupyArea>();
			}
			return this._occupyArea;
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06002F88 RID: 12168 RVA: 0x000FB1FF File Offset: 0x000F93FF
	public bool GetEntombed
	{
		get
		{
			return this.isEntombed;
		}
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x06002F89 RID: 12169 RVA: 0x000FB207 File Offset: 0x000F9407
	public string WiltStateString
	{
		get
		{
			return Db.Get().CreatureStatusItems.Entombed.resolveStringCallback(CREATURES.STATUSITEMS.ENTOMBED.LINE_ITEM, base.gameObject);
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x06002F8A RID: 12170 RVA: 0x000FB232 File Offset: 0x000F9432
	public WiltCondition.Condition[] Conditions
	{
		get
		{
			return new WiltCondition.Condition[] { WiltCondition.Condition.Entombed };
		}
	}

	// Token: 0x06002F8B RID: 12171 RVA: 0x000FB240 File Offset: 0x000F9440
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.partitionerEntry = GameScenePartitioner.Instance.Add("EntombVulnerable", base.gameObject, this.occupyArea.GetExtents(), GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		this.CheckEntombed();
		if (this.isEntombed)
		{
			base.GetComponent<KPrefabID>().AddTag(GameTags.Entombed, false);
			base.Trigger(-1089732772, true);
		}
	}

	// Token: 0x06002F8C RID: 12172 RVA: 0x000FB2BF File Offset: 0x000F94BF
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002F8D RID: 12173 RVA: 0x000FB2D7 File Offset: 0x000F94D7
	private void OnSolidChanged(object data)
	{
		this.CheckEntombed();
	}

	// Token: 0x06002F8E RID: 12174 RVA: 0x000FB2E0 File Offset: 0x000F94E0
	private void CheckEntombed()
	{
		int num = Grid.PosToCell(base.gameObject.transform.GetPosition());
		if (!Grid.IsValidCell(num))
		{
			return;
		}
		if (!this.IsCellSafe(num))
		{
			if (!this.isEntombed)
			{
				this.isEntombed = true;
				this.selectable.AddStatusItem(Db.Get().CreatureStatusItems.Entombed, base.gameObject);
				base.GetComponent<KPrefabID>().AddTag(GameTags.Entombed, false);
				base.Trigger(-1089732772, true);
				return;
			}
		}
		else if (this.isEntombed)
		{
			this.isEntombed = false;
			this.selectable.RemoveStatusItem(Db.Get().CreatureStatusItems.Entombed, false);
			base.GetComponent<KPrefabID>().RemoveTag(GameTags.Entombed);
			base.Trigger(-1089732772, false);
		}
	}

	// Token: 0x06002F8F RID: 12175 RVA: 0x000FB3B7 File Offset: 0x000F95B7
	public bool IsCellSafe(int cell)
	{
		return this.occupyArea.TestArea(cell, null, EntombVulnerable.IsCellSafeCBDelegate);
	}

	// Token: 0x06002F90 RID: 12176 RVA: 0x000FB3CB File Offset: 0x000F95CB
	private static bool IsCellSafeCB(int cell, object data)
	{
		return Grid.IsValidCell(cell) && !Grid.Solid[cell];
	}

	// Token: 0x04001C9D RID: 7325
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001C9E RID: 7326
	private OccupyArea _occupyArea;

	// Token: 0x04001C9F RID: 7327
	[Serialize]
	private bool isEntombed;

	// Token: 0x04001CA0 RID: 7328
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001CA1 RID: 7329
	private static readonly Func<int, object, bool> IsCellSafeCBDelegate = (int cell, object data) => EntombVulnerable.IsCellSafeCB(cell, data);
}
