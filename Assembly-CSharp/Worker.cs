using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000506 RID: 1286
[AddComponentMenu("KMonoBehaviour/scripts/Worker")]
public class Worker : KMonoBehaviour
{
	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06001E9F RID: 7839 RVA: 0x000A3D48 File Offset: 0x000A1F48
	// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x000A3D50 File Offset: 0x000A1F50
	public Worker.State state { get; private set; }

	// Token: 0x17000155 RID: 341
	// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000A3D59 File Offset: 0x000A1F59
	// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x000A3D61 File Offset: 0x000A1F61
	public Worker.StartWorkInfo startWorkInfo { get; private set; }

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x000A3D6A File Offset: 0x000A1F6A
	public Workable workable
	{
		get
		{
			if (this.startWorkInfo != null)
			{
				return this.startWorkInfo.workable;
			}
			return null;
		}
	}

	// Token: 0x06001EA4 RID: 7844 RVA: 0x000A3D81 File Offset: 0x000A1F81
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.state = Worker.State.Idle;
		base.Subscribe<Worker>(1485595942, Worker.OnChoreInterruptDelegate);
	}

	// Token: 0x06001EA5 RID: 7845 RVA: 0x000A3DA1 File Offset: 0x000A1FA1
	private string GetWorkableDebugString()
	{
		if (this.workable == null)
		{
			return "Null";
		}
		return this.workable.name;
	}

	// Token: 0x06001EA6 RID: 7846 RVA: 0x000A3DC4 File Offset: 0x000A1FC4
	public void CompleteWork()
	{
		this.successFullyCompleted = false;
		this.state = Worker.State.Idle;
		if (this.workable != null)
		{
			if (this.workable.triggerWorkReactions && this.workable.GetWorkTime() > 30f)
			{
				string conversationTopic = this.workable.GetConversationTopic();
				if (!conversationTopic.IsNullOrWhiteSpace())
				{
					this.CreateCompletionReactable(conversationTopic);
				}
			}
			this.DetachAnimOverrides();
			this.workable.CompleteWork(this);
			if (this.workable.worker != null && !(this.workable is Constructable) && !(this.workable is Deconstructable) && !(this.workable is Repairable) && !(this.workable is Disinfectable))
			{
				BonusEvent.GameplayEventData gameplayEventData = new BonusEvent.GameplayEventData();
				gameplayEventData.workable = this.workable;
				gameplayEventData.worker = this.workable.worker;
				gameplayEventData.building = this.workable.GetComponent<BuildingComplete>();
				gameplayEventData.eventTrigger = GameHashes.UseBuilding;
				GameplayEventManager.Instance.Trigger(1175726587, gameplayEventData);
			}
		}
		this.InternalStopWork(this.workable, false);
	}

	// Token: 0x06001EA7 RID: 7847 RVA: 0x000A3EE4 File Offset: 0x000A20E4
	public Worker.WorkResult Work(float dt)
	{
		if (this.state == Worker.State.PendingCompletion)
		{
			bool flag = Time.time - this.workPendingCompletionTime > 10f;
			if (!base.GetComponent<KAnimControllerBase>().IsStopped() && !flag)
			{
				return Worker.WorkResult.InProgress;
			}
			Navigator component = base.GetComponent<Navigator>();
			if (component != null)
			{
				NavGrid.NavTypeData navTypeData = component.NavGrid.GetNavTypeData(component.CurrentNavType);
				if (navTypeData.idleAnim.IsValid)
				{
					base.GetComponent<KAnimControllerBase>().Play(navTypeData.idleAnim, KAnim.PlayMode.Once, 1f, 0f);
				}
			}
			if (this.successFullyCompleted)
			{
				this.CompleteWork();
				return Worker.WorkResult.Success;
			}
			this.StopWork();
			return Worker.WorkResult.Failed;
		}
		else
		{
			if (this.state != Worker.State.Completing)
			{
				if (this.workable != null)
				{
					if (this.facing)
					{
						if (this.workable.ShouldFaceTargetWhenWorking())
						{
							this.facing.Face(this.workable.GetFacingTarget());
						}
						else
						{
							Rotatable component2 = this.workable.GetComponent<Rotatable>();
							bool flag2 = component2 != null && component2.GetOrientation() == Orientation.FlipH;
							Vector3 vector = this.facing.transform.GetPosition();
							vector += (flag2 ? Vector3.left : Vector3.right);
							this.facing.Face(vector);
						}
					}
					if (dt > 0f && Game.Instance.FastWorkersModeActive)
					{
						dt = Mathf.Min(this.workable.WorkTimeRemaining + 0.01f, 5f);
					}
					Klei.AI.Attribute workAttribute = this.workable.GetWorkAttribute();
					AttributeLevels component3 = base.GetComponent<AttributeLevels>();
					if (workAttribute != null && workAttribute.IsTrainable && component3 != null)
					{
						float attributeExperienceMultiplier = this.workable.GetAttributeExperienceMultiplier();
						component3.AddExperience(workAttribute.Id, dt, attributeExperienceMultiplier);
					}
					string skillExperienceSkillGroup = this.workable.GetSkillExperienceSkillGroup();
					if (this.resume != null && skillExperienceSkillGroup != null)
					{
						float skillExperienceMultiplier = this.workable.GetSkillExperienceMultiplier();
						this.resume.AddExperienceWithAptitude(skillExperienceSkillGroup, dt, skillExperienceMultiplier);
					}
					float efficiencyMultiplier = this.workable.GetEfficiencyMultiplier(this);
					float num = dt * efficiencyMultiplier * 1f;
					if (this.workable.WorkTick(this, num) && this.state == Worker.State.Working)
					{
						this.successFullyCompleted = true;
						this.StartPlayingPostAnim();
						this.workable.OnPendingCompleteWork(this);
					}
				}
				return Worker.WorkResult.InProgress;
			}
			if (this.successFullyCompleted)
			{
				this.CompleteWork();
				return Worker.WorkResult.Success;
			}
			this.StopWork();
			return Worker.WorkResult.Failed;
		}
	}

	// Token: 0x06001EA8 RID: 7848 RVA: 0x000A414C File Offset: 0x000A234C
	private void StartPlayingPostAnim()
	{
		if (this.workable != null && !this.workable.alwaysShowProgressBar)
		{
			this.workable.ShowProgressBar(false);
		}
		base.GetComponent<KPrefabID>().AddTag(GameTags.PreventChoreInterruption, false);
		this.state = Worker.State.PendingCompletion;
		this.workPendingCompletionTime = Time.time;
		KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
		HashedString[] workPstAnims = this.workable.GetWorkPstAnims(this, this.successFullyCompleted);
		if (this.smi == null)
		{
			if (workPstAnims != null && workPstAnims.Length != 0)
			{
				if (this.workable != null && this.workable.synchronizeAnims)
				{
					KAnimControllerBase component2 = this.workable.GetComponent<KAnimControllerBase>();
					if (component2 != null)
					{
						component2.Play(workPstAnims, KAnim.PlayMode.Once);
					}
				}
				else
				{
					component.Play(workPstAnims, KAnim.PlayMode.Once);
				}
			}
			else
			{
				this.state = Worker.State.Completing;
			}
		}
		base.Trigger(-1142962013, this);
	}

	// Token: 0x06001EA9 RID: 7849 RVA: 0x000A4224 File Offset: 0x000A2424
	private void InternalStopWork(Workable target_workable, bool is_aborted)
	{
		this.state = Worker.State.Idle;
		base.gameObject.RemoveTag(GameTags.PerformingWorkRequest);
		base.GetComponent<KAnimControllerBase>().Offset -= this.workAnimOffset;
		this.workAnimOffset = Vector3.zero;
		base.GetComponent<KPrefabID>().RemoveTag(GameTags.PreventChoreInterruption);
		this.DetachAnimOverrides();
		this.ClearPasserbyReactable();
		AnimEventHandler component = base.GetComponent<AnimEventHandler>();
		if (component)
		{
			component.ClearContext();
		}
		if (this.previousStatusItem.item != null)
		{
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, this.previousStatusItem.item, this.previousStatusItem.data);
		}
		if (target_workable != null)
		{
			target_workable.Unsubscribe(this.onWorkChoreDisabledHandle);
			target_workable.StopWork(this, is_aborted);
		}
		if (this.smi != null)
		{
			this.smi.StopSM("stopping work");
			this.smi = null;
		}
		Vector3 position = base.transform.GetPosition();
		position.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
		base.transform.SetPosition(position);
		this.startWorkInfo = null;
	}

	// Token: 0x06001EAA RID: 7850 RVA: 0x000A4348 File Offset: 0x000A2548
	private void OnChoreInterrupt(object data)
	{
		if (this.state == Worker.State.Working)
		{
			this.successFullyCompleted = false;
			this.StartPlayingPostAnim();
		}
	}

	// Token: 0x06001EAB RID: 7851 RVA: 0x000A4360 File Offset: 0x000A2560
	private void OnWorkChoreDisabled(object data)
	{
		string text = data as string;
		ChoreConsumer component = base.GetComponent<ChoreConsumer>();
		if (component != null && component.choreDriver != null)
		{
			component.choreDriver.GetCurrentChore().Fail((text != null) ? text : "WorkChoreDisabled");
		}
	}

	// Token: 0x06001EAC RID: 7852 RVA: 0x000A43B0 File Offset: 0x000A25B0
	public void StopWork()
	{
		if (this.state != Worker.State.PendingCompletion && this.state != Worker.State.Completing)
		{
			if (this.state == Worker.State.Working)
			{
				if (this.workable != null && this.workable.synchronizeAnims)
				{
					KBatchedAnimController component = this.workable.GetComponent<KBatchedAnimController>();
					if (component != null)
					{
						HashedString[] workPstAnims = this.workable.GetWorkPstAnims(this, false);
						if (workPstAnims != null && workPstAnims.Length != 0)
						{
							component.Play(workPstAnims, KAnim.PlayMode.Once);
							component.SetPositionPercent(1f);
						}
					}
				}
				this.InternalStopWork(this.workable, true);
			}
			return;
		}
		this.state = Worker.State.Idle;
		if (this.successFullyCompleted)
		{
			this.CompleteWork();
			return;
		}
		this.InternalStopWork(this.workable, true);
	}

	// Token: 0x06001EAD RID: 7853 RVA: 0x000A4464 File Offset: 0x000A2664
	public void StartWork(Worker.StartWorkInfo start_work_info)
	{
		this.startWorkInfo = start_work_info;
		Game.Instance.StartedWork();
		if (this.state != Worker.State.Idle)
		{
			string text = "";
			if (this.workable != null)
			{
				text = this.workable.name;
			}
			global::Debug.LogError(string.Concat(new string[]
			{
				base.name,
				".",
				text,
				".state should be idle but instead it's:",
				this.state.ToString()
			}));
		}
		string name = this.workable.GetType().Name;
		try
		{
			base.gameObject.AddTag(GameTags.PerformingWorkRequest);
			this.state = Worker.State.Working;
			base.gameObject.Trigger(1568504979, this);
			if (this.workable != null)
			{
				this.animInfo = this.workable.GetAnim(this);
				if (this.animInfo.smi != null)
				{
					this.smi = this.animInfo.smi;
					this.smi.StartSM();
				}
				Vector3 position = base.transform.GetPosition();
				position.z = Grid.GetLayerZ(this.workable.workLayer);
				base.transform.SetPosition(position);
				KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
				if (this.animInfo.smi == null)
				{
					this.AttachOverrideAnims(component);
				}
				HashedString[] workAnims = this.workable.GetWorkAnims(this);
				KAnim.PlayMode workAnimPlayMode = this.workable.GetWorkAnimPlayMode();
				Vector3 workOffset = this.workable.GetWorkOffset();
				this.workAnimOffset = workOffset;
				component.Offset += workOffset;
				if (this.usesMultiTool && this.animInfo.smi == null && workAnims != null && workAnims.Length != 0 && this.resume != null)
				{
					if (this.workable.synchronizeAnims)
					{
						KAnimControllerBase component2 = this.workable.GetComponent<KAnimControllerBase>();
						if (component2 != null)
						{
							this.kanimSynchronizer = component2.GetSynchronizer();
							if (this.kanimSynchronizer != null)
							{
								this.kanimSynchronizer.Add(component);
							}
						}
						component2.Play(workAnims, workAnimPlayMode);
					}
					else
					{
						component.Play(workAnims, workAnimPlayMode);
					}
				}
			}
			this.workable.StartWork(this);
			if (this.workable == null)
			{
				global::Debug.LogWarning("Stopped work as soon as I started. This is usually a sign that a chore is open when it shouldn't be or that it's preconditions are wrong.");
			}
			else
			{
				this.onWorkChoreDisabledHandle = this.workable.Subscribe(2108245096, new Action<object>(this.OnWorkChoreDisabled));
				if (this.workable.triggerWorkReactions && this.workable.WorkTimeRemaining > 10f)
				{
					this.CreatePasserbyReactable();
				}
				KSelectable component3 = base.GetComponent<KSelectable>();
				this.previousStatusItem = component3.GetStatusItem(Db.Get().StatusItemCategories.Main);
				component3.SetStatusItem(Db.Get().StatusItemCategories.Main, this.workable.GetWorkerStatusItem(), this.workable);
			}
		}
		catch (Exception ex)
		{
			string text2 = "Exception in: Worker.StartWork(" + name + ")";
			DebugUtil.LogErrorArgs(this, new object[] { text2 + "\n" + ex.ToString() });
			throw;
		}
	}

	// Token: 0x06001EAE RID: 7854 RVA: 0x000A47A4 File Offset: 0x000A29A4
	private void Update()
	{
		if (this.state == Worker.State.Working)
		{
			this.ForceSyncAnims();
		}
	}

	// Token: 0x06001EAF RID: 7855 RVA: 0x000A47B5 File Offset: 0x000A29B5
	private void ForceSyncAnims()
	{
		if (Time.deltaTime > 0f && this.kanimSynchronizer != null)
		{
			this.kanimSynchronizer.SyncTime();
		}
	}

	// Token: 0x06001EB0 RID: 7856 RVA: 0x000A47D6 File Offset: 0x000A29D6
	public bool InstantlyFinish()
	{
		return this.workable != null && this.workable.InstantlyFinish(this);
	}

	// Token: 0x06001EB1 RID: 7857 RVA: 0x000A47F4 File Offset: 0x000A29F4
	private void AttachOverrideAnims(KAnimControllerBase worker_controller)
	{
		if (this.animInfo.overrideAnims != null && this.animInfo.overrideAnims.Length != 0)
		{
			for (int i = 0; i < this.animInfo.overrideAnims.Length; i++)
			{
				worker_controller.AddAnimOverrides(this.animInfo.overrideAnims[i], 0f);
			}
		}
	}

	// Token: 0x06001EB2 RID: 7858 RVA: 0x000A484C File Offset: 0x000A2A4C
	private void DetachAnimOverrides()
	{
		KAnimControllerBase component = base.GetComponent<KAnimControllerBase>();
		if (this.kanimSynchronizer != null)
		{
			this.kanimSynchronizer.Remove(component);
			this.kanimSynchronizer = null;
		}
		if (this.animInfo.overrideAnims != null)
		{
			for (int i = 0; i < this.animInfo.overrideAnims.Length; i++)
			{
				component.RemoveAnimOverrides(this.animInfo.overrideAnims[i]);
			}
			this.animInfo.overrideAnims = null;
		}
	}

	// Token: 0x06001EB3 RID: 7859 RVA: 0x000A48C0 File Offset: 0x000A2AC0
	private void CreateCompletionReactable(string topic)
	{
		if (GameClock.Instance.GetTime() / 600f < 1f)
		{
			return;
		}
		EmoteReactable emoteReactable = OneshotReactableLocator.CreateOneshotReactable(base.gameObject, 3f, "WorkCompleteAcknowledgement", Db.Get().ChoreTypes.Emote, 9, 5, 100f);
		Emote clapCheer = Db.Get().Emotes.Minion.ClapCheer;
		emoteReactable.SetEmote(clapCheer);
		emoteReactable.RegisterEmoteStepCallbacks("clapcheer_pre", new Action<GameObject>(this.GetReactionEffect), null).RegisterEmoteStepCallbacks("clapcheer_pst", null, delegate(GameObject r)
		{
			r.Trigger(937885943, topic);
		});
		global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(topic, "ui", true);
		if (uisprite != null)
		{
			Thought thought = new Thought("Completion_" + topic, null, uisprite.first, "mode_satisfaction", "conversation_short", "bubble_conversation", SpeechMonitor.PREFIX_HAPPY, "", true, 4f);
			emoteReactable.SetThought(thought);
		}
	}

	// Token: 0x06001EB4 RID: 7860 RVA: 0x000A49D8 File Offset: 0x000A2BD8
	public void CreatePasserbyReactable()
	{
		if (GameClock.Instance.GetTime() / 600f < 1f)
		{
			return;
		}
		if (this.passerbyReactable == null)
		{
			EmoteReactable emoteReactable = new EmoteReactable(base.gameObject, "WorkPasserbyAcknowledgement", Db.Get().ChoreTypes.Emote, 5, 5, 30f, 720f * TuningData<DupeGreetingManager.Tuning>.Get().greetingDelayMultiplier, float.PositiveInfinity, 0f);
			Emote thumbsUp = Db.Get().Emotes.Minion.ThumbsUp;
			emoteReactable.SetEmote(thumbsUp).SetThought(Db.Get().Thoughts.Encourage).AddPrecondition(new Reactable.ReactablePrecondition(this.ReactorIsOnFloor))
				.AddPrecondition(new Reactable.ReactablePrecondition(this.ReactorIsFacingMe))
				.AddPrecondition(new Reactable.ReactablePrecondition(this.ReactorIsntPartying));
			emoteReactable.RegisterEmoteStepCallbacks("react", new Action<GameObject>(this.GetReactionEffect), null);
			this.passerbyReactable = emoteReactable;
		}
	}

	// Token: 0x06001EB5 RID: 7861 RVA: 0x000A4AD7 File Offset: 0x000A2CD7
	private void GetReactionEffect(GameObject reactor)
	{
		base.GetComponent<Effects>().Add("WorkEncouraged", true);
	}

	// Token: 0x06001EB6 RID: 7862 RVA: 0x000A4AEB File Offset: 0x000A2CEB
	private bool ReactorIsOnFloor(GameObject reactor, Navigator.ActiveTransition transition)
	{
		return transition.end == NavType.Floor;
	}

	// Token: 0x06001EB7 RID: 7863 RVA: 0x000A4AF8 File Offset: 0x000A2CF8
	private bool ReactorIsFacingMe(GameObject reactor, Navigator.ActiveTransition transition)
	{
		Facing component = reactor.GetComponent<Facing>();
		return base.transform.GetPosition().x < reactor.transform.GetPosition().x == component.GetFacing();
	}

	// Token: 0x06001EB8 RID: 7864 RVA: 0x000A4B38 File Offset: 0x000A2D38
	private bool ReactorIsntPartying(GameObject reactor, Navigator.ActiveTransition transition)
	{
		ChoreConsumer component = reactor.GetComponent<ChoreConsumer>();
		return component.choreDriver.HasChore() && component.choreDriver.GetCurrentChore().choreType != Db.Get().ChoreTypes.Party;
	}

	// Token: 0x06001EB9 RID: 7865 RVA: 0x000A4B7F File Offset: 0x000A2D7F
	public void ClearPasserbyReactable()
	{
		if (this.passerbyReactable != null)
		{
			this.passerbyReactable.Cleanup();
			this.passerbyReactable = null;
		}
	}

	// Token: 0x04001132 RID: 4402
	private const float EARLIEST_REACT_TIME = 1f;

	// Token: 0x04001133 RID: 4403
	[MyCmpGet]
	private Facing facing;

	// Token: 0x04001134 RID: 4404
	[MyCmpGet]
	private MinionResume resume;

	// Token: 0x04001137 RID: 4407
	private float workPendingCompletionTime;

	// Token: 0x04001138 RID: 4408
	private int onWorkChoreDisabledHandle;

	// Token: 0x04001139 RID: 4409
	public object workCompleteData;

	// Token: 0x0400113A RID: 4410
	private Workable.AnimInfo animInfo;

	// Token: 0x0400113B RID: 4411
	private KAnimSynchronizer kanimSynchronizer;

	// Token: 0x0400113C RID: 4412
	private StatusItemGroup.Entry previousStatusItem;

	// Token: 0x0400113D RID: 4413
	private StateMachine.Instance smi;

	// Token: 0x0400113E RID: 4414
	private bool successFullyCompleted;

	// Token: 0x0400113F RID: 4415
	private Vector3 workAnimOffset = Vector3.zero;

	// Token: 0x04001140 RID: 4416
	public bool usesMultiTool = true;

	// Token: 0x04001141 RID: 4417
	private static readonly EventSystem.IntraObjectHandler<Worker> OnChoreInterruptDelegate = new EventSystem.IntraObjectHandler<Worker>(delegate(Worker component, object data)
	{
		component.OnChoreInterrupt(data);
	});

	// Token: 0x04001142 RID: 4418
	private Reactable passerbyReactable;

	// Token: 0x02001142 RID: 4418
	public enum State
	{
		// Token: 0x04005A61 RID: 23137
		Idle,
		// Token: 0x04005A62 RID: 23138
		Working,
		// Token: 0x04005A63 RID: 23139
		PendingCompletion,
		// Token: 0x04005A64 RID: 23140
		Completing
	}

	// Token: 0x02001143 RID: 4419
	public class StartWorkInfo
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06007602 RID: 30210 RVA: 0x002B71A8 File Offset: 0x002B53A8
		// (set) Token: 0x06007603 RID: 30211 RVA: 0x002B71B0 File Offset: 0x002B53B0
		public Workable workable { get; set; }

		// Token: 0x06007604 RID: 30212 RVA: 0x002B71B9 File Offset: 0x002B53B9
		public StartWorkInfo(Workable workable)
		{
			this.workable = workable;
		}
	}

	// Token: 0x02001144 RID: 4420
	public enum WorkResult
	{
		// Token: 0x04005A67 RID: 23143
		Success,
		// Token: 0x04005A68 RID: 23144
		InProgress,
		// Token: 0x04005A69 RID: 23145
		Failed
	}
}
