using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000617 RID: 1559
public class ObjectDispenser : Switch, IUserControlledCapacity
{
	// Token: 0x1700029E RID: 670
	// (get) Token: 0x060028BC RID: 10428 RVA: 0x000D80D9 File Offset: 0x000D62D9
	// (set) Token: 0x060028BD RID: 10429 RVA: 0x000D80F1 File Offset: 0x000D62F1
	public virtual float UserMaxCapacity
	{
		get
		{
			return Mathf.Min(this.userMaxCapacity, base.GetComponent<Storage>().capacityKg);
		}
		set
		{
			this.userMaxCapacity = value;
			this.filteredStorage.FilterChanged();
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x060028BE RID: 10430 RVA: 0x000D8105 File Offset: 0x000D6305
	public float AmountStored
	{
		get
		{
			return base.GetComponent<Storage>().MassStored();
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x060028BF RID: 10431 RVA: 0x000D8112 File Offset: 0x000D6312
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x060028C0 RID: 10432 RVA: 0x000D8119 File Offset: 0x000D6319
	public float MaxCapacity
	{
		get
		{
			return base.GetComponent<Storage>().capacityKg;
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x060028C1 RID: 10433 RVA: 0x000D8126 File Offset: 0x000D6326
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x060028C2 RID: 10434 RVA: 0x000D8129 File Offset: 0x000D6329
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x060028C3 RID: 10435 RVA: 0x000D8131 File Offset: 0x000D6331
	protected override void OnPrefabInit()
	{
		this.Initialize();
	}

	// Token: 0x060028C4 RID: 10436 RVA: 0x000D813C File Offset: 0x000D633C
	protected void Initialize()
	{
		base.OnPrefabInit();
		this.log = new LoggerFS("ObjectDispenser", 35);
		this.filteredStorage = new FilteredStorage(this, null, this, false, Db.Get().ChoreTypes.StorageFetch);
		base.Subscribe<ObjectDispenser>(-905833192, ObjectDispenser.OnCopySettingsDelegate);
	}

	// Token: 0x060028C5 RID: 10437 RVA: 0x000D8190 File Offset: 0x000D6390
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.smi = new ObjectDispenser.Instance(this, base.IsSwitchedOn);
		this.smi.StartSM();
		if (ObjectDispenser.infoStatusItem == null)
		{
			ObjectDispenser.infoStatusItem = new StatusItem("ObjectDispenserAutomationInfo", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			ObjectDispenser.infoStatusItem.resolveStringCallback = new Func<string, object, string>(ObjectDispenser.ResolveInfoStatusItemString);
		}
		this.filteredStorage.FilterChanged();
		base.GetComponent<KSelectable>().ToggleStatusItem(ObjectDispenser.infoStatusItem, true, this.smi);
	}

	// Token: 0x060028C6 RID: 10438 RVA: 0x000D8228 File Offset: 0x000D6428
	protected override void OnCleanUp()
	{
		this.filteredStorage.CleanUp();
		base.OnCleanUp();
	}

	// Token: 0x060028C7 RID: 10439 RVA: 0x000D823C File Offset: 0x000D643C
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		ObjectDispenser component = gameObject.GetComponent<ObjectDispenser>();
		if (component == null)
		{
			return;
		}
		this.UserMaxCapacity = component.UserMaxCapacity;
	}

	// Token: 0x060028C8 RID: 10440 RVA: 0x000D8278 File Offset: 0x000D6478
	public void DropHeldItems()
	{
		while (this.storage.Count > 0)
		{
			GameObject gameObject = this.storage.Drop(this.storage.items[0], true);
			if (this.rotatable != null)
			{
				gameObject.transform.SetPosition(base.transform.GetPosition() + this.rotatable.GetRotatedCellOffset(this.dropOffset).ToVector3());
			}
			else
			{
				gameObject.transform.SetPosition(base.transform.GetPosition() + this.dropOffset.ToVector3());
			}
		}
		this.smi.GetMaster().GetComponent<Storage>().DropAll(false, false, default(Vector3), true, null);
	}

	// Token: 0x060028C9 RID: 10441 RVA: 0x000D8347 File Offset: 0x000D6547
	protected override void Toggle()
	{
		base.Toggle();
	}

	// Token: 0x060028CA RID: 10442 RVA: 0x000D834F File Offset: 0x000D654F
	protected override void OnRefreshUserMenu(object data)
	{
		if (!this.smi.IsAutomated())
		{
			base.OnRefreshUserMenu(data);
		}
	}

	// Token: 0x060028CB RID: 10443 RVA: 0x000D8368 File Offset: 0x000D6568
	private static string ResolveInfoStatusItemString(string format_str, object data)
	{
		ObjectDispenser.Instance instance = (ObjectDispenser.Instance)data;
		string text = (instance.IsAutomated() ? BUILDING.STATUSITEMS.OBJECTDISPENSER.AUTOMATION_CONTROL : BUILDING.STATUSITEMS.OBJECTDISPENSER.MANUAL_CONTROL);
		string text2 = (instance.IsOpened ? BUILDING.STATUSITEMS.OBJECTDISPENSER.OPENED : BUILDING.STATUSITEMS.OBJECTDISPENSER.CLOSED);
		return string.Format(text, text2);
	}

	// Token: 0x040017EE RID: 6126
	public static readonly HashedString PORT_ID = "ObjectDispenser";

	// Token: 0x040017EF RID: 6127
	private LoggerFS log;

	// Token: 0x040017F0 RID: 6128
	public CellOffset dropOffset;

	// Token: 0x040017F1 RID: 6129
	[MyCmpReq]
	private Building building;

	// Token: 0x040017F2 RID: 6130
	[MyCmpReq]
	private Storage storage;

	// Token: 0x040017F3 RID: 6131
	[MyCmpGet]
	private Rotatable rotatable;

	// Token: 0x040017F4 RID: 6132
	private ObjectDispenser.Instance smi;

	// Token: 0x040017F5 RID: 6133
	private static StatusItem infoStatusItem;

	// Token: 0x040017F6 RID: 6134
	[Serialize]
	private float userMaxCapacity = float.PositiveInfinity;

	// Token: 0x040017F7 RID: 6135
	protected FilteredStorage filteredStorage;

	// Token: 0x040017F8 RID: 6136
	private static readonly EventSystem.IntraObjectHandler<ObjectDispenser> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<ObjectDispenser>(delegate(ObjectDispenser component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x02001289 RID: 4745
	public class States : GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser>
	{
		// Token: 0x06007AA2 RID: 31394 RVA: 0x002C86D8 File Offset: 0x002C68D8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.idle;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.idle.PlayAnim("on").EventHandler(GameHashes.OnStorageChange, delegate(ObjectDispenser.Instance smi)
			{
				smi.UpdateState();
			}).ParamTransition<bool>(this.should_open, this.drop_item, (ObjectDispenser.Instance smi, bool p) => p && !smi.master.GetComponent<Storage>().IsEmpty());
			this.load_item.PlayAnim("working_load").OnAnimQueueComplete(this.load_item_pst);
			this.load_item_pst.ParamTransition<bool>(this.should_open, this.idle, (ObjectDispenser.Instance smi, bool p) => !p).ParamTransition<bool>(this.should_open, this.drop_item, (ObjectDispenser.Instance smi, bool p) => p);
			this.drop_item.PlayAnim("working_dispense").OnAnimQueueComplete(this.idle).Exit(delegate(ObjectDispenser.Instance smi)
			{
				smi.master.DropHeldItems();
			});
		}

		// Token: 0x04005E17 RID: 24087
		public GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.State load_item;

		// Token: 0x04005E18 RID: 24088
		public GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.State load_item_pst;

		// Token: 0x04005E19 RID: 24089
		public GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.State drop_item;

		// Token: 0x04005E1A RID: 24090
		public GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.State idle;

		// Token: 0x04005E1B RID: 24091
		public StateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.BoolParameter should_open;
	}

	// Token: 0x0200128A RID: 4746
	public class Instance : GameStateMachine<ObjectDispenser.States, ObjectDispenser.Instance, ObjectDispenser, object>.GameInstance
	{
		// Token: 0x06007AA4 RID: 31396 RVA: 0x002C882C File Offset: 0x002C6A2C
		public Instance(ObjectDispenser master, bool manual_start_state)
			: base(master)
		{
			this.manual_on = manual_start_state;
			this.operational = base.GetComponent<Operational>();
			this.logic = base.GetComponent<LogicPorts>();
			base.Subscribe(-592767678, new Action<object>(this.OnOperationalChanged));
			base.Subscribe(-801688580, new Action<object>(this.OnLogicValueChanged));
			base.smi.sm.should_open.Set(true, base.smi, false);
		}

		// Token: 0x06007AA5 RID: 31397 RVA: 0x002C88B2 File Offset: 0x002C6AB2
		public void UpdateState()
		{
			base.smi.GoTo(base.sm.load_item);
		}

		// Token: 0x06007AA6 RID: 31398 RVA: 0x002C88CA File Offset: 0x002C6ACA
		public bool IsAutomated()
		{
			return this.logic.IsPortConnected(ObjectDispenser.PORT_ID);
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06007AA7 RID: 31399 RVA: 0x002C88DC File Offset: 0x002C6ADC
		public bool IsOpened
		{
			get
			{
				if (!this.IsAutomated())
				{
					return this.manual_on;
				}
				return this.logic_on;
			}
		}

		// Token: 0x06007AA8 RID: 31400 RVA: 0x002C88F3 File Offset: 0x002C6AF3
		public void SetSwitchState(bool on)
		{
			this.manual_on = on;
			this.UpdateShouldOpen();
		}

		// Token: 0x06007AA9 RID: 31401 RVA: 0x002C8902 File Offset: 0x002C6B02
		public void SetActive(bool active)
		{
			this.operational.SetActive(active, false);
		}

		// Token: 0x06007AAA RID: 31402 RVA: 0x002C8911 File Offset: 0x002C6B11
		private void OnOperationalChanged(object data)
		{
			this.UpdateShouldOpen();
		}

		// Token: 0x06007AAB RID: 31403 RVA: 0x002C891C File Offset: 0x002C6B1C
		private void OnLogicValueChanged(object data)
		{
			LogicValueChanged logicValueChanged = (LogicValueChanged)data;
			if (logicValueChanged.portID != ObjectDispenser.PORT_ID)
			{
				return;
			}
			this.logic_on = LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue);
			this.UpdateShouldOpen();
		}

		// Token: 0x06007AAC RID: 31404 RVA: 0x002C895C File Offset: 0x002C6B5C
		private void UpdateShouldOpen()
		{
			this.SetActive(this.operational.IsOperational);
			if (!this.operational.IsOperational)
			{
				return;
			}
			if (this.IsAutomated())
			{
				base.smi.sm.should_open.Set(this.logic_on, base.smi, false);
				return;
			}
			base.smi.sm.should_open.Set(this.manual_on, base.smi, false);
		}

		// Token: 0x04005E1C RID: 24092
		private Operational operational;

		// Token: 0x04005E1D RID: 24093
		public LogicPorts logic;

		// Token: 0x04005E1E RID: 24094
		public bool logic_on = true;

		// Token: 0x04005E1F RID: 24095
		private bool manual_on;
	}
}
