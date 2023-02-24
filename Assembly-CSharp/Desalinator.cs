using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005A1 RID: 1441
[SerializationConfig(MemberSerialization.OptIn)]
public class Desalinator : StateMachineComponent<Desalinator.StatesInstance>
{
	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06002388 RID: 9096 RVA: 0x000BFD04 File Offset: 0x000BDF04
	// (set) Token: 0x06002389 RID: 9097 RVA: 0x000BFD0C File Offset: 0x000BDF0C
	public float SaltStorageLeft
	{
		get
		{
			return this._storageLeft;
		}
		set
		{
			this._storageLeft = value;
			base.smi.sm.saltStorageLeft.Set(value, base.smi, false);
		}
	}

	// Token: 0x0600238A RID: 9098 RVA: 0x000BFD34 File Offset: 0x000BDF34
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.deliveryComponents = base.GetComponents<ManualDeliveryKG>();
		this.OnConduitConnectionChanged(base.GetComponent<ConduitConsumer>().IsConnected);
		base.Subscribe<Desalinator>(-2094018600, Desalinator.OnConduitConnectionChangedDelegate);
		base.smi.StartSM();
	}

	// Token: 0x0600238B RID: 9099 RVA: 0x000BFD88 File Offset: 0x000BDF88
	private void OnConduitConnectionChanged(object data)
	{
		bool flag = (bool)data;
		foreach (ManualDeliveryKG manualDeliveryKG in this.deliveryComponents)
		{
			Element element = ElementLoader.GetElement(manualDeliveryKG.RequestedItemTag);
			if (element != null && element.IsLiquid)
			{
				manualDeliveryKG.Pause(flag, "pipe connected");
			}
		}
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x000BFDDC File Offset: 0x000BDFDC
	private void OnRefreshUserMenu(object data)
	{
		if (base.smi.GetCurrentState() == base.smi.sm.full || !base.smi.HasSalt || base.smi.emptyChore != null)
		{
			return;
		}
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("status_item_desalinator_needs_emptying", UI.USERMENUACTIONS.EMPTYDESALINATOR.NAME, delegate
		{
			base.smi.GoTo(base.smi.sm.earlyEmpty);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.CLEANTOILET.TOOLTIP, true), 1f);
	}

	// Token: 0x0600238D RID: 9101 RVA: 0x000BFE70 File Offset: 0x000BE070
	private bool CheckCanConvert()
	{
		if (this.converters == null)
		{
			this.converters = base.GetComponents<ElementConverter>();
		}
		for (int i = 0; i < this.converters.Length; i++)
		{
			if (this.converters[i].CanConvertAtAll())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600238E RID: 9102 RVA: 0x000BFEB8 File Offset: 0x000BE0B8
	private bool CheckEnoughMassToConvert()
	{
		if (this.converters == null)
		{
			this.converters = base.GetComponents<ElementConverter>();
		}
		for (int i = 0; i < this.converters.Length; i++)
		{
			if (this.converters[i].HasEnoughMassToStartConverting(false))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400145F RID: 5215
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001460 RID: 5216
	private ManualDeliveryKG[] deliveryComponents;

	// Token: 0x04001461 RID: 5217
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04001462 RID: 5218
	[Serialize]
	public float maxSalt = 1000f;

	// Token: 0x04001463 RID: 5219
	[Serialize]
	private float _storageLeft = 1000f;

	// Token: 0x04001464 RID: 5220
	private ElementConverter[] converters;

	// Token: 0x04001465 RID: 5221
	private static readonly EventSystem.IntraObjectHandler<Desalinator> OnConduitConnectionChangedDelegate = new EventSystem.IntraObjectHandler<Desalinator>(delegate(Desalinator component, object data)
	{
		component.OnConduitConnectionChanged(data);
	});

	// Token: 0x020011D1 RID: 4561
	public class StatesInstance : GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.GameInstance
	{
		// Token: 0x06007824 RID: 30756 RVA: 0x002BD60A File Offset: 0x002BB80A
		public StatesInstance(Desalinator smi)
			: base(smi)
		{
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06007825 RID: 30757 RVA: 0x002BD613 File Offset: 0x002BB813
		public bool HasSalt
		{
			get
			{
				return base.master.storage.Has(ElementLoader.FindElementByHash(SimHashes.Salt).tag);
			}
		}

		// Token: 0x06007826 RID: 30758 RVA: 0x002BD634 File Offset: 0x002BB834
		public bool IsFull()
		{
			return base.master.SaltStorageLeft <= 0f;
		}

		// Token: 0x06007827 RID: 30759 RVA: 0x002BD64B File Offset: 0x002BB84B
		public bool IsSaltRemoved()
		{
			return !this.HasSalt;
		}

		// Token: 0x06007828 RID: 30760 RVA: 0x002BD658 File Offset: 0x002BB858
		public void CreateEmptyChore()
		{
			if (this.emptyChore != null)
			{
				this.emptyChore.Cancel("dupe");
			}
			DesalinatorWorkableEmpty component = base.master.GetComponent<DesalinatorWorkableEmpty>();
			this.emptyChore = new WorkChore<DesalinatorWorkableEmpty>(Db.Get().ChoreTypes.EmptyDesalinator, component, null, true, new Action<Chore>(this.OnEmptyComplete), null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
		}

		// Token: 0x06007829 RID: 30761 RVA: 0x002BD6C0 File Offset: 0x002BB8C0
		public void CancelEmptyChore()
		{
			if (this.emptyChore != null)
			{
				this.emptyChore.Cancel("Cancelled");
				this.emptyChore = null;
			}
		}

		// Token: 0x0600782A RID: 30762 RVA: 0x002BD6E4 File Offset: 0x002BB8E4
		private void OnEmptyComplete(Chore chore)
		{
			this.emptyChore = null;
			Tag tag = GameTagExtensions.Create(SimHashes.Salt);
			ListPool<GameObject, Desalinator>.PooledList pooledList = ListPool<GameObject, Desalinator>.Allocate();
			base.master.storage.Find(tag, pooledList);
			foreach (GameObject gameObject in pooledList)
			{
				base.master.storage.Drop(gameObject, true);
			}
			pooledList.Recycle();
		}

		// Token: 0x0600782B RID: 30763 RVA: 0x002BD770 File Offset: 0x002BB970
		public void UpdateStorageLeft()
		{
			Tag tag = GameTagExtensions.Create(SimHashes.Salt);
			base.master.SaltStorageLeft = base.master.maxSalt - base.master.storage.GetMassAvailable(tag);
		}

		// Token: 0x04005C13 RID: 23571
		public Chore emptyChore;
	}

	// Token: 0x020011D2 RID: 4562
	public class States : GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator>
	{
		// Token: 0x0600782C RID: 30764 RVA: 0x002BD7B0 File Offset: 0x002BB9B0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (Desalinator.StatesInstance smi) => smi.master.operational.IsOperational);
			this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (Desalinator.StatesInstance smi) => !smi.master.operational.IsOperational).DefaultState(this.on.waiting);
			this.on.waiting.EventTransition(GameHashes.OnStorageChange, this.on.working_pre, (Desalinator.StatesInstance smi) => smi.master.CheckEnoughMassToConvert());
			this.on.working_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.on.working);
			this.on.working.Enter(delegate(Desalinator.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).QueueAnim("working_loop", true, null).EventTransition(GameHashes.OnStorageChange, this.on.working_pst, (Desalinator.StatesInstance smi) => !smi.master.CheckCanConvert())
				.ParamTransition<float>(this.saltStorageLeft, this.full, (Desalinator.StatesInstance smi, float p) => smi.IsFull())
				.EventHandler(GameHashes.OnStorageChange, delegate(Desalinator.StatesInstance smi)
				{
					smi.UpdateStorageLeft();
				})
				.Exit(delegate(Desalinator.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				});
			this.on.working_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.on.waiting);
			this.earlyEmpty.PlayAnims((Desalinator.StatesInstance smi) => Desalinator.States.FULL_ANIMS, KAnim.PlayMode.Once).OnAnimQueueComplete(this.earlyWaitingForEmpty);
			this.earlyWaitingForEmpty.Enter(delegate(Desalinator.StatesInstance smi)
			{
				smi.CreateEmptyChore();
			}).Exit(delegate(Desalinator.StatesInstance smi)
			{
				smi.CancelEmptyChore();
			}).EventTransition(GameHashes.OnStorageChange, this.empty, (Desalinator.StatesInstance smi) => smi.IsSaltRemoved());
			this.full.PlayAnims((Desalinator.StatesInstance smi) => Desalinator.States.FULL_ANIMS, KAnim.PlayMode.Once).OnAnimQueueComplete(this.fullWaitingForEmpty);
			this.fullWaitingForEmpty.Enter(delegate(Desalinator.StatesInstance smi)
			{
				smi.CreateEmptyChore();
			}).Exit(delegate(Desalinator.StatesInstance smi)
			{
				smi.CancelEmptyChore();
			}).ToggleMainStatusItem(Db.Get().BuildingStatusItems.DesalinatorNeedsEmptying, null)
				.EventTransition(GameHashes.OnStorageChange, this.empty, (Desalinator.StatesInstance smi) => smi.IsSaltRemoved());
			this.empty.PlayAnim("off").Enter("ResetStorage", delegate(Desalinator.StatesInstance smi)
			{
				smi.master.SaltStorageLeft = smi.master.maxSalt;
			}).GoTo(this.on.waiting);
		}

		// Token: 0x04005C14 RID: 23572
		public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State off;

		// Token: 0x04005C15 RID: 23573
		public Desalinator.States.OnStates on;

		// Token: 0x04005C16 RID: 23574
		public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State full;

		// Token: 0x04005C17 RID: 23575
		public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State fullWaitingForEmpty;

		// Token: 0x04005C18 RID: 23576
		public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State earlyEmpty;

		// Token: 0x04005C19 RID: 23577
		public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State earlyWaitingForEmpty;

		// Token: 0x04005C1A RID: 23578
		public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State empty;

		// Token: 0x04005C1B RID: 23579
		private static readonly HashedString[] FULL_ANIMS = new HashedString[] { "working_pst", "off" };

		// Token: 0x04005C1C RID: 23580
		public StateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.FloatParameter saltStorageLeft = new StateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.FloatParameter(0f);

		// Token: 0x02001F9A RID: 8090
		public class OnStates : GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State
		{
			// Token: 0x04008CA7 RID: 36007
			public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State waiting;

			// Token: 0x04008CA8 RID: 36008
			public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State working_pre;

			// Token: 0x04008CA9 RID: 36009
			public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State working;

			// Token: 0x04008CAA RID: 36010
			public GameStateMachine<Desalinator.States, Desalinator.StatesInstance, Desalinator, object>.State working_pst;
		}
	}
}
