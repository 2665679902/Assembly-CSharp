using System;
using Klei.AI;
using UnityEngine;

// Token: 0x0200069E RID: 1694
public class Hit
{
	// Token: 0x06002DFB RID: 11771 RVA: 0x000F1C8D File Offset: 0x000EFE8D
	public Hit(AttackProperties properties, GameObject target)
	{
		this.properties = properties;
		this.target = target;
		this.DeliverHit();
	}

	// Token: 0x06002DFC RID: 11772 RVA: 0x000F1CA9 File Offset: 0x000EFEA9
	private float rollDamage()
	{
		return (float)Mathf.RoundToInt(UnityEngine.Random.Range(this.properties.base_damage_min, this.properties.base_damage_max));
	}

	// Token: 0x06002DFD RID: 11773 RVA: 0x000F1CCC File Offset: 0x000EFECC
	private void DeliverHit()
	{
		Health component = this.target.GetComponent<Health>();
		if (!component)
		{
			return;
		}
		this.target.Trigger(-787691065, this.properties.attacker.GetComponent<FactionAlignment>());
		float num = this.rollDamage();
		AttackableBase component2 = this.target.GetComponent<AttackableBase>();
		num *= 1f + component2.GetDamageMultiplier();
		component.Damage(num);
		if (this.properties.effects == null)
		{
			return;
		}
		Effects component3 = this.target.GetComponent<Effects>();
		if (component3)
		{
			foreach (AttackEffect attackEffect in this.properties.effects)
			{
				if (UnityEngine.Random.Range(0f, 100f) < attackEffect.effectProbability * 100f)
				{
					component3.Add(attackEffect.effectID, true);
				}
			}
		}
	}

	// Token: 0x04001B46 RID: 6982
	private AttackProperties properties;

	// Token: 0x04001B47 RID: 6983
	private GameObject target;
}
