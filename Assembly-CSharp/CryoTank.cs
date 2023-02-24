using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006F6 RID: 1782
public class CryoTank : StateMachineComponent<CryoTank.StatesInstance>, ISidescreenButtonControl
{
	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06003083 RID: 12419 RVA: 0x001004B5 File Offset: 0x000FE6B5
	public string SidescreenButtonText
	{
		get
		{
			return BUILDINGS.PREFABS.CRYOTANK.DEFROSTBUTTON;
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06003084 RID: 12420 RVA: 0x001004C1 File Offset: 0x000FE6C1
	public string SidescreenButtonTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.CRYOTANK.DEFROSTBUTTONTOOLTIP;
		}
	}

	// Token: 0x06003085 RID: 12421 RVA: 0x001004CD File Offset: 0x000FE6CD
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x06003086 RID: 12422 RVA: 0x001004D0 File Offset: 0x000FE6D0
	public void OnSidescreenButtonPressed()
	{
		this.OnClickOpen();
	}

	// Token: 0x06003087 RID: 12423 RVA: 0x001004D8 File Offset: 0x000FE6D8
	public bool SidescreenButtonInteractable()
	{
		return this.HasDefrostedFriend();
	}

	// Token: 0x06003088 RID: 12424 RVA: 0x001004E0 File Offset: 0x000FE6E0
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x06003089 RID: 12425 RVA: 0x001004E4 File Offset: 0x000FE6E4
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600308A RID: 12426 RVA: 0x001004EC File Offset: 0x000FE6EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		Demolishable component = base.GetComponent<Demolishable>();
		if (component != null)
		{
			component.allowDemolition = !this.HasDefrostedFriend();
		}
	}

	// Token: 0x0600308B RID: 12427 RVA: 0x00100529 File Offset: 0x000FE729
	public bool HasDefrostedFriend()
	{
		return base.smi.IsInsideState(base.smi.sm.closed) && this.chore == null;
	}

	// Token: 0x0600308C RID: 12428 RVA: 0x00100554 File Offset: 0x000FE754
	public void DropContents()
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(MinionConfig.ID), null, null);
		gameObject.name = Assets.GetPrefab(MinionConfig.ID).name;
		Immigration.Instance.ApplyDefaultPersonalPriorities(gameObject);
		Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(base.transform.position), this.dropOffset), Grid.SceneLayer.Move);
		gameObject.transform.SetLocalPosition(vector);
		gameObject.SetActive(true);
		new MinionStartingStats(false, null, "AncientKnowledge", false).Apply(gameObject);
		gameObject.GetComponent<MinionIdentity>().arrivalTime = (float)UnityEngine.Random.Range(-2000, -1000);
		MinionResume component = gameObject.GetComponent<MinionResume>();
		int num = 3;
		for (int i = 0; i < num; i++)
		{
			component.ForceAddSkillPoint();
		}
		base.smi.sm.defrostedDuplicant.Set(gameObject, base.smi, false);
		gameObject.GetComponent<Navigator>().SetCurrentNavType(NavType.Floor);
		ChoreProvider component2 = gameObject.GetComponent<ChoreProvider>();
		if (component2 != null)
		{
			base.smi.defrostAnimChore = new EmoteChore(component2, Db.Get().ChoreTypes.EmoteHighPriority, "anim_interacts_cryo_chamber_kanim", new HashedString[] { "defrost", "defrost_exit" }, KAnim.PlayMode.Once, false);
			Vector3 position = gameObject.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.Gas);
			gameObject.transform.SetPosition(position);
			gameObject.GetMyWorld().SetDupeVisited();
		}
		SaveGame.Instance.GetComponent<ColonyAchievementTracker>().defrostedDuplicant = true;
	}

	// Token: 0x0600308D RID: 12429 RVA: 0x001006F8 File Offset: 0x000FE8F8
	public void ShowEventPopup()
	{
		GameObject gameObject = base.smi.sm.defrostedDuplicant.Get(base.smi);
		if (this.opener != null && gameObject != null)
		{
			SimpleEvent.StatesInstance statesInstance = GameplayEventManager.Instance.StartNewEvent(Db.Get().GameplayEvents.CryoFriend, -1).smi as SimpleEvent.StatesInstance;
			statesInstance.minions = new GameObject[] { gameObject, this.opener };
			statesInstance.SetTextParameter("dupe", this.opener.GetProperName());
			statesInstance.SetTextParameter("friend", gameObject.GetProperName());
			statesInstance.ShowEventPopup();
		}
	}

	// Token: 0x0600308E RID: 12430 RVA: 0x001007A4 File Offset: 0x000FE9A4
	public void Cheer()
	{
		GameObject gameObject = base.smi.sm.defrostedDuplicant.Get(base.smi);
		if (this.opener != null && gameObject != null)
		{
			Db db = Db.Get();
			this.opener.GetComponent<Effects>().Add(Db.Get().effects.Get("CryoFriend"), true);
			new EmoteChore(this.opener.GetComponent<Effects>(), db.ChoreTypes.EmoteHighPriority, db.Emotes.Minion.Cheer, 1, null);
			gameObject.GetComponent<Effects>().Add(Db.Get().effects.Get("CryoFriend"), true);
			new EmoteChore(gameObject.GetComponent<Effects>(), db.ChoreTypes.EmoteHighPriority, db.Emotes.Minion.Cheer, 1, null);
		}
	}

	// Token: 0x0600308F RID: 12431 RVA: 0x0010088E File Offset: 0x000FEA8E
	private void OnClickOpen()
	{
		this.ActivateChore(null);
	}

	// Token: 0x06003090 RID: 12432 RVA: 0x00100897 File Offset: 0x000FEA97
	private void OnClickCancel()
	{
		this.CancelActivateChore(null);
	}

	// Token: 0x06003091 RID: 12433 RVA: 0x001008A0 File Offset: 0x000FEAA0
	public void ActivateChore(object param = null)
	{
		if (this.chore != null)
		{
			return;
		}
		base.GetComponent<Workable>().SetWorkTime(1.5f);
		this.chore = new WorkChore<Workable>(Db.Get().ChoreTypes.EmptyStorage, this, null, true, delegate(Chore o)
		{
			this.CompleteActivateChore();
		}, null, null, true, null, false, true, Assets.GetAnim(this.overrideAnim), false, true, true, PriorityScreen.PriorityClass.high, 5, false, true);
	}

	// Token: 0x06003092 RID: 12434 RVA: 0x0010090C File Offset: 0x000FEB0C
	public void CancelActivateChore(object param = null)
	{
		if (this.chore == null)
		{
			return;
		}
		this.chore.Cancel("User cancelled");
		this.chore = null;
	}

	// Token: 0x06003093 RID: 12435 RVA: 0x00100930 File Offset: 0x000FEB30
	private void CompleteActivateChore()
	{
		this.opener = this.chore.driver.gameObject;
		base.smi.GoTo(base.smi.sm.open);
		this.chore = null;
		Demolishable component = base.smi.GetComponent<Demolishable>();
		if (component != null)
		{
			component.allowDemolition = true;
		}
		Game.Instance.userMenu.Refresh(base.gameObject);
	}

	// Token: 0x04001D51 RID: 7505
	public string[][] possible_contents_ids;

	// Token: 0x04001D52 RID: 7506
	public string machineSound;

	// Token: 0x04001D53 RID: 7507
	public string overrideAnim;

	// Token: 0x04001D54 RID: 7508
	public CellOffset dropOffset = CellOffset.none;

	// Token: 0x04001D55 RID: 7509
	private GameObject opener;

	// Token: 0x04001D56 RID: 7510
	private Chore chore;

	// Token: 0x02001416 RID: 5142
	public class StatesInstance : GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.GameInstance
	{
		// Token: 0x06008010 RID: 32784 RVA: 0x002DE520 File Offset: 0x002DC720
		public StatesInstance(CryoTank master)
			: base(master)
		{
		}

		// Token: 0x0400627B RID: 25211
		public Chore defrostAnimChore;
	}

	// Token: 0x02001417 RID: 5143
	public class States : GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank>
	{
		// Token: 0x06008011 RID: 32785 RVA: 0x002DE52C File Offset: 0x002DC72C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.closed;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.closed.PlayAnim("on").Enter(delegate(CryoTank.StatesInstance smi)
			{
				if (smi.master.machineSound != null)
				{
					LoopingSounds component = smi.master.GetComponent<LoopingSounds>();
					if (component != null)
					{
						component.StartSound(GlobalAssets.GetSound(smi.master.machineSound, false));
					}
				}
			});
			this.open.GoTo(this.defrost).Exit(delegate(CryoTank.StatesInstance smi)
			{
				smi.master.DropContents();
			});
			this.defrost.PlayAnim("defrost").OnAnimQueueComplete(this.defrostExit).Update(delegate(CryoTank.StatesInstance smi, float dt)
			{
				smi.sm.defrostedDuplicant.Get(smi).GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.BuildingUse);
			}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(CryoTank.StatesInstance smi)
				{
					smi.master.ShowEventPopup();
				});
			this.defrostExit.PlayAnim("defrost_exit").Update(delegate(CryoTank.StatesInstance smi, float dt)
			{
				if (smi.defrostAnimChore == null || smi.defrostAnimChore.isComplete)
				{
					smi.GoTo(this.off);
				}
			}, UpdateRate.SIM_200ms, false).Exit(delegate(CryoTank.StatesInstance smi)
			{
				smi.sm.defrostedDuplicant.Get(smi).GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Move);
				smi.master.Cheer();
			});
			this.off.PlayAnim("off").Enter(delegate(CryoTank.StatesInstance smi)
			{
				if (smi.master.machineSound != null)
				{
					LoopingSounds component2 = smi.master.GetComponent<LoopingSounds>();
					if (component2 != null)
					{
						component2.StopSound(GlobalAssets.GetSound(smi.master.machineSound, false));
					}
				}
			});
		}

		// Token: 0x0400627C RID: 25212
		public StateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.TargetParameter defrostedDuplicant;

		// Token: 0x0400627D RID: 25213
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State closed;

		// Token: 0x0400627E RID: 25214
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State open;

		// Token: 0x0400627F RID: 25215
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State defrost;

		// Token: 0x04006280 RID: 25216
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State defrostExit;

		// Token: 0x04006281 RID: 25217
		public GameStateMachine<CryoTank.States, CryoTank.StatesInstance, CryoTank, object>.State off;
	}
}
