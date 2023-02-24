using System;

// Token: 0x0200046E RID: 1134
public class RanchableMonitor : GameStateMachine<RanchableMonitor, RanchableMonitor.Instance, IStateMachineTarget, RanchableMonitor.Def>
{
	// Token: 0x06001919 RID: 6425 RVA: 0x00086245 File Offset: 0x00084445
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToGetRanched, (RanchableMonitor.Instance smi) => smi.ShouldGoGetRanched(), null);
	}

	// Token: 0x020010B1 RID: 4273
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020010B2 RID: 4274
	public new class Instance : GameStateMachine<RanchableMonitor, RanchableMonitor.Instance, IStateMachineTarget, RanchableMonitor.Def>.GameInstance
	{
		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06007406 RID: 29702 RVA: 0x002B224D File Offset: 0x002B044D
		// (set) Token: 0x06007407 RID: 29703 RVA: 0x002B2255 File Offset: 0x002B0455
		public ChoreConsumer ChoreConsumer { get; private set; }

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06007408 RID: 29704 RVA: 0x002B225E File Offset: 0x002B045E
		public Navigator NavComponent
		{
			get
			{
				return this.navComponent;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06007409 RID: 29705 RVA: 0x002B2266 File Offset: 0x002B0466
		public RanchedStates.Instance States
		{
			get
			{
				if (this.states == null)
				{
					this.states = this.controller.GetSMI<RanchedStates.Instance>();
				}
				return this.states;
			}
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x002B2287 File Offset: 0x002B0487
		public Instance(IStateMachineTarget master, RanchableMonitor.Def def)
			: base(master, def)
		{
			this.ChoreConsumer = base.GetComponent<ChoreConsumer>();
			this.navComponent = base.GetComponent<Navigator>();
		}

		// Token: 0x0600740B RID: 29707 RVA: 0x002B22A9 File Offset: 0x002B04A9
		public bool ShouldGoGetRanched()
		{
			return this.TargetRanchStation != null && this.TargetRanchStation.IsRunning() && this.TargetRanchStation.HasRancher;
		}

		// Token: 0x04005877 RID: 22647
		public RanchStation.Instance TargetRanchStation;

		// Token: 0x04005878 RID: 22648
		private Navigator navComponent;

		// Token: 0x04005879 RID: 22649
		private RanchedStates.Instance states;
	}
}
