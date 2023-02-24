using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000569 RID: 1385
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Workable/BuildingHP")]
public class BuildingHP : Workable
{
	// Token: 0x17000199 RID: 409
	// (get) Token: 0x06002177 RID: 8567 RVA: 0x000B6596 File Offset: 0x000B4796
	public int HitPoints
	{
		get
		{
			return this.hitpoints;
		}
	}

	// Token: 0x06002178 RID: 8568 RVA: 0x000B659E File Offset: 0x000B479E
	public void SetHitPoints(int hp)
	{
		this.hitpoints = hp;
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06002179 RID: 8569 RVA: 0x000B65A7 File Offset: 0x000B47A7
	public int MaxHitPoints
	{
		get
		{
			return this.building.Def.HitPoints;
		}
	}

	// Token: 0x0600217A RID: 8570 RVA: 0x000B65B9 File Offset: 0x000B47B9
	public BuildingHP.DamageSourceInfo GetDamageSourceInfo()
	{
		return this.damageSourceInfo;
	}

	// Token: 0x0600217B RID: 8571 RVA: 0x000B65C1 File Offset: 0x000B47C1
	protected override void OnLoadLevel()
	{
		this.smi = null;
		base.OnLoadLevel();
	}

	// Token: 0x0600217C RID: 8572 RVA: 0x000B65D0 File Offset: 0x000B47D0
	public void DoDamage(int damage)
	{
		if (!this.invincible)
		{
			damage = Math.Max(0, damage);
			this.hitpoints = Math.Max(0, this.hitpoints - damage);
			base.Trigger(-1964935036, this);
		}
	}

	// Token: 0x0600217D RID: 8573 RVA: 0x000B6604 File Offset: 0x000B4804
	public void Repair(int repair_amount)
	{
		if (this.hitpoints + repair_amount < this.hitpoints)
		{
			this.hitpoints = this.building.Def.HitPoints;
		}
		else
		{
			this.hitpoints = Math.Min(this.hitpoints + repair_amount, this.building.Def.HitPoints);
		}
		base.Trigger(-1699355994, this);
		if (this.hitpoints >= this.building.Def.HitPoints)
		{
			base.Trigger(-1735440190, this);
		}
	}

	// Token: 0x0600217E RID: 8574 RVA: 0x000B668C File Offset: 0x000B488C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetWorkTime(10f);
		this.multitoolContext = "build";
		this.multitoolHitEffectTag = EffectConfigs.BuildSplashId;
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x000B66C0 File Offset: 0x000B48C0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new BuildingHP.SMInstance(this);
		this.smi.StartSM();
		base.Subscribe<BuildingHP>(-794517298, BuildingHP.OnDoBuildingDamageDelegate);
		if (this.destroyOnDamaged)
		{
			base.Subscribe<BuildingHP>(774203113, BuildingHP.DestroyOnDamagedDelegate);
		}
		if (this.hitpoints <= 0)
		{
			base.Trigger(774203113, this);
		}
	}

