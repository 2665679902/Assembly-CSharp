using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200048B RID: 1163
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/KAnimGraphTileVisualizer")]
public class KAnimGraphTileVisualizer : KMonoBehaviour, ISaveLoadable, IUtilityItem
{
	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060019F7 RID: 6647 RVA: 0x0008AEAF File Offset: 0x000890AF
	// (set) Token: 0x060019F8 RID: 6648 RVA: 0x0008AEB7 File Offset: 0x000890B7
	public UtilityConnections Connections
	{
		get
		{
			return this._connections;
		}
		set
		{
			this._connections = value;
			base.Trigger(-1041684577, this._connections);
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0008AED8 File Offset: 0x000890D8
	public IUtilityNetworkMgr ConnectionManager
	{
		get
		{
			switch (this.connectionSource)
			{
			case KAnimGraphTileVisualizer.ConnectionSource.Gas:
				return Game.Instance.gasConduitSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Liquid:
				return Game.Instance.liquidConduitSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Electrical:
				return Game.Instance.electricalConduitSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Logic:
				return Game.Instance.logicCircuitSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Tube:
				return Game.Instance.travelTubeSystem;
			case KAnimGraphTileVisualizer.ConnectionSource.Solid:
				return Game.Instance.solidConduitSystem;
			default:
				return null;
			}
		}
	}

	// Token: 0x060019FA RID: 6650 RVA: 0x0008AF50 File Offset: 0x00089150
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.connectionManager = this.ConnectionManager;
		int num = Grid.PosToCell(base.transform.GetPosition());
		this.connectionManager.SetConnections(this.Connections, num, this.isPhysicalBuilding);
		Building component = base.GetComponent<Building>();
		TileVisualizer.RefreshCell(num, component.Def.TileLayer, component.Def.ReplacementLayer);
	}

	// Token: 0x060019FB RID: 6651 RVA: 0x0008AFBC File Offset: 0x000891BC
	protected override void OnCleanUp()
	{
		if (this.connectionManager != null && !this.skipCleanup)
		{
			this.skipRefresh = true;
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.connectionManager.ClearCell(num, this.isPhysicalBuilding);
			Building component = base.GetComponent<Building>();
			TileVisualizer.RefreshCell(num, component.Def.TileLayer, component.Def.ReplacementLayer);
		}
	}

	// Token: 0x060019FC RID: 6652 RVA: 0x0008B028 File Offset: 0x00089228
	[ContextMenu("Refresh")]
	public void Refresh()
	{
		if (this.connectionManager == null || this.skipRefresh)
		{
			return;
		}
		int num = Grid.PosToCell(base.transform.GetPosition());
		this.Connections = this.connectionManager.GetConnections(num, this.isPhysicalBuilding);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			string text = this.connectionManager.GetVisualizerString(num);
			if (base.GetComponent<BuildingUnderConstruction>() != null && component.HasAnimation(text + "_place"))
			{
				text += "_place";
			}
			if (text != null && text != "")
			{
				component.Play(text, KAnim.PlayMode.Once, 1f, 0f);
			}
		}
	}

	// Token: 0x060019FD RID: 6653 RVA: 0x0008B0E8 File Offset: 0x000892E8
	public int GetNetworkID()
	{
		UtilityNetwork network = this.GetNetwork();
		if (network == null)
		{
			return -1;
		}
		return network.id;
	}

	// Token: 0x060019FE RID: 6654 RVA: 0x0008B108 File Offset: 0x00089308
	private UtilityNetwork GetNetwork()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return this.connectionManager.GetNetworkForDirection(num, Direction.None);
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x0008B134 File Offset: 0x00089334
	public UtilityNetwork GetNetworkForDirection(Direction d)
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		return this.connectionManager.GetNetworkForDirection(num, d);
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x0008B160 File Offset: 0x00089360
	public void UpdateConnections(UtilityConnections new_connections)
	{
		this._connections = new_connections;
		if (this.connectionManager != null)
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			this.connectionManager.SetConnections(new_connections, num, this.isPhysicalBuilding);
		}
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x0008B1A0 File Offset: 0x000893A0
	public KAnimGraphTileVisualizer GetNeighbour(Direction d)
	{
		KAnimGraphTileVisualizer kanimGraphTileVisualizer = null;
		Vector2I vector2I;
		Grid.PosToXY(base.transform.GetPosition(), out vector2I);
		int num = -1;
		switch (d)
		{
		case Direction.Up:
			if (vector2I.y < Grid.HeightInCells - 1)
			{
				num = Grid.XYToCell(vector2I.x, vector2I.y + 1);
			}
			break;
		case Direction.Right:
			if (vector2I.x < Grid.WidthInCells - 1)
			{
				num = Grid.XYToCell(vector2I.x + 1, vector2I.y);
			}
			break;
		case Direction.Down:
			if (vector2I.y > 0)
			{
				num = Grid.XYToCell(vector2I.x, vector2I.y - 1);
			}
			break;
		case Direction.Left:
			if (vector2I.x > 0)
			{
				num = Grid.XYToCell(vector2I.x - 1, vector2I.y);
			}
			break;
		}
		if (num != -1)
		{
			ObjectLayer objectLayer;
			switch (this.connectionSource)
			{
			case KAnimGraphTileVisualizer.ConnectionSource.Gas:
				objectLayer = ObjectLayer.GasConduitTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Liquid:
				objectLayer = ObjectLayer.LiquidConduitTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Electrical:
				objectLayer = ObjectLayer.WireTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Logic:
				objectLayer = ObjectLayer.LogicWireTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Tube:
				objectLayer = ObjectLayer.TravelTubeTile;
				break;
			case KAnimGraphTileVisualizer.ConnectionSource.Solid:
				objectLayer = ObjectLayer.SolidConduitTile;
				break;
			default:
				throw new ArgumentNullException("wtf");
			}
			GameObject gameObject = Grid.Objects[num, (int)objectLayer];
			if (gameObject != null)
			{
				kanimGraphTileVisualizer = gameObject.GetComponent<KAnimGraphTileVisualizer>();
			}
		}
		return kanimGraphTileVisualizer;
	}

	// Token: 0x04000E89 RID: 3721
	[Serialize]
	private UtilityConnections _connections;

	// Token: 0x04000E8A RID: 3722
	public bool isPhysicalBuilding;

	// Token: 0x04000E8B RID: 3723
	public bool skipCleanup;

	// Token: 0x04000E8C RID: 3724
	public bool skipRefresh;

	// Token: 0x04000E8D RID: 3725
	public KAnimGraphTileVisualizer.ConnectionSource connectionSource;

	// Token: 0x04000E8E RID: 3726
	[NonSerialized]
	public IUtilityNetworkMgr connectionManager;

	// Token: 0x020010D1 RID: 4305
	public enum ConnectionSource
	{
		// Token: 0x040058C2 RID: 22722
		Gas,
		// Token: 0x040058C3 RID: 22723
		Liquid,
		// Token: 0x040058C4 RID: 22724
		Electrical,
		// Token: 0x040058C5 RID: 22725
		Logic,
		// Token: 0x040058C6 RID: 22726
		Tube,
		// Token: 0x040058C7 RID: 22727
		Solid
	}
}
