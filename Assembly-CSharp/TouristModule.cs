using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000968 RID: 2408
[SerializationConfig(MemberSerialization.OptIn)]
public class TouristModule : StateMachineComponent<TouristModule.StatesInstance>
{
	// Token: 0x17000559 RID: 1369
	// (get) Token: 0x06004761 RID: 18273 RVA: 0x00191E74 File Offset: 0x00190074
	public bool IsSuspended
	{
		get
		{
			return this.isSuspended;
		}
	}

	// Token: 0x06004762 RID: 18274 RVA: 0x00191E7C File Offset: 0x0019007C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06004763 RID: 18275 RVA: 0x00191E84 File Offset: 0x00190084
	public void SetSuspended(bool state)
	{
		this.isSuspended = state;
	}

	// Token: 0x06004764 RID: 18276 RVA: 0x00191E90 File Offset: 0x00190090
	public void ReleaseAstronaut(object data, bool applyBuff = false)
	{
		if (this.releasingAstronaut)
		{
			return;
		}
		this.releasingAstronaut = true;
		MinionStorage component = base.GetComponent<MinionStorage>();
		List<MinionStorage.Info> storedMinionInfo = component.GetStoredMinionInfo();
		for (int i = storedMinionInfo.Count - 1; i >= 0; i--)
		{
			MinionStorage.Info info = storedMinionInfo[i];
			GameObject gameObject = component.DeserializeMinion(info.id, Grid.CellToPos(Grid.PosToCell(base.smi.master.transform.GetPosition())));
			if (Grid.FakeFloor[Grid.OffsetCell(Grid.PosToCell(base.smi.master.gameObject), 0, -1)])
			{
				gameObject.GetComponent<Navigator>().SetCurrentNavType(NavType.Floor);
				if (applyBuff)
				{
					gameObject.GetComponent<Effects>().Add(Db.Get().effects.Get("SpaceTourist"), true);
					JoyBehaviourMonitor.Instance smi = gameObject.GetSMI<JoyBehaviourMonitor.Instance>();
					if (smi != null)
					{
						smi.GoToOverjoyed();
					}
				}
			}
		}
		this.releasingAstronaut = false;
	}

	// Token: 0x06004765 RID: 18277 RVA: 0x00191F80 File Offset: 0x00190180
	public void OnSuspend(object data)
	{
		Storage component = base.GetComponent<Storage>();
		if (component != null)
		{
			component.capacityKg = component.MassStored();
			component.allowItemRemoval = false;
		}
		if (base.GetComponent<ManualDeliveryKG>() != null)
		{
			UnityEngine.Object.Destroy(base.GetComponent<ManualDeliveryKG>());
		}
		this.SetSuspended(true);
	}