	// Token: 0x06002180 RID: 8576 RVA: 0x000B6729 File Offset: 0x000B4929
	private void DestroyOnDamaged(object data)
	{
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06002181 RID: 8577 RVA: 0x000B6738 File Offset: 0x000B4938
	protected override void OnCompleteWork(Worker worker)
	{
		int num = (int)Db.Get().Attributes.Machinery.Lookup(worker).GetTotalValue();
		int num2 = 10 + Math.Max(0, num * 10);
		this.Repair(num2);
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x000B6776 File Offset: 0x000B4976
	private void OnDoBuildingDamage(object data)
	{
		if (this.invincible)
		{
			return;
		}
		this.damageSourceInfo = (BuildingHP.DamageSourceInfo)data;
		this.DoDamage(this.damageSourceInfo.damage);
		this.DoDamagePopFX(this.damageSourceInfo);
		this.DoTakeDamageFX(this.damageSourceInfo);
	}

	// Token: 0x06002183 RID: 8579 RVA: 0x000B67B8 File Offset: 0x000B49B8
	private void DoTakeDamageFX(BuildingHP.DamageSourceInfo info)
	{
		if (info.takeDamageEffect != SpawnFXHashes.None)
		{
			BuildingDef def = base.GetComponent<BuildingComplete>().Def;
			int num = Grid.OffsetCell(Grid.PosToCell(this), 0, def.HeightInCells - 1);
			Game.Instance.SpawnFX(info.takeDamageEffect, num, 0f);
		}
	}

	// Token: 0x06002184 RID: 8580 RVA: 0x000B6804 File Offset: 0x000B4A04
	private void DoDamagePopFX(BuildingHP.DamageSourceInfo info)
	{
		if (info.popString != null && Time.time > this.lastPopTime + this.minDamagePopInterval)
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Building, info.popString, base.gameObject.transform, 1.5f, false);
			this.lastPopTime = Time.time;
		}
	}

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x06002185 RID: 8581 RVA: 0x000B6864 File Offset: 0x000B4A64
	public bool IsBroken
	{
		get
		{
			return this.hitpoints == 0;
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x06002186 RID: 8582 RVA: 0x000B686F File Offset: 0x000B4A6F
	public bool NeedsRepairs
	{
		get
		{
			return this.HitPoints < this.building.Def.HitPoints;
		}
	}

	// Token: 0x0400133B RID: 4923
	[Serialize]
	[SerializeField]
	private int hitpoints;

	// Token: 0x0400133C RID: 4924
	[Serialize]
	private BuildingHP.DamageSourceInfo damageSourceInfo;

	// Token: 0x0400133D RID: 4925
	private static readonly EventSystem.IntraObjectHandler<BuildingHP> OnDoBuildingDamageDelegate = new EventSystem.IntraObjectHandler<BuildingHP>(delegate(BuildingHP component, object data)
	{
		component.OnDoBuildingDamage(data);
	});

	// Token: 0x0400133E RID: 4926
	private static readonly EventSystem.IntraObjectHandler<BuildingHP> DestroyOnDamagedDelegate = new EventSystem.IntraObjectHandler<BuildingHP>(delegate(BuildingHP component, object data)
	{
		component.DestroyOnDamaged(data);
	});

	// Token: 0x0400133F RID: 4927
	public static List<Meter> kbacQueryList = new List<Meter>();

	// Token: 0x04001340 RID: 4928
	public bool destroyOnDamaged;

	// Token: 0x04001341 RID: 4929
	public bool invincible;

	// Token: 0x04001342 RID: 4930
	[MyCmpGet]
	private Building building;

	// Token: 0x04001343 RID: 4931
	private BuildingHP.SMInstance smi;

	// Token: 0x04001344 RID: 4932
	private float minDamagePopInterval = 4f;

	// Token: 0x04001345 RID: 4933
	private float lastPopTime;

	// Token: 0x0200118E RID: 4494
	public struct DamageSourceInfo
	{
		// Token: 0x060076F6 RID: 30454 RVA: 0x002B98FB File Offset: 0x002B7AFB
		public override string ToString()
		{
			return this.source;
		}

		// Token: 0x04005B2B RID: 23339
		public int damage;

		// Token: 0x04005B2C RID: 23340
		public string source;

		// Token: 0x04005B2D RID: 23341
		public string popString;

		// Token: 0x04005B2E RID: 23342
		public SpawnFXHashes takeDamageEffect;

		// Token: 0x04005B2F RID: 23343
		public string fullDamageEffectName;

		// Token: 0x04005B30 RID: 23344
		public string statusItemID;
	}

	// Token: 0x0200118F RID: 4495
	public class SMInstance : GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.GameInstance
	{
		// Token: 0x060076F7 RID: 30455 RVA: 0x002B9903 File Offset: 0x002B7B03
		public SMInstance(BuildingHP master)
			: base(master)
		{
		}

		// Token: 0x060076F8 RID: 30456 RVA: 0x002B990C File Offset: 0x002B7B0C
		public Notification CreateBrokenMachineNotification()
		{
			return new Notification(MISC.NOTIFICATIONS.BROKENMACHINE.NAME, NotificationType.BadMinor, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.BROKENMACHINE.TOOLTIP + notificationList.ReduceMessages(false), "/t• " + base.master.damageSourceInfo.source, false, 0f, null, null, null, true, false, false);
		}

		// Token: 0x060076F9 RID: 30457 RVA: 0x002B9970 File Offset: 0x002B7B70
		public void ShowProgressBar(bool show)
		{
			if (show && Grid.IsValidCell(Grid.PosToCell(base.gameObject)) && Grid.IsVisible(Grid.PosToCell(base.gameObject)))
			{
				this.CreateProgressBar();
				return;
			}
			if (this.progressBar != null)
			{
				this.progressBar.gameObject.DeleteObject();
				this.progressBar = null;
			}
		}

		// Token: 0x060076FA RID: 30458 RVA: 0x002B99D0 File Offset: 0x002B7BD0
		public void UpdateMeter()
		{
			if (this.progressBar == null)
			{
				this.ShowProgressBar(true);
			}
			if (this.progressBar)
			{
				this.progressBar.Update();
			}
		}

		// Token: 0x060076FB RID: 30459 RVA: 0x002B99FF File Offset: 0x002B7BFF
		private float HealthPercent()
		{
			return (float)base.smi.master.HitPoints / (float)base.smi.master.building.Def.HitPoints;
		}

		// Token: 0x060076FC RID: 30460 RVA: 0x002B9A30 File Offset: 0x002B7C30
		private void CreateProgressBar()
		{
			if (this.progressBar != null)
			{
				return;
			}
			this.progressBar = Util.KInstantiateUI<ProgressBar>(ProgressBarsConfig.Instance.progressBarPrefab, null, false);
			this.progressBar.transform.SetParent(GameScreenManager.Instance.worldSpaceCanvas.transform);
			this.progressBar.name = base.smi.master.name + "." + base.smi.master.GetType().Name + " ProgressBar";
			this.progressBar.transform.Find("Bar").GetComponent<Image>().color = ProgressBarsConfig.Instance.GetBarColor("ProgressBar");
			this.progressBar.SetUpdateFunc(new Func<float>(this.HealthPercent));
			this.progressBar.barColor = ProgressBarsConfig.Instance.GetBarColor("HealthBar");
			CanvasGroup component = this.progressBar.GetComponent<CanvasGroup>();
			component.interactable = false;
			component.blocksRaycasts = false;
			this.progressBar.Update();
			float num = 0.15f;
			Vector3 vector = base.gameObject.transform.GetPosition() + Vector3.down * num;
			vector.z += 0.05f;
			Rotatable component2 = base.GetComponent<Rotatable>();
			if (component2 == null || component2.GetOrientation() == Orientation.Neutral || base.smi.master.building.Def.WidthInCells < 2 || base.smi.master.building.Def.HeightInCells < 2)
			{
				vector -= Vector3.right * 0.5f * (float)(base.smi.master.building.Def.WidthInCells % 2);
			}
			else
			{
				vector += Vector3.left * (1f + 0.5f * (float)(base.smi.master.building.Def.WidthInCells % 2));
			}
			this.progressBar.transform.SetPosition(vector);
			this.progressBar.gameObject.SetActive(true);
		}

		// Token: 0x060076FD RID: 30461 RVA: 0x002B9C64 File Offset: 0x002B7E64
		private static string ToolTipResolver(List<Notification> notificationList, object data)
		{
			string text = "";
			for (int i = 0; i < notificationList.Count; i++)
			{
				Notification notification = notificationList[i];
				text += string.Format(BUILDINGS.DAMAGESOURCES.NOTIFICATION_TOOLTIP, notification.NotifierName, (string)notification.tooltipData);
				if (i < notificationList.Count - 1)
				{
					text += "\n";
				}
			}
			return text;
		}

		// Token: 0x060076FE RID: 30462 RVA: 0x002B9CD0 File Offset: 0x002B7ED0
		public void ShowDamagedEffect()
		{
			if (base.master.damageSourceInfo.takeDamageEffect != SpawnFXHashes.None)
			{
				BuildingDef def = base.master.GetComponent<BuildingComplete>().Def;
				int num = Grid.OffsetCell(Grid.PosToCell(base.master), 0, def.HeightInCells - 1);
				Game.Instance.SpawnFX(base.master.damageSourceInfo.takeDamageEffect, num, 0f);
			}
		}

		// Token: 0x060076FF RID: 30463 RVA: 0x002B9D3C File Offset: 0x002B7F3C
		public FXAnim.Instance InstantiateDamageFX()
		{
			if (base.master.damageSourceInfo.fullDamageEffectName == null)
			{
				return null;
			}
			BuildingDef def = base.master.GetComponent<BuildingComplete>().Def;
			Vector3 zero = Vector3.zero;
			if (def.HeightInCells > 1)
			{
				zero = new Vector3(0f, (float)(def.HeightInCells - 1), 0f);
			}
			else
			{
				zero = new Vector3(0f, 0.5f, 0f);
			}
			return new FXAnim.Instance(base.smi.master, base.master.damageSourceInfo.fullDamageEffectName, "idle", KAnim.PlayMode.Loop, zero, Color.white);
		}

		// Token: 0x06007700 RID: 30464 RVA: 0x002B9DE0 File Offset: 0x002B7FE0
		public void SetCrackOverlayValue(float value)
		{
			KBatchedAnimController component = base.master.GetComponent<KBatchedAnimController>();
			if (component == null)
			{
				return;
			}
			component.SetBlendValue(value);
			BuildingHP.kbacQueryList.Clear();
			base.master.GetComponentsInChildren<Meter>(BuildingHP.kbacQueryList);
			for (int i = 0; i < BuildingHP.kbacQueryList.Count; i++)
			{
				BuildingHP.kbacQueryList[i].GetComponent<KBatchedAnimController>().SetBlendValue(value);
			}
		}

		// Token: 0x04005B31 RID: 23345
		private ProgressBar progressBar;
	}

	// Token: 0x02001190 RID: 4496
	public class States : GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP>
	{
		// Token: 0x06007701 RID: 30465 RVA: 0x002B9E50 File Offset: 0x002B8050
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.healthy;
			this.healthy.DefaultState(this.healthy.imperfect).EventTransition(GameHashes.BuildingReceivedDamage, this.damaged, (BuildingHP.SMInstance smi) => smi.master.HitPoints <= 0);
			this.healthy.imperfect.Enter(delegate(BuildingHP.SMInstance smi)
			{
				smi.ShowProgressBar(true);
			}).DefaultState(this.healthy.imperfect.playEffect).EventTransition(GameHashes.BuildingPartiallyRepaired, this.healthy.perfect, (BuildingHP.SMInstance smi) => smi.master.HitPoints == smi.master.building.Def.HitPoints)
				.EventHandler(GameHashes.BuildingPartiallyRepaired, delegate(BuildingHP.SMInstance smi)
				{
					smi.UpdateMeter();
				})
				.ToggleStatusItem(delegate(BuildingHP.SMInstance smi)
				{
					if (smi.master.damageSourceInfo.statusItemID == null)
					{
						return null;
					}
					return Db.Get().BuildingStatusItems.Get(smi.master.damageSourceInfo.statusItemID);
				}, null)
				.Exit(delegate(BuildingHP.SMInstance smi)
				{
					smi.ShowProgressBar(false);
				});
			this.healthy.imperfect.playEffect.Transition(this.healthy.imperfect.waiting, (BuildingHP.SMInstance smi) => true, UpdateRate.SIM_200ms);
			this.healthy.imperfect.waiting.ScheduleGoTo((BuildingHP.SMInstance smi) => UnityEngine.Random.Range(15f, 30f), this.healthy.imperfect.playEffect);
			this.healthy.perfect.EventTransition(GameHashes.BuildingReceivedDamage, this.healthy.imperfect, (BuildingHP.SMInstance smi) => smi.master.HitPoints < smi.master.building.Def.HitPoints);
			this.damaged.Enter(delegate(BuildingHP.SMInstance smi)
			{
				Operational component = smi.GetComponent<Operational>();
				if (component != null)
				{
					component.SetFlag(BuildingHP.States.healthyFlag, false);
				}
				smi.ShowProgressBar(true);
				smi.master.Trigger(774203113, smi.master);
				smi.SetCrackOverlayValue(1f);
			}).ToggleNotification((BuildingHP.SMInstance smi) => smi.CreateBrokenMachineNotification()).ToggleStatusItem(Db.Get().BuildingStatusItems.Broken, null)
				.ToggleFX((BuildingHP.SMInstance smi) => smi.InstantiateDamageFX())
				.EventTransition(GameHashes.BuildingPartiallyRepaired, this.healthy.perfect, (BuildingHP.SMInstance smi) => smi.master.HitPoints == smi.master.building.Def.HitPoints)
				.EventHandler(GameHashes.BuildingPartiallyRepaired, delegate(BuildingHP.SMInstance smi)
				{
					smi.UpdateMeter();
				})
				.Exit(delegate(BuildingHP.SMInstance smi)
				{
					Operational component2 = smi.GetComponent<Operational>();
					if (component2 != null)
					{
						component2.SetFlag(BuildingHP.States.healthyFlag, true);
					}
					smi.ShowProgressBar(false);
					smi.SetCrackOverlayValue(0f);
				});
		}

		// Token: 0x06007702 RID: 30466 RVA: 0x002BA174 File Offset: 0x002B8374
		private Chore CreateRepairChore(BuildingHP.SMInstance smi)
		{
			return new WorkChore<BuildingHP>(Db.Get().ChoreTypes.Repair, smi.master, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x04005B32 RID: 23346
		private static readonly Operational.Flag healthyFlag = new Operational.Flag("healthy", Operational.Flag.Type.Functional);

		// Token: 0x04005B33 RID: 23347
		public GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State damaged;

		// Token: 0x04005B34 RID: 23348
		public BuildingHP.States.Healthy healthy;

		// Token: 0x02001F8B RID: 8075
		public class Healthy : GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State
		{
			// Token: 0x04008C37 RID: 35895
			public BuildingHP.States.ImperfectStates imperfect;

			// Token: 0x04008C38 RID: 35896
			public GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State perfect;
		}

		// Token: 0x02001F8C RID: 8076
		public class ImperfectStates : GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State
		{
			// Token: 0x04008C39 RID: 35897
			public GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State playEffect;

			// Token: 0x04008C3A RID: 35898
			public GameStateMachine<BuildingHP.States, BuildingHP.SMInstance, BuildingHP, object>.State waiting;
		}
	}
}
