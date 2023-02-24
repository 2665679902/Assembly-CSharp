using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000893 RID: 2195
[AddComponentMenu("KMonoBehaviour/scripts/HeatBulb")]
public class HeatBulb : KMonoBehaviour, ISim200ms
{
	// Token: 0x06003EF4 RID: 16116 RVA: 0x0015FBDD File Offset: 0x0015DDDD
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.kanim.Play("off", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06003EF5 RID: 16117 RVA: 0x0015FC08 File Offset: 0x0015DE08
	public void Sim200ms(float dt)
	{
		float num = this.kjConsumptionRate * dt;
		Vector2I vector2I = this.maxCheckOffset - this.minCheckOffset + 1;
		int num2 = vector2I.x * vector2I.y;
		float num3 = num / (float)num2;
		int num4;
		int num5;
		Grid.PosToXY(base.transform.GetPosition(), out num4, out num5);
		for (int i = this.minCheckOffset.y; i <= this.maxCheckOffset.y; i++)
		{
			for (int j = this.minCheckOffset.x; j <= this.maxCheckOffset.x; j++)
			{
				int num6 = Grid.XYToCell(num4 + j, num5 + i);
				if (Grid.IsValidCell(num6) && Grid.Temperature[num6] > this.minTemperature)
				{
					this.kjConsumed += num3;
					SimMessages.ModifyEnergy(num6, -num3, 5000f, SimMessages.EnergySourceID.HeatBulb);
				}
			}
		}
		float num7 = this.lightKJConsumptionRate * dt;
		if (this.kjConsumed > num7)
		{
			if (!this.lightSource.enabled)
			{
				this.kanim.Play("open", KAnim.PlayMode.Once, 1f, 0f);
				this.kanim.Queue("on", KAnim.PlayMode.Once, 1f, 0f);
				this.lightSource.enabled = true;
			}
			this.kjConsumed -= num7;
			return;
		}
		if (this.lightSource.enabled)
		{
			this.kanim.Play("close", KAnim.PlayMode.Once, 1f, 0f);
			this.kanim.Queue("off", KAnim.PlayMode.Once, 1f, 0f);
		}
		this.lightSource.enabled = false;
	}

	// Token: 0x04002947 RID: 10567
	[SerializeField]
	private float minTemperature;

	// Token: 0x04002948 RID: 10568
	[SerializeField]
	private float kjConsumptionRate;

	// Token: 0x04002949 RID: 10569
	[SerializeField]
	private float lightKJConsumptionRate;

	// Token: 0x0400294A RID: 10570
	[SerializeField]
	private Vector2I minCheckOffset;

	// Token: 0x0400294B RID: 10571
	[SerializeField]
	private Vector2I maxCheckOffset;

	// Token: 0x0400294C RID: 10572
	[MyCmpGet]
	private Light2D lightSource;

	// Token: 0x0400294D RID: 10573
	[MyCmpGet]
	private KBatchedAnimController kanim;

	// Token: 0x0400294E RID: 10574
	[Serialize]
	private float kjConsumed;
}
