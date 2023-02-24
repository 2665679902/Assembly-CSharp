using System;
using System.Collections;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200066D RID: 1645
public class WarpPortal : Workable
{
	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06002C55 RID: 11349 RVA: 0x000E8F68 File Offset: 0x000E7168
	public bool ReadyToWarp
	{
		get
		{
			return this.warpPortalSMI.IsInsideState(this.warpPortalSMI.sm.occupied.waiting);
		}
	}

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06002C56 RID: 11350 RVA: 0x000E8F8A File Offset: 0x000E718A
	public bool IsWorking
	{
		get
		{
			return this.warpPortalSMI.IsInsideState(this.warpPortalSMI.sm.occupied);
		}
	}

	// Token: 0x06002C57 RID: 11351 RVA: 0x000E8FA7 File Offset: 0x000E71A7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.assignable.OnAssign += this.Assign;
	}

	// Token: 0x06002C58 RID: 11352 RVA: 0x000E8FC8 File Offset: 0x000E71C8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.warpPortalSMI = new WarpPortal.WarpPortalSM.Instance(this);
		this.warpPortalSMI.sm.isCharged.Set(!this.IsConsumed, this.warpPortalSMI, false);
		this.warpPortalSMI.StartSM();
		this.selectEventHandle = Game.Instance.Subscribe(-1503271301, new Action<object>(this.OnObjectSelected));
	}

	// Token: 0x06002C59 RID: 11353 RVA: 0x000E9039 File Offset: 0x000E7239
	private void OnObjectSelected(object data)
	{
		if (data != null && (GameObject)data == base.gameObject && Components.LiveMinionIdentities.Count > 0)
		{
			this.Discover();
		}
	}

	// Token: 0x06002C5A RID: 11354 RVA: 0x000E9064 File Offset: 0x000E7264
	protected override void OnCleanUp()
	{
		Game.Instance.Unsubscribe(this.selectEventHandle);
		base.OnCleanUp();
	}

	// Token: 0x06002C5B RID: 11355 RVA: 0x000E907C File Offset: 0x000E727C
	private void Discover()
	{
		if (this.discovered)
		{
			return;
		}
		ClusterManager.Instance.GetWorld(this.GetTargetWorldID()).SetDiscovered(true);
		SimpleEvent.StatesInstance statesInstance = GameplayEventManager.Instance.StartNewEvent(Db.Get().GameplayEvents.WarpWorldReveal, -1).smi as SimpleEvent.StatesInstance;
		statesInstance.minions = new GameObject[] { Components.LiveMinionIdentities[0].gameObject };
		statesInstance.callback = delegate
		{
			ManagementMenu.Instance.OpenClusterMap();
			ClusterMapScreen.Instance.SetTargetFocusPosition(ClusterManager.Instance.GetWorld(this.GetTargetWorldID()).GetMyWorldLocation(), 0.5f);
		};
		statesInstance.ShowEventPopup();
		this.discovered = true;
	}

	// Token: 0x06002C5C RID: 11356 RVA: 0x000E910B File Offset: 0x000E730B
	public void StartWarpSequence()
	{
		this.warpPortalSMI.GoTo(this.warpPortalSMI.sm.occupied.warping);
	}

	// Token: 0x06002C5D RID: 11357 RVA: 0x000E912D File Offset: 0x000E732D
	public void CancelAssignment()
	{
		this.CancelChore();
		this.assignable.Unassign();
		this.warpPortalSMI.GoTo(this.warpPortalSMI.sm.idle);
	}

	// Token: 0x06002C5E RID: 11358 RVA: 0x000E915C File Offset: 0x000E735C
	private int GetTargetWorldID()
	{
		SaveGame.Instance.GetComponent<WorldGenSpawner>().SpawnTag(WarpReceiverConfig.ID);
		foreach (WarpReceiver warpReceiver in UnityEngine.Object.FindObjectsOfType<WarpReceiver>())
		{
			if (warpReceiver.GetMyWorldId() != this.GetMyWorldId())
			{
				return warpReceiver.GetMyWorldId();
			}
		}
		global::Debug.LogError("No receiver world found for warp portal sender");
		return -1;
	}

	// Token: 0x06002C5F RID: 11359 RVA: 0x000E91B8 File Offset: 0x000E73B8
	private void Warp()
	{
		if (base.worker == null || base.worker.HasTag(GameTags.Dying) || base.worker.HasTag(GameTags.Dead))
		{
			return;
		}
		WarpReceiver warpReceiver = null;
		foreach (WarpReceiver warpReceiver2 in UnityEngine.Object.FindObjectsOfType<WarpReceiver>())
		{
			if (warpReceiver2.GetMyWorldId() != this.GetMyWorldId())
			{
				warpReceiver = warpReceiver2;
				break;
			}
		}
		if (warpReceiver == null)
		{
			SaveGame.Instance.GetComponent<WorldGenSpawner>().SpawnTag(WarpReceiverConfig.ID);
			warpReceiver = UnityEngine.Object.FindObjectOfType<WarpReceiver>();
		}
		if (warpReceiver != null)
		{
			this.delayWarpRoutine = base.StartCoroutine(this.DelayedWarp(warpReceiver));
		}
		else
		{
			global::Debug.LogWarning("No warp receiver found - maybe POI stomping or failure to spawn?");
		}
		if (SelectTool.Instance.selected == base.GetComponent<KSelectable>())
		{
			SelectTool.Instance.Select(null, true);
		}
	}

	// Token: 0x06002C60 RID: 11360 RVA: 0x000E9292 File Offset: 0x000E7492
	public IEnumerator DelayedWarp(WarpReceiver receiver)
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		int myWorldId = base.worker.GetMyWorldId();
		int myWorldId2 = receiver.GetMyWorldId();
		CameraController.Instance.ActiveWorldStarWipe(myWorldId2, Grid.CellToPos(Grid.PosToCell(receiver)), 10f, null);
		Worker worker = base.worker;
		worker.StopWork();
		receiver.ReceiveWarpedDuplicant(worker);
		ClusterManager.Instance.MigrateMinion(worker.GetComponent<MinionIdentity>(), myWorldId2, myWorldId);
		this.delayWarpRoutine = null;
		yield break;
	}

	// Token: 0x06002C61 RID: 11361 RVA: 0x000E92A8 File Offset: 0x000E74A8
	public void SetAssignable(bool set_it)
	{
		this.assignable.SetCanBeAssigned(set_it);
		this.RefreshSideScreen();
	}

	// Token: 0x06002C62 RID: 11362 RVA: 0x000E92BC File Offset: 0x000E74BC
	private void Assign(IAssignableIdentity new_assignee)
	{
		this.CancelChore();
		if (new_assignee != null)
		{
			this.ActivateChore();
		}
	}

	// Token: 0x06002C63 RID: 11363 RVA: 0x000E92D0 File Offset: 0x000E74D0
	private void ActivateChore()
	{
		global::Debug.Assert(this.chore == null);
		this.chore = new WorkChore<Workable>(Db.Get().ChoreTypes.Migrate, this, null, true, delegate(Chore o)
		{
			this.CompleteChore();
		}, null, null, true, null, false, true, Assets.GetAnim("anim_interacts_warp_portal_sender_kanim"), false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		base.SetWorkTime(float.PositiveInfinity);
		this.workLayer = Grid.SceneLayer.Building;
		this.workAnims = new HashedString[] { "sending_pre", "sending_loop" };
		this.workingPstComplete = new HashedString[] { "sending_pst" };
		this.workingPstFailed = new HashedString[] { "idle_loop" };
		this.showProgressBar = false;
	}

	// Token: 0x06002C64 RID: 11364 RVA: 0x000E93B2 File Offset: 0x000E75B2
	private void CancelChore()
	{
		if (this.chore == null)
		{
			return;
		}
		this.chore.Cancel("User cancelled");
		this.chore = null;
		if (this.delayWarpRoutine != null)
		{
			base.StopCoroutine(this.delayWarpRoutine);
			this.delayWarpRoutine = null;
		}
	}

	// Token: 0x06002C65 RID: 11365 RVA: 0x000E93EF File Offset: 0x000E75EF
	private void CompleteChore()
	{
		this.IsConsumed = true;
		this.chore.Cleanup();
		this.chore = null;
	}

	// Token: 0x06002C66 RID: 11366 RVA: 0x000E940A File Offset: 0x000E760A
	public void RefreshSideScreen()
	{
		if (base.GetComponent<KSelectable>().IsSelected)
		{
			DetailsScreen.Instance.Refresh(base.gameObject);
		}
	}

	// Token: 0x04001A65 RID: 6757
	[MyCmpReq]
	public Assignable assignable;

	// Token: 0x04001A66 RID: 6758
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x04001A67 RID: 6759
	private Chore chore;

	// Token: 0x04001A68 RID: 6760
	private WarpPortal.WarpPortalSM.Instance warpPortalSMI;

	// Token: 0x04001A69 RID: 6761
	private Notification notification;

	// Token: 0x04001A6A RID: 6762
	public const float RECHARGE_TIME = 3000f;

	// Token: 0x04001A6B RID: 6763
	[Serialize]
	public bool IsConsumed;

	// Token: 0x04001A6C RID: 6764
	[Serialize]
	public float rechargeProgress;

	// Token: 0x04001A6D RID: 6765
	[Serialize]
	private bool discovered;

	// Token: 0x04001A6E RID: 6766
	private int selectEventHandle = -1;

	// Token: 0x04001A6F RID: 6767
	private Coroutine delayWarpRoutine;

	// Token: 0x04001A70 RID: 6768
	private static readonly HashedString[] printing_anim = new HashedString[] { "printing_pre", "printing_loop", "printing_pst" };

	// Token: 0x02001334 RID: 4916
	public class WarpPortalSM : GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal>
	{
		// Token: 0x06007CF7 RID: 31991 RVA: 0x002D27FC File Offset: 0x002D09FC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				if (smi.master.rechargeProgress != 0f)
				{
					smi.GoTo(this.recharging);
				}
			}).DefaultState(this.idle);
			this.idle.PlayAnim("idle", KAnim.PlayMode.Loop).Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.IsConsumed = false;
				smi.sm.isCharged.Set(true, smi, false);
				smi.master.SetAssignable(true);
			}).Exit(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.SetAssignable(false);
			})
				.WorkableStartTransition((WarpPortal.WarpPortalSM.Instance smi) => smi.master, this.become_occupied)
				.ParamTransition<bool>(this.isCharged, this.recharging, GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.IsFalse);
			this.become_occupied.Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				this.worker.Set(smi.master.worker, smi);
				smi.GoTo(this.occupied.get_on);
			});
			this.occupied.OnTargetLost(this.worker, this.idle).Target(this.worker).TagTransition(GameTags.Dying, this.idle, false)
				.Target(this.masterTarget)
				.Exit(delegate(WarpPortal.WarpPortalSM.Instance smi)
				{
					this.worker.Set(null, smi);
				});
			this.occupied.get_on.PlayAnim("sending_pre").OnAnimQueueComplete(this.occupied.waiting);
			this.occupied.waiting.PlayAnim("sending_loop", KAnim.PlayMode.Loop).ToggleNotification((WarpPortal.WarpPortalSM.Instance smi) => smi.CreateDupeWaitingNotification()).Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.RefreshSideScreen();
			})
				.Exit(delegate(WarpPortal.WarpPortalSM.Instance smi)
				{
					smi.master.RefreshSideScreen();
				});
			this.occupied.warping.PlayAnim("sending_pst").OnAnimQueueComplete(this.do_warp);
			this.do_warp.Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.Warp();
			}).GoTo(this.recharging);
			this.recharging.Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.SetAssignable(false);
				smi.master.IsConsumed = true;
				this.isCharged.Set(false, smi, false);
			}).PlayAnim("recharge", KAnim.PlayMode.Loop).ToggleStatusItem(Db.Get().BuildingStatusItems.WarpPortalCharging, (WarpPortal.WarpPortalSM.Instance smi) => smi.master)
				.Update(delegate(WarpPortal.WarpPortalSM.Instance smi, float dt)
				{
					smi.master.rechargeProgress += dt;
					if (smi.master.rechargeProgress > 3000f)
					{
						this.isCharged.Set(true, smi, false);
						smi.master.rechargeProgress = 0f;
						smi.GoTo(this.idle);
					}
				}, UpdateRate.SIM_200ms, false);
		}

		// Token: 0x04005FD9 RID: 24537
		public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State idle;

		// Token: 0x04005FDA RID: 24538
		public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State become_occupied;

		// Token: 0x04005FDB RID: 24539
		public WarpPortal.WarpPortalSM.OccupiedStates occupied;

		// Token: 0x04005FDC RID: 24540
		public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State do_warp;

		// Token: 0x04005FDD RID: 24541
		public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State recharging;

		// Token: 0x04005FDE RID: 24542
		public StateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.BoolParameter isCharged;

		// Token: 0x04005FDF RID: 24543
		private StateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.TargetParameter worker;

		// Token: 0x0200201C RID: 8220
		public class OccupiedStates : GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State
		{
			// Token: 0x04008F44 RID: 36676
			public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State get_on;

			// Token: 0x04008F45 RID: 36677
			public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State waiting;

			// Token: 0x04008F46 RID: 36678
			public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State warping;
		}

		// Token: 0x0200201D RID: 8221
		public new class Instance : GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.GameInstance
		{
			// Token: 0x0600A275 RID: 41589 RVA: 0x00344406 File Offset: 0x00342606
			public Instance(WarpPortal master)
				: base(master)
			{
			}

			// Token: 0x0600A276 RID: 41590 RVA: 0x00344410 File Offset: 0x00342610
			public Notification CreateDupeWaitingNotification()
			{
				if (base.master.worker != null)
				{
					return new Notification(MISC.NOTIFICATIONS.WARP_PORTAL_DUPE_READY.NAME.Replace("{dupe}", base.master.worker.name), NotificationType.Neutral, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.WARP_PORTAL_DUPE_READY.TOOLTIP.Replace("{dupe}", base.master.worker.name), null, false, 0f, null, null, base.master.transform, true, false, false);
				}
				return null;
			}
		}
	}
}
