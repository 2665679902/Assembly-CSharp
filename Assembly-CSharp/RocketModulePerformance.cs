using System;

// Token: 0x02000960 RID: 2400
[Serializable]
public class RocketModulePerformance
{
	// Token: 0x060046F9 RID: 18169 RVA: 0x0018F9E3 File Offset: 0x0018DBE3
	public RocketModulePerformance(float burden, float fuelKilogramPerDistance, float enginePower)
	{
		this.burden = burden;
		this.fuelKilogramPerDistance = fuelKilogramPerDistance;
		this.enginePower = enginePower;
	}

	// Token: 0x17000548 RID: 1352
	// (get) Token: 0x060046FA RID: 18170 RVA: 0x0018FA00 File Offset: 0x0018DC00
	public float Burden
	{
		get
		{
			return this.burden;
		}
	}

	// Token: 0x17000549 RID: 1353
	// (get) Token: 0x060046FB RID: 18171 RVA: 0x0018FA08 File Offset: 0x0018DC08
	public float FuelKilogramPerDistance
	{
		get
		{
			return this.fuelKilogramPerDistance;
		}
	}

	// Token: 0x1700054A RID: 1354
	// (get) Token: 0x060046FC RID: 18172 RVA: 0x0018FA10 File Offset: 0x0018DC10
	public float EnginePower
	{
		get
		{
			return this.enginePower;
		}
	}

	// Token: 0x04002F05 RID: 12037
	public float burden;

	// Token: 0x04002F06 RID: 12038
	public float fuelKilogramPerDistance;

	// Token: 0x04002F07 RID: 12039
	public float enginePower;
}
