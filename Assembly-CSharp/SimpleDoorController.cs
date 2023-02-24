using System;

// Token: 0x02000961 RID: 2401
public class SimpleDoorController : GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>
{
	// Token: 0x060046FD RID: 18173 RVA: 0x0018FA18 File Offset: 0x0018DC18
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inactive;
		this.inactive.TagTransition(GameTags.RocketOnGround, this.active, false);
		this.active.DefaultState(this.active.closed).TagTransition(GameTags.RocketOnGround, this.inactive, true).Enter(delegate(SimpleDoorController.StatesInstance smi)
		{
			smi.Register();
		})
			.Exit(delegate(SimpleDoorController.StatesInstance smi)
			{
				smi.Unregister();
			});
		this.active.closed.PlayAnim((SimpleDoorController.StatesInstance smi) => smi.GetDefaultAnim(), KAnim.PlayMode.Loop).ParamTransition<int>(this.numOpens, this.active.opening, (SimpleDoorController.StatesInstance smi, int p) => p > 0);
		this.active.opening.PlayAnim("enter_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.active.open);
		this.active.open.PlayAnim("enter_loop", KAnim.PlayMode.Loop).ParamTransition<int>(this.numOpens, this.active.closedelay, (SimpleDoorController.StatesInstance smi, int p) => p == 0);
		this.active.closedelay.ParamTransition<int>(this.numOpens, this.active.open, (SimpleDoorController.StatesInstance smi, int p) => p > 0).ScheduleGoTo(0.5f, this.active.closing);
		this.active.closing.PlayAnim("enter_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.active.closed);
	}

	// Token: 0x04002F08 RID: 12040
	public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State inactive;

	// Token: 0x04002F09 RID: 12041
	public SimpleDoorController.ActiveStates active;

	// Token: 0x04002F0A RID: 12042
	public StateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.IntParameter numOpens;

	// Token: 0x02001760 RID: 5984
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001761 RID: 5985
	public class ActiveStates : GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State
	{
		// Token: 0x04006CDF RID: 27871
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State closed;

		// Token: 0x04006CE0 RID: 27872
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State opening;

		// Token: 0x04006CE1 RID: 27873
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State open;

		// Token: 0x04006CE2 RID: 27874
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State closedelay;

		// Token: 0x04006CE3 RID: 27875
		public GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.State closing;
	}

	// Token: 0x02001762 RID: 5986
	public class StatesInstance : GameStateMachine<SimpleDoorController, SimpleDoorController.StatesInstance, IStateMachineTarget, SimpleDoorController.Def>.GameInstance, INavDoor
	{
		// Token: 0x06008AC4 RID: 35524 RVA: 0x002FE1B5 File Offset: 0x002FC3B5
		public StatesInstance(IStateMachineTarget master, SimpleDoorController.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008AC5 RID: 35525 RVA: 0x002FE1C0 File Offset: 0x002FC3C0
		public string GetDefaultAnim()
		{
			KBatchedAnimController component = base.master.GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				return component.initialAnim;
			}
			return "idle_loop";
		}

		// Token: 0x06008AC6 RID: 35526 RVA: 0x002FE1F0 File Offset: 0x002FC3F0
		public void Register()
		{
			int num = Grid.PosToCell(base.gameObject.transform.GetPosition());
			Grid.HasDoor[num] = true;
		}

		// Token: 0x06008AC7 RID: 35527 RVA: 0x002FE220 File Offset: 0x002FC420
		public void Unregister()
		{
			int num = Grid.PosToCell(base.gameObject.transform.GetPosition());
			Grid.HasDoor[num] = false;
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06008AC8 RID: 35528 RVA: 0x002FE24F File Offset: 0x002FC44F
		public bool isSpawned
		{
			get
			{
				return base.master.gameObject.GetComponent<KMonoBehaviour>().isSpawned;
			}
		}

		// Token: 0x06008AC9 RID: 35529 RVA: 0x002FE266 File Offset: 0x002FC466
		public void Close()
		{
			base.sm.numOpens.Delta(-1, base.smi);
		}

		// Token: 0x06008ACA RID: 35530 RVA: 0x002FE280 File Offset: 0x002FC480
		public bool IsOpen()
		{
			return base.IsInsideState(base.sm.active.open) || base.IsInsideState(base.sm.active.closedelay);
		}

		// Token: 0x06008ACB RID: 35531 RVA: 0x002FE2B2 File Offset: 0x002FC4B2
		public void Open()
		{
			base.sm.numOpens.Delta(1, base.smi);
		}
	}
}
