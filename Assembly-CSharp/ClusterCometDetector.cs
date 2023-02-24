using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000586 RID: 1414
public class ClusterCometDetector : GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>
{
	// Token: 0x06002281 RID: 8833 RVA: 0x000BAE2C File Offset: 0x000B902C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (ClusterCometDetector.Instance smi) => smi.GetComponent<Operational>().IsOperational).Update("Scan Sky", delegate(ClusterCometDetector.Instance smi, float dt)
		{
			smi.ScanSky(false);
		}, UpdateRate.SIM_4000ms, false);
		this.on.DefaultState(this.on.pre).ToggleStatusItem(Db.Get().BuildingStatusItems.DetectorScanning, null).Enter("ToggleActive", delegate(ClusterCometDetector.Instance smi)
		{
			smi.GetComponent<Operational>().SetActive(true, false);
		})
			.Exit("ToggleActive", delegate(ClusterCometDetector.Instance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			});
		this.on.pre.PlayAnim("on_pre").OnAnimQueueComplete(this.on.loop);
		this.on.loop.PlayAnim("on", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.on.pst, (ClusterCometDetector.Instance smi) => !smi.GetComponent<Operational>().IsOperational).TagTransition(GameTags.Detecting, this.on.working, false)
			.Enter("UpdateLogic", delegate(ClusterCometDetector.Instance smi)
			{
				smi.UpdateDetectionState(smi.HasTag(GameTags.Detecting), false);
			})
			.Update("Scan Sky", delegate(ClusterCometDetector.Instance smi, float dt)
			{
				smi.ScanSky(false);
			}, UpdateRate.SIM_200ms, false);
		this.on.pst.PlayAnim("on_pst").OnAnimQueueComplete(this.off);
		this.on.working.DefaultState(this.on.working.pre).ToggleStatusItem(Db.Get().BuildingStatusItems.IncomingMeteors, null).Enter("UpdateLogic", delegate(ClusterCometDetector.Instance smi)
		{
			smi.SetLogicSignal(true);
		})
			.Exit("UpdateLogic", delegate(ClusterCometDetector.Instance smi)
			{
				smi.SetLogicSignal(false);
			})
			.Update("Scan Sky", delegate(ClusterCometDetector.Instance smi, float dt)
			{
				smi.ScanSky(true);
			}, UpdateRate.SIM_200ms, false);
		this.on.working.pre.PlayAnim("detect_pre").OnAnimQueueComplete(this.on.working.loop);
		this.on.working.loop.PlayAnim("detect_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.on.working.pst, (ClusterCometDetector.Instance smi) => !smi.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.on.working.pst, (ClusterCometDetector.Instance smi) => !smi.GetComponent<Operational>().IsActive)
			.TagTransition(GameTags.Detecting, this.on.working.pst, true);
		this.on.working.pst.PlayAnim("detect_pst").OnAnimQueueComplete(this.on.loop).Enter("Reroll", delegate(ClusterCometDetector.Instance smi)
		{
			smi.RerollAccuracy();
		});
	}

	// Token: 0x040013E5 RID: 5093
	public GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.State off;

	// Token: 0x040013E6 RID: 5094
	public ClusterCometDetector.OnStates on;

	// Token: 0x020011B2 RID: 4530
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020011B3 RID: 4531
	public class OnStates : GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.State
	{
		// Token: 0x04005B98 RID: 23448
		public GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.State pre;

		// Token: 0x04005B99 RID: 23449
		public GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.State loop;

		// Token: 0x04005B9A RID: 23450
		public ClusterCometDetector.WorkingStates working;

		// Token: 0x04005B9B RID: 23451
		public GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.State pst;
	}

	// Token: 0x020011B4 RID: 4532
	public class WorkingStates : GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.State
	{
		// Token: 0x04005B9C RID: 23452
		public GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.State pre;

		// Token: 0x04005B9D RID: 23453
		public GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.State loop;

		// Token: 0x04005B9E RID: 23454
		public GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.State pst;
	}

	// Token: 0x020011B5 RID: 4533
	public new class Instance : GameStateMachine<ClusterCometDetector, ClusterCometDetector.Instance, IStateMachineTarget, ClusterCometDetector.Def>.GameInstance
	{
		// Token: 0x0600778F RID: 30607 RVA: 0x002BBA60 File Offset: 0x002B9C60
		public Instance(IStateMachineTarget master, ClusterCometDetector.Def def)
			: base(master, def)
		{
			this.detectorNetworkDef = new DetectorNetwork.Def();
			this.detectorNetworkDef.interferenceRadius = 15;
			this.detectorNetworkDef.worstWarningTime = 1f;
			this.detectorNetworkDef.bestWarningTime = 200f;
			this.detectorNetworkDef.bestNetworkSize = 6;
			this.RerollAccuracy();
		}

		// Token: 0x06007790 RID: 30608 RVA: 0x002BBACA File Offset: 0x002B9CCA
		public override void StartSM()
		{
			if (this.detectorNetwork == null)
			{
				this.detectorNetwork = (DetectorNetwork.Instance)this.detectorNetworkDef.CreateSMI(base.master);
			}
			this.detectorNetwork.StartSM();
			base.StartSM();
		}

		// Token: 0x06007791 RID: 30609 RVA: 0x002BBB01 File Offset: 0x002B9D01
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			this.detectorNetwork.StopSM(reason);
		}

		// Token: 0x06007792 RID: 30610 RVA: 0x002BBB18 File Offset: 0x002B9D18
		public void UpdateDetectionState(bool currentDetection, bool expectedDetectionForState)
		{
			KPrefabID component = base.GetComponent<KPrefabID>();
			if (currentDetection)
			{
				component.AddTag(GameTags.Detecting, false);
			}
			else
			{
				component.RemoveTag(GameTags.Detecting);
			}
			if (currentDetection == expectedDetectionForState)
			{
				this.SetLogicSignal(currentDetection);
			}
		}

		// Token: 0x06007793 RID: 30611 RVA: 0x002BBB54 File Offset: 0x002B9D54
		public void ScanSky(bool expectedDetectionForState)
		{
			float detectTime = this.GetDetectTime();
			int myWorldId = this.GetMyWorldId();
			if (this.GetDetectorState() == ClusterCometDetector.Instance.ClusterCometDetectorState.MeteorShower)
			{
				SaveGame.Instance.GetComponent<GameplayEventManager>().GetActiveEventsOfType<MeteorShowerEvent>(myWorldId, ref this.meteorShowers);
				float num = float.MaxValue;
				foreach (GameplayEventInstance gameplayEventInstance in this.meteorShowers)
				{
					MeteorShowerEvent.StatesInstance statesInstance = gameplayEventInstance.smi as MeteorShowerEvent.StatesInstance;
					if (statesInstance != null)
					{
						num = Mathf.Min(num, statesInstance.TimeUntilNextShower());
					}
				}
				this.meteorShowers.Clear();
				this.UpdateDetectionState(num < detectTime, expectedDetectionForState);
			}
			if (this.GetDetectorState() == ClusterCometDetector.Instance.ClusterCometDetectorState.BallisticObject)
			{
				float num2 = float.MaxValue;
				foreach (object obj in Components.ClusterTravelers)
				{
					ClusterTraveler clusterTraveler = (ClusterTraveler)obj;
					bool flag = clusterTraveler.IsTraveling();
					bool flag2 = clusterTraveler.GetComponent<Clustercraft>() != null;
					if (flag && !flag2 && clusterTraveler.GetDestinationWorldID() == myWorldId)
					{
						num2 = Mathf.Min(num2, clusterTraveler.TravelETA());
					}
				}
				this.UpdateDetectionState(num2 < detectTime, expectedDetectionForState);
			}
			if (this.GetDetectorState() == ClusterCometDetector.Instance.ClusterCometDetectorState.Rocket && this.targetCraft != null)
			{
				Clustercraft clustercraft = this.targetCraft.Get();
				if (!clustercraft.IsNullOrDestroyed())
				{
					ClusterTraveler component = clustercraft.GetComponent<ClusterTraveler>();
					bool flag3 = false;
					if (clustercraft.Status != Clustercraft.CraftStatus.Grounded)
					{
						bool flag4 = component.GetDestinationWorldID() == myWorldId;
						bool flag5 = component.IsTraveling();
						bool flag6 = clustercraft.HasResourcesToMove(1, Clustercraft.CombustionResource.All);
						float num3 = component.TravelETA();
						flag3 = (flag4 && flag5 && flag6 && num3 < detectTime) || (!flag5 && flag4 && clustercraft.Status == Clustercraft.CraftStatus.Landing);
						if (!flag3)
						{
							ClusterGridEntity adjacentAsteroid = clustercraft.GetAdjacentAsteroid();
							flag3 = ((adjacentAsteroid != null) ? ClusterUtil.GetAsteroidWorldIdAtLocation(adjacentAsteroid.Location) : ((int)ClusterManager.INVALID_WORLD_IDX)) == myWorldId && clustercraft.Status == Clustercraft.CraftStatus.Launching;
						}
					}
					this.UpdateDetectionState(flag3, expectedDetectionForState);
				}
			}
		}

		// Token: 0x06007794 RID: 30612 RVA: 0x002BBD8C File Offset: 0x002B9F8C
		public void RerollAccuracy()
		{
			this.nextAccuracy = UnityEngine.Random.value;
		}

		// Token: 0x06007795 RID: 30613 RVA: 0x002BBD99 File Offset: 0x002B9F99
		public void SetLogicSignal(bool on)
		{
			base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, on ? 1 : 0);
		}

		// Token: 0x06007796 RID: 30614 RVA: 0x002BBDB4 File Offset: 0x002B9FB4
		public float GetDetectTime()
		{
			return this.detectorNetwork.GetDetectTimeRange().Lerp(this.nextAccuracy);
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x002BBDDA File Offset: 0x002B9FDA
		public void SetDetectorState(ClusterCometDetector.Instance.ClusterCometDetectorState newState)
		{
			this.detectorState = newState;
		}

		// Token: 0x06007798 RID: 30616 RVA: 0x002BBDE3 File Offset: 0x002B9FE3
		public ClusterCometDetector.Instance.ClusterCometDetectorState GetDetectorState()
		{
			return this.detectorState;
		}

		// Token: 0x06007799 RID: 30617 RVA: 0x002BBDEB File Offset: 0x002B9FEB
		public void SetClustercraftTarget(Clustercraft target)
		{
			if (target)
			{
				this.targetCraft = new Ref<Clustercraft>(target);
				return;
			}
			this.targetCraft = null;
		}

		// Token: 0x0600779A RID: 30618 RVA: 0x002BBE09 File Offset: 0x002BA009
		public Clustercraft GetClustercraftTarget()
		{
			Ref<Clustercraft> @ref = this.targetCraft;
			if (@ref == null)
			{
				return null;
			}
			return @ref.Get();
		}

		// Token: 0x04005B9F RID: 23455
		public bool ShowWorkingStatus;

		// Token: 0x04005BA0 RID: 23456
		private const float BEST_WARNING_TIME = 200f;

		// Token: 0x04005BA1 RID: 23457
		private const float WORST_WARNING_TIME = 1f;

		// Token: 0x04005BA2 RID: 23458
		private const float VARIANCE = 50f;

		// Token: 0x04005BA3 RID: 23459
		private const int MAX_DISH_COUNT = 6;

		// Token: 0x04005BA4 RID: 23460
		private const int INTERFERENCE_RADIUS = 15;

		// Token: 0x04005BA5 RID: 23461
		[Serialize]
		private ClusterCometDetector.Instance.ClusterCometDetectorState detectorState;

		// Token: 0x04005BA6 RID: 23462
		[Serialize]
		private float nextAccuracy;

		// Token: 0x04005BA7 RID: 23463
		[Serialize]
		private Ref<Clustercraft> targetCraft;

		// Token: 0x04005BA8 RID: 23464
		private DetectorNetwork.Def detectorNetworkDef;

		// Token: 0x04005BA9 RID: 23465
		private DetectorNetwork.Instance detectorNetwork;

		// Token: 0x04005BAA RID: 23466
		private List<GameplayEventInstance> meteorShowers = new List<GameplayEventInstance>();

		// Token: 0x02001F94 RID: 8084
		public enum ClusterCometDetectorState
		{
			// Token: 0x04008C7A RID: 35962
			MeteorShower,
			// Token: 0x04008C7B RID: 35963
			BallisticObject,
			// Token: 0x04008C7C RID: 35964
			Rocket
		}
	}
}
