using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000376 RID: 886
public class BansheeChore : Chore<BansheeChore.StatesInstance>
{
	// Token: 0x06001224 RID: 4644 RVA: 0x00060080 File Offset: 0x0005E280
	public BansheeChore(ChoreType chore_type, IStateMachineTarget target, Notification notification, Action<Chore> on_complete = null)
		: base(Db.Get().ChoreTypes.BansheeWail, target, target.GetComponent<ChoreProvider>(), false, on_complete, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new BansheeChore.StatesInstance(this, target.gameObject, notification);
	}

	// Token: 0x040009B3 RID: 2483
	private const string audienceEffectName = "WailedAt";

	// Token: 0x02000F4F RID: 3919
	public class StatesInstance : GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.GameInstance
	{
		// Token: 0x06006EED RID: 28397 RVA: 0x0029E028 File Offset: 0x0029C228
		public StatesInstance(BansheeChore master, GameObject wailer, Notification notification)
			: base(master)
		{
			base.sm.wailer.Set(wailer, base.smi, false);
			this.notification = notification;
		}

		// Token: 0x06006EEE RID: 28398 RVA: 0x0029E054 File Offset: 0x0029C254
		public void FindAudience()
		{
			Navigator component = base.GetComponent<Navigator>();
			int num = (int)Grid.WorldIdx[Grid.PosToCell(base.gameObject)];
			int num2 = int.MaxValue;
			int num3 = Grid.InvalidCell;
			List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(num, false);
			for (int i = 0; i < worldItems.Count; i++)
			{
				if (!worldItems[i].IsNullOrDestroyed() && !(worldItems[i].gameObject == base.gameObject))
				{
					int num4 = Grid.PosToCell(worldItems[i]);
					if (component.CanReach(num4) && !worldItems[i].GetComponent<Effects>().HasEffect("WailedAt"))
					{
						int navigationCost = component.GetNavigationCost(num4);
						if (navigationCost < num2)
						{
							num2 = navigationCost;
							num3 = num4;
						}
					}
				}
			}
			if (num3 == Grid.InvalidCell)
			{
				num3 = this.FindIdleCell();
			}
			base.sm.targetWailLocation.Set(num3, base.smi, false);
			this.GoTo(base.sm.moveToAudience);
		}

		// Token: 0x06006EEF RID: 28399 RVA: 0x0029E15C File Offset: 0x0029C35C
		public int FindIdleCell()
		{
			Navigator component = base.smi.master.GetComponent<Navigator>();
			MinionPathFinderAbilities minionPathFinderAbilities = (MinionPathFinderAbilities)component.GetCurrentAbilities();
			minionPathFinderAbilities.SetIdleNavMaskEnabled(true);
			IdleCellQuery idleCellQuery = PathFinderQueries.idleCellQuery.Reset(base.GetComponent<MinionBrain>(), UnityEngine.Random.Range(30, 90));
			component.RunQuery(idleCellQuery);
			minionPathFinderAbilities.SetIdleNavMaskEnabled(false);
			return idleCellQuery.GetResultCell();
		}

		// Token: 0x06006EF0 RID: 28400 RVA: 0x0029E1BC File Offset: 0x0029C3BC
		public void BotherAudience(float dt)
		{
			if (dt <= 0f)
			{
				return;
			}
			int num = Grid.PosToCell(base.smi.master.gameObject);
			int num2 = (int)Grid.WorldIdx[num];
			foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.GetWorldItems(num2, false))
			{
				if (!minionIdentity.IsNullOrDestroyed() && !(minionIdentity.gameObject == base.smi.master.gameObject))
				{
					int num3 = Grid.PosToCell(minionIdentity);
					if (Grid.GetCellDistance(num, Grid.PosToCell(minionIdentity)) <= STRESS.BANSHEE_WAIL_RADIUS)
					{
						HashSet<int> hashSet = new HashSet<int>();
						Grid.CollectCellsInLine(num, num3, hashSet);
						bool flag = false;
						foreach (int num4 in hashSet)
						{
							if (Grid.Solid[num4])
							{
								flag = true;
								break;
							}
						}
						if (!flag && !minionIdentity.GetComponent<Effects>().HasEffect("WailedAt"))
						{
							minionIdentity.GetComponent<Effects>().Add("WailedAt", true);
							minionIdentity.GetSMI<ThreatMonitor.Instance>().ClearMainThreat();
							new FleeChore(minionIdentity.GetComponent<IStateMachineTarget>(), base.smi.master.gameObject);
						}
					}
				}
			}
		}

