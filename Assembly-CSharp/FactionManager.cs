using System;
using UnityEngine;

// Token: 0x0200069C RID: 1692
[AddComponentMenu("KMonoBehaviour/scripts/FactionManager")]
public class FactionManager : KMonoBehaviour
{
	// Token: 0x06002DF2 RID: 11762 RVA: 0x000F18BE File Offset: 0x000EFABE
	public static void DestroyInstance()
	{
		FactionManager.Instance = null;
	}

	// Token: 0x06002DF3 RID: 11763 RVA: 0x000F18C6 File Offset: 0x000EFAC6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		FactionManager.Instance = this;
	}

	// Token: 0x06002DF4 RID: 11764 RVA: 0x000F18D4 File Offset: 0x000EFAD4
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06002DF5 RID: 11765 RVA: 0x000F18DC File Offset: 0x000EFADC
	public Faction GetFaction(FactionManager.FactionID faction)
	{
		switch (faction)
		{
		case FactionManager.FactionID.Duplicant:
			return this.Duplicant;
		case FactionManager.FactionID.Friendly:
			return this.Friendly;
		case FactionManager.FactionID.Hostile:
			return this.Hostile;
		case FactionManager.FactionID.Prey:
			return this.Prey;
		case FactionManager.FactionID.Predator:
			return this.Predator;
		case FactionManager.FactionID.Pest:
			return this.Pest;
		default:
			return null;
		}
	}

	// Token: 0x06002DF6 RID: 11766 RVA: 0x000F1934 File Offset: 0x000EFB34
	public FactionManager.Disposition GetDisposition(FactionManager.FactionID of_faction, FactionManager.FactionID to_faction)
	{
		if (FactionManager.Instance.GetFaction(of_faction).Dispositions.ContainsKey(to_faction))
		{
			return FactionManager.Instance.GetFaction(of_faction).Dispositions[to_faction];
		}
		return FactionManager.Disposition.Neutral;
	}

	// Token: 0x04001B3C RID: 6972
	public static FactionManager Instance;

	// Token: 0x04001B3D RID: 6973
	public Faction Duplicant = new Faction(FactionManager.FactionID.Duplicant);

	// Token: 0x04001B3E RID: 6974
	public Faction Friendly = new Faction(FactionManager.FactionID.Friendly);

	// Token: 0x04001B3F RID: 6975
	public Faction Hostile = new Faction(FactionManager.FactionID.Hostile);

	// Token: 0x04001B40 RID: 6976
	public Faction Predator = new Faction(FactionManager.FactionID.Predator);

	// Token: 0x04001B41 RID: 6977
	public Faction Prey = new Faction(FactionManager.FactionID.Prey);

	// Token: 0x04001B42 RID: 6978
	public Faction Pest = new Faction(FactionManager.FactionID.Pest);

	// Token: 0x0200136B RID: 4971
	public enum FactionID
	{
		// Token: 0x0400607B RID: 24699
		Duplicant,
		// Token: 0x0400607C RID: 24700
		Friendly,
		// Token: 0x0400607D RID: 24701
		Hostile,
		// Token: 0x0400607E RID: 24702
		Prey,
		// Token: 0x0400607F RID: 24703
		Predator,
		// Token: 0x04006080 RID: 24704
		Pest,
		// Token: 0x04006081 RID: 24705
		NumberOfFactions
	}

	// Token: 0x0200136C RID: 4972
	public enum Disposition
	{
		// Token: 0x04006083 RID: 24707
		Assist,
		// Token: 0x04006084 RID: 24708
		Neutral,
		// Token: 0x04006085 RID: 24709
		Attack
	}
}
