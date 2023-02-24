using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000698 RID: 1688
public class Attack
{
	// Token: 0x06002DD9 RID: 11737 RVA: 0x000F1456 File Offset: 0x000EF656
	public Attack(AttackProperties properties, GameObject[] targets)
	{
		this.properties = properties;
		this.targets = targets;
		this.RollHits();
	}

	// Token: 0x06002DDA RID: 11738 RVA: 0x000F1474 File Offset: 0x000EF674
	private void RollHits()
	{
		int num = 0;
		while (num < this.targets.Length && num <= this.properties.maxHits - 1)
		{
			if (this.targets[num] != null)
			{
				new Hit(this.properties, this.targets[num]);
			}
			num++;
		}
	}

	// Token: 0x04001B23 RID: 6947
	private AttackProperties properties;

	// Token: 0x04001B24 RID: 6948
	private GameObject[] targets;

	// Token: 0x04001B25 RID: 6949
	public List<Hit> Hits;
}
