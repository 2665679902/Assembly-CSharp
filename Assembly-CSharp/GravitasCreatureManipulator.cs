using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005CB RID: 1483
public class GravitasCreatureManipulator : GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>
{
	// Token: 0x060024E7 RID: 9447 RVA: 0x000C777C File Offset: 0x000C597C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.Enter(delegate(GravitasCreatureManipulator.Instance smi)
		{
			smi.DropCritter();
		}).Enter(delegate(GravitasCreatureManipulator.Instance smi)
		{
			smi.UpdateMeter();
		}).EventHandler(GameHashes.BuildingActivated, delegate(GravitasCreatureManipulator.Instance smi, object activated)
		{
			if ((bool)activated)
			{
				StoryManager.Instance.BeginStoryEvent(Db.Get().Stories.CreatureManipulator);
			}
		});
		this.inoperational.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.operational.idle, (GravitasCreatureManipulator.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.operational.DefaultState(this.operational.idle).EventTransition(GameHashes.OperationalChanged, this.inoperational, (GravitasCreatureManipulator.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		this.operational.idle.PlayAnim("idle", KAnim.PlayMode.Loop).Enter(new StateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State.Callback(GravitasCreatureManipulator.CheckForCritter)).ToggleMainStatusItem(Db.Get().BuildingStatusItems.CreatureManipulatorWaiting, null)
			.ParamTransition<GameObject>(this.creatureTarget, this.operational.capture, (GravitasCreatureManipulator.Instance smi, GameObject p) => p != null && !smi.IsCritterStored)
			.ParamTransition<GameObject>(this.creatureTarget, this.operational.working.pre, (GravitasCreatureManipulator.Instance smi, GameObject p) => p != null && smi.IsCritterStored)
			.ParamTransition<float>(this.cooldownTimer, this.operational.cooldown, GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.IsGTZero);
		this.operational.capture.PlayAnim("working_capture").OnAnimQueueComplete(this.operational.working.pre);
		this.operational.working.DefaultState(this.operational.working.pre).ToggleMainStatusItem(Db.Get().BuildingStatusItems.CreatureManipulatorWorking, null);
		this.operational.working.pre.PlayAnim("working_pre").OnAnimQueueComplete(this.operational.working.loop).Enter(delegate(GravitasCreatureManipulator.Instance smi)
		{
			smi.StoreCreature();
		})
			.Exit(delegate(GravitasCreatureManipulator.Instance smi)
			{
				smi.sm.workingTimer.Set(smi.def.workingDuration, smi, false);
			})
			.OnTargetLost(this.creatureTarget, this.operational.idle)
			.Target(this.creatureTarget)
			.ToggleStationaryIdling();
		this.operational.working.loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).Update(delegate(GravitasCreatureManipulator.Instance smi, float dt)
		{
			smi.sm.workingTimer.DeltaClamp(-dt, 0f, float.MaxValue, smi);
		}, UpdateRate.SIM_1000ms, false).ParamTransition<float>(this.workingTimer, this.operational.working.pst, GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.IsLTEZero)
			.OnTargetLost(this.creatureTarget, this.operational.idle);
		this.operational.working.pst.PlayAnim("working_pst").Enter(delegate(GravitasCreatureManipulator.Instance smi)
		{
			smi.DropCritter();
		}).OnAnimQueueComplete(this.operational.cooldown);
		GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State state = this.operational.cooldown.PlayAnim("working_cooldown", KAnim.PlayMode.Loop).Update(delegate(GravitasCreatureManipulator.Instance smi, float dt)
		{
			smi.sm.cooldownTimer.DeltaClamp(-dt, 0f, float.MaxValue, smi);
		}, UpdateRate.SIM_1000ms, false).ParamTransition<float>(this.cooldownTimer, this.operational.idle, GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.IsLTEZero);
		string text = CREATURES.STATUSITEMS.GRAVITAS_CREATURE_MANIPULATOR_COOLDOWN.NAME;
		string text2 = CREATURES.STATUSITEMS.GRAVITAS_CREATURE_MANIPULATOR_COOLDOWN.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		HashedString hashedString = default(HashedString);
		int num = 129022;
		StatusItemCategory main = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, hashedString, num, new Func<string, GravitasCreatureManipulator.Instance, string>(GravitasCreatureManipulator.Processing), new Func<string, GravitasCreatureManipulator.Instance, string>(GravitasCreatureManipulator.ProcessingTooltip), main);
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x000C7BD8 File Offset: 0x000C5DD8
	private static string Processing(string str, GravitasCreatureManipulator.Instance smi)
	{
		return str.Replace("{percent}", GameUtil.GetFormattedPercent((1f - smi.sm.cooldownTimer.Get(smi) / smi.def.cooldownDuration) * 100f, GameUtil.TimeSlice.None));
	}

	// Token: 0x060024E9 RID: 9449 RVA: 0x000C7C14 File Offset: 0x000C5E14
	private static string ProcessingTooltip(string str, GravitasCreatureManipulator.Instance smi)
	{
		return str.Replace("{timeleft}", GameUtil.GetFormattedTime(smi.sm.cooldownTimer.Get(smi), "F0"));
	}

	// Token: 0x060024EA RID: 9450 RVA: 0x000C7C3C File Offset: 0x000C5E3C
	private static void CheckForCritter(GravitasCreatureManipulator.Instance smi)
	{
		if (smi.sm.creatureTarget.IsNull(smi))
		{
			GameObject gameObject = Grid.Objects[smi.pickupCell, 3];
			if (gameObject != null)
			{
				ObjectLayerListItem objectLayerListItem = gameObject.GetComponent<Pickupable>().objectLayerListItem;
				while (objectLayerListItem != null)
				{
					GameObject gameObject2 = objectLayerListItem.gameObject;
					objectLayerListItem = objectLayerListItem.nextItem;
					if (!(gameObject2 == null) && smi.IsAccepted(gameObject2))
					{
						smi.SetCritterTarget(gameObject2);
						return;
					}
				}
			}
		}
	}

	// Token: 0x04001546 RID: 5446
	public GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State inoperational;

	// Token: 0x04001547 RID: 5447
	public GravitasCreatureManipulator.ActiveStates operational;

	// Token: 0x04001548 RID: 5448
	public StateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.TargetParameter creatureTarget;

	// Token: 0x04001549 RID: 5449
	public StateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.FloatParameter cooldownTimer;

	// Token: 0x0400154A RID: 5450
	public StateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.FloatParameter workingTimer;

	// Token: 0x02001216 RID: 4630
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005CF7 RID: 23799
		public CellOffset pickupOffset;

		// Token: 0x04005CF8 RID: 23800
		public CellOffset dropOffset;

		// Token: 0x04005CF9 RID: 23801
		public int numSpeciesToUnlockMorphMode;

		// Token: 0x04005CFA RID: 23802
		public float workingDuration;

		// Token: 0x04005CFB RID: 23803
		public float cooldownDuration;
	}

	// Token: 0x02001217 RID: 4631
	public class WorkingStates : GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State
	{
		// Token: 0x04005CFC RID: 23804
		public GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State pre;

		// Token: 0x04005CFD RID: 23805
		public GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State loop;

		// Token: 0x04005CFE RID: 23806
		public GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State pst;
	}

	// Token: 0x02001218 RID: 4632
	public class ActiveStates : GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State
	{
		// Token: 0x04005CFF RID: 23807
		public GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State idle;

		// Token: 0x04005D00 RID: 23808
		public GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State capture;

		// Token: 0x04005D01 RID: 23809
		public GravitasCreatureManipulator.WorkingStates working;

		// Token: 0x04005D02 RID: 23810
		public GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.State cooldown;
	}

	// Token: 0x02001219 RID: 4633
	public new class Instance : GameStateMachine<GravitasCreatureManipulator, GravitasCreatureManipulator.Instance, IStateMachineTarget, GravitasCreatureManipulator.Def>.GameInstance
	{
		// Token: 0x060078F2 RID: 30962 RVA: 0x002C11B0 File Offset: 0x002BF3B0
		public Instance(IStateMachineTarget master, GravitasCreatureManipulator.Def def)
			: base(master, def)
		{
			this.pickupCell = Grid.OffsetCell(Grid.PosToCell(master.gameObject), base.smi.def.pickupOffset);
			this.m_partitionEntry = GameScenePartitioner.Instance.Add("GravitasCreatureManipulator", base.gameObject, this.pickupCell, GameScenePartitioner.Instance.pickupablesChangedLayer, new Action<object>(this.DetectCreature));
			this.m_largeCreaturePartitionEntry = GameScenePartitioner.Instance.Add("GravitasCreatureManipulator.large", base.gameObject, Grid.CellLeft(this.pickupCell), GameScenePartitioner.Instance.pickupablesChangedLayer, new Action<object>(this.DetectLargeCreature));
			this.m_progressMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.TileFront, Array.Empty<string>());
		}

		// Token: 0x060078F3 RID: 30963 RVA: 0x002C128C File Offset: 0x002BF48C
		public override void StartSM()
		{
			base.StartSM();
			this.UpdateStatusItems();
			this.UpdateMeter();
			StoryManager.Instance.ForceCreateStory(Db.Get().Stories.CreatureManipulator, base.gameObject.GetMyWorldId());
			if (this.ScannedSpecies.Count >= base.smi.def.numSpeciesToUnlockMorphMode)
			{
				StoryManager.Instance.BeginStoryEvent(Db.Get().Stories.CreatureManipulator);
			}
			this.TryShowCompletedNotification();
			base.Subscribe(-1503271301, new Action<object>(this.OnBuildingSelect));
			StoryManager.Instance.DiscoverStoryEvent(Db.Get().Stories.CreatureManipulator);
		}

		// Token: 0x060078F4 RID: 30964 RVA: 0x002C133B File Offset: 0x002BF53B
		public override void StopSM(string reason)
		{
			base.Unsubscribe(-1503271301, new Action<object>(this.OnBuildingSelect));
			base.StopSM(reason);
		}

		// Token: 0x060078F5 RID: 30965 RVA: 0x002C135B File Offset: 0x002BF55B
		private void OnBuildingSelect(object obj)
		{
			if (!(bool)obj)
			{
				return;
			}
			if (!this.m_introPopupSeen)
			{
				this.ShowIntroNotification();
			}
			if (this.m_endNotification != null)
			{
				this.m_endNotification.customClickCallback(this.m_endNotification.customClickData);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060078F6 RID: 30966 RVA: 0x002C1397 File Offset: 0x002BF597
		public bool IsMorphMode
		{
			get
			{
				return this.m_morphModeUnlocked;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060078F7 RID: 30967 RVA: 0x002C139F File Offset: 0x002BF59F
		public bool IsCritterStored
		{
			get
			{
				return this.m_storage.Count > 0;
			}
		}

		// Token: 0x060078F8 RID: 30968 RVA: 0x002C13B0 File Offset: 0x002BF5B0
		private void UpdateStatusItems()
		{
			KSelectable component = base.gameObject.GetComponent<KSelectable>();
			component.ToggleStatusItem(Db.Get().BuildingStatusItems.CreatureManipulatorProgress, !this.IsMorphMode, this);
			component.ToggleStatusItem(Db.Get().BuildingStatusItems.CreatureManipulatorMorphMode, this.IsMorphMode, this);
			component.ToggleStatusItem(Db.Get().BuildingStatusItems.CreatureManipulatorMorphModeLocked, !this.IsMorphMode, this);
		}

		// Token: 0x060078F9 RID: 30969 RVA: 0x002C1424 File Offset: 0x002BF624
		public void UpdateMeter()
		{
			this.m_progressMeter.SetPositionPercent(Mathf.Clamp01((float)this.ScannedSpecies.Count / (float)base.smi.def.numSpeciesToUnlockMorphMode - 0.1f));
		}

		// Token: 0x060078FA RID: 30970 RVA: 0x002C145C File Offset: 0x002BF65C
		public bool IsAccepted(GameObject go)
		{
			KPrefabID component = go.GetComponent<KPrefabID>();
			return component.HasTag(GameTags.Creature) && !component.HasTag(GameTags.Robot) && component.PrefabTag != GameTags.Creature;
		}

		// Token: 0x060078FB RID: 30971 RVA: 0x002C149C File Offset: 0x002BF69C
		private void DetectLargeCreature(object obj)
		{
			Pickupable pickupable = obj as Pickupable;
			if (pickupable == null)
			{
				return;
			}
			if (pickupable.GetComponent<KCollider2D>().bounds.size.x > 1.5f)
			{
				this.DetectCreature(obj);
				return;
			}
			if (base.smi.IsInsideState(base.sm.operational.idle))
			{
				Navigator component = pickupable.GetComponent<Navigator>();
				if (component != null && this.IsAccepted(component.gameObject) && !pickupable.HasTag(GameTags.Dead))
				{
					component.GoTo(base.smi.pickupCell, null);
				}
			}
		}

		// Token: 0x060078FC RID: 30972 RVA: 0x002C153C File Offset: 0x002BF73C
		private void DetectCreature(object obj)
		{
			Pickupable pickupable = obj as Pickupable;
			if (pickupable != null && this.IsAccepted(pickupable.gameObject) && base.smi.sm.creatureTarget.IsNull(base.smi) && base.smi.IsInsideState(base.smi.sm.operational.idle))
			{
				this.SetCritterTarget(pickupable.gameObject);
			}
		}

		// Token: 0x060078FD RID: 30973 RVA: 0x002C15B2 File Offset: 0x002BF7B2
		public void SetCritterTarget(GameObject go)
		{
			base.smi.sm.creatureTarget.Set(go.gameObject, base.smi, false);
		}

		// Token: 0x060078FE RID: 30974 RVA: 0x002C15D8 File Offset: 0x002BF7D8
		public void StoreCreature()
		{
			GameObject gameObject = base.smi.sm.creatureTarget.Get(base.smi);
			this.m_storage.Store(gameObject, false, false, true, false);
		}

		// Token: 0x060078FF RID: 30975 RVA: 0x002C1614 File Offset: 0x002BF814
		public void DropCritter()
		{
			List<GameObject> list = new List<GameObject>();
			Vector3 vector = Grid.CellToPosCBC(Grid.PosToCell(base.smi), Grid.SceneLayer.Creatures);
			this.m_storage.DropAll(vector, false, false, base.smi.def.dropOffset.ToVector3(), true, list);
			foreach (GameObject gameObject in list)
			{
				CreatureBrain component = gameObject.GetComponent<CreatureBrain>();
				if (!(component == null))
				{
					this.Scan(component.species);
					if (component.HasTag(GameTags.OriginalCreature) && this.IsMorphMode)
					{
						this.SpawnMorph(component);
					}
					else
					{
						gameObject.GetSMI<AnimInterruptMonitor.Instance>().PlayAnim("idle_loop");
					}
				}
			}
			base.smi.sm.creatureTarget.Set(null, base.smi);
		}

		// Token: 0x06007900 RID: 30976 RVA: 0x002C170C File Offset: 0x002BF90C
		private void Scan(Tag species)
		{
			if (this.ScannedSpecies.Add(species))
			{
				base.gameObject.Trigger(1980521255, null);
				this.UpdateStatusItems();
				this.UpdateMeter();
				this.ShowCritterScannedNotification(species);
			}
			this.TryShowCompletedNotification();
		}

		// Token: 0x06007901 RID: 30977 RVA: 0x002C1748 File Offset: 0x002BF948
		public void SpawnMorph(Brain brain)
		{
			Tag tag = Tag.Invalid;
			BabyMonitor.Instance smi = brain.GetSMI<BabyMonitor.Instance>();
			FertilityMonitor.Instance smi2 = brain.GetSMI<FertilityMonitor.Instance>();
			bool flag = smi != null;
			bool flag2 = smi2 != null;
			if (flag2)
			{
				tag = FertilityMonitor.EggBreedingRoll(smi2.breedingChances, true);
			}
			else if (flag)
			{
				FertilityMonitor.Def def = Assets.GetPrefab(smi.def.adultPrefab).GetDef<FertilityMonitor.Def>();
				if (def == null)
				{
					return;
				}
				tag = FertilityMonitor.EggBreedingRoll(def.initialBreedingWeights, true);
			}
			if (!tag.IsValid)
			{
				return;
			}
			Tag tag2 = Assets.GetPrefab(tag).GetDef<IncubationMonitor.Def>().spawnedCreature;
			if (flag2)
			{
				tag2 = Assets.GetPrefab(tag2).GetDef<BabyMonitor.Def>().adultPrefab;
			}
			Vector3 position = brain.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Creatures);
			GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(tag2), position);
			gameObject.SetActive(true);
			gameObject.GetSMI<AnimInterruptMonitor.Instance>().PlayAnim("growup_pst");
			foreach (AmountInstance amountInstance in brain.gameObject.GetAmounts())
			{
				AmountInstance amountInstance2 = amountInstance.amount.Lookup(gameObject);
				if (amountInstance2 != null)
				{
					float num = amountInstance.value / amountInstance.GetMax();
					amountInstance2.value = num * amountInstance2.GetMax();
				}
			}
			gameObject.Trigger(-2027483228, brain.gameObject);
			KSelectable component = brain.gameObject.GetComponent<KSelectable>();
			if (SelectTool.Instance != null && SelectTool.Instance.selected != null && SelectTool.Instance.selected == component)
			{
				SelectTool.Instance.Select(gameObject.GetComponent<KSelectable>(), false);
			}
			base.smi.sm.cooldownTimer.Set(base.smi.def.cooldownDuration, base.smi, false);
			brain.gameObject.DeleteObject();
		}

		// Token: 0x06007902 RID: 30978 RVA: 0x002C1948 File Offset: 0x002BFB48
		public void ShowIntroNotification()
		{
			Game.Instance.unlocks.Unlock("story_trait_critter_manipulator_initial", true);
			this.m_introPopupSeen = true;
			EventInfoScreen.ShowPopup(EventInfoDataHelper.GenerateStoryTraitData(CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.BEGIN_POPUP.NAME, CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.BEGIN_POPUP.DESCRIPTION, CODEX.STORY_TRAITS.CLOSE_BUTTON, "crittermanipulatoractivate_kanim", EventInfoDataHelper.PopupType.BEGIN, null, null, null));
		}

		// Token: 0x06007903 RID: 30979 RVA: 0x002C19A4 File Offset: 0x002BFBA4
		public void ShowCritterScannedNotification(Tag species)
		{
			GravitasCreatureManipulator.Instance.<>c__DisplayClass29_0 CS$<>8__locals1 = new GravitasCreatureManipulator.Instance.<>c__DisplayClass29_0();
			CS$<>8__locals1.species = species;
			CS$<>8__locals1.<>4__this = this;
			string text = GravitasCreatureManipulatorConfig.CRITTER_LORE_UNLOCK_ID.For(CS$<>8__locals1.species);
			Game.Instance.unlocks.Unlock(text, false);
			CS$<>8__locals1.<ShowCritterScannedNotification>g__ShowCritterScannedNotificationAndWaitForClick|1().Then(delegate
			{
				GravitasCreatureManipulator.Instance.ShowLoreUnlockedPopup(CS$<>8__locals1.species);
			});
		}

		// Token: 0x06007904 RID: 30980 RVA: 0x002C19FC File Offset: 0x002BFBFC
		public static void ShowLoreUnlockedPopup(Tag species)
		{
			InfoDialogScreen infoDialogScreen = LoreBearer.ShowPopupDialog().SetHeader(CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.UNLOCK_SPECIES_POPUP.NAME).AddDefaultOK(false);
			bool flag = CodexCache.GetEntryForLock(GravitasCreatureManipulatorConfig.CRITTER_LORE_UNLOCK_ID.For(species)) != null;
			Option<string> bodyContentForSpeciesTag = GravitasCreatureManipulatorConfig.GetBodyContentForSpeciesTag(species);
			if (flag && bodyContentForSpeciesTag.HasValue)
			{
				infoDialogScreen.AddPlainText(bodyContentForSpeciesTag.Value).AddOption(CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.UNLOCK_SPECIES_POPUP.VIEW_IN_CODEX, LoreBearerUtil.OpenCodexByEntryID("STORYTRAITCRITTERMANIPULATOR"), false);
				return;
			}
			infoDialogScreen.AddPlainText(GravitasCreatureManipulatorConfig.GetBodyContentForUnknownSpecies());
		}

		// Token: 0x06007905 RID: 30981 RVA: 0x002C1A7C File Offset: 0x002BFC7C
		public void TryShowCompletedNotification()
		{
			if (this.ScannedSpecies.Count < base.smi.def.numSpeciesToUnlockMorphMode)
			{
				return;
			}
			if (this.IsMorphMode)
			{
				return;
			}
			this.eventInfo = EventInfoDataHelper.GenerateStoryTraitData(CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.END_POPUP.NAME, CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.END_POPUP.DESCRIPTION, CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.END_POPUP.BUTTON, "crittermanipulatormorphmode_kanim", EventInfoDataHelper.PopupType.COMPLETE, null, null, null);
			this.m_endNotification = EventInfoScreen.CreateNotification(this.eventInfo, new Notification.ClickCallback(this.UnlockMorphMode));
			base.gameObject.AddOrGet<Notifier>().Add(this.m_endNotification, "");
			base.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.AttentionRequired, base.smi);
		}

		// Token: 0x06007906 RID: 30982 RVA: 0x002C1B40 File Offset: 0x002BFD40
		public void ClearEndNotification()
		{
			base.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.AttentionRequired, false);
			if (this.m_endNotification != null)
			{
				base.gameObject.AddOrGet<Notifier>().Remove(this.m_endNotification);
			}
			this.m_endNotification = null;
		}

		// Token: 0x06007907 RID: 30983 RVA: 0x002C1B94 File Offset: 0x002BFD94
		public void UnlockMorphMode(object _)
		{
			if (this.m_morphModeUnlocked)
			{
				return;
			}
			Game.Instance.unlocks.Unlock("story_trait_critter_manipulator_complete", true);
			if (this.m_endNotification != null)
			{
				base.gameObject.AddOrGet<Notifier>().Remove(this.m_endNotification);
			}
			this.m_morphModeUnlocked = true;
			this.UpdateStatusItems();
			this.ClearEndNotification();
			Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(Grid.PosToCell(base.smi), new CellOffset(0, 2)), Grid.SceneLayer.Ore);
			StoryManager.Instance.CompleteStoryEvent(Db.Get().Stories.CreatureManipulator, base.gameObject.GetComponent<MonoBehaviour>(), new FocusTargetSequence.Data
			{
				WorldId = base.smi.GetMyWorldId(),
				OrthographicSize = 6f,
				TargetSize = 6f,
				Target = vector,
				PopupData = this.eventInfo,
				CompleteCB = new System.Action(this.OnStorySequenceComplete),
				CanCompleteCB = null
			});
		}

		// Token: 0x06007908 RID: 30984 RVA: 0x002C1C98 File Offset: 0x002BFE98
		private void OnStorySequenceComplete()
		{
			Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(Grid.PosToCell(base.smi), new CellOffset(-1, 1)), Grid.SceneLayer.Ore);
			StoryManager.Instance.CompleteStoryEvent(Db.Get().Stories.CreatureManipulator, vector);
			this.eventInfo = null;
		}

		// Token: 0x06007909 RID: 30985 RVA: 0x002C1CE5 File Offset: 0x002BFEE5
		protected override void OnCleanUp()
		{
			GameScenePartitioner.Instance.Free(ref this.m_partitionEntry);
			GameScenePartitioner.Instance.Free(ref this.m_largeCreaturePartitionEntry);
			if (this.m_endNotification != null)
			{
				base.gameObject.AddOrGet<Notifier>().Remove(this.m_endNotification);
			}
		}

		// Token: 0x04005D03 RID: 23811
		public int pickupCell;

		// Token: 0x04005D04 RID: 23812
		[MyCmpGet]
		private Storage m_storage;

		// Token: 0x04005D05 RID: 23813
		[Serialize]
		public HashSet<Tag> ScannedSpecies = new HashSet<Tag>();

		// Token: 0x04005D06 RID: 23814
		[Serialize]
		private bool m_introPopupSeen;

		// Token: 0x04005D07 RID: 23815
		[Serialize]
		private bool m_morphModeUnlocked;

		// Token: 0x04005D08 RID: 23816
		private EventInfoData eventInfo;

		// Token: 0x04005D09 RID: 23817
		private Notification m_endNotification;

		// Token: 0x04005D0A RID: 23818
		private MeterController m_progressMeter;

		// Token: 0x04005D0B RID: 23819
		private HandleVector<int>.Handle m_partitionEntry;

		// Token: 0x04005D0C RID: 23820
		private HandleVector<int>.Handle m_largeCreaturePartitionEntry;
	}
}
