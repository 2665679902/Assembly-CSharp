using System;
using System.Collections.Generic;

// Token: 0x02000805 RID: 2053
public class LogicUtilityNetworkLink : UtilityNetworkLink, IHaveUtilityNetworkMgr, IBridgedNetworkItem
{
	// Token: 0x06003B7B RID: 15227 RVA: 0x0014A17A File Offset: 0x0014837A
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06003B7C RID: 15228 RVA: 0x0014A182 File Offset: 0x00148382
	protected override void OnConnect(int cell1, int cell2)
	{
		this.cell_one = cell1;
		this.cell_two = cell2;
		Game.Instance.logicCircuitSystem.AddLink(cell1, cell2);
		Game.Instance.logicCircuitManager.Connect(this);
	}

	// Token: 0x06003B7D RID: 15229 RVA: 0x0014A1B3 File Offset: 0x001483B3
	protected override void OnDisconnect(int cell1, int cell2)
	{
		Game.Instance.logicCircuitSystem.RemoveLink(cell1, cell2);
		Game.Instance.logicCircuitManager.Disconnect(this);
	}

	// Token: 0x06003B7E RID: 15230 RVA: 0x0014A1D6 File Offset: 0x001483D6
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.logicCircuitSystem;
	}

	// Token: 0x06003B7F RID: 15231 RVA: 0x0014A1E4 File Offset: 0x001483E4
	public void AddNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		if (networkForCell != null)
		{
			networks.Add(networkForCell);
		}
	}

	// Token: 0x06003B80 RID: 15232 RVA: 0x0014A210 File Offset: 0x00148410
	public bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks)
	{
		int networkCell = base.GetNetworkCell();
		UtilityNetwork networkForCell = this.GetNetworkManager().GetNetworkForCell(networkCell);
		return networks.Contains(networkForCell);
	}

	// Token: 0x040026D5 RID: 9941
	public LogicWire.BitDepth bitDepth;

	// Token: 0x040026D6 RID: 9942
	public int cell_one;

	// Token: 0x040026D7 RID: 9943
	public int cell_two;
}
