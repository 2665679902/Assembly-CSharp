using System;
using UnityEngine;

// Token: 0x020008EA RID: 2282
[AddComponentMenu("KMonoBehaviour/scripts/RobotExhaustPipe")]
public class RobotExhaustPipe : KMonoBehaviour, ISim4000ms
{
	// Token: 0x060041C0 RID: 16832 RVA: 0x00171C0C File Offset: 0x0016FE0C
	public void Sim4000ms(float dt)
	{
		Facing component = base.GetComponent<Facing>();
		bool flag = false;
		if (component)
		{
			flag = component.GetFacing();
		}
		CO2Manager.instance.SpawnBreath(Grid.CellToPos(Grid.PosToCell(base.gameObject)), dt * this.CO2_RATE, 303.15f, flag);
	}

	// Token: 0x04002BCE RID: 11214
	private float CO2_RATE = 0.001f;
}
