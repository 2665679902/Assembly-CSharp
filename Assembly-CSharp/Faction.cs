using System;
using System.Collections.Generic;

// Token: 0x0200069D RID: 1693
public class Faction
{
	// Token: 0x06002DF8 RID: 11768 RVA: 0x000F19C4 File Offset: 0x000EFBC4
	public HashSet<FactionAlignment> HostileTo()
	{
		HashSet<FactionAlignment> hashSet = new HashSet<FactionAlignment>();
		foreach (KeyValuePair<FactionManager.FactionID, FactionManager.Disposition> keyValuePair in this.Dispositions)
		{
			if (keyValuePair.Value == FactionManager.Disposition.Attack)
			{
				hashSet.UnionWith(FactionManager.Instance.GetFaction(keyValuePair.Key).Members);
			}
		}
		return hashSet;
	}

	// Token: 0x06002DF9 RID: 11769 RVA: 0x000F1A40 File Offset: 0x000EFC40
	public Faction(FactionManager.FactionID faction)
	{
		this.ID = faction;
		this.ConfigureAlignments(faction);
	}

	// Token: 0x06002DFA RID: 11770 RVA: 0x000F1A88 File Offset: 0x000EFC88
	private void ConfigureAlignments(FactionManager.FactionID faction)
	{
		switch (faction)
		{
		case FactionManager.FactionID.Duplicant:
			this.Dispositions.Add(FactionManager.FactionID.Duplicant, FactionManager.Disposition.Assist);
			this.Dispositions.Add(FactionManager.FactionID.Friendly, FactionManager.Disposition.Assist);
			this.Dispositions.Add(FactionManager.FactionID.Hostile, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Predator, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Prey, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Pest, FactionManager.Disposition.Neutral);
			return;
		case FactionManager.FactionID.Friendly:
			this.Dispositions.Add(FactionManager.FactionID.Duplicant, FactionManager.Disposition.Assist);
			this.Dispositions.Add(FactionManager.FactionID.Friendly, FactionManager.Disposition.Assist);
			this.Dispositions.Add(FactionManager.FactionID.Hostile, FactionManager.Disposition.Attack);
			this.Dispositions.Add(FactionManager.FactionID.Predator, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Prey, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Pest, FactionManager.Disposition.Neutral);
			return;
		case FactionManager.FactionID.Hostile:
			this.Dispositions.Add(FactionManager.FactionID.Duplicant, FactionManager.Disposition.Attack);
			this.Dispositions.Add(FactionManager.FactionID.Friendly, FactionManager.Disposition.Attack);
			this.Dispositions.Add(FactionManager.FactionID.Hostile, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Predator, FactionManager.Disposition.Attack);
			this.Dispositions.Add(FactionManager.FactionID.Prey, FactionManager.Disposition.Attack);
			this.Dispositions.Add(FactionManager.FactionID.Pest, FactionManager.Disposition.Attack);
			return;
		case FactionManager.FactionID.Prey:
			this.Dispositions.Add(FactionManager.FactionID.Duplicant, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Friendly, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Hostile, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Predator, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Prey, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Pest, FactionManager.Disposition.Neutral);
			return;
		case FactionManager.FactionID.Predator:
			this.Dispositions.Add(FactionManager.FactionID.Duplicant, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Friendly, FactionManager.Disposition.Attack);
			this.Dispositions.Add(FactionManager.FactionID.Hostile, FactionManager.Disposition.Attack);
			this.Dispositions.Add(FactionManager.FactionID.Predator, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Prey, FactionManager.Disposition.Attack);
			this.Dispositions.Add(FactionManager.FactionID.Pest, FactionManager.Disposition.Attack);
			return;
		case FactionManager.FactionID.Pest:
			this.Dispositions.Add(FactionManager.FactionID.Duplicant, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Friendly, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Hostile, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Predator, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Prey, FactionManager.Disposition.Neutral);
			this.Dispositions.Add(FactionManager.FactionID.Pest, FactionManager.Disposition.Neutral);
			return;
		default:
			return;
		}
	}

	// Token: 0x04001B43 RID: 6979
	public HashSet<FactionAlignment> Members = new HashSet<FactionAlignment>();

	// Token: 0x04001B44 RID: 6980
	public FactionManager.FactionID ID;

	// Token: 0x04001B45 RID: 6981
	public Dictionary<FactionManager.FactionID, FactionManager.Disposition> Dispositions = new Dictionary<FactionManager.FactionID, FactionManager.Disposition>(default(Faction.FactionIDComparer));

	// Token: 0x0200136D RID: 4973
	public struct FactionIDComparer : IEqualityComparer<FactionManager.FactionID>
	{
		// Token: 0x06007DD3 RID: 32211 RVA: 0x002D6DB6 File Offset: 0x002D4FB6
		public bool Equals(FactionManager.FactionID x, FactionManager.FactionID y)
		{
			return x == y;
		}

		// Token: 0x06007DD4 RID: 32212 RVA: 0x002D6DBC File Offset: 0x002D4FBC
		public int GetHashCode(FactionManager.FactionID obj)
		{
			return (int)obj;
		}
	}
}
