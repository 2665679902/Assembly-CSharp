using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000B0 RID: 176
public class ConduitSleepStates : GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>
{
	// Token: 0x0600030E RID: 782 RVA: 0x00018514 File Offset: 0x00016714
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.connector.moveToSleepLocation;
		this.root.EventTransition(GameHashes.NewDay, (ConduitSleepStates.Instance smi) => GameClock.Instance, this.behaviourcomplete, null).Exit(new StateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State.Callback(ConduitSleepStates.CleanUp));
		this.connector.moveToSleepLocation.ToggleStatusItem(CREATURES.STATUSITEMS.DROWSY.NAME, CREATURES.STATUSITEMS.DROWSY.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).MoveTo(delegate(ConduitSleepStates.Instance smi)
		{
			ConduitSleepMonitor.Instance smi2 = smi.GetSMI<ConduitSleepMonitor.Instance>();
			return smi2.sm.targetSleepCell.Get(smi2);
		}, this.drowsy, this.behaviourcomplete, false);
		this.drowsy.ToggleStatusItem(CREATURES.STATUSITEMS.DROWSY.NAME, CREATURES.STATUSITEMS.DROWSY.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Enter(delegate(ConduitSleepStates.Instance smi)
		{
			smi.GetComponent<Navigator>().SetCurrentNavType(NavType.Ceiling);
		}).Enter(delegate(ConduitSleepStates.Instance smi)
		{
			if (GameClock.Instance.IsNighttime())
			{
				smi.GoTo(this.connector.sleep);
			}
		})
			.DefaultState(this.drowsy.loop);
		this.drowsy.loop.PlayAnim("drowsy_pre").QueueAnim("drowsy_loop", true, null).EventTransition(GameHashes.Nighttime, (ConduitSleepStates.Instance smi) => GameClock.Instance, this.drowsy.pst, (ConduitSleepStates.Instance smi) => GameClock.Instance.IsNighttime());
		this.drowsy.pst.PlayAnim("drowsy_pst").OnAnimQueueComplete(this.connector.sleep);
		this.connector.sleep.ToggleStatusItem(CREATURES.STATUSITEMS.SLEEPING.NAME, CREATURES.STATUSITEMS.SLEEPING.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).Enter(delegate(ConduitSleepStates.Instance smi)
		{
			if (!smi.staterpillar.IsConnectorBuildingSpawned())
			{
				smi.GoTo(this.behaviourcomplete);
				return;
			}
			smi.GetComponent<Navigator>().SetCurrentNavType(NavType.Ceiling);
			smi.staterpillar.EnableConnector();
			if (smi.staterpillar.IsConnected())
			{
				smi.GoTo(this.connector.sleep.connected);
				return;
			}
			smi.GoTo(this.connector.sleep.noConnection);
		});
		this.connector.sleep.connected.Enter(delegate(ConduitSleepStates.Instance smi)
		{
			smi.animController.SetSceneLayer(ConduitSleepStates.GetSleepingLayer(smi));
		}).Exit(delegate(ConduitSleepStates.Instance smi)
		{
			smi.animController.SetSceneLayer(Grid.SceneLayer.Creatures);
		}).EventTransition(GameHashes.NewDay, (ConduitSleepStates.Instance smi) => GameClock.Instance, this.connector.connectedWake, null)
			.Transition(this.connector.sleep.noConnection, (ConduitSleepStates.Instance smi) => !smi.staterpillar.IsConnected(), UpdateRate.SIM_200ms)
			.PlayAnim("sleep_charging_pre")
			.QueueAnim("sleep_charging_loop", true, null)
			.Update(new Action<ConduitSleepStates.Instance, float>(ConduitSleepStates.UpdateGulpSymbol), UpdateRate.SIM_1000ms, false)
			.EventHandler(GameHashes.OnStorageChange, new GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.GameEvent.Callback(ConduitSleepStates.OnStorageChanged));
		this.connector.sleep.noConnection.PlayAnim("sleep_pre").QueueAnim("sleep_loop", true, null).ToggleStatusItem(new Func<ConduitSleepStates.Instance, StatusItem>(ConduitSleepStates.GetStatusItem), null)
			.EventTransition(GameHashes.NewDay, (ConduitSleepStates.Instance smi) => GameClock.Instance, this.connector.noConnectionWake, null)
			.Transition(this.connector.sleep.connected, (ConduitSleepStates.Instance smi) => smi.staterpillar.IsConnected(), UpdateRate.SIM_200ms);
		this.connector.connectedWake.QueueAnim("sleep_charging_pst", false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.connector.noConnectionWake.QueueAnim("sleep_pst", false, null).OnAnimQueueComplete(this.behaviourcomplete);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsConduitConnection, false);
	}

	// Token: 0x0600030F RID: 783 RVA: 0x00018974 File Offset: 0x00016B74
	private static Grid.SceneLayer GetSleepingLayer(ConduitSleepStates.Instance smi)
	{
		ObjectLayer conduitLayer = smi.staterpillar.conduitLayer;
		Grid.SceneLayer sceneLayer;
		if (conduitLayer != ObjectLayer.GasConduit)
		{
			if (conduitLayer != ObjectLayer.LiquidConduit)
			{
				if (conduitLayer == ObjectLayer.Wire)
				{
					sceneLayer = Grid.SceneLayer.SolidConduitBridges;
				}
				else
				{
					sceneLayer = Grid.SceneLayer.SolidConduitBridges;
				}
			}
			else
			{
				sceneLayer = Grid.SceneLayer.GasConduitBridges;
			}
		}
		else
		{
			sceneLayer = Grid.SceneLayer.Gas;
		}
		return sceneLayer;
	}

