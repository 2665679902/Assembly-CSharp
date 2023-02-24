using System;
using UnityEngine;

// Token: 0x020005A7 RID: 1447
public class DevLifeSupport : KMonoBehaviour, ISim200ms
{
	// Token: 0x060023B0 RID: 9136 RVA: 0x000C0FF8 File Offset: 0x000BF1F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.elementConsumer != null)
		{
			this.elementConsumer.EnableConsumption(true);
		}
	}

	// Token: 0x060023B1 RID: 9137 RVA: 0x000C101C File Offset: 0x000BF21C
	public void Sim200ms(float dt)
	{
		Vector2I vector2I = new Vector2I(-this.effectRadius, -this.effectRadius);
		Vector2I vector2I2 = new Vector2I(this.effectRadius, this.effectRadius);
		int num;
		int num2;
		Grid.PosToXY(base.transform.GetPosition(), out num, out num2);
		int num3 = Grid.XYToCell(num, num2);
		if (Grid.IsValidCell(num3))
		{
			int num4 = (int)Grid.WorldIdx[num3];
			for (int i = vector2I.y; i <= vector2I2.y; i++)
			{
				for (int j = vector2I.x; j <= vector2I2.x; j++)
				{
					int num5 = Grid.XYToCell(num + j, num2 + i);
					if (Grid.IsValidCellInWorld(num5, num4))
					{
						float num6 = (this.targetTemperature - Grid.Temperature[num5]) * Grid.Element[num5].specificHeatCapacity * Grid.Mass[num5];
						if (!Mathf.Approximately(0f, num6))
						{
							SimMessages.ModifyEnergy(num5, num6 * 0.2f, 5000f, (num6 > 0f) ? SimMessages.EnergySourceID.DebugHeat : SimMessages.EnergySourceID.DebugCool);
						}
					}
				}
			}
		}
	}

	// Token: 0x04001475 RID: 5237
	[MyCmpReq]
	private ElementConsumer elementConsumer;

	// Token: 0x04001476 RID: 5238
	public float targetTemperature = 303.15f;

	// Token: 0x04001477 RID: 5239
	public int effectRadius = 7;

	// Token: 0x04001478 RID: 5240
	private const float temperatureControlK = 0.2f;
}
