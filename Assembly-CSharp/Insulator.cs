using System;
using UnityEngine;

// Token: 0x020007BB RID: 1979
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Insulator")]
public class Insulator : KMonoBehaviour
{
	// Token: 0x06003846 RID: 14406 RVA: 0x00137C6E File Offset: 0x00135E6E
	protected override void OnSpawn()
	{
		SimMessages.SetInsulation(Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), this.offset), this.building.Def.ThermalConductivity);
	}

	// Token: 0x06003847 RID: 14407 RVA: 0x00137CA0 File Offset: 0x00135EA0
	protected override void OnCleanUp()
	{
		SimMessages.SetInsulation(Grid.OffsetCell(Grid.PosToCell(base.transform.GetPosition()), this.offset), 1f);
	}

	// Token: 0x04002580 RID: 9600
	[MyCmpReq]
	private Building building;

	// Token: 0x04002581 RID: 9601
	[SerializeField]
	public CellOffset offset = CellOffset.none;
}
