using System;
using UnityEngine;

// Token: 0x020004E9 RID: 1257
public class PowerUseTracker : WorldTracker
{
	// Token: 0x06001DBB RID: 7611 RVA: 0x0009E9EE File Offset: 0x0009CBEE
	public PowerUseTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x0009E9F8 File Offset: 0x0009CBF8
	public override void UpdateData()
	{
		float num = 0f;
		foreach (UtilityNetwork utilityNetwork in Game.Instance.electricalConduitSystem.GetNetworks())
		{
			ElectricalUtilityNetwork electricalUtilityNetwork = (ElectricalUtilityNetwork)utilityNetwork;
			if (electricalUtilityNetwork.allWires != null && electricalUtilityNetwork.allWires.Count != 0)
			{
				int num2 = Grid.PosToCell(electricalUtilityNetwork.allWires[0]);
				if ((int)Grid.WorldIdx[num2] == base.WorldID)
				{
					num += Game.Instance.circuitManager.GetWattsUsedByCircuit(Game.Instance.circuitManager.GetCircuitID(num2));
				}
			}
		}
		base.AddPoint(Mathf.Round(num));
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x0009EAB8 File Offset: 0x0009CCB8
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedWattage(value, GameUtil.WattageFormatterUnit.Automatic, true);
	}
}
