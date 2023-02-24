using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000588 RID: 1416
public class CometDetector : GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>
{
	// Token: 0x06002287 RID: 8839 RVA: 0x000BB438 File Offset: 0x000B9638
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (CometDetector.Instance smi) => smi.GetComponent<Operational>().IsOperational).Update("Scan Sky", delegate(CometDetector.Instance smi, float dt)
		{
			smi.ScanSky(false);
		}, UpdateRate.SIM_4000ms, false);
		this.on.DefaultState(this.on.pre).ToggleStatusItem(Db.Get().BuildingStatusItems.DetectorScanning, null).Enter("ToggleActive", delegate(CometDetector.Instance smi)
		{
			smi.GetComponent<Operational>().SetActive(true, false);
		})
			.Exit("ToggleActive", delegate(CometDetector.Instance smi)
			{
				smi.GetComponent<Operational>().SetActive(false, false);
			});
		this.on.pre.PlayAnim("on_pre").OnAnimQueueComplete(this.on.loop);
		this.on.loop.PlayAnim("on", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.on.pst, (CometDetector.Instance smi) => !smi.GetComponent<Operational>().IsOperational).TagTransition(GameTags.Detecting, this.on.working, false)
			.Enter("UpdateLogic", delegate(CometDetector.Instance smi)
			{
				smi.UpdateDetectionState(smi.HasTag(GameTags.Detecting), false);
			})
			.Update("Scan Sky", delegate(CometDetector.Instance smi, float dt)
			{
				smi.ScanSky(false);
			}, UpdateRate.SIM_200ms, false);
		this.on.pst.PlayAnim("on_pst").OnAnimQueueComplete(this.off);
		this.on.working.DefaultState(this.on.working.pre).ToggleStatusItem(Db.Get().BuildingStatusItems.IncomingMeteors, null).Enter("UpdateLogic", delegate(CometDetector.Instance smi)
		{
			smi.SetLogicSignal(true);
		})
			.Exit("UpdateLogic", delegate(CometDetector.Instance smi)
			{
				smi.SetLogicSignal(false);
			})
			.Update("Scan Sky", delegate(CometDetector.Instance smi, float dt)
			{
				smi.ScanSky(true);
			}, UpdateRate.SIM_200ms, false);
		this.on.working.pre.PlayAnim("detect_pre").OnAnimQueueComplete(this.on.working.loop);
		this.on.working.loop.PlayAnim("detect_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.on.working.pst, (CometDetector.Instance smi) => !smi.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.on.working.pst, (CometDetector.Instance smi) => !smi.GetComponent<Operational>().IsActive)
			.TagTransition(GameTags.Detecting, this.on.working.pst, true);
		this.on.working.pst.PlayAnim("detect_pst").OnAnimQueueComplete(this.on.loop).Enter("Reroll", delegate(CometDetector.Instance smi)
		{
			smi.RerollAccuracy();
		});
	}

	// Token: 0x040013EA RID: 5098
	public GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.State off;

	// Token: 0x040013EB RID: 5099
	public CometDetector.OnStates on;

	// Token: 0x020011BC RID: 4540
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020011BD RID: 4541
	public class OnStates : GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.State
	{
		// Token: 0x04005BD6 RID: 23510
		public GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.State pre;

		// Token: 0x04005BD7 RID: 23511
		public GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.State loop;

		// Token: 0x04005BD8 RID: 23512
		public CometDetector.WorkingStates working;

		// Token: 0x04005BD9 RID: 23513
		public GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.State pst;
	}

	// Token: 0x020011BE RID: 4542
	public class WorkingStates : GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.State
	{
		// Token: 0x04005BDA RID: 23514
		public GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.State pre;

		// Token: 0x04005BDB RID: 23515
		public GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.State loop;

		// Token: 0x04005BDC RID: 23516
		public GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.State pst;
	}

	// Token: 0x020011BF RID: 4543
	public new class Instance : GameStateMachine<CometDetector, CometDetector.Instance, IStateMachineTarget, CometDetector.Def>.GameInstance
	{
		// Token: 0x060077CB RID: 30667 RVA: 0x002BC574 File Offset: 0x002BA774
		public Instance(IStateMachineTarget master, CometDetector.Def def)
			: base(master, def)
		{
			this.detectorNetworkDef = new DetectorNetwork.Def();
			this.detectorNetworkDef.interferenceRadius = 15;
			this.detectorNetworkDef.worstWarningTime = 1f;
			this.detectorNetworkDef.bestWarningTime = 200f;
			this.detectorNetworkDef.bestNetworkSize = 6;
			this.targetCraft = new Ref<LaunchConditionManager>();
			this.RerollAccuracy();
		}

		// Token: 0x060077CC RID: 30668 RVA: 0x002BC5E9 File Offset: 0x002BA7E9
		public override void StartSM()
		{
			if (this.detectorNetwork == null)
			{
				this.detectorNetwork = (DetectorNetwork.Instance)this.detectorNetworkDef.CreateSMI(base.master);
			}
			this.detectorNetwork.StartSM();
			base.StartSM();
		}

		// Token: 0x060077CD RID: 30669 RVA: 0x002BC620 File Offset: 0x002BA820
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			this.detectorNetwork.StopSM(reason);
		}

		// Token: 0x060077CE RID: 30670 RVA: 0x002BC638 File Offset: 0x002BA838
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

		// Token: 0x060077CF RID: 30671 RVA: 0x002BC674 File Offset: 0x002BA874
		public void ScanSky(bool expectedDetectionForState)
		{
			float detectTime = this.GetDetectTime();
			if (this.targetCraft.Get() == null)
			{
				SaveGame.Instance.GetComponent<GameplayEventManager>().GetActiveEventsOfType<MeteorShowerEvent>(this.GetMyWorldId(), ref this.meteorShowers);
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
				return;
			}
			Spacecraft spacecraftFromLaunchConditionManager = SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(this.targetCraft.Get());
			if (spacecraftFromLaunchConditionManager.state == Spacecraft.MissionState.Destroyed)
			{
				this.targetCraft.Set(null);
				this.UpdateDetectionState(false, expectedDetectionForState);
				return;
			}
			if (spacecraftFromLaunchConditionManager.state == Spacecraft.MissionState.Launching || spacecraftFromLaunchConditionManager.state == Spacecraft.MissionState.WaitingToLand || spacecraftFromLaunchConditionManager.state == Spacecraft.MissionState.Landing || (spacecraftFromLaunchConditionManager.state == Spacecraft.MissionState.Underway && spacecraftFromLaunchConditionManager.GetTimeLeft() <= detectTime))
			{
				this.UpdateDetectionState(true, expectedDetectionForState);
				return;
			}
			this.UpdateDetectionState(false, expectedDetectionForState);
		}

		// Token: 0x060077D0 RID: 30672 RVA: 0x002BC7A8 File Offset: 0x002BA9A8
		public void RerollAccuracy()
		{
			this.nextAccuracy = UnityEngine.Random.value;
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x002BC7B5 File Offset: 0x002BA9B5
		public void SetLogicSignal(bool on)
		{
			base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, on ? 1 : 0);
		}

		// Token: 0x060077D2 RID: 30674 RVA: 0x002BC7D0 File Offset: 0x002BA9D0
		public float GetDetectTime()
		{
			return this.detectorNetwork.GetDetectTimeRange().Lerp(this.nextAccuracy);
		}

		// Token: 0x060077D3 RID: 30675 RVA: 0x002BC7F6 File Offset: 0x002BA9F6
		public void SetTargetCraft(LaunchConditionManager target)
		{
			this.targetCraft.Set(target);
		}

		// Token: 0x060077D4 RID: 30676 RVA: 0x002BC804 File Offset: 0x002BAA04
		public LaunchConditionManager GetTargetCraft()
		{
			return this.targetCraft.Get();
		}

		// Token: 0x04005BDD RID: 23517
		public bool ShowWorkingStatus;

		// Token: 0x04005BDE RID: 23518
		private const float BEST_WARNING_TIME = 200f;

		// Token: 0x04005BDF RID: 23519
		private const float WORST_WARNING_TIME = 1f;

		// Token: 0x04005BE0 RID: 23520
		private const float VARIANCE = 50f;

		// Token: 0x04005BE1 RID: 23521
		private const int MAX_DISH_COUNT = 6;

		// Token: 0x04005BE2 RID: 23522
		private const int INTERFERENCE_RADIUS = 15;

		// Token: 0x04005BE3 RID: 23523
		[Serialize]
		private float nextAccuracy;

		// Token: 0x04005BE4 RID: 23524
		[Serialize]
		private Ref<LaunchConditionManager> targetCraft;

		// Token: 0x04005BE5 RID: 23525
		private DetectorNetwork.Def detectorNetworkDef;

		// Token: 0x04005BE6 RID: 23526
		private DetectorNetwork.Instance detectorNetwork;

		// Token: 0x04005BE7 RID: 23527
		private List<GameplayEventInstance> meteorShowers = new List<GameplayEventInstance>();
	}
}
