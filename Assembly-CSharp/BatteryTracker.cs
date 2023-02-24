using System;
using UnityEngine;

// Token: 0x020004EA RID: 1258
public class BatteryTracker : WorldTracker
{
	// Token: 0x06001DBE RID: 7614 RVA: 0x0009EAC2 File Offset: 0x0009CCC2
	public BatteryTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x0009EACC File Offset: 0x0009CCCC
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
					ushort circuitID = Game.Instance.circuitManager.GetCircuitID(num2);
					foreach (Battery battery in Game.Instance.circuitManager.GetBatteriesOnCircuit(circuitID))
					{
						num += battery.JoulesAvailable;
					}
				}
			}
		}
		base.AddPoint(Mathf.Round(num));
	}

	// Token: 0x06001DC0 RID: 7616 RVA: 0x0009EBD8 File Offset: 0x0009CDD8
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedJoules(value, "F1", GameUtil.TimeSlice.None);
	}
}
