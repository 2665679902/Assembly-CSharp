using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000453 RID: 1107
[AddComponentMenu("KMonoBehaviour/Workable/Breakable")]
public class Breakable : Workable
{
	// Token: 0x060017F3 RID: 6131 RVA: 0x0007D5B1 File Offset: 0x0007B7B1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = false;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_break_kanim") };
		base.SetWorkTime(float.PositiveInfinity);
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x0007D5E9 File Offset: 0x0007B7E9
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.Breakables.Add(this);
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x0007D5FC File Offset: 0x0007B7FC
	public bool isBroken()
	{
		return this.hp == null || this.hp.HitPoints <= 0;
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x0007D620 File Offset: 0x0007B820
	public Notification CreateDamageNotification()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		return new Notification(BUILDING.STATUSITEMS.ANGERDAMAGE.NOTIFICATION, NotificationType.BadMinor, (List<Notification> notificationList, object data) => BUILDING.STATUSITEMS.ANGERDAMAGE.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), component.GetProperName(), false, 0f, null, null, null, true, false, false);
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x0007D678 File Offset: 0x0007B878
	private static string ToolTipResolver(List<Notification> notificationList, object data)
	{
		string text = "";
		for (int i = 0; i < notificationList.Count; i++)
		{
			Notification notification = notificationList[i];
			text += (string)notification.tooltipData;
			if (i < notificationList.Count - 1)
			{
				text += "\n";
			}
		}
		return string.Format(BUILDING.STATUSITEMS.ANGERDAMAGE.NOTIFICATION_TOOLTIP, text);
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x0007D6E0 File Offset: 0x0007B8E0
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.secondsPerTenPercentDamage = 2f;
		this.tenPercentDamage = Mathf.CeilToInt((float)this.hp.MaxHitPoints * 0.1f);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.AngerDamage, this);
		this.notification = this.CreateDamageNotification();
		base.gameObject.AddOrGet<Notifier>().Add(this.notification, "");
		this.elapsedDamageTime = 0f;
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x0007D76C File Offset: 0x0007B96C
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		if (this.elapsedDamageTime >= this.secondsPerTenPercentDamage)
		{
			this.elapsedDamageTime -= this.elapsedDamageTime;
			base.Trigger(-794517298, new BuildingHP.DamageSourceInfo
			{
				damage = this.tenPercentDamage,
				source = BUILDINGS.DAMAGESOURCES.MINION_DESTRUCTION,
				popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.MINION_DESTRUCTION
			});
		}
		this.elapsedDamageTime += dt;
		return this.hp.HitPoints <= 0;
	}

	// Token: 0x060017FA RID: 6138 RVA: 0x0007D804 File Offset: 0x0007BA04
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.AngerDamage, false);
		base.gameObject.AddOrGet<Notifier>().Remove(this.notification);
		if (worker != null)
		{
			worker.Trigger(-1734580852, null);
		}
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x0007D85F File Offset: 0x0007BA5F
	public override bool InstantlyFinish(Worker worker)
	{
		return false;
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x0007D862 File Offset: 0x0007BA62
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.Breakables.Remove(this);
	}

	// Token: 0x04000D4E RID: 3406
	private const float TIME_TO_BREAK_AT_FULL_HEALTH = 20f;

	// Token: 0x04000D4F RID: 3407
	private Notification notification;

	// Token: 0x04000D50 RID: 3408
	private float secondsPerTenPercentDamage = float.PositiveInfinity;

	// Token: 0x04000D51 RID: 3409
	private float elapsedDamageTime;

	// Token: 0x04000D52 RID: 3410
	private int tenPercentDamage = int.MaxValue;

	// Token: 0x04000D53 RID: 3411
	[MyCmpGet]
	private BuildingHP hp;
}
