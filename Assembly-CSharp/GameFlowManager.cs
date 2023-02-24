using System;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000781 RID: 1921
[SerializationConfig(MemberSerialization.OptIn)]
public class GameFlowManager : StateMachineComponent<GameFlowManager.StatesInstance>, ISaveLoadable
{
	// Token: 0x06003530 RID: 13616 RVA: 0x001201CF File Offset: 0x0011E3CF
	public static void DestroyInstance()
	{
		GameFlowManager.Instance = null;
	}

	// Token: 0x06003531 RID: 13617 RVA: 0x001201D7 File Offset: 0x0011E3D7
	protected override void OnPrefabInit()
	{
		GameFlowManager.Instance = this;
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x001201DF File Offset: 0x0011E3DF
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x001201EC File Offset: 0x0011E3EC
	public bool IsGameOver()
	{
		return base.smi.IsInsideState(base.smi.sm.gameover);
	}

	// Token: 0x0400213A RID: 8506
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x0400213B RID: 8507
	public static GameFlowManager Instance;

	// Token: 0x0200148F RID: 5263
	public class StatesInstance : GameStateMachine<GameFlowManager.States, GameFlowManager.StatesInstance, GameFlowManager, object>.GameInstance
	{
		// Token: 0x0600815A RID: 33114 RVA: 0x002E1647 File Offset: 0x002DF847
		public bool IsIncapacitated(GameObject go)
		{
			return false;
		}

		// Token: 0x0600815B RID: 33115 RVA: 0x002E164C File Offset: 0x002DF84C
		public void CheckForGameOver()
		{
			if (!Game.Instance.GameStarted())
			{
				return;
			}
			if (GenericGameSettings.instance.disableGameOver)
			{
				return;
			}
			bool flag = false;
			if (Components.LiveMinionIdentities.Count == 0)
			{
				flag = true;
			}
			else
			{
				flag = true;
				foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
				{
					if (!this.IsIncapacitated(minionIdentity.gameObject))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				this.GoTo(base.sm.gameover.pending);
			}
		}

		// Token: 0x0600815C RID: 33116 RVA: 0x002E16F8 File Offset: 0x002DF8F8
		public StatesInstance(GameFlowManager smi)
			: base(smi)
		{
		}

		// Token: 0x040063CF RID: 25551
		public Notification colonyLostNotification = new Notification(MISC.NOTIFICATIONS.COLONYLOST.NAME, NotificationType.Bad, null, null, false, 0f, null, null, null, true, false, false);
	}

	// Token: 0x02001490 RID: 5264
	public class States : GameStateMachine<GameFlowManager.States, GameFlowManager.StatesInstance, GameFlowManager>
	{
		// Token: 0x0600815D RID: 33117 RVA: 0x002E1730 File Offset: 0x002DF930
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.loading;
			this.loading.ScheduleGoTo(4f, this.running);
			this.running.Update("CheckForGameOver", delegate(GameFlowManager.StatesInstance smi, float dt)
			{
				smi.CheckForGameOver();
			}, UpdateRate.SIM_200ms, false);
			this.gameover.TriggerOnEnter(GameHashes.GameOver, null).ToggleNotification((GameFlowManager.StatesInstance smi) => smi.colonyLostNotification);
			this.gameover.pending.Enter("Goto(gameover.active)", delegate(GameFlowManager.StatesInstance smi)
			{
				UIScheduler.Instance.Schedule("Goto(gameover.active)", 4f, delegate(object d)
				{
					smi.GoTo(this.gameover.active);
				}, null, null);
			});
			this.gameover.active.Enter(delegate(GameFlowManager.StatesInstance smi)
			{
				if (GenericGameSettings.instance.demoMode)
				{
					DemoTimer.Instance.EndDemo();
					return;
				}
				GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.GameOverScreen, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay).GetComponent<KScreen>().Show(true);
			});
		}

		// Token: 0x040063D0 RID: 25552
		public GameStateMachine<GameFlowManager.States, GameFlowManager.StatesInstance, GameFlowManager, object>.State loading;

		// Token: 0x040063D1 RID: 25553
		public GameStateMachine<GameFlowManager.States, GameFlowManager.StatesInstance, GameFlowManager, object>.State running;

		// Token: 0x040063D2 RID: 25554
		public GameFlowManager.States.GameOverState gameover;

		// Token: 0x02002053 RID: 8275
		public class GameOverState : GameStateMachine<GameFlowManager.States, GameFlowManager.StatesInstance, GameFlowManager, object>.State
		{
			// Token: 0x04009006 RID: 36870
			public GameStateMachine<GameFlowManager.States, GameFlowManager.StatesInstance, GameFlowManager, object>.State pending;

			// Token: 0x04009007 RID: 36871
			public GameStateMachine<GameFlowManager.States, GameFlowManager.StatesInstance, GameFlowManager, object>.State active;
		}
	}
}
