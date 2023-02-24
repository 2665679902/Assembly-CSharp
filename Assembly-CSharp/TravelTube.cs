using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000661 RID: 1633
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/TravelTube")]
public class TravelTube : KMonoBehaviour, IFirstFrameCallback, ITravelTubePiece, IHaveUtilityNetworkMgr
{
	// Token: 0x06002BD4 RID: 11220 RVA: 0x000E63B8 File Offset: 0x000E45B8
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.travelTubeSystem;
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000E63C4 File Offset: 0x000E45C4
	public Vector3 Position
	{
		get
		{
			return base.transform.GetPosition();
		}
	}

	// Token: 0x06002BD6 RID: 11222 RVA: 0x000E63D1 File Offset: 0x000E45D1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Grid.HasTube[Grid.PosToCell(this)] = true;
		Components.ITravelTubePieces.Add(this);
	}

	// Token: 0x06002BD7 RID: 11223 RVA: 0x000E63F8 File Offset: 0x000E45F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		int num = Grid.PosToCell(base.transform.GetPosition());
		Game.Instance.travelTubeSystem.AddToNetworks(num, this, false);
		base.Subscribe<TravelTube>(-1041684577, TravelTube.OnConnectionsChangedDelegate);
	}

	// Token: 0x06002BD8 RID: 11224 RVA: 0x000E6440 File Offset: 0x000E4640
	protected override void OnCleanUp()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.Def.ReplacementLayer == ObjectLayer.NumLayers || Grid.Objects[num, (int)component.Def.ReplacementLayer] == null)
		{
			Game.Instance.travelTubeSystem.RemoveFromNetworks(num, this, false);
		}
		base.Unsubscribe(-1041684577);
		Grid.HasTube[Grid.PosToCell(this)] = false;
		Components.ITravelTubePieces.Remove(this);
		GameScenePartitioner.Instance.Free(ref this.dirtyNavCellUpdatedEntry);
		base.OnCleanUp();
	}

	// Token: 0x06002BD9 RID: 11225 RVA: 0x000E64E4 File Offset: 0x000E46E4
	private void OnConnectionsChanged(object data)
	{
		this.connections = (UtilityConnections)data;
		bool flag = this.connections == UtilityConnections.Up || this.connections == UtilityConnections.Down || this.connections == UtilityConnections.Left || this.connections == UtilityConnections.Right;
		if (flag != this.isExitTube)
		{
			this.isExitTube = flag;
			this.UpdateExitListener(this.isExitTube);
			this.UpdateExitStatus();
		}
	}

	// Token: 0x06002BDA RID: 11226 RVA: 0x000E6548 File Offset: 0x000E4748
	private void UpdateExitListener(bool enable)
	{
		if (enable && !this.dirtyNavCellUpdatedEntry.IsValid())
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.dirtyNavCellUpdatedEntry = GameScenePartitioner.Instance.Add("TravelTube.OnDirtyNavCellUpdated", this, num, GameScenePartitioner.Instance.dirtyNavCellUpdateLayer, new Action<object>(this.OnDirtyNavCellUpdated));
			this.OnDirtyNavCellUpdated(null);
			return;
		}
		if (!enable && this.dirtyNavCellUpdatedEntry.IsValid())
		{
			GameScenePartitioner.Instance.Free(ref this.dirtyNavCellUpdatedEntry);
		}
	}

	// Token: 0x06002BDB RID: 11227 RVA: 0x000E65CC File Offset: 0x000E47CC
	private void OnDirtyNavCellUpdated(object data)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		NavGrid navGrid = Pathfinding.Instance.GetNavGrid("MinionNavGrid");
		int num2 = num * navGrid.maxLinksPerCell;
		bool flag = false;
		if (this.isExitTube)
		{
			NavGrid.Link link = navGrid.Links[num2];
			while (link.link != PathFinder.InvalidHandle)
			{
				if (link.startNavType == NavType.Tube)
				{
					if (link.endNavType != NavType.Tube)
					{
						flag = true;
						break;
					}
					UtilityConnections utilityConnections = UtilityConnectionsExtensions.DirectionFromToCell(link.link, num);
					if (this.connections == utilityConnections)
					{
						flag = true;
						break;
					}
				}
				num2++;
				link = navGrid.Links[num2];
			}
		}
		if (flag != this.hasValidExitTransitions)
		{
			this.hasValidExitTransitions = flag;
			this.UpdateExitStatus();
		}
	}

	// Token: 0x06002BDC RID: 11228 RVA: 0x000E6688 File Offset: 0x000E4888
	private void UpdateExitStatus()
	{
		if (!this.isExitTube || this.hasValidExitTransitions)
		{
			this.connectedStatus = this.selectable.RemoveStatusItem(this.connectedStatus, false);
			return;
		}
		if (this.connectedStatus == Guid.Empty)
		{
			this.connectedStatus = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.NoTubeExits, null);
		}
	}

	// Token: 0x06002BDD RID: 11229 RVA: 0x000E66F1 File Offset: 0x000E48F1
	public void SetFirstFrameCallback(System.Action ffCb)
	{
		this.firstFrameCallback = ffCb;
		base.StartCoroutine(this.RunCallback());
	}

	// Token: 0x06002BDE RID: 11230 RVA: 0x000E6707 File Offset: 0x000E4907
	private IEnumerator RunCallback()
	{
		yield return null;
		if (this.firstFrameCallback != null)
		{
			this.firstFrameCallback();
			this.firstFrameCallback = null;
		}
		yield return null;
		yield break;
	}

	// Token: 0x040019EF RID: 6639
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x040019F0 RID: 6640
	private HandleVector<int>.Handle dirtyNavCellUpdatedEntry;

	// Token: 0x040019F1 RID: 6641
	private bool isExitTube;

	// Token: 0x040019F2 RID: 6642
	private bool hasValidExitTransitions;

	// Token: 0x040019F3 RID: 6643
	private UtilityConnections connections;

	// Token: 0x040019F4 RID: 6644
	private static readonly EventSystem.IntraObjectHandler<TravelTube> OnConnectionsChangedDelegate = new EventSystem.IntraObjectHandler<TravelTube>(delegate(TravelTube component, object data)
	{
		component.OnConnectionsChanged(data);
	});

	// Token: 0x040019F5 RID: 6645
	private Guid connectedStatus;

	// Token: 0x040019F6 RID: 6646
	private System.Action firstFrameCallback;
}