	// Token: 0x06000310 RID: 784 RVA: 0x000189B0 File Offset: 0x00016BB0
	private static StatusItem GetStatusItem(ConduitSleepStates.Instance smi)
	{
		ObjectLayer conduitLayer = smi.staterpillar.conduitLayer;
		StatusItem statusItem;
		if (conduitLayer != ObjectLayer.GasConduit)
		{
			if (conduitLayer != ObjectLayer.LiquidConduit)
			{
				if (conduitLayer == ObjectLayer.Wire)
				{
					statusItem = Db.Get().BuildingStatusItems.NoWireConnected;
				}
				else
				{
					statusItem = Db.Get().BuildingStatusItems.Normal;
				}
			}
			else
			{
				statusItem = Db.Get().BuildingStatusItems.NeedLiquidOut;
			}
		}
		else
		{
			statusItem = Db.Get().BuildingStatusItems.NeedGasOut;
		}
		return statusItem;
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00018A20 File Offset: 0x00016C20
	private static void OnStorageChanged(ConduitSleepStates.Instance smi, object obj)
	{
		GameObject gameObject = obj as GameObject;
		if (gameObject != null)
		{
			smi.amountDeposited += gameObject.GetComponent<PrimaryElement>().Mass;
		}
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00018A55 File Offset: 0x00016C55
	private static void UpdateGulpSymbol(ConduitSleepStates.Instance smi, float dt)
	{
		smi.SetGulpSymbolVisibility(smi.amountDeposited > 0f);
		smi.amountDeposited = 0f;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x00018A78 File Offset: 0x00016C78
	private static void CleanUp(ConduitSleepStates.Instance smi)
	{
		ConduitSleepMonitor.Instance smi2 = smi.GetSMI<ConduitSleepMonitor.Instance>();
		if (smi2 != null)
		{
			smi2.sm.targetSleepCell.Set(Grid.InvalidCell, smi2, false);
		}
		smi.staterpillar.DestroyOrphanedConnectorBuilding();
	}

	// Token: 0x04000201 RID: 513
	public ConduitSleepStates.DrowsyStates drowsy;

	// Token: 0x04000202 RID: 514
	public ConduitSleepStates.HasConnectorStates connector;

	// Token: 0x04000203 RID: 515
	public GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State behaviourcomplete;

	// Token: 0x02000E27 RID: 3623
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040050FA RID: 20730
		public HashedString gulpSymbol = "gulp";
	}

	// Token: 0x02000E28 RID: 3624
	public new class Instance : GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.GameInstance
	{
		// Token: 0x06006BAD RID: 27565 RVA: 0x00296D84 File Offset: 0x00294F84
		public Instance(Chore<ConduitSleepStates.Instance> chore, ConduitSleepStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsConduitConnection);
		}

		// Token: 0x06006BAE RID: 27566 RVA: 0x00296DA8 File Offset: 0x00294FA8
		public void SetGulpSymbolVisibility(bool state)
		{
			string sound = GlobalAssets.GetSound("PlugSlug_Charging_Gulp_LP", false);
			if (this.gulpSymbolVisible != state)
			{
				this.gulpSymbolVisible = state;
				this.animController.SetSymbolVisiblity(base.def.gulpSymbol, state);
				if (state)
				{
					this.loopingSounds.StartSound(sound);
					return;
				}
				this.loopingSounds.StopSound(sound);
			}
		}

		// Token: 0x040050FB RID: 20731
		[MyCmpReq]
		public KBatchedAnimController animController;

		// Token: 0x040050FC RID: 20732
		[MyCmpReq]
		public Staterpillar staterpillar;

		// Token: 0x040050FD RID: 20733
		[MyCmpAdd]
		private LoopingSounds loopingSounds;

		// Token: 0x040050FE RID: 20734
		public bool gulpSymbolVisible;

		// Token: 0x040050FF RID: 20735
		public float amountDeposited;
	}

	// Token: 0x02000E29 RID: 3625
	public class SleepStates : GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State
	{
		// Token: 0x04005100 RID: 20736
		public GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State connected;

		// Token: 0x04005101 RID: 20737
		public GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State noConnection;
	}

	// Token: 0x02000E2A RID: 3626
	public class DrowsyStates : GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State
	{
		// Token: 0x04005102 RID: 20738
		public GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State loop;

		// Token: 0x04005103 RID: 20739
		public GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State pst;
	}

	// Token: 0x02000E2B RID: 3627
	public class HasConnectorStates : GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State
	{
		// Token: 0x04005104 RID: 20740
		public GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State moveToSleepLocation;

		// Token: 0x04005105 RID: 20741
		public ConduitSleepStates.SleepStates sleep;

		// Token: 0x04005106 RID: 20742
		public GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State noConnectionWake;

		// Token: 0x04005107 RID: 20743
		public GameStateMachine<ConduitSleepStates, ConduitSleepStates.Instance, IStateMachineTarget, ConduitSleepStates.Def>.State connectedWake;
	}
}
