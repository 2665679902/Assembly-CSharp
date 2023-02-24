using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009D2 RID: 2514
public class WireUtilitySemiVirtualNetworkLink : UtilityNetworkLink, IHaveUtilityNetworkMgr, ICircuitConnected
{
	// Token: 0x06004ABF RID: 19135 RVA: 0x001A24D8 File Offset: 0x001A06D8
	public Wire.WattageRating GetMaxWattageRating()
	{
		return this.maxWattageRating;
	}

	// Token: 0x06004AC0 RID: 19136 RVA: 0x001A24E0 File Offset: 0x001A06E0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06004AC1 RID: 19137 RVA: 0x001A24E8 File Offset: 0x001A06E8
	protected override void OnSpawn()
	{
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component != null)
		{
			this.VirtualCircuitKey = component.CraftInterface;
		}
		else
		{
			CraftModuleInterface component2 = this.GetMyWorld().GetComponent<CraftModuleInterface>();
			if (component2 != null)
			{
				this.VirtualCircuitKey = component2;
			}
		}
		Game.Instance.electricalConduitSystem.AddToVirtualNetworks(this.VirtualCircuitKey, this, true);
		base.OnSpawn();
	}

	// Token: 0x06004AC2 RID: 19138 RVA: 0x001A254C File Offset: 0x001A074C
	public void SetLinkConnected(bool connect)
	{
		if (connect && this.visualizeOnly)
		{
			this.visualizeOnly = false;
			if (base.isSpawned)
			{
				base.Connect();
				return;
			}
		}
		else if (!connect && !this.visualizeOnly)
		{
			if (base.isSpawned)
			{
				base.Disconnect();
			}
			this.visualizeOnly = true;
		}
	}

	// Token: 0x06004AC3 RID: 19139 RVA: 0x001A259A File Offset: 0x001A079A
	protected override void OnDisconnect(int cell1, int cell2)
	{
		Game.Instance.electricalConduitSystem.RemoveSemiVirtualLink(cell1, this.VirtualCircuitKey);
	}

	// Token: 0x06004AC4 RID: 19140 RVA: 0x001A25B2 File Offset: 0x001A07B2
	protected override void OnConnect(int cell1, int cell2)
	{
		Game.Instance.electricalConduitSystem.AddSemiVirtualLink(cell1, this.VirtualCircuitKey);
	}

	// Token: 0x06004AC5 RID: 19141 RVA: 0x001A25CA File Offset: 0x001A07CA
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.electricalConduitSystem;
	}

	// Token: 0x17000583 RID: 1411
	// (get) Token: 0x06004AC6 RID: 19142 RVA: 0x001A25D6 File Offset: 0x001A07D6
	// (set) Token: 0x06004AC7 RID: 19143 RVA: 0x001A25DE File Offset: 0x001A07DE
	public bool IsVirtual { get; private set; }

	// Token: 0x17000584 RID: 1412
	// (get) Token: 0x06004AC8 RID: 19144 RVA: 0x001A25E7 File Offset: 0x001A07E7
	public int PowerCell
	{
		get
		{
			return base.GetNetworkCell();
		}
	}

	// Token: 0x17000585 RID: 1413
	// (get) Token: 0x06004AC9 RID: 19145 RVA: 0x001A25EF File Offset: 0x001A07EF
	// (set) Token: 0x06004ACA RID: 19146 RVA: 0x001A25F7 File Offset: 0x001A07F7
	public object VirtualCircuitKey { get; private set; }

	// Token: 0x06004ACB RID: 19147 RVA: 0x001A2600 File Offset: 0x001A0800
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x06004ACC RID: 19148 RVA: 0x001A262C File Offset: 0x001A082C
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		return networks.Contains(networkForCell);
	}

	// Token: 0x04003113 RID: 12563
	[SerializeField]
	public Wire.WattageRating maxWattageRating;
}
