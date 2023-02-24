using System;
using System.Collections.Generic;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x020005C4 RID: 1476
public class GeoTuner : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>
{
	// Token: 0x060024B0 RID: 9392 RVA: 0x000C613C File Offset: 0x000C433C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.operational;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshAnimationGeyserSymbolType));
		this.nonOperational.DefaultState(this.nonOperational.off).OnSignal(this.geyserSwitchSignal, this.nonOperational.switchingGeyser).Enter(delegate(GeoTuner.Instance smi)
		{
			smi.RefreshLogicOutput();
		})
			.TagTransition(GameTags.Operational, this.operational, false);
		this.nonOperational.off.PlayAnim("off");
		this.nonOperational.switchingGeyser.QueueAnim("geyser_down", false, null).OnAnimQueueComplete(this.nonOperational.down);
		this.nonOperational.down.PlayAnim("geyser_up").QueueAnim("off", false, null).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshAnimationGeyserSymbolType))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.TriggerSoundsForGeyserChange));
		this.operational.PlayAnim("on").Enter(delegate(GeoTuner.Instance smi)
		{
			smi.RefreshLogicOutput();
		}).DefaultState(this.operational.idle)
			.TagTransition(GameTags.Operational, this.nonOperational, true);
		this.operational.idle.ParamTransition<GameObject>(this.AssignedGeyser, this.operational.geyserSelected, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsNotNull).ParamTransition<GameObject>(this.AssignedGeyser, this.operational.noGeyserSelected, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsNull);
		this.operational.noGeyserSelected.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerNoGeyserSelected, null).ParamTransition<GameObject>(this.AssignedGeyser, this.operational.geyserSelected.switchingGeyser, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsNotNull).Enter(delegate(GeoTuner.Instance smi)
		{
			smi.RefreshLogicOutput();
		})
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.DropStorage))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshStorageRequirements))
			.Exit(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ForgetWorkDoneByDupe))
			.Exit(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ResetExpirationTimer))
			.QueueAnim("geyser_down", false, null)
			.OnAnimQueueComplete(this.operational.noGeyserSelected.idle);
		this.operational.noGeyserSelected.idle.PlayAnim("geyser_up").QueueAnim("on", false, null).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshAnimationGeyserSymbolType))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.TriggerSoundsForGeyserChange));
		this.operational.geyserSelected.DefaultState(this.operational.geyserSelected.idle).ToggleStatusItem(Db.Get().BuildingStatusItems.GeoTunerGeyserStatus, null).ParamTransition<GameObject>(this.AssignedGeyser, this.operational.noGeyserSelected, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsNull)
			.OnSignal(this.geyserSwitchSignal, this.operational.geyserSelected.switchingGeyser)
			.Enter(delegate(GeoTuner.Instance smi)
			{
				smi.RefreshLogicOutput();
			});
		this.operational.geyserSelected.idle.ParamTransition<bool>(this.hasBeenWorkedByResearcher, this.operational.geyserSelected.broadcasting.active, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsTrue).ParamTransition<bool>(this.hasBeenWorkedByResearcher, this.operational.geyserSelected.researcherInteractionNeeded, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsFalse);
		this.operational.geyserSelected.switchingGeyser.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.DropStorageIfNotMatching)).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ForgetWorkDoneByDupe)).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ResetExpirationTimer))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshStorageRequirements))
			.Enter(delegate(GeoTuner.Instance smi)
			{
				smi.RefreshLogicOutput();
			})
			.QueueAnim("geyser_down", false, null)
			.OnAnimQueueComplete(this.operational.geyserSelected.switchingGeyser.down);
		this.operational.geyserSelected.switchingGeyser.down.QueueAnim("geyser_up", false, null).QueueAnim("on", false, null).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RefreshAnimationGeyserSymbolType))
			.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.TriggerSoundsForGeyserChange))
			.ScheduleActionNextFrame("Switch Animation Completed", delegate(GeoTuner.Instance smi)
			{
				smi.GoTo(this.operational.geyserSelected.idle);
			});
		this.operational.geyserSelected.researcherInteractionNeeded.EventTransition(GameHashes.UpdateRoom, this.operational.geyserSelected.researcherInteractionNeeded.blocked, (GeoTuner.Instance smi) => !GeoTuner.WorkRequirementsMet(smi)).EventTransition(GameHashes.UpdateRoom, this.operational.geyserSelected.researcherInteractionNeeded.available, new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.Transition.ConditionCallback(GeoTuner.WorkRequirementsMet)).EventTransition(GameHashes.OnStorageChange, this.operational.geyserSelected.researcherInteractionNeeded.blocked, (GeoTuner.Instance smi) => !GeoTuner.WorkRequirementsMet(smi))
			.EventTransition(GameHashes.OnStorageChange, this.operational.geyserSelected.researcherInteractionNeeded.available, new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.Transition.ConditionCallback(GeoTuner.WorkRequirementsMet))
			.ParamTransition<bool>(this.hasBeenWorkedByResearcher, this.operational.geyserSelected.broadcasting.active, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsTrue)
			.Exit(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ResetExpirationTimer));
		this.operational.geyserSelected.researcherInteractionNeeded.blocked.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerResearchNeeded, null).DoNothing();
		this.operational.geyserSelected.researcherInteractionNeeded.available.DefaultState(this.operational.geyserSelected.researcherInteractionNeeded.available.waitingForDupe).ToggleRecurringChore(new Func<GeoTuner.Instance, Chore>(this.CreateResearchChore), null).WorkableCompleteTransition((GeoTuner.Instance smi) => smi.workable, this.operational.geyserSelected.researcherInteractionNeeded.completed);
		this.operational.geyserSelected.researcherInteractionNeeded.available.waitingForDupe.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerResearchNeeded, null).WorkableStartTransition((GeoTuner.Instance smi) => smi.workable, this.operational.geyserSelected.researcherInteractionNeeded.available.inProgress);
		this.operational.geyserSelected.researcherInteractionNeeded.available.inProgress.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerResearchInProgress, null).WorkableStopTransition((GeoTuner.Instance smi) => smi.workable, this.operational.geyserSelected.researcherInteractionNeeded.available.waitingForDupe);
		this.operational.geyserSelected.researcherInteractionNeeded.completed.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.OnResearchCompleted));
		this.operational.geyserSelected.broadcasting.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoTunerBroadcasting, null).Toggle("Tuning", new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ApplyTuning), new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.RemoveTuning));
		this.operational.geyserSelected.broadcasting.onHold.PlayAnim("on").UpdateTransition(this.operational.geyserSelected.broadcasting.active, (GeoTuner.Instance smi, float dt) => !GeoTuner.GeyserExitEruptionTransition(smi, dt), UpdateRate.SIM_200ms, false);
		this.operational.geyserSelected.broadcasting.active.Toggle("EnergyConsumption", delegate(GeoTuner.Instance smi)
		{
			smi.operational.SetActive(true, false);
		}, delegate(GeoTuner.Instance smi)
		{
			smi.operational.SetActive(false, false);
		}).Toggle("BroadcastingAnimations", new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.PlayBroadcastingAnimation), new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.StopPlayingBroadcastingAnimation)).Update(new Action<GeoTuner.Instance, float>(GeoTuner.ExpirationTimerUpdate), UpdateRate.SIM_200ms, false)
			.UpdateTransition(this.operational.geyserSelected.broadcasting.onHold, new Func<GeoTuner.Instance, float, bool>(GeoTuner.GeyserExitEruptionTransition), UpdateRate.SIM_200ms, false)
			.ParamTransition<float>(this.expirationTimer, this.operational.geyserSelected.broadcasting.expired, GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.IsLTEZero);
		this.operational.geyserSelected.broadcasting.expired.Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ForgetWorkDoneByDupe)).Enter(new StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State.Callback(GeoTuner.ResetExpirationTimer)).ScheduleActionNextFrame("Expired", delegate(GeoTuner.Instance smi)
		{
			smi.GoTo(this.operational.geyserSelected.researcherInteractionNeeded);
		});
	}

	// Token: 0x060024B1 RID: 9393 RVA: 0x000C6A88 File Offset: 0x000C4C88
	private static void TriggerSoundsForGeyserChange(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser != null)
		{
			EventInstance eventInstance = default(EventInstance);
			switch (assignedGeyser.configuration.geyserType.shape)
			{
			case GeyserConfigurator.GeyserShape.Gas:
				eventInstance = SoundEvent.BeginOneShot(GeoTuner.gasGeyserTuningSoundPath, smi.transform.GetPosition(), 1f, false);
				break;
			case GeyserConfigurator.GeyserShape.Liquid:
				eventInstance = SoundEvent.BeginOneShot(GeoTuner.liquidGeyserTuningSoundPath, smi.transform.GetPosition(), 1f, false);
				break;
			case GeyserConfigurator.GeyserShape.Molten:
				eventInstance = SoundEvent.BeginOneShot(GeoTuner.metalGeyserTuningSoundPath, smi.transform.GetPosition(), 1f, false);
				break;
			}
			SoundEvent.EndOneShot(eventInstance);
		}
	}

	// Token: 0x060024B2 RID: 9394 RVA: 0x000C6B34 File Offset: 0x000C4D34
	private static void RefreshStorageRequirements(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser == null)
		{
			smi.storage.capacityKg = 0f;
			smi.storage.storageFilters = null;
			smi.manualDelivery.capacity = 0f;
			smi.manualDelivery.refillMass = 0f;
			smi.manualDelivery.RequestedItemTag = null;
			smi.manualDelivery.AbortDelivery("No geyser is selected for tuning");
			return;
		}
		GeoTunerConfig.GeotunedGeyserSettings settingsForGeyser = smi.def.GetSettingsForGeyser(assignedGeyser);
		smi.storage.capacityKg = settingsForGeyser.quantity;
		smi.storage.storageFilters = new List<Tag> { settingsForGeyser.material };
		smi.manualDelivery.AbortDelivery("Switching to new delivery request");
		smi.manualDelivery.capacity = settingsForGeyser.quantity;
		smi.manualDelivery.refillMass = settingsForGeyser.quantity;
		smi.manualDelivery.MinimumMass = settingsForGeyser.quantity;
		smi.manualDelivery.RequestedItemTag = settingsForGeyser.material;
	}

	// Token: 0x060024B3 RID: 9395 RVA: 0x000C6C40 File Offset: 0x000C4E40
	private static void DropStorage(GeoTuner.Instance smi)
	{
		smi.storage.DropAll(false, false, default(Vector3), true, null);
	}

	// Token: 0x060024B4 RID: 9396 RVA: 0x000C6C68 File Offset: 0x000C4E68
	private static void DropStorageIfNotMatching(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser != null)
		{
			GeoTunerConfig.GeotunedGeyserSettings settingsForGeyser = smi.def.GetSettingsForGeyser(assignedGeyser);
			List<GameObject> items = smi.storage.GetItems();
			if (smi.storage.GetItems() != null && items.Count > 0)
			{
				Tag tag = items[0].PrefabID();
				PrimaryElement component = items[0].GetComponent<PrimaryElement>();
				if (tag != settingsForGeyser.material)
				{
					smi.storage.DropAll(false, false, default(Vector3), true, null);
					return;
				}
				float num = component.Mass - settingsForGeyser.quantity;
				if (num > 0f)
				{
					smi.storage.DropSome(tag, num, false, false, default(Vector3), true, false);
					return;
				}
			}
		}
		else
		{
			smi.storage.DropAll(false, false, default(Vector3), true, null);
		}
	}

	// Token: 0x060024B5 RID: 9397 RVA: 0x000C6D50 File Offset: 0x000C4F50
	private static bool GeyserExitEruptionTransition(GeoTuner.Instance smi, float dt)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		return assignedGeyser != null && assignedGeyser.smi.GetCurrentState().parent != assignedGeyser.smi.sm.erupt;
	}

	// Token: 0x060024B6 RID: 9398 RVA: 0x000C6D94 File Offset: 0x000C4F94
	public static void OnResearchCompleted(GeoTuner.Instance smi)
	{
		smi.storage.ConsumeAllIgnoringDisease();
		smi.sm.hasBeenWorkedByResearcher.Set(true, smi, false);
	}

	// Token: 0x060024B7 RID: 9399 RVA: 0x000C6DB5 File Offset: 0x000C4FB5
	public static void PlayBroadcastingAnimation(GeoTuner.Instance smi)
	{
		smi.animController.Play("broadcasting", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x060024B8 RID: 9400 RVA: 0x000C6DD7 File Offset: 0x000C4FD7
	public static void StopPlayingBroadcastingAnimation(GeoTuner.Instance smi)
	{
		smi.animController.Play("broadcasting", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x060024B9 RID: 9401 RVA: 0x000C6DF9 File Offset: 0x000C4FF9
	public static void RefreshAnimationGeyserSymbolType(GeoTuner.Instance smi)
	{
		smi.RefreshGeyserSymbol();
	}

	// Token: 0x060024BA RID: 9402 RVA: 0x000C6E01 File Offset: 0x000C5001
	public static float GetRemainingExpiraionTime(GeoTuner.Instance smi)
	{
		return smi.sm.expirationTimer.Get(smi);
	}

	// Token: 0x060024BB RID: 9403 RVA: 0x000C6E14 File Offset: 0x000C5014
	private static void ExpirationTimerUpdate(GeoTuner.Instance smi, float dt)
	{
		float num = GeoTuner.GetRemainingExpiraionTime(smi);
		num -= dt;
		smi.sm.expirationTimer.Set(num, smi, false);
	}

	// Token: 0x060024BC RID: 9404 RVA: 0x000C6E40 File Offset: 0x000C5040
	private static void ResetExpirationTimer(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser != null)
		{
			smi.sm.expirationTimer.Set(smi.def.GetSettingsForGeyser(assignedGeyser).duration, smi, false);
			return;
		}
		smi.sm.expirationTimer.Set(0f, smi, false);
	}

	// Token: 0x060024BD RID: 9405 RVA: 0x000C6E9A File Offset: 0x000C509A
	private static void ForgetWorkDoneByDupe(GeoTuner.Instance smi)
	{
		smi.sm.hasBeenWorkedByResearcher.Set(false, smi, false);
		smi.workable.WorkTimeRemaining = smi.workable.GetWorkTime();
	}

	// Token: 0x060024BE RID: 9406 RVA: 0x000C6EC8 File Offset: 0x000C50C8
	private Chore CreateResearchChore(GeoTuner.Instance smi)
	{
		return new WorkChore<GeoTunerWorkable>(Db.Get().ChoreTypes.Research, smi.workable, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
	}

	// Token: 0x060024BF RID: 9407 RVA: 0x000C6F00 File Offset: 0x000C5100
	private static void ApplyTuning(GeoTuner.Instance smi)
	{
		smi.GetAssignedGeyser().AddModification(smi.currentGeyserModification);
	}

	// Token: 0x060024C0 RID: 9408 RVA: 0x000C6F14 File Offset: 0x000C5114
	private static void RemoveTuning(GeoTuner.Instance smi)
	{
		Geyser assignedGeyser = smi.GetAssignedGeyser();
		if (assignedGeyser != null)
		{
			assignedGeyser.RemoveModification(smi.currentGeyserModification);
		}
	}

	// Token: 0x060024C1 RID: 9409 RVA: 0x000C6F3D File Offset: 0x000C513D
	public static bool WorkRequirementsMet(GeoTuner.Instance smi)
	{
		return GeoTuner.IsInLabRoom(smi) && smi.storage.MassStored() == smi.storage.capacityKg;
	}

	// Token: 0x060024C2 RID: 9410 RVA: 0x000C6F61 File Offset: 0x000C5161
	public static bool IsInLabRoom(GeoTuner.Instance smi)
	{
		return smi.roomTracker.IsInCorrectRoom();
	}

	// Token: 0x04001522 RID: 5410
	private StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.Signal geyserSwitchSignal;

	// Token: 0x04001523 RID: 5411
	private GeoTuner.NonOperationalState nonOperational;

	// Token: 0x04001524 RID: 5412
	private GeoTuner.OperationalState operational;

	// Token: 0x04001525 RID: 5413
	private StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.TargetParameter FutureGeyser;

	// Token: 0x04001526 RID: 5414
	private StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.TargetParameter AssignedGeyser;

	// Token: 0x04001527 RID: 5415
	public StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.BoolParameter hasBeenWorkedByResearcher;

	// Token: 0x04001528 RID: 5416
	public StateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.FloatParameter expirationTimer;

	// Token: 0x04001529 RID: 5417
	public static string liquidGeyserTuningSoundPath = GlobalAssets.GetSound("GeoTuner_Tuning_Geyser", false);

	// Token: 0x0400152A RID: 5418
	public static string gasGeyserTuningSoundPath = GlobalAssets.GetSound("GeoTuner_Tuning_Vent", false);

	// Token: 0x0400152B RID: 5419
	public static string metalGeyserTuningSoundPath = GlobalAssets.GetSound("GeoTuner_Tuning_Volcano", false);

	// Token: 0x0400152C RID: 5420
	public const string anim_switchGeyser_down = "geyser_down";

	// Token: 0x0400152D RID: 5421
	public const string anim_switchGeyser_up = "geyser_up";

	// Token: 0x0400152E RID: 5422
	private const string BroadcastingOnHoldAnimationName = "on";

	// Token: 0x0400152F RID: 5423
	private const string OnAnimName = "on";

	// Token: 0x04001530 RID: 5424
	private const string OffAnimName = "off";

	// Token: 0x04001531 RID: 5425
	private const string BroadcastingAnimationName = "broadcasting";

	// Token: 0x02001204 RID: 4612
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x060078B6 RID: 30902 RVA: 0x002C066C File Offset: 0x002BE86C
		public GeoTunerConfig.GeotunedGeyserSettings GetSettingsForGeyser(Geyser geyser)
		{
			GeoTunerConfig.GeotunedGeyserSettings geotunedGeyserSettings;
			if (!this.geotunedGeyserSettings.TryGetValue(geyser.configuration.typeId, out geotunedGeyserSettings))
			{
				DebugUtil.DevLogError(string.Format("Geyser {0} is missing a Geotuner setting, using default", geyser.configuration.typeId));
				return this.defaultSetting;
			}
			return geotunedGeyserSettings;
		}

		// Token: 0x04005CB7 RID: 23735
		public string OUTPUT_LOGIC_PORT_ID;

		// Token: 0x04005CB8 RID: 23736
		public Dictionary<HashedString, GeoTunerConfig.GeotunedGeyserSettings> geotunedGeyserSettings;

		// Token: 0x04005CB9 RID: 23737
		public GeoTunerConfig.GeotunedGeyserSettings defaultSetting;
	}

	// Token: 0x02001205 RID: 4613
	public class BroadcastingState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04005CBA RID: 23738
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State active;

		// Token: 0x04005CBB RID: 23739
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State onHold;

		// Token: 0x04005CBC RID: 23740
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State expired;
	}

	// Token: 0x02001206 RID: 4614
	public class ResearchProgress : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04005CBD RID: 23741
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State waitingForDupe;

		// Token: 0x04005CBE RID: 23742
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State inProgress;
	}

	// Token: 0x02001207 RID: 4615
	public class ResearchState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04005CBF RID: 23743
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State blocked;

		// Token: 0x04005CC0 RID: 23744
		public GeoTuner.ResearchProgress available;

		// Token: 0x04005CC1 RID: 23745
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State completed;
	}

	// Token: 0x02001208 RID: 4616
	public class SwitchingGeyser : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04005CC2 RID: 23746
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State down;
	}

	// Token: 0x02001209 RID: 4617
	public class GeyserSelectedState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04005CC3 RID: 23747
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State idle;

		// Token: 0x04005CC4 RID: 23748
		public GeoTuner.SwitchingGeyser switchingGeyser;

		// Token: 0x04005CC5 RID: 23749
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State resourceNeeded;

		// Token: 0x04005CC6 RID: 23750
		public GeoTuner.ResearchState researcherInteractionNeeded;

		// Token: 0x04005CC7 RID: 23751
		public GeoTuner.BroadcastingState broadcasting;
	}

	// Token: 0x0200120A RID: 4618
	public class SimpleIdleState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04005CC8 RID: 23752
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State idle;
	}

	// Token: 0x0200120B RID: 4619
	public class NonOperationalState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04005CC9 RID: 23753
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State off;

		// Token: 0x04005CCA RID: 23754
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State switchingGeyser;

		// Token: 0x04005CCB RID: 23755
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State down;
	}

	// Token: 0x0200120C RID: 4620
	public class OperationalState : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State
	{
		// Token: 0x04005CCC RID: 23756
		public GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.State idle;

		// Token: 0x04005CCD RID: 23757
		public GeoTuner.SimpleIdleState noGeyserSelected;

		// Token: 0x04005CCE RID: 23758
		public GeoTuner.GeyserSelectedState geyserSelected;
	}

	// Token: 0x0200120D RID: 4621
	public enum GeyserAnimTypeSymbols
	{
		// Token: 0x04005CD0 RID: 23760
		meter_gas,
		// Token: 0x04005CD1 RID: 23761
		meter_metal,
		// Token: 0x04005CD2 RID: 23762
		meter_liquid,
		// Token: 0x04005CD3 RID: 23763
		meter_board
	}

	// Token: 0x0200120E RID: 4622
	public new class Instance : GameStateMachine<GeoTuner, GeoTuner.Instance, IStateMachineTarget, GeoTuner.Def>.GameInstance
	{
		// Token: 0x060078C0 RID: 30912 RVA: 0x002C0704 File Offset: 0x002BE904
		public Instance(IStateMachineTarget master, GeoTuner.Def def)
			: base(master, def)
		{
			this.originID = UI.StripLinkFormatting("GeoTuner") + " [" + base.gameObject.GetInstanceID().ToString() + "]";
			this.switchGeyserMeter = new MeterController(this.animController, "geyser_target", this.GetAnimationSymbol().ToString(), Meter.Offset.Behind, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		}

		// Token: 0x060078C1 RID: 30913 RVA: 0x002C0780 File Offset: 0x002BE980
		public override void StartSM()
		{
			base.StartSM();
			Components.GeoTuners.Add(base.smi.GetMyWorldId(), this);
			Geyser assignedGeyser = this.GetAssignedGeyser();
			if (assignedGeyser != null)
			{
				assignedGeyser.Subscribe(-593169791, new Action<object>(this.OnEruptionStateChanged));
				this.RefreshModification();
			}
			this.RefreshLogicOutput();
			this.AssignFutureGeyser(this.GetFutureGeyser());
			base.gameObject.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
		}

		// Token: 0x060078C2 RID: 30914 RVA: 0x002C0806 File Offset: 0x002BEA06
		public Geyser GetFutureGeyser()
		{
			if (base.smi.sm.FutureGeyser.IsNull(this))
			{
				return null;
			}
			return base.sm.FutureGeyser.Get(this).GetComponent<Geyser>();
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x002C0838 File Offset: 0x002BEA38
		public Geyser GetAssignedGeyser()
		{
			if (base.smi.sm.AssignedGeyser.IsNull(this))
			{
				return null;
			}
			return base.sm.AssignedGeyser.Get(this).GetComponent<Geyser>();
		}

		// Token: 0x060078C4 RID: 30916 RVA: 0x002C086C File Offset: 0x002BEA6C
		public void AssignFutureGeyser(Geyser newFutureGeyser)
		{
			bool flag = newFutureGeyser != this.GetFutureGeyser();
			bool flag2 = this.GetAssignedGeyser() != newFutureGeyser;
			base.sm.FutureGeyser.Set(newFutureGeyser, this);
			if (flag)
			{
				if (flag2)
				{
					this.RecreateSwitchGeyserChore();
					return;
				}
				if (this.switchGeyserChore != null)
				{
					this.AbortSwitchGeyserChore("Future Geyser was set to current Geyser");
					return;
				}
			}
			else if (this.switchGeyserChore == null && flag2)
			{
				this.RecreateSwitchGeyserChore();
			}
		}

		// Token: 0x060078C5 RID: 30917 RVA: 0x002C08DC File Offset: 0x002BEADC
		private void AbortSwitchGeyserChore(string reason = "Aborting Switch Geyser Chore")
		{
			if (this.switchGeyserChore != null)
			{
				Chore chore = this.switchGeyserChore;
				chore.onComplete = (Action<Chore>)Delegate.Remove(chore.onComplete, new Action<Chore>(this.OnSwitchGeyserChoreCompleted));
				this.switchGeyserChore.Cancel(reason);
				this.switchGeyserChore = null;
			}
			this.switchGeyserChore = null;
		}

		// Token: 0x060078C6 RID: 30918 RVA: 0x002C0934 File Offset: 0x002BEB34
		private Chore RecreateSwitchGeyserChore()
		{
			this.AbortSwitchGeyserChore("Recreating Chore");
			this.switchGeyserChore = new WorkChore<GeoTunerSwitchGeyserWorkable>(Db.Get().ChoreTypes.Toggle, this.switchGeyserWorkable, null, true, null, new Action<Chore>(this.ShowSwitchingGeyserStatusItem), new Action<Chore>(this.HideSwitchingGeyserStatusItem), true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
			Chore chore = this.switchGeyserChore;
			chore.onComplete = (Action<Chore>)Delegate.Combine(chore.onComplete, new Action<Chore>(this.OnSwitchGeyserChoreCompleted));
			return this.switchGeyserChore;
		}

		// Token: 0x060078C7 RID: 30919 RVA: 0x002C09C0 File Offset: 0x002BEBC0
		private void ShowSwitchingGeyserStatusItem(Chore chore)
		{
			base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.PendingSwitchToggle, null);
		}

		// Token: 0x060078C8 RID: 30920 RVA: 0x002C09E3 File Offset: 0x002BEBE3
		private void HideSwitchingGeyserStatusItem(Chore chore)
		{
			base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.PendingSwitchToggle, false);
		}

		// Token: 0x060078C9 RID: 30921 RVA: 0x002C0A08 File Offset: 0x002BEC08
		private void OnSwitchGeyserChoreCompleted(Chore chore)
		{
			this.GetCurrentState();
			GeoTuner.NonOperationalState nonOperational = base.sm.nonOperational;
			Geyser futureGeyser = this.GetFutureGeyser();
			bool flag = this.GetAssignedGeyser() != futureGeyser;
			if (chore.isComplete && flag)
			{
				this.AssignGeyser(futureGeyser);
			}
			base.Trigger(1980521255, null);
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x002C0A5C File Offset: 0x002BEC5C
		public void AssignGeyser(Geyser geyser)
		{
			Geyser assignedGeyser = this.GetAssignedGeyser();
			if (assignedGeyser != null && assignedGeyser != geyser)
			{
				GeoTuner.RemoveTuning(base.smi);
				assignedGeyser.Unsubscribe(-593169791, new Action<object>(base.smi.OnEruptionStateChanged));
			}
			Geyser geyser2 = assignedGeyser;
			base.sm.AssignedGeyser.Set(geyser, this);
			this.RefreshModification();
			if (geyser2 != geyser)
			{
				if (geyser != null)
				{
					geyser.Subscribe(-593169791, new Action<object>(this.OnEruptionStateChanged));
					geyser.Trigger(1763323737, null);
				}
				if (geyser2 != null)
				{
					geyser2.Trigger(1763323737, null);
				}
				base.sm.geyserSwitchSignal.Trigger(this);
			}
		}

		// Token: 0x060078CB RID: 30923 RVA: 0x002C0B20 File Offset: 0x002BED20
		private void RefreshModification()
		{
			Geyser assignedGeyser = this.GetAssignedGeyser();
			if (assignedGeyser != null)
			{
				GeoTunerConfig.GeotunedGeyserSettings settingsForGeyser = base.def.GetSettingsForGeyser(assignedGeyser);
				this.currentGeyserModification = settingsForGeyser.template;
				this.currentGeyserModification.originID = this.originID;
				this.enhancementDuration = settingsForGeyser.duration;
				assignedGeyser.Trigger(1763323737, null);
			}
			GeoTuner.RefreshStorageRequirements(this);
			GeoTuner.DropStorageIfNotMatching(this);
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x002C0B8C File Offset: 0x002BED8C
		public void RefreshGeyserSymbol()
		{
			this.switchGeyserMeter.meterController.Play(this.GetAnimationSymbol().ToString(), KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x060078CD RID: 30925 RVA: 0x002C0BD0 File Offset: 0x002BEDD0
		private GeoTuner.GeyserAnimTypeSymbols GetAnimationSymbol()
		{
			GeoTuner.GeyserAnimTypeSymbols geyserAnimTypeSymbols = GeoTuner.GeyserAnimTypeSymbols.meter_board;
			Geyser assignedGeyser = base.smi.GetAssignedGeyser();
			if (assignedGeyser != null)
			{
				switch (assignedGeyser.configuration.geyserType.shape)
				{
				case GeyserConfigurator.GeyserShape.Gas:
					geyserAnimTypeSymbols = GeoTuner.GeyserAnimTypeSymbols.meter_gas;
					break;
				case GeyserConfigurator.GeyserShape.Liquid:
					geyserAnimTypeSymbols = GeoTuner.GeyserAnimTypeSymbols.meter_liquid;
					break;
				case GeyserConfigurator.GeyserShape.Molten:
					geyserAnimTypeSymbols = GeoTuner.GeyserAnimTypeSymbols.meter_metal;
					break;
				}
			}
			return geyserAnimTypeSymbols;
		}

		// Token: 0x060078CE RID: 30926 RVA: 0x002C0C24 File Offset: 0x002BEE24
		public void OnEruptionStateChanged(object data)
		{
			bool flag = (bool)data;
			this.RefreshLogicOutput();
		}

		// Token: 0x060078CF RID: 30927 RVA: 0x002C0C34 File Offset: 0x002BEE34
		public void RefreshLogicOutput()
		{
			Geyser assignedGeyser = this.GetAssignedGeyser();
			bool flag = this.GetCurrentState() != base.smi.sm.nonOperational;
			bool flag2 = assignedGeyser != null && this.GetCurrentState() != base.smi.sm.operational.noGeyserSelected;
			bool flag3 = assignedGeyser != null && assignedGeyser.smi.GetCurrentState() != null && (assignedGeyser.smi.GetCurrentState() == assignedGeyser.smi.sm.erupt || assignedGeyser.smi.GetCurrentState().parent == assignedGeyser.smi.sm.erupt);
			bool flag4 = flag && flag2 && flag3;
			this.logicPorts.SendSignal(base.def.OUTPUT_LOGIC_PORT_ID, flag4 ? 1 : 0);
			this.switchGeyserMeter.meterController.SetSymbolVisiblity("light_bloom", flag4);
		}

		// Token: 0x060078D0 RID: 30928 RVA: 0x002C0D30 File Offset: 0x002BEF30
		public void OnCopySettings(object data)
		{
			GameObject gameObject = (GameObject)data;
			if (gameObject != null)
			{
				GeoTuner.Instance smi = gameObject.GetSMI<GeoTuner.Instance>();
				if (smi != null && smi.GetFutureGeyser() != this.GetFutureGeyser())
				{
					Geyser futureGeyser = smi.GetFutureGeyser();
					if (futureGeyser != null && futureGeyser.GetAmountOfGeotunersPointingOrWillPointAtThisGeyser() < 5)
					{
						this.AssignFutureGeyser(smi.GetFutureGeyser());
					}
				}
			}
		}

		// Token: 0x060078D1 RID: 30929 RVA: 0x002C0D90 File Offset: 0x002BEF90
		protected override void OnCleanUp()
		{
			Geyser assignedGeyser = this.GetAssignedGeyser();
			Components.GeoTuners.Remove(base.smi.GetMyWorldId(), this);
			if (assignedGeyser != null)
			{
				assignedGeyser.Unsubscribe(-593169791, new Action<object>(base.smi.OnEruptionStateChanged));
			}
			GeoTuner.RemoveTuning(this);
		}

		// Token: 0x04005CD4 RID: 23764
		[MyCmpReq]
		public Operational operational;

		// Token: 0x04005CD5 RID: 23765
		[MyCmpReq]
		public Storage storage;

		// Token: 0x04005CD6 RID: 23766
		[MyCmpReq]
		public ManualDeliveryKG manualDelivery;

		// Token: 0x04005CD7 RID: 23767
		[MyCmpReq]
		public GeoTunerWorkable workable;

		// Token: 0x04005CD8 RID: 23768
		[MyCmpReq]
		public GeoTunerSwitchGeyserWorkable switchGeyserWorkable;

		// Token: 0x04005CD9 RID: 23769
		[MyCmpReq]
		public LogicPorts logicPorts;

		// Token: 0x04005CDA RID: 23770
		[MyCmpReq]
		public RoomTracker roomTracker;

		// Token: 0x04005CDB RID: 23771
		[MyCmpReq]
		public KBatchedAnimController animController;

		// Token: 0x04005CDC RID: 23772
		public MeterController switchGeyserMeter;

		// Token: 0x04005CDD RID: 23773
		public string originID;

		// Token: 0x04005CDE RID: 23774
		public float enhancementDuration;

		// Token: 0x04005CDF RID: 23775
		public Geyser.GeyserModification currentGeyserModification;

		// Token: 0x04005CE0 RID: 23776
		private Chore switchGeyserChore;
	}
}
