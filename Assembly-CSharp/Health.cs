using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x020007A7 RID: 1959
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Health")]
public class Health : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x0600375D RID: 14173 RVA: 0x00133DD5 File Offset: 0x00131FD5
	public AmountInstance GetAmountInstance
	{
		get
		{
			return this.amountInstance;
		}
	}

	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x0600375E RID: 14174 RVA: 0x00133DDD File Offset: 0x00131FDD
	// (set) Token: 0x0600375F RID: 14175 RVA: 0x00133DEA File Offset: 0x00131FEA
	public float hitPoints
	{
		get
		{
			return this.amountInstance.value;
		}
		set
		{
			this.amountInstance.value = value;
		}
	}

	// Token: 0x1700040F RID: 1039
	// (get) Token: 0x06003760 RID: 14176 RVA: 0x00133DF8 File Offset: 0x00131FF8
	public float maxHitPoints
	{
		get
		{
			return this.amountInstance.GetMax();
		}
	}

	// Token: 0x06003761 RID: 14177 RVA: 0x00133E05 File Offset: 0x00132005
	public float percent()
	{
		return this.hitPoints / this.maxHitPoints;
	}

	// Token: 0x06003762 RID: 14178 RVA: 0x00133E14 File Offset: 0x00132014
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.Health.Add(this);
		this.amountInstance = Db.Get().Amounts.HitPoints.Lookup(base.gameObject);
		this.amountInstance.value = this.amountInstance.GetMax();
		AmountInstance amountInstance = this.amountInstance;
		amountInstance.OnDelta = (Action<float>)Delegate.Combine(amountInstance.OnDelta, new Action<float>(this.OnHealthChanged));
	}

	// Token: 0x06003763 RID: 14179 RVA: 0x00133E90 File Offset: 0x00132090
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.State == Health.HealthState.Incapacitated || this.hitPoints == 0f)
		{
			if (this.CanBeIncapacitated)
			{
				this.Incapacitate(GameTags.HitPointsDepleted);
			}
			else
			{
				this.Kill();
			}
		}
		if (this.State != Health.HealthState.Incapacitated && this.State != Health.HealthState.Dead)
		{
			this.UpdateStatus();
		}
		this.effects = base.GetComponent<Effects>();
		this.UpdateHealthBar();
	}

	// Token: 0x06003764 RID: 14180 RVA: 0x00133EFE File Offset: 0x001320FE
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Health.Remove(this);
	}

	// Token: 0x06003765 RID: 14181 RVA: 0x00133F14 File Offset: 0x00132114
	public void UpdateHealthBar()
	{
		if (NameDisplayScreen.Instance == null)
		{
			return;
		}
		bool flag = this.State == Health.HealthState.Dead || this.State == Health.HealthState.Incapacitated || this.hitPoints >= this.maxHitPoints;
		NameDisplayScreen.Instance.SetHealthDisplay(base.gameObject, new Func<float>(this.percent), !flag);
	}

	// Token: 0x06003766 RID: 14182 RVA: 0x00133F76 File Offset: 0x00132176
	private void Recover()
	{
		base.GetComponent<KPrefabID>().RemoveTag(GameTags.HitPointsDepleted);
	}

	// Token: 0x06003767 RID: 14183 RVA: 0x00133F88 File Offset: 0x00132188
	public void OnHealthChanged(float delta)
	{
		base.Trigger(-1664904872, delta);
		if (this.State != Health.HealthState.Invincible)
		{
			if (this.hitPoints == 0f && !this.IsDefeated())
			{
				if (this.CanBeIncapacitated)
				{
					this.Incapacitate(GameTags.HitPointsDepleted);
				}
				else
				{
					this.Kill();
				}
			}
			else
			{
				base.GetComponent<KPrefabID>().RemoveTag(GameTags.HitPointsDepleted);
			}
		}
		this.UpdateStatus();
		this.UpdateWoundEffects();
		this.UpdateHealthBar();
	}

	// Token: 0x06003768 RID: 14184 RVA: 0x00134003 File Offset: 0x00132203
	[ContextMenu("DoDamage")]
	public void DoDamage()
	{
		this.Damage(1f);
	}

	// Token: 0x06003769 RID: 14185 RVA: 0x00134010 File Offset: 0x00132210
	public void Damage(float amount)
	{
		if (this.State != Health.HealthState.Invincible)
		{
			this.hitPoints = Mathf.Max(0f, this.hitPoints - amount);
		}
		this.OnHealthChanged(-amount);
	}

	// Token: 0x0600376A RID: 14186 RVA: 0x0013403C File Offset: 0x0013223C
	private void UpdateWoundEffects()
	{
		if (!this.effects)
		{
			return;
		}
		switch (this.State)
		{
		case Health.HealthState.Perfect:
			this.effects.Remove("LightWounds");
			this.effects.Remove("ModerateWounds");
			this.effects.Remove("SevereWounds");
			return;
		case Health.HealthState.Alright:
			this.effects.Remove("LightWounds");
			this.effects.Remove("ModerateWounds");
			this.effects.Remove("SevereWounds");
			return;
		case Health.HealthState.Scuffed:
			this.effects.Remove("ModerateWounds");
			this.effects.Remove("SevereWounds");
			if (!this.effects.HasEffect("LightWounds"))
			{
				this.effects.Add("LightWounds", true);
				return;
			}
			break;
		case Health.HealthState.Injured:
			this.effects.Remove("LightWounds");
			this.effects.Remove("SevereWounds");
			if (!this.effects.HasEffect("ModerateWounds"))
			{
				this.effects.Add("ModerateWounds", true);
				return;
			}
			break;
		case Health.HealthState.Critical:
			this.effects.Remove("LightWounds");
			this.effects.Remove("ModerateWounds");
			if (!this.effects.HasEffect("SevereWounds"))
			{
				this.effects.Add("SevereWounds", true);
				return;
			}
			break;
		case Health.HealthState.Incapacitated:
			this.effects.Remove("LightWounds");
			this.effects.Remove("ModerateWounds");
			this.effects.Remove("SevereWounds");
			return;
		case Health.HealthState.Dead:
			this.effects.Remove("LightWounds");
			this.effects.Remove("ModerateWounds");
			this.effects.Remove("SevereWounds");
			break;
		default:
			return;
		}
	}

	// Token: 0x0600376B RID: 14187 RVA: 0x0013421C File Offset: 0x0013241C
	private void UpdateStatus()
	{
		float num = this.hitPoints / this.maxHitPoints;
		Health.HealthState healthState;
		if (this.State == Health.HealthState.Invincible)
		{
			healthState = Health.HealthState.Invincible;
		}
		else if (num >= 1f)
		{
			healthState = Health.HealthState.Perfect;
		}
		else if (num >= 0.85f)
		{
			healthState = Health.HealthState.Alright;
		}
		else if (num >= 0.66f)
		{
			healthState = Health.HealthState.Scuffed;
		}
		else if ((double)num >= 0.33)
		{
			healthState = Health.HealthState.Injured;
		}
		else if (num > 0f)
		{
			healthState = Health.HealthState.Critical;
		}
		else if (num == 0f)
		{
			healthState = Health.HealthState.Incapacitated;
		}
		else
		{
			healthState = Health.HealthState.Dead;
		}
		if (this.State != healthState)
		{
			if (this.State == Health.HealthState.Incapacitated && healthState != Health.HealthState.Dead)
			{
				this.Recover();
			}
			if (healthState == Health.HealthState.Perfect)
			{
				base.Trigger(-1491582671, this);
			}
			this.State = healthState;
			KSelectable component = base.GetComponent<KSelectable>();
			if (this.State != Health.HealthState.Dead && this.State != Health.HealthState.Perfect && this.State != Health.HealthState.Alright)
			{
				component.SetStatusItem(Db.Get().StatusItemCategories.Hitpoints, Db.Get().CreatureStatusItems.HealthStatus, this.State);
				return;
			}
			component.SetStatusItem(Db.Get().StatusItemCategories.Hitpoints, null, null);
		}
	}

	// Token: 0x0600376C RID: 14188 RVA: 0x00134332 File Offset: 0x00132532
	public bool IsIncapacitated()
	{
		return this.State == Health.HealthState.Incapacitated;
	}

	// Token: 0x0600376D RID: 14189 RVA: 0x0013433D File Offset: 0x0013253D
	public bool IsDefeated()
	{
		return this.State == Health.HealthState.Incapacitated || this.State == Health.HealthState.Dead;
	}

	// Token: 0x0600376E RID: 14190 RVA: 0x00134353 File Offset: 0x00132553
	public void Incapacitate(Tag cause)
	{
		this.State = Health.HealthState.Incapacitated;
		base.GetComponent<KPrefabID>().AddTag(cause, false);
		this.Damage(this.hitPoints);
	}

	// Token: 0x0600376F RID: 14191 RVA: 0x00134375 File Offset: 0x00132575
	private void Kill()
	{
		if (base.gameObject.GetSMI<DeathMonitor.Instance>() != null)
		{
			base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Slain);
		}
	}

	// Token: 0x04002516 RID: 9494
	[Serialize]
	public bool CanBeIncapacitated;

	// Token: 0x04002517 RID: 9495
	[Serialize]
	public Health.HealthState State;

	// Token: 0x04002518 RID: 9496
	[Serialize]
	private Death source_of_death;

	// Token: 0x04002519 RID: 9497
	public HealthBar healthBar;

	// Token: 0x0400251A RID: 9498
	private Effects effects;

	// Token: 0x0400251B RID: 9499
	private AmountInstance amountInstance;

	// Token: 0x0200150A RID: 5386
	public enum HealthState
	{
		// Token: 0x04006569 RID: 25961
		Perfect,
		// Token: 0x0400656A RID: 25962
		Alright,
		// Token: 0x0400656B RID: 25963
		Scuffed,
		// Token: 0x0400656C RID: 25964
		Injured,
		// Token: 0x0400656D RID: 25965
		Critical,
		// Token: 0x0400656E RID: 25966
		Incapacitated,
		// Token: 0x0400656F RID: 25967
		Dead,
		// Token: 0x04006570 RID: 25968
		Invincible
	}
}
