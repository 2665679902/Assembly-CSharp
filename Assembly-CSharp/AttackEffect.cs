using System;

// Token: 0x0200069A RID: 1690
[Serializable]
public class AttackEffect
{
	// Token: 0x06002DDC RID: 11740 RVA: 0x000F14DB File Offset: 0x000EF6DB
	public AttackEffect(string ID, float probability)
	{
		this.effectID = ID;
		this.effectProbability = probability;
	}

	// Token: 0x04001B2E RID: 6958
	public string effectID;

	// Token: 0x04001B2F RID: 6959
	public float effectProbability;
}
