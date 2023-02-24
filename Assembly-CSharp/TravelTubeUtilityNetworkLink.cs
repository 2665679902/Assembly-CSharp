using System;

// Token: 0x020009B0 RID: 2480
public class TravelTubeUtilityNetworkLink : UtilityNetworkLink, IHaveUtilityNetworkMgr
{
	// Token: 0x060049A0 RID: 18848 RVA: 0x0019C55D File Offset: 0x0019A75D
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060049A1 RID: 18849 RVA: 0x0019C565 File Offset: 0x0019A765
	protected override void OnConnect(int cell1, int cell2)
	{
		Game.Instance.travelTubeSystem.AddLink(cell1, cell2);
	}

	// Token: 0x060049A2 RID: 18850 RVA: 0x0019C578 File Offset: 0x0019A778
	protected override void OnDisconnect(int cell1, int cell2)
	{
		Game.Instance.travelTubeSystem.RemoveLink(cell1, cell2);
	}

	// Token: 0x060049A3 RID: 18851 RVA: 0x0019C58B File Offset: 0x0019A78B
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.travelTubeSystem;
	}
}
