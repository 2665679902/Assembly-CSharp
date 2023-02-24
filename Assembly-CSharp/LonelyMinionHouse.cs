using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020005FF RID: 1535
public class LonelyMinionHouse : StoryTraitStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, LonelyMinionHouse.Def>
{
	// Token: 0x060027E6 RID: 10214 RVA: 0x000D46D8 File Offset: 0x000D28D8
	private bool ValidateOperationalTransition(LonelyMinionHouse.Instance smi)
	{
		Operational component = smi.GetComponent<Operational>();
		bool flag = smi.IsInsideState(smi.sm.Active);
		return component != null && flag != component.IsOperational;
	}

	// Token: 0x060027E7 RID: 10215 RVA: 0x000D4715 File Offset: 0x000D2915
	private static bool AllQuestsComplete(LonelyMinionHouse.Instance smi)
	{
		return 1f - smi.sm.QuestProgress.Get(smi) <= Mathf.Epsilon;
	}

	// Token: 0x060027E8 RID: 10216 RVA: 0x000D4738 File Offset: 0x000D2938
	public static void EvaluateLights(LonelyMinionHouse.Instance smi, float dt)
	{
		bool flag = smi.IsInsideState(smi.sm.Active);
		QuestInstance instance = QuestManager.GetInstance(smi.QuestOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
		if (!flag || !smi.Light.enabled || instance.IsComplete)
		{
			return;
		}
		bool flag2;
		bool flag3;
		instance.TrackProgress(new Quest.ItemData
		{
			CriteriaId = LonelyMinionConfig.PowerCriteriaId,
			CurrentValue = instance.GetCurrentValue(LonelyMinionConfig.PowerCriteriaId, 0) + dt
		}, out flag2, out flag3);
	}

	// Token: 0x060027E9 RID: 10217 RVA: 0x000D47C0 File Offset: 0x000D29C0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.Inactive;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.Update(new Action<LonelyMinionHouse.Instance, float>(LonelyMinionHouse.EvaluateLights), UpdateRate.SIM_1000ms, false);
		this.Inactive.EventTransition(GameHashes.OperationalChanged, this.Active, new StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition));
		this.Active.Enter(delegate(LonelyMinionHouse.Instance smi)
		{
			smi.OnPoweredStateChanged(smi.GetComponent<NonEssentialEnergyConsumer>().IsPowered);
		}).Exit(delegate(LonelyMinionHouse.Instance smi)
		{
			smi.OnPoweredStateChanged(smi.GetComponent<NonEssentialEnergyConsumer>().IsPowered);
		}).OnSignal(this.CompleteStory, this.Active.StoryComplete, new Func<LonelyMinionHouse.Instance, bool>(LonelyMinionHouse.AllQuestsComplete))
			.EventTransition(GameHashes.OperationalChanged, this.Inactive, new StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition));
		this.Active.StoryComplete.Enter(new StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.State.Callback(LonelyMinionHouse.ActiveStates.OnEnterStoryComplete));
	}

	// Token: 0x060027EA RID: 10218 RVA: 0x000D48C4 File Offset: 0x000D2AC4
	public static float CalculateAverageDecor(Extents area)
	{
		float num = 0f;
		int num2 = Grid.XYToCell(area.x, area.y);
		for (int i = 0; i < area.width * area.height; i++)
		{
			int num3 = Grid.OffsetCell(num2, i % area.width, i / area.width);
			num += Grid.Decor[num3];
		}
		return num / (float)(area.width * area.height);
	}

	// Token: 0x04001777 RID: 6007
	public GameStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.State Inactive;

	// Token: 0x04001778 RID: 6008
	public LonelyMinionHouse.ActiveStates Active;

	// Token: 0x04001779 RID: 6009
	public StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.Signal MailDelivered;

	// Token: 0x0400177A RID: 6010
	public StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.Signal CompleteStory;

	// Token: 0x0400177B RID: 6011
	public StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.FloatParameter QuestProgress;

	// Token: 0x02001261 RID: 4705
	public class Def : StoryTraitStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, LonelyMinionHouse.Def>.TraitDef
	{
	}

	// Token: 0x02001262 RID: 4706
	public new class Instance : StoryTraitStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, LonelyMinionHouse.Def>.TraitInstance, ICheckboxListGroupControl
	{
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060079F0 RID: 31216 RVA: 0x002C46D0 File Offset: 0x002C28D0
		public HashedString QuestOwnerId
		{
			get
			{
				return this.questOwnerId;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060079F1 RID: 31217 RVA: 0x002C46D8 File Offset: 0x002C28D8
		public KBatchedAnimController AnimController
		{
			get
			{
				return this.animControllers[0];
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060079F2 RID: 31218 RVA: 0x002C46E2 File Offset: 0x002C28E2
		public KBatchedAnimController LightsController
		{
			get
			{
				return this.animControllers[1];
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060079F3 RID: 31219 RVA: 0x002C46EC File Offset: 0x002C28EC
		public KBatchedAnimController BlindsController
		{
			get
			{
				return this.blinds.meterController;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060079F4 RID: 31220 RVA: 0x002C46F9 File Offset: 0x002C28F9
		public Light2D Light
		{
			get
			{
				return this.light;
			}
		}

		// Token: 0x060079F5 RID: 31221 RVA: 0x002C4701 File Offset: 0x002C2901
		public Instance(StateMachineController master, LonelyMinionHouse.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060079F6 RID: 31222 RVA: 0x002C4714 File Offset: 0x002C2914
		public override void StartSM()
		{
			this.animControllers = base.gameObject.GetComponentsInChildren<KBatchedAnimController>(true);
			this.light = this.LightsController.GetComponent<Light2D>();
			this.light.transform.position += Vector3.forward * Grid.GetLayerZ(Grid.SceneLayer.TransferArm);
			this.light.gameObject.SetActive(true);
			this.lightsLink = new KAnimLink(this.AnimController, this.LightsController);
			Activatable component = base.GetComponent<Activatable>();
			component.SetOffsets(new CellOffset[]
			{
				new CellOffset(-3, 0)
			});
			if (!component.IsActivated)
			{
				Activatable activatable = component;
				activatable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(activatable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
				Activatable activatable2 = component;
				activatable2.onActivate = (System.Action)Delegate.Combine(activatable2.onActivate, new System.Action(this.StartStoryTrait));
			}
			this.meter = new MeterController(this.AnimController, "meter_storage_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.TransferArm, LonelyMinionHouseConfig.METER_SYMBOLS);
			this.blinds = new MeterController(this.AnimController, "blinds_target", string.Format("{0}_{1}", "meter_blinds", 0), Meter.Offset.UserSpecified, Grid.SceneLayer.TransferArm, LonelyMinionHouseConfig.BLINDS_SYMBOLS);
			KPrefabID component2 = base.GetComponent<KPrefabID>();
			this.questOwnerId = new HashedString(component2.PrefabTag.GetHash());
			this.SpawnMinion();
			if (this.lonelyMinion != null && !this.TryFindMailbox())
			{
				GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingLayerChanged));
			}
			QuestManager.InitializeQuest(this.questOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest);
			QuestInstance questInstance = QuestManager.InitializeQuest(this.questOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
			QuestInstance questInstance2 = QuestManager.InitializeQuest(this.questOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
			QuestInstance questInstance3 = QuestManager.InitializeQuest(this.questOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
			NonEssentialEnergyConsumer component3 = base.GetComponent<NonEssentialEnergyConsumer>();
			NonEssentialEnergyConsumer nonEssentialEnergyConsumer = component3;
			nonEssentialEnergyConsumer.PoweredStateChanged = (Action<bool>)Delegate.Combine(nonEssentialEnergyConsumer.PoweredStateChanged, new Action<bool>(this.OnPoweredStateChanged));
			this.OnPoweredStateChanged(component3.IsPowered);
			if (this.lonelyMinion == null)
			{
				base.StartSM();
				return;
			}
			base.Subscribe(-592767678, new Action<object>(this.OnBuildingActivated));
			base.StartSM();
			QuestInstance questInstance4 = questInstance;
			questInstance4.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Combine(questInstance4.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			QuestInstance questInstance5 = questInstance2;
			questInstance5.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Combine(questInstance5.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			QuestInstance questInstance6 = questInstance3;
			questInstance6.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Combine(questInstance6.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			float num = base.sm.QuestProgress.Get(this) * 3f;
			int num2 = (Mathf.Approximately(num, Mathf.Ceil(num)) ? Mathf.CeilToInt(num) : Mathf.FloorToInt(num));
			if (num2 == 0)
			{
				return;
			}
			HashedString[] array = new HashedString[num2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = string.Format("{0}_{1}", "meter_blinds", i);
			}
			this.blinds.meterController.Play(array, KAnim.PlayMode.Once);
		}

		// Token: 0x060079F7 RID: 31223 RVA: 0x002C4A80 File Offset: 0x002C2C80
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			Activatable component = base.GetComponent<Activatable>();
			component.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(component.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
			component.onActivate = (System.Action)Delegate.Remove(component.onActivate, new System.Action(this.StartStoryTrait));
			base.Unsubscribe(-592767678, new Action<object>(this.OnBuildingActivated));
		}

		// Token: 0x060079F8 RID: 31224 RVA: 0x002C4AF8 File Offset: 0x002C2CF8
		private void OnQuestProgressChanged(QuestInstance quest, Quest.State prevState, float delta)
		{
			float num = base.sm.QuestProgress.Get(this);
			num += delta / 3f;
			if (1f - num <= 0.001f)
			{
				num = 1f;
			}
			base.sm.QuestProgress.Set(Mathf.Clamp01(num), this, true);
			this.lonelyMinion.UnlockQuestIdle(quest, prevState, delta);
			this.lonelyMinion.ShowQuestCompleteNotification(quest, prevState, 0f);
			base.gameObject.Trigger(1980521255, null);
			if (!quest.IsComplete)
			{
				return;
			}
			if (num == 1f)
			{
				base.sm.CompleteStory.Trigger(this);
			}
			float num2 = num * 3f;
			int num3 = (Mathf.Approximately(num2, Mathf.Ceil(num2)) ? Mathf.CeilToInt(num2) : Mathf.FloorToInt(num2));
			this.blinds.meterController.Queue(string.Format("{0}_{1}", "meter_blinds", num3 - 1), KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x002C4BFD File Offset: 0x002C2DFD
		public void MailboxContentChanged(GameObject item)
		{
			this.lonelyMinion.sm.Mail.Set(item, this.lonelyMinion, false);
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x002C4C20 File Offset: 0x002C2E20
		public override void CompleteEvent()
		{
			if (this.lonelyMinion == null)
			{
				base.smi.AnimController.Play(LonelyMinionHouseConfig.STORAGE, KAnim.PlayMode.Loop, 1f, 0f);
				base.gameObject.AddOrGet<TreeFilterable>();
				base.gameObject.AddOrGet<BuildingEnabledButton>();
				base.gameObject.GetComponent<Deconstructable>().allowDeconstruction = true;
				base.gameObject.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.None;
				base.gameObject.GetComponent<Prioritizable>().SetMasterPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, 5));
				Storage component = base.GetComponent<Storage>();
				component.allowItemRemoval = true;
				component.showInUI = true;
				component.showDescriptor = true;
				component.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(component.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
				this.storageFilter = new FilteredStorage(base.smi.GetComponent<KPrefabID>(), null, null, false, Db.Get().ChoreTypes.StorageFetch);
				this.storageFilter.SetMeter(this.meter);
				this.meter = null;
				RootMenu.Instance.Refresh();
				component.RenotifyAll();
				return;
			}
			List<MinionIdentity> list = new List<MinionIdentity>(Components.LiveMinionIdentities.Items);
			list.Shuffle<MinionIdentity>();
			int num = 3;
			base.def.EventCompleteInfo.Minions = new GameObject[1 + Mathf.Min(num, list.Count)];
			base.def.EventCompleteInfo.Minions[0] = this.lonelyMinion.gameObject;
			int num2 = 0;
			while (num2 < list.Count && num > 0)
			{
				base.def.EventCompleteInfo.Minions[num2 + 1] = list[num2].gameObject;
				num--;
				num2++;
			}
			base.CompleteEvent();
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x002C4DD0 File Offset: 0x002C2FD0
		public override void OnCompleteStorySequence()
		{
			this.SpawnMinion();
			base.Unsubscribe(-592767678, new Action<object>(this.OnBuildingActivated));
			base.OnCompleteStorySequence();
			QuestInstance instance = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
			instance.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Remove(instance.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			QuestInstance instance2 = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
			instance2.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Remove(instance2.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			QuestInstance instance3 = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
			instance3.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Remove(instance3.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			this.blinds.meterController.Play(this.blinds.meterController.initialAnim, this.blinds.meterController.initialMode, 1f, 0f);
			base.smi.AnimController.Play(LonelyMinionHouseConfig.STORAGE, KAnim.PlayMode.Loop, 1f, 0f);
			base.gameObject.AddOrGet<TreeFilterable>();
			base.gameObject.AddOrGet<BuildingEnabledButton>();
			base.gameObject.GetComponent<Deconstructable>().allowDeconstruction = true;
			base.gameObject.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.None;
			base.gameObject.GetComponent<Prioritizable>().SetMasterPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, 5));
			Storage component = base.GetComponent<Storage>();
			component.allowItemRemoval = true;
			component.showInUI = true;
			component.showDescriptor = true;
			component.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(component.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
			this.storageFilter = new FilteredStorage(base.smi.GetComponent<KPrefabID>(), null, null, false, Db.Get().ChoreTypes.StorageFetch);
			this.storageFilter.SetMeter(this.meter);
			this.meter = null;
			RootMenu.Instance.Refresh();
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x002C4FE8 File Offset: 0x002C31E8
		private void SpawnMinion()
		{
			if (StoryManager.Instance.IsStoryComplete(Db.Get().Stories.LonelyMinion))
			{
				return;
			}
			if (this.lonelyMinion == null)
			{
				GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(LonelyMinionConfig.ID), base.gameObject, null);
				global::Debug.Assert(gameObject != null);
				gameObject.transform.localPosition = new Vector3(0.54f, 0f, -0.01f);
				gameObject.SetActive(true);
				Vector2I vector2I = Grid.CellToXY(Grid.PosToCell(base.gameObject));
				BuildingDef def = base.GetComponent<Building>().Def;
				this.lonelyMinion = gameObject.GetSMI<LonelyMinion.Instance>();
				this.lonelyMinion.def.QuestOwnerId = this.questOwnerId;
				this.lonelyMinion.def.DecorInspectionArea = new Extents(vector2I.x - Mathf.CeilToInt((float)def.WidthInCells / 2f) + 1, vector2I.y, def.WidthInCells, def.HeightInCells);
				return;
			}
			MinionStartingStats minionStartingStats = new MinionStartingStats(this.lonelyMinion.def.Personality, null, "AncientKnowledge", false);
			minionStartingStats.Traits.Add(Db.Get().traits.TryGet("Chatty"));
			minionStartingStats.voiceIdx = -2;
			string[] all_ATTRIBUTES = DUPLICANTSTATS.ALL_ATTRIBUTES;
			for (int i = 0; i < all_ATTRIBUTES.Length; i++)
			{
				Dictionary<string, int> startingLevels = minionStartingStats.StartingLevels;
				string text = all_ATTRIBUTES[i];
				startingLevels[text] += 7;
			}
			UnityEngine.Object.Destroy(this.lonelyMinion.gameObject);
			this.lonelyMinion = null;
			MinionIdentity minionIdentity = Util.KInstantiate<MinionIdentity>(Assets.GetPrefab(MinionConfig.ID), null, null);
			Immigration.Instance.ApplyDefaultPersonalPriorities(minionIdentity.gameObject);
			minionIdentity.gameObject.SetActive(true);
			minionStartingStats.Apply(minionIdentity.gameObject);
			minionIdentity.arrivalTime += (float)UnityEngine.Random.Range(2190, 3102);
			minionIdentity.arrivalTime *= -1f;
			MinionResume component = minionIdentity.GetComponent<MinionResume>();
			for (int j = 0; j < 3; j++)
			{
				component.ForceAddSkillPoint();
			}
			Vector3 vector = base.transform.position + Vector3.left * Grid.CellSizeInMeters * 2f;
			vector.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			minionIdentity.transform.SetPosition(vector);
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x002C5260 File Offset: 0x002C3460
		private bool TryFindMailbox()
		{
			if (base.sm.QuestProgress.Get(this) == 1f)
			{
				return true;
			}
			int num = Grid.PosToCell(base.gameObject);
			ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
			GameScenePartitioner.Instance.GatherEntries(new Extents(num, 10), GameScenePartitioner.Instance.objectLayers[1], pooledList);
			bool flag = false;
			int num2 = 0;
			while (!flag && num2 < pooledList.Count)
			{
				if ((pooledList[num2].obj as GameObject).GetComponent<KPrefabID>().PrefabTag.GetHash() == LonelyMinionMailboxConfig.IdHash.HashValue)
				{
					this.OnBuildingLayerChanged(0, pooledList[num2].obj);
					flag = true;
				}
				num2++;
			}
			pooledList.Recycle();
			return flag;
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x002C531C File Offset: 0x002C351C
		private void OnBuildingLayerChanged(int cell, object data)
		{
			GameObject gameObject = data as GameObject;
			if (gameObject == null)
			{
				return;
			}
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component.PrefabTag.GetHash() == LonelyMinionMailboxConfig.IdHash.HashValue)
			{
				component.GetComponent<LonelyMinionMailbox>().Initialize(this);
				GameScenePartitioner.Instance.RemoveGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingLayerChanged));
			}
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x002C538C File Offset: 0x002C358C
		public void OnPoweredStateChanged(bool isPowered)
		{
			this.light.enabled = isPowered && base.GetComponent<Operational>().IsOperational;
			this.LightsController.Play(this.light.enabled ? LonelyMinionHouseConfig.LIGHTS_ON : LonelyMinionHouseConfig.LIGHTS_OFF, KAnim.PlayMode.Loop, 1f, 0f);
		}

		// Token: 0x06007A00 RID: 31232 RVA: 0x002C53E4 File Offset: 0x002C35E4
		private void StartStoryTrait()
		{
			base.TriggerStoryEvent(StoryInstance.State.IN_PROGRESS);
		}

		// Token: 0x06007A01 RID: 31233 RVA: 0x002C53F0 File Offset: 0x002C35F0
		protected override void OnBuildingActivated(object data)
		{
			if (!this.IsIntroSequenceComplete())
			{
				return;
			}
			bool isActivated = base.GetComponent<Activatable>().IsActivated;
			if (this.lonelyMinion != null)
			{
				this.lonelyMinion.sm.Active.Set(isActivated && base.GetComponent<Operational>().IsOperational, this.lonelyMinion, false);
			}
			if (isActivated && base.sm.QuestProgress.Get(this) < 1f)
			{
				base.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.AllPower;
			}
		}

		// Token: 0x06007A02 RID: 31234 RVA: 0x002C5470 File Offset: 0x002C3670
		protected override void OnObjectSelect(object clicked)
		{
			if (!(bool)clicked)
			{
				return;
			}
			if (this.knockNotification != null)
			{
				this.knocker.gameObject.Unsubscribe(-1503271301, new Action<object>(this.OnObjectSelect));
				this.knockNotification.Clear();
				this.knockNotification = null;
				this.PlayIntroSequence(null);
				return;
			}
			if (!StoryManager.Instance.HasDisplayedPopup(Db.Get().Stories.LonelyMinion, EventInfoDataHelper.PopupType.BEGIN))
			{
				int count = Components.LiveMinionIdentities.Count;
				int num = UnityEngine.Random.Range(0, count);
				base.def.EventIntroInfo.Minions = new GameObject[]
				{
					this.lonelyMinion.gameObject,
					(count == 0) ? null : Components.LiveMinionIdentities[num].gameObject
				};
			}
			base.OnObjectSelect(clicked);
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x002C5540 File Offset: 0x002C3740
		private void OnWorkStateChanged(Workable w, Workable.WorkableEvent state)
		{
			Activatable activatable = w as Activatable;
			if (activatable != null)
			{
				if (state == Workable.WorkableEvent.WorkStarted)
				{
					this.knocker = w.worker.GetComponent<KBatchedAnimController>();
					this.knocker.gameObject.Subscribe(-1503271301, new Action<object>(this.OnObjectSelect));
					this.knockNotification = new Notification(CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.TEXT, NotificationType.Event, null, null, false, 0f, new Notification.ClickCallback(this.PlayIntroSequence), null, null, true, true, false);
					base.gameObject.AddOrGet<Notifier>().Add(this.knockNotification, "");
					base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.AttentionRequired, base.smi);
				}
				if (state == Workable.WorkableEvent.WorkStopped)
				{
					if (this.currentWorkState == Workable.WorkableEvent.WorkStarted)
					{
						if (this.knockNotification != null)
						{
							base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.AttentionRequired, false);
							this.knockNotification.Clear();
							this.knockNotification = null;
						}
						FocusTargetSequence.Cancel(base.master);
						this.knocker.gameObject.Unsubscribe(-1503271301, new Action<object>(this.OnObjectSelect));
						this.knocker = null;
					}
					if (this.currentWorkState == Workable.WorkableEvent.WorkCompleted)
					{
						Activatable activatable2 = activatable;
						activatable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(activatable2.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
						Activatable activatable3 = activatable;
						activatable3.onActivate = (System.Action)Delegate.Remove(activatable3.onActivate, new System.Action(this.StartStoryTrait));
					}
				}
				this.currentWorkState = state;
				return;
			}
			if (state == Workable.WorkableEvent.WorkStopped)
			{
				this.AnimController.Play(LonelyMinionHouseConfig.STORAGE_WORK_PST, KAnim.PlayMode.Once, 1f, 0f);
				this.AnimController.Queue(LonelyMinionHouseConfig.STORAGE, KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			bool flag = this.AnimController.currentAnim == LonelyMinionHouseConfig.STORAGE_WORKING[0] || this.AnimController.currentAnim == LonelyMinionHouseConfig.STORAGE_WORKING[1];
			if (state == Workable.WorkableEvent.WorkStarted && !flag)
			{
				this.AnimController.Play(LonelyMinionHouseConfig.STORAGE_WORKING, KAnim.PlayMode.Loop);
			}
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x002C5764 File Offset: 0x002C3964
		private void ReleaseKnocker(object _)
		{
			Navigator component = this.knocker.GetComponent<Navigator>();
			NavGrid.NavTypeData navTypeData = component.NavGrid.GetNavTypeData(component.CurrentNavType);
			this.knocker.RemoveAnimOverrides(base.GetComponent<Activatable>().overrideAnims[0]);
			this.knocker.Play(navTypeData.idleAnim, KAnim.PlayMode.Once, 1f, 0f);
			this.blinds.meterController.Play(this.blinds.meterController.initialAnim, this.blinds.meterController.initialMode, 1f, 0f);
			this.lonelyMinion.AnimController.Play(this.lonelyMinion.AnimController.defaultAnim, this.lonelyMinion.AnimController.initialMode, 1f, 0f);
			this.knocker.gameObject.Unsubscribe(-1061186183, new Action<object>(this.ReleaseKnocker));
			this.knocker.GetComponent<Brain>().Reset("knock sequence");
			this.knocker = null;
		}

		// Token: 0x06007A05 RID: 31237 RVA: 0x002C5880 File Offset: 0x002C3A80
		private void PlayIntroSequence(object _ = null)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.AttentionRequired, false);
			Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(Grid.PosToCell(base.gameObject), base.def.CompletionData.CameraTargetOffset), Grid.SceneLayer.Ore);
			FocusTargetSequence.Start(base.master, new FocusTargetSequence.Data
			{
				WorldId = base.master.GetMyWorldId(),
				OrthographicSize = 2f,
				TargetSize = 6f,
				Target = vector,
				PopupData = null,
				CompleteCB = new System.Action(this.OnIntroSequenceComplete),
				CanCompleteCB = new Func<bool>(this.IsIntroSequenceComplete)
			});
			base.GetComponent<KnockKnock>().AnswerDoor();
			this.knockNotification = null;
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x002C5958 File Offset: 0x002C3B58
		private void OnIntroSequenceComplete()
		{
			this.OnBuildingActivated(null);
			bool flag;
			bool flag2;
			QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest).TrackProgress(new Quest.ItemData
			{
				CriteriaId = LonelyMinionConfig.GreetingCriteraId
			}, out flag, out flag2);
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x002C59A8 File Offset: 0x002C3BA8
		private bool IsIntroSequenceComplete()
		{
			if (this.currentWorkState == Workable.WorkableEvent.WorkStarted)
			{
				return false;
			}
			if (this.currentWorkState == Workable.WorkableEvent.WorkStopped && this.knocker != null && this.knocker.currentAnim != LonelyMinionHouseConfig.ANSWER)
			{
				this.knocker.GetComponent<Brain>().Stop("knock sequence");
				this.knocker.gameObject.Subscribe(-1061186183, new Action<object>(this.ReleaseKnocker));
				this.knocker.AddAnimOverrides(base.GetComponent<Activatable>().overrideAnims[0], 0f);
				this.knocker.Play(LonelyMinionHouseConfig.ANSWER, KAnim.PlayMode.Once, 1f, 0f);
				this.lonelyMinion.AnimController.Play(LonelyMinionHouseConfig.ANSWER, KAnim.PlayMode.Once, 1f, 0f);
				this.blinds.meterController.Play(LonelyMinionHouseConfig.ANSWER, KAnim.PlayMode.Once, 1f, 0f);
			}
			return this.currentWorkState == Workable.WorkableEvent.WorkStopped && this.knocker == null;
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x002C5ABC File Offset: 0x002C3CBC
		public Vector3 GetParcelPosition()
		{
			int num = -1;
			KAnimFileData data = Assets.GetAnim("anim_interacts_lonely_dupe_kanim").GetData();
			for (int i = 0; i < data.animCount; i++)
			{
				if (data.GetAnim(i).hash == LonelyMinionConfig.CHECK_MAIL)
				{
					num = data.GetAnim(i).firstFrameIdx;
					break;
				}
			}
			List<KAnim.Anim.FrameElement> frameElements = this.lonelyMinion.AnimController.GetBatch().group.data.frameElements;
			KAnim.Anim.Frame frame = this.lonelyMinion.AnimController.GetBatch().group.data.GetFrame(num);
			bool flag = false;
			Matrix2x3 matrix2x = default(Matrix2x3);
			int num2 = 0;
			while (!flag && num2 < frame.numElements)
			{
				if (frameElements[frame.firstElementIdx + num2].symbol == LonelyMinionConfig.PARCEL_SNAPTO)
				{
					flag = true;
					matrix2x = frameElements[frame.firstElementIdx + num2].transform;
					break;
				}
				num2++;
			}
			Vector3 vector = Vector3.zero;
			if (flag)
			{
				Matrix4x4 matrix4x = this.lonelyMinion.AnimController.GetTransformMatrix();
				vector = (matrix4x * matrix2x).GetColumn(3);
			}
			return vector;
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06007A09 RID: 31241 RVA: 0x002C5C01 File Offset: 0x002C3E01
		public string Title
		{
			get
			{
				return CODEX.STORY_TRAITS.LONELYMINION.NAME;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06007A0A RID: 31242 RVA: 0x002C5C0D File Offset: 0x002C3E0D
		public string Description
		{
			get
			{
				return CODEX.STORY_TRAITS.LONELYMINION.DESCRIPTION_BUILDINGMENU;
			}
		}

		// Token: 0x06007A0B RID: 31243 RVA: 0x002C5C1C File Offset: 0x002C3E1C
		public ICheckboxListGroupControl.ListGroup[] GetData()
		{
			QuestInstance greetingQuest = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest);
			if (!greetingQuest.IsComplete)
			{
				return new ICheckboxListGroupControl.ListGroup[]
				{
					new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionGreetingQuest.Title, greetingQuest.GetCheckBoxData(null), (string title) => this.ResolveQuestTitle(title, greetingQuest))
				};
			}
			QuestInstance foodQuest = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
			QuestInstance decorQuest = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
			QuestInstance powerQuest = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
			return new ICheckboxListGroupControl.ListGroup[]
			{
				new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionGreetingQuest.Title, greetingQuest.GetCheckBoxData(null), (string title) => this.ResolveQuestTitle(title, greetingQuest)),
				new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionFoodQuest.Title, foodQuest.GetCheckBoxData(new Func<int, string, QuestInstance, string>(this.ResolveQuestToolTips)), (string title) => this.ResolveQuestTitle(title, foodQuest)),
				new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionDecorQuest.Title, decorQuest.GetCheckBoxData(new Func<int, string, QuestInstance, string>(this.ResolveQuestToolTips)), (string title) => this.ResolveQuestTitle(title, decorQuest)),
				new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionPowerQuest.Title, powerQuest.GetCheckBoxData(new Func<int, string, QuestInstance, string>(this.ResolveQuestToolTips)), (string title) => this.ResolveQuestTitle(title, powerQuest))
			};
		}

		// Token: 0x06007A0C RID: 31244 RVA: 0x002C5E0C File Offset: 0x002C400C
		private string ResolveQuestTitle(string title, QuestInstance quest)
		{
			string text = GameUtil.FloatToString(quest.CurrentProgress * 100f, "##0") + UI.UNITSUFFIXES.PERCENT;
			return title + " - " + text;
		}

		// Token: 0x06007A0D RID: 31245 RVA: 0x002C5E4C File Offset: 0x002C404C
		private string ResolveQuestToolTips(int criteriaId, string toolTip, QuestInstance quest)
		{
			if (criteriaId == LonelyMinionConfig.FoodCriteriaId.HashValue)
			{
				int num = (int)quest.GetTargetValue(LonelyMinionConfig.FoodCriteriaId, 0);
				int targetCount = quest.GetTargetCount(LonelyMinionConfig.FoodCriteriaId);
				string text = string.Empty;
				for (int i = 0; i < targetCount; i++)
				{
					Tag satisfyingItem = quest.GetSatisfyingItem(LonelyMinionConfig.FoodCriteriaId, i);
					if (!satisfyingItem.IsValid)
					{
						break;
					}
					text = text + "    • " + TagManager.GetProperName(satisfyingItem, false);
					if (targetCount - i != 1)
					{
						text += "\n";
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = string.Format("{0}{1}", "    • ", CODEX.QUESTS.CRITERIA.FOODQUALITY.NONE);
				}
				return string.Format(toolTip, GameUtil.GetFormattedFoodQuality(num), text);
			}
			if (criteriaId == LonelyMinionConfig.DecorCriteriaId.HashValue)
			{
				int num2 = (int)quest.GetTargetValue(LonelyMinionConfig.DecorCriteriaId, 0);
				float num3 = LonelyMinionHouse.CalculateAverageDecor(this.lonelyMinion.def.DecorInspectionArea);
				return string.Format(toolTip, num2, num3);
			}
			float num4 = quest.GetTargetValue(LonelyMinionConfig.PowerCriteriaId, 0) - quest.GetCurrentValue(LonelyMinionConfig.PowerCriteriaId, 0);
			return string.Format(toolTip, Mathf.CeilToInt(num4));
		}

		// Token: 0x06007A0E RID: 31246 RVA: 0x002C5F84 File Offset: 0x002C4184
		public bool SidescreenEnabled()
		{
			return StoryManager.Instance.HasDisplayedPopup(Db.Get().Stories.LonelyMinion, EventInfoDataHelper.PopupType.BEGIN) && !StoryManager.Instance.CheckState(StoryInstance.State.COMPLETE, Db.Get().Stories.LonelyMinion);
		}

		// Token: 0x06007A0F RID: 31247 RVA: 0x002C5FC1 File Offset: 0x002C41C1
		public int CheckboxSideScreenSortOrder()
		{
			return 20;
		}

		// Token: 0x04005DAA RID: 23978
		private KAnimLink lightsLink;

		// Token: 0x04005DAB RID: 23979
		private HashedString questOwnerId;

		// Token: 0x04005DAC RID: 23980
		private LonelyMinion.Instance lonelyMinion;

		// Token: 0x04005DAD RID: 23981
		private KBatchedAnimController[] animControllers;

		// Token: 0x04005DAE RID: 23982
		private Light2D light;

		// Token: 0x04005DAF RID: 23983
		private FilteredStorage storageFilter;

		// Token: 0x04005DB0 RID: 23984
		private MeterController meter;

		// Token: 0x04005DB1 RID: 23985
		private MeterController blinds;

		// Token: 0x04005DB2 RID: 23986
		private Workable.WorkableEvent currentWorkState = Workable.WorkableEvent.WorkStopped;

		// Token: 0x04005DB3 RID: 23987
		private Notification knockNotification;

		// Token: 0x04005DB4 RID: 23988
		private KBatchedAnimController knocker;
	}

	// Token: 0x02001263 RID: 4707
	public class ActiveStates : GameStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.State
	{
		// Token: 0x06007A10 RID: 31248 RVA: 0x002C5FC5 File Offset: 0x002C41C5
		public static void OnEnterStoryComplete(LonelyMinionHouse.Instance smi)
		{
			smi.CompleteEvent();
		}

		// Token: 0x04005DB5 RID: 23989
		public GameStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.State StoryComplete;
	}
}