	// Token: 0x06004766 RID: 18278 RVA: 0x00191FD0 File Offset: 0x001901D0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.storage = base.GetComponent<Storage>();
		this.assignable = base.GetComponent<Assignable>();
		base.smi.StartSM();
		int num = Grid.PosToCell(base.gameObject);
		this.partitionerEntry = GameScenePartitioner.Instance.Add("TouristModule.gantryChanged", base.gameObject, num, GameScenePartitioner.Instance.validNavCellChangedLayer, new Action<object>(this.OnGantryChanged));
		this.OnGantryChanged(null);
		base.Subscribe<TouristModule>(-1277991738, TouristModule.OnSuspendDelegate);
		base.Subscribe<TouristModule>(684616645, TouristModule.OnAssigneeChangedDelegate);
	}

	// Token: 0x06004767 RID: 18279 RVA: 0x00192070 File Offset: 0x00190270
	private void OnGantryChanged(object data)
	{
		if (base.gameObject != null)
		{
			KSelectable component = base.GetComponent<KSelectable>();
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.HasGantry, false);
			component.RemoveStatusItem(Db.Get().BuildingStatusItems.MissingGantry, false);
			int num = Grid.OffsetCell(Grid.PosToCell(base.smi.master.gameObject), 0, -1);
			if (Grid.FakeFloor[num])
			{
				component.AddStatusItem(Db.Get().BuildingStatusItems.HasGantry, null);
				return;
			}
			component.AddStatusItem(Db.Get().BuildingStatusItems.MissingGantry, null);
		}
	}

	// Token: 0x06004768 RID: 18280 RVA: 0x0019211C File Offset: 0x0019031C
	private Chore CreateWorkChore()
	{
		WorkChore<CommandModuleWorkable> workChore = new WorkChore<CommandModuleWorkable>(Db.Get().ChoreTypes.Astronaut, this, null, true, null, null, null, false, null, false, true, Assets.GetAnim("anim_hat_kanim"), false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, true);
		workChore.AddPrecondition(ChorePreconditions.instance.IsAssignedtoMe, this.assignable);
		return workChore;
	}

	// Token: 0x06004769 RID: 18281 RVA: 0x00192174 File Offset: 0x00190374
	private void OnAssigneeChanged(object data)
	{
		if (data == null && base.gameObject.HasTag(GameTags.RocketOnGround) && base.GetComponent<MinionStorage>().GetStoredMinionInfo().Count > 0)
		{
			this.ReleaseAstronaut(null, false);
			Game.Instance.userMenu.Refresh(base.gameObject);
		}
	}

	// Token: 0x0600476A RID: 18282 RVA: 0x001921CC File Offset: 0x001903CC
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		this.partitionerEntry.Clear();
		this.ReleaseAstronaut(null, false);
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		base.smi.StopSM("cleanup");
	}

	// Token: 0x04002F2F RID: 12079
	public Storage storage;

	// Token: 0x04002F30 RID: 12080
	[Serialize]
	private bool isSuspended;

	// Token: 0x04002F31 RID: 12081
	private bool releasingAstronaut;

	// Token: 0x04002F32 RID: 12082
	private const Sim.Cell.Properties floorCellProperties = (Sim.Cell.Properties)39;

	// Token: 0x04002F33 RID: 12083
	public Assignable assignable;

	// Token: 0x04002F34 RID: 12084
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04002F35 RID: 12085
	private static readonly EventSystem.IntraObjectHandler<TouristModule> OnSuspendDelegate = new EventSystem.IntraObjectHandler<TouristModule>(delegate(TouristModule component, object data)
	{
		component.OnSuspend(data);
	});

	// Token: 0x04002F36 RID: 12086
	private static readonly EventSystem.IntraObjectHandler<TouristModule> OnAssigneeChangedDelegate = new EventSystem.IntraObjectHandler<TouristModule>(delegate(TouristModule component, object data)
	{
		component.OnAssigneeChanged(data);
	});

	// Token: 0x02001769 RID: 5993
	public class StatesInstance : GameStateMachine<TouristModule.States, TouristModule.StatesInstance, TouristModule, object>.GameInstance
	{
		// Token: 0x06008ADE RID: 35550 RVA: 0x002FE458 File Offset: 0x002FC658
		public StatesInstance(TouristModule smi)
			: base(smi)
		{
			smi.gameObject.Subscribe(-887025858, delegate(object data)
			{
				smi.SetSuspended(false);
				smi.ReleaseAstronaut(null, true);
				smi.assignable.Unassign();
			});
		}
	}

	// Token: 0x0200176A RID: 5994
	public class States : GameStateMachine<TouristModule.States, TouristModule.StatesInstance, TouristModule>
	{
		// Token: 0x06008ADF RID: 35551 RVA: 0x002FE4A0 File Offset: 0x002FC6A0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			this.idle.PlayAnim("grounded", KAnim.PlayMode.Loop).GoTo(this.awaitingTourist);
			this.awaitingTourist.PlayAnim("grounded", KAnim.PlayMode.Loop).ToggleChore((TouristModule.StatesInstance smi) => smi.master.CreateWorkChore(), this.hasTourist);
			this.hasTourist.PlayAnim("grounded", KAnim.PlayMode.Loop).EventTransition(GameHashes.RocketLanded, this.idle, null).EventTransition(GameHashes.AssigneeChanged, this.idle, null);
		}

		// Token: 0x04006CFF RID: 27903
		public GameStateMachine<TouristModule.States, TouristModule.StatesInstance, TouristModule, object>.State idle;

		// Token: 0x04006D00 RID: 27904
		public GameStateMachine<TouristModule.States, TouristModule.StatesInstance, TouristModule, object>.State awaitingTourist;

		// Token: 0x04006D01 RID: 27905
		public GameStateMachine<TouristModule.States, TouristModule.StatesInstance, TouristModule, object>.State hasTourist;
	}
}
