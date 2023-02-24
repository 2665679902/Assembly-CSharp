using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020005C1 RID: 1473
[AddComponentMenu("KMonoBehaviour/Workable/GeneShuffler")]
public class GeneShuffler : Workable
{
	// Token: 0x170001FA RID: 506
	// (get) Token: 0x0600248D RID: 9357 RVA: 0x000C5880 File Offset: 0x000C3A80
	public bool WorkComplete
	{
		get
		{
			return this.geneShufflerSMI.IsInsideState(this.geneShufflerSMI.sm.working.complete);
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x0600248E RID: 9358 RVA: 0x000C58A2 File Offset: 0x000C3AA2
	public bool IsWorking
	{
		get
		{
			return this.geneShufflerSMI.IsInsideState(this.geneShufflerSMI.sm.working);
		}
	}

	// Token: 0x0600248F RID: 9359 RVA: 0x000C58BF File Offset: 0x000C3ABF
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.assignable.OnAssign += this.Assign;
		this.lightEfficiencyBonus = false;
	}

	// Token: 0x06002490 RID: 9360 RVA: 0x000C58E8 File Offset: 0x000C3AE8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.showProgressBar = false;
		this.geneShufflerSMI = new GeneShuffler.GeneShufflerSM.Instance(this);
		this.RefreshRechargeChore();
		this.RefreshConsumedState();
		base.Subscribe<GeneShuffler>(-1697596308, GeneShuffler.OnStorageChangeDelegate);
		this.geneShufflerSMI.StartSM();
	}

	// Token: 0x06002491 RID: 9361 RVA: 0x000C5936 File Offset: 0x000C3B36
	private void Assign(IAssignableIdentity new_assignee)
	{
		this.CancelChore();
		if (new_assignee != null)
		{
			this.ActivateChore();
		}
	}

	// Token: 0x06002492 RID: 9362 RVA: 0x000C5947 File Offset: 0x000C3B47
	private void Recharge()
	{
		this.SetConsumed(false);
		this.RequestRecharge(false);
		this.RefreshRechargeChore();
		this.RefreshSideScreen();
	}

	// Token: 0x06002493 RID: 9363 RVA: 0x000C5963 File Offset: 0x000C3B63
	private void SetConsumed(bool consumed)
	{
		this.IsConsumed = consumed;
		this.RefreshConsumedState();
	}

	// Token: 0x06002494 RID: 9364 RVA: 0x000C5972 File Offset: 0x000C3B72
	private void RefreshConsumedState()
	{
		this.geneShufflerSMI.sm.isCharged.Set(!this.IsConsumed, this.geneShufflerSMI, false);
	}

	// Token: 0x06002495 RID: 9365 RVA: 0x000C599C File Offset: 0x000C3B9C
	private void OnStorageChange(object data)
	{
		if (this.storage_recursion_guard)
		{
			return;
		}
		this.storage_recursion_guard = true;
		if (this.IsConsumed)
		{
			for (int i = this.storage.items.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = this.storage.items[i];
				if (!(gameObject == null) && gameObject.IsPrefabID(GeneShuffler.RechargeTag))
				{
					this.storage.ConsumeIgnoringDisease(gameObject);
					this.Recharge();
					break;
				}
			}
		}
		this.storage_recursion_guard = false;
	}

	// Token: 0x06002496 RID: 9366 RVA: 0x000C5A24 File Offset: 0x000C3C24
	protected override void OnStartWork(Worker worker)
	{
		base.OnStartWork(worker);
		this.notification = new Notification(MISC.NOTIFICATIONS.GENESHUFFLER.NAME, NotificationType.Good, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.GENESHUFFLER.TOOLTIP + notificationList.ReduceMessages(false), null, false, 0f, null, null, null, true, false, false);
		this.notifier.Add(this.notification, "");
		this.DeSelectBuilding();
	}

	// Token: 0x06002497 RID: 9367 RVA: 0x000C5A96 File Offset: 0x000C3C96
	private void DeSelectBuilding()
	{
		if (base.GetComponent<KSelectable>().IsSelected)
		{
			SelectTool.Instance.Select(null, true);
		}
	}

