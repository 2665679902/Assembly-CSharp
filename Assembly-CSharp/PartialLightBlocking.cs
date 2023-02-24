using System;
using KSerialization;

// Token: 0x02000620 RID: 1568
[SerializationConfig(MemberSerialization.OptIn)]
public class PartialLightBlocking : KMonoBehaviour
{
	// Token: 0x06002913 RID: 10515 RVA: 0x000D9141 File Offset: 0x000D7341
	protected override void OnSpawn()
	{
		this.SetLightBlocking();
		base.OnSpawn();
	}

	// Token: 0x06002914 RID: 10516 RVA: 0x000D914F File Offset: 0x000D734F
	protected override void OnCleanUp()
	{
		this.ClearLightBlocking();
		base.OnCleanUp();
	}

	// Token: 0x06002915 RID: 10517 RVA: 0x000D9160 File Offset: 0x000D7360
	public void SetLightBlocking()
	{
		int[] placementCells = base.GetComponent<Building>().PlacementCells;
		for (int i = 0; i < placementCells.Length; i++)
		{
			SimMessages.SetCellProperties(placementCells[i], 48);
		}
	}

	// Token: 0x06002916 RID: 10518 RVA: 0x000D9194 File Offset: 0x000D7394
	public void ClearLightBlocking()
	{
		int[] placementCells = base.GetComponent<Building>().PlacementCells;
		for (int i = 0; i < placementCells.Length; i++)
		{
			SimMessages.ClearCellProperties(placementCells[i], 48);
		}
	}

	// Token: 0x04001829 RID: 6185
	private const byte PartialLightBlockingProperties = 48;
}
