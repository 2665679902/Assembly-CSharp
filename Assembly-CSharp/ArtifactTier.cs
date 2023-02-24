using System;

// Token: 0x02000240 RID: 576
public class ArtifactTier
{
	// Token: 0x06000B5A RID: 2906 RVA: 0x000406D5 File Offset: 0x0003E8D5
	public ArtifactTier(StringKey str_key, EffectorValues values, float payload_drop_chance)
	{
		this.decorValues = values;
		this.name_key = str_key;
		this.payloadDropChance = payload_drop_chance;
	}

	// Token: 0x040006C0 RID: 1728
	public EffectorValues decorValues;

	// Token: 0x040006C1 RID: 1729
	public StringKey name_key;

	// Token: 0x040006C2 RID: 1730
	public float payloadDropChance;
}
