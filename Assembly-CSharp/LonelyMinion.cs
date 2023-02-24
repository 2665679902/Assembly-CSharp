using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000497 RID: 1175
public class LonelyMinion : GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>
{
	// Token: 0x06001A5A RID: 6746 RVA: 0x0008C458 File Offset: 0x0008A658
	private bool HahCheckedMail(LonelyMinion.Instance smi)
	{
		if (smi.AnimController.currentAnim == LonelyMinionConfig.CHECK_MAIL)
		{
			if (this.Mail.Get(smi) == smi.gameObject)
			{
				this.Mail.Set(null, smi, true);
				smi.AnimController.Play(LonelyMinionConfig.CHECK_MAIL_FAILURE, KAnim.PlayMode.Once, 1f, 0f);
				return false;
			}
			this.CheckForMail(smi);
			return false;
		}
		else
		{
			if (smi.AnimController.currentAnim == LonelyMinionConfig.FOOD_FAILURE || smi.AnimController.currentAnim == LonelyMinionConfig.FOOD_DUPLICATE)
			{
				smi.Drop();
				return false;
			}
			return smi.AnimController.currentAnim == LonelyMinionConfig.CHECK_MAIL_FAILURE || smi.AnimController.currentAnim == LonelyMinionConfig.CHECK_MAIL_SUCCESS || smi.AnimController.currentAnim == LonelyMinionConfig.CHECK_MAIL_DUPLICATE;
		}
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x0008C548 File Offset: 0x0008A748
	private void CheckForMail(LonelyMinion.Instance smi)
	{
		Tag prefabTag = this.Mail.Get(smi).GetComponent<KPrefabID>().PrefabTag;
		QuestInstance instance = QuestManager.GetInstance(smi.def.QuestOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
		global::Debug.Assert(instance != null);
		Quest.ItemData itemData = new Quest.ItemData
		{
			CriteriaId = LonelyMinionConfig.FoodCriteriaId,
			SatisfyingItem = prefabTag,
			QualifyingTag = GameTags.Edible,
			CurrentValue = (float)EdiblesManager.GetFoodInfo(prefabTag.Name).Quality
		};
		LonelyMinion.MailStates mailStates = smi.GetCurrentState() as LonelyMinion.MailStates;
		bool flag;
		bool flag2;
		instance.TrackProgress(itemData, out flag, out flag2);
		StateMachine.BaseState baseState = mailStates.Success;
		string text = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.TASTYFOOD.NAME;
		string text2 = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.TASTYFOOD.TOOLTIP;
		if (flag2)
		{
			baseState = mailStates.Duplicate;
			text = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.REPEATEDFOOD.NAME;
			text2 = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.REPEATEDFOOD.TOOLTIP;
		}
		else if (!flag)
		{
			baseState = mailStates.Failure;
			text = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.CRAPPYFOOD.NAME;
			text2 = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.CRAPPYFOOD.TOOLTIP;
		}
		Pickupable component = this.Mail.Get(smi).GetComponent<Pickupable>();
		smi.Pickup(component, baseState != mailStates.Success);
		smi.ScheduleGoTo(0.016f, baseState);
		Notification notification = new Notification(text, NotificationType.Event, (List<Notification> notificationList, object data) => data as string, text2, false, 0f, null, null, smi.transform.parent, true, true, true);
		smi.transform.parent.gameObject.AddOrGet<Notifier>().Add(notification, "");
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x0008C6F0 File Offset: 0x0008A8F0
	private void EvaluateCurrentDecor(LonelyMinion.Instance smi, float dt)
	{
		QuestInstance instance = QuestManager.GetInstance(smi.def.QuestOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
		if (smi.GetCurrentState() == this.Inactive || instance.IsComplete)
		{
			return;
		}
		float num = LonelyMinionHouse.CalculateAverageDecor(smi.def.DecorInspectionArea);
		bool flag = num >= 0f || (num > smi.StartingAverageDecor && 1f - num / smi.StartingAverageDecor > 0.01f);
		if (!instance.IsStarted && !flag)
		{
			return;
		}
		bool flag2;
		bool flag3;
		instance.TrackProgress(new Quest.ItemData
		{
			CriteriaId = LonelyMinionConfig.DecorCriteriaId,
			CurrentValue = num
		}, out flag2, out flag3);
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x0008C7A8 File Offset: 0x0008A9A8
	private void DelayIdle(LonelyMinion.Instance smi, float dt)
	{
		if (smi.AnimController.currentAnim != smi.AnimController.defaultAnim)
		{
			return;
		}
		if (smi.IdleDelayTimer > 0f)
		{
			smi.IdleDelayTimer -= dt;
		}
		if (smi.IdleDelayTimer <= 0f)
		{
			this.PlayIdle(smi, smi.ChooseIdle());
			smi.IdleDelayTimer = UnityEngine.Random.Range(20f, 40f);
		}
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x0008C824 File Offset: 0x0008AA24
	private void PlayIdle(LonelyMinion.Instance smi, HashedString idleAnim)
	{
		if (!idleAnim.IsValid)
		{
			return;
		}
		if (idleAnim == LonelyMinionConfig.CHECK_MAIL)
		{
			this.Mail.Set(smi.gameObject, smi, false);
			return;
		}
		QuestInstance instance = QuestManager.GetInstance(smi.def.QuestOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
		int num = instance.GetCurrentCount(LonelyMinionConfig.FoodCriteriaId) - 1;
		if (idleAnim == LonelyMinionConfig.FOOD_IDLE && num >= 0)
		{
			KBatchedAnimController component = Assets.GetPrefab(instance.GetSatisfyingItem(LonelyMinionConfig.FoodCriteriaId, UnityEngine.Random.Range(0, num))).GetComponent<KBatchedAnimController>();
			smi.PackageSnapPoint.SwapAnims(component.AnimFiles);
			smi.PackageSnapPoint.Play("object", KAnim.PlayMode.Loop, 1f, 0f);
		}
		if (!(idleAnim == LonelyMinionConfig.FOOD_IDLE) && !(idleAnim == LonelyMinionConfig.DECOR_IDLE) && !(idleAnim == LonelyMinionConfig.POWER_IDLE))
		{
			LonelyMinionHouse.Instance smi2 = smi.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			smi.AnimController.GetSynchronizer().Remove(smi2.AnimController);
			if (idleAnim == LonelyMinionConfig.BLINDS_IDLE_0)
			{
				smi2.BlindsController.Play(LonelyMinionConfig.BLINDS_IDLE_0, KAnim.PlayMode.Once, 1f, 0f);
			}
		}
		smi.AnimController.Play(idleAnim, KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06001A5F RID: 6751 RVA: 0x0008C97C File Offset: 0x0008AB7C
	private void OnIdleAnimComplete(LonelyMinion.Instance smi)
	{
		if (smi.AnimController.currentAnim == smi.AnimController.defaultAnim)
		{
			return;
		}
		if (!(smi.AnimController.currentAnim == LonelyMinionConfig.FOOD_IDLE) && !(smi.AnimController.currentAnim == LonelyMinionConfig.DECOR_IDLE) && !(smi.AnimController.currentAnim == LonelyMinionConfig.POWER_IDLE))
		{
			LonelyMinionHouse.Instance smi2 = smi.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			smi.AnimController.GetSynchronizer().Add(smi2.AnimController);
			if (smi.AnimController.currentAnim == LonelyMinionConfig.BLINDS_IDLE_0)
			{
				smi2.BlindsController.Play(string.Format("{0}_{1}", "meter_blinds", 0), KAnim.PlayMode.Paused, 1f, 0f);
			}
		}
		smi.AnimController.Play(smi.AnimController.defaultAnim, smi.AnimController.initialMode, 1f, 0f);
		if (this.Active.Get(smi) && this.Mail.Get(smi) != null)
		{
			smi.ScheduleGoTo(0f, this.CheckMail);
		}
	}

	// Token: 0x06001A60 RID: 6752 RVA: 0x0008CAC8 File Offset: 0x0008ACC8
	private void OnBecomeInactive(LonelyMinion.Instance smi)
	{
		smi.AnimController.GetSynchronizer().Clear();
		smi.AnimController.Play(smi.AnimController.initialAnim, smi.AnimController.initialMode, 1f, 0f);
	}

	// Token: 0x06001A61 RID: 6753 RVA: 0x0008CB18 File Offset: 0x0008AD18
	private void OnBecomeActive(LonelyMinion.Instance smi)
	{
		LonelyMinionHouse.Instance smi2 = smi.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
		smi.AnimController.GetSynchronizer().Add(smi2.AnimController);
		if (smi.StartingAverageDecor == float.NegativeInfinity)
		{
			smi.StartingAverageDecor = LonelyMinionHouse.CalculateAverageDecor(smi.def.DecorInspectionArea);
		}
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x0008CB70 File Offset: 0x0008AD70
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.Inactive;
		this.root.ParamTransition<bool>(this.Active, this.Inactive, (LonelyMinion.Instance smi, bool p) => !this.Active.Get(smi)).ParamTransition<bool>(this.Active, this.Idle, (LonelyMinion.Instance smi, bool p) => this.Active.Get(smi)).Update(new Action<LonelyMinion.Instance, float>(this.EvaluateCurrentDecor), UpdateRate.SIM_1000ms, false);
		this.Inactive.Enter(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(this.OnBecomeInactive)).Exit(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(this.OnBecomeActive));
		this.Idle.ParamTransition<GameObject>(this.Mail, this.CheckMail, (LonelyMinion.Instance smi, GameObject p) => smi.AnimController.currentAnim == smi.AnimController.defaultAnim && this.Mail.Get(smi) != null).Update(new Action<LonelyMinion.Instance, float>(this.DelayIdle), UpdateRate.SIM_EVERY_TICK, false).EventHandler(GameHashes.AnimQueueComplete, new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(this.OnIdleAnimComplete))
			.Exit(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(this.OnIdleAnimComplete));
		this.CheckMail.Enter(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(LonelyMinion.MailStates.OnEnter)).ParamTransition<GameObject>(this.Mail, this.Idle, (LonelyMinion.Instance smi, GameObject p) => this.Mail.Get(smi) == null).EventTransition(GameHashes.AnimQueueComplete, this.Idle, new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.Transition.ConditionCallback(this.HahCheckedMail))
			.Exit(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(LonelyMinion.MailStates.OnExit));
		this.CheckMail.Success.Enter(delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.FOOD_SUCCESS);
		}).EventHandler(GameHashes.AnimQueueComplete, delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.CHECK_MAIL_SUCCESS);
		});
		this.CheckMail.Failure.Enter(delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.FOOD_FAILURE);
		}).EventHandler(GameHashes.AnimQueueComplete, delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.CHECK_MAIL_FAILURE);
		});
		this.CheckMail.Duplicate.Enter(delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.FOOD_DUPLICATE);
		}).EventHandler(GameHashes.AnimQueueComplete, delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.CHECK_MAIL_DUPLICATE);
		});
	}

	// Token: 0x04000EA8 RID: 3752
	public StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.TargetParameter Mail;

	// Token: 0x04000EA9 RID: 3753
	public StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.BoolParameter Active;

	// Token: 0x04000EAA RID: 3754
	public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Idle;

	// Token: 0x04000EAB RID: 3755
	public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Inactive;

	// Token: 0x04000EAC RID: 3756
	public LonelyMinion.MailStates CheckMail;

	// Token: 0x020010D6 RID: 4310
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040058CE RID: 22734
		public Personality Personality;

		// Token: 0x040058CF RID: 22735
		public HashedString QuestOwnerId;

		// Token: 0x040058D0 RID: 22736
		public Extents DecorInspectionArea;
	}

	// Token: 0x020010D7 RID: 4311
	public new class Instance : GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.GameInstance
	{
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06007499 RID: 29849 RVA: 0x002B3760 File Offset: 0x002B1960
		public KBatchedAnimController AnimController
		{
			get
			{
				return this.animControllers[0];
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600749A RID: 29850 RVA: 0x002B376A File Offset: 0x002B196A
		public KBatchedAnimController PackageSnapPoint
		{
			get
			{
				return this.animControllers[1];
			}
		}

		// Token: 0x0600749B RID: 29851 RVA: 0x002B3774 File Offset: 0x002B1974
		public Instance(StateMachineController master, LonelyMinion.Def def)
			: base(master, def)
		{
			this.animControllers = base.gameObject.GetComponentsInChildren<KBatchedAnimController>(true);
			this.storage = base.GetComponent<Storage>();
			global::Debug.Assert(def.Personality != null);
			base.GetComponent<Accessorizer>().ApplyMinionPersonality(def.Personality);
			StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(Db.Get().Stories.LonelyMinion.HashId);
			storyInstance.StoryStateChanged = (Action<StoryInstance.State>)Delegate.Combine(storyInstance.StoryStateChanged, new Action<StoryInstance.State>(this.OnStoryStateChanged));
		}

		// Token: 0x0600749C RID: 29852 RVA: 0x002B381C File Offset: 0x002B1A1C
		public override void StartSM()
		{
			LonelyMinionHouse.Instance smi = base.smi.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			base.smi.AnimController.GetSynchronizer().Add(smi.AnimController);
			QuestInstance instance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest);
			instance.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Combine(instance.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.ShowQuestCompleteNotification));
			base.smi.IdleDelayTimer = UnityEngine.Random.Range(20f, 40f);
			this.InitializeIdles();
			base.StartSM();
		}

		// Token: 0x0600749D RID: 29853 RVA: 0x002B38C0 File Offset: 0x002B1AC0
		public override void StopSM(string reason)
		{
			QuestInstance instance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest);
			instance.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Remove(instance.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.ShowQuestCompleteNotification));
			this.StoryCleanUp();
			base.StopSM(reason);
			this.ResetHandle.ClearScheduler();
			this.ResetHandle.FreeResources();
		}

		// Token: 0x0600749E RID: 29854 RVA: 0x002B3930 File Offset: 0x002B1B30
		public HashedString ChooseIdle()
		{
			if (this.availableIdles.Count > 1)
			{
				this.availableIdles.Shuffle<HashedString>();
			}
			return this.availableIdles[0];
		}

		// Token: 0x0600749F RID: 29855 RVA: 0x002B3958 File Offset: 0x002B1B58
		public void Pickup(Pickupable pickupable, bool store)
		{
			base.sm.Mail.Set(null, this, true);
			pickupable.storage.GetComponent<SingleEntityReceptacle>().OrderRemoveOccupant();
			this.PackageSnapPoint.Play("object", KAnim.PlayMode.Loop, 1f, 0f);
			if (store)
			{
				this.storage.Store(pickupable.gameObject, true, true, false, false);
				return;
			}
			UnityEngine.Object.Destroy(pickupable.gameObject);
		}

		// Token: 0x060074A0 RID: 29856 RVA: 0x002B39D0 File Offset: 0x002B1BD0
		public void Drop()
		{
			this.storage.DropAll(this.PackageSnapPoint.transform.position, false, false, default(Vector3), true, null);
		}

		// Token: 0x060074A1 RID: 29857 RVA: 0x002B3A05 File Offset: 0x002B1C05
		private void OnStoryStateChanged(StoryInstance.State state)
		{
			if (state != StoryInstance.State.COMPLETE)
			{
				return;
			}
			this.StoryCleanUp();
		}

		// Token: 0x060074A2 RID: 29858 RVA: 0x002B3A14 File Offset: 0x002B1C14
		private void StoryCleanUp()
		{
			this.AnimController.GetSynchronizer().Clear();
			StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(Db.Get().Stories.LonelyMinion.HashId);
			storyInstance.StoryStateChanged = (Action<StoryInstance.State>)Delegate.Remove(storyInstance.StoryStateChanged, new Action<StoryInstance.State>(this.OnStoryStateChanged));
		}

		// Token: 0x060074A3 RID: 29859 RVA: 0x002B3A70 File Offset: 0x002B1C70
		private void InitializeIdles()
		{
			QuestInstance questInstance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
			if (questInstance.IsStarted)
			{
				this.availableIdles.Add(LonelyMinionConfig.FOOD_IDLE);
				if (!questInstance.IsComplete)
				{
					this.availableIdles.Add(LonelyMinionConfig.CHECK_MAIL);
				}
			}
			questInstance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
			if (questInstance.IsStarted)
			{
				this.availableIdles.Add(LonelyMinionConfig.DECOR_IDLE);
			}
			questInstance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
			if (questInstance.IsStarted)
			{
				this.availableIdles.Add(LonelyMinionConfig.POWER_IDLE);
			}
			LonelyMinionHouse.Instance smi = base.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			LonelyMinionHouse lonelyMinionHouse = smi.GetStateMachine() as LonelyMinionHouse;
			float num = 3f * lonelyMinionHouse.QuestProgress.Get(smi);
			int num2 = (Mathf.Approximately((float)Mathf.CeilToInt(num), num) ? Mathf.CeilToInt(num) : Mathf.FloorToInt(num));
			if (num2 == 0)
			{
				this.availableIdles.Add(LonelyMinionConfig.BLINDS_IDLE_0);
				return;
			}
			int num3 = 1;
			while (num3 <= num2 && num3 != 3)
			{
				this.availableIdles.Add(string.Format("{0}_{1}", "idle_blinds", num3));
				num3++;
			}
		}

		// Token: 0x060074A4 RID: 29860 RVA: 0x002B3BE0 File Offset: 0x002B1DE0
		public void UnlockQuestIdle(QuestInstance quest, Quest.State prevState, float delta)
		{
			if (prevState == Quest.State.NotStarted && quest.IsStarted)
			{
				if (quest.Id == Db.Get().Quests.LonelyMinionFoodQuest.IdHash)
				{
					this.availableIdles.Add(LonelyMinionConfig.FOOD_IDLE);
				}
				else if (quest.Id == Db.Get().Quests.LonelyMinionDecorQuest.IdHash)
				{
					this.availableIdles.Add(LonelyMinionConfig.DECOR_IDLE);
				}
				else
				{
					this.availableIdles.Add(LonelyMinionConfig.POWER_IDLE);
				}
			}
			if (!quest.IsComplete)
			{
				return;
			}
			if (quest.Id == Db.Get().Quests.LonelyMinionFoodQuest.IdHash)
			{
				this.availableIdles.Remove(LonelyMinionConfig.CHECK_MAIL);
			}
			LonelyMinionHouse.Instance smi = base.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			LonelyMinionHouse lonelyMinionHouse = smi.GetStateMachine() as LonelyMinionHouse;
			float num = 3f * lonelyMinionHouse.QuestProgress.Get(smi);
			int num2 = (Mathf.Approximately((float)Mathf.CeilToInt(num), num) ? Mathf.CeilToInt(num) : Mathf.FloorToInt(num));
			if (num2 > 0 && num2 < 3)
			{
				this.availableIdles.Add(string.Format("{0}_{1}", "idle_blinds", num2));
			}
			this.availableIdles.Remove(LonelyMinionConfig.BLINDS_IDLE_0);
		}

		// Token: 0x060074A5 RID: 29861 RVA: 0x002B3D38 File Offset: 0x002B1F38
		public void ShowQuestCompleteNotification(QuestInstance quest, Quest.State prevState, float delta = 0f)
		{
			if (!quest.IsComplete)
			{
				return;
			}
			string text = string.Empty;
			if (quest.Id != Db.Get().Quests.LonelyMinionGreetingQuest.IdHash)
			{
				text = "story_trait_lonelyminion_" + quest.Name.ToLower();
				Game.Instance.unlocks.Unlock(text, false);
			}
			Notification notification = new Notification(CODEX.STORY_TRAITS.LONELYMINION.QUESTCOMPLETE_POPUP.NAME, NotificationType.Event, null, null, false, 0f, new Notification.ClickCallback(this.ShowQuestCompletePopup), new global::Tuple<string, string>(text, quest.CompletionText), null, true, true, true);
			base.transform.parent.gameObject.AddOrGet<Notifier>().Add(notification, "");
		}

		// Token: 0x060074A6 RID: 29862 RVA: 0x002B3DF4 File Offset: 0x002B1FF4
		private void ShowQuestCompletePopup(object data)
		{
			global::Tuple<string, string> tuple = data as global::Tuple<string, string>;
			InfoDialogScreen infoDialogScreen = LoreBearer.ShowPopupDialog().SetHeader(CODEX.STORY_TRAITS.LONELYMINION.QUESTCOMPLETE_POPUP.NAME).AddPlainText(tuple.second)
				.AddDefaultOK(false);
			if (!string.IsNullOrEmpty(tuple.first))
			{
				infoDialogScreen.AddOption(CODEX.STORY_TRAITS.LONELYMINION.QUESTCOMPLETE_POPUP.VIEW_IN_CODEX, LoreBearerUtil.OpenCodexByLockKeyID(tuple.first, true), false);
			}
		}

		// Token: 0x040058D1 RID: 22737
		public SchedulerHandle ResetHandle;

		// Token: 0x040058D2 RID: 22738
		public float StartingAverageDecor = float.NegativeInfinity;

		// Token: 0x040058D3 RID: 22739
		public float IdleDelayTimer;

		// Token: 0x040058D4 RID: 22740
		private KBatchedAnimController[] animControllers;

		// Token: 0x040058D5 RID: 22741
		private Storage storage;

		// Token: 0x040058D6 RID: 22742
		private const int maxIdles = 8;

		// Token: 0x040058D7 RID: 22743
		private List<HashedString> availableIdles = new List<HashedString>(8);
	}

	// Token: 0x020010D8 RID: 4312
	public class MailStates : GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State
	{
		// Token: 0x060074A7 RID: 29863 RVA: 0x002B3E5C File Offset: 0x002B205C
		public static void OnEnter(LonelyMinion.Instance smi)
		{
			KBatchedAnimController component = smi.sm.Mail.Get(smi).GetComponent<KBatchedAnimController>();
			smi.PackageSnapPoint.gameObject.SetActive(component.gameObject != smi.gameObject);
			if (smi.PackageSnapPoint.gameObject.activeSelf)
			{
				smi.PackageSnapPoint.SwapAnims(component.AnimFiles);
			}
			smi.AnimController.Play(LonelyMinionConfig.CHECK_MAIL, KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x060074A8 RID: 29864 RVA: 0x002B3EDF File Offset: 0x002B20DF
		public static void OnExit(LonelyMinion.Instance smi)
		{
			smi.ResetHandle = smi.ScheduleNextFrame(new Action<object>(LonelyMinion.MailStates.ResetState), smi);
		}

		// Token: 0x060074A9 RID: 29865 RVA: 0x002B3EFC File Offset: 0x002B20FC
		private static void ResetState(object data)
		{
			LonelyMinion.Instance instance = data as LonelyMinion.Instance;
			instance.AnimController.Play(instance.AnimController.initialAnim, instance.AnimController.initialMode, 1f, 0f);
			instance.Drop();
		}

		// Token: 0x060074AA RID: 29866 RVA: 0x002B3F46 File Offset: 0x002B2146
		public static void PlayAnims(LonelyMinion.Instance smi, HashedString anim)
		{
			if (anim.IsValid)
			{
				smi.AnimController.Queue(anim, KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			smi.GoTo(smi.sm.Idle);
		}

		// Token: 0x040058D8 RID: 22744
		public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Success;

		// Token: 0x040058D9 RID: 22745
		public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Failure;

		// Token: 0x040058DA RID: 22746
		public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Duplicate;
	}
}