		// Token: 0x040053D2 RID: 21458
		public Notification notification;
	}

	// Token: 0x02000F50 RID: 3920
	public class States : GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore>
	{
		// Token: 0x06006EF1 RID: 28401 RVA: 0x0029E34C File Offset: 0x0029C54C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.findAudience;
			base.Target(this.wailer);
			this.wailPreEffect = new Effect("BansheeWailing", DUPLICANTS.MODIFIERS.BANSHEE_WAILING.NAME, DUPLICANTS.MODIFIERS.BANSHEE_WAILING.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
			this.wailPreEffect.Add(new AttributeModifier("AirConsumptionRate", 7.5f, null, false, false, true));
			Db.Get().effects.Add(this.wailPreEffect);
			this.wailRecoverEffect = new Effect("BansheeWailingRecovery", DUPLICANTS.MODIFIERS.BANSHEE_WAILING_RECOVERY.NAME, DUPLICANTS.MODIFIERS.BANSHEE_WAILING_RECOVERY.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
			this.wailRecoverEffect.Add(new AttributeModifier("AirConsumptionRate", 1f, null, false, false, true));
			Db.Get().effects.Add(this.wailRecoverEffect);
			this.findAudience.Enter("FindAudience", delegate(BansheeChore.StatesInstance smi)
			{
				smi.FindAudience();
			}).ToggleAnims("anim_loco_banshee_kanim", 0f, "");
			this.moveToAudience.MoveTo((BansheeChore.StatesInstance smi) => smi.sm.targetWailLocation.Get(smi), this.wail, this.delay, false).ToggleAnims("anim_loco_banshee_kanim", 0f, "");
			this.wail.defaultState = this.wail.pre.DoNotification((BansheeChore.StatesInstance smi) => smi.notification);
			this.wail.pre.ToggleAnims("anim_banshee_kanim", 0f, "").PlayAnim("working_pre").ToggleEffect((BansheeChore.StatesInstance smi) => this.wailPreEffect)
				.OnAnimQueueComplete(this.wail.loop);
			this.wail.loop.ToggleAnims("anim_banshee_kanim", 0f, "").Enter(delegate(BansheeChore.StatesInstance smi)
			{
				smi.Play("working_loop", KAnim.PlayMode.Loop);
				AcousticDisturbance.Emit(smi.master.gameObject, STRESS.BANSHEE_WAIL_RADIUS);
			}).ScheduleGoTo(5f, this.wail.pst)
				.Update(delegate(BansheeChore.StatesInstance smi, float dt)
				{
					smi.BotherAudience(dt);
				}, UpdateRate.SIM_200ms, false);
			this.wail.pst.ToggleAnims("anim_banshee_kanim", 0f, "").QueueAnim("working_pst", false, null).EventHandlerTransition(GameHashes.AnimQueueComplete, this.recover, (BansheeChore.StatesInstance smi, object data) => true)
				.ScheduleGoTo(3f, this.recover);
			this.recover.ToggleEffect((BansheeChore.StatesInstance smi) => this.wailRecoverEffect).ToggleAnims("anim_emotes_default_kanim", 0f, "").QueueAnim("breathe_pre", false, null)
				.QueueAnim("breathe_loop", false, null)
				.QueueAnim("breathe_loop", false, null)
				.QueueAnim("breathe_loop", false, null)
				.QueueAnim("breathe_pst", false, null)
				.OnAnimQueueComplete(this.complete);
			this.delay.ScheduleGoTo(1f, this.wander);
			this.wander.MoveTo((BansheeChore.StatesInstance smi) => smi.FindIdleCell(), this.findAudience, this.findAudience, false).ToggleAnims("anim_loco_banshee_kanim", 0f, "");
			this.complete.Enter(delegate(BansheeChore.StatesInstance smi)
			{
				smi.StopSM("complete");
			});
		}

		// Token: 0x040053D3 RID: 21459
		public StateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.TargetParameter wailer;

		// Token: 0x040053D4 RID: 21460
		public StateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.IntParameter targetWailLocation;

		// Token: 0x040053D5 RID: 21461
		public GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State findAudience;

		// Token: 0x040053D6 RID: 21462
		public GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State moveToAudience;

		// Token: 0x040053D7 RID: 21463
		public BansheeChore.States.Wail wail;

		// Token: 0x040053D8 RID: 21464
		public GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State recover;

		// Token: 0x040053D9 RID: 21465
		public GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State delay;

		// Token: 0x040053DA RID: 21466
		public GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State wander;

		// Token: 0x040053DB RID: 21467
		public GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State complete;

		// Token: 0x040053DC RID: 21468
		private Effect wailPreEffect;

		// Token: 0x040053DD RID: 21469
		private Effect wailRecoverEffect;

		// Token: 0x02001EA2 RID: 7842
		public class Wail : GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State
		{
			// Token: 0x0400893B RID: 35131
			public GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State pre;

			// Token: 0x0400893C RID: 35132
			public GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State loop;

			// Token: 0x0400893D RID: 35133
			public GameStateMachine<BansheeChore.States, BansheeChore.StatesInstance, BansheeChore, object>.State pst;
		}
	}
}
