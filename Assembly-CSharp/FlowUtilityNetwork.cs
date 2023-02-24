using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009BC RID: 2492
public class FlowUtilityNetwork : UtilityNetwork
{
	// Token: 0x1700057B RID: 1403
	// (get) Token: 0x06004A02 RID: 18946 RVA: 0x0019EDAA File Offset: 0x0019CFAA
	public bool HasSinks
	{
		get
		{
			return this.sinks.Count > 0;
		}
	}

	// Token: 0x06004A03 RID: 18947 RVA: 0x0019EDBA File Offset: 0x0019CFBA
	public int GetActiveCount()
	{
		return this.sinks.Count;
	}

	// Token: 0x06004A04 RID: 18948 RVA: 0x0019EDC8 File Offset: 0x0019CFC8
	public override void AddItem(object generic_item)
	{
		FlowUtilityNetwork.IItem item = (FlowUtilityNetwork.IItem)generic_item;
		if (item != null)
		{
			switch (item.EndpointType)
			{
			case Endpoint.Source:
				if (this.sources.Contains(item))
				{
					return;
				}
				this.sources.Add(item);
				item.Network = this;
				return;
			case Endpoint.Sink:
				if (this.sinks.Contains(item))
				{
					return;
				}
				this.sinks.Add(item);
				item.Network = this;
				return;
			case Endpoint.Conduit:
				this.conduitCount++;
				return;
			default:
				item.Network = this;
				break;
			}
		}
	}

	// Token: 0x06004A05 RID: 18949 RVA: 0x0019EE58 File Offset: 0x0019D058
	public override void Reset(UtilityNetworkGridNode[] grid)
	{
		for (int i = 0; i < this.sinks.Count; i++)
		{
			FlowUtilityNetwork.IItem item = this.sinks[i];
			item.Network = null;
			UtilityNetworkGridNode utilityNetworkGridNode = grid[item.Cell];
			utilityNetworkGridNode.networkIdx = -1;
			grid[item.Cell] = utilityNetworkGridNode;
		}
		for (int j = 0; j < this.sources.Count; j++)
		{
			FlowUtilityNetwork.IItem item2 = this.sources[j];
			item2.Network = null;
			UtilityNetworkGridNode utilityNetworkGridNode2 = grid[item2.Cell];
			utilityNetworkGridNode2.networkIdx = -1;
			grid[item2.Cell] = utilityNetworkGridNode2;
		}
		this.conduitCount = 0;
		for (int k = 0; k < this.conduits.Count; k++)
		{
			FlowUtilityNetwork.IItem item3 = this.conduits[k];
			item3.Network = null;
			UtilityNetworkGridNode utilityNetworkGridNode3 = grid[item3.Cell];
			utilityNetworkGridNode3.networkIdx = -1;
			grid[item3.Cell] = utilityNetworkGridNode3;
		}
	}

	// Token: 0x0400309F RID: 12447
	public List<FlowUtilityNetwork.IItem> sources = new List<FlowUtilityNetwork.IItem>();

	// Token: 0x040030A0 RID: 12448
	public List<FlowUtilityNetwork.IItem> sinks = new List<FlowUtilityNetwork.IItem>();

	// Token: 0x040030A1 RID: 12449
	public List<FlowUtilityNetwork.IItem> conduits = new List<FlowUtilityNetwork.IItem>();

	// Token: 0x040030A2 RID: 12450
	public int conduitCount;

	// Token: 0x020017C0 RID: 6080
	public interface IItem
	{
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06008BBF RID: 35775
		int Cell { get; }

		// Token: 0x1700093B RID: 2363
		// (set) Token: 0x06008BC0 RID: 35776
		FlowUtilityNetwork Network { set; }

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06008BC1 RID: 35777
		Endpoint EndpointType { get; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06008BC2 RID: 35778
		ConduitType ConduitType { get; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06008BC3 RID: 35779
		GameObject GameObject { get; }
	}

	// Token: 0x020017C1 RID: 6081
	public class NetworkItem : FlowUtilityNetwork.IItem
	{
		// Token: 0x06008BC4 RID: 35780 RVA: 0x00300415 File Offset: 0x002FE615
		public NetworkItem(ConduitType conduit_type, Endpoint endpoint_type, int cell, GameObject parent)
		{
			this.conduitType = conduit_type;
			this.endpointType = endpoint_type;
			this.cell = cell;
			this.parent = parent;
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06008BC5 RID: 35781 RVA: 0x0030043A File Offset: 0x002FE63A
		public Endpoint EndpointType
		{
			get
			{
				return this.endpointType;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06008BC6 RID: 35782 RVA: 0x00300442 File Offset: 0x002FE642
		public ConduitType ConduitType
		{
			get
			{
				return this.conduitType;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06008BC7 RID: 35783 RVA: 0x0030044A File Offset: 0x002FE64A
		public int Cell
		{
			get
			{
				return this.cell;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06008BC8 RID: 35784 RVA: 0x00300452 File Offset: 0x002FE652
		// (set) Token: 0x06008BC9 RID: 35785 RVA: 0x0030045A File Offset: 0x002FE65A
		public FlowUtilityNetwork Network
		{
			get
			{
				return this.network;
			}
			set
			{
				this.network = value;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06008BCA RID: 35786 RVA: 0x00300463 File Offset: 0x002FE663
		public GameObject GameObject
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x04006DEC RID: 28140
		private int cell;

		// Token: 0x04006DED RID: 28141
		private FlowUtilityNetwork network;

		// Token: 0x04006DEE RID: 28142
		private Endpoint endpointType;

		// Token: 0x04006DEF RID: 28143
		private ConduitType conduitType;

		// Token: 0x04006DF0 RID: 28144
		private GameObject parent;
	}
}
