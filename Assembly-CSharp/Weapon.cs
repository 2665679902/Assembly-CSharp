using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200069F RID: 1695
[AddComponentMenu("KMonoBehaviour/scripts/Weapon")]
public class Weapon : KMonoBehaviour
{
	// Token: 0x06002DFE RID: 11774 RVA: 0x000F1DCC File Offset: 0x000EFFCC
	public void Configure(float base_damage_min, float base_damage_max, AttackProperties.DamageType attackType = AttackProperties.DamageType.Standard, AttackProperties.TargetType targetType = AttackProperties.TargetType.Single, int maxHits = 1, float aoeRadius = 0f)
	{
		this.properties = new AttackProperties();
		this.properties.base_damage_min = base_damage_min;
		this.properties.base_damage_max = base_damage_max;
		this.properties.maxHits = maxHits;
		this.properties.damageType = attackType;
		this.properties.aoe_radius = aoeRadius;
		this.properties.attacker = this;
	}

	// Token: 0x06002DFF RID: 11775 RVA: 0x000F1E2E File Offset: 0x000F002E
	public void AddEffect(string effectID = "WasAttacked", float probability = 1f)
	{
		if (this.properties.effects == null)
		{
			this.properties.effects = new List<AttackEffect>();
		}
		this.properties.effects.Add(new AttackEffect(effectID, probability));
	}

	// Token: 0x06002E00 RID: 11776 RVA: 0x000F1E64 File Offset: 0x000F0064
	public int AttackArea(Vector3 centerPoint)
	{
		Vector3 vector = Vector3.zero;
		this.alignment = base.GetComponent<FactionAlignment>();
		if (this.alignment == null)
		{
			return 0;
		}
		List<GameObject> list = new List<GameObject>();
		foreach (Health health in Components.Health.Items)
		{
			if (!(health.gameObject == base.gameObject) && !health.IsDefeated())
			{
				FactionAlignment component = health.GetComponent<FactionAlignment>();
				if (!(component == null) && component.IsAlignmentActive() && FactionManager.Instance.GetDisposition(this.alignment.Alignment, component.Alignment) == FactionManager.Disposition.Attack)
				{
					vector = health.transform.GetPosition();
					vector.z = centerPoint.z;
					if (Vector3.Distance(centerPoint, vector) <= this.properties.aoe_radius)
					{
						list.Add(health.gameObject);
					}
				}
			}
		}
		this.AttackTargets(list.ToArray());
		return list.Count;
	}

	// Token: 0x06002E01 RID: 11777 RVA: 0x000F1F8C File Offset: 0x000F018C
	public void AttackTarget(GameObject target)
	{
		this.AttackTargets(new GameObject[] { target });
	}

	// Token: 0x06002E02 RID: 11778 RVA: 0x000F1F9E File Offset: 0x000F019E
	public void AttackTargets(GameObject[] targets)
	{
		if (this.properties == null)
		{
			global::Debug.LogWarning(string.Format("Attack properties not configured. {0} cannot attack with weapon.", base.gameObject.name));
			return;
		}
		new Attack(this.properties, targets);
	}

	// Token: 0x06002E03 RID: 11779 RVA: 0x000F1FD0 File Offset: 0x000F01D0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.properties.attacker = this;
	}

	// Token: 0x04001B48 RID: 6984
	[MyCmpReq]
	private FactionAlignment alignment;

	// Token: 0x04001B49 RID: 6985
	public AttackProperties properties;
}
