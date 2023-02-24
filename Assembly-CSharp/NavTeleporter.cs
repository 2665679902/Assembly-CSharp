using System;

// Token: 0x02000616 RID: 1558
public class NavTeleporter : KMonoBehaviour
{
	// Token: 0x060028AF RID: 10415 RVA: 0x000D7DE4 File Offset: 0x000D5FE4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.GetComponent<KPrefabID>().AddTag(GameTags.NavTeleporters, false);
		this.Register();
		Singleton<CellChangeMonitor>.Instance.RegisterCellChangedHandler(base.transform, new System.Action(this.OnCellChanged), "NavTeleporterCellChanged");
	}

	// Token: 0x060028B0 RID: 10416 RVA: 0x000D7E30 File Offset: 0x000D6030
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		int cell = this.GetCell();
		if (cell != Grid.InvalidCell)
		{
			Grid.HasNavTeleporter[cell] = false;
		}
		this.Deregister();
		Components.NavTeleporters.Remove(this);
	}

	// Token: 0x060028B1 RID: 10417 RVA: 0x000D7E6F File Offset: 0x000D606F
	public void SetOverrideCell(int cell)
	{
		this.overrideCell = cell;
	}

	// Token: 0x060028B2 RID: 10418 RVA: 0x000D7E78 File Offset: 0x000D6078
	public int GetCell()
	{
		if (this.overrideCell >= 0)
		{
			return this.overrideCell;
		}
		return Grid.OffsetCell(Grid.PosToCell(this), this.offset);
	}

	// Token: 0x060028B3 RID: 10419 RVA: 0x000D7E9C File Offset: 0x000D609C
	public void TwoWayTarget(NavTeleporter nt)
	{
		if (this.target != null)
		{
			if (nt != null)
			{
				nt.SetTarget(null);
			}
			this.BreakLink();
		}
		this.target = nt;
		if (this.target != null)
		{
			this.SetLink();
			if (nt != null)
			{
				nt.SetTarget(this);
			}
		}
	}

	// Token: 0x060028B4 RID: 10420 RVA: 0x000D7EF8 File Offset: 0x000D60F8
	public void EnableTwoWayTarget(bool enable)
	{
		if (enable)
		{
			this.target.SetLink();
			this.SetLink();
			return;
		}
		this.target.BreakLink();
		this.BreakLink();
	}

	// Token: 0x060028B5 RID: 10421 RVA: 0x000D7F20 File Offset: 0x000D6120
	public void SetTarget(NavTeleporter nt)
	{
		if (this.target != null)
		{
			this.BreakLink();
		}
		this.target = nt;
		if (this.target != null)
		{
			this.SetLink();
		}
	}

	// Token: 0x060028B6 RID: 10422 RVA: 0x000D7F54 File Offset: 0x000D6154
	private void Register()
	{
		int cell = this.GetCell();
		if (!Grid.IsValidCell(cell))
		{
			this.lastRegisteredCell = Grid.InvalidCell;
			return;
		}
		Grid.HasNavTeleporter[cell] = true;
		Pathfinding.Instance.AddDirtyNavGridCell(cell);
		this.lastRegisteredCell = cell;
		if (this.target != null)
		{
			this.SetLink();
		}
	}

	// Token: 0x060028B7 RID: 10423 RVA: 0x000D7FB0 File Offset: 0x000D61B0
	private void SetLink()
	{
		int cell = this.target.GetCell();
		Pathfinding.Instance.GetNavGrid(MinionConfig.MINION_NAV_GRID_NAME).teleportTransitions[this.lastRegisteredCell] = cell;
		Pathfinding.Instance.AddDirtyNavGridCell(this.lastRegisteredCell);
	}

	// Token: 0x060028B8 RID: 10424 RVA: 0x000D7FFC File Offset: 0x000D61FC
	private void Deregister()
	{
		if (this.lastRegisteredCell != Grid.InvalidCell)
		{
			this.BreakLink();
			Grid.HasNavTeleporter[this.lastRegisteredCell] = false;
			Pathfinding.Instance.AddDirtyNavGridCell(this.lastRegisteredCell);
			this.lastRegisteredCell = Grid.InvalidCell;
		}
	}

	// Token: 0x060028B9 RID: 10425 RVA: 0x000D8048 File Offset: 0x000D6248
	private void BreakLink()
	{
		Pathfinding.Instance.GetNavGrid(MinionConfig.MINION_NAV_GRID_NAME).teleportTransitions.Remove(this.lastRegisteredCell);
		Pathfinding.Instance.AddDirtyNavGridCell(this.lastRegisteredCell);
	}

	// Token: 0x060028BA RID: 10426 RVA: 0x000D807C File Offset: 0x000D627C
	private void OnCellChanged()
	{
		this.Deregister();
		this.Register();
		if (this.target != null)
		{
			NavTeleporter component = this.target.GetComponent<NavTeleporter>();
			if (component != null)
			{
				component.SetTarget(this);
			}
		}
	}

	// Token: 0x040017EA RID: 6122
	private NavTeleporter target;

	// Token: 0x040017EB RID: 6123
	private int lastRegisteredCell = Grid.InvalidCell;

	// Token: 0x040017EC RID: 6124
	public CellOffset offset;

	// Token: 0x040017ED RID: 6125
	private int overrideCell = -1;
}