	// Token: 0x06002498 RID: 9368 RVA: 0x000C5AB1 File Offset: 0x000C3CB1
	protected override bool OnWorkTick(Worker worker, float dt)
	{
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06002499 RID: 9369 RVA: 0x000C5ABB File Offset: 0x000C3CBB
	protected override void OnAbortWork(Worker worker)
	{
		base.OnAbortWork(worker);
		if (this.chore != null)
		{
			this.chore.Cancel("aborted");
		}
		this.notifier.Remove(this.notification);
	}

	// Token: 0x0600249A RID: 9370 RVA: 0x000C5AED File Offset: 0x000C3CED
	protected override void OnStopWork(Worker worker)
	{
		base.OnStopWork(worker);
		if (this.chore != null)
		{
			this.chore.Cancel("stopped");
		}
		this.notifier.Remove(this.notification);
	}

	// Token: 0x0600249B RID: 9371 RVA: 0x000C5B20 File Offset: 0x000C3D20
	protected override void OnCompleteWork(Worker worker)
	{
		base.OnCompleteWork(worker);
		CameraController.Instance.CameraGoTo(base.transform.GetPosition(), 1f, false);
		this.ApplyRandomTrait(worker);
		this.assignable.Unassign();
		this.DeSelectBuilding();
		this.notifier.Remove(this.notification);
	}

	// Token: 0x0600249C RID: 9372 RVA: 0x000C5B78 File Offset: 0x000C3D78
	private void ApplyRandomTrait(Worker worker)
	{
		Traits component = worker.GetComponent<Traits>();
		List<string> list = new List<string>();
		foreach (DUPLICANTSTATS.TraitVal traitVal in DUPLICANTSTATS.GENESHUFFLERTRAITS)
		{
			if (!component.HasTrait(traitVal.id))
			{
				list.Add(traitVal.id);
			}
		}
		if (list.Count > 0)
		{
			string text = list[UnityEngine.Random.Range(0, list.Count)];
			Trait trait = Db.Get().traits.TryGet(text);
			worker.GetComponent<Traits>().Add(trait);
			InfoDialogScreen infoDialogScreen = (InfoDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
			string text2 = string.Format(UI.GENESHUFFLERMESSAGE.BODY_SUCCESS, worker.GetProperName(), trait.Name, trait.GetTooltip());
			infoDialogScreen.SetHeader(UI.GENESHUFFLERMESSAGE.HEADER).AddPlainText(text2).AddDefaultOK(false);
			this.SetConsumed(true);
			return;
		}
		InfoDialogScreen infoDialogScreen2 = (InfoDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
		string text3 = string.Format(UI.GENESHUFFLERMESSAGE.BODY_FAILURE, worker.GetProperName());
		infoDialogScreen2.SetHeader(UI.GENESHUFFLERMESSAGE.HEADER).AddPlainText(text3).AddDefaultOK(false);
	}

	// Token: 0x0600249D RID: 9373 RVA: 0x000C5D08 File Offset: 0x000C3F08
	private void ActivateChore()
	{
		global::Debug.Assert(this.chore == null);
		base.GetComponent<Workable>().SetWorkTime(float.PositiveInfinity);
		this.chore = new WorkChore<Workable>(Db.Get().ChoreTypes.GeneShuffle, this, null, true, delegate(Chore o)
		{
			this.CompleteChore();
		}, null, null, true, null, false, true, Assets.GetAnim("anim_interacts_neuralvacillator_kanim"), false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
	}

	// Token: 0x0600249E RID: 9374 RVA: 0x000C5D78 File Offset: 0x000C3F78
	private void CancelChore()
	{
		if (this.chore == null)
		{
			return;
		}
		this.chore.Cancel("User cancelled");
		this.chore = null;
	}

	// Token: 0x0600249F RID: 9375 RVA: 0x000C5D9A File Offset: 0x000C3F9A
	private void CompleteChore()
	{
		this.chore.Cleanup();
		this.chore = null;
	}

	// Token: 0x060024A0 RID: 9376 RVA: 0x000C5DAE File Offset: 0x000C3FAE
	public void RequestRecharge(bool request)
	{
		this.RechargeRequested = request;
		this.RefreshRechargeChore();
	}

	// Token: 0x060024A1 RID: 9377 RVA: 0x000C5DBD File Offset: 0x000C3FBD
	private void RefreshRechargeChore()
	{
		this.delivery.Pause(!this.RechargeRequested, "No recharge requested");
	}

	// Token: 0x060024A2 RID: 9378 RVA: 0x000C5DD8 File Offset: 0x000C3FD8
	public void RefreshSideScreen()
	{
		if (base.GetComponent<KSelectable>().IsSelected)
		{
			DetailsScreen.Instance.Refresh(base.gameObject);
		}
	}

	// Token: 0x060024A3 RID: 9379 RVA: 0x000C5DF7 File Offset: 0x000C3FF7
	public void SetAssignable(bool set_it)
	{
		this.assignable.SetCanBeAssigned(set_it);
		this.RefreshSideScreen();
	}

	// Token: 0x0400150E RID: 5390
	[MyCmpReq]
	public Assignable assignable;

	// Token: 0x0400150F RID: 5391
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x04001510 RID: 5392
	[MyCmpReq]
	public ManualDeliveryKG delivery;

	// Token: 0x04001511 RID: 5393
	[MyCmpReq]
	public Storage storage;

	// Token: 0x04001512 RID: 5394
	[Serialize]
	public bool IsConsumed;

	// Token: 0x04001513 RID: 5395
	[Serialize]
	public bool RechargeRequested;

	// Token: 0x04001514 RID: 5396
	private Chore chore;

	// Token: 0x04001515 RID: 5397
	private GeneShuffler.GeneShufflerSM.Instance geneShufflerSMI;

	// Token: 0x04001516 RID: 5398
	private Notification notification;

	// Token: 0x04001517 RID: 5399
	private static Tag RechargeTag = new Tag("GeneShufflerRecharge");

	// Token: 0x04001518 RID: 5400
	private static readonly EventSystem.IntraObjectHandler<GeneShuffler> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<GeneShuffler>(delegate(GeneShuffler component, object data)
	{
		component.OnStorageChange(data);
	});

	// Token: 0x04001519 RID: 5401
	private bool storage_recursion_guard;

	// Token: 0x02001200 RID: 4608
	public class GeneShufflerSM : GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler>
	{
		// Token: 0x060078AA RID: 30890 RVA: 0x002C0328 File Offset: 0x002BE528
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.PlayAnim("on").Enter(delegate(GeneShuffler.GeneShufflerSM.Instance smi)
			{
				smi.master.SetAssignable(true);
			}).Exit(delegate(GeneShuffler.GeneShufflerSM.Instance smi)
			{
				smi.master.SetAssignable(false);
			})
				.WorkableStartTransition((GeneShuffler.GeneShufflerSM.Instance smi) => smi.master, this.working.pre)
				.ParamTransition<bool>(this.isCharged, this.consumed, GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.IsFalse);
			this.working.pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working.loop);
			this.working.loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).ScheduleGoTo(5f, this.working.complete);
			this.working.complete.ToggleStatusItem(Db.Get().BuildingStatusItems.GeneShuffleCompleted, null).Enter(delegate(GeneShuffler.GeneShufflerSM.Instance smi)
			{
				smi.master.RefreshSideScreen();
			}).WorkableStopTransition((GeneShuffler.GeneShufflerSM.Instance smi) => smi.master, this.working.pst);
			this.working.pst.OnAnimQueueComplete(this.consumed);
			this.consumed.PlayAnim("off", KAnim.PlayMode.Once).ParamTransition<bool>(this.isCharged, this.recharging, GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.IsTrue);
			this.recharging.PlayAnim("recharging", KAnim.PlayMode.Once).OnAnimQueueComplete(this.idle);
		}

		// Token: 0x04005CAC RID: 23724
		public GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.State idle;

		// Token: 0x04005CAD RID: 23725
		public GeneShuffler.GeneShufflerSM.WorkingStates working;

		// Token: 0x04005CAE RID: 23726
		public GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.State consumed;

		// Token: 0x04005CAF RID: 23727
		public GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.State recharging;

		// Token: 0x04005CB0 RID: 23728
		public StateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.BoolParameter isCharged;

		// Token: 0x02001FAA RID: 8106
		public class WorkingStates : GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.State
		{
			// Token: 0x04008D12 RID: 36114
			public GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.State pre;

			// Token: 0x04008D13 RID: 36115
			public GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.State loop;

			// Token: 0x04008D14 RID: 36116
			public GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.State complete;

			// Token: 0x04008D15 RID: 36117
			public GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.State pst;
		}

		// Token: 0x02001FAB RID: 8107
		public new class Instance : GameStateMachine<GeneShuffler.GeneShufflerSM, GeneShuffler.GeneShufflerSM.Instance, GeneShuffler, object>.GameInstance
		{
			// Token: 0x0600A035 RID: 41013 RVA: 0x00341650 File Offset: 0x0033F850
			public Instance(GeneShuffler master)
				: base(master)
			{
			}
		}
	}
}
