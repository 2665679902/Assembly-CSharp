using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009D1 RID: 2513
public class WireUtilityNetworkLink : UtilityNetworkLink, IWattageRating, IHaveUtilityNetworkMgr, IBridgedNetworkItem, ICircuitConnected
{
	// Token: 0x06004AB2 RID: 19122 RVA: 0x001A23F0 File Offset: 0x001A05F0
	public Wire.WattageRating GetMaxWattageRating()
	{
		return this.maxWattageRating;
	}

	// Token: 0x06004AB3 RID: 19123 RVA: 0x001A23F8 File Offset: 0x001A05F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06004AB4 RID: 19124 RVA: 0x001A2400 File Offset: 0x001A0600
	protected override void OnDisconnect(int cell1, int cell2)
	{
		Game.Instance.electricalConduitSystem.RemoveLink(cell1, cell2);
		Game.Instance.circuitManager.Disconnect(this);
	}

	// Token: 0x06004AB5 RID: 19125 RVA: 0x001A2423 File Offset: 0x001A0623
	protected override void OnConnect(int cell1, int cell2)
	{
		Game.Instance.electricalConduitSystem.AddLink(cell1, cell2);
		Game.Instance.circuitManager.Connect(this);
	}

	// Token: 0x06004AB6 RID: 19126 RVA: 0x001A2446 File Offset: 0x001A0646
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.electricalConduitSystem;
	}

	// Token: 0x17000580 RID: 1408
	// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x001A2452 File Offset: 0x001A0652
	// (set) Token: 0x06004AB8 RID: 19128 RVA: 0x001A245A File Offset: 0x001A065A
	public bool IsVirtual { get; private set; }

	// Token: 0x17000581 RID: 1409
	// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x001A2463 File Offset: 0x001A0663
	public int PowerCell
	{
		get
		{
			return base.GetNetworkCell();
		}
	}

	// Token: 0x17000582 RID: 1410
	// (get) Token: 0x06004ABA RID: 19130 RVA: 0x001A246B File Offset: 0x001A066B
	// (set) Token: 0x06004ABB RID: 19131 RVA: 0x001A2473 File Offset: 0x001A0673
	public object VirtualCircuitKey { get; private set; }

	// Token: 0x06004ABC RID: 19132 RVA: 0x001A247C File Offset: 0x001A067C
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x06004ABD RID: 19133 RVA: 0x001A24A8 File Offset: 0x001A06A8
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		return networks.Contains(networkForCell);
	}

	// Token: 0x04003110 RID: 12560
	[SerializeField]
	public Wire.WattageRating maxWattageRating;
}
