using System;
using STRINGS;

// Token: 0x020005BF RID: 1471
public class Gantry : Switch
{
	// Token: 0x06002480 RID: 9344 RVA: 0x000C5630 File Offset: 0x000C3830
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (Gantry.infoStatusItem == null)
		{
			Gantry.infoStatusItem = new StatusItem("GantryAutomationInfo", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			Gantry.infoStatusItem.resolveStringCallback = new Func<string, object, string>(Gantry.ResolveInfoStatusItemString);
		}
		base.GetComponent<KAnimControllerBase>().PlaySpeedMultiplier = 0.5f;
		this.smi = new Gantry.Instance(this, base.IsSwitchedOn);
		this.smi.StartSM();
		base.GetComponent<KSelectable>().ToggleStatusItem(Gantry.infoStatusItem, true, this.smi);
	}

	// Token: 0x06002481 RID: 9345 RVA: 0x000C56CD File Offset: 0x000C38CD
	protected override void OnCleanUp()
	{
		if (this.smi != null)
		{
			this.smi.StopSM("cleanup");
		}
		base.OnCleanUp();
	}

	// Token: 0x06002482 RID: 9346 RVA: 0x000C56ED File Offset: 0x000C38ED
	public void SetWalkable(bool active)
	{
		this.fakeFloorAdder.SetFloor(active);
	}

	// Token: 0x06002483 RID: 9347 RVA: 0x000C56FB File Offset: 0x000C38FB
	protected override void Toggle()
	{
		base.Toggle();
		this.smi.SetSwitchState(this.switchedOn);
	}

	// Token: 0x06002484 RID: 9348 RVA: 0x000C5714 File Offset: 0x000C3914
	protected override void OnRefreshUserMenu(object data)
	{
		if (!this.smi.IsAutomated())
		{
			base.OnRefreshUserMenu(data);
		}
	}

	// Token: 0x06002485 RID: 9349 RVA: 0x000C572A File Offset: 0x000C392A
	protected override void UpdateSwitchStatus()
	{
	}

	// Token: 0x06002486 RID: 9350 RVA: 0x000C572C File Offset: 0x000C392C
	private static string ResolveInfoStatusItemString(string format_str, object data)
	{
		Gantry.Instance instance = (Gantry.Instance)data;
		string text = (instance.IsAutomated() ? BUILDING.STATUSITEMS.GANTRY.AUTOMATION_CONTROL : BUILDING.STATUSITEMS.GANTRY.MANUAL_CONTROL);
		string text2 = (instance.IsExtended() ? BUILDING.STATUSITEMS.GANTRY.EXTENDED : BUILDING.STATUSITEMS.GANTRY.RETRACTED);
		return string.Format(text, text2);
	}

	// Token: 0x04001507 RID: 5383
	public static readonly HashedString PORT_ID = "Gantry";

	// Token: 0x04001508 RID: 5384
	[MyCmpReq]
	private Building building;

	// Token: 0x04001509 RID: 5385
	[MyCmpReq]
	private FakeFloorAdder fakeFloorAdder;

	// Token: 0x0400150A RID: 5386
	private Gantry.Instance smi;

	// Token: 0x0400150B RID: 5387
	private static StatusItem infoStatusItem;

