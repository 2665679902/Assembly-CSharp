using System;
using UnityEngine;

// Token: 0x020005A6 RID: 1446
public class DevGenerator : Generator
{
	// Token: 0x060023AE RID: 9134 RVA: 0x000C0F78 File Offset: 0x000BF178
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		ushort circuitID = base.CircuitID;
		this.operational.SetFlag(Generator.wireConnectedFlag, circuitID != ushort.MaxValue);
		if (!this.operational.IsOperational)
		{
			return;
		}
		float num = this.wattageRating;
		if (num > 0f)
		{
			num *= dt;
			num = Mathf.Max(num, 1f * dt);
			base.GenerateJoules(num, false);
		}
	}

	// Token: 0x04001474 RID: 5236
	public float wattageRating = 100000f;
}
