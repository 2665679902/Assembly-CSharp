using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200089B RID: 2203
public class StandardCropPlant : StateMachineComponent<StandardCropPlant.StatesInstance>
{
	// Token: 0x06003F23 RID: 16163 RVA: 0x00160B1A File Offset: 0x0015ED1A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06003F24 RID: 16164 RVA: 0x00160B2D File Offset: 0x0015ED2D
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06003F25 RID: 16165 RVA: 0x00160B48 File Offset: 0x0015ED48
	public Notification CreateDeathNotification()
	{
		return new Notification(CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION, NotificationType.Bad, (List<Notification> notificationList, object data) => CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), "/t• " + base.gameObject.GetProperName(), true, 0f, null, null, null, true, false, false);
	}

	// Token: 0x06003F26 RID: 16166 RVA: 0x00160BA5 File Offset: 0x0015EDA5
	public void RefreshPositionPercent()
	{
		this.animController.SetPositionPercent(this.growing.PercentOfCurrentHarvest());
	}

	// Token: 0x06003F27 RID: 16167 RVA: 0x00160BC0 File Offset: 0x0015EDC0
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
		return string.Format(CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION_TOOLTIP, text);
	}

	// Token: 0x04002971 RID: 10609
	private const int WILT_LEVELS = 3;

	// Token: 0x04002972 RID: 10610
	[MyCmpReq]
	private Crop crop;

	// Token: 0x04002973 RID: 10611
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x04002974 RID: 10612
	[MyCmpReq]
	private ReceptacleMonitor rm;

	// Token: 0x04002975 RID: 10613
	[MyCmpReq]
	private Growing growing;

	// Token: 0x04002976 RID: 10614
	[MyCmpReq]
	private KAnimControllerBase animController;

	// Token: 0x04002977 RID: 10615
	[MyCmpGet]
	private Harvestable harvestable;

	// Token: 0x04002978 RID: 10616
	public static StandardCropPlant.AnimSet defaultAnimSet = new StandardCropPlant.AnimSet
	{
		grow = "grow",
		grow_pst = "grow_pst",
		idle_full = "idle_full",
		wilt_base = "wilt",
		harvest = "harvest",
		waning = "waning"
	};

	// Token: 0x04002979 RID: 10617
	public StandardCropPlant.AnimSet anims = StandardCropPlant.defaultAnimSet;

	// Token: 0x0200166E RID: 5742
	public class AnimSet
	{
		// Token: 0x060087B9 RID: 34745 RVA: 0x002F3898 File Offset: 0x002F1A98
		public string GetWiltLevel(int level)
		{
			if (this.m_wilt == null)
			{
				this.m_wilt = new string[3];
				for (int i = 0; i < 3; i++)
				{
					this.m_wilt[i] = this.wilt_base + (i + 1).ToString();
				}
			}
			return this.m_wilt[level - 1];
		}

		// Token: 0x040069B6 RID: 27062
		public string grow;

		// Token: 0x040069B7 RID: 27063
		public string grow_pst;

		// Token: 0x040069B8 RID: 27064
		public string idle_full;

		// Token: 0x040069B9 RID: 27065
		public string wilt_base;

		// Token: 0x040069BA RID: 27066
		public string harvest;

		// Token: 0x040069BB RID: 27067
		public string waning;

		// Token: 0x040069BC RID: 27068
		private string[] m_wilt;
	}

	// Token: 0x0200166F RID: 5743
	public class States : GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant>
	{
		// Token: 0x060087BB RID: 34747 RVA: 0x002F38F8 File Offset: 0x002F1AF8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.alive;
			this.dead.ToggleMainStatusItem(Db.Get().CreatureStatusItems.Dead, null).Enter(delegate(StandardCropPlant.StatesInstance smi)
			{
				if (smi.master.rm.Replanted && !smi.master.GetComponent<KPrefabID>().HasTag(GameTags.Uprooted))
				{
					Notifier notifier = smi.master.gameObject.AddOrGet<Notifier>();
					Notification notification = smi.master.CreateDeathNotification();
					notifier.Add(notification, "");
				}
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				Harvestable component = smi.master.GetComponent<Harvestable>();
				if (component != null && component.CanBeHarvested && GameScheduler.Instance != null)
				{
					GameScheduler.Instance.Schedule("SpawnFruit", 0.2f, new Action<object>(smi.master.crop.SpawnConfiguredFruit), null, null);
				}
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
			this.blighted.InitializeStates(this.masterTarget, this.dead).PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.waning, KAnim.PlayMode.Once).ToggleMainStatusItem(Db.Get().CreatureStatusItems.Crop_Blighted, null)
				.TagTransition(GameTags.Blighted, this.alive, true);
			this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.idle).ToggleComponent<Growing>(false)
				.TagTransition(GameTags.Blighted, this.blighted, false);
			this.alive.idle.EventTransition(GameHashes.Wilt, this.alive.wilting, (StandardCropPlant.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).EventTransition(GameHashes.Grow, this.alive.pre_fruiting, (StandardCropPlant.StatesInstance smi) => smi.master.growing.ReachedNextHarvest()).EventTransition(GameHashes.CropSleep, this.alive.sleeping, new StateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.Transition.ConditionCallback(this.IsSleeping))
				.PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.grow, KAnim.PlayMode.Paused)
				.Enter(new StateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State.Callback(StandardCropPlant.States.RefreshPositionPercent))
				.Update(new Action<StandardCropPlant.StatesInstance, float>(StandardCropPlant.States.RefreshPositionPercent), UpdateRate.SIM_4000ms, false)
				.EventHandler(GameHashes.ConsumePlant, new StateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State.Callback(StandardCropPlant.States.RefreshPositionPercent));
			this.alive.pre_fruiting.PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.grow_pst, KAnim.PlayMode.Once).TriggerOnEnter(GameHashes.BurstEmitDisease, null).EventTransition(GameHashes.AnimQueueComplete, this.alive.fruiting, null);
			this.alive.fruiting_lost.Enter(delegate(StandardCropPlant.StatesInstance smi)
			{
				if (smi.master.harvestable != null)
				{
					smi.master.harvestable.SetCanBeHarvested(false);
				}
			}).GoTo(this.alive.idle);
			this.alive.wilting.PlayAnim(new Func<StandardCropPlant.StatesInstance, string>(StandardCropPlant.States.GetWiltAnim), KAnim.PlayMode.Loop).EventTransition(GameHashes.WiltRecover, this.alive.idle, (StandardCropPlant.StatesInstance smi) => !smi.master.wiltCondition.IsWilting()).EventTransition(GameHashes.Harvest, this.alive.harvest, null);
			this.alive.sleeping.PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.grow, KAnim.PlayMode.Once).EventTransition(GameHashes.CropWakeUp, this.alive.idle, GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.Not(new StateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.Transition.ConditionCallback(this.IsSleeping))).EventTransition(GameHashes.Harvest, this.alive.harvest, null)
				.EventTransition(GameHashes.Wilt, this.alive.wilting, null);
			this.alive.fruiting.DefaultState(this.alive.fruiting.fruiting_idle).EventTransition(GameHashes.Wilt, this.alive.wilting, null).EventTransition(GameHashes.Harvest, this.alive.harvest, null)
				.EventTransition(GameHashes.Grow, this.alive.fruiting_lost, (StandardCropPlant.StatesInstance smi) => !smi.master.growing.ReachedNextHarvest());
			this.alive.fruiting.fruiting_idle.PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.idle_full, KAnim.PlayMode.Loop).Enter(delegate(StandardCropPlant.StatesInstance smi)
			{
				if (smi.master.harvestable != null)
				{
					smi.master.harvestable.SetCanBeHarvested(true);
				}
			}).Transition(this.alive.fruiting.fruiting_old, new StateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.Transition.ConditionCallback(this.IsOld), UpdateRate.SIM_4000ms);
			this.alive.fruiting.fruiting_old.PlayAnim(new Func<StandardCropPlant.StatesInstance, string>(StandardCropPlant.States.GetWiltAnim), KAnim.PlayMode.Loop).Enter(delegate(StandardCropPlant.StatesInstance smi)
			{
				if (smi.master.harvestable != null)
				{
					smi.master.harvestable.SetCanBeHarvested(true);
				}
			}).Transition(this.alive.fruiting.fruiting_idle, GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.Not(new StateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.Transition.ConditionCallback(this.IsOld)), UpdateRate.SIM_4000ms);
			this.alive.harvest.PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.harvest, KAnim.PlayMode.Once).Enter(delegate(StandardCropPlant.StatesInstance smi)
			{
				if (GameScheduler.Instance != null && smi.master != null)
				{
					GameScheduler.Instance.Schedule("SpawnFruit", 0.2f, new Action<object>(smi.master.crop.SpawnConfiguredFruit), null, null);
				}
				if (smi.master.harvestable != null)
				{
					smi.master.harvestable.SetCanBeHarvested(false);
				}
			}).Exit(delegate(StandardCropPlant.StatesInstance smi)
			{
				smi.Trigger(113170146, null);
			})
				.OnAnimQueueComplete(this.alive.idle);
		}

		// Token: 0x060087BC RID: 34748 RVA: 0x002F3E5C File Offset: 0x002F205C
		private static string GetWiltAnim(StandardCropPlant.StatesInstance smi)
		{
			float num = smi.master.growing.PercentOfCurrentHarvest();
			int num2;
			if (num < 0.75f)
			{
				num2 = 1;
			}
			else if (num < 1f)
			{
				num2 = 2;
			}
			else
			{
				num2 = 3;
			}
			return smi.master.anims.GetWiltLevel(num2);
		}

		// Token: 0x060087BD RID: 34749 RVA: 0x002F3EA5 File Offset: 0x002F20A5
		private static void RefreshPositionPercent(StandardCropPlant.StatesInstance smi, float dt)
		{
			smi.master.RefreshPositionPercent();
		}

		// Token: 0x060087BE RID: 34750 RVA: 0x002F3EB2 File Offset: 0x002F20B2
		private static void RefreshPositionPercent(StandardCropPlant.StatesInstance smi)
		{
			smi.master.RefreshPositionPercent();
		}

		// Token: 0x060087BF RID: 34751 RVA: 0x002F3EBF File Offset: 0x002F20BF
		public bool IsOld(StandardCropPlant.StatesInstance smi)
		{
			return smi.master.growing.PercentOldAge() > 0.5f;
		}

		// Token: 0x060087C0 RID: 34752 RVA: 0x002F3ED8 File Offset: 0x002F20D8
		public bool IsSleeping(StandardCropPlant.StatesInstance smi)
		{
			CropSleepingMonitor.Instance smi2 = smi.master.GetSMI<CropSleepingMonitor.Instance>();
			return smi2 != null && smi2.IsSleeping();
		}

		// Token: 0x040069BD RID: 27069
		public StandardCropPlant.States.AliveStates alive;

		// Token: 0x040069BE RID: 27070
		public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State dead;

		// Token: 0x040069BF RID: 27071
		public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.PlantAliveSubState blighted;

		// Token: 0x0200209E RID: 8350
		public class AliveStates : GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.PlantAliveSubState
		{
			// Token: 0x04009158 RID: 37208
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State idle;

			// Token: 0x04009159 RID: 37209
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State pre_fruiting;

			// Token: 0x0400915A RID: 37210
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State fruiting_lost;

			// Token: 0x0400915B RID: 37211
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State barren;

			// Token: 0x0400915C RID: 37212
			public StandardCropPlant.States.FruitingState fruiting;

			// Token: 0x0400915D RID: 37213
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State wilting;

			// Token: 0x0400915E RID: 37214
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State destroy;

			// Token: 0x0400915F RID: 37215
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State harvest;

			// Token: 0x04009160 RID: 37216
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State sleeping;
		}

		// Token: 0x0200209F RID: 8351
		public class FruitingState : GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State
		{
			// Token: 0x04009161 RID: 37217
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State fruiting_idle;

			// Token: 0x04009162 RID: 37218
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State fruiting_old;
		}
	}

	// Token: 0x02001670 RID: 5744
	public class StatesInstance : GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.GameInstance
	{
		// Token: 0x060087C2 RID: 34754 RVA: 0x002F3F04 File Offset: 0x002F2104
		public StatesInstance(StandardCropPlant master)
			: base(master)
		{
		}
	}
}