	// Token: 0x020011FD RID: 4605
	public class States : GameStateMachine<Gantry.States, Gantry.Instance, Gantry>
	{
		// Token: 0x0600789E RID: 30878 RVA: 0x002BFEF8 File Offset: 0x002BE0F8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.extended;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.retracted_pre.Enter(delegate(Gantry.Instance smi)
			{
				smi.SetActive(true);
			}).Exit(delegate(Gantry.Instance smi)
			{
				smi.SetActive(false);
			}).PlayAnim("off_pre")
				.OnAnimQueueComplete(this.retracted);
			this.retracted.PlayAnim("off").ParamTransition<bool>(this.should_extend, this.extended_pre, GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.IsTrue);
			this.extended_pre.Enter(delegate(Gantry.Instance smi)
			{
				smi.SetActive(true);
			}).Exit(delegate(Gantry.Instance smi)
			{
				smi.SetActive(false);
			}).PlayAnim("on_pre")
				.OnAnimQueueComplete(this.extended);
			this.extended.Enter(delegate(Gantry.Instance smi)
			{
				smi.master.SetWalkable(true);
			}).Exit(delegate(Gantry.Instance smi)
			{
				smi.master.SetWalkable(false);
			}).PlayAnim("on")
				.ParamTransition<bool>(this.should_extend, this.retracted_pre, GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.IsFalse)
				.ToggleTag(GameTags.GantryExtended);
		}

		// Token: 0x04005C9F RID: 23711
		public GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.State retracted_pre;

		// Token: 0x04005CA0 RID: 23712
		public GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.State retracted;

		// Token: 0x04005CA1 RID: 23713
		public GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.State extended_pre;

		// Token: 0x04005CA2 RID: 23714
		public GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.State extended;

		// Token: 0x04005CA3 RID: 23715
		public StateMachine<Gantry.States, Gantry.Instance, Gantry, object>.BoolParameter should_extend;
	}

	// Token: 0x020011FE RID: 4606
	public class Instance : GameStateMachine<Gantry.States, Gantry.Instance, Gantry, object>.GameInstance
	{
		// Token: 0x060078A0 RID: 30880 RVA: 0x002C0084 File Offset: 0x002BE284
		public Instance(Gantry master, bool manual_start_state)
			: base(master)
		{
			this.manual_on = manual_start_state;
			this.operational = base.GetComponent<Operational>();
			this.logic = base.GetComponent<LogicPorts>();
			base.Subscribe(-592767678, new Action<object>(this.OnOperationalChanged));
			base.Subscribe(-801688580, new Action<object>(this.OnLogicValueChanged));
			base.smi.sm.should_extend.Set(true, base.smi, false);
		}

		// Token: 0x060078A1 RID: 30881 RVA: 0x002C010A File Offset: 0x002BE30A
		public bool IsAutomated()
		{
			return this.logic.IsPortConnected(Gantry.PORT_ID);
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x002C011C File Offset: 0x002BE31C
		public bool IsExtended()
		{
			if (!this.IsAutomated())
			{
				return this.manual_on;
			}
			return this.logic_on;
		}

		// Token: 0x060078A3 RID: 30883 RVA: 0x002C0133 File Offset: 0x002BE333
		public void SetSwitchState(bool on)
		{
			this.manual_on = on;
			this.UpdateShouldExtend();
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x002C0142 File Offset: 0x002BE342
		public void SetActive(bool active)
		{
			this.operational.SetActive(this.operational.IsOperational && active, false);
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x002C015D File Offset: 0x002BE35D
		private void OnOperationalChanged(object data)
		{
			this.UpdateShouldExtend();
		}

		// Token: 0x060078A6 RID: 30886 RVA: 0x002C0168 File Offset: 0x002BE368
		private void OnLogicValueChanged(object data)
		{
			LogicValueChanged logicValueChanged = (LogicValueChanged)data;
			if (logicValueChanged.portID != Gantry.PORT_ID)
			{
				return;
			}
			this.logic_on = LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue);
			this.UpdateShouldExtend();
		}

		// Token: 0x060078A7 RID: 30887 RVA: 0x002C01A8 File Offset: 0x002BE3A8
		private void UpdateShouldExtend()
		{
			if (!this.operational.IsOperational)
			{
				return;
			}
			if (this.IsAutomated())
			{
				base.smi.sm.should_extend.Set(this.logic_on, base.smi, false);
				return;
			}
			base.smi.sm.should_extend.Set(this.manual_on, base.smi, false);
		}

		// Token: 0x04005CA4 RID: 23716
		private Operational operational;

		// Token: 0x04005CA5 RID: 23717
		public LogicPorts logic;

		// Token: 0x04005CA6 RID: 23718
		public bool logic_on = true;

		// Token: 0x04005CA7 RID: 23719
		private bool manual_on;
	}
}
